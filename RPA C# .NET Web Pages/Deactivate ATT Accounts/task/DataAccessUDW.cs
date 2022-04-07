using log4net;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Reflection;


namespace Task
{
    class DataAccessUDW
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private static string QueryAll2()
        {
            string query = " SELECT T1.LOGID AS VerizonEmpID, T2.LOGID AS STARRID,  T2.SRC_PERSON_ID ";

            query = query + " FROM  UDW.DIM_XREFPERSON T1 INNER JOIN UDW.DIM_XREFPERSON T2 ";
            query = query + " ON T2.SRC_PERSON_ID = T1.SRC_PERSON_ID ";

            query = query + " WHERE T1.JOB_SYSTEM_ID = 2358 "; // 25 2358
            query = query + " AND T2.JOB_SYSTEM_ID = 32407987 ";

            query = query + " AND ( T1.DW_END_DT LIKE '31-DEC-99' OR T1.DW_END_DT IS NULL) ";
            return query;
        }


        public static DataTable GetStarrIDs2()
        {
            DataTable dt = CreateDT();

            string OracleConn = ConfigurationManager.ConnectionStrings["ATTUID"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(OracleConn))
            {
                try
                {
                    conn.Open();
                    string query = " SELECT T1.LOGID AS VerizonEmpID, T2.LOGID AS STARRID,  T2.SRC_PERSON_ID ";

                    query = query + " FROM  UDW.DIM_XREFPERSON T1 INNER JOIN UDW.DIM_XREFPERSON T2 ";
                    query = query + " ON T2.SRC_PERSON_ID = T1.SRC_PERSON_ID ";

                   
                    //query = query + " WHERE T1.JOB_SYSTEM_ID = 2358 ";
                    //query = query + " AND T2.JOB_SYSTEM_ID = 32407987 ";
                    query = query + " WHERE T1.JOB_SYSTEM_ID in (SELECT JOB_SYSTEM_ID FROM udw.DIM_JOBSYSTEM where JOB_SYSTEM_NAME = 'GAXEID') ";
                    query = query + " AND T2.JOB_SYSTEM_ID = 32407987 ";

                    query = query + " AND ( T1.DW_END_DT LIKE '31-DEC-99' OR T1.DW_END_DT IS NULL) ";

                    //    query = "select business_unit, site_dt from UDW.PERSON_CURR  ";
                    query = "SELECT JOB_SYSTEM_ID FROM udw.DIM_JOBSYSTEM where JOB_SYSTEM_NAME = 'GAXEID'";


                    OracleCommand cmd = new OracleCommand(query, conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    //for (int i = 0; i < dr.FieldCount; i++)
                    //{
                    //    string sss = dr.GetName(i);
                    //}


                    while (dr.Read())
                    {
                        string f1 = dr.GetString(0).Trim();
                        string f2 = dr.GetString(1).Trim();
                        string f3 = dr.GetString(2).Trim();

                        dt.Rows.Add("", "", f1, "", f3, f2);
                    }
                }
                catch (Exception es)
                {
                    conn.Close();
                    string msgError = "FROM VERIZONVALIDATION, it is not possible to retrieve data<br><br>GetAllStarrIDs, Contact david.adkins@concentrix.com;rajeswari.napa@concentrix.com; >;prashant.mediratta@convergys.com ";
                    msgError = msgError + "<br><br>Retrieving All StarrIDs from Oracle<br><br>" + es.ToString();

                    log.Error(msgError);

                    SendEmails.SendEmail("Error Notification Robot, Oracle connection string error", msgError,"");
                    //System.Threading.Thread.Sleep(5 * 1000 * 60);
                    System.Environment.Exit(0);
                    return null;
                }

                conn.Close();
            }
            int ss = dt.Rows.Count;
            return dt;
        }
        public static DataTable GetManagerInfomation()
        {
            DataTable dt = DataAccess.CreateDT();

            string OracleConn = ConfigurationManager.ConnectionStrings["ATTUID"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(OracleConn))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT T1.FIRST_NAME , T1.LAST_NAME, T1.EMAIL_ADDR AS EMAIL, ";
                    query = query + " T2.SRC_PERSON_ID AS EMPLID, T2.FIRST_NAME , T2.LAST_NAME , T3.LOGID ";

                    query = query + " FROM UDW.PERSON_CURR T1,  UDW.PERSON_CURR T2,  UDW.DIM_XREFPERSON T3 ";
                    query = query + " WHERE T1.SRC_PERSON_ID = T2.SUPERVISOR_ID ";
                    query = query + " AND T2.AGENT_IND ='Y' ";
                    query = query + " AND T2.SRC_PERSON_ID = T3.SRC_PERSON_ID ";
                    query = query + " AND T3.JOB_SYSTEM_ID = 32407987 ";
                    query = query + " AND (SYSDATE < T3.DW_END_DT  OR T3.DW_END_DT is null) ";

                    //query = query + " AND T3.LOGID ='5514PH'";
                    //query = query + " AND T2.SRC_PERSON_ID='101377085' ";

                    OracleCommand cmd = new OracleCommand(query, conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    int i = 0;

                    while (dr.Read())
                    {
                        try
                        {
                            i++;
                            string f1 = dr.GetString(0).Trim() + " " + dr.GetString(1).Trim(); // MANAGER
                            string f2 = "";
                            try
                            {
                                f2 = dr.GetString(2).Trim(); // EMAIL
                            }
                            catch (Exception) { }

                            string f3 = dr.GetString(3).Trim(); // EMPLOYEE ID
                            string f4 = dr.GetString(4).Trim() + " " + dr.GetString(5).Trim();  // EMP NAME
                            string f5 = dr.GetString(6).Trim();

                            dt.Rows.Add(f1, f2, f3, f4, "", f5);
                        }
                        catch (Exception) { }
                    }
                }
                catch (Exception es)
                {
                    string msgError = "GetAllStarrIDs, Contact david.adkins@concentrix.com;rajeswari.napa@concentrix.com; > or Prashant Mediratta <prashant.mediratta@convergys.com> ";
                    msgError = msgError + "\\n\\nRetrieving All StarrIDs from Oracle\\n\\n" + es.ToString();

                    log.Error(msgError);

                    SendEmails.SendEmail("Error Notification Robot, Oracle connection string error", msgError,"");

                    conn.Close();
                    System.Environment.Exit(0);
                    return null;
                }

                conn.Close();
            }
            int ss = dt.Rows.Count;
            return dt;
        }
        public static DataTable GetInactiveStarrIDs()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("EndDate", typeof(string));
            dt.Columns.Add("StarrID", typeof(string));

            string OracleConn = ConfigurationManager.ConnectionStrings["ATTUID"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(OracleConn))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MAX(DW_END_DT), LOGID ";

                    query = query + " FROM UDW.DIM_XREFPERSON ";
                    query = query + " WHERE JOB_SYSTEM_ID = '32407987' ";
                    query = query + " and  SYSDATE > DW_END_DT ";
                    query = query + " GROUP BY  LOGID ";

                    OracleCommand cmd = new OracleCommand(query, conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (dr.Read())
                    {
                        string f1 = dr.GetDateTime(0).ToString();
                        string f2 = dr.GetString(1).Trim();
                        dt.Rows.Add(f1, f2);
                    }
                }
                catch (Exception es)
                {
                    string msgError = "GetAllStarrIDs, Contact david.adkins@concentrix.com;rajeswari.napa@concentrix.com; > or Prashant Mediratta <prashant.mediratta@convergys.com> ";
                    msgError = msgError + "\\n\\nRetrieving All StarrIDs from Oracle\\n\\n" + es.ToString();

                    log.Error(msgError);

                    SendEmails.SendEmail("Error Notification Robot, Oracle connection string error", msgError,"");

                    conn.Close();
#if !DEBUG
                    System.Environment.Exit(0);
                    System.Threading.Thread.Sleep(5 * 1000 * 60);
#endif
                    return null;
                }

                conn.Close();
            }
            int ss = dt.Rows.Count;
            return dt;
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

        public static DataTable GetStarrIDs()
        {
            DataTable dt = CreateDT();

            string OracleConn = ConfigurationManager.ConnectionStrings["ATTUID"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(OracleConn))
            {
                try
                {
                    conn.Open();
                    ORACLE_SQL q = new ORACLE_SQL();
                    string query = q.Verizon();

                    OracleCommand cmd = new OracleCommand(query, conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string sss = dr.GetName(i);
                    }


                    while (dr.Read())
                    {
                        string f1 = dr.GetString(0).Trim();
                        string f2 = dr.GetString(1).Trim();
                        string f3 = dr.GetString(2).Trim();

                        dt.Rows.Add("", "", f1, "", f3, f2);
                    }
                }
                catch (Exception es)
                {
                    string msgError = "FROM VERIZONVALIDATION, it is not possible to retrieve data<br><br>GetAllStarrIDs, Contact david.adkins@concentrix.com;rajeswari.napa@concentrix.com; >;prashant.mediratta@convergys.com ";
                    msgError = msgError + "<br><br>Retrieving All StarrIDs from Oracle<br><br>" + es.ToString();

                    log.Error(msgError);

                    SendEmails.SendEmail("Error Notification Robot, Oracle connection string error", msgError,"");

                    conn.Close();
                    System.Environment.Exit(0);
                    return null;
                }

                conn.Close();
            }
            int ss = dt.Rows.Count;
            return dt;
        }

        //===================
        //  ATT DEACTIVATION
        //===================

        public static DataTable GetAllATTUIDs()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ATTUID", typeof(string));
            dt.Columns.Add("EMPLID", typeof(string));

            string OracleConn = ConfigurationManager.ConnectionStrings["ATTUID"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(OracleConn))
            {
                try
                {
                    conn.Open();
                    ORACLE_SQL SQL = new ORACLE_SQL();

                    OracleCommand cmd = new OracleCommand(SQL.ATTUID_SQL(""), conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        string ATTUID = dr.GetString(0).Trim();
                        if (ATTUID == "-") ATTUID = "";

                        string EMPLID = dr.GetString(1).Trim();
                        if (ATTUID != "") dt.Rows.Add(ATTUID, EMPLID);
                    }
                }
                catch (Exception es)
                {
                    string msgError = "Procedure GetAllATTUIDs, Contact david.adkins@concentrix.com;rajeswari.napa@concentrix.com; > or Prashant Mediratta <prashant.mediratta@convergys.com> ";
                    msgError = msgError + "<br><br>Retrieving All ATTUID's from Oracle<br><br>" + es.ToString();

                    log.Error(msgError);

                    SendEmails.SendEmail("Error Task, Oracle connection string error", msgError,"");

                    conn.Close();
                    System.Environment.Exit(0);
                    return null;
                }

                conn.Close();
            }

            log.Info("Conteo ORALE UTTIILD " + dt.Rows.Count);
            return dt;
        }

        public static string GetATTUID(string EmplID)
        {
            string result = "-";

            OracleConnection connection = null;
            OracleCommand command = null;

            try
            {
                connection = new OracleConnection(ConfigurationManager.ConnectionStrings["ATTUID"].ConnectionString);
                command = new OracleCommand();

                command.Connection = connection;

                ORACLE_SQL q = new ORACLE_SQL();
                command.CommandText = q.ATTUID_SQL(EmplID);
                command.CommandType = CommandType.Text;

                connection.Open();
                var attuid = command.ExecuteScalar();
                result = "";

                if (attuid != null) result = attuid.ToString();
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
    }
}
