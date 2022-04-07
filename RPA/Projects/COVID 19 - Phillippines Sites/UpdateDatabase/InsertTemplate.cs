using log4net;
using System.Data;
using System.Reflection;
using System;
using System.Data.SqlClient;
using System.Configuration;

namespace UpdateDatabase
{
    class InsertTemplate
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool InsertUpdate(ContactDate ct, string sender, string senderName,
                                 DataTable dtExposure, DataTable dtWK)
        {
            Functions fx = new Functions();
            
            if (ct.Email.Trim()=="") ct.Email = sender;
            //if (ct.dtModified == "") ct.dtModified = fx.Today();

            ct.EmpID = fx.RemoveGarbage(ct.EmpID);
            ct.EmpName = fx.RemoveGarbage(ct.EmpName);

            string missing = "";
            bool IncorrectEmpID = false;
            bool IncorrectName = false;
            bool IncorrectMobile = false;

            //missing = fx.MissingInfo(ct.RepType, "Report Type", missing);
            //missing = fx.MissingInfo(ct.EmpID, "Employee ID", missing);
            //missing = fx.MissingInfo(ct.EmpName, "Employee Name", missing);
            //missing = fx.MissingInfo(ct.Mobile, "Mobile", missing);

            //missing = fx.MissingInfo(ct.BestTime, "Best Time to Call", missing);
            //missing = fx.MissingInfo(ct.dtContactConfirm, "Date of Contact", missing);
            //missing = fx.MissingInfo(ct.WhereAbouts, "Latest Whereabouts", missing);
            //missing = fx.MissingInfo(ct.Symptoms, "Symptoms/Assessment/Details", missing);
            //missing = fx.MissingInfo(ct.LastBadge, "Last Seen in the Office/Last Badge Entry", missing);
            //missing = fx.MissingInfo(ct.Transport, "Type of Transport", missing);
            //missing = fx.MissingInfo(ct.Remarks, "Remarks", missing);

            //ct.Mobile = ct.Mobile.Trim();
            //if (ct.Mobile != "" && ct.Mobile.Length < 10) IncorrectMobile = true;

            //if (ct.EmpID.Length > 10 || ct.EmpID.Length < 5) IncorrectEmpID = true;
            ContactDate ct2 = null;

           // ct.EmpID = "101804536";

            if (ct.EmpID != "") // && !IncorrectEmpID)
            {
                ct2 = fx.GetDataInfo(dtExposure, ct.EmpID, true, false, dtWK);
                if (ct2 == null && ct.Recommendation == "DECEASED")
                {
                    goto DEAD;
                }

                if (ct2 !=null && ct2.Status == "Update")
                {
                    // the EmpID exists, then email error
                    // email   
                    return false;
                }

                if (ct2 == null)
                {
                    IncorrectEmpID = true; // not found
                    return false;
                }
                else
                {
                    //if (ct.EmpName != "")
                    //{
                    //    string name1 = ct.EmpName.ToUpper();
                    //    string Last2 = ct2.LastName.ToUpper();
                    //    if (name1.IndexOf(Last2) < 0) IncorrectName = true;
                    //}

                    if (ct2.LastName != ct.LastName) return false;

                    ct.FirstName = ct2.FirstName;
                    ct.MiddleName = ct2.MiddleName;
                    ct.LastName = ct2.LastName;
                    ct.EmpName = ct2.EmpName;
                    ct.Location = ct2.Location;
                    ct.MSA = ct2.MSA;
                    //ct.Status = ct2.Status;

                    if (ct.Address == "") ct.Address = ct2.WK_Address1 + " " + ct2.WK_Address2;
                }
            }

            DEAD:

            if (ct.EmpID == "") IncorrectEmpID = false;

            if (missing != "" || IncorrectMobile
                || IncorrectEmpID || IncorrectName)
            {
                //MissingData(missing, IncorrectMobile, IncorrectEmpID, IncorrectName,
                //            ct.Mobile, ct.EmpID, ct.EmpName, ct.Location,
                //            sender, senderName);
                return false;
            }

            ct.sender = sender;
            //if (ct.RepType.Contains("CONFIRM")) ct.A1SQNewEndDate = fx.TodayDate();

            DataAccess da = new DataAccess();
            string retorno = da.NewExposure(ct, senderName);

            if (retorno == "") return false;
            return true;
        }


        public void MissingData(string missing, bool IncorrectMobile, bool IncorrectEmpID, bool IncorrectName,
               string Mobile, string EmpID, string EmpName, string location, string sender, string senderName)
        {
            string empID2 = EmpID;
            if (missing != "")
            {
                if (missing.IndexOf(",") > 0)
                    missing = "<b>Missing Fields - " + missing + ".<b></br>";
                else
                    missing = "<b>Missing Field - " + missing + ".</b></br>";
            }

            if (IncorrectMobile)
            {
                if (missing != "")
                    missing = missing + "</br>Also, the Mobile Number <b>" + Mobile + "</b> is invalid.";
                else
                    missing = "<b>Mobile Number " + Mobile + " is Invalid</b></br>";
            }

            if (IncorrectEmpID)
            {
                empID2 = "";
                string strError = "Incorrect Employee ID.";
                strError = strError + "</br>Please make sure to send the employee’s<b> Workday ID.</b></br>";

                if (missing != "")
                    missing = missing + "</br>Also, " + strError + "</b></br>";
                else
                    missing = "<b>" + strError + "</b></br>";
            }

            if (IncorrectName)
            {
                string strError = "Incorrect Employee Name in Database.";
                strError = strError + "</br>Please make sure to send the employee’s<b> Workday ID.</b></br>";

                if (missing != "")
                    missing = missing + "</br>Also, " + strError + "</b></br>";
                else
                    missing = "<b>" + strError + "</b></br>";
            }

            // for missing employee id
            if (EmpID == "") missing = missing + "</br>Please make sure to send the employee’s<b> Workday ID.</b></br>";

            missing = missing + "</br>";

            if (location == "")
                location = GetLocation(empID2, sender);

            SendError(missing, EmpID, EmpName, sender, senderName, "", missing, location);
        }

        public string GetLocation(string EmpID, string sender)
        {
            string strLoc = "";
            if (EmpID == null) return "";

            try
            {
                string SqlEmpl = ConfigurationManager.ConnectionStrings["SqlEmpl"].ConnectionString;
                SqlConnection sqlcon = new SqlConnection(SqlEmpl);
                sqlcon.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlcon;
                cmd.CommandType = System.Data.CommandType.Text;

                string sql = "select Location from Employee(nolock) ";
                if (EmpID != "")
                    sql = sql + " where HR_ID = '" + EmpID + "'";
                else
                    sql = sql + " where CNX_LAN_ID = '" + sender + "'";
                sql = sql + " AND UPPER(Country) = 'PHILIPPINES' ";
                cmd.CommandText = sql;

                strLoc = cmd.ExecuteScalar().ToString();
                sqlcon.Close();
                sqlcon.Dispose();
            }
            catch (Exception) { }
            return strLoc;
        }

        public void SendError(string missing, string emplID, string names, string sender, string senderName, string lastName, string logInfo, string strLocation)
        {
            string subject = "COVID19 Case Report " + emplID + " – Error Encountered";
            string body = "Hi " + senderName + ",<br><br>This is an automated response. Please do not reply to this email.<br><br>";
            body = body + "We encountered an error in your report: ";

            // NOT FOUND IN EMPLOYEE DATABASE
            if (missing == "")
            {
                missing = "<b>Incorrect Employee ID.</b><br>Please make sure that you have sent the employee’s <b>Workday ID.</b><br><br>";
                logInfo = "Incorrect Employee ID";
            }

            body = body + missing;
            logInfo = logInfo.Replace("<b>", "");
            logInfo = logInfo.Replace("</b>", "");
            logInfo = logInfo.Replace("<br>", " ");

            if (logInfo.Length > 500) logInfo = logInfo.Substring(0, 500);

            Functions fx = new Functions();
            fx.InsertIntoLog(logInfo, emplID, names, senderName);

            if (emplID != "") body = body + " Employee ID: <b>" + emplID + "<br></b>";
            if (names != "") body = body + " Name: <b>" + names + "</b><br>";
            if (lastName != "") body = body + " Name in Database: <b>" + lastName + "</b><br>";

            body = body + "<br>Please review the employee information and resend it as a new email for processing. Otherwise, report will not be endorsed to Active One for evaluation/assessment.</b>";
            //body = body + firm();

            log.Info(strLocation + " " + sender);
            //sender = fx.GetHREmails(strLocation, sender, true);

            SendEmail sent = new SendEmail();
            log.Info("send error to " + sender);
            sent.SendEmails(subject, body, sender);
        }
    }
}
