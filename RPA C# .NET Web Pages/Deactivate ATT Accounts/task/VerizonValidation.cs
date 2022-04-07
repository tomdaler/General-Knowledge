using System;
using Microsoft.Exchange.WebServices.Data;
using System.Configuration;
using System.Net.Mail;
using System.Data;
using System.IO;
using System.Linq;
using log4net;

// working folder c:\temp\verizon
// sharefolder for account attuid  \\Cdcawv262\sirius_radio
// PDF file  C:\\Temp\\Verizon\\WPM Profiling Request_Remedy Ticket Submission.pdf

    
namespace Task
{
    public class VerizonValidation
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void prueba()
        {
            string ff = "C:\\Temp\\Verizon\\Issue.xlsx";
            string ff2 = "C:\\Temp\\Verizon\\output.csv";

            try {  System.IO.File.Delete(ff2);  }
            catch (Exception)  {  }

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(ff);
            wb.SaveAs(ff2, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSVWindows);
            wb.Close(false);
            app.Quit();

            DataTable dt = new DataTable();
            dt.Columns.Add("Old", typeof(string));
            dt.Columns.Add("New", typeof(string));
            int i = 0;

            using (var reader = new StreamReader(ff2))
            {
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (i++ > 0)
                        {
                            string f1 = values[3].ToString();
                            string f2 = values[4].ToString();
                            dt.Rows.Add(f1, f2);
                        }
                    }
                }
            }
        }

        #region File1 SIRIUS_XMUSOU
        private DataTable JoinTables(DataTable dt2, DataTable dt1)
        {

            var dtJoin = (from p in dt1.AsEnumerable()
                          join t in dt2.AsEnumerable()
                          on p.Field<string>("StarrID") equals t.Field<string>("StarrID")
                          select new
                          {
                              Manager = p.Field<string>("Manager"),
                              Email = p.Field<string>("Email"),
                              EmplID = p.Field<string>("EmplID"),
                              Verizon = t.Field<string>("EmplID"),
                              EmpName = p.Field<string>("Employee"),
                              StarrID = p.Field<string>("StarrID"),
                              SITE = p.Field<string>("SITE"),
                              LOB = p.Field<string>("LOB")
                          }).ToList();

            DataTable dtFit = DataAccess.CreateDT();
            foreach (var item in dtJoin)
            {
                string f1 = item.Manager;
                string f2 = item.Email;

                string f3 = item.EmplID;
                string f4 = item.EmpName;

                string f5 = item.Verizon;
                string f6 = item.StarrID;

                string f7 = item.SITE;
                string f8 = item.LOB;

                dtFit.Rows.Add(f1, f2, f3, f4, f5, f6, f7, f8);
            }
            return dtFit;
        }

        private string Extract(string str)
        {
            int pos1 = str.IndexOf(")");

            int pos2 = str.LastIndexOf("_");
            str = str.Substring(0, pos1) + "  -  " + str.Substring(pos2 + 1);
            pos1 = str.IndexOf("(");
            str = str.Substring(0, pos1) + str.Substring(pos1 + 5); ;
            str = str.Replace("_", "  ");

            return str.Trim();
        }

        public void File1(EmailMessage email)
        {
            string filename = @"C:\Temp\Verizon\Agent Efficiency Elements - Prior Day by Interval - CVG.csv";

            if (email != null) Download(filename, email);
            log.Info("Downloaded");

            // LOAD IN TABLE StarrIDS
            DataTable dtEmpID_StarrID = DataAccessUDW.GetStarrIDs();
            log.Info("Get UDW");

            DataTable dtInfo = DataAccessUDW.GetManagerInfomation();

            DataTable dtFix = JoinTables(dtEmpID_StarrID, dtInfo);
            log.Info(" ");
            log.Info("Records File1 " + dtFix.Rows.Count.ToString());
            int TotRecords = 0;
            DateTime dtInner = DateTime.Now;

            if (dtFix.Rows.Count < 100)
            {
                string msg2 = " File " + filename + " (2) was not loaded";
                string msg3 = msg2 + " because it is not possible to coneect to UDW";
                log.Info(msg3);
                SendEmail(msg2, msg3, "");
                log.Info(msg2);
                DataAccess.UpdateMonitor("Not loaded", "7");
                return;
            }

            System.Data.DataTable dtReport = DataAccess.CreateDT();
            System.Text.StringBuilder sbFile = new System.Text.StringBuilder();

            string sbNotFoundVerizon = "";
            string sbNotFoundStarrID = "";

            try
            {
                int count = 0;
                using (var reader = new StreamReader(filename))
                {
                    {
                        while (!reader.EndOfStream)
                        {
                            TotRecords++;
                            if (TotRecords > 449)
                            {
                                // int j = 0;
                            }
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            string fec1 = values[0];
                            try
                            {
                                dtInner = System.Convert.ToDateTime(fec1);
                                string fec111 = dtInner.ToString("MM/dd/yyyy HH:mm");
                                string line2 = line.ToString();
                                line2 = line2.Replace(fec1, fec111);
                                line2 = line2.Replace("�", "");
                                line = line2;
                            }
                            catch (Exception) { }

                            values = line.Split(',');

                            if (count == 108 || count == 137)
                            {
                                // int i = 2;
                            }

                            if (count++ > 0)
                            {
                                //ChinYet_3005699, JoannChin (CB_JYet_17998CB)
                                string EmplData = values[6];

                                int pos1 = EmplData.LastIndexOf("_");
                                string StarrID = EmplData.Substring(pos1 + 1);

                                // 17998CB
                                StarrID = StarrID.Replace(")", "");
                                StarrID = StarrID.Replace("\"", "");

                                //3005699
                                string empID = values[7];
                                pos1 = empID.LastIndexOf("_");
                                empID = empID.Substring(pos1 + 1);

                                string SITE = values[2];
                                string LOB = values[3];
                                LOB = LOB.Replace("Care_", "");
                                pos1 = LOB.IndexOf("_");
                                LOB = LOB.Substring(0, pos1);

                                // special characters

                                string where1 = "Verizon = '" + empID + "' ";
                                DataRow[] result = dtFix.Select(where1);
                                if (result.Length > 0)
                                {
                                    string UDW_StarrID = result[0][5].ToString();
                                    if (UDW_StarrID != StarrID)
                                    {
                                        // DIFFERENT STARRIDs
                                        string EmplData2 = EmplData.Replace(StarrID, UDW_StarrID);
                                        line = line.Replace(EmplData, EmplData2);

                                        string f0 = result[0][0].ToString(); // manager
                                        string f1 = result[0][1].ToString();  // email

                                        string f2 = result[0][2].ToString();  // Emp ID
                                        string f3 = result[0][3].ToString();  // Emp Name
                                        // 4 Verizon ID

                                        dtReport.Rows.Add(f0, f1, f2, f3, StarrID, UDW_StarrID, SITE, LOB);
                                        sbFile.Append(line + System.Environment.NewLine);
                                    }
                                }
                                else
                                {
                                    // not found
                                    string whichOne = values[6] + " " + values[7];
                                    whichOne = Extract(whichOne);
                                    string where2 = "StarrID = '" + StarrID + "' ";

                                    string EmpName = whichOne.Replace(StarrID, "");
                                    EmpName = EmpName.Replace(empID, "");
                                    EmpName = EmpName.Replace("-", "");

                                    EmpName = EmpName.Replace(" B A", "");
                                    EmpName = EmpName.Replace(" B B", "");
                                    EmpName = EmpName.Replace(" B C", "");
                                    EmpName = EmpName.Replace(" B D", "");
                                    EmpName = EmpName.Replace(" B E", "");
                                    EmpName = EmpName.Replace(" B F", "");
                                    EmpName = EmpName.Replace(" B G", "");
                                    EmpName = EmpName.Replace(" B H", "");
                                    EmpName = EmpName.Replace(" B I", "");
                                    EmpName = EmpName.Replace(" B J", "");
                                    EmpName = EmpName.Replace(" B K", "");
                                    EmpName = EmpName.Replace(" B L", "");
                                    EmpName = EmpName.Replace(" B M", "");
                                    EmpName = EmpName.Replace(" B N", "");
                                    EmpName = EmpName.Replace(" B O", "");
                                    EmpName = EmpName.Replace(" B P", "");
                                    EmpName = EmpName.Replace(" B Q", "");
                                    EmpName = EmpName.Replace(" B R", "");
                                    EmpName = EmpName.Replace(" B S", "");
                                    EmpName = EmpName.Replace(" B T", "");
                                    EmpName = EmpName.Replace(" B U", "");
                                    EmpName = EmpName.Replace(" B V", "");
                                    EmpName = EmpName.Replace(" B W", "");
                                    EmpName = EmpName.Replace(" B X", "");
                                    EmpName = EmpName.Replace(" B Y", "");
                                    EmpName = EmpName.Replace(" B Z", "");

                                    EmpName = EmpName.Trim();

                                    EmpName = "<tr><td>" + EmpName + "</td><td>" + empID + "</td><td>" + StarrID + "</td><td>" + SITE + "</td><td>" + LOB + "</td></tr>";

                                    DataRow[] result2 = dtFix.Select(where2);
                                    if (result2.Length > 0)
                                    {
                                        if (sbNotFoundVerizon.IndexOf(EmpName) < 0)
                                            sbNotFoundVerizon = sbNotFoundVerizon + EmpName;
                                    }
                                    else
                                    {
                                        if (sbNotFoundStarrID.IndexOf(EmpName) < 0)
                                            sbNotFoundStarrID = sbNotFoundStarrID + EmpName;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception es)
            {
                string msg2 = " File " + filename + " (2) was not loaded";
                string msg3 = msg2 + " <br><br>" + es.ToString();
                
                SendEmails.SendEmail(msg2, msg3,"");

                log.Info("ERROR 277" + msg3);
                DataAccess.UpdateMonitor("Not loaded", "7");

                return;
            }

            string fec2 = dtInner.ToString("yyyyMMdd");
            filename = @"\\Cdcawv262\sirius_radio\SIRIUS_XMUSOU_" + fec2 + ".csv";
            log.Info("To transfer " + filename);

            try
            {
                System.IO.File.Delete(filename);
            }
            catch (Exception) { }

            try
            {
                System.IO.File.WriteAllText(filename, sbFile.ToString());
            }
            catch (Exception es)
            {
                string msg4 = "This is to inform that " + filename + " (2) was NOT uploaded ";
                msg4 = msg4 + "Kindly take ownership and address this inconvenience as soon as possible. ";
                SendEmail(msg4, msg4 + " <br><br>" + es.ToString(), "");
                log.Info(msg4);
                DataAccess.UpdateMonitor("Error, not loaded", "7");
                return;
            }

            if (sbNotFoundVerizon != "")
            {
                sbNotFoundVerizon = "<table border='1'><thead><th>Employee</th><th>CMS IDs</th><th>Genesys STARR IDs</th><th>Site</th><th>LOB</th></thead><tbody>" + sbNotFoundVerizon + "</table>";
            }

            if (sbNotFoundStarrID != "")
            {
                sbNotFoundStarrID = "<table border='1'><thead><th>Employee</th><th>CMS IDs</th><th>Starr ID</th><th>Site</th><th>LOB</th></thead><tbody>" + sbNotFoundStarrID + "</table>";
            }

            string Change = "";

            // SEND EMAILS
            //log.Info("Send Emails");

            if (File.Exists("C:\\Temp\\Verizon\\WPM Profiling Request_Remedy Ticket Submission.pdf"))
                Change = SendEmailManagers(dtReport);
            else
                SendEmails.SendEmail("Robot Error", "missing file C:\\Temp\\Verizon\\WPM Profiling Request_Remedy Ticket Submission.pdf  ","");

            SendResult(filename, sbNotFoundVerizon, sbNotFoundStarrID, Change);

            string msgInfo = "Records " + TotRecords.ToString();
            log.Info(msgInfo);
            DataAccess.UpdateMonitor(msgInfo, "7");
            log.Info("Finished file1");
        }
        
        private void SendResult(string filename, string NotFound1, string NotFound2, string Change)
        {
            string msg = " File " + filename + " was loaded<br>";
            msg = "";
            if (NotFound1 != "") msg = msg + "<br><b>The following CMS ID's were not found in UDW :</b> <br><br>" + NotFound1;
            if (NotFound2 != "") msg = msg + "<br><b>The following STARR IDs and CMS IDs were not found in UDW :</b> <br><br>" + NotFound2;
            if (Change != "") msg = msg + "<br><br><b>Sirius US STARR IDs Daily Audit Results - UDW vs Genesys :</b> <br><br>" + Change;

            string subject = "STARR IDs Daily Audit Results - UDW vs Genesys " + DateTime.Now.ToString("MM-dd-yyyy");

            try
            {
                SendEmail(subject, msg, "");
            }
            catch (Exception es)
            {
                log.Info(es.ToString());
            }
        }

        private string SendEmailManagers(DataTable dt)
        {
            log.Info("Emails to managers");
            DataView dv = dt.DefaultView;
            dv.Sort = "Site,LOB,Manager,Employee";
            DataTable sortedDT = dv.ToTable();
            string email = "";
            string manager = "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();

            // FOR dt
            sb.Append("<table border='1'><thead>");
            sb.Append("<th>Manager</th>");

            sb.Append("<th>EmplID</th>");
            sb.Append("<th>Employee</th>");
            sb.Append("<th>GENESYS STARR IDs</th>");
            sb.Append("<th>UDW STARR IDs</th>");
            sb.Append("<th>SITE</th>");
            sb.Append("<th>LOB</th>");
            sb.Append("</thead><tbody>");

            // FOR EACH MANAGER
            sb2.Append("<table border='1'><thead>");

            sb2.Append("<th>EmplID</th>");
            sb2.Append("<th>Employee</th>");
            sb2.Append("<th>GENESYS STARR IDs</th>");
            sb2.Append("<th>UDW STARR IDs</th>");
            sb2.Append("<th>SITE</th>");
            sb2.Append("<th>LOB</th>");
            sb2.Append("</thead><tbody>");

            string emp1 = "";
            string manager1 = "";
            string email1 = "";

            foreach (DataRow dr in sortedDT.Rows)
            {
                manager1 = dr[0].ToString();
                email1 = dr[1].ToString();

                string emplID = dr[2].ToString();
                string employee = dr[3].ToString();

                string ST1 = dr[4].ToString();
                string ST2 = dr[5].ToString();

                string SITE = dr[6].ToString();
                string LOB = dr[7].ToString();

                if (manager == "") manager = manager1;

                if (manager != manager1)
                {
                    sb2.Append("</tbody></table>");
                    SendEmailToManager(email, sb2.ToString());
                    sb2.Clear();

                    sb2.Append("<table border='1'><thead>");

                    sb2.Append("<th>EmplID</th>");
                    sb2.Append("<th>Employee</th>");

                    sb2.Append("<th>Verizon Starr ID</th>");
                    sb2.Append("<th>UDW Starr ID</th>");

                    sb2.Append("<th>SITE</th>");
                    sb2.Append("<th>LOB</th>");

                    sb2.Append("</thead><tbody>");
                }

                manager = manager1;
                email = email1;

                if (employee != emp1)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + manager + "</td>");
                    sb.Append("<td>" + emplID + "</td>");
                    sb.Append("<td>" + employee + "</td>");
                    sb.Append("<td>" + ST1 + "</td>");
                    sb.Append("<td>" + ST2 + "</td>");
                    sb.Append("<td>" + SITE + "</td>");
                    sb.Append("<td>" + LOB + "</td>");
                    sb.Append("</tr>");


                    sb2.Append("<tr>");
                    sb2.Append("<td>" + emplID + "</td>");
                    sb2.Append("<td>" + employee + "</td>");
                    sb2.Append("<td>" + ST1 + "</td>");
                    sb2.Append("<td>" + ST2 + "</td>");
                    sb2.Append("<td>" + SITE + "</td>");
                    sb2.Append("<td>" + LOB + "</td>");
                    sb2.Append("</tr>");
                }
                emp1 = employee;
            }

            //if (email.Trim() == "") email = email1;
            //=======================================

            string body = sb2.ToString();
            body = body + "</tbody></table>";
            int pos1 = body.IndexOf("LOB<");
            int largo = body.Length- pos1;

            if (largo > 50 )
                SendEmailToManager(email, body);

            // SUMMARY
            sb.Append("</tbody></table>");
            return sb.ToString();
        }

        public void SendEmailToManager(string manager, string body)
        {
            if (manager is null) manager = "";
            if (manager.Trim() == "")
            {
                //SendEmails.SendEmail("Missing Manager", manager + " no manager for " + body);
                return;
            }


            SmtpClient client = null;
            MailMessage message = null;

            try
            {
                client = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"].ToString());
                client.UseDefaultCredentials = false;

                message = new MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"].ToString());
                message.Subject = "STARR IDs Daily Audit Result - Missing/Mismatching " + DateTime.Now.ToString("MM-dd-yyyy"); ;

                string msg = "Kindly identify the correct STARR ID and proceed accordingly.<br><br>";
                msg = msg + "<b>i. If the incorrect STARR ID is the one in UDW,</b> submit a WPM ticket following the instructions below:<br>";
                msg = msg + "  1.Login to Remedy using your Concentrix SSO Credentials <br>";
                msg = msg + "  2.On the left hand side, select “Applications” under “Service Request Management <br> ";
                msg = msg + "  3.Select “Request Entry <br> ";
                msg = msg + "  4.Search for “WPM Profiling Request <br> ";
                msg = msg + "  5.Click the link under Requests – “WPM Profiling <b>Request</b>“. <br> ";
                msg = msg + "  6.Select the correct template and attach with required information. <br> ";
                msg = msg + "  7.Fill the required information and click on “Submit”.  <br><br> ";

                msg = msg + "<b>ii. If the incorrect STARR ID is the one in the Verizon file,</b> please engage the WFM team( ML.SiriusXM.WFM.F&S@convergys.com ) to correct mismatched STARR ID on client - side.<br><br>" + body;

                message.Body = msg;
                message.To.Add(new MailAddress(manager));

                message.IsBodyHtml = true;

                if (msg.IndexOf("Breton") > 0)
                {
                    message.CC.Add(new MailAddress("Gerard.Gouthro@concentrix.com"));
                }

                // TOMAS
                //string CC = ConfigurationManager.AppSettings["EmailToMgrs"].ToString();

                //if (CC != "")
                //{
                //    string[] cc = CC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (var address in cc)
                //    {
                //        if (!string.IsNullOrEmpty(address)) message.CC.Add(new MailAddress(address + "@concentrix.com"));
                //    }
                //}

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment("C:\\Temp\\Verizon\\WPM Profiling Request_Remedy Ticket Submission.pdf");
                message.Attachments.Add(attachment);
                client.Send(message);
            }
            catch (SmtpException smtpex)
            {
                log.Info("ERROR smtp error " + smtpex.ToString());
                throw smtpex;
            }
            catch (Exception ex)
            {
                log.Info("ERROR EMAIL" + ex.ToString());
                throw ex;
            }
            finally
            {
                if (message != null)
                    message.Dispose();

                if (client != null)
                    client.Dispose();
            }
        }
        #endregion


        #region File2  SXM_CALL DETAIL  -> SIRIUS_XMIBSALES
        public void File2(EmailMessage email)
        {
            string filename = @"C:\Temp\Verizon\SXM_Call_Detail_Convergys.csv";
            if (email != null) Download(filename, email);

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Resource Name", typeof(string));
            dt.Columns.Add("DATE_OF_CALL", typeof(string));

            dt.Columns.Add("INBOUNDNUMBER", typeof(long));
            dt.Columns.Add("ANI", typeof(long));
            dt.Columns.Add("NCID", typeof(string));

            dt.Columns.Add("DURATION", typeof(int));
            dt.Columns.Add("TIME_TO_ANSWER", typeof(int));
            dt.Columns.Add("HANDLE_TIME", typeof(int));
            dt.Columns.Add("WRAP_TIME", typeof(int));

            dt.Columns.Add("VCB_STATUS", typeof(string));
            dt.Columns.Add("CALL_TYPE", typeof(string));

            string fec2 = ""; // DateTime.Now.ToString("yyyyMMdd");

            try
            {
                int count = 0;
                using (var reader = new StreamReader(filename))
                {
                    {
                        while (!reader.EndOfStream)
                        {
                            if (count > 449)
                            {
                                //int j = 0;
                            }
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            string NCID = "";

                            try
                            {
                                var NCID2 = values[5];
                                NCID = NCID2.ToString();
                            }
                            catch (Exception)
                            {
                                //int i = 0;
                                continue;
                            }

                            if (fec2 == "")
                            {
                                try
                                {
                                    fec2 = values[2];
                                    int pos1 = fec2.IndexOf(" ");
                                    if (pos1 > 0) fec2 = fec2.Substring(0, pos1);
                                    DateTime dt1 = System.Convert.ToDateTime(fec2);
                                    fec2 = dt1.ToString("yyyyMMdd");
                                }
                                catch (Exception)
                                {
                                    fec2 = "";
                                }
                            }
                            if (count++ > 0 && NCID != null && NCID != "0")
                            {
                                DataRow dr = dt.NewRow();
                                string names = "";
                                for (int i = 0; i < 12; i++)
                                {
                                    string field = GetValue(values[i]);
                                    field = field.Replace("\"", "");
                                    if (i == 0) names = field;
                                    if (i == 1)
                                    {
                                        names = names + ", " + field;
                                        dr[0] = names;
                                    }

                                    if (i == 2) dr[1] = field;

                                    if (i ==3 || i ==4)
                                    {
                                        if (field == "") field = "0";
                                    }

                                    if (i == 3) dr[2] = System.Convert.ToInt64(field);
                                    if (i == 4) dr[3] = System.Convert.ToInt64(field);
                                                                       
                                    if (i == 5)
                                    {
                                        //double valor = System.Convert.ToDouble(field);
                                        dr[4] = values[i];
                                    }
                                    if (i == 6) dr[5] = field; // GetNumber(field);
                                    if (i == 7) dr[6] = field; // GetNumber(field);
                                    if (i == 8) dr[7] = field; // GetNumber(field);
                                    if (i == 9) dr[8] = field; // GetNumber(field);

                                    if (i == 10) dr[9] = field;
                                    if (i == 11) dr[10] = field;
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            catch (Exception es)
            {

                string msg2 = " File SXM_Call_Detail_Convergys.csv (2) was not loaded";
                string msg3 = msg2 + " " + es.ToString();
                SendEmail(msg2, msg3, "");
                DataAccess.UpdateMonitor(msg2, "8");
                log.Info(msg2);
                return;
            }


            if (dt.Rows.Count < 3)
            {
                SendEmail("EMPTY", "", "");
                DataAccess.UpdateMonitor("Empty", "8");
                return;
            }


            filename = @"\\Cdcawv262\sirius_radio\SIRIUS_XMIBSALES_" + fec2 + ".txt";
            log.Info("Transfer file2 " + filename);

            bool change = true;
            while (change)
            {
                int count1 = 0;
                change = false;

                foreach (DataRow dr in dt.Rows)
                {
                    count1++;
                    string a0 = dr[0].ToString();
                    string a1 = dr[1].ToString();
                    string a3 = dr[3].ToString();

                    int count2 = 0;
                    foreach (DataRow dr2 in dt.Rows)
                    {
                        count2++;
                        if (count1 == count2) continue;
                        string a01 = dr2[0].ToString();
                        string a11 = dr2[1].ToString();
                        string a31 = dr2[3].ToString();

                        if (a0 == a01 && a1 == a11 && a3 == a31)
                        {
                            string cambio = a11;
                            int pos1 = cambio.IndexOf(" ");
                            string fec = cambio.Substring(0, pos1);
                            string hora = cambio.Substring(pos1 + 1);
                            int hora2 = System.Convert.ToInt32(hora) + 1;
                            cambio = fec + " " + hora2.ToString("00##");
                            dr[1] = cambio;
                            a1 = cambio;
                            change = true;
                        }
                    }
                }
                if (change) dt.AcceptChanges();
            }

            log.Info("Second step");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Resource Name\tDATE_OF_CALL\tINBOUNDNUMBER\tANI\tNCID\tDURATION\tTIME_TO_ANSWER\tHANDLE_TIME\tWRAP_TIME\tVCB_STATUS\tCALL_TYPE" + System.Environment.NewLine);
            foreach (DataRow dr in dt.Rows)
            {
                string line = "";
                for (int i = 0; i < 11; i++)
                {
                    string value = dr[i].ToString();

                    if (i == 0) value = "\"" + value + "\"";
                                        
                    if (i < 10)
                        line = line + value + "\t";
                    else
                        line = line + value + System.Environment.NewLine;
                }
                sb.Append(line);
            }

            try
            {
                System.IO.File.Delete(filename);
            }
            catch (Exception)
            {

            }

            try
            {
                System.IO.File.WriteAllText(filename, sb.ToString());
                log.Info("Saved " + filename);
            }
            catch (Exception es)
            {
                string msg4 = "This is to inform that " + filename + " (1) was NOT uploaded ";
                string msg44 = msg4 + "Kindly take ownership and address this inconvenience as soon as possible. ";
                SendEmail(msg4, msg44 + " <br><br> " + es.ToString(), "");
                log.Info("ERROR line 758" + msg4);
                DataAccess.UpdateMonitor(msg4, "8");
                return;
            }

            string msg = " File " + filename + " was loaded";
            log.Info("Sending");
            try
            {
                SendEmail("File loaded", msg, "");
            }
            catch (Exception es)
            {
                log.Info("Error en correo  " + es.ToString());
            }
            log.Info("Loaded");
            DataAccess.UpdateMonitor("Records " + dt.Rows.Count.ToString(), "8");
        }

       int GetNumber(string valor)
        {
            int field = 0;
            try
            {
                field = System.Convert.ToInt32(field);
            }
            catch (Exception) { return 0; }
            return field;
        }

        string GetValue(object valor)
        {
            string field="";
            try
            {
                field = valor.ToString();
            }
            catch(Exception) { return ""; }
            return field;
        }
        

        public void Download(string filename, EmailMessage email)
        {
            FileAttachment fileAttach = email.Attachments[0] as FileAttachment;
            string ff = fileAttach.Name;
            if (!ff.Contains(".csv") && !ff.Contains(".CSV"))
            {

                try
                {
                    fileAttach = email.Attachments[1] as FileAttachment;
                }
                catch (Exception)
                {
                    fileAttach = email.Attachments[0] as FileAttachment;
                }
            }


            try
            {
                System.IO.File.Delete(filename);
            }
            catch (Exception) { }

            FileStream theStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fileAttach.Load(theStream);
            theStream.Dispose();
        }

        public void SendEmail(string subject, string body, string file)
        {
            log.Info("");
            log.Info(subject);
            //log.Info(body);
            log.Info(file);
            log.Info("");

            SmtpClient client = null;
            MailMessage message = null;
            
            try
            {
                client = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"].ToString());
                client.UseDefaultCredentials = false;

                message = new MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"].ToString());
                string input = ConfigurationManager.AppSettings["EmailToMgrs"].ToString();


                if (subject == "EMPTY")
                {
                    subject = "SXM_Call_Detail_Convergys.csv empty file * URGENT * ";
                    body = "</br>The SXM_Call_Detail_Convergys.csv file was received empty today. Kindly review and resend the file as soon as possible. ";
                    input = "brent.neef@verizon.com;William.Wakefield@siriusxm.com;";
                    //CC = "lorena.castilloburgos@concentrix.com;Steve.Lilly@concentrix.com;Andrea.Etheridge@concentrix.com";
                }

                message.Subject = subject;
                message.Body = body;

                message.IsBodyHtml = true;

                if (file != "")
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(file);
                    message.Attachments.Add(attachment);
                }

                if (body.IndexOf("(1)") > 0)
                {
                    message.Subject = "SIRIUS_XMIBSALES_YYYYMMDD.csv did **NOT** process successfully";
                    input = "lorena.castilloburgos@concentrix.com;tomas.dale@concentrix.com";
                }

                if (body.IndexOf("(2)") > 0)
                {
                    message.Subject = "SIRIUS_XMUSOU_YYYYMMDD.csv did **NOT** process successfully";
                    input = "lorena.castilloburgos@concentrix.com;tomas.dale@concentrix.com";
                }

                string mgr = "";
                string[] to = input.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

             
                foreach (var address in to)
                {
                    string email="";
                    mgr = mgr + "  " + address.ToString();
                    try
                    {
                        email = address.ToString().Trim() + "@concentrix.com";
                        if (email.Contains("Warrior.Rock")) continue;
                        if (!string.IsNullOrEmpty(address)) message.To.Add(new MailAddress(email));
                    }
                    catch(Exception)
                    {
                        log.Info("ERROR EMAIL "+email);
                    }
                }

                // INCLUDE THIS EMAIL

                try
                {
                  //  message.To.Add(new MailAddress("ML.SiriusXM.WFM.F&S@concentrix.com"));
                }
                catch (Exception)
                {
                    log.Info("ERROR ML.SiriusXM");
                }

                log.Info("Managers: "+mgr);

                //if (CC != "")
                //{
                //    string[] cc = CC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (var address in cc)
                //    {
                //        log.Info(address.ToString());
                //        if (!string.IsNullOrEmpty(address)) message.CC.Add(new MailAddress(address));
                //    }
                //}

                log.Info("send");
                client.Send(message);
            }
            catch (SmtpException smtpex)
            {
                log.Info("ERROR LINE 887 smtp error " + smtpex.ToString());
                //log.Info(smtpex.ToString());
                throw smtpex;
            }
            catch (Exception ex)
            {
                log.Info("ERROR LINE 893" + ex.ToString());
                throw ex;
            }
            finally
            {
                if (message != null)
                    message.Dispose();

                if (client != null)
                    client.Dispose();
            }
        }

        #endregion

    }
}
