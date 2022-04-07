using log4net;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

 // if (type1 == "EXTENSION") TemplateNo = 2;
 // if (type1 == "FTW") TemplateNo = 4;
 // if (type1 == "SWAB") TemplateNo = 5;


namespace ConsoleApp1
{
    class NOTIFICATIONS
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

       
        public bool GetFiles(EmailMessage email, string type1, string subject1)
        {
            Funciones fx = new Funciones();

            string sender = email.Sender.Address;
            string SenderNoClinic = fx.RemoveClinics(sender);

            string ErrSubject = subject1 + " - Error Encountered";
      
            Exposure.SendEmails fx2 = new Exposure.SendEmails();
            SendErrorEmail check = new SendErrorEmail();
            List<string> Missing = new List<string>();

            string wrongFiles = "";
            //string wrongEmplID = "";
            //string created = "";

            int TemplateNo = 0;
            if (type1 == "EXTENSION") TemplateNo = 2;
            if (type1 == "FTW") TemplateNo = 4;

            if (type1 == "SWAB_BATCH") TemplateNo = 15;
            //if (type1 == "InitialSwab") TemplateNo = 15;
            if (type1 == "SWAB") TemplateNo = 5;   // 2nd Swab

            string LoadInto = "";
            if (type1 == "EXTENSION") LoadInto = "EXTENSION";
            if (type1 == "FTW") LoadInto = "FTW";
                
            if (type1 == "SWAB_BATCH") LoadInto = "InitialSwab";  
            //if (type1 == "InitialSwab") LoadInto = "InitialSwab"; 
            if (type1 == "SWAB") LoadInto = "SWAB"; 
            
            //=========================================
            string FileExt = "";
            if (type1 == "EXTENSION") FileExt = "_MCE.PDF";
            if (type1 == "FTW") FileExt = "_FTW.PDF";
           
            if (type1 == "SWAB_BATCH") FileExt = "_MCIS.PDF";
            //if (type1 == "InitialSwab") FileExt = "_MCIS.PDF";
            if (type1 == "SWAB") FileExt = "_MCS.PDF";

            //=============================================
            string SumType = "";
            if (type1 == "EXTENSION") SumType = "SQ Extension";
            if (type1 == "FTW") SumType = "FTW";
            if (type1 == "SWAB_BATCH") SumType = "1st Swab Test";
            //if (type1 == "InitialSwab") SumType = "1st Swab Test";
            if (type1 == "SWAB") SumType = "2nd Swab";

            //================================================
            string distro = "";

            if (type1 == "EXTENSION") distro = "Not_Doctors"; // Not_Doctors
            if (type1 == "FTW") distro = "Not_Doctors";

            if (type1 == "SWAB_BATCH") distro = "SQ_Update";
            //if (type1 == "InitialSwab") distro = "SWAB_First";
            if (type1 == "SWAB") distro = "Not_Doctors";

            //=================================================
            Variables vari = new Variables();
            DataTable HR = vari.getHR();
            
            Exposure.DataBase db = new Exposure.DataBase();
            DataTable dtNotif = db.dtNotif();
            DataTable dtInitial = db.dtInitial();
           
            DataTable dtWorkDay = null;
            if (type1 == "SWAB_BATCH") dtWorkDay = Data.getWK();

            //====================================

            Exposure.SendEmails correos = new Exposure.SendEmails();

            string[] File = fx.getAttachments(email, 100, sender);
            DataTable dtExtension = new DataTable();
 
            if (type1 == "EXTENSION")
            {
                for (int i = 0; i < File.Length; i++)
                {
                    if (File[i].ToUpper().IndexOf(".XLSX") > 1)
                    {
                        dtExtension = Parse_Extended(File[i], File, email.Sender.Address.ToString());
                        break;
                    }
                    continue;
                }

                if (dtExtension == null || dtExtension.Rows.Count == 0)
                {
                    string msg = "Hi " + email.Sender.Name.ToString() + "<br></br>";
                    msg = msg + "The Email was not processed because it does not have an excel file attachted.<br></br>";
                    msg = msg + EmailEnd();
                    fx2.SendEmail("Extension Batch Error - " + email.Subject.ToString(), msg, email.Sender.Address.ToString(),null);
                    return false;
                }
            }

            string lista = "";
            string Comma = "";
            for (int i = 0; i < File.Length; i++)
            {
                string archivo = File[i];
                if (archivo == "" && i > 5) break;
                if (archivo.ToUpper().IndexOf(".PDF") < 1) continue;

                string archivo2 = archivo;
                archivo = archivo.Replace(" ", "");
                if (!archivo.ToUpper().Contains(FileExt))
                {
                    int pos1 = archivo.LastIndexOf("\\");
                    continue;
                }

                string EmpID = fx2.GetEmpID(File[i]);

                if (EmpID != "") lista = lista + Comma + "'"+ EmpID+"'";
                Comma = ",";
            }

            if (lista == "") return false;
            lista = " AND empid in (" + lista + ")";
            DataTable dtExposure = db.GetData(0,lista);
            int conteo = dtExposure.Rows.Count;
            if (conteo ==0)
            {
                lista = lista.Substring(14);
                lista = lista.Replace("(", "");
                lista = lista.Replace(")", "");
                check.SendErrorProcess(ErrSubject, lista, "", SenderNoClinic);
                return true;
                // nothing to process
            }


            //===========================================
            //==
            //=========================================== 
            for (int i = 0; i < File.Length; i++)
            {
                string archivo = File[i];
                if (archivo == "" && i > 5) break;
                if (archivo.ToUpper().IndexOf(".PDF") < 1) continue;

                string archivo2 = archivo;
                archivo = archivo.Replace(" ", "");
                if (!archivo.ToUpper().Contains(FileExt))
                {
                    int pos1 = archivo.LastIndexOf("\\");
                    if (pos1 > 0) archivo = archivo.Substring(pos1 + 1);
                    fx.AddWrong(ref wrongFiles, archivo2);
                    //====================================
                    File[i] = "";
                    continue;
                }

                string EmpID = fx2.GetEmpID(File[i]);
                if (EmpID == "")
                {
                    int pos1 = archivo.LastIndexOf("\\");
                    if (pos1 > 0) archivo = archivo.Substring(pos1 + 1);
                    fx.AddWrong(ref wrongFiles, archivo2);
                    //===================================
                    File[i] = "";
                    continue;
                }

                string where1 = "EmpID = '" + EmpID + "'";
                DataRow[] result = dtExposure.Select(where1);
                if (result.Length == 0)
                {
                    Missing.Add(EmpID);
                    File[i] = "";
                    continue;
                }


                //DataRow drEmp = fx.IfEmplInDB(ref wrongEmplID, EmpID, sender,
                //SumType, "", "", "", dtExposure);

                //if (drEmp == null)
                //{
                //    fx.AddWrong(ref wrongEmplID, EmpID);
                //    File[i] = "";
                //    continue;
                //}

//                fx.IfCreated(ref created, drEmp);
                string sql = LoadInto + " = @DOC, STATUS = 'Notified " + SumType + "' ";

                DataAccessUpdate.LoadDocument(EmpID, File[i], sql, sender);

                //string EmpName = drEmp[1].ToString();
                //string EmpLoc = drEmp[2].ToString();
                string EmpName = result[0][1].ToString();

                string EmpLoc = result[0][2].ToString();
                string EmpMobile = result[0][8].ToString();
                string DOH = result[0][11].ToString();

                string NewEndDt = "";  // val4
                string val5 = "";
                string AddDays = ""; 

                //========================
                if (type1 == "EXTENSION")
                {
                    
                    try
                    {
                        string where11 = "EmpID = '" + EmpID + "'";
                        DataRow[] result1 = dtExtension.Select(where1);

                        if (result1.Length > 0)
                        {
                            NewEndDt = result1[0][1].ToString(); // new end date
                            val5 = result1[0][2].ToString(); // recommendation
                            AddDays = result1[0][3].ToString(); // add days
                            log.Info(EmpID + " " + NewEndDt + " " + val5);
                        }
                        else
                        {
                            NewEndDt = "Not Found in File";
                            log.Info(EmpID + "  not found");
                        }
                    }
                    catch (Exception es)
                    {
                        log.Info(es.ToString());
                        System.Environment.Exit(0);
                    }

                    //string sql2 = "update declarationforms set gender = '" + val5 + "', ";
                    //sql2 = sql2 + " ModifiedBy='" + sender + "', ";
                    //sql2 = sql2 + " DateModified='" + fx.Today() + "' ";
                    //sql2 = sql2 + " where EmpID ='" + EmpID + "' ";
                }

                try
                {
                    // SEND NOTIFICATION FOR EACH CASE (NOT EXTENSION)
                    string[] arrSent = correos.sendingAllEmails(TemplateNo, EmpID, EmpName, EmpLoc,
                    "", "", AddDays, NewEndDt, val5, File[i], HR);

                    // FOR SUMMARY
                    dtNotif.Rows.Add(System.DateTime.Now.ToString(),
                        EmpID, EmpName, EmpLoc, SumType,
                        arrSent[0], arrSent[1], arrSent[2], arrSent[3]);
                }
                catch(Exception es)
                {
                    log.Info(es.ToString());
                }

                string mt = fx.Today();

                if (type1 == "SWAB_BATCH")
                {
                    result = dtWorkDay.Select(where1);
                    if (result.Length > 0)
                    {
                        dtInitial.Rows.Add(
                        EmpID,
                         
                        result[0][1].ToString(), // first
                        result[0][2].ToString(), // middle
                        result[0][3].ToString(), // last

                        "", //DOH,
                        result[0][6].ToString(), // DOB
                        EmpMobile,
                        "Initial Swab",  // W/REFERRAL

                        result[0][4].ToString(), //GENDER
                        "", //"Civil",

                        "", //"Remarks",
                         result[0][9].ToString()+" " + result[0][10].ToString(),
                         result[0][5].ToString(), // site
                        mt //"MC DATE"
                        );
                    }

                    //dt1.Columns.Add("DOH", typeof(string));
                    //dt1.Columns.Add("DOB", typeof(string));
                    //dt1.Columns.Add("Contact Number", typeof(string));
                    //dt1.Columns.Add("W/REFERRAL", typeof(string));
                    //dt1.Columns.Add("Gender", typeof(string));
                    //dt1.Columns.Add("Civil Status", typeof(string));
                    //dt1.Columns.Add("Remarks", typeof(string));
                    //dt1.Columns.Add("Address", typeof(string));
                    //dt1.Columns.Add("SITE", typeof(string));
                    //dt1.Columns.Add("MC DATE", typeof(string));

                }
            }



            if (type1 == "EXTENSION") //CheckIfMissing(dtExtension);
            {
                string MISSING = "";
                foreach (DataRow dr in dtExtension.Rows)
                {
                    string eid = dr[0].ToString();
                    string missing = dr[1].ToString();
                    if (missing == "MISSING")
                    {
                        if (MISSING == "") MISSING = eid;
                        else MISSING = MISSING + ", " + eid;

                    }
                }

                if (MISSING != "")
                {
                    string msg = "Hi " + email.Sender.Name.ToString() + "<br></br>";
                    msg = msg + "The following employees didn''t have an attachment and were not processed: ";
                    msg = msg + MISSING + EmailEnd();

                    fx2.SendEmail("Extension Batch Error - " + email.Subject.ToString(), msg, email.Sender.Address.ToString(),null);
                }
            }

            string subject = email.Subject;
            string subject2 = "Summary of Notifications for " + subject;

            //============================
            //if (type1 == "InitialSwab")
            //    SummaryInitialSwab(dtInitial, File, distro);
            //else

            correos.SentSummary(dtNotif, type1, subject2, distro);

            if (type1 == "SWAB_BATCH")
            {
                SummaryInitialSwab(dtInitial, File, "SWAB_First");
            }

            

            string wrongEmplID = "";
            if (Missing.Count>0) wrongEmplID = string.Join<string>(",", Missing);

            if (wrongFiles != "" || wrongEmplID != "")
                check.SendErrorProcess(ErrSubject, wrongEmplID, wrongFiles, SenderNoClinic);

            //if (created != "")
            //    check.SendCreated(SenderNoClinic, created, subject);

            return true;
        }

        void SummaryInitialSwab(DataTable dtNotif, string[] File, string distro)
        {
            Funciones fx = new Funciones();
            string now1 = fx.Today();
            int pos1 = now1.IndexOf(" ");
            string subject = "For Initial Swab Test_" + now1.Substring(0, pos1) + " ";
            subject = subject + now1.Substring(pos1 + 1);
            subject = subject.Replace(":", ".");
            subject = subject.Replace("/", ".");

            string body = "List of employees for initial Swab test";
            distro = ConfigurationManager.AppSettings[distro].ToString();

            string tempFolder = ConfigurationManager.AppSettings["TempFolder"].ToString();
            string SummaryInitial = tempFolder + "\\" + subject + ".xlsx";
            Exposure.Program.ExportData(dtNotif, SummaryInitial);

            bool included = false;
            for (int i = 0; i < File.Length; i++)
            {
                if (!File[i].ToUpper().Contains(".PDF")) File[i] = "";
                if (File[i] == "" && !included )
                {
                    File[i] = SummaryInitial;
                    included = true;
                }
            }

            Exposure.SendEmails correos = new Exposure.SendEmails();
            correos.GeneralEmail(subject, body, File, distro);
        }


        string EmailEnd()
        {
            string msg = "<br></br>" + "This is an automated email for notification. Please do not reply. </br></br>";
            msg = msg + "Thank you,</br>CNX HR Auto Response";
            return msg;
        }

        DataTable Parse_Extended(string file1, string[] File, string sender)
        {
            DataTable dtExt = new DataTable();
            dtExt.Columns.Add("EmpID", typeof(string));
            dtExt.Columns.Add("NewEnd", typeof(string));
            dtExt.Columns.Add("Recommendation", typeof(string));
            dtExt.Columns.Add("Days", typeof(string));

            Download_Read down = new Download_Read();
            DataTable dtRead = down.ReadExcel(file1,0,"");
            int conteo = 0;
            if (dtRead != null) conteo = dtRead.Rows.Count;
            if (conteo == 0)
            {
                SendErrorEmail SE = new SendErrorEmail();
                //SE.FileWithoutData(senderName, sender, FILENAME);
                return null;
            }

            foreach (DataRow dr in dtRead.Rows)
            {
                string eid = dr[0].ToString();
                eid = Regex.Replace(eid, @"[^\d-+()]", "");
                if (eid == "") continue;

                bool found = false;
                for (int i = 0; i < File.Length; i++)
                {
                    string archivo = File[i];
                    if (archivo == "" && i > 5) break;

                    if (archivo.Contains(eid)) found = true;
                    if (archivo == "") continue;
                    if (archivo.ToUpper().IndexOf(".PDF") < 1) continue;
                }

                if (!found)
                {
                    dtExt.Rows.Add(eid, "MISSING", "");
                    continue;
                }

                string QED = dr[2].ToString();
                string NewExt = dr[3].ToString();
                string days = dr[4].ToString();
                string recommend = dr[5].ToString();

                if (recommend.Length > 100) recommend = recommend.Substring(0, 100);

                QED = QED.Replace("'", "");
                NewExt = NewExt.Replace("'", "");
                days = days.Replace("'", "");
                recommend = recommend.Replace("'", ".");

                string sql = "update declarationforms ";
                sql = sql + " set QuarantineStartDate = '" + dr[1].ToString() + "', ";
                if (QED.Length > 7) sql = sql + " QuarantineEndDate = '" + QED + "', ";
                if (NewExt.Length>7) sql = sql + " dtExtEnd = '" + NewExt + "', ";
                sql = sql + " SQAdditionalDays = '" + days + "', ";

                Funciones fx = new Funciones();
                sql = sql + " DateModified = '" + fx.Today() + "', ";
                sql = sql + " UpdatedBy = '" + sender + "', ";

                

                sql = sql + " Gender = '" + recommend + "' ";
                sql = sql + " WHERE EmpID ='" + dr[0].ToString() + "' ";
                sql = sql + " AND STATUS <> 'Notified FTW' ";

                DataAccessUpdate.UpdateSql(sql);

                dtExt.Rows.Add(eid, NewExt, recommend, days);

            }
            return dtExt;

        }
    }
}
