using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
namespace Exposure
{
    public class DataBase
    {

        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DateTime GetDate(string fec)
        {
            fec = fec.Replace("/1/", "/01/");
            fec = fec.Replace("/2/", "/02/");
            fec = fec.Replace("/3/", "/03/");
            fec = fec.Replace("/4/", "/04/");
            fec = fec.Replace("/5/", "/05/");
            fec = fec.Replace("/6/", "/06/");
            fec = fec.Replace("/7/", "/07/");
            fec = fec.Replace("/8/", "/08/");

            fec = fec.Replace("/9/", "/09/");


            fec = fec.Trim();
            if (fec.Length < 7) return Convert.ToDateTime("01/01/1900");
            if (fec.Trim() == "" || fec.IndexOf("Quaran") > 0 || fec == "1/0/1900") return DateTime.Now.AddDays(100);

            int pos1 = fec.IndexOf("/");
            if (pos1 == 1) fec = "0" + fec;

            pos1 = fec.IndexOf("/", 3);
            if (pos1 < 1) return Convert.ToDateTime("01/01/1900");

            if (pos1 == 4)
                fec = fec.Substring(0, 3) + "0" + fec.Substring(3);

            DateTime dt = DateTime.Now;
            fec = fec.Replace("-", "/");
            try
            {
                dt = DateTime.ParseExact(fec, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                int dias = (dt - DateTime.Now).Days;
                string ss = dt.ToString(); ;
                if (dias > 40)
                    dt = DateTime.ParseExact(fec, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                try
                {
                    dt = DateTime.ParseExact(fec, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                }
                catch (Exception)
                {
                    try
                    {
                        dt = DateTime.ParseExact(fec, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            dt = DateTime.ParseExact(fec, "m/dd/yyyy", CultureInfo.InvariantCulture);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                dt = DateTime.ParseExact(fec, "mm/d/yyyy", CultureInfo.InvariantCulture);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    dt = DateTime.ParseExact(fec, "m/d/yyyy", CultureInfo.InvariantCulture);
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        dt = DateTime.Now.AddDays(60);
                                    }
                                    catch (Exception)
                                    {
                                        return Convert.ToDateTime("01/01/1900");
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return dt;
        }

        public DateTime GetDateMMddYYYY(string fec)
        {
            fec = fec.Replace("/1/", "/01/");
            fec = fec.Replace("/2/", "/02/");
            fec = fec.Replace("/3/", "/03/");
            fec = fec.Replace("/4/", "/04/");
            fec = fec.Replace("/5/", "/05/");
            fec = fec.Replace("/6/", "/06/");
            fec = fec.Replace("/7/", "/07/");
            fec = fec.Replace("/8/", "/08/");

            fec = fec.Replace("/9/", "/09/");


            fec = fec.Trim();
            if (fec.Length < 7) return Convert.ToDateTime("01/01/1900");
            if (fec.Trim() == "" || fec.IndexOf("Quaran") > 0 || fec == "1/0/1900") return DateTime.Now.AddDays(100);

            int pos1 = fec.IndexOf("/");
            if (pos1 == 1) fec = "0" + fec;

            pos1 = fec.IndexOf("/", 3);
            if (pos1 < 1) return Convert.ToDateTime("01/01/1900");

            if (pos1 == 4)
                fec = fec.Substring(0, 3) + "0" + fec.Substring(3);

            DateTime dt = DateTime.Now;
            fec = fec.Replace("-", "/");
            try
            {
                dt = DateTime.ParseExact(fec, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                    try
                    {
                        dt = DateTime.ParseExact(fec, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            dt = DateTime.ParseExact(fec, "m/dd/yyyy", CultureInfo.InvariantCulture);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                dt = DateTime.ParseExact(fec, "mm/d/yyyy", CultureInfo.InvariantCulture);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    dt = DateTime.ParseExact(fec, "m/d/yyyy", CultureInfo.InvariantCulture);
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        dt = DateTime.Now.AddDays(60);
                                    }
                                    catch (Exception)
                                    {
                                        return Convert.ToDateTime("01/01/1900");
                                    }

                                }
                            }
                        }
                    }
               
            }
            return dt;
        }
        public DataTable dtEmployee()
        {
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Created", typeof(string));
            dt2.Columns.Add("Employee ID", typeof(string));
            dt2.Columns.Add("Employee Name", typeof(string));
            dt2.Columns.Add("StartDate", typeof(string));
            dt2.Columns.Add("EndDate", typeof(string));
            dt2.Columns.Add("Site", typeof(string));
            dt2.Columns.Add("Status", typeof(string));

            return dt2;
        }
        public DataTable dtTASK()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("DATE REPORTED", typeof(string));
            dt.Columns.Add("POC", typeof(string));
            dt.Columns.Add("SOURCE", typeof(string));
            dt.Columns.Add("REPORT TYPE", typeof(string));
            dt.Columns.Add("FOR CONTACT TRACING", typeof(string));

            dt.Columns.Add("CLOSE CONTACT", typeof(string));
            dt.Columns.Add("TYPE OF TEST", typeof(string));
            dt.Columns.Add("PCR", typeof(string));
            dt.Columns.Add("EMPLOYEE ID", typeof(string));
            dt.Columns.Add("FIRST NAME", typeof(string));

            dt.Columns.Add("MIDDLE NAME", typeof(string));
            dt.Columns.Add("LAST NAME", typeof(string));
            dt.Columns.Add("MSA", typeof(string));
            dt.Columns.Add("PILLAR", typeof(string));
            dt.Columns.Add("SITE", typeof(string));

            dt.Columns.Add("RESIDENCE", typeof(string));
            dt.Columns.Add("MOBILE", typeof(string));
            dt.Columns.Add("LANDLINE", typeof(string));
            dt.Columns.Add("BEST TIME TO CALL", typeof(string));
            dt.Columns.Add("LAST SEEN", typeof(string));

            dt.Columns.Add("TYPE TRANSPORT", typeof(string));
            dt.Columns.Add("PCR FROM START OF QUARANTINE", typeof(string));
            dt.Columns.Add("DATE TESTING", typeof(string));
            dt.Columns.Add("RELEASE DATE", typeof(string));
            dt.Columns.Add("FACULTY", typeof(string));

            dt.Columns.Add("CONTACT TRACING", typeof(string));
            dt.Columns.Add("DATE OF CONTACT", typeof(string));
            dt.Columns.Add("WD ID", typeof(string));
            dt.Columns.Add("NAME CNX", typeof(string));
            dt.Columns.Add("LATEST WHEREABOUT", typeof(string));

            dt.Columns.Add("CURRENT FACULTY", typeof(string));
            dt.Columns.Add("DATE CONFINEMENT", typeof(string));
            dt.Columns.Add("IN ICU", typeof(string));
            dt.Columns.Add("FIRST SYMPTOM", typeof(string));
            dt.Columns.Add("SYMPTOM", typeof(string));

            dt.Columns.Add("CATEGORY", typeof(string));
            dt.Columns.Add("DOH", typeof(string));
            dt.Columns.Add("SEVERITY", typeof(string));
            dt.Columns.Add("BHERT", typeof(string));
            dt.Columns.Add("RECOMMENDATION", typeof(string));

            dt.Columns.Add("QUARANTINE START", typeof(string));
            dt.Columns.Add("QUARANTINE END", typeof(string));
            dt.Columns.Add("EXTENSION", typeof(string));
            dt.Columns.Add("FTW", typeof(string));
            dt.Columns.Add("RETURN DATE", typeof(string));
            return dt;
        }

        public DataTable dtNotif()
        {
            DataTable dt2 = new DataTable();

            dt2.Columns.Add("Date", typeof(string));
            dt2.Columns.Add("Employee ID", typeof(string));
            dt2.Columns.Add("Employee Name", typeof(string));
            dt2.Columns.Add("Location", typeof(string));
            dt2.Columns.Add("Notification", typeof(string));
            dt2.Columns.Add("Supervisor", typeof(string));
            dt2.Columns.Add("Next Level Manager", typeof(string));
            dt2.Columns.Add("Third Level Manager", typeof(string));
            dt2.Columns.Add("Site HR", typeof(string));


            return dt2;
        }

        public DataTable dtNotifTask2()
        {
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("ID", typeof(string));
            dt2.Columns.Add("Date", typeof(string));
            dt2.Columns.Add("Employee ID", typeof(string));
            dt2.Columns.Add("Employee Name", typeof(string));
            dt2.Columns.Add("Location", typeof(string));
            dt2.Columns.Add("Notification", typeof(string));
            dt2.Columns.Add("Supervisor", typeof(string));
            dt2.Columns.Add("Next Level Manager", typeof(string));
            dt2.Columns.Add("Third Level Manager", typeof(string));
            dt2.Columns.Add("Site HR", typeof(string));

            dt2.Columns.Add("General Category", typeof(string));
            dt2.Columns.Add("DOH Category", typeof(string));
            dt2.Columns.Add("Severity", typeof(string));
            dt2.Columns.Add("Recommendation", typeof(string));
            dt2.Columns.Add("Quarantine Start Date", typeof(string));
            dt2.Columns.Add("Quarantine End Date", typeof(string));
            dt2.Columns.Add("New Quarantine End Date", typeof(string));
            dt2.Columns.Add("Sender", typeof(string));

            return dt2;
        }

        public DataTable dtInitial()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("EID", typeof(string));
            dt1.Columns.Add("First Name", typeof(string));
            dt1.Columns.Add("Middle Name", typeof(string));
            dt1.Columns.Add("Last Name", typeof(string));
            dt1.Columns.Add("DOH", typeof(string));
            dt1.Columns.Add("DOB", typeof(string));
            dt1.Columns.Add("Contact Number", typeof(string));
            dt1.Columns.Add("W/REFERRAL", typeof(string));
            dt1.Columns.Add("Gender", typeof(string));
            dt1.Columns.Add("Civil Status", typeof(string));
            dt1.Columns.Add("Remarks", typeof(string));
            dt1.Columns.Add("Address", typeof(string));
            dt1.Columns.Add("SITE", typeof(string));
            dt1.Columns.Add("MC DATE", typeof(string));

            return dt1;
        }
        public string GetHR_Info2(string location, DataTable HR, bool AvoidClinic)
        {
            log.Info("Location " + location);

            if (location.Contains("Damosa")) location = "Damosa";

            if (location.Contains("CyberOne")) location = "Eastwood";

            if (location.ToUpper().Contains("CEBU ")

            && !location.ToUpper().Contains("ASIATOWN")
            && !location.ToUpper().Contains("MACTAN"))
            {
                location = location.Replace("10", "");
                location = location.Replace("20", "");

                if (location.Contains("1")) location = "Cebu i1";
                if (location.Contains("2")) location = "Cebu i2";
                if (location.Contains("3")) location = "Cebu i3";
            }


            if (location.ToUpper().Contains("CLARK"))
            {
                if (location.Contains("1")) location = "Clark 1";
                if (location.Contains("2")) location = "Clark 2";

                if (!location.Contains("1")
                && !location.Contains("2")) location = "Clark 1";
            }

            if (location.Trim() == "Baguio - Ayalaland Technohub Bldg A")
                location = "Baguio City - TechnoHub A";
            if (location.Trim() == "Baguio - Ayalaland Technohub Bldg B")
                location = "Baguio City - TechnoHub B";

            if (location.Contains("Pasay")) location = "MOA";
            if (location.Contains("Bonifacio")) location = "Bonifacio";
            if (location.Contains("Fort BG")) location = "Bonifacio";

            if (location.Contains("Nuvali")) location = "Laguna";

            string where1 = "";
            string emails = "";

            foreach (DataRow dr in HR.Rows)
            {
                string loc = dr[0].ToString();
                if (location.ToUpper().Contains(loc.ToUpper()))
                {
                    where1 = "Location Like '" + loc + "'";

                    if (where1 == "") continue;

                    DataRow[] HREmails = HR.Select(where1);
                    if (HREmails == null) continue;

                    for (int i = 0; i < HREmails.Length; i++)
                    {
                        string OneName = HREmails[i][1].ToString();
                        string OneEmail = HREmails[i][2].ToString();
                        if (AvoidClinic && OneName.ToUpper().Contains("CLINIC"))
                            OneEmail = "";

                        if (!emails.Contains(OneEmail))
                        {
                            if (emails == "") emails = OneEmail;
                            else emails = emails + ";" + OneEmail;
                        }
                    }
                }
            }

            log.Info("Site HR " + emails);
            return emails;
        }
        public DataTable ReadXML()
        {
            string XMLFile = ConfigurationManager.AppSettings["XMLSites"].ToString();
            DataSet ds = new DataSet();
            ds.ReadXml(XMLFile);
            DataTable dt = ds.Tables[0];
            int i = dt.Rows.Count;
            return dt;
        }
        public DataTable GetData(int option, string lista)
        {
            //string sql = "SELECT EmpID, FirstName+' '+LastName, EmpLocation, ";
            string sql = "SELECT EmpID, EmpName, EmpLocation, ";
            sql = sql + " QuarantineStartDate, QuarantineEndDate,";
            sql = sql + " dtExtEnd, SQAdditionalDays, EmpProgram, EmpMobile, Landline, ";
            sql = sql + " BestTime, DOH   ";

            sql = sql + " FROM DeclarationForms (nolock) ";
            sql = sql + " WHERE STATUS <> 'Notified FTW' ";

            if (lista == "")
                sql = sql + " AND datecreated > DATEADD(day, -100, convert(date, GETDATE())) ";
            else
                sql = sql + lista;
           // sql = sql + " AND EMPID = '1063792'";
          

            if (option == 14)
            {
                Exposure.Program condition = new Exposure.Program();
                sql = sql + condition.DailyReportCondition();
            }

            return GetTable(sql);
        }

        public DataTable GetData14()
        {
            //string sql = "SELECT EmpID, FirstName+' '+LastName, EmpLocation, ";
            string sql = "SELECT EmpID, EmpName, EmpLocation, ";
            sql = sql + " QuarantineStartDate, QuarantineEndDate,";
            sql = sql + " dtExtEnd, SQAdditionalDays, EmpProgram, EmpMobile,";
            sql = sql + " Landline, BestTime, DOH, ";

            sql = sql + " Category,Recommendation,WhereAbouts,CurrFaculty,";
            sql = sql + " dtConfinement,ICU, dtFirstSymptoms,MoreDetails,";
            sql = sql + " Severity,BHERT, ID,";

            sql = sql + " DateCreated, A1SQNewEndDate  ";

            sql = sql + " FROM DeclarationForms (nolock) ";
            //sql = sql + " WHERE  datecreated > DATEADD(day, -300, convert(date, GETDATE())) ";
            // sql = sql + " AND EMPID = '1063792'";
            sql = sql + " WHERE STATUS <> 'Notified FTW' ";

            sql = sql + " AND DOH <> '' ";
            sql = sql + " AND DOH is not null ";

            sql = sql + " AND Recommendation <> '' ";
            sql = sql + " AND Recommendation is not null ";
           // sql = sql + " and ID IN (15166,15169,15178,15180,15153,15184) ";
            return GetTable(sql);
        }

        public DataTable GetDataExposure(string TASK)
        {
            string sql = "";
            if (TASK == "TASK1")
            {
                sql = "SELECT EmpID, EmpName, EmpLocation, ";
                sql = sql + " QuarantineStartDate, QuarantineEndDate,";
                sql = sql + " dtExtEnd, SQAdditionalDays, DateCreated,   ";
                sql = sql + " EmpMobile, Landline,   ";
                sql = sql + " POC, RepType, ForCTracing, Severity, BHERT, dtFTW, ";
                sql = sql + " CurrFaculty,moredetails,ICU, dtConfinement, FirstName, MSA, LastName, ID, MiddleName ";

                sql = sql + " FROM DeclarationForms (nolock) ";
                sql = sql + " WHERE STATUS <> 'Notified FTW' ";
            }
            else
            {
                sql = "SELECT CT_EmpID, CT_FirstName+' '+CT_LastName, CT_EmpLocation, ";
                sql = sql + " CT_QuarantineStartDate, CT_QuarantineEndDate,";
                sql = sql + " CT_dtExtEnd, ' ', CT_DateCreated,   ";
                sql = sql + " CT_EmpMobile, CT_Landline, CT_WD_ID, ID   ";

                sql = sql + " FROM DeclarationFormsCT (nolock) ";
                //sql = sql + " where status <> 'Notified FTW' ";
            }

            if (TASK == "TASK2")
            {
                sql = "SELECT EmpID, EmpName, EmpLocation, ";
                sql = sql + " FirstName, MiddleName, LastName, DateCreated, ID ";

                sql = sql + " FROM DeclarationForms (nolock) ";
                sql = sql + " WHERE STATUS <> 'Notified FTW' ";
                //sql = sql + " AND EMPID = '912550'";
            }

            //sql = sql + " WHERE  datecreated > DATEADD(day, -50, convert(date, GETDATE())) ";
            // sql = sql + " AND ID < 2076";
            return GetTable(sql);
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

            try {
                sqlcon.Dispose();
            }
            catch (Exception) { }

            SendEmails sent = new SendEmails();
            sent.SendEmail("Error Common.DataAccess.GetTable", sql + " " + err.ToString(), "", "");
            log.Info(sql + " " + err.ToString());
            return null;
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
        public int UpdateStatus(string EmpID, string status)
        {
            string sql = "UPDATE DeclarationForms ";
            sql = sql + " SET STATUS = '" + status + "', ";

            var timeToConvert = DateTime.Now;
            var est = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            string today1 = TimeZoneInfo.ConvertTime(timeToConvert, est).ToString();

            sql = sql + " DateModified ='" + today1 + "' ";

            sql = sql + " WHERE EmpID='" + EmpID + "' ";
            sql = sql + " AND STATUS <> 'Notified FTW' ";

            return UpdateDB(sql);
        }
        public int UpdateDB(string sql)
        {
            string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(SqlConn);
            sqlcon.Open();

            try
            {
                SqlCommand comm = new SqlCommand(sql, sqlcon);
                comm.ExecuteNonQuery();
                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception ws)
            {
                SendEmails sent = new SendEmails();
                sent.SendEmail("EXPOSURE ERROR Common.DataAccess.UpdateDB ", sql + "</br>,/br>" + ws.ToString(), "", "");
                log.Info("Error " + sql + "  " + ws.ToString());
                sqlcon.Close();
                sqlcon.Dispose();

                return 0;
            }
            return 1;
        }
    }
}
