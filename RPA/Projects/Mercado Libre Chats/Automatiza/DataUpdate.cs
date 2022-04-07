using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Automatiza
{
    class DataUpdate
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int SinTL = 1;
        bool LOG = false;
        public void UpdateInfo(int row, Info2 infor)
        {
            if (LOG) log.Info(row.ToString() + " " + infor.nome + " " + infor.estado + " " + infor.chat1 + " " + infor.chat2 + " " + infor.chat3);

            string where1 = "LoginName='" + infor.logName + "'";
            DataRow[] dr = Data.dtAgents.Select(where1);
            if (dr.Length >0)
            {
                dr[0]["LastUpdate"] = DateTime.Now.Day.ToString() + " - " + DateTime.Now.ToShortTimeString();

                dr[0]["Chat1"] = infor.chat1;
                dr[0]["Chat2"] = infor.chat2;
                dr[0]["Chat3"] = infor.chat3;

                dr[0]["ChatID1"] = infor.ChatID1;
                dr[0]["ChatID2"] = infor.ChatID2;
                dr[0]["ChatID3"] = infor.ChatID3;

                dr[0]["Status"] = infor.estado;
                dr[0]["next"] = infor.next;
                dr[0]["loginTime"] = infor.logear;
                return;
            }


            //foreach (DataRow dr3 in Data.dtAgents.Rows)
            //{
            //    if (dr3["LoginName"].ToString() == infor.logName)
            //    {
            //        dr3["Chat1"] = infor.chat1;
            //        dr3["Chat2"] = infor.chat2;
            //        dr3["Chat3"] = infor.chat3;

            //        dr3["Status2"] = infor.estado;
            //        dr3["next"] = infor.next;
            //        dr3["loginTime"] = infor.logear;
            //        dr3["LastUpdate"] = DateTime.Now.Day.ToString() + " - " + DateTime.Now.ToShortTimeString();
            //        return;
            //    }
            //}

            log.Info("Nuevo a Base");
            DataRow dr2 = Data.dtAgents.NewRow();
            dr2["nome"] = infor.nome;
            dr2["LoginName"] = infor.logName;
            dr2["Status2"] = infor.estado;
            dr2["TL"] = SinTL;

            dr2["Chat1"] = infor.chat1;
            dr2["Chat2"] = infor.chat2;
            dr2["Chat3"] = infor.chat3;

            dr2["ChatID1"] = infor.ChatID1;
            dr2["ChatID2"] = infor.ChatID2;
            dr2["ChatID3"] = infor.ChatID3;

            dr2["next"] = infor.next;
            dr2["loginTime"] = infor.logear;
            dr2["LastUpdate"] = DateTime.Now.Day.ToString() + " - " + DateTime.Now.ToShortTimeString();

            log.Info("****** NOT FOUND " + infor.nome);
            Data.dtAgents.Rows.Add(dr2);
            log.Info("NUEVO " + infor.nome + " " + infor.logName);

            //Bulk();
            //return;

            //SendEmail send = new SendEmail();
            //send.SendEmails("MeLi, missing agent", infor.nome + " " + infor.logName + " not found in Database ");


            //string sql = "insert into ML_agents (nome, LoginName, TL, Chat1, Chat2, chat3, Status, next, loginTime) values ";
            //sql = sql + "( '" + infor.nome + "','";
            //sql = sql + infor.logName + "', SinTL ,";

            //sql = sql + infor.chat1 + ","+ infor.chat2 + "," + infor.chat3 + ",'";

            //sql = sql + infor.estado + "','";
            //sql = sql + infor.next + "','";
            //sql = sql + infor.logear + "',)";

            //ExecuteNonQuery(sql);

            //Data.dtAgents = null;
            //Data.getAgents();
        }

        public void Bulk()
        {
            ExecuteNonQuery("Truncate Table ML_Agents");

            string StrConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(StrConn))

            {
                conn.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(conn))
                {
                    s.DestinationTableName = "ML_Agents";

                    foreach (var column in Data.dtAgents.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());

                    s.WriteToServer(Data.dtAgents);
                }
            }
        }

        public void NewTracker(Info2 infor, string chatNo)
        {
            string sql = "";

            if (chatNo.Trim() != "")
            {
                sql = "Insert into ML_Tracker (EmpID, Status, CaseNo) ";
                sql = sql + " values (" + infor.EmpID + ",'" + infor.estado + "'," + chatNo + ")";
            }
            else
            {
                sql = "Insert into ML_Tracker (EmpID, Status) ";
                sql = sql + " values (" + infor.EmpID + ",'" + infor.estado + "')";
            }

            ExecuteNonQuery(sql);
        }

        public  void ExecuteNonQuery(string sql)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            SqlConnection conn = new SqlConnection(StrConn);
            try
            {
                conn.Open();
                SqlCommand dbComm = new SqlCommand(sql, conn);
                dbComm.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception)
            {
                log.Info("ERROR " + sql);
                try
                {
                    conn.Close();
                }
                catch (Exception) { }
            }
        }
    }
}
