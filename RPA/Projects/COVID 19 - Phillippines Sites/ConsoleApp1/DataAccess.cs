using log4net;
using Objects;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ConsoleApp1
{
    class DataAccess
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
                
        public string ExposureDB(ContactDate ct, string senderName,
                           byte[] doc1, string ext1,
                           byte[] doc2, string ext2,
                           byte[] doc3, string ext3, string Source)
        {

#if DEBUG
            //  if (email != "tomas.dale@concentrix.com") return 1;
#endif
            log.Info("NEW  - " + ct.EmpID + " " + ct.EmpName + " " + ct.Mobile);

            string Supervisor, NextLevel;
            Supervisor = NextLevel = "";
            Funciones fx = new Funciones();
            SendErrorEmail Errors = new SendErrorEmail();
            
            Exposure.DataBase db = new Exposure.DataBase();
            string[] Supervisors = db.GetManagers(ct.EmpID);

            Supervisor = Supervisors[0].ToString();
            NextLevel = Supervisors[2].ToString();
            Supervisor = Supervisor.Replace("'", "");
            NextLevel = NextLevel.Replace("'", "");

            if (ct.Email == "") ct.Email = Supervisor[6].ToString();

            if (ct.Remarks != "")  ct.Remarks = fx.AppendTime(ct.Remarks);
            if (ct.Symptoms!="") ct.Symptoms = fx.AppendTime(ct.Symptoms); // Symptoms - details

            ct.EmpProgram = DataAccessUpdate.GetProgram(ct.EmpID);

            if (ext1.Length > 4) ext1 = "";
            if (ext2.Length > 4) ext2 = "";
            if (ext3.Length > 4) ext3 = "";

            if (ct.EmpName == "" || ct.EmpName == null)
                ct.EmpName = ct.FirstName + " " + ct.MiddleName + " " + ct.LastName;

            string sql = "INSERT into DeclarationForms ";
            sql = sql + " (Email, EmpID, EmpName, EmpMobile,  ";
            sql = sql + " Landline, DateContact, WhereAbouts, ";
            sql = sql + " EmpLocation, EmpProgram, EmpTeamLeader, EmpNextLevelMgr, ";
            sql = sql + " MoreDetails, LastBadge, Remarks,   ";
            sql = sql + " QuarantineStartDate, QuarantineEndDate, ReportSource, InitialRemarks, ";
            sql = sql + " FirstName, MiddleName, LastName, ";
            sql = sql + " File1, ext1, File2, ext2, File3, ext3, ";
            sql = sql + " BestTime, Transport, RepType, ";

            sql = sql + " dtRelease,  POC,  TestType,  dtTest, ";
            sql = sql + " Source, ";
            sql = sql + " FourPillars,  PCR,  TotPCR,  Faculty,  CurrFaculty, ";
            sql = sql + " WD_ID,  dtFirstSymptoms,  Category, ";
            sql = sql + " dtExtEnd,  dtFTW,  dtReturn,  dtConfinement, ";
            sql = sql + " dtReported,  ICU, dtContactConfirm,  ";
            sql = sql + " EmpNameCNX, EmpAddress, Residence, MSA, A1SQNewEndDate ) ";

            sql = sql + " VALUES (@email, @EmpID, @Name1, @Mobile, ";
            sql = sql + " @Landline, @dtContact, @Latest,  ";
            sql = sql + " @Location,   @Program,   @Supervisor,   @NextMgr, ";
            sql = sql + " @MoreDetails,@LastBadge, @Remarks, ";
            sql = sql + " @Start, @End,   @ReportSource, '"+ ct.InitialRemarks+"', ";
            sql = sql + " @First, @Middle,      @Last,   ";
            sql = sql + " @doc1, @ext1, @doc2, @ext2, @doc3, @ext3, ";
            sql = sql + " @BestTime, @Transport, @RepType, ";

            sql = sql + " @dtRelease, @POC,  @TestType,  @dtTest, ";
            sql = sql + " @Source, ";
            sql = sql + " @FourPillars,  @PCR,  @TotPCR,  @Faculty,  @CurrFaculty, ";
            sql = sql + " @WD_ID,  @dtFirstSymptoms,  @Category, ";

            sql = sql + " @dtExtEnd,  @dtFTW,  @dtReturn,  @dtConfinement, ";
            sql = sql + " @dtReported, @ICU, @dtContactConfirm, ";
            sql = sql + " @EmpNameCNX, @Address, @Residence, ";
            sql = sql +   "'" + ct.MSA + "', '" + ct.A1SQNewEndDate+"') " ;

            try
            {

                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                using (SqlCommand comm = new SqlCommand(sql, sqlcon))
                {
                    //string ss = fx.Today();

                    comm.Parameters.AddWithValue("@email", ct.sender);
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
                    //comm.Parameters.AddWithValue("@Recommendation", ct.Recommendation);

                    if (ct.RepType == "") ct.RepType = " ";
                    comm.Parameters.AddWithValue("@RepType", ct.RepType);
                    comm.Parameters.AddWithValue("@BestTime", ct.BestTime);
                    comm.Parameters.AddWithValue("@Transport", ct.Transport);

                    comm.Parameters.AddWithValue("@dtRelease", ct.dtRelease);
                    comm.Parameters.AddWithValue("@POC", ct.POC);
                    comm.Parameters.AddWithValue("@TestType", ct.TestType);
                    comm.Parameters.AddWithValue("@dtTest", ct.dtTest);
                    comm.Parameters.AddWithValue("@Source", Source);
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
                    comm.Parameters.AddWithValue("@dtReported", ct.DateCreated);
                    comm.Parameters.AddWithValue("@ICU", ct.ICU);

                    comm.Parameters.AddWithValue("@dtContactConfirm", ct.dtContactConfirm);
                    comm.Parameters.AddWithValue("@EmpNameCNX", ct.EmpNameCNX);
                    comm.Parameters.AddWithValue("@Address", ct.Address);
                    comm.Parameters.AddWithValue("@Residence", ct.Residence);

                    comm.Parameters.AddWithValue("@ext1", ext1);
                    comm.Parameters.AddWithValue("@ext2", ext2);
                    comm.Parameters.AddWithValue("@ext3", ext3);

                    if (ext1 != "")
                        comm.Parameters.Add("@doc1", SqlDbType.Image, doc1.Length).Value = doc1;
                    else
                        comm.Parameters.Add("@doc1", SqlDbType.Image, 1).Value = DBNull.Value;

                    if (ext2 != "")
                        comm.Parameters.Add("@doc2", SqlDbType.Image, doc2.Length).Value = doc2;
                    else
                        comm.Parameters.Add("@doc2", SqlDbType.Image, 1).Value = DBNull.Value;

                    if (ext3 != "")
                        comm.Parameters.Add("@doc3", SqlDbType.Image, doc3.Length).Value = doc3;
                    else
                        comm.Parameters.Add("@doc3", SqlDbType.Image, 1).Value = DBNull.Value;

                    comm.ExecuteNonQuery();
                    log.Info("ADDED");
                }

                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception ws)
            {
                SendErrorEmail Errors2 = new SendErrorEmail();
                string msgErr = "ERROR INSERTING " + ct.EmpID + " " + ct.EmpName + " " + ws.ToString();
                if (msgErr.Contains("String or binary data would be truncated"))
                    msgErr = "ERROR when inserting " + ct.EmpID + " " + ct.EmpName + " a field is too long";
                Errors.GeneralError(msgErr, ct.sender, senderName);
                return "";
            }

            if (ct.Location == "") ct.Location = "x";
            return ct.Location;
        }

        public string CreateFromNotification(string EmpID, string sender, string status,
           string EmpLoc, string EmpProgram, string EmpFirst, string EmpMiddle, string EmpLast,
           string Mobile, string Landline, string TimeCall)
        {
            string Supervisor, NextLevel;
            Supervisor = NextLevel = "";

            Funciones fx = new Funciones();
            SendErrorEmail Errors = new SendErrorEmail();

            Exposure.DataBase db = new Exposure.DataBase();
            string[] Supervisors = db.GetManagers(EmpID);

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
                    log.Info("ADDED NOTIFICATION "+EmpID+" "+EmpName);
                }

                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception ws)
            {
                SendErrorEmail Errors2 = new SendErrorEmail();
                string msgErr = "ERROR INSERTING " + EmpID + " " + EmpName + " " + ws.ToString();
                Errors.GeneralError(msgErr, sender, "");
                return "";
            }

            return "DONE";
        }
    }
}
