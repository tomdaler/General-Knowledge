using log4net;
using Objects;
using System.Data;
using System.Reflection;
namespace ConsoleApp1
{
    class InsertTemplate
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool InsertUpdate(string FROM,
        ContactDate ct, string sender, string senderName, 
        string EmailBody, string[] File, DataTable dtExposure)
        {
            Funciones fx = new Funciones();
            ct.Email = ct.sender;
            ct.EmpID   = fx.RemoveGarbage(ct.EmpID);
            ct.EmpName = fx.RemoveGarbage(ct.EmpName);

            string missing = "";
            bool IncorrectEmpID = false;
            bool IncorrectName = false;
            bool IncorrectMobile = false;

            // HTML AND EXCEL TEMPLATES
            missing = fx.MissingInfo(ct.RepType, "Report Type", missing);
            missing = fx.MissingInfo(ct.EmpID, "Employee ID", missing);
            missing = fx.MissingInfo(ct.EmpName, "Employee Name", missing);
            missing = fx.MissingInfo(ct.Mobile, "Mobile", missing);

            // FOR HTML TEMPLATE
            if (FROM == "TEMPLATE")
            {
                missing = fx.MissingInfo(ct.BestTime, "Best Time to Call", missing);
                missing = fx.MissingInfo(ct.dtContactConfirm, "Date of Contact", missing);
                missing = fx.MissingInfo(ct.WhereAbouts, "Latest Whereabouts", missing);
                missing = fx.MissingInfo(ct.Symptoms, "Symptoms/Assessment/Details", missing);
                missing = fx.MissingInfo(ct.LastBadge, "Last Seen in the Office/Last Badge Entry", missing);
                missing = fx.MissingInfo(ct.Transport, "Type of Transport", missing);
                missing = fx.MissingInfo(ct.Remarks, "Remarks", missing);
            }

            ct.Mobile = ct.Mobile.Trim();
            if (ct.Mobile != "" && ct.Mobile.Length < 10) IncorrectMobile = true;

            if (ct.EmpID.Length > 10 || ct.EmpID.Length < 5) IncorrectEmpID = true;
            ContactDate ct2 = null;

            if (ct.EmpID != "" && !IncorrectEmpID)
            {
                Exposure.SendEmails fx2 = new Exposure.SendEmails();

                ct2 = fx.GetDataInfo(dtExposure, ct.EmpID, true, false);
                if (ct2 == null)
                {
                    IncorrectEmpID = true; // not found
                }
                else
                {
                    if (ct.EmpName != "")
                    {
                        string name1 = ct.EmpName.ToUpper();
                        string Last2 = ct2.LastName.ToUpper();
                        if (name1.IndexOf(Last2) < 0) IncorrectName = true;
                    }

                    ct.FirstName = ct2.FirstName;
                    ct.MiddleName = ct2.MiddleName;
                    ct.LastName = ct2.LastName;
                    ct.EmpName = ct2.EmpName;
                    ct.Location = ct2.Location;
                    ct.MSA = ct2.MSA;
                    ct.Status = ct2.Status;

                    if (ct.Address == "") ct.Address = ct2.WK_Address1 + " " + ct2.WK_Address2;
                   
                }
            }

            if (ct.EmpID == "") IncorrectEmpID = false;

            if (missing != "" || IncorrectMobile
                || IncorrectEmpID || IncorrectName)
            {
                SendErrorEmail Errors = new SendErrorEmail();
                Errors.MissingData(missing,
                    IncorrectMobile, IncorrectEmpID, IncorrectName,
                   ct.Mobile, ct.EmpID, ct.EmpName, ct.Location, sender, senderName);
                return false;
            }
            //=========================
            
            byte[] doc1 = null;
            byte[] doc2 = null;
            byte[] doc3 = null;

            string ext1 = "";
            string ext2 = "";
            string ext3 = "";
            int pos = 1;

            for (int i = 0; i < File.Length; i++)
            {
                string ff = File[i];
                if (ff == "") continue;

                if (File[i] != null 
                 && File[i] != ""
                 && !File[i].Contains(".xl"))
                {
                    int pos1 = File[i].LastIndexOf(".");
                    string extension = File[i].Substring(pos1 + 1);

                    if (pos == 1)
                    {
                        doc1 = fx.GetFile(File[i]);
                        ext1 = extension;
                    }
                    if (pos == 2)
                    {
                        doc2 = fx.GetFile(File[i]);
                        ext2 = extension;
                    }
                    if (pos == 3)
                    {
                        doc3 = fx.GetFile(File[i]);
                        ext3 = extension;
                        pos = 100;
                    }
                    pos++;
                }
            }

            if (ext1.Length > 4) ext1 = ext1.Substring(0, 4);
            if (ext2.Length > 4) ext2 = ext2.Substring(0, 4);
            if (ext3.Length > 4) ext3 = ext3.Substring(0, 4);
            
            //=================
           
            ct.sender = sender;
            string PrevRepType = "";
            if (ct2.Status =="New")
            {
                if (ct.RepType.Contains("CONFIRM") && 
                    (FROM == "TEMPLATE" || FROM=="FILE"))
                    ct.A1SQNewEndDate = fx.TodayDate();
                
                DataAccess da = new DataAccess();
                string retorno = da.ExposureDB(ct, senderName,
                 doc1, ext1, doc2, ext2, doc3, ext3, "OPS");

                if (retorno == "") return false;
            }
            else
            {
                if (ct.Email == "") ct.Email = ct.sender;
                Duplicate IfInDB = fx.IsDuplicated(ct.EmpID);

                int retorno = DataAccessUpdate.UpdateExposure(IfInDB, ct, 
                                doc1, ext1, doc2, ext2, doc3, ext3, false);

                if (retorno == 0) return false;
               
                PrevRepType = ct2.RepType.ToUpper();
                if (ct.RepType.Contains("CONFIRM") && !PrevRepType.Contains("CONFIRM"))
                {
                    DataAccessUpdate.UpdateA1SQNewEndDate(ct.EmpID);
                }
            }

            // if NEW OR NOW IS COVID CONFIRMED, SEND CONFIRMATION EMAIL
            string senderList = fx.GetConfirmEmails(PrevRepType, ct.RepType, ct.Location);
            if (senderList != "")
            {
                senderList = sender + ";" + senderList;
                Exposure.SendEmails sentConf = new Exposure.SendEmails();
                log.Info("Confirmation " + senderList);
                sentConf.EmailConfirmed(EmailBody, File, senderList);
            }

            // ACK1
            fx.SendAcknowledge(ct.Location, "COVID19 Case Report Acknowledgment",
                sender,senderName, ct.EmpName, true,"",true);

            return true;
        }
    }
}
