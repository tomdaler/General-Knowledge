using log4net;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

// UTC_DM
// UTC_FTW

namespace ConsoleApp1
{
    class NotContacted
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        string FILENAME = ConfigurationManager.AppSettings["TempFolder"].ToString() + "\\" + "Excel.xlsx";
        public void UTC(string type1, string sender, string senderName, string TheFile, string subject)
        {
            string ErrSubject = subject + " - Error Encountered";
            
            Funciones fx = new Funciones();
            Exposure.DataBase DB = new Exposure.DataBase();
            Exposure.SendEmails sent = new Exposure.SendEmails();
            SendErrorEmail sent2 = new SendErrorEmail();

            try
            {
                DataTable dtNotif = DB.dtNotif();
                DataTable dtExposure = DB.GetData(0,"");

                //string sql = "SELECT EmpID, EmpName, EmpLocation, ";
                //sql = sql + " QuarantineStartDate, QuarantineEndDate,";
                //sql = sql + " dtExtEnd, SQAdditionalDays, EmpProgram, EmpMobile, Landline, BestTime   ";

                log.Info(type1 + " Exposure " + dtExposure.Rows.Count);

                string setting = " STATUS ='Notified " + type1+ "' ";
                int Template = 3;
                if (type1 == "UTC-DM") Template = 7;

                //====================================
                List<string> Missing = new List<string>();

                Download_Read down = new Download_Read();
                DataTable dtRead = down.ReadExcel("",0,"");
                int conteo = 0;
                if (dtRead != null) conteo = dtRead.Rows.Count;
                if (conteo == 0)
                {
                    SendErrorEmail SE = new SendErrorEmail();
                    SE.FileWithoutData(senderName, sender, FILENAME);
                    return;
                }

                log.Info("Loaded " + conteo.ToString());

                Variables vari = new Variables();
                DataTable HR = vari.getHR();

                foreach (DataRow dr in dtRead.Rows)
                {
                    conteo--;
                    string EmpID = dr[0].ToString();
                    if (EmpID == "") continue;
                    EmpID = Regex.Replace(EmpID, @"[^\d-+()]", "");
                    if (EmpID == "") continue;

                    string Mobile = dr[1].ToString();
                    Mobile = Regex.Replace(Mobile, @"[^\d-+()]", "");

                    string Landline = dr[2].ToString();
                    Landline = Regex.Replace(Landline, @"[^\d-+()]", "");

                    string TimeCall = dr[3].ToString();
                    TimeCall = TimeCall.Replace("'", "''");

                    string reason = dr[4].ToString();
                    reason = reason.Replace("'", "''");

                    if (Mobile.Trim() == "") Mobile = " ";
                    if (Landline.Trim() == "") Landline = " ";
                    if (TimeCall.Trim() == "") TimeCall = " ";
                    if (reason.Trim() == "") reason = " ";

                    string where1 = "EmpID = '" + EmpID + "'";
                    DataRow[] result = dtExposure.Select(where1);
                    if (result.Length == 0)
                    {
                        Missing.Add(EmpID);
                        continue;
                    }
                    
                    //    DataRow drEmp = fx.IfEmplInDB(ref wrongEmplID, EmpID, sender,
                    //type1, Mobile, Landline, TimeCall, dtExposure);

                    //    if (drEmp == null)
                    //    {
                    //        fx.AddWrong(ref wrongEmplID, EmpID);
                    //        continue;
                    //    }

                    string EmpName = result[0][1].ToString();
                    string strLoc = result[0][2].ToString();
                    //if (strLoc == "")
                    //{
                    //    fx.AddWrong(ref wrongEmplID, EmpID);
                    //    continue;
                    //}

                    //fx.IfCreated(ref created, drEmp);

                    log.Info("Conteo " + conteo.ToString() + " " + EmpID + " " + EmpName + " " + Mobile + " " + reason + " " + TheFile);
                    string dtFec = "";

                    try
                    {
                        // extend end date
                        dtFec = result[0][5].ToString();
                        if (dtFec == "") dtFec = result[0][4].ToString();
                    }
                    catch (Exception)
                    {
                        dtFec = result[0][4].ToString(); 
                    }

                    if (dtFec == "") dtFec = " ";
                    DataAccessUpdate.LoadDocument(EmpID, "", setting, sender);

                    if (TimeCall.Length > 20)
                    {
                        int posi2 = TimeCall.IndexOf(" ");
                        if (posi2 > 0) TimeCall = TimeCall.Substring(posi2);
                        TimeCall = TimeCall.Trim();
                    }

                    fx.SetLimit(ref TimeCall, 20);
                    fx.SetLimit(ref EmpID, 20);
                    fx.SetLimit(ref Mobile, 20);
                    fx.SetLimit(ref Landline, 20);
                    fx.SetLimit(ref reason, 200);

                    string theFile2 = FILENAME.Replace(".xlsx", "");
                    int posi = theFile2.LastIndexOf("\\");
                    theFile2 = theFile2.Substring(posi + 1);
                    fx.SetLimit(ref theFile2, 30);

                    string UTC_File = " DeclarationFormsUTC ";

                    // INSERT
                    string sqlInsert = "INSERT INTO " + UTC_File;
                    sqlInsert = sqlInsert + " ( EmpID,  EmpName, Reason,     UTCType,";
                    sqlInsert = sqlInsert + "   Sender, Mobile,  SourceFile, Landline, TimeOfCall) values ( ";
                    sqlInsert = sqlInsert + "'" + EmpID + "', ";
                    sqlInsert = sqlInsert + "'" + EmpName + "', ";
                    sqlInsert = sqlInsert + "'" + reason + "', ";
                    sqlInsert = sqlInsert + "'" + type1 + "', ";

                    sqlInsert = sqlInsert + "'" + sender + "', ";
                    sqlInsert = sqlInsert + "'" + Mobile + "', ";

                    sqlInsert = sqlInsert + "'" + theFile2 + "', ";
                    sqlInsert = sqlInsert + "'" + Landline + "', ";
                    sqlInsert = sqlInsert + "'" + TimeCall + "') ";

                    // CHECK IF IT IS ALREADY IN DATABASE
                    string sqlSelect = "SELECT EmpID FROM " + UTC_File;
                    sqlSelect = sqlSelect + " WHERE EmpID='" + EmpID + "' ";
                    sqlSelect = sqlSelect + " AND cast(DateReceived as Date) = cast((dateadd(hour,(8),getutcdate())) as Date) ";

                    // UPDATE REASON IF IT IS ALREADY IN DATABASE
                    string sql3 = " UPDATE" + UTC_File;
                    sql3 = sql3 + " set reason = '" + reason + "', ";
                    sql3 = sql3 + " Mobile ='" + Mobile + "', ";
                    sql3 = sql3 + " Landline= '" + Landline + "', ";
                    sql3 = sql3 + " Sender= '" + sender + "', ";
                    sql3 = sql3 + " TimeOfCall='" + TimeCall + "', ";
                    sql3 = sql3 + " SourceFile='" + FILENAME + "' ";

                    sql3 = sql3 + " WHERE EmpID='" + EmpID + "', ";
                    sql3 = sql3 + " AND CAST(DateReceived as Date) = CAST((dateadd(hour,(8),getutcdate())) as Date) ";

                    string sql4 = "";

                    //try
                    //{
                    //    if (Mobile != "" && result[0][8].ToString() != "")
                    //    {
                    //        sql4 = "UPDATE DeclarationForms ";
                    //        sql4 = sql4 + "  SET EmpMobile = '" + Mobile + "' ";
                            
                    //        if (Landline != "")
                    //            sql4 = sql4 + ", Landline= '" + Landline + "', ";

                    //        sql4 = sql4 + " DateModified = '" + fx.Today() + "',  ";
                    //        sql4 = sql4 + " UpdatedBy = '" + sender + "'  ";

                    //        sql4 = sql4 + " WHERE EmpID='" + EmpID + "' ";
                    //        sql4 = sql4 + " AND STATUS <> 'Notified FTW' ";
                    //    }
                    //}
                    //catch (Exception) { }

                    bool inserted = DataAccessUpdate.UpdateInsert(sqlInsert, sqlSelect, sql3, sql4);

                    // SENT NOTIFICATION FOR EACH CASE
                    try
                    {
                        string[] arrSent = sent.sendingAllEmails(Template, EmpID, EmpName, strLoc,
                               dtFec, Mobile, Landline, reason, TimeCall, "", HR);
                
                        // FOR SUMMARY
                        dtNotif.Rows.Add(DateTime.Now.ToString(),
                            EmpID, EmpName, strLoc, type1,
                            arrSent[0], arrSent[1], arrSent[2], arrSent[3]);
                    }
                    catch(Exception es)
                    {
                        log.Info(es.ToString());
                    }
                }

                string subject2 = "Summary of Notifications for " + subject;

                sent.SentSummary(dtNotif, "UTC", subject2, "Not_Doctors");

                string SenderNoClinic = fx.RemoveClinics(sender);

                SendErrorEmail sentErr = new SendErrorEmail();
                sentErr.MissingEmplID(Missing, sender, senderName);

                //if (wrongEmplID != "")
                //    sent2.SendErrorProcess(ErrSubject, wrongEmplID, "", SenderNoClinic);

                //if (created != "")
                //    sent2.SendCreated(SenderNoClinic, created, subject);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                sent2.GeneralError("NOT UTC notification </br>" + ex.ToString(), "", "");
            }
        }
          
        public string Download(EmailMessage email)
        {
            string subject = email.Subject.ToString();
            if (subject.Length < 8) return "";
            FileAttachment file1 = null;
            string theFile = "";
            Funciones fx2 = new Funciones();

            string[] File1 = fx2.getAttachments(email, 100, email.Sender.Address.ToString());
            for (int i = 0; i < File1.Length; i++)
            {
                if (File1[i].ToUpper().IndexOf(".XLSX") > 1)
                {
                    file1 = email.Attachments[i] as FileAttachment;
                    theFile = File1[i];

                    string compare = File1[i];
                    compare = compare.Replace(".XLSX", "");
                    compare = compare.Replace(".xlsx", "");
                    int pos1 = compare.LastIndexOf("\\");
                    if (pos1 > 1) compare = compare.Substring(pos1 + 1);
                    compare = compare.Replace(" ", "");
                    subject = subject.Replace(" ", "");
                    if (compare.Contains("_UTC_") && !subject.Contains(compare)) return "";

                    break;
                }
            }

            if (file1 == null) return "";
            log.Info(" COPY from "+ theFile + " TO " + FILENAME);
            log.Info("---------");
            File.Copy(theFile, FILENAME, true);
            return theFile;
        }
    }
}
