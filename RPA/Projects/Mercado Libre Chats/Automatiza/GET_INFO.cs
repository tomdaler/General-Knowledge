using System;
using System.Data;
using System.Globalization;

namespace Automatiza
{
    class GET_INFO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        bool LOG = false;

        public Info2 GetAgentInfo(string pagina)
        {
            Info2 infor = new Info2();

            int pos1 = pagina.IndexOf("agentData_");
            if (pos1 < 0) return null;

            string logName = pagina.Substring(pos1 + 10);
            int pos3 = logName.IndexOf(" ");
            logName = logName.Substring(0, pos3).ToString();   // <-  logName
            logName = logName.Replace("ext_", "");
            pos3 = logName.IndexOf("<");
            if (pos3 > 0) logName = logName.Substring(0, pos3-1);
            infor.logName = logName;
            //============================

            int pos2 = pagina.IndexOf("<td", pos1);
            string agent = pagina.Substring(pos1 + 10, pos2 - pos1 - 11).Trim();

            pos2 = pagina.IndexOf(agent, pos2 + 2);
            pos2 = pagina.IndexOf(agent, pos2 + 2);
            pos2 = pagina.IndexOf("<td", pos2 + 2);
            string informacion = pagina.Substring(pos2 + 4, 100);

            int pos5 = informacion.IndexOf("<");
            infor.nome = informacion.Substring(0, pos5).Trim();  // <= nome
            
            //infor.nome = infor.nome.Replace("</td>", "");
            if (LOG) log.Info(infor.nome);

            infor.EmpID = GetEmpID(infor);

            pos5 = informacion.IndexOf("<td");
            string estado1 = informacion.Substring(pos5+5);
            pos5 = estado1.IndexOf("<");
            infor.estado = estado1.Substring(0,pos5);

            
            if (infor.estado.Length > 20)
            {
                int pos4 = infor.estado.IndexOf("<td ");
                if (pos4 > 1) infor.estado = infor.estado.Substring(0, pos4);
            }
            infor.estado = infor.estado.Trim();

            pos1 = pagina.IndexOf("HOY-");
            if (pos1 > 1)
            {
                pos2 = pagina.IndexOf("<", pos1);
                infor.logear = pagina.Substring(pos1, pos2 - pos1);
                infor.logear = infor.logear.Replace("HOY- ", "");  // <-- logear
            }

            if (infor.estado == "")
            {
                pos1 = informacion.IndexOf("<td");
                if (pos1 > 1) infor.nome = informacion.Substring(0, pos1).Trim();  // <= nome

                int pos4 = pagina.IndexOf("systematic_failure");
                if (pos4 > 0 && pos4 < pos2) infor.estado = "systematic_failure";
            }

            if (infor.estado != "online")
            {
                CheckTracker(infor);
                return infor;
            }

            pos1 = pagina.IndexOf("-NEXT");
            if (pos1 > 0)
            {
                pagina = pagina.Substring(pos1 + 5);
                pos1 = pagina.IndexOf("</td");
                if (pos1 > 0)
                {
                    string next = pagina.Substring(0, pos1);
                    if (next != "-")
                    {
                        infor.next = next.Trim();
                    }
                }
            }

            int chatNo = 1;
            pagina = pagina.Substring(pos1 + 10);

            while (true)
            {
                pos1 = pagina.IndexOf("<button");
                pos2 = pagina.IndexOf("agentData_");

                if (pos2 < pos1) break;
                if (pos1 < 1 && pos2 < 1) break;
                if (pos1 < 1) break;

                string durar = pagina.Substring(pos1 - 19, 9).Trim();
                durar = durar.Replace("<", "");
                durar = durar.Replace(">", "");

                pagina = pagina.Substring(pos1 + 10);

                // CHATS TIME AND CODE
                //=====================

                double durar2 = 0;
                try
                {
                    TimeSpan tt = Convert.ToDateTime(durar).TimeOfDay;
                    durar2 = tt.Hours * 60;
                    durar2 = durar2 + tt.Minutes;
                    double seg = tt.Seconds;
                    seg = seg / 60;
                    durar2 = durar2 + seg;
                    durar2 = Math.Round((Double)durar2, 2);
                }
                catch (Exception) { durar2 = 0; }

                if (durar2 > 0)
                {
                    if (chatNo == 1) infor.chat1 = durar2;
                    if (chatNo == 2) infor.chat2 = durar2;
                    if (chatNo == 3) infor.chat3 = durar2;

                    pos1 = pagina.IndexOf("javascript:viewChat(");
                    if (pos1 > 0)
                    {
                        pagina = pagina.Substring(pos1 + 20);
                        pos1 = pagina.IndexOf(",");
                        string chatID = pagina.Substring(0, pos1);
                        chatID = chatID.Replace("'", "");
                        if (chatID.Length < 12)
                        {
                            if (chatNo == 1) infor.ChatID1 = chatID;
                            if (chatNo == 2) infor.ChatID2 = chatID;
                            if (chatNo == 3) infor.ChatID3 = chatID;
                        }
                        pagina = pagina.Substring(170);
                    }
                }
                chatNo++;
            }

            //MessageBox.Show(pagina);
            //System.Threading.Thread.Sleep(10000);

            //string filePath2 = @"C:\temp\WriteFile2.txt";

            //using (StreamWriter outputFile = new StreamWriter(filePath2))
            //{
            //    outputFile.WriteLine(pagina);
            //}

            //// CHECK TRACKER
            CheckTracker(infor);
            return infor;
        }

        string GetEmpID(Info2 infor)
        {
            DataTable dtEmp = Data.getAgents();

            DataRow[] dr = dtEmp.Select("LoginName ='" + infor.logName + "'");
            if (dr.Length > 0) return dr[0]["EmpID"].ToString();
                       
            string sql = "INSERT INTO ML_Agents (Nome, LoginName) ";
            sql = sql + " VALUES ('" + infor.nome + "','" + infor.logName + "')";
            DataUpdate du = new DataUpdate();
            du.ExecuteNonQuery(sql);

            sql = "SELECT EmpID FROM ML_Agents WHERE LoginName='" + infor.logName + "'";
            string EmpID = Data.GetScalar(sql);
            log.Info("NUEVO " + infor.EmpID + " " + infor.logName + " " + infor.nome);
            return EmpID;
        }
        
        void CheckTracker(Info2 infor)
        {
            if (infor.EmpID == "") return;
           
            if (LOG) log.Info("Check Tracker " +infor.estado);
            
            DataUpdate du = new DataUpdate();

            DataTable dt = Data.getTracker();
            DataRow[] dr = dt.Select("EmpID =" + infor.EmpID);

            // IF PRIMERO
            if (dr.Length == 0)
            {
                if (infor.estado != "online")
                {
                    du.NewTracker(infor, " ");
                    return;
                }
                if (infor.chat1 != 0) du.NewTracker(infor, infor.ChatID1);
                if (infor.chat2 != 0) du.NewTracker(infor, infor.ChatID2);
                if (infor.chat3 != 0) du.NewTracker(infor, infor.ChatID3);
                return;
            }

            if (infor.estado == "online")
            {
                EachChat(infor, dt, infor.ChatID1);
                EachChat(infor, dt, infor.ChatID1);
                EachChat(infor, dt, infor.ChatID1);
            }
            else
            {
                if (infor.estado != dr[0]["status"].ToString())
                    du.NewTracker(infor, " ");
            }
        }

        void EachChat(Info2 infor, DataTable dt, string CaseNo)
        {
            if (CaseNo == "") return;
            string ss = "EmpID =" + infor.EmpID + " AND CaseNo=" + CaseNo;

            DataRow[] dr = dt.Select(ss);
            if (dr.Length > 0) return;

            DataUpdate du = new DataUpdate();
            du.NewTracker(infor, CaseNo);
        }

        public string CleanPage(string pagina)
        {
            //string filePath = @"C:\temp\WriteFile1.txt";

            //using (StreamWriter outputFile = new StreamWriter(filePath))
            //{
            //    outputFile.WriteLine(pagina);
            //}

            int pos1 = pagina.IndexOf("agentData");
            if (pos1 < 2) return "";
            pagina = pagina.Substring(pos1 - 3);

            pos1 = pagina.IndexOf("Total de");
            pagina = pagina.Substring(0, pos1);

            pagina = pagina.Replace("<option disabled= value=", "");
            pagina = pagina.Replace("<option value=", "");
            pagina = pagina.Replace("</option>", "");

            pagina = pagina.Replace("\n", "");
            pagina = pagina.Replace("\r", "");
            pagina = pagina.Replace("\t", "");
            //pagina = pagina.Replace("onclick=javascript:", "");

            pagina = pagina.Replace("width:100%", "");
            pagina = pagina.Replace("width=100%", "");
            pagina = pagina.Replace("error:function(XMLHttpRequest,textStatus,errorThrown)", "");
            pagina = pagina.Replace("success:function(data,textStatus){jQuery(", "");

            pagina = pagina.Replace("\"", "");

            pagina = pagina.Replace("<option disabled=", "");
            pagina = pagina.Replace("value=training_km>TRAINING KM", "");
            pagina = pagina.Replace("value=shadowing>SHADOWING ", "");
            pagina = pagina.Replace("value=help_low_priority>", "");
            pagina = pagina.Replace("HELP LOW PRIORITY", "");
            pagina = pagina.Replace("OFFLINE  descanso>", "");
            pagina = pagina.Replace("BREAK  evento>", "");
            pagina = pagina.Replace("EVENT  post_chat>", "");
            pagina = pagina.Replace("POST CONTACT  help_exclusive>", "");
            pagina = pagina.Replace("HELP EXCLUSIVE  coaching>", "");
            pagina = pagina.Replace("COACHING  training_ac>", "");
            pagina = pagina.Replace("TRAINING AC", "");
            pagina = pagina.Replace("expert_trainer>", "");
            pagina = pagina.Replace("EXPERT TRAINER  operational_failure>", "");
            pagina = pagina.Replace("operational_failure>OPERATIONAL FAILURE", "");
            pagina = pagina.Replace("selected=&quot;selected&quot;> ", "");
            pagina = pagina.Replace("SYSTEMATIC FAILURE", "");
            pagina = pagina.Replace("offline>OFFLINE", "");
            pagina = pagina.Replace("descanso>BREAK", "");
            pagina = pagina.Replace("evento>EVENT", "");
            pagina = pagina.Replace("post_chat>POST CONTACT", "");
            pagina = pagina.Replace("help_exclusive>HELP EXCLUSIVE", "");
            pagina = pagina.Replace("coaching>COACHING", "");
            pagina = pagina.Replace("training_ac>", "");
            pagina = pagina.Replace("EXPERT TRAINER", "");
            pagina = pagina.Replace("operational_failure>OPERATIONAL FAILURE", "");
            pagina = pagina.Replace("systematic_failure selected=&quot;selected&quot;>", "");

            pagina = pagina.Replace("</select></td><td>", "-NEXT");
            pagina = pagina.Replace("</select></td> <td>", "-NEXT");
            pagina = pagina.Replace("</select></td>  <td>", "-NEXT");
            pagina = pagina.Replace("                                          ", "");


            pagina = pagina.Replace("online selected=&quot;selected&quot;>ONLINE", "");

            pagina = pagina.Replace("online>ONLINE", "");

            pagina = pagina.Replace("<td>Chrome", "");
            pagina = pagina.Replace("Windows", "");

            pagina = pagina.Replace("<a href= class=glyphicon glyphicon-plus>", "");
            pagina = pagina.Replace("{alert('');", "");
            pagina = pagina.Replace("<td>Mobile Safari UI", "");

            pagina = pagina.Replace("     ", " ");
            pagina = pagina.Replace("    ", " ");
            pagina = pagina.Replace("   ", " ");
            pagina = pagina.Replace("  ", " ");
            pagina = pagina.Replace("  ", " ");


            pagina = pagina.Replace("<a href = \"javascript: void(0); \" class=\"glyphicon glyphicon-minus\"></a></td>", "");
            pagina = pagina.Replace("\"", "");

            //pagina = pagina.Replace("<td>-</td>", "");
            //pagina = pagina.Replace("</td>", "");

            pagina = pagina.Replace("<th>", "");
            pagina = pagina.Replace("</th>", "");
            pagina = pagina.Replace(").html(data);},", "");

            pagina = pagina.Replace("<td  STATUS>", "");
            pagina = pagina.Replace("<td >", "");

            pagina = pagina.Replace("<a href= class=glyphicon glyphicon-plus></a>", "");
            pagina = pagina.Replace("<span class=glyphicon glyphicon-off style=color:green;font-size:16px;></span>", "");
            pagina = pagina.Replace("<span class=glyphicon glyphicon-off style=color:rgb(219, 219, 39);font-size:16px;></span>", "");

            pagina = pagina.Replace("<td  STATUS>", "");
            pagina = pagina.Replace("</tr>", "");
            pagina = pagina.Replace("</button>", "");

            pagina = pagina.Replace("</tr><tr><td colspan=10>", "");
            pagina = pagina.Replace("<table class=table text-center text-center cellpadding=5 cellspacing=0 border=0 width=100%>", "TABLE-INFO");

            pagina = pagina.Replace("style=", "");
            pagina = pagina.Replace("display: block; height: 100%;", "");

            pagina = pagina.Replace("<td class=sorting_1><STATUS=online type:'GET',", "");

            pagina = pagina.Replace("hidden=", "");
            pagina = pagina.Replace("javascript:void(0);", "");
            pagina = pagina.Replace("select name=agentStatus class=form-control id=agentStatus value", "STATUS");
            pagina = pagina.Replace("class=sorting_2", "STATUS");
            pagina = pagina.Replace("class=even id=", "");
            pagina = pagina.Replace("role=row>", "");
            pagina = pagina.Replace("onchange=jQuery.ajax({", "");
            pagina = pagina.Replace("type: 'GET',", "");
            pagina = pagina.Replace("font-size:0; class=text-center", "");

            pagina = pagina.Replace("#agentData", "");
            pagina = pagina.Replace("data:", "");
            pagina = pagina.Replace("status:$(this).val(),agentId:", "");

            pagina = pagina.Replace("success: function(data, textStatus){ jQuery('", "");

            pagina = pagina.Replace("error: function(XMLHttpRequest, textStatus, errorThrown){ alert('", "");
            pagina = pagina.Replace("error:function(XMLHttpRequest,textStatus,errorThrown){alert('", "");

            pagina = pagina.Replace("url: '/supervisor/changeStatus',", "");
            pagina = pagina.Replace("url:'/supervisor/changeStatus',", "");

            pagina = pagina.Replace("<td style=font-size:0; class=text-center>", "ESTADO=");

            pagina = pagina.Replace("success:function(data,textStatus){jQuery('#agentData_", "");
            pagina = pagina.Replace("Hubo un error al cambiar de estado.Por favor refresque la pantalla y reintente nuevamente", "");
            pagina = pagina.Replace("\n\n", "");

            pagina = pagina.Replace("type=button class=btn btn-default btn-xs>", "");

            //pagina = pagina.Replace("<td class=sorting_1><STATUS=online type:'GET',", "");
            pagina = pagina.Replace("<td class=sorting_1><STATUS=online type:'GET',", "INFO-CHAT");

            pagina = pagina.Replace("margin-top: 2px; class=btn btn-danger btn-xs", "");

            pagina = pagina.Replace("<td  STATUS>", "");
            pagina = pagina.Replace("<td class=sorting_1><STATUS=systematic_failure type:'GET',", "");
            pagina = pagina.Replace("</div>", "");
            pagina = pagina.Replace("<div id=", "");
            pagina = pagina.Replace("width=100%", "");
            pagina = pagina.Replace("class=glyphicon glyphicon-plus></a>", "");

            string today1 = DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return pagina.Replace(today1, "HOY-");
        }
    }
}
