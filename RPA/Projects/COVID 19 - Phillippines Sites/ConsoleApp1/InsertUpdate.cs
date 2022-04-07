using log4net;
using Objects;
using System.Reflection;
namespace ConsoleApp1
{
    class InsertUpdate
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool UpdateInsertFromExcel(ContactDate ct, string sender, string senderName,
                   byte[] doc1, string ext1,
                   byte[] doc2, string ext2,
                   byte[] doc3, string ext3, string[] File)
        {
            Funciones fx = new Funciones();

            Objects.Duplicate duplicate = fx.IsDuplicated(ct.EmpID);

            if (duplicate.Remarks == "NEW")
            {
                ct.Remarks = fx.AppendTime(ct.Remarks);
                ct.Symptoms = fx.AppendTime(ct.Symptoms);

                DataAccess da = new DataAccess();
                ct.Location = da.ExposureDB(ct, senderName,
                          doc1, ext1, doc2, ext2,  doc3, ext3, "HR");
            }
            else
            {
                // UPDATE
                //========
                ct.Location = duplicate.Location;
                ct.Status = "Update";

                int retorno = DataAccessUpdate.UpdateExposure(duplicate ,ct,
                           doc1, ext1, doc2, ext2, doc3, ext3, false);

                             
                if (retorno == 0) return false;
                log.Info("Duplicated ");
            }

            string senderList = fx.GetConfirmEmails(duplicate.RepType, ct.RepType, ct.Location);
            if (senderList != "")
            { 
                string body = "</br>";
                body = body + "Employee ID   : <b>" + ct.EmpID + "</b></br>";
                body = body + "Employee Name : <b>" + ct.EmpName + "</b></br>";
                body = body + "Site          : <b>" + ct.Location + "</b></br>";
                body = body + "Mobile        : <b>" + ct.Mobile + "</b></br></br>";
                body = body + "Remarks       : <b>" + ct.Remarks + "</b></br></br>";
                body = body + "Symptoms      : <b>" + ct.Symptoms + "</b></br>";

                body = body + "</br>Please see attached files for more details.</br></br>";
                body = body + "Thanks</br>CNX HR Auto Response";

                senderList = sender + ";" + senderList;
                Exposure.SendEmails send2 = new Exposure.SendEmails();
                send2.EmailConfirmed(body, File, senderList);
            }
            return true;
        }
    }
}
