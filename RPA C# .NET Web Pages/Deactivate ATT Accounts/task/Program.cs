using HtmlAgilityPack;
using log4net;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
namespace Task
{
    class ItemRequest
    {
        public string ResetType { get; set; }
        public string ToolID { get; set; }
        public string Name { get; set; }
        public string CVGReferenceNumber { get; set; }
        public string EmployeeId { get; set; }
        public string Ticket { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TMEmailAddress { get; set; }
        public string TMFirstName { get; set; }
    }


    class Program
    {

        static ExchangeService service;

        static string ExchangeServiceUrl = string.Empty;
        static string EmailsError;
        static string[] File = new string[] { "", "", "" };  // 3 downloads
        static string[] ext = new string[] { "", "", "" };
        enum valRet { Success, NoTemplate, NoAttach, MissingInfo, WrongSubject, AutoReply, WrongFormat, DBProblem };

        //static string path = ".\\temp\\";
        //static string previous = "";

        static int procesados = 0;

        static public int Macy1 = 0;
        static public int Macy2 = 0;

        static public int Macy3 = 0;
        static public int Macy4 = 0;

        static public int Att1 = 0;
        static public int Att2 = 0;
        static public int Transfers = 0;
        static public int QC = 0;


        // FOLDERS
        static FolderId SurveysFolderId = null;
        static string FOLDER = "";

        static FolderId ATTDeactivationFolderId = null;
        static FolderId MacysSurveyFolderId = null;
        static FolderId SprintBKOFolderId = null;

        static FolderId ExceptionFolderId = null;
        static FolderId IgnoredFolderId = null;
        static FolderId ProcessedFolderId = null;

        // Subjects of Emails
        static string ATT_Ref = string.Empty;
        static string ATT_TRANSFER = string.Empty;
        static string MTYPE = string.Empty;
        static string SprintSubject = string.Empty;

        //static string EmplVerif_Ref = string.Empty;
        // static string Macy_BCOM = string.Empty;
        // static string Macy_MCOM = string.Empty;

        static BlockingCollection<string> queue = new BlockingCollection<string>();
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Finish()
        {
            WriteDashboard();
            System.Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            //Sharepoint sp = new Sharepoint();
            //sp.Update();

            //DataAccess.GetBPID("101365440");


            EmailsError = "tomas.dale@concentrix.com";
            // EmailsError = EmailsError + "faith.guerra@concentrix.com;";
            // EmailsError = EmailsError + "christopher.elera@concentrix.com";

            //string ATTUID = DataAccessUDW.GetATTUID("101434940");
            //string emails = "tomas.dale@concentrix.com;";
            //emails = emails + "faith.guerra@concentrix.com;";
            //emails = emails + "christopher.elera@concentrix.com";
            //SendEmails.SendEmail("VERIZON test email", "Processed AGENT_EFFICIENCY A TEST", emails);

            FOLDER = ConfigurationManager.AppSettings["FOLDER"].ToString();
            if (FOLDER == "INBOX")
            {
                int hora = DateTime.Now.Hour;
                //if (hora > 20) Finish();
                //if (hora < 1) Finish();
            }

            //DataTable dt = DataAccessUDW.GetStarrIDs();

            //DataSet DS = new DataSet();
            //DS.Tables.Add(dt);
            //DS.WriteXml(@"C:\temp\\Students2.xml");


            //DataAccess.NotLoaded("11", "name1", "lastname1", "termina",
            //        "project", "location", "LOB", "Reason");


            log4net.Config.XmlConfigurator.Configure();
            //DataAccess.GetATTUID("101365440");
            //string path = "C:\\temp\\ATT_Transfers.xlsx";
            //log.Info("File loading " + path);
            //ReadExcel rr = new ReadExcel();
            //DataTable dt = rr.ReadExcelFile(path);
            //log.Info(dt.Rows.Count.ToString());
            // DataAccess.LoadIntoDataBase("al254y", "101416614", "Allison", "Lee", "02/07/2020", "02/07/2020", "C496", "USA", "ATT", "T", "me");

            DataAccess.UpdateNotLoaded();

            log.Info("Settings");
            GetSettings();
            StartExchangeService();

            SetFolders();

            // while (true)
            // {
            log.Info("GET PENDING EMAILS");
            GetPendingEmails();

            try
            {
                int i = 1;
                int conteo = queue.Count();
                if (conteo == 0)
                {
                    if ((Macy1 + Macy2 + Macy3 + Macy4 + Att1 + Att2 + Transfers) > 0)
                        WriteDashboard();

                    log.Info("---End---");
                    Environment.Exit(0);
                }


                foreach (var item in queue.GetConsumingEnumerable())
                {
                    log.Info(i.ToString());
                    i++;
                    conteo--;
                    procesados++;
                    ProcessEmail(item);

                    log.Info("Processed --- " + procesados.ToString());

                    if (conteo == 0) break;
                }
            }
            catch (Exception ex)
            {
                DataAccess.UpdateMonitor("getEmailsFromQueue", "1");
                string msgErr = "GetEmailsFromQueue \\n\\n" + ex.ToString();
                ReportError(msgErr);
            }

            log.Info("Procesados " + procesados.ToString());
            if (procesados > 50)
            {
                WriteDashboard();
                procesados = 0;
            }
            while (queue.TryTake(out _)) { }
            System.Threading.Thread.Sleep(10000);
            log.Info("== DONE ==");
            //}         
        }

        private static void SetFolders()
        {
            try
            {
                FolderView folderview = new FolderView(10);
                //log.Info("Set Folders");

                var results = service.FindFolders(
                    WellKnownFolderName.Inbox,
                    new SearchFilter.SearchFilterCollection(
                        LogicalOperator.Or,

                        new SearchFilter.IsEqualTo(FolderSchema.DisplayName, "ATTDeactivation"),

                        new SearchFilter.IsEqualTo(FolderSchema.DisplayName, "SURVEYS"),
                        new SearchFilter.IsEqualTo(FolderSchema.DisplayName, "MacysSurvey"),
                        new SearchFilter.IsEqualTo(FolderSchema.DisplayName, "SprintBKO"),

                        new SearchFilter.IsEqualTo(FolderSchema.DisplayName, "Exception"),
                        new SearchFilter.IsEqualTo(FolderSchema.DisplayName, "Ignored"),
                        new SearchFilter.IsEqualTo(FolderSchema.DisplayName, "Processed")),
                    folderview);

                foreach (var folder in results)
                {
                    if (folder is Folder)
                    {
                        log.Info(folder.DisplayName);
                        switch (folder.DisplayName)
                        {
                            case "SURVEYS":
                                SurveysFolderId = folder.Id;
                                break;

                            case "ATTDeactivation":
                                log.Info("ATT DEACTIVATION FOLDER SET");
                                ATTDeactivationFolderId = folder.Id;
                                break;

                            case "MacysSurvey":
                                MacysSurveyFolderId = folder.Id;
                                break;

                            case "SprintBKO":
                                SprintBKOFolderId = folder.Id;
                                break;

                            case "Exception":
                                ExceptionFolderId = folder.Id;
                                break;

                            case "Ignored":
                                IgnoredFolderId = folder.Id;
                                break;

                            case "Processed":
                                ProcessedFolderId = folder.Id;
                                break;

                            default:
                                break;
                        }
                    }
                }

                return;

                //if (SurveysFolderId == null)
                //    SurveysFolderId = CreateFolder("SURVEYS", WellKnownFolderName.Inbox);

                //Microsoft.Exchange.WebServices.Data.Rule newRule = new Microsoft.Exchange.WebServices.Data.Rule();


                //newRule.DisplayName = "MoveSurveys";
                //newRule.Priority = 1;
                //newRule.IsEnabled = true;
                //newRule.Conditions.ContainsSubjectStrings.Add("RECEIVED A CUSTOMER SURVEY");
                //newRule.Actions.MoveToFolder = SurveysFolderId;

                if (ATTDeactivationFolderId == null)
                    ATTDeactivationFolderId = CreateFolder("ATTDeactivation", WellKnownFolderName.Inbox);

                if (MacysSurveyFolderId == null)
                    MacysSurveyFolderId = CreateFolder("MacysSurvey", WellKnownFolderName.Inbox);

                if (SprintBKOFolderId == null)
                    SprintBKOFolderId = CreateFolder("SprintBKO", WellKnownFolderName.Inbox);


                if (ExceptionFolderId == null)
                    ExceptionFolderId = CreateFolder("Exception", WellKnownFolderName.Inbox);

                if (IgnoredFolderId == null)
                    IgnoredFolderId = CreateFolder("Ignored", WellKnownFolderName.Inbox);

                if (ProcessedFolderId == null)
                    ProcessedFolderId = CreateFolder("Processed", WellKnownFolderName.Inbox);
            }
            catch (Exception ex)
            {
                log.Info("ERROR EN SET FOLDER");
                string msgErr = " Error when Creating folders \\n\\n" + ex.ToString();
                //ReportError(msgErr);
                System.Environment.Exit(0);
            }
        }

        private static FolderId CreateFolder(string DisplayName, WellKnownFolderName parentFolderName)
        {
            FolderId folderId = null;

            try
            {
                Folder folder = new Folder(service);
                folder.DisplayName = DisplayName;
                folder.Save(parentFolderName);

                folderId = folder.Id;
            }
            catch (Exception ex)
            {
                string msgErr = "Error when Creating Folder\\n\\n " + ex.ToString();
                ReportError(msgErr);
            }

            return folderId;
        }

        private static void GetPendingEmails()
        {
            try
            {
                int offset = 0;
                int pageSize = 50;
                bool more = false;

                ItemView view = new ItemView(pageSize, offset, OffsetBasePoint.Beginning);

                // start with the oldest one in the inbox
#if DEBUG
                    view.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
#else
                view.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Ascending);
#endif
                view.PropertySet = PropertySet.IdOnly;

                FindItemsResults<Item> results;

                do
                {
                    if (FOLDER == "INBOX")
                        results = service.FindItems(WellKnownFolderName.Inbox, view);
                    else
                        results = service.FindItems(SurveysFolderId, view);

                    log.Info("Emails " + results.Count().ToString());

                    foreach (var item in results.Items)
                    {
                        queue.Add(item.Id.UniqueId);
                    }

                    more = results.MoreAvailable;
                    if (more)
                    {
                        view.Offset += pageSize;
                    }
                } while (more);
            }
            catch (Exception ex)
            {
                if (FOLDER != "INBOX") return;
                int hour = System.DateTime.Now.Hour;
                int minute = System.DateTime.Now.Minute;

                if (hour > 19 || hour < 8) return;
                if (minute == 30 || minute < 11 || minute > 49) return;

                string msgErr = "GetPendingEmails\\n\\n " + ex.ToString();
                SendEmails.SendEmail("Error NotificationRobot", "Error when reading emails, pasword MAY EXPIRED  " + ex.ToString(), "");

#if !DEBUG
                System.Threading.Thread.Sleep(5 * 100 * 60);
#endif
                System.Environment.Exit(0);
            }
        }

        static private void StartExchangeService()
        {
            try
            {
                service = new ExchangeService(ExchangeVersion.Exchange2010_SP2);

                Uri uri = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
                IWebProxy proxy = WebRequest.GetSystemWebProxy();
                bool isBypassed = proxy.IsBypassed(uri);

                if (!isBypassed)
                {
                    //if proxy is needed, then provide network credentials
                    proxy.Credentials = CredentialCache.DefaultNetworkCredentials;

                    service.WebProxy = proxy;
                }


                //https://msdn.microsoft.com/en-us/library/office/dn567668(v=exchg.150).aspx

                string account = ConfigurationManager.AppSettings["Account"].ToString();
                string passwd = ConfigurationManager.AppSettings["Password"].ToString();

                service.Credentials = new WebCredentials(account, passwd, "concentrix.com"); //enter your credentials to test

                //service.Credentials = new WebCredentials("rpa@cnxmail.onmicrosoft.com", "$hun+F3dT0w3r");


                service.Url = uri;
            }
            catch (Exception ex)
            {
                string msgErr = "StartExchangeService \\n\\n " + ex.ToString();
                ReportError(msgErr);
                log.Error(msgErr);
            }
        }

        private static void GetSettings()
        {
            try
            {
                ExchangeServiceUrl = ConfigurationManager.AppSettings["ExchangeServiceUrl"];
                FOLDER = ConfigurationManager.AppSettings["FOLDER"].ToString();

                // EMAIL SUBJECTS CONTAINS
                ATT_Ref = ConfigurationManager.AppSettings["AttTermination"];
                ATT_TRANSFER = ConfigurationManager.AppSettings["AttTransfer"];
                MTYPE = ConfigurationManager.AppSettings["MacysSubject"];
                SprintSubject = ConfigurationManager.AppSettings["SprintSubject"];

                //EmplVerif_Ref = ConfigurationManager.AppSettings["EmplVerif"];
                //Macy_BCOM = ConfigurationManager.AppSettings["MacysBCOMSubject"];
                //Macy_MCOM = ConfigurationManager.AppSettings["MacysMCOMSubject"];

            }
            catch (Exception ex)
            {
                string msgErr = "Get Settings \\n\\n" + ex.ToString();
                ReportError(msgErr);
            }
        }

        private static void ProcessEmail(string itemId)
        {
            log.Info("");
            log.Info("processEmail");
            Item item = null;
            StringBuilder sb = new StringBuilder();

            string sender = "";
            string dtSended = "";
            string subject = "";
            string msg1 = "";

            try
            {
                item = Item.Bind(service, itemId);

                if (item is EmailMessage)
                {
                    EmailMessage email = item as EmailMessage;
                    if (string.IsNullOrEmpty(email.Subject)) return;
                    if (string.IsNullOrEmpty(email.Body)) return;

                    try
                    {
                        subject = email.Subject.ToUpper();
                        sender = email.Sender.Address;
                        dtSended = email.DateTimeSent.ToString().Trim();
                        msg1 = email.Body.Text.ToUpper();
                    }
                    catch (Exception es)
                    {
                        string msg = FOLDER + " Check Task/Program/ line 485 and logs - After reading email variable " + es.ToString();
                        //SendEmails.SendEmail("ROBOT ERROR 487 Task - Program - ProcessEmail()", msg, EmailsError);

                        log.Info("ERROR 487");

                        log.Info(subject);
                        log.Info(sender);
                        log.Info(dtSended);
                        log.Info(msg1);
                    }

                    log.Info(" ");
                    log.Info("SUbject " + subject);
                    if (subject.Contains("Notification Robot Wrong subjec"))
                    {
                        // try
                        // {
                        item.Move(IgnoredFolderId);
                        // }
                        //  catch(Exception)
                        //                            {

                        // item.Move()
                        //                          }
                        return;
                    }

                    if (sender == "noreply-spam@concentrix.com"
                     || sender == "employee.solutions@concentrix.com"
                     || sender == "internal.communications@concentrix.com"
                     || sender == "rpa@concentrix.com"
                     || sender == "postmaster@email.teams.microsoft.com"
                      || sender == "MAILER-DAEMON@concentrix.com")
                    {
                        item.Move(IgnoredFolderId);
                        return;
                    }

                    if (subject.IndexOf("Email Confirmation") > 0)
                    {
                        item.Move(IgnoredFolderId);
                        return;
                    }

                    subject = subject.Replace("_", " ");

                    bool TRACE = false;
                    if (subject.IndexOf(ATT_Ref) > 3) TRACE = true;


                    if (TRACE) log.Info(sender + " " + subject);

                    string AttachFile = "";
                    try
                    {
                        for (int i = 0; i <= email.Attachments.Count; i++)
                        {
                            try
                            {
                                string Attach1 = email.Attachments[i].Name.ToString();
                                if (!Attach1.Contains(".jpg"))
                                {
                                    AttachFile = Attach1;
                                    i = 100;
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                    catch (Exception)
                    {
                        AttachFile = "";
                    }

                    log.Info(subject + "    " + AttachFile);
                    // AttachFile = AttachFile.Replace("_", "");

                    // MACYS SURVEY
                    //=============
                    //if (subject.Contains(Macy_BCOM) || subject.Contains(Macy_MCOM))

                    if (FOLDER != "INBOX")
                    {
                        if (subject.Contains(MTYPE))
                        {
                            try
                            {
                                DataAccess.MacysInsert(email.InternetMessageId, sender, subject, msg1, email.DateTimeReceived.ToUniversalTime());
                                item.Move(MacysSurveyFolderId);
                                Macy3++;
                                DataAccess.UpdateMonitor(MTYPE, "1");
                            }
                            catch (Exception es)
                            {
                                log.Info("ERROR MACY -" + MTYPE + " " + es.ToString());
                                item.Move(ExceptionFolderId);
                                Macy4++;
                            }

                            return;
                        }
                        return;
                    }

                    string Attached = AttachFile.ToUpper();

                    // VERIZON
                    if (AttachFile.ToUpper().Contains("AGENT EFFICIENCY ELEMENTS")
                     || AttachFile.ToUpper().Contains("AGENT_EFFICIENCY_ELEMENTS"))
                    {
                        log.Info(" ");
                        log.Info("VERIZON Agent Efficacy Elements File1");
                        try
                        {
                            VerizonValidation vv = new VerizonValidation();
                            vv.File1(email);
                            try
                            {
                                SetFolders();
                                item.Move(ProcessedFolderId);
                                //                      SendEmails.SendEmail("VERIZON FILE1 AGENT VALIDATION PROCESSED", "AGENT VALIDATION PROCESSED "+ ProcessedFolderId.ToString(), EmailsError);
                            }
                            catch (Exception )
                            {
                                Verizon(1, ProcessedFolderId.ToString());
                            }
                            return;
                        }
                        catch (Exception es)
                        {
                            int hora = email.DateTimeCreated.Hour;
                            int hora2 = DateTime.Now.Hour;

                            SendEmails.SendEmail("VERIZON NOT PROCESSED", "NOT PROCESSED AGENT_EFFICIENCY " + hora.ToString() + " " + hora2.ToString() + " " + es.ToString(), EmailsError);

                            if (hora2 < hora) hora2 = hora2 + 24;

                            if (hora2 > (hora + 2))
                            {
                                try
                                {
                                    item.Move(IgnoredFolderId);
                                }
                                catch (Exception)
                                {

                                    item.Move(IgnoredFolderId);
                                }
                            }
                            return;
                        }
                    }

                    if (AttachFile.ToUpper().Contains("XM_CALL_DETAIL"))
                    {
                        log.Info("VERIZON XM_CALL_DETAIL File2 " + AttachFile);
                        VerizonValidation vv = new VerizonValidation();
                        try
                        {
                            vv.File2(email);
                            try
                            {
                                SetFolders();
                                item.Move(ProcessedFolderId);
                                //                    SendEmails.SendEmail("VERIZON FILE2 XM_CALL_DETAIL (FILE2) PROCESSED", "XM_CALL_DETAIL PROCESSED " + ProcessedFolderId.ToString(), EmailsError);
                            }
                            catch (Exception )
                            {
                                Verizon(2, ProcessedFolderId.ToString());
                            }
                            return;
                        }
                        catch (Exception es)
                        {
                            int hora = email.DateTimeCreated.Hour;
                            int hora2 = DateTime.Now.Hour;

                            SendEmails.SendEmail("VERIZON FILE 2 NOT PROCESSED", "NOT PROCESSED XM_CALL_DETAIL " + hora.ToString() + " " + hora2.ToString() + " " + es.ToString(), EmailsError);

                            if (hora2 < hora) hora2 = hora2 + 24;

                            if (hora2 > (hora + 2))
                                item.Move(IgnoredFolderId);
                            return;
                        }
                    }

                    // ATT TERMINATION WITH EMAIL
                    //===========================
                    if (subject.IndexOf(ATT_Ref) > 3) // && dtSended == "convergys@myworkday.com")  
                    {
                        log.Info("");
                        log.Info("ATT TEMINATION");
                        string body = item.Body;
                        // int pos1 = body.IndexOf("<body");
                        // body = body.Substring(pos1);

                        string msgError = ProcessTerminations.MainProcessTermination(item.Body, dtSended, sender, subject, "ATT");
                        if (msgError != "")
                        {
                            ReportError(msgError);
                            Att2++;
                        }
                        else
                        {
                            DataAccess.UpdateMonitor("", "2");
                            Att1++;
                        }

                        log.Info("MOVE TO FOLDER PROCESSED");
                        item.Move(ATTDeactivationFolderId);
                        return;


                        // ATT TRANSFERS WITH EXCEL ATTACHED
                        //==================================
                        if (subject.IndexOf(ATT_TRANSFER) > 3 || subject.IndexOf("TRANSFERS REPORT") > 3)  // && dtSended == "convergys@myworkday.com")  
                        {
                            Transfers++;
                            ProcessTransfers.WithAttachment(email, sender);

                            item.Move(ATTDeactivationFolderId);
                            return;
                        }
                    }

                    // ATT TRANSFERS WITH EXCEL ATTACHED
                    //==================================
                    if (subject.IndexOf(ATT_TRANSFER) > -1 || subject.IndexOf("TRANSFERS REPORT") > 3)  // && dtSended == "convergys@myworkday.com")  
                    {
                        log.Info("");
                        log.Info("TRANSFERS FILE");
                        //Status.Transfers++;
                        ProcessTransfers.WithAttachment(email, sender);
                        item.Move(ATTDeactivationFolderId);
                        return;
                    }

                    // VERIZON

                    if (subject.Contains(SprintSubject))
                    {
                        log.Info("Sprint ");
                        try
                        {
                            string SprintDownloadedFolder = ConfigurationManager.AppSettings["SprintDownloadedFolder"];
                            string SprintCompletedFolder = ConfigurationManager.AppSettings["SprintCompletedFolder"];
                            string SprintForPostingFolder = ConfigurationManager.AppSettings["SprintForPostingFolder"];
                            string SprintErrorFolder = ConfigurationManager.AppSettings["SprintErrorFolder"];
                            string SprintDestFolder = ConfigurationManager.AppSettings["SprintDestFolder"];
                            string SprintCopyToShare = ConfigurationManager.AppSettings["SprintCopyToShare"];
                            string SprintSharedFolder = ConfigurationManager.AppSettings["SprintSharedFolder"];

                            if (!Directory.Exists(SprintDestFolder))
                            {
                                log.Info("not found folder " + SprintDestFolder);
                                SendEmails.SendEmail("Not found " + SprintDestFolder, "Notification Robot Not found " + SprintDestFolder, "");
                                item.Move(ExceptionFolderId);
                                return;
                            }

                            if (!Directory.Exists(SprintSharedFolder))
                            {
                                string msg = "Not found folder " + SprintSharedFolder;
                                log.Info(msg);
                                SendEmails.SendEmail(msg, msg, "");
                                item.Move(ExceptionFolderId);
                                return;
                            }

                            SprintProcess p = new SprintProcess();
                            p.DownloadAttachment(email, SprintDownloadedFolder);
                            p.ProcessAttachment(SprintDownloadedFolder, SprintCompletedFolder, SprintForPostingFolder, SprintErrorFolder);
                            p.PostAttachment(SprintForPostingFolder, SprintDestFolder, SprintCopyToShare, SprintSharedFolder);
                            p.MoveAttachment(SprintCompletedFolder, SprintDestFolder);

                            item.Move(SprintBKOFolderId);
                        }
                        catch (Exception es)
                        {
                            log.Info(es.ToString());
                            item.Move(ExceptionFolderId);
                            SendEmails.SendEmail("Notification Robot Error", es.ToString(), "");
                            //Macy2 = Macy2 + 1;
                        }
                        return;
                    }

                    log.Info("WRONG SUBJECT - " + item.Subject + " - " + AttachFile);
                    if (item.Subject.IndexOf("Aux State Report") < 1)
                    {
                        SendEmails.SendEmail("Notification Robot Wrong subject or wrong attach file name", "Do not respond this email<br><br>This subject is invalid to be processed " + item.Subject + "<br><br>" + AttachFile + "<br><br>Sent by " + sender, sender);
                    }
                    try
                    {
                        SetFolders();
                        item.Move(IgnoredFolderId);
                    }
                    catch (Exception es3)
                    {
                        log.Info("=================");
                        log.Info(es3.ToString());
                        item.Move(ProcessedFolderId);
                    }
                }
            }
            catch (Exception es2)
            {
                if (string.IsNullOrEmpty(subject)) return;
                if (string.IsNullOrEmpty(msg1)) return;

                if (es2.ToString().IndexOf("destinationFolderId at ") > 1)
                {
                    Finish();
                    System.Environment.Exit(0);
                }
                SleepMinutes();

                log.Info("Error 800");
                log.Info(subject);
                log.Info(sender);
                log.Info(dtSended);
                log.Info(msg1);

                SendEmails.SendEmail(FOLDER + " ROBOT ERROR 800 Task - Program - ProcessEmail() ", es2.ToString(), EmailsError);
            }
        }

        private static void SleepMinutes()
        {
#if !DEBUG
            System.Threading.Thread.Sleep(5 * 60 * 1000);
#endif
        }

        private static void Verizon(int cual, string folder)
        {
            string subject = "Robot ProcessFolder not found " + folder;
            string msg = "VERIZON FILE " + cual.ToString() + " WAS PROCESSED\nBUT IT IS NOT ABLE TO BE MOVED TO FOLDER PROCESSED ";
            SendEmails.SendEmail(subject, msg, EmailsError);
            SleepMinutes();

        }

        private static void ReportError(string msgError)
        {
            DateTime dVal = DateTime.Now;
            int iDAY = (int)dVal.DayOfWeek;
            int iHour = dVal.Hour;

            //if (iDAY == 7) return;
            //if (iDAY == 6 && iHour > 14) return;
            if (iHour > 22 || iHour < 3) return;

            msgError = msgError + " Check logs according to this time in D:\\Services\\NotificationRobotSvc\\Logs  Also check last and next request for deactivation if it was done " + msgError;
            // SendEmails.SendEmail("ERROR IN NOTIFCIATION ROBOT, CHECK LOGS IN SERVER ORLIWV022", msgError);
            log.Error(msgError);
        }

        private static List<ItemRequest> ReadEmail(string emailBody)
        {
            List<ItemRequest> list = new List<ItemRequest>();
            try
            {
                HtmlDocument document = new HtmlDocument();
                string xpath = "//table";

                document.LoadHtml(emailBody);
                var tables = document.DocumentNode.SelectNodes(xpath);

                if (tables != null)
                {
                    var table = tables.FirstOrDefault(); //only get the first table
                    var rows = table.Descendants("tr");
                    int items = 0;

                    foreach (var row in rows)
                    {
                        var cells = row.Descendants("td").ToArray();
                        var item = new ItemRequest();

                        if (items > 0) //start on 2nd row, 1st one is header
                        {
                            item.ResetType = cells[0].InnerText.Trim(); //now mapped to "Tool" column in current DSC email format
                            item.ToolID = cells[1].InnerText.Trim();    //now mapped to "Login ID format/example" column in current DSC email format
                            item.Name = cells[2].InnerText.Trim() + " " + cells[3].InnerText.Trim();
                            item.FirstName = cells[2].InnerText.Trim();
                            item.LastName = cells[3].InnerText.Trim();
                            item.CVGReferenceNumber = cells[6].InnerText.Trim();
                            item.Password = cells[7].InnerText.Trim();

                            //ticket starts with IM
                            int position = item.CVGReferenceNumber.ToLower().IndexOf("im");

                            if (position > 0)
                            {
                                item.EmployeeId = item.CVGReferenceNumber.Substring(0, position);
                                item.Ticket = item.CVGReferenceNumber.Substring(position);
                            }
                            list.Add(item);
                        }
                        items++;
                    }
                }
            }
            catch (Exception)
            {
                //string msgError = ex.ToString();
                //ReportError(msgError);                
            }

            return list;
        }

        static protected void WriteDashboard()
        {
            string user = System.Environment.UserName;
            string wk = System.Environment.MachineName;
            string LAN = System.Environment.UserDomainName + "\\" + user;
            string nulos = null;

            ServiceReference2.DADashboardLogSoapClient DashBoard = new ServiceReference2.DADashboardLogSoapClient();

            DashBoard.STAT2("ATT Deactivation Email Robot", "ATT", nulos, nulos, nulos, wk, LAN, "", 0, 0, 0, 0, "", "", "", "", "", "", "", "", "", "", Att1, Att2, Transfers, 0, 0, 0, 0, 0, 0, 0, "", user);

            DashBoard.STAT2("Macy's CES Email Robot", "Macy's", nulos, nulos, nulos, wk, LAN, "", 0, 0, 0, 0, "", "", "", "", "", "", "", "", "", "", Macy3, Macy4, 00, 0, 0, 0, 0, 0, 0, 0, "", user);
            log.Info("Written in Dashboard");

            Macy1 = 0;
            Macy2 = 0;
            Macy3 = 0;
            Macy4 = 0;

            Att1 = 0;
            Att2 = 0;
            Transfers = 0;

            QC = 0;
            procesados = 0;
        }

    }
}
