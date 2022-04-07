using log4net;
using Microsoft.Exchange.WebServices.Data;
using System.Data;
using System.Reflection;

namespace ConsoleApp1
{
    class FTW
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool GetFiles(EmailMessage email, string subject1)
        {
            string sender = email.Sender.Address.ToString();
            string ErrSubject = subject1 + " - Error Encountered";
            Exposure.SendEmails fx = new Exposure.SendEmails();
            Funciones fx2 = new Funciones();
            SendErrorEmail check = new SendErrorEmail();

            Exposure.DataBase notif = new Exposure.DataBase();
            DataTable dtExposure = notif.GetData(0,"");

            Exposure.SendEmails sent = new Exposure.SendEmails();
            Variables vari = new Variables();
            DataTable HR = vari.getHR();

            string wrongEmplID = "";

            string[] File = fx2.getAttachments(email, 100,sender);
            for (int i = 0; i < File.Length; i++)
            {
                if (File[i].ToUpper().IndexOf(".PDF") > 1)
                {
                    string EmpID = fx.GetEmpID(File[i]);
                    if (EmpID == "") continue;

                    // check if employee in DeclarationForms 
                    //if (check.NotFoundInTable(EmpID))
                    //{
                    //    string archivo = File[i];
                    //    int posi = archivo.LastIndexOf("\\");
                    //    archivo = archivo.Substring(posi + 1);
                    //    posi = archivo.IndexOf("_");
                    //    archivo = archivo.Substring(0,posi);


                    //    if (wrongEmplID == "")
                    //        wrongEmplID = archivo;
                    //    else
                    //    {

                    //        wrongEmplID = wrongEmplID + ", " + archivo;

                    //    }

                    //      continue;
                    //}
                    string where1 = "EMPID = '" + EmpID + "'";
                    DataRow[] result = dtExposure.Select(where1);

                    if (result.Length == 0)
                    {
                        string archivo = File[i];
                        File[i] = "";
                        int posi = archivo.LastIndexOf("\\");
                        archivo = archivo.Substring(posi + 1);
                        posi = archivo.IndexOf("_");
                        archivo = archivo.Substring(0, posi);
                        
                        if (wrongEmplID == "")
                            wrongEmplID = archivo;
                        else
                        {
                            wrongEmplID = wrongEmplID + ", " + archivo;
                        }

                        continue;
                    }
                                                         
                    //string setting = " FTW = @DOC, Status = 'Notified FTW', DateModified=GetDate() ";
                    //DataAccessUpdate.UpdateDocument(EmpID, File[i], setting);

                    // SENT NOTIFICATION FOR EACH CASE
                    //string[] arrSent = sent.sendingAllEmails(Template, EmpID, EmpName, strLoc,
                    //   "","","", "","", "", HR);
                }
            }
            
            //string subject = "Summary of Notifications for " + email.Subject;
            //sent.SendNotifications("FTW", File, email.Sender.Address, dt1, subject);

            if (wrongEmplID != "")
                check.SendErrorProcess(ErrSubject, wrongEmplID, "", sender);

            return true;
        }
    }
}
