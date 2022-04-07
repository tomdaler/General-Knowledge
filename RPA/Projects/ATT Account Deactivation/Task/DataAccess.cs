using log4net;
using Microsoft.SharePoint.Client;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Task
{
    public static class DataAccess
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void MacysInsert(string id, string from,
            string subject, string body, DateTime dt_received)
        {
            MacysProcess ma = new MacysProcess();
            string[] result = ma.ReadBody2(body);
            string[] survey = ma.ProcessBody(result);

            subject = subject.Replace("?", "");
            subject = subject.Replace("'", "");
            subject = subject.Replace("\'", "");
            subject = subject.Replace("/'", "");
            subject = subject.Replace("$'", "");
            subject = subject.Replace("#'", "");
            subject = subject.Replace("&'", "");
            
            try
            {
                string SqlConn = ConfigurationManager.ConnectionStrings["Sql28"].ConnectionString;

                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                SqlCommand comm1 = new SqlCommand();
                comm1.Connection = sqlcon;
                comm1.CommandType = CommandType.StoredProcedure;
                comm1.CommandTimeout = 60;
                comm1.CommandText = "usp_MacysCES_SaveRaw";
                comm1.Parameters.AddWithValue("@message_id", id);
                comm1.Parameters.AddWithValue("@from", from);
                comm1.Parameters.AddWithValue("@subject", subject);
                comm1.Parameters.AddWithValue("@body", body);
                comm1.Parameters.AddWithValue("@received_time", dt_received);
                comm1.ExecuteNonQuery();

                //==========================

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlcon;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_MacysCES_SaveEmail2";
                cmd.CommandTimeout = 60;

                if (subject is null) subject = "";
                //log.Info("subject " + subject);

                cmd.Parameters.Add(new SqlParameter("@message_id", id));
                cmd.Parameters.Add(new SqlParameter("@subject", subject));

                if (survey.Length > 0)
                    cmd.Parameters.Add(new SqlParameter("@type", survey[0]));
                else
                    cmd.Parameters.Add(new SqlParameter("@type", DBNull.Value));

                for (int i = 1; i < 12; i++)
                {
                    if (survey[i] is null) survey[i] = string.Empty;
                    // log.Info(i.ToString() + " " + survey[i]);
                }

                cmd.Parameters.Add(new SqlParameter("@date", survey[1]));
                cmd.Parameters.Add(new SqlParameter("@contactid", survey[2]));
                cmd.Parameters.Add(new SqlParameter("@vrukey", survey[3]));
                cmd.Parameters.Add(new SqlParameter("@reservation", survey[4]));
                cmd.Parameters.Add(new SqlParameter("@language1", survey[5]));
                cmd.Parameters.Add(new SqlParameter("@language2", survey[6]));

                cmd.Parameters.Add(new SqlParameter("@q1", survey[7]));
                cmd.Parameters.Add(new SqlParameter("@q2", survey[8]));
                cmd.Parameters.Add(new SqlParameter("@q3", survey[9]));
                cmd.Parameters.Add(new SqlParameter("@q4", survey[10]));
                cmd.Parameters.Add(new SqlParameter("@q5", survey[11]));

                cmd.ExecuteNonQuery();

                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception es)
            {
                log.Info("error " + subject + " " + es.ToString());
                string msgErr = "Error when INSERTING MACYS " + es.ToString();
                SendEmails.SendEmail(msgErr, "Macys error on insert", "");
            }

        }

        public static void DA_Dashboard(string which, string client, int STAT1, int STAT2, int STAT3)
        {
            return;
            string user = System.Environment.UserName;
            string wk = System.Environment.MachineName;
            string LAN = System.Environment.UserDomainName + "\\" + user;

            string sql = "";


            //into DA_Dashboard(RobotName, Type, Client, WorkStation, LANID, STAT01, STAT02, STAT03, LastUpdateBy) ";
            sql = sql + " values ('" + which + "','STAT','" + client + "','" + wk + "','" + LAN + "',";

            sql = sql + STAT1.ToString() + ",";
            sql = sql + STAT2.ToString() + ",";
            sql = sql + STAT3.ToString() + ", '" + user + "' )";

            //  log.Info(sql);
            try
            {
                string SqlConn = ConfigurationManager.ConnectionStrings["Sql28"].ConnectionString;

                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlcon;
                sqlCommand.CommandType = System.Data.CommandType.Text;

                sqlCommand.CommandText = sql;
                sqlCommand.ExecuteNonQuery();

                sql = "  update DA_Robots set LastUpdate = GetDate(), LastUpdateBy = '" + user + "' where RObotName = '" + which + "' ";
                sqlCommand.CommandText = sql;
                sqlCommand.ExecuteNonQuery();

                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception)
            {
                string msgErr = "Error when INSERTING DA_Dashboard " + sql;
                SendEmails.SendEmail("Error updating tbLogs from NotificationRobot ", msgErr, "");
            }

            //Status.LastTime = DateTime.Now;

        }

        public static DataTable CreateDT()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Manager", typeof(string));
            dt.Columns.Add("Email", typeof(string));

            dt.Columns.Add("EmplID", typeof(string));
            dt.Columns.Add("Employee", typeof(string));

            dt.Columns.Add("Verizon", typeof(string));
            dt.Columns.Add("StarrID", typeof(string));

            dt.Columns.Add("SITE", typeof(string));
            dt.Columns.Add("LOB", typeof(string));

            return dt;
        }


        public static void UpdateMonitor(string comment, string ID)
        {
            try
            {
                // CAMBIAR
                string SqlConn = ConfigurationManager.ConnectionStrings["Sql28"].ConnectionString;

                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlcon;
                sqlCommand.CommandText = "upSet_RPA_Monitor";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlCommand.Parameters.AddWithValue("@Comment", comment);

                sqlCommand.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception ws)
            {
                string msgErr = "Error when updating tblLogs exec upSet_RPA_Monitor '" + ID + "', '" + comment + "'  " + ws.ToString();
                SendEmails.SendEmail("Error updating tbLogs from NotificationRobot ", msgErr, "");
            }
        }

        //===================
        //  ATT DEACTIVATION
        //===================
        public static string GetBPID(string EmplID)
        {
            string result = "-";
            OracleConnection connection = null;
            OracleCommand command = null;

            try
            {
                string OracleString = ConfigurationManager.ConnectionStrings["ATTUID"].ConnectionString.ToString();
              
                
                connection = new OracleConnection(ConfigurationManager.ConnectionStrings["ATTUID"].ConnectionString);
                command = new OracleCommand();

                string sql = "SELECT LOGID AS BPID , SRC_PERSON_ID AS EMPLID ";
                sql = sql + " FROM UDW.DIM_XREFPERSON ";
                sql = sql + "  WHERE JOB_SYSTEM_ID = '44160755' ";
                sql = sql + " AND SRC_PERSON_ID = '" + EmplID.Trim() + "' ";
                sql = sql + " AND SYSDATE >= DW_START_DT ";
                sql = sql + " AND DW_END_DT >= '31-DEC-99' ";

                command.Connection = connection;
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                connection.Open();
                var BPID = command.ExecuteScalar();
                result = "";

                if (BPID != null) result = BPID.ToString();
                result = result.Trim();
                if (result == "-") result = "";
                return result;
            }
            catch (Exception es)
            {
                log.Error("Oracle Error from notification robot " + es.ToString());
               
            }
            finally
            {
                if (command != null)
                    command.Dispose();

                if (connection != null)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();

                    connection.Dispose();
                }
            }
            return result;
        }

        public static string CheckFieldGlassStatus(string ATTUID)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString);
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "ATT_CheckFieldGlassStatus";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ATTUID", ATTUID);

                var attuid = command.ExecuteScalar();
                connection.Close();

                if (attuid == null)
                    attuid = "";

                return attuid.ToString();
            }
            catch (Exception es)
            {
                log.Error("When searching ATT_CheckFieldGlassStatus " + es.ToString());
                return "Error";
            }
            finally
            {
                if (command != null)
                    command.Dispose();

                if (connection != null)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();

                    connection.Dispose();
                }
            }
        }

        static public void UpdateNotLoaded()
        {
            string sqlconn = ConfigurationManager.ConnectionStrings["Sql28"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(sqlconn))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM RPA_ATT_NotLoaded where loaded = 0 ";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    string lista = "";

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        string ATTUID = dr.GetString(1).Trim();
                        string EmplID = dr.GetString(2).Trim();
                        string name1 = dr.GetString(3).Trim();
                        string last1 = dr.GetString(4).Trim();

                        string term1 = dr.GetString(5).Trim();
                        string proy1 = dr.GetString(6).Trim();
                        string Loc1 = dr.GetString(7).Trim();
                        string LOB = dr.GetString(8).Trim();
                        string reason1 = dr.GetString(9).Trim();

                        bool ss = LoadIntoSharePoint(ATTUID,
                            EmplID, name1, last1, term1, proy1,
                            Loc1, LOB, reason1);

                        lista = lista + EmplID + " " + name1 + " " + last1;
                        if (!ss) return;

                        lista = lista + "loaded ";
                        string sql = "update RPA_ATT_NotLoaded set loaded = 1 where EmpID ='" + EmplID + "' ";

                        log.Info(" ");
                        log.Info(sql);
                        UpdateDB(sql);
                        lista = lista + "</br>";
                    }

                    if (lista != "")
                    {
                        SendEmails.SendEmail("updated", lista, "tomas.dale@concentrix.com");
                    }
                }
                catch (Exception )
                {

                }
                conn.Close();
            }
        }

        static int UpdateDB(string sql)
        {
            try
            {
                string SqlConn = ConfigurationManager.ConnectionStrings["Sql28"].ConnectionString;
                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                SqlCommand comm = new SqlCommand(sql, sqlcon);
                comm.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception ws)
            {
                log.Info("Error " + sql + "  " + ws.ToString());
                return 0;
            }
            return 1;
        }

        public static bool LoadIntoDataBase(string ATTUID, string EmplID, string name1, string lastname1, string termina, string sended, string project, string location, string LOB, string Reason, string SENDER)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            SENDER = SENDER.Replace("@", ".");

            if (ATTUID == null) ATTUID = "";
            if (location == null) location = "";
            if (project == null) project = "";

            try
            {
                string TERM = "Y";
                if (Reason == "Transfer") TERM = "N";

                connection = new SqlConnection(SqlConn);
                connection.Open();

                command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "ATT_Insert_tblDailyReport2";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ATTUID", ATTUID);
                command.Parameters.AddWithValue("@EMPLID", EmplID);
                command.Parameters.AddWithValue("@NAME1", name1);
                command.Parameters.AddWithValue("@LAST1", lastname1);
                command.Parameters.AddWithValue("@DT1", termina);
                command.Parameters.AddWithValue("@DT2", sended);

                command.Parameters.AddWithValue("@project", project);
                command.Parameters.AddWithValue("@location", location);
                command.Parameters.AddWithValue("@LOB", LOB);
                command.Parameters.AddWithValue("@TERM", TERM);
                command.Parameters.AddWithValue("@SENDER", SENDER);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception es)
            {
                string msgError = es.ToString() + "  david.adkins or prashant.mediratta </br>";
                msgError = msgError + "DataAccess.LoadIntoDataBase When Inserting into Database </br> ";

                string TERM2 = "Y";
                if (Reason == "Transfer") TERM2 = "N";

                msgError = msgError + "select * from tblDailyReport2 where EmplID = '" + EmplID + "'  </br></br> ";
                msgError = msgError + "select * from tblLogs         where EmplID = '" + EmplID + "'  </br></br> ";
                msgError = msgError + "exec ATT_Insert_tblDailyReport2 '" + EmplID + "', '" + name1 + "', '" + lastname1 + "', '" + termina + "', '" + sended + "', '" + location + "',  '" + LOB + "',  '" + TERM2 + "', '" + SENDER + "' </BR></BR>";
                msgError = msgError + SqlConn + " " + es.ToString();

                log.Error(msgError);

                SendEmails.SendEmail("Error Notification Robot, DataAccess, LoadIntoDataBase ", msgError, "");
                System.Threading.Thread.Sleep(5 * 1000 * 60);
            }
            finally
            {
                if (command != null)
                    command.Dispose();

                if (connection != null)
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                    connection.Dispose();
                }
            }
            return true;
        }

        public static void NotLoaded(string ATTUID, string EmpID, string name1, string lastname1, string termina, string project, string location, string LOB, string reason)
        {
            project = project.Replace("'", "''");
            location = location.Replace("'", "''");
            LOB = LOB.Replace("'", "''");
            reason = reason.Replace("'", "''");
            lastname1 = lastname1.Replace("'", "''");
            name1 = name1.Replace("'", "''");
            if (ATTUID == null) ATTUID = "";

            string sql = "insert into RPA_ATT_NotLoaded ";
            sql = sql + " ( ATTUID, EmpID, name1, lastname1, termina, project, location, LOB, Reason) ";
            sql = sql + " values (";
            sql = sql + "'" + ATTUID + "',";
            sql = sql + "'" + EmpID + "',";
            sql = sql + "'" + name1 + "',";
            sql = sql + "'" + lastname1 + "',";
            sql = sql + "'" + termina + "',";
            sql = sql + "'" + project + "',";

            sql = sql + "'" + location + "',";
            sql = sql + "'" + LOB + "',";
            sql = sql + "'" + reason + "')";

            try
            {
                string SqlConn = ConfigurationManager.ConnectionStrings["Sql28"].ConnectionString;
                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlcon;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                sqlCommand.ExecuteNonQuery();

                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception es)
            {
                string ss = es.ToString();
                string msgErr = "Error when INSERTING " + sql;
                SendEmails.SendEmail("Error updating tbLogs from NotificationRobot ", msgErr, "");
            }

            //Status.LastTime = DateTime.Now;
        }

        public static bool LoadIntoSharePoint(string ATTUID, string EmplID, string name1, string lastname1, string termina, string project, string location, string LOB, string Reason)
        {
            ClientContext context = null;
            try
            {
                System.Net.ServicePointManager.Expect100Continue = true;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                context = new ClientContext(ConfigurationManager.AppSettings["SiteUrl"]);
                List list = context.Web.Lists.GetByTitle(ConfigurationManager.AppSettings["ListTitle"]);

                System.Net.ServicePointManager.Expect100Continue = true;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                ListItemCreationInformation itemCreationInformation;
                itemCreationInformation = new ListItemCreationInformation();
                ListItem item;

                FieldCollection fields;
                List<Field> fieldList;
                Field field;

                fields = list.Fields;
                context.Load(fields, collection => collection.Include(f => f.Title, f => f.InternalName));
                context.ExecuteQuery();

                fieldList = fields.ToList();

                //System.Security.Principal.WindowsIdentity.GetCurrent().Nam
                var requestor = context.Web.EnsureUser("NA\\attuid");

                itemCreationInformation = new ListItemCreationInformation();
                item = list.AddItem(itemCreationInformation);

                field = fieldList.Where(f => f.Title == "Employee ID").FirstOrDefault();
                item[field.InternalName] = EmplID;
                //item["Title"] = row["EMPLID"];

                field = fieldList.Where(f => f.Title == "Emp First Name").FirstOrDefault();
                item[field.InternalName] = name1;
                //item["Emp_x0020_First_x0020_Name"] = name1;

                field = fieldList.Where(f => f.Title == "Emp Last Name").FirstOrDefault();
                item[field.InternalName] = lastname1;
                //item["Emp_x0020_Last_x0020_Name"] = lastname1;

                field = fieldList.Where(f => f.Title == "Attrition Reason").FirstOrDefault();
                item[field.InternalName] = Reason;

                field = fieldList.Where(f => f.Title == "Automation Requestor").FirstOrDefault();
                item[field.InternalName] = requestor;

                field = fieldList.Where(f => f.Title == "Effective Date of Attrition").FirstOrDefault();
                DateTime dt1 = Convert.ToDateTime(termina);
                item[field.InternalName] = dt1;

                //===========

                item["ATTUID"] = ATTUID;
                item["Project"] = project;
                item["Location"] = location;
                item["LOB"] = LOB;

                if (ATTUID == "")
                {
                    item["Status"] = "Error";
                    item["Comments"] = "Missing ATTUID";
                }

                item.Update();
                context.ExecuteQuery();
                string msg1 = ATTUID + " " + EmplID + " " + name1 + " " + lastname1;
                log.Info("Success to Sharepoint " + msg1);

                return true;
            }
            catch (Exception ex)
            {
                // LOAD WHEN NOT ABLE TO INSERT
                NotLoaded(ATTUID, EmplID, name1, lastname1, termina,
                    project, location, LOB, Reason);

                SendEmails.SendEmail("Sharepoint Failed TASK,</br> loading in NotLoaded</br>" + EmplID + "</br>Check SSL - Protocol", ex.ToString(), "");
                log.Error(ex.ToString());
            }
            finally
            {
                if (context != null)
                    context.Dispose();
            }

            return false;
        }
    }
}
