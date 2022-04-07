using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using log4net;

namespace UpdateDatabase
{
    class Functions
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public string GetLista(DataTable dt, int Pos_EmpID)
        {
            List<string> ListID = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string EmpID = dr[Pos_EmpID].ToString();
                if (EmpID == "") continue;
                if (EmpID == "EID") continue;
                if (EmpID == "0") continue;
                ListID.Add(EmpID);
            }
            string listID2 = string.Join(",", ListID);
            return listID2;
        }


        public string GetListaCT(DataTable dt, int pos1)
        {
            List<string> ListID = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string EmpID = dr[pos1].ToString();
                if (EmpID == "") continue;
                if (EmpID == "EID") continue;
                if (EmpID == "Employee ID") continue;
                if (EmpID == "0") continue;
                ListID.Add(EmpID);
            }
            string listID2 = string.Join(",", ListID);
            if (listID2 == "") return null;

            listID2 = listID2.Replace("Employee ID,", "");

            string sqlExp = "select ID, Empid, Status FROM DeclarationForms ";
            sqlExp = sqlExp + " WHERE EmpID in (" + listID2 + ")";
            sqlExp = sqlExp + " AND Status != 'Notified FTW'";

            DataTable dtExp = GetTable(sqlExp);
            string listado = "";
            foreach (DataRow dr in dtExp.Rows)
            {
                listado = listado + "," + dr[1].ToString();
            }
            return listado;
        }
          
      

        public string[] GetManagers(string EmpID)
        {
            string SqlEmpl = ConfigurationManager.ConnectionStrings["SqlEmpl"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(SqlEmpl);
            sqlcon.Open();

            string sql = " select top 1 ";
            sql = sql + " t1.supervisor_name, t1.supervisor_email, ";
            sql = sql + " t2.supervisor_name, t2.supervisor_email, ";
            sql = sql + " t3.supervisor_name, t3.supervisor_email, ";
            sql = sql + " t1.email ";

            sql = sql + " FROM Employee t1(nolock), employee t2(nolock), employee t3(nolock) ";

            sql = sql + " WHERE t1.HR_ID = '" + EmpID + "' ";
            sql = sql + " AND   t1.supervisor = t2.HR_ID";
            sql = sql + " AND   t2.supervisor = t3.HR_ID";
            sql = sql + " AND   UPPER(t1.Country) = 'PHILIPPINES' ";
            //sql = sql + " AND   UPPER(t2.Country) = 'PHILIPPINES' ";

            // IMPORTANT
            //sql = sql + " ORDER  BY t1.last_cvg_logon_date DESC,  ";
            //sql = sql + "           t2.last_cvg_logon_date DESC ";

            sql = sql + " ORDER  BY t1.HR_Effective_Date DESC,  ";
            sql = sql + "           t2.HR_Effective_Date DESC, ";
            sql = sql + "           t3.HR_Effective_Date DESC ";
            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            string[] emails = new string[6];
            for (int i = 0; i < 6; i++) emails[i] = "";

            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < 6; j++)
                {
                    string each = dt.Rows[0][j].ToString();
                    each = each.Replace("'", "");
                    emails[j] = each;
                }
            }

            sqlcon.Close();
            sqlcon.Dispose();
            log.Info("managers " + string.Join(",", emails));
            return emails;
        }


        public int UpdateDB(string sql)
        {
            log.Info("UpdateDB");
            string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            log.Info(SqlConn);
            SqlConnection sqlcon = new SqlConnection(SqlConn);
            sqlcon.Open();

            try
            {
                SqlCommand comm = new SqlCommand(sql, sqlcon);
                log.Info(sql);
                comm.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception es)
            {
                //SendEmails sent = new SendEmails();
                //sent.SendEmail("EXPOSURE ERROR Common.DataAccess.UpdateDB ", sql + "</br>,/br>" + ws.ToString(), "", "");
                log.Info("Error " + sql + "  " + es.ToString());
                sqlcon.Close();
                sqlcon.Dispose();

                return 0;
            }

            return 1;
        }

        public void InsertIntoLog(string logInfo, string emplID, string names, string sender)
        {
            logInfo = logInfo.Replace("'", "''");
            logInfo = logInfo.Replace("<b>", "");
            logInfo = logInfo.Replace("</b>", "");
            logInfo = logInfo.Replace("</ b >", "");

            // NEW1
            //if (emplID.Length > 15) emplID = emplID.Substring(0, 15);

            //DataAccess da = new DataAccess();
            if (emplID == "") emplID = " ";
            if (names == "") names = " ";
            if (sender == "") sender = " ";

            logInfo = logInfo.Replace("</br>", "");
            string logInfo2 = "";
            if (logInfo.Length > 500)
            {
                int pos1 = logInfo.IndexOf(",", 490);
                if (pos1 > 500) pos1 = logInfo.IndexOf(" ", 480);
                if (pos1 > 500) pos1 = 490;
                logInfo2 = logInfo.Substring(pos1);
                logInfo = logInfo.Substring(0, pos1);
            }

            logInfo = logInfo.Replace("'", "''");
            string LogInfo = "('" + emplID + "','" + names + "','" + sender + "','" + logInfo + "') ";
            string sql = "INSERT into DeclarationFormsLogs (EmpID, EmpName, Sender, LogInfo) values " + LogInfo;
            UpdateDB(sql);

            if (logInfo2 != "")
            {
                logInfo2 = logInfo2.Replace("'", "''");
                LogInfo = "('" + emplID + "','" + names + "','" + sender + "','" + logInfo2 + "') ";
                sql = "INSERT into DeclarationFormsLogs (EmpID, EmpName, Sender, LogInfo) values " + LogInfo;
                UpdateDB(sql);
            }
        }


        public DataTable getWK()
        {
                string sql = "SELECT EmpID, FirstName, MiddleName, LastName, Gender, Location, DOB, LOCATIONADDRESS1, ";
                sql = sql + "LOCATIONADDRESS2, PRIMARYADDRESS1, PRIMARYADDRESS2, MSA from declarationFormsWorkday ";
                return GetTable(sql);
       }

        public ContactDate GetDataInfo(DataTable dtExposure,
      string EmpID, bool CheckEmployee, bool LoadWKData, DataTable dtWK)
        {
            
            ContactDate ct = new ContactDate();
            ct.EmpID = EmpID;

            string where1 = "EmpID = '" + ct.EmpID + "' AND STATUS <>'Notified FTW' ";

            //sql = "SELECT EmpID, EmpName, EmpLocation, ";
            //sql = sql + " QuarantineStartDate, QuarantineEndDate,";
            //sql = sql + " dtExtEnd, SQAdditionalDays, DateCreated,   ";
            //sql = sql + " EmpMobile, Landline,   ";
            //sql = sql + " POC, RepType, ForCTracing, Severity, BHERT, dtFTW, ";
            //sql = sql + " CurrFaculty,moredetails,ICU, dtConfinement, FirstName, MSA, LastName, ID";

            // IN EXPOSURE
           
                DataRow[] result = dtExposure.Select(where1);
                if (result.Length > 0)
                {
                    ct.Status = "Update";
                    //ct.EmpName = result[0]["EmpName"].ToString();
                    //ct.Location = result[0]["EmpLocation"].ToString();
                    //ct.DateCreated = result[0]["DateCreated"].ToString();
                    //ct.RepType = result[0]["RepType"].ToString();
                    //ct.FirstName = result[0]["FirstName"].ToString();
                    //ct.LastName = result[0]["LastName"].ToString();
                    //ct.ID = result[0]["ID"].ToString();
                    //ct.MiddleName = result[0]["MiddleName"].ToString();
                    //try
                    //{
                    //    ct.MSA = result[0]["MSA"].ToString();
                    //}
                    //catch (Exception) { }

                    if (!LoadWKData) return ct;
                }
           
            // NEW
            //=====
            //if (ct.Status != "Update")
            //{
                //ct.Status = "New";
                //ct.DateCreated = Today();
            //}

            // DeclarationFormsWorkDay
            // empid, first, middle, last, gender, location, dob, loc1, loc2, add1, add2, msa
            where1 = "EmpID ='"+ ct.EmpID+"'";
            //return null;

            result = dtWK.Select(where1);
            if (result.Length > 0)
            {
                ct.FirstName = result[0][1].ToString();
                ct.MiddleName = result[0][2].ToString();
                ct.LastName = result[0][3].ToString();
                ct.EmpName = ct.FirstName + " " + ct.MiddleName + " " + ct.LastName;

                ct.Gender = result[0][4].ToString();
                ct.Location = result[0][5].ToString();
                ct.Age = result[0][6].ToString();

                ct.SiteCity = result[0][8].ToString();

                ct.WK_Address1 = result[0][9].ToString();
                ct.WK_Address2 = result[0][10].ToString();

                ct.MSA = result[0][11].ToString();
                return ct;
            }

            if (!CheckEmployee) return null;

            // IN EMPLOYEE
            //string sql = "SELECT HR_ID, First_Name, Middle_Name, Last_Name, ";
            //sql = sql + " Location, Program_Name FROM EMPLOYEE(nolock) ";
            //sql = sql + " WHERE HR_ID = '" + EmpID + "'";
                        
            DataTable dtEMPLOYEE = GetInfoEmployee(ct.EmpID);
            if (dtEMPLOYEE == null) return null;

            ct.FirstName = dtEMPLOYEE.Rows[0][1].ToString();
            ct.MiddleName = dtEMPLOYEE.Rows[0][2].ToString();
            ct.LastName = dtEMPLOYEE.Rows[0][3].ToString();
            ct.EmpName = ct.FirstName + " " + ct.MiddleName + " " + ct.LastName;

            ct.Location = dtEMPLOYEE.Rows[0][4].ToString();
            ct.EmpProgram = dtEMPLOYEE.Rows[0][5].ToString();

            return ct;
        }

        public DataTable GetInfoEmployee(string EmpID)
        {
            if (EmpID == null) return null;
            string conn = ConfigurationManager.ConnectionStrings["SqlEmpl"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(conn);
            sqlcon.Open();

            string sql = "SELECT HR_ID, First_Name, Middle_Name, Last_Name, ";
            sql = sql + " Location, Program_Name FROM EMPLOYEE(nolock) ";
            sql = sql + " WHERE HR_ID = '" + EmpID + "'";
            sql = sql + " AND UPPER(Country) = 'PHILIPPINES' ";
            sql = sql + " ORDER BY HR_Effective_Date DESC";

            log.Info("SEARCH ON EMPLOYEE TABLE");

            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            sqlcon.Close();
            sqlcon.Dispose();

            if (dt.Rows.Count == 0) return null;
            //log.Info("found in employee table");
            return dt;
        }

        public string TodayDate()
        {
            string dd = Today();
            int pos1 = dd.IndexOf(" ");
            dd = dd.Substring(0, pos1);
            return dd.Trim();
        }

        public string Today()
        {
            var timeToConvert = DateTime.Now;  //whereever you're getting the time from
            var est = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            return TimeZoneInfo.ConvertTime(timeToConvert, est).ToString();
        }

        public string TodayEmail()
        {
            var timeToConvert = DateTime.Now;  //whereever you're getting the time from
            var est = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime fec = TimeZoneInfo.ConvertTime(timeToConvert, est);
            string hora = fec.ToString();
            int pos1 = hora.IndexOf(" ");
            hora = hora.Substring(pos1);
            return fec.Month.ToString() + fec.Day.ToString() + fec.Year.ToString() + hora;
        }


        public string RemoveGarbage(string field)
        {
            field = field.Replace("Required field", "");
            field = field.Replace("Workday ID", "");
            field = field.Replace(":", "");
            field = field.Replace("Employee Name", "");
            field = field.Replace("(", "");
            field = field.Replace(")", "");
            field = field.Replace("Ex.", "");
            field = field.Trim();
            return field;
        }

        public string MissingInfo(string variable1, string feedback, string missing)
        {
            if (variable1.Trim() == "")
            {
                if (missing == "")
                    missing = feedback;
                else
                    missing = missing + ", " + feedback;
            }
            return missing;
        }

        public DataTable GetTable(string sql)
        {
            string err = "";
            string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            DataTable table = new DataTable();
            SqlConnection sqlcon = new SqlConnection(SqlConn);

            int i = 0;
            while (i < 3)
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(sql, sqlcon);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);
                    cmd.Dispose();
                    sqlcon.Dispose();
                    sqlcon.Close();
                    int conteo = table.Rows.Count;
                    return table;
                }
                catch (Exception es2)
                {
                    err = es2.ToString();
                    if (err.Contains("but then an error occurred during the login process"))
                    {
                        err = "A connection was established with the server, but an error occurred during the login process. (provider: SSL Provider, error: 0 - An existing connection was forcibly closed by the remote host.) ---> System.ComponentModel.Win32Exception (0x80004005): An existing connection was forcibly closed by the remote host";
                        System.Threading.Thread.Sleep(15 * 1000);
                        i++;
                    }
                    else
                        i = 5;
                }
            }

            try
            {
                sqlcon.Dispose();
            }
            catch (Exception) { }

            //SendEmails sent = new SendEmails();
            //sent.SendEmail("Error Common.DataAccess.GetTable", sql + " " + err.ToString(), "", "");
            //log.Info(sql + " " + err.ToString());
            return null;
        }

    }
}
