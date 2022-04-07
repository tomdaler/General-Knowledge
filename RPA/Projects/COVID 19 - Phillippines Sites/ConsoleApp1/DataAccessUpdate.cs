using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Objects;
namespace ConsoleApp1
{
    static public class DataAccessUpdate
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
        static public void UpdateA1SQNewEndDate(string EmpID)
        {
            Funciones fx = new Funciones();
            string sql = "UPDATE DeclarationForms ";
            sql = sql + " SET A1SQNewEndDate='" + fx.TodayDate() + "' ";
            sql = sql + " WHERE EmpID ='" + EmpID + "' ";
            sql = sql + " AND   Status <> 'Notified FTW'";

            sql = sql + " AND (A1SQNewEndDate='' OR A1SQNewEndDate is null ) ";
            DataAccessUpdate.UpdateSql(sql);
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

        static public void NewNumber(ContactDate ct)
        {
            string sender = ct.sender;
            try { if (ct.Email == "Survey Monkey") sender = "Survey Monkey"; }
            catch (Exception) { }

            string sql = "insert into  DeclarationFormsContactNumber ";
            sql = sql + " (EmpID, EmpName, EmpMobile, LandLine, sender ) ";
            sql = sql + " values (";
            sql = sql + "'" + ct.EmpID + "', ";
            sql = sql + "'" + ct.EmpName + "', ";
            sql = sql + "'" + ct.Mobile + "', ";
            sql = sql + "'" + ct.Landline + "', ";
            sql = sql + "'" + sender + "') ";
            UpdateSql(sql);
        }

        static public void InsertDeclarationForms(ContactDate cd)
        {
            Exposure.DataBase db = new Exposure.DataBase();
            string[] Supervisors = db.GetManagers(cd.EmpID);

            string Supervisor = Supervisors[0].ToString();
            string NextLevel = Supervisors[2].ToString();
            Supervisor = Supervisor.Replace("'", "");
            NextLevel = NextLevel.Replace("'", "");

            if (cd.EmpName == "" || cd.EmpName == null)
                cd.EmpName = cd.FirstName + " " + cd.MiddleName + " " + cd.LastName;

            cd.EmpProgram = GetProgram(cd.EmpID);
            string sql = "insert into DeclarationForms (";

            sql = sql + " POC, ReportSource,  RepType, ForCTracing, CloseContact,";
            sql = sql + " TestType, PCR, EmpID,   FirstName,   MiddleName,   LastName,  ";
            sql = sql + " EmpProgram,  FourPillars,  EmpLocation , Residence , EmpMobile ,";
            sql = sql + " Landline , BestTime,  LastBadge, Transport , TotPCR,  dtTest, ";
            sql = sql + " dtRelease,  Faculty, ContactTracing , dtContactConfirm,  WD_ID, ";
            sql = sql + " EmpNameCNX,  WhereAbouts , CurrFaculty , dtConfinement,  ICU, ";
            sql = sql + " dtFirstSymptoms,  MoreDetails , Category,  DOH,  Severity,  ";
            sql = sql + " BHERT,  Recommendation , QuarantineStartDate,  QuarantineEndDate,  ";

            sql = sql + " EmpTeamLeader, EmpNextLevelMgr, ";

            sql = sql + " dtExtEnd,  dtFTW,  dtReturn, Email, MSA, UpdatedBy, ";
            sql = sql + " EmpName, Status, A1SQNewEndDate ) values (";

            // sql = sql + "'" + cd.DateCreated + "' ,";
            sql = sql + "'" + cd.POC + " ' ,";
            sql = sql + "'" + cd.ReportSource + " ' ,";
            sql = sql + "'" + cd.RepType + " ' ,";
            sql = sql + "'" + cd.ForCTTracing + " ' ,"; //======
            sql = sql + "'" + cd.CloseContact + " ' ,";
            sql = sql + "'" + cd.TestType + " ' ,";
            sql = sql + "'" + cd.PCR + " ' ,";
            sql = sql + "'" + cd.EmpID + " ' ,";
            sql = sql + "'" + cd.FirstName + " ' ,";
            sql = sql + "'" + cd.MiddleName + " ' ,";
            sql = sql + "'" + cd.LastName + " ' ,";
            sql = sql + "'" + cd.EmpProgram + " ' ,";
            sql = sql + "'" + cd.FourPillars + " ' ,";
            sql = sql + "'" + cd.Location + " ' ,";
            sql = sql + "'" + cd.Residence + " ' ,";
            sql = sql + "'" + cd.Mobile + " ' ,";
            sql = sql + "'" + cd.Landline + " ' ,";
            sql = sql + "'" + cd.BestTime + " ' ,";
            sql = sql + "'" + cd.LastBadge + " ' ,";
            sql = sql + "'" + cd.Transport + " ' ,";
            sql = sql + "'" + cd.TotPCR + " ' ,";
            sql = sql + "'" + cd.dtTest + " ' ,";
            sql = sql + "'" + cd.dtRelease + " ' ,";
            sql = sql + "'" + cd.Faculty + " ' ,";
            sql = sql + "'" + cd.ContactTracing + " ' ,";
            sql = sql + "'" + cd.dtContactConfirm + " ' ,";
            sql = sql + "'" + cd.WD_ID + " ' ,";
            sql = sql + "'" + cd.EmpNameCNX + " ' ,";
            sql = sql + "'" + cd.WhereAbouts + " ' ,";
            sql = sql + "'" + cd.CurrFaculty + " ' ,";
            sql = sql + "'" + cd.dtConfinement + " ' ,";
            sql = sql + "'" + cd.ICU + " ' ,";
            sql = sql + "'" + cd.dtFirstSymptoms + " ' ,";
            sql = sql + "'" + cd.Symptoms + " ' ,";
            sql = sql + "'" + cd.Category + " ' ,";
            sql = sql + "'" + cd.DOH + " ' ,";
            sql = sql + "'" + cd.Severity + " ' ,";
            sql = sql + "'" + cd.BHERT + " ' ,";
            sql = sql + "'" + cd.Recommendation + " ' ,";
            sql = sql + "'" + cd.QuarantineStartDate + " ' ,";
            sql = sql + "'" + cd.QuarantineEndDate + " ' ,";

            sql = sql + "'" + Supervisor + " ' ,";
            sql = sql + "'" + NextLevel + " ' ,";

            sql = sql + "'" + cd.dtExtEnd + " ' ,";
            sql = sql + "'" + cd.dtFTW + " ' ,";
            sql = sql + "'" + cd.dtReturn + " ', ";
            sql = sql + "'" + cd.Email + " ', ";
            sql = sql + "'" + cd.MSA + " ', ";
            sql = sql + "'" + cd.sender + " ', ";
            sql = sql + "'" + cd.EmpName + " ', ";

            sql = sql + "'" + cd.Status + "', ";
            sql = sql + "'" + cd.A1SQNewEndDate + "' ) ";

            UpdateSql(sql);

            //if (cd.Upd_dtA1SQNewEndDate) dtA1Assessment(cd.EmpID);
        }

        public static void UpdateDeclarationForms(ContactDate cd)
        {
            Exposure.DataBase db = new Exposure.DataBase();
            string[] Supervisors = db.GetManagers(cd.EmpID);

            Funciones fx = new Funciones();
            Duplicate duplicate = fx.IsDuplicated(cd.EmpID);
            if (cd.Mobile.Trim() != duplicate.EmpMobile.Trim()
              || cd.Landline.Trim() != duplicate.Landline.Trim())
            {

                DataAccessUpdate.NewNumber(cd);
            }

            string Supervisor = "";
            string NextLevel = "";

            if (Supervisors != null)
            {
                Supervisor = Supervisors[0].ToString();
                NextLevel = Supervisors[2].ToString();
                Supervisor = Supervisor.Replace("'", "");
                NextLevel = NextLevel.Replace("'", "");
            }

            string sql = "UPDATE DeclarationForms SET ";

            // sql = sql + "DateCreated='" + cd.DateCreated + "' ,";
            // location, msa, program, datecreated, names

            if (cd.POC.Trim() != "") sql = sql + "POC ='" + cd.POC + "' ,";
            if (cd.ReportSource.Trim() != "") sql = sql + "ReportSource ='" + cd.ReportSource + "' ,";
            if (cd.RepType.Trim() != "") sql = sql + "RepType  = '" + cd.RepType + "' ,";
            if (cd.ForCTTracing.Trim() != "") sql = sql + "ForCTracing  = '" + cd.ForCTTracing + "' ,";
            if (cd.CloseContact.Trim() != "") sql = sql + "CloseContact  = '" + cd.CloseContact + "' ,";
            if (cd.TestType.Trim() != "") sql = sql + "TestType= '" + cd.TestType + "' ,";
            if (cd.PCR.Trim() != "") sql = sql + "PCR= '" + cd.PCR + "' ,";

            if (cd.FourPillars.Trim() != "") sql = sql + "FourPillars  = '" + cd.FourPillars + "' ,";
            if (cd.Residence.Trim() != "") sql = sql + "Residence= '" + cd.Residence + "' ,";
            if (cd.Mobile.Trim() != "") sql = sql + "EmpMobile = '" + cd.Mobile + "' ,";
            if (cd.Landline.Trim() != "") sql = sql + "Landline  = '" + cd.Landline + "' ,";
            if (cd.BestTime.Trim() != "") sql = sql + "BestTime  = '" + cd.BestTime + "' ,";
            if (cd.LastBadge.Trim() != "") sql = sql + "LastBadge  = '" + cd.LastBadge + "' ,";
            if (cd.Transport.Trim() != "") sql = sql + "Transport  = '" + cd.Transport + "' ,";
            if (cd.TotPCR.Trim() != "") sql = sql + "TotPcr  = '" + cd.TotPCR + "' ,";
            if (cd.dtTest.Trim() != "") sql = sql + "dtTest  = '" + cd.dtTest + "' ,";
            if (cd.dtRelease.Trim() != "") sql = sql + "dtRelease  = '" + cd.dtRelease + "' ,";
            if (cd.Faculty.Trim() != "") sql = sql + "Faculty  = '" + cd.Faculty + "' ,";
            if (cd.ContactTracing.Trim() != "") sql = sql + "ContactTracing = '" + cd.ContactTracing + "' ,";  // Remarks
            if (cd.dtContactConfirm.Trim() != "") sql = sql + "dtContactConfirm  = '" + cd.dtContactConfirm + "' ,";
            if (cd.WD_ID.Trim() != "") sql = sql + "WD_ID  = '" + cd.WD_ID + "' ,";
            if (cd.EmpNameCNX.Trim() != "") sql = sql + "EmpNameCNX  = '" + cd.EmpNameCNX + "' ,";
            if (cd.WhereAbouts.Trim() != "") sql = sql + "WhereAbouts= '" + cd.WhereAbouts + "' ,";
            if (cd.CurrFaculty.Trim() != "") sql = sql + "CurrFaculty= '" + cd.CurrFaculty + "' ,";
            if (cd.dtConfinement.Trim() != "") sql = sql + "dtConfinement= '" + cd.dtConfinement + "' ,";

            if (cd.InitialRemarks != "") sql = sql + "InitialRemarks='" + cd.InitialRemarks + "' ,";
            if (cd.Remarks != "") sql = sql + "Remarks='" + cd.Remarks + "' ,";

            if (cd.ICU.Trim() != "") sql = sql + "ICU= '" + cd.ICU + "' ,";
            if (cd.dtFirstSymptoms.Trim() != "") sql = sql + "dtFirstSymptoms= '" + cd.dtFirstSymptoms + "' ,";
            if (cd.Symptoms.Trim() != "") sql = sql + "MoreDetails= '" + cd.Symptoms + "' ,"; // ModeDetails = Symptoms
            if (cd.Category.Trim() != "") sql = sql + "Category= '" + cd.Category + "' ,";
            if (cd.DOH.Trim() != "") sql = sql + "DOH= '" + cd.DOH + "' ,";
            if (cd.Severity.Trim() != "") sql = sql + "Severity = '" + cd.Severity + "' ,";
            if (cd.BHERT.Trim() != "") sql = sql + "BHERT= '" + cd.BHERT + "' ,";
            if (cd.Recommendation.Trim() != "") sql = sql + "Recommendation= '" + cd.Recommendation + "' ,";
            if (cd.QuarantineStartDate.Trim() != "") sql = sql + "QuarantineStartDate= '" + cd.QuarantineStartDate + "' ,";
            if (cd.QuarantineEndDate.Trim() != "") sql = sql + "QuarantineEndDate= '" + cd.QuarantineEndDate + "' ,";
            if (cd.dtExtEnd.Trim() != "") sql = sql + "dtExtEnd  = '" + cd.dtExtEnd + "' ,";
            if (cd.dtFTW.Trim() != "") sql = sql + "dtFTW = '" + cd.dtFTW + "' ,";
            if (cd.dtReturn.Trim() != "") sql = sql + "dtReturn = '" + cd.dtReturn + "',  ";
            if (cd.Address.Trim() != "") sql = sql + "EmpAddress = '" + cd.Address + "',  ";

            if (cd.dtA1Assessment.Trim() != "") sql = sql + "dtA1Assessment= '" + cd.dtA1Assessment + "',  ";
                       
            if (Supervisor.Trim() != "") sql = sql + "EmpTeamLeader= '" + Supervisor + "',  ";
            if (NextLevel.Trim() != "") sql = sql + "EmpNextLevelMgr= '" + NextLevel + "',  ";

            
            sql = sql + "DateModified = '" + fx.Today() + "', ";

            if (cd.Email == "") cd.Email = cd.sender;
            sql = sql + "UpdatedBy='" + cd.Email + "', ";

            sql = sql + "Status ='" + cd.Status + "' ";

            sql = sql + " WHERE EMPID = '" + cd.EmpID + "'";
            sql = sql + " AND STATUS <> 'Notified FTW' ";

            UpdateSql(sql);

            //if (cd.Upd_dtA1SQNewEndDate) 
            // UpdateA1SQNewEndDate(cd.EmpID);
            //dtA1Assessment(cd.EmpID);
        }

        //static public void A1SQNewEndDate(string EmpID)
        //{
        //    Funciones fx = new Funciones();
        //    string sql = "UPDATE DeclarationForms ";
        //    sql = sql + " SET A1SQNewEndDate = '" + fx.Today() + "'";

        //    sql = sql + " WHERE EMPID = '" + EmpID + "'";
        //    sql = sql + " AND STATUS <> 'Notified FTW' ";

        //    sql = sql + " AND (A1SQNewEndDate is null ";
        //    sql = sql + " OR   A1SQNewEndDate = '' ) ";
        //    UpdateSql(sql);
        //}

        static public void InsertDeclarationFormsCT(ContactDate cd)
        {
            Funciones fx = new Funciones();

            cd.EmpProgram = GetProgram(cd.EmpID);
            string sql = "insert into DeclarationFormsCT (";

            sql = sql + " CT_DateCreated, CT_POC, CT_ReportSource,  CT_RepType, CT_ForCTracing, CT_CloseContact,";
            sql = sql + " CT_TestType, CT_PCR,  CT_EmpID,   CT_FirstName,   CT_MiddleName,   CT_LastName,  ";
            sql = sql + " CT_EmpProgram,  CT_FourPillars,  CT_EmpLocation , CT_Residence , CT_EmpMobile ,";
            sql = sql + " CT_Landline , CT_BestTime,  CT_LastBadge, CT_Transport , CT_TotPCR,  CT_dtTest, ";
            sql = sql + " CT_dtRelease,  CT_Faculty , CT_ContactTracing, CT_dtContactConfirm,  CT_WD_ID, ";
            sql = sql + " CT_EmpNameCNX,  CT_WhereAbouts , CT_CurrFaculty , CT_dtConfinement,  CT_ICU, ";
            sql = sql + " CT_dtFirstSymptoms,  CT_MoreDetails , CT_Category,  CT_DOH,  CT_Severity,  ";
            sql = sql + " CT_BHERT,  CT_Recommendation , CT_QuarantineStartDate,  CT_QuarantineEndDate,  ";
            sql = sql + " CT_dtExtEnd,  CT_dtFTW,  CT_Email, CT_MSA, CT_Status, CT_dtReturn ) values (";

            //sql = sql + "'" + cd.DateCreated + "' ,";
            sql = sql + "'" + fx.Today() + "' ,";
            sql = sql + "'" + cd.POC + "' ,";
            sql = sql + "'" + cd.ReportSource + "' ,";
            sql = sql + "'" + cd.RepType + "' ,";
            sql = sql + "'" + cd.ForCTTracing + "' ,";
            sql = sql + "'" + cd.CloseContact + "' ,";
            sql = sql + "'" + cd.TestType + "' ,";
            sql = sql + "'" + cd.PCR + "' ,";
            sql = sql + "'" + cd.EmpID + "' ,";
            sql = sql + "'" + cd.FirstName + "' ,";
            sql = sql + "'" + cd.MiddleName + "' ,";
            sql = sql + "'" + cd.LastName + "' ,";
            sql = sql + "'" + cd.EmpProgram + "' ,";
            sql = sql + "'" + cd.FourPillars + "' ,";
            sql = sql + "'" + cd.Location + "' ,";
            sql = sql + "'" + cd.Residence + "' ,";
            sql = sql + "'" + cd.Mobile + "' ,";
            sql = sql + "'" + cd.Landline + "' ,";
            sql = sql + "'" + cd.BestTime + "' ,";
            sql = sql + "'" + cd.LastBadge + "' ,";
            sql = sql + "'" + cd.Transport + "' ,";
            sql = sql + "'" + cd.TotPCR + "' ,";
            sql = sql + "'" + cd.dtTest + "' ,";
            sql = sql + "'" + cd.dtRelease + "' ,";
            sql = sql + "'" + cd.Faculty + "' ,";
            sql = sql + "'" + cd.ContactTracing + "' ,";  // remarks
            sql = sql + "'" + cd.dtContactConfirm + "' ,";
            sql = sql + "'" + cd.WD_ID + "' ,";
            sql = sql + "'" + cd.EmpNameCNX + "' ,";
            sql = sql + "'" + cd.WhereAbouts + "' ,";
            sql = sql + "'" + cd.CurrFaculty + "' ,";
            sql = sql + "'" + cd.dtConfinement + "' ,";
            sql = sql + "'" + cd.ICU + "' ,";
            sql = sql + "'" + cd.dtFirstSymptoms + "' ,";
            sql = sql + "'" + cd.Symptoms + "' ,";
            sql = sql + "'" + cd.Category + "' ,";
            sql = sql + "'" + cd.DOH + "' ,";
            sql = sql + "'" + cd.Severity + "' ,";
            sql = sql + "'" + cd.BHERT + "' ,";
            sql = sql + "'" + cd.Recommendation + "' ,";
            sql = sql + "'" + cd.QuarantineStartDate + "' ,";
            sql = sql + "'" + cd.QuarantineEndDate + "' ,";
            sql = sql + "'" + cd.dtExtEnd + "' ,";
            sql = sql + "'" + cd.dtFTW + "' ,";
            sql = sql + "'" + cd.Email + "' ,";
            sql = sql + "'" + cd.MSA + "' ,";
            sql = sql + "'" + cd.Status + "' ,";
            sql = sql + "'" + cd.dtReturn + "') ";

            UpdateSql(sql);
        }

        static public void UpdateDeclarationFormsCT(ContactDate cd, string indexEmpId)
        {
            Funciones fx = new Funciones();

            string sql = "UPDATE DeclarationFormsCT SET ";

            //sql = sql + "CT_DateCreated='" + cd.DateCreated + "' ,";
            //sql = sql + "CT_DateCreated=IIF((CT_DateCreated is null),'" + fx.Today() + "' ,CT_DateCreated) ,";

            if (cd.POC.Trim() != "") sql = sql + "CT_POC ='" + cd.POC + "' ,";
            if (cd.ReportSource.Trim() != "") sql = sql + "CT_ReportSource ='" + cd.ReportSource + "' ,";
            if (cd.RepType.Trim() != "") sql = sql + "CT_RepType  = '" + cd.RepType + "' ,";
            if (cd.Tracing.Trim() != "") sql = sql + "CT_ForCTracing  = '" + cd.Tracing + "' ,";
            if (cd.CloseContact.Trim() != "") sql = sql + "CT_CloseContact  = '" + cd.CloseContact + "' ,";
            if (cd.TestType.Trim() != "") sql = sql + "CT_TestType  = '" + cd.TestType + "' ,";
            if (cd.PCR.Trim() != "") sql = sql + "CT_PCR  = '" + cd.PCR + "' ,";

            //sql = sql + "CT_EmpID  = '" + cd.EmpID + "' ,";
            //sql = sql + "CT_FirstName  = '" + cd.FirstName + "' ,";
            //sql = sql + "CT_MiddleName  = '" + cd.MiddleName + "' ,";
            //sql = sql + "CT_LastName  = '" + cd.LastName + "' ,";

            // sql = sql + "CT_EmpProgram  = '" + cd.EmpProgram + "' ,";
            if (cd.FourPillars.Trim() != "") sql = sql + "CT_FourPillars  = '" + cd.FourPillars + "' ,";
            //sql = sql + "CT_EmpLocation  = '" + cd.Location + "' ,";
            if (cd.Residence.Trim() != "") sql = sql + "CT_Residence  = '" + cd.Residence + "' ,";
            if (cd.Mobile.Trim() != "") sql = sql + "CT_EmpMobile = '" + cd.Mobile + "' ,";
            if (cd.Landline.Trim() != "") sql = sql + "CT_Landline  = '" + cd.Landline + "' ,";
            if (cd.BestTime.Trim() != "") sql = sql + "CT_BestTime  = '" + cd.BestTime + "' ,";
            if (cd.LastBadge.Trim() != "") sql = sql + "CT_LastBadge  = '" + cd.LastBadge + "' ,";
            if (cd.Transport.Trim() != "") sql = sql + "CT_Transport  = '" + cd.Transport + "' ,";
            if (cd.TotPCR.Trim() != "") sql = sql + "CT_TotPcr  = '" + cd.TotPCR + "' ,";
            if (cd.dtTest.Trim() != "") sql = sql + "CT_dtTest  = '" + cd.dtTest + "' ,";
            if (cd.dtRelease.Trim() != "") sql = sql + "CT_dtRelease  = '" + cd.dtRelease + "' ,";
            if (cd.Faculty.Trim() != "") sql = sql + "CT_Faculty  = '" + cd.Faculty + "' ,";
            if (cd.ContactTracing.Trim() != "") sql = sql + "CT_ContactTracing  = '" + cd.ContactTracing + "' ,";  // remarks

            if (cd.dtContactConfirm.Trim() != "") sql = sql + "CT_dtContactConfirm  = '" + cd.dtContactConfirm + "' ,";
            if (cd.WD_ID.Trim() != "") sql = sql + "CT_WD_ID  = '" + cd.WD_ID + "' ,";
            //sql = sql + "CT_EmpNameCNX  = '" + cd.EmpNameCNX + "' ,";
            if (cd.WhereAbouts.Trim() != "") sql = sql + "CT_WhereAbouts  = '" + cd.WhereAbouts + "' ,";
            if (cd.CurrFaculty.Trim() != "") sql = sql + "CT_CurrFaculty  = '" + cd.CurrFaculty + "' ,";
            if (cd.dtConfinement.Trim() != "") sql = sql + "CT_dtConfinement  = '" + cd.dtConfinement + "' ,";
            if (cd.ICU.Trim() != "") sql = sql + "CT_ICU  = '" + cd.ICU + "' ,";
            if (cd.dtFirstSymptoms.Trim() != "") sql = sql + "CT_dtFirstSymptoms  = '" + cd.dtFirstSymptoms + "' ,";
            if (cd.Symptoms.Trim() != "") sql = sql + "CT_MoreDetails  = '" + cd.Symptoms + "' ,";    //Symptoms/Assessment/Details
            if (cd.Category.Trim() != "") sql = sql + "CT_Category  = '" + cd.Category + "' ,";
            if (cd.DOH.Trim() != "") sql = sql + "CT_DOH  = '" + cd.DOH + "' ,";
            if (cd.Severity.Trim() != "") sql = sql + "CT_Severity  = '" + cd.Severity + "' ,";
            if (cd.BHERT.Trim() != "") sql = sql + "CT_BHERT  = '" + cd.BHERT + "' ,";
            if (cd.Recommendation.Trim() != "") sql = sql + "CT_Recommendation  = '" + cd.Recommendation + "' ,";
            if (cd.QuarantineStartDate.Trim() != "") sql = sql + "CT_QuarantineStartDate  = '" + cd.QuarantineStartDate + "' ,";
            if (cd.QuarantineEndDate.Trim() != "") sql = sql + "CT_QuarantineEndDate  = '" + cd.QuarantineEndDate + "' ,";
            if (cd.dtExtEnd.Trim() != "") sql = sql + "CT_dtExtEnd  = '" + cd.dtExtEnd + "' ,";
            if (cd.dtFTW.Trim() != "") sql = sql + "CT_dtFTW  = '" + cd.dtFTW + "' ,";
            if (cd.dtReturn.Trim() != "") sql = sql + "CT_dtReturn  = '" + cd.dtReturn + "' ,";
            //sql = sql + "CT_MSA='" + cd.MSA + "' ,";
            sql = sql + "CT_DateModified = '" + fx.Today() + "', ";
            sql = sql + "CT_Status  = '" + cd.Status + "', ";
            sql = sql + "CT_UpdatedBy = '" + cd.Email + "' ";

            sql = sql + " WHERE CT_EMPID = '" + cd.EmpID + "' and CT_WD_ID = '" + indexEmpId + "'";

            UpdateSql(sql);
        }

        static public void UpdateDeclarationFormsCTTask4(ContactDate cd, string indexEmpId)
        {
            Funciones fx = new Funciones();
            string sql = "UPDATE DeclarationFormsCT SET ";

            sql = sql + "CT_ContactTracing  = '" + cd.ContactTracing + "' ,";
            sql = sql + "CT_WhereAbouts  = '" + cd.WhereAbouts + "' ,";
            sql = sql + "CT_CurrFaculty  = '" + cd.CurrFaculty + "' ,";
            sql = sql + "CT_dtConfinement  = '" + cd.dtConfinement + "' ,";
            sql = sql + "CT_ICU  = '" + cd.ICU + "' ,";
            sql = sql + "CT_dtFirstSymptoms  = '" + cd.dtFirstSymptoms + "' ,";
            sql = sql + "CT_MoreDetails  = '" + cd.Symptoms + "' ,";        //Symptoms / Assessment / Details
            sql = sql + "CT_Category  = '" + cd.Category + "' ,";
            sql = sql + "CT_DOH  = '" + cd.DOH + "' ,";
            sql = sql + "CT_Severity  = '" + cd.Severity + "' ,";
            sql = sql + "CT_BHERT  = '" + cd.BHERT + "' ,";
            sql = sql + "CT_Recommendation  = '" + cd.Recommendation + "' ,";
            sql = sql + "CT_QuarantineStartDate  = '" + cd.QuarantineStartDate + "' ,";
            sql = sql + "CT_QuarantineEndDate  = '" + cd.QuarantineEndDate + "' ,";
            sql = sql + "CT_dtExtEnd  = '" + cd.dtExtEnd + "', ";
            sql = sql + "CT_dtFTW  = '" + cd.dtFTW + "' ,";
            sql = sql + "CT_dtReturn  = '" + cd.dtReturn + "', ";
            sql = sql + "CT_dtCTA1Start=IIF((CT_dtCTA1Start is null),'" + fx.Today() + "' ,CT_dtCTA1Start) ,";
            sql = sql + "CT_dtCTA1End  = '" + fx.Today() + "', ";
            sql = sql + "CT_DateModified = '" + fx.Today() + "', ";
            sql = sql + "CT_Status  = '" + cd.Status + "', ";
            sql = sql + "CT_UpdatedBy = '" + cd.Email + "' ";

            sql = sql + " WHERE CT_EMPID = '" + cd.EmpID + "' and CT_WD_ID = '" + indexEmpId + "'";

            UpdateSql(sql);
        }


        static public int UpdateSql(string sql)
        {
            log.Info(sql);
            try
            {
                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                SqlCommand comm = new SqlCommand(sql, sqlcon);
                var ss = comm.ExecuteNonQuery();

                comm.Dispose();
                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception ws)
            {
                SendErrorEmail Errors = new SendErrorEmail();
                string msgErr = "Error when Updating Exposure " + sql + " " + ws.ToString();

                Errors.GeneralError(msgErr, "", "");
                return 2;
            }
            return 1;
        }

        static public int UpdateExposure(Duplicate DB_Info, ContactDate ct,
                            byte[] doc1, string ext1,
                            byte[] doc2, string ext2,
                            byte[] doc3, string ext3,
                            bool AlwaysAppend )
        {
            Funciones fx = new Funciones();

            // FOR DAILY AlwaysAppend = true
            // FOR THE REST AlwaysAppend = false

            if (ct.Remarks != "") ct.Remarks = fx.AppendNew(DB_Info.Remarks, ct.Remarks, 3000, AlwaysAppend);
            if (ct.Symptoms != "") ct.Symptoms = fx.AppendNew(DB_Info.Symptom, ct.Symptoms, 3500, AlwaysAppend);

            ct.Status = "Updated";
            DataAccessUpdate.UpdateDeclarationForms(ct);

            if (ext1.Length > 4) ext1 = "";
            if (ext2.Length > 4) ext2 = "";
            if (ext3.Length > 4) ext3 = "";

            if (DB_Info.len1 < 20) DB_Info.len1 = 0;
            if (DB_Info.len2 < 20) DB_Info.len2 = 0;
            if (DB_Info.len3 < 20) DB_Info.len3 = 0;

            // ATTACHMENTS
            if (DB_Info.len1 > 0 &&
                DB_Info.len2 > 0 &&
                DB_Info.len3 > 0)
            {
                ext1 = ext2 = ext3 = "";
            }
            else
            {
                // if duplicated
                if (ext1 != "")
                {
                    if (doc1.Length == DB_Info.len1) ext1 = "";
                    if (doc1.Length == DB_Info.len2) ext1 = "";
                    if (doc1.Length == DB_Info.len3) ext1 = "";
                }

                if (ext2 != "")
                {
                    if (doc2.Length == DB_Info.len1) ext2 = "";
                    if (doc2.Length == DB_Info.len2) ext2 = "";
                    if (doc2.Length == DB_Info.len3) ext2 = "";
                }

                if (ext3 != "")
                {
                    if (doc3.Length == DB_Info.len1) ext3 = "";
                    if (doc3.Length == DB_Info.len2) ext3 = "";
                    if (doc3.Length == DB_Info.len3) ext3 = "";
                }
            }

            if (ext3 != "" && ext2 == "")
            {
                ext2 = ext3;
                doc2 = doc3;
                ext3 = ""; // el tercero no se usara, pasa al segundo
            }

            if (ext3 != "" && ext1 == "")
            {
                ext1 = ext3;
                doc1 = doc3;
                ext3 = "";  // el tercero no se usara, se pasa al priemro
            }

            if (ext2 != "" && ext1 == "")
            {
                ext1 = ext2;
                doc1 = doc2;
                ext2 = ""; // segundo no se usara, se pasa al primero
            }

            //======= // forget doc3 only use  doc1 and doc2
            if (ext1 != "" && DB_Info.len1 > 0)
            {
                ext3 = ext2;
                doc3 = doc2;

                ext2 = ext1;
                doc2 = doc1;

                ext1 = "";
            }

            //  move doc2 to doc3
            if (ext2 != "" && DB_Info.len2 > 0)
            {
                ext3 = ext2;
                doc3 = doc2;

                ext2 = "";
            }


            log.Info("UPDATEING ATTACHMENTS");
            string sql = " UPDATE DeclarationForms  ";

            //sql = sql + " SET EmpMobile = '" + ct.Mobile + "' , ";
            //sql = sql + " Landline      = '" + ct.Landline + "' , ";

            //sql = sql + " WhereAbouts = '" + ct.WhereAbouts + "' , ";
            //sql = sql + " DateContact = '" + ct.dtContactConfirm + "' , ";

            //sql = sql + " MoreDetails= '" + ct.Symptoms + "' , ";
            //sql = sql + " LastBadge  = '" + ct.LastBadge + "' , ";
            //sql = sql + " Remarks    = '" + ct.Remarks + "' , ";

            //sql = sql + " QuarantineStartDate ='" + ct.QuarantineStartDate + "' , ";
            //sql = sql + " QuarantineEndDate   ='" + ct.QuarantineEndDate + "' , ";

            //sql = sql + " ReportSource   = 'OPS', ";
            //sql = sql + " InitialRemarks ='" + ct.Recommendation + "' , ";

            //sql = sql + " RepType  = '" + ct.RepType + "' , ";
            //sql = sql + " BestTime ='" + ct.BestTime + "' , ";
            //sql = sql + " Transport= '" + ct.Transport + "' , ";

            //sql = sql + "dtRelease = '" + ct.dtRelease + "' , ";

            //sql = sql + " POC      =  '" + ct.POC + "' , ";
            //sql = sql + " TestType =  '" + ct.TestType + "' , ";
            //sql = sql + " dtTest   =  '" + ct.dtTest + "' , ";

            //sql = sql + " FourPillars = '" + ct.FourPillars + "' , ";
            //sql = sql + " PCR    =  '" + ct.PCR + "' , ";
            //sql = sql + " TotPCR =  '" + ct.TotPCR + "' , ";

            //sql = sql + " Faculty     =  '" + ct.Faculty + "' , ";
            //sql = sql + " CurrFaculty =  '" + ct.CurrFaculty + "' , ";

            //sql = sql + " WD_ID           =  '" + ct.WD_ID + "' , ";
            //sql = sql + " dtFirstSymptoms =  '" + ct.dtFirstSymptoms + "' , ";

            //sql = sql + " Category =  '" + ct.Category + "' , ";
            //sql = sql + " dtExtEnd =  '" + ct.dtExtEnd + "' , ";
            //sql = sql + " dtFTW    =  '" + ct.dtFTW + "' , ";
            //sql = sql + " dtReturn      = '" + ct.dtReturn + "' , ";
            //sql = sql + " dtConfinement = '" + ct.dtConfinement + "' , ";
            //sql = sql + " dtReported    =  '" + ct.DateCreated + "' , ";
            //sql = sql + " ICU           =  '" + ct.ICU + "' , ";

            sql = sql + " SET DateModified = '" + fx.Today() + "' ";
            //sql = sql + " UpdatedBy = '" + ct.sender + "', ";

            //sql = sql + " dtContactConfirm = '" + ct.dtContactConfirm + "' , ";
            //sql = sql + " EmpNameCNX       =  '" + ct.EmpNameCNX + "' , ";
            //sql = sql + " EmpAddress       = '" + ct.Address + "' , ";
            //sql = sql + " Residence        = '" + ct.Residence + "' ";

            if (ext1 != "")
            {
                sql = sql + ", File1 = @doc1 ";
                sql = sql + ", ext1 = @ext1 ";
            }

            if (ext2 != "")
            {
                sql = sql + ", File2 = @doc2 ";
                sql = sql + ", ext2 = @ext2 ";
            }
            if (ext3 != "")
            {
                sql = sql + ", File3 = @doc3 ";
                sql = sql + ", ext3 = @ext3 ";
            }

            if (!sql.Contains("File")) return 1;

            sql = sql + " WHERE EmpID= '" + ct.EmpID + "' ";
            sql = sql + " AND STATUS <> 'Notified FTW' ";
            SqlConnection sqlcon = new SqlConnection(SqlConn);

            try
            {

                sqlcon.Open();

                using (SqlCommand comm = new SqlCommand(sql, sqlcon))
                {

                    if (ext1 != "")
                    {
                        comm.Parameters.Add("@doc1", SqlDbType.Image, doc1.Length).Value = doc1;
                        comm.Parameters.AddWithValue("@ext1", ext1);
                    }

                    if (ext2 != "")
                    {
                        comm.Parameters.Add("@doc2", SqlDbType.Image, doc1.Length).Value = doc2;
                        comm.Parameters.AddWithValue("@ext2", ext2);
                    }

                    if (ext3 != "")
                    {
                        comm.Parameters.Add("@doc3", SqlDbType.Image, doc1.Length).Value = doc3;
                        comm.Parameters.AddWithValue("@ext3", ext3);
                    }

                    if (ext1 != "" || ext2 != "" || ext3 != "")
                        comm.ExecuteNonQuery();

                    log.Info("UPDATED");

                    if (ct.RepType == "ON SELF-QUARANTINE")
                    {
                        sql = "INSERT INTO DeclarationFormsDetails ";
                        sql = sql + " (EmpID, Remarks, Category) values ";

                        int pos1 = ct.Remarks.LastIndexOf(" : ");
                        if (pos1 > 0) ct.Remarks = ct.Remarks.Substring(pos1 + 3).Trim();

                        sql = sql + "('" + ct.EmpID + "'";
                        sql = sql + "'" + ct.Remarks + "', ";
                        sql = sql + "'" + ct.Category + "') ";

                        comm.CommandText = sql;
                        comm.ExecuteNonQuery();
                    }
                }

                sqlcon.Close();
                sqlcon.Dispose();
                return 1;
            }
            catch (Exception ws)
            {
                sqlcon.Close();
                sqlcon.Dispose();

                string msgErr = "Sender: " + ct.sender + " ERROR UPDATE " + ct.EmpID + " " + sql + " " + ws.ToString();
                log.Info(msgErr);
                Exposure.SendEmails sent = new Exposure.SendEmails();
                sent.SendEmail("Error Exposure Update", msgErr, "", "");
                return 0;
            }
            return 1;
        }

        static public string UpdatePDF(string[] Files, string EmpID, string FieldDB, string FileName)
        {
            Funciones fx = new Funciones();

            if (FileName == "")
            {
                FileName = fx.GetEIDFile(Files, EmpID);
                if (FileName == "") return "";
            }

            byte[] doc1 = fx.GetFile(FileName);

            string sql = "Update DeclarationForms ";
            sql = sql + " SET " + FieldDB + " = @doc1 ";
            sql = sql + " where EmpID = '" + EmpID + "' ";
            sql = sql + " AND STATUS <> 'Notified FTW' ";
            SqlConnection sqlcon = new SqlConnection(SqlConn);

            log.Info("Loaded into " + FieldDB + " " + EmpID);
            log.Info(sql);

            try
            {
                sqlcon.Open();

                using (SqlCommand comm = new SqlCommand(sql, sqlcon))
                {

                    comm.Parameters.Add("@doc1", SqlDbType.Image, doc1.Length).Value = doc1;
                    //comm.Parameters.AddWithValue("@ext1", extension);

                    comm.CommandText = sql;
                    comm.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                string msgErr = "Task 1, Error when updateing " + FileName;
                log.Info(msgErr);
                Exposure.SendEmails sent = new Exposure.SendEmails();
                sent.SendEmail("Error Exposure Update", msgErr, "", "");

            }
            finally
            {

                sqlcon.Close();
                sqlcon.Dispose();
            }
            return FileName;
        }


        static public bool UpdateInsert(string sqlInsert, string sqlSelect, string sql3, string sql4)
        {
            bool inserted = false;
            string SqlEmpl = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(SqlEmpl);
            sqlcon.Open();

            SqlCommand cmd = new SqlCommand(sqlSelect, sqlcon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // UPDATE
                if (sql3 != "")
                {
                    cmd.CommandText = sqlInsert;
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                //INSERT
                inserted = true;
                cmd.CommandText = sqlInsert;
                cmd.ExecuteNonQuery();
            }

            if (sql4 != "")
            {
                cmd.CommandText = sql4;
                cmd.ExecuteNonQuery();
            }

            sqlcon.Close();
            sqlcon.Dispose();
            return inserted;
        }

        static public void LoadDocument(string EmpID, string FileName, string setting, string sender)
        {
            Funciones fx = new Funciones();
            byte[] Doc = null;
            if (FileName !=null && FileName != "" && File.Exists(FileName))
                Doc = fx.GetFile(FileName);
            else
            {
                FileName = "";
                setting = setting.Replace("@DOC", "null");
            }

            string sql = "UPDATE DeclarationForms SET ";
            sql = sql + setting; // " SET FTW = @FTW , Status='Notified FTW' ";

            sql = sql + ", DateModified = '" + fx.Today() + "'";
            sql = sql + ", UpdatedBy = '" + sender + "'";

            sql = sql + " WHERE EMPID = '" + EmpID + "'";
            sql = sql + " AND STATUS <> 'Notified FTW' ";

            log.Info(sql);

            try
            {
                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                using (SqlCommand comm = new SqlCommand(sql, sqlcon))
                {
                    if (sql.Contains("@DOC"))
                    {
                        if (FileName != "")
                            comm.Parameters.Add("@DOC", SqlDbType.Image, Doc.Length).Value = Doc;
                    }
                    comm.ExecuteNonQuery();
                }
                sqlcon.Close();
                sqlcon.Dispose();


            }
            catch (Exception es)
            {
                string msgErr = " ERROR UPDATE " + EmpID + " " + sql + " " + es.ToString();
                log.Info(msgErr);
                Exposure.SendEmails sentErr = new Exposure.SendEmails();
                sentErr.SendEmail("Error Exposure Update", msgErr, "", "");
            }
        }

        static public void NewNotification(string EmpID, string EmpName, string Location, string CaseEmail)
        {
            log.Info("TASK 1 NewNotification");

            string sql = "INSERT INTO DeclarationFormsEmails ";
            sql = sql + " ( EmpID, EmpName, Location, CaseEmail, ";
            sql = sql + " SupEmail, NextLevelEmail, ThirdLevelEmail, HREmail) values ( ";

            sql = sql + "'" + EmpID + "', ";
            sql = sql + "'" + EmpName + "', ";
            sql = sql + "'" + Location + "', ";
            sql = sql + "'" + CaseEmail + "', ' ',' ',' ' ,' ')  ";

            UpdateSql(sql);
        }

        static public DataTable GetDataCT(string sql)
        {
            string strCon = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(strCon);
            sqlcon.Open();

            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtData = new DataTable();
            adapter.Fill(dtData);

            sqlcon.Dispose();
            sqlcon.Close();

            return dtData;
        }


        static public string GetData(string sql)
        {
            string strCon = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(strCon);
            sqlcon.Open();

            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            var strData = cmd.ExecuteScalar();

            sqlcon.Dispose();
            sqlcon.Close();

            return Convert.ToString(strData);
        }
    }
}
