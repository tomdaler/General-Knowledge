using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace UpdateDatabase
{
    public class DataAccess
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

        public string NewExposure(ContactDate ct, string senderName)
        {

            log.Info("NEW  - " + ct.EmpID + " " + ct.EmpName + " " + ct.Mobile);

            string Supervisor, NextLevel;
            Supervisor = NextLevel = "";
            
            string[] Supervisors = GetManagers(ct.EmpID);

            Supervisor = Supervisors[0].ToString();
            NextLevel = Supervisors[2].ToString();
            Supervisor = Supervisor.Replace("'", "");
            NextLevel = NextLevel.Replace("'", "");

            try {
                if (ct.Email == "") ct.Email = Supervisor[6].ToString();
            }
            catch (Exception) { }

            if (ct.EmpProgram == "")
                ct.EmpProgram = GetProgram(ct.EmpID);

            if (ct.EmpName == "" || ct.EmpName == null)
                ct.EmpName = ct.FirstName + " " + ct.MiddleName + " " + ct.LastName;

            string sql = "INSERT into DeclarationForms ";
            sql = sql + " (Email, EmpID, EmpName, EmpMobile,  ";
            sql = sql + " Landline, DateContact, WhereAbouts, ";
            sql = sql + " EmpLocation, EmpProgram, EmpTeamLeader, EmpNextLevelMgr, ";
            sql = sql + " MoreDetails, LastBadge, Remarks,   ";
            sql = sql + " QuarantineStartDate, QuarantineEndDate, ReportSource, InitialRemarks, ";
            sql = sql + " FirstName, MiddleName, LastName, ";
            sql = sql + " BestTime, Transport, RepType,Status, ";

            sql = sql + "closecontact,DOH,CONTACTTRACING, ForCTracing, SEVERITY,";
            sql = sql + "BHERT,dtCTA1End,dtCTA1Start,";

            sql = sql + " dtRelease,  POC,  TestType,  dtTest, ";
            sql = sql + " Source, ";
            sql = sql + " FourPillars,  PCR,  TotPCR,  Faculty,  CurrFaculty, ";
            sql = sql + " WD_ID,  dtFirstSymptoms,  Category, Recommendation, ";
            sql = sql + " dtExtEnd,  dtFTW,  dtReturn,  dtConfinement, ";
            sql = sql + " DateCreated,  ICU, dtContactConfirm,  ";
            sql = sql + " EmpNameCNX, EmpAddress, Residence, DateModified, MSA, A1SQNewEndDate ) ";

            sql = sql + " VALUES (@email, @EmpID, @Name1, @Mobile, ";
            sql = sql + " @Landline, @dtContact, @Latest,  ";
            sql = sql + " @Location,   @Program,   @Supervisor,   @NextMgr, ";
            sql = sql + " @MoreDetails,@LastBadge, @Remarks, ";
            sql = sql + " @Start, @End,   @ReportSource, '" + ct.InitialRemarks + "', ";
            sql = sql + " @First, @Middle,      @Last,   ";
            sql = sql + " @BestTime, @Transport, @RepType, @Status, ";


            sql = sql + "@closecontact, @DOH, @CONTACTTRACING, @ForCTracing, @SEVERITY,";
            sql = sql + "@BHERT, @dtCTA1End, @dtCTA1Start,";

            sql = sql + " @dtRelease, @POC,  @TestType,  @dtTest, ";
            sql = sql + " @Source, ";
            sql = sql + " @FourPillars,  @PCR,  @TotPCR,  @Faculty,  @CurrFaculty, ";
            sql = sql + " @WD_ID,  @dtFirstSymptoms,  @Category, @Recommendation, ";

            sql = sql + " @dtExtEnd,  @dtFTW,  @dtReturn,  @dtConfinement, ";
            sql = sql + " @DateCreated, @ICU, @dtContactConfirm, ";
            sql = sql + " @EmpNameCNX, @Address, @Residence, @dtModified, ";
            sql = sql + "'" + ct.MSA + "', '" + ct.A1SQNewEndDate + "') ";

            try
            {
                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                using (SqlCommand comm = new SqlCommand(sql, sqlcon))
                {
                    //string ss = fx.Today();

                    comm.Parameters.AddWithValue("@email", ct.Email);
                    comm.Parameters.AddWithValue("@Status", ct.Status);
                    //comm.Parameters.AddWithValue("@created", fx.Today()); //  emailCreated);

                    comm.Parameters.AddWithValue("@EmpID", ct.EmpID);
                    comm.Parameters.AddWithValue("@Name1", ct.EmpName);
                    comm.Parameters.AddWithValue("@First", ct.FirstName);
                    comm.Parameters.AddWithValue("@Last", ct.LastName);
                    comm.Parameters.AddWithValue("@Middle", ct.MiddleName);

                    comm.Parameters.AddWithValue("@Mobile", ct.Mobile);
                    comm.Parameters.AddWithValue("@Landline", ct.Landline);
                    comm.Parameters.AddWithValue("@Latest", ct.WhereAbouts);
                    comm.Parameters.AddWithValue("@dtContact", ct.dtContactConfirm);

                    comm.Parameters.AddWithValue("@Location", ct.Location);
                    comm.Parameters.AddWithValue("@Supervisor", Supervisor);
                    comm.Parameters.AddWithValue("@Program", ct.EmpProgram);
                    comm.Parameters.AddWithValue("@NextMgr", NextLevel);

                    comm.Parameters.AddWithValue("@MoreDetails", ct.Symptoms);
                    comm.Parameters.AddWithValue("@LastBadge", ct.LastBadge);
                    comm.Parameters.AddWithValue("@Remarks", ct.Remarks);

                    comm.Parameters.AddWithValue("@Start", ct.QuarantineStartDate);
                    comm.Parameters.AddWithValue("@End", ct.QuarantineEndDate);

                    comm.Parameters.AddWithValue("@ReportSource", ct.ReportSource);
                    comm.Parameters.AddWithValue("@Recommendation", ct.Recommendation);


                    comm.Parameters.AddWithValue("@closecontact", ct.CloseContact);
                    comm.Parameters.AddWithValue("@DOH", ct.DOH);
                    comm.Parameters.AddWithValue("@CONTACTTRACING", ct.ContactTracing);
                    comm.Parameters.AddWithValue("@ForCTracing", ct.ForCTTracing);

                    comm.Parameters.AddWithValue("@SEVERITY", ct.Severity);

                    comm.Parameters.AddWithValue("@BHERT", ct.BHERT);
                    comm.Parameters.AddWithValue("@dtCTA1Start", ct.DtCTA1End);
                    comm.Parameters.AddWithValue("@dtCTA1End", ct.DtCTA1End);



                    if (ct.RepType == "") ct.RepType = " ";
                    comm.Parameters.AddWithValue("@RepType", ct.RepType);
                    comm.Parameters.AddWithValue("@BestTime", ct.BestTime);
                    comm.Parameters.AddWithValue("@Transport", ct.Transport);

                    comm.Parameters.AddWithValue("@dtRelease", ct.dtRelease);
                    comm.Parameters.AddWithValue("@POC", ct.POC);
                    comm.Parameters.AddWithValue("@TestType", ct.TestType);
                    comm.Parameters.AddWithValue("@dtTest", ct.dtTest);
                    comm.Parameters.AddWithValue("@Source", "OPS");
                    comm.Parameters.AddWithValue("@FourPillars", ct.FourPillars);
                    comm.Parameters.AddWithValue("@PCR", ct.PCR);
                    comm.Parameters.AddWithValue("@TotPCR", ct.TotPCR);

                    comm.Parameters.AddWithValue("@Faculty", ct.Faculty);
                    comm.Parameters.AddWithValue("@CurrFaculty", ct.CurrFaculty);
                    comm.Parameters.AddWithValue("@WD_ID", ct.WD_ID);
                    comm.Parameters.AddWithValue("@dtFirstSymptoms", ct.dtFirstSymptoms);

                    comm.Parameters.AddWithValue("@Category", ct.Category);
                    comm.Parameters.AddWithValue("@dtExtEnd", ct.dtExtEnd);
                    comm.Parameters.AddWithValue("@dtFTW", ct.dtFTW);
                    comm.Parameters.AddWithValue("@dtReturn", ct.dtReturn);
                    comm.Parameters.AddWithValue("@dtConfinement", ct.dtConfinement);
                
                    comm.Parameters.AddWithValue("@ICU", ct.ICU);
                    comm.Parameters.AddWithValue("@dtContactConfirm", ct.dtContactConfirm);
                    comm.Parameters.AddWithValue("@EmpNameCNX", ct.EmpNameCNX);
                    comm.Parameters.AddWithValue("@Address", ct.Address);
                    comm.Parameters.AddWithValue("@Residence", ct.Residence);
                    comm.Parameters.AddWithValue("@dtModified", ct.dtModified);

                    comm.Parameters.AddWithValue("@DateCreated", ct.DateCreated);
                    comm.ExecuteNonQuery();
                    log.Info("ADDED");
                }

                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception ws)
            {
                //SendErrorEmail Errors2 = new SendErrorEmail();
                string msgErr = "ERROR INSERTING " + ct.EmpID + " " + ct.EmpName + " " + ws.ToString();
                if (msgErr.Contains("String or binary data would be truncated"))
                    msgErr = "ERROR when inserting " + ct.EmpID + " " + ct.EmpName + " a field is too long";
                //Errors.GeneralError(msgErr, ct.sender, senderName);
                return "ERR";
            }

            if (ct.Location == "") ct.Location = "No Location Found";
            return ct.Location;
        }

        static public string GetProgram(string EmpID)
        {
            string SqlEmpl = ConfigurationManager.ConnectionStrings["SqlEmpl"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(SqlEmpl);
            sqlcon.Open();

            string sql = "SELECT top(1) Program_Name FROM Employee(nolock) ";
            sql = sql + " WHERE HR_ID = '" + EmpID + "'";
            sql = sql + " AND UPPER(Country) = 'PHILIPPINES' ";

            // IMPORTANT
            sql = sql + " ORDER BY HR_Effective_Date DESC";

            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            var program = cmd.ExecuteScalar();
            string program2 = "";
            if (program != null) program2 = program.ToString();
            sqlcon.Dispose();
            sqlcon.Close();

            return program2;
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


        public string CreateFromNotification(string EmpID, string sender, string status,
           string EmpLoc, string EmpProgram, string EmpFirst, string EmpMiddle, string EmpLast,
           string Mobile, string Landline, string TimeCall)
        {
            string Supervisor, NextLevel;
            Supervisor = NextLevel = "";

            string[] Supervisors = GetManagers(EmpID);

            Supervisor = Supervisors[0].ToString();
            NextLevel = Supervisors[2].ToString();
            Supervisor = Supervisor.Replace("'", "");
            NextLevel = NextLevel.Replace("'", "");

            string EmpName = EmpFirst + " " + EmpLast;

            string sql = "INSERT into DeclarationForms ";
            sql = sql + " (Email, EmpID, EmpName, EmpLocation, EmpProgram, EmpTeamLeader, EmpNextLevelMgr, ";
            sql = sql + " FirstName, MiddleName, LastName, Source, status,  ";
            sql = sql + " EmpMobile, Landline, BestTime, ";

            sql = sql + " Whereabouts, Transport, EmpAddress, DateContact, ";

            sql = sql + " MoreDetails, LastBadge, Remarks, RepType, Residence,   ";
            sql = sql + " QuarantineStartDate, QuarantineEndDate, ReportSource, InitialRemarks ) ";

            sql = sql + " VALUES (@email, @EmpID, @Name1, @Location,   @Program,   @Supervisor,   @NextMgr, ";
            sql = sql + " @First, @Middle,  @Last, 'OPS', @status,  ";
            sql = sql + " @Mobile, @Landline, @TimeCall,  ";

            sql = sql + " ' ', ' ', ' ',' ',  ";
            sql = sql + " ' ', ' ', ' ', ' ', ' ', ";
            sql = sql + " ' ', ' ', ' ', ' ' ) ";

            try
            {
                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                using (SqlCommand comm = new SqlCommand(sql, sqlcon))
                {
                    comm.Parameters.AddWithValue("@email", sender);

                    comm.Parameters.AddWithValue("@EmpID", EmpID);
                    comm.Parameters.AddWithValue("@Name1", EmpName);
                    comm.Parameters.AddWithValue("@First", EmpFirst);
                    comm.Parameters.AddWithValue("@Last", EmpLast);
                    comm.Parameters.AddWithValue("@Middle", EmpMiddle);
                    comm.Parameters.AddWithValue("@status", status);

                    comm.Parameters.AddWithValue("@Location", EmpLoc);
                    comm.Parameters.AddWithValue("@Program", EmpProgram);

                    comm.Parameters.AddWithValue("@Supervisor", Supervisor);
                    comm.Parameters.AddWithValue("@NextMgr", NextLevel);

                    comm.Parameters.AddWithValue("@Mobile", Mobile);
                    comm.Parameters.AddWithValue("@Landline", Landline);
                    comm.Parameters.AddWithValue("@TimeCall", TimeCall);

                    comm.ExecuteNonQuery();
                    log.Info("ADDED NOTIFICATION " + EmpID + " " + EmpName);
                }

                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception ws)
            {
                //SendErrorEmail Errors2 = new SendErrorEmail();
                string msgErr = "ERROR INSERTING " + EmpID + " " + EmpName + " " + ws.ToString();
                //Errors.GeneralError(msgErr, sender, "");
                return "";
            }

            return "DONE";
        }
    }
}
