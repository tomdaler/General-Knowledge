using Microsoft.Exchange.WebServices.Data;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Objects;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class Funciones
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void NotFoundColumn(string subject, string Task)
        {
            SendErrorEmail sent2 = new SendErrorEmail();
            string MSG = "Email Subject " + subject;
            MSG = MSG + "</BR>Process cannot find columns, you may be using a wrong file for " + Task + " scenario.";
            sent2.GeneralError(MSG, "", "");
            return;
        }

        public string CleanFileName(string filename)
        {
            filename = filename.Replace("|", "");
            filename = filename.Replace("*", "");
            filename = filename.Replace("&", "");
            filename = filename.Replace("@", "");
            filename = filename.Replace("!", "");
            filename = filename.Replace("?", "");
            filename = filename.Replace(">", "");
            filename = filename.Replace("<", "");
            filename = filename.Replace("\\", "");
            filename = filename.Replace("\"", "");
            return filename;
        }

        public ContactDate GetDataInfo(DataTable dtExposure, 
          string EmpID, bool CheckEmployee, bool LoadWKData)
        {
            if (dtExposure == null)
            {
                Exposure.DataBase DB2 = new Exposure.DataBase();
                dtExposure = DB2.GetDataExposure("TASK1");
            }
            
            ContactDate ct = new ContactDate();
            ct.EmpID = EmpID;

            string where1 = "EmpID = '" + ct.EmpID + "'";

            //sql = "SELECT EmpID, EmpName, EmpLocation, ";
            //sql = sql + " QuarantineStartDate, QuarantineEndDate,";
            //sql = sql + " dtExtEnd, SQAdditionalDays, DateCreated,   ";
            //sql = sql + " EmpMobile, Landline,   ";
            //sql = sql + " POC, RepType, ForCTracing, Severity, BHERT, dtFTW, ";
            //sql = sql + " CurrFaculty,moredetails,ICU, dtConfinement, FirstName, MSA, LastName, ID";

            // IN EXPOSURE
            DataRow[] result = dtExposure.Select(where1);
            if (result.Length > 0)
            {
                ct.Status = "Update";
                ct.EmpName     = result[0]["EmpName"].ToString();
                ct.Location    = result[0]["EmpLocation"].ToString();
                ct.DateCreated = result[0]["DateCreated"].ToString();
                ct.RepType     = result[0]["RepType"].ToString();
                ct.FirstName   = result[0]["FirstName"].ToString();
                ct.LastName    = result[0]["LastName"].ToString();
                ct.ID          = result[0]["ID"].ToString();
                ct.MiddleName  = result[0]["MiddleName"].ToString();
                try
                {
                    ct.MSA = result[0]["MSA"].ToString();
                }
                catch (Exception) { }

                if (!LoadWKData) return ct;
            }

            // NEW
            //=====
            if (ct.Status != "Update")
            {
                ct.Status = "New";
                ct.DateCreated = Today();
            }

            // DeclarationFormsWorkDay
            // empid, first, middle, last, gender, location, dob, loc1, loc2, add1, add2, msa
            DataTable dtWorkDay = Data.getWK();

            result = dtWorkDay.Select(where1);
            if (result.Length > 0)
            {
                ct.FirstName = result[0][1].ToString();
                ct.MiddleName = result[0][2].ToString();
                ct.LastName = result[0][3].ToString();
                ct.EmpName = ct.FirstName + " " + ct.MiddleName + " " + ct.LastName;

                ct.Gender = result[0][4].ToString();
                ct.Location = result[0][5].ToString();
                ct.Age = result[0][6].ToString();

                ct.SiteCity = result[0][8].ToString();

                ct.WK_Address1 = result[0][9].ToString();
                ct.WK_Address2 = result[0][10].ToString();

                ct.MSA = result[0][11].ToString();
                return ct;
            }

            if (!CheckEmployee) return null;

            // IN EMPLOYEE
            //string sql = "SELECT HR_ID, First_Name, Middle_Name, Last_Name, ";
            //sql = sql + " Location, Program_Name FROM EMPLOYEE(nolock) ";
            //sql = sql + " WHERE HR_ID = '" + EmpID + "'";

            Exposure.SendEmails DB = new Exposure.SendEmails();
            DataTable dtEMPLOYEE = DB.GetInfoEmployee(ct.EmpID);
            if (dtEMPLOYEE == null) return null;

            ct.FirstName = dtEMPLOYEE.Rows[0][1].ToString();
            ct.MiddleName = dtEMPLOYEE.Rows[0][2].ToString();
            ct.LastName = dtEMPLOYEE.Rows[0][3].ToString();
            ct.EmpName = ct.FirstName + " " + ct.MiddleName + " " + ct.LastName;

            ct.Location = dtEMPLOYEE.Rows[0][4].ToString();
            ct.EmpProgram = dtEMPLOYEE.Rows[0][5].ToString();

            return ct;
        }

      
        public string EmailSenderSupClinic(string EmpID, string strLocation, string sender, bool AvoidClinic)
        {
            if (strLocation == "" || EmpID =="")
            {
                // get info by email
                SendErrorEmail ss = new SendErrorEmail();
                DataTable dtLocInfo = ss.GetSenderInfo(sender);

                if (dtLocInfo.Rows.Count > 0)
                {
                    strLocation = dtLocInfo.Rows[0][2].ToString();
                    EmpID = dtLocInfo.Rows[0][3].ToString();
                }
            }

            if (EmpID != "")
            {
                Exposure.DataBase db = new Exposure.DataBase();
                string[] Supervisors = db.GetManagers(EmpID);

                sender = sender + ";" + Supervisors[1].ToString();
                sender = sender + GetHREmails(strLocation, "", AvoidClinic);
            }
            return sender;
        }

        public string ConstructBody(string senderName, bool CT, string msg, string subject)
        {
            Exposure.SendEmails sent = new Exposure.SendEmails();
            string body = "Hi " + senderName + ",<br><br>This is an automated response. Please do not reply to this email.<br><br>";
            body = body + "We encountered an error in your email: "+ subject+"</br></br>";
            //if (CT) body = body + "</br>COVID19 CONTACT TRACING LIST - </br>";
            body = body + msg+ sent.ending;
            return body;
        }

        public void Task3_4_Err(string TEMPLATEFILE, string Task, 
           string err, string sender, string subject, 
            string senderName, string EmpId, string EmpName)
        {
            string err2 = "";
            
            if (err.Contains("Cannot find column"))
            {
                err = "Cannot find column";
                err2 = ConstructBody(senderName, true, "Error: Incorrect Contact Tracing List Template.", subject);
            }

            if (err.Contains("Invalid Index Case Found"))
            {
                err = "Invalid Index Case Found - "+ Task;
                err2 = ConstructBody(senderName, true, err, subject);
            }

            if (err.Contains("No Index Case Found in"))
            {
                err = "No Index Case Found in " + Task;
                err2 = ConstructBody(senderName, true, err, subject);
            }

            if (err.Contains("There is no row at position 1"))
            {
                err = "Error: Incorrect Contact Tracing List Template. - ";
                err2 = ConstructBody(senderName, true, err, subject);
            }
            
            if (EmpId.Length > 15)
                EmpId = EmpId.Substring(0, 15);

            log.Error(sender + "-->" + subject + "-->" + EmpId + "-->" + err);
            
            if (err.Length > 500) err = err.Substring(0, 500);

            SendErrorEmail logs = new SendErrorEmail();
            logs.InsertIntoLog(err,EmpId, EmpName, sender);
            log.Error(sender + "-->" + EmpId + "-->" + err);

            Exposure.SendEmails sent = new Exposure.SendEmails();

            if (err2 != "")
            {
                sender = EmailSenderSupClinic("", "", sender, false);
                sent.SendEmail("COVID19 Case Report – Error Encountered - CT", err2, sender, TEMPLATEFILE);
                return;
            }
           
            err = "GENERIC ERROR "+Task + "</br>Sender" + sender+"</br></br>" + err;
            sent.SendEmail("COVID19 Case Report – Error Encountered - CT", err, "", TEMPLATEFILE);
        }

        public string[] ListAttachments(EmailMessage email)
        {
            int count = email.Attachments.Count;
            FileAttachment fileAttach = null;

            string[] Files = new string[100];
            for (int i = 0; i < 100; i++) Files[i] = "";

            for (int i = 0; i < count; i++)
            {
                int size1 = email.Attachments[i].Size;
                fileAttach = email.Attachments[i] as FileAttachment;
                if (fileAttach == null) continue;
                string filename = fileAttach.Name;
                if (filename == "") continue;
                filename = @"C:\Temp\Exposure\" + filename;
                filename = filename.Replace(" -", "-");
                Files[i] = filename;
                log.Info(filename);
            }
            return Files;
        }

        public string ConvertToText(string msg)
        {
            //msg = msg.Replace("Required information", "");
            //msg = msg.Replace("Required Information", "");
            msg = msg.Replace("&nbsp;", " ");
            msg = msg.Replace("*", "");

            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            msg = reg.Replace(msg, "");
            msg = msg.Replace("\r\n", "");

            msg = msg.Replace("'", "�");
            msg = msg.Replace("  ", " ");

            msg = msg.Replace("Recommendation (", "XYZ");
            msg = msg.Replace("Recommendation(", "XYZ");

            msg = msg.Replace("Quarantine Start Date", "SQD");

            msg = msg.Replace("COVID19 )", "COVID19)");

            string LogMsg = msg;
            if (LogMsg.Length > 930) LogMsg = LogMsg.Substring(0, 930);
            //log.Info(LogMsg);
            return msg;
        }

        public void DeleteFile(string file)
        {
            if (file == "") return;
            try
            {
                File.Delete(file);
            }
            catch (Exception es) {
                string ss = es.ToString();
            }
        }
             
        public string AppendNew(string prev1, string new1, int limit, bool AlwaysAppend)
        {
            if (new1.Trim() == "") return prev1;
            string new2 = AppendTime(new1);
            if (prev1.Trim() == "")
                return new2;

            string retorno = prev1;

            if (!prev1.Contains(new1) || AlwaysAppend)
                retorno = prev1 + ". " + new2; // append

            if (retorno.Length > limit)
                retorno = retorno.Substring(0, limit);

            return retorno;
        }
        public string MissingInfo(string variable1, string feedback, string missing)
        {
            if (variable1.Trim() == "")
            {
                if (missing == "")
                    missing = feedback;
                else
                    missing = missing + ", " + feedback;
            }
            return missing;
        }
        
        public string ChangeF2(string field1)
        {
            field1 = field1.Replace("'", "");
            field1 = field1.Replace("\\", "-");
            field1 = field1.Replace("/", " - ");
            return field1;
        }
                
        public void SetLimit(ref string valor, int limit)
        {
            if (valor.Length > limit) valor = valor.Substring(0, limit);
        }
        public void AddWrong(ref string wrong, string OneValue)
        {
            if (wrong == "") wrong = OneValue;
            else wrong = wrong + ", " + OneValue;
        }
        //public void IfCreated(ref string created, DataRow dr)
        //{
        //    try
        //    {
        //        string empID = dr[0].ToString();
        //        string First = dr[6].ToString();
        //        string Middle = dr[7].ToString();
        //        string Last = dr[8].ToString();
        //        string Loc = dr[2].ToString();

        //        if (Last != "")
        //        {
        //            empID = empID + " " + First + " " + Last + " - " + Loc+" <br>";
        //            created = created + empID;
        //        }
        //    }
        //    catch (Exception) { }
        //}

        public string CheckDate(string fec)
        {
            try
            {
                DateTime dd = DateTime.Parse(fec);
            }
            catch (Exception)
            {
                Funciones fx = new Funciones();
                fec = fx.Today();
            }
            return fec;
        }

        public string RemoveGarbage(string field)
        {
            field = field.Replace("Required field", "");
            field = field.Replace("Workday ID", "");
            field = field.Replace(":", "");
            field = field.Replace("Employee Name", "");
            field = field.Replace("(", "");
            field = field.Replace(")", "");
            field = field.Replace("Ex.", "");
            field = field.Trim();
            return field;
        }
        //public DataRow IfEmplInDB(ref string wrongEmplID, string EmpID, string sender, 
        //    string status, string Mobile, string Landline, string TimeCall, DataTable dtExposure)
        //{
        //    string where1 = "EMPID = '" + EmpID + "'";
        //    DataRow[] result = dtExposure.Select(where1);
        //    if (result.Length > 0) return result[0];

        //    status = "Notified " + status;
            
        //    string SqlEmpl = ConfigurationManager.ConnectionStrings["SqlEmpl"].ConnectionString;
        //    SqlConnection sqlcon = new SqlConnection(SqlEmpl);
        //    sqlcon.Open();

        //    string sql = "SELECT HR_ID, First_Name +' '+ Last_Name, ";
        //    sql = sql + " Location, '','','', ";
        //    sql = sql + " First_Name, Middle_Name, Last_Name, Program_Name ";

        //    sql = sql + " FROM Employee(nolock) ";
        //    sql = sql + " WHERE HR_ID = '" + EmpID + "'";

        //    // IMPORTANT
        //    sql = sql + " ORDER BY last_cvg_logon_date DESC";

        //    SqlCommand cmd = new SqlCommand(sql, sqlcon);
        //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    adapter.Fill(dt);

        //    if (dt.Rows.Count == 0)
        //    {
        //        sqlcon.Close();
        //        sqlcon.Dispose();
        //        return null;
        //    }

        //    sqlcon.Close();
        //    sqlcon.Dispose();

        //    // SITES
        //    string connSites = ConfigurationManager.ConnectionStrings["SqlSites"].ConnectionString;
        //    SqlConnection sqlcon3 = new SqlConnection(connSites);
        //    sqlcon3.Open();

        //    DataRow dr = dt.Rows[0];
        //    sql = "SELECT Location FROM HR_WDActiveEmployees(nolock) ";
        //    sql = sql + " WHERE firstname = '" + dr[6].ToString() + "' ";
        //    sql = sql + " AND middlename = '" + dr[7].ToString() + "' ";
        //    sql = sql + " AND lastname =   '" + dr[8].ToString() + "' ";

        //    SqlCommand cmd3 = new SqlCommand(sql, sqlcon3);
        //    SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);

        //    DataTable dt3 = new DataTable();
        //    adapter3.Fill(dt3);

        //    string loc2 = "";
        //    string loc1 = dt.Rows[0][2].ToString();
        //    if (dt3.Rows.Count > 0)
        //    {
        //        loc2 = dt3.Rows[0][0].ToString();
        //        dt.Rows[0][2] = dt3.Rows[0][0].ToString();
        //        dt.AcceptChanges();
        //    }
            
        //    sqlcon3.Dispose();
        //    sqlcon3.Close();
            
        //    string EmpLoc = dt.Rows[0][2].ToString();
        //    string EmpFirst = dt.Rows[0][6].ToString();
        //    string EmpMiddle= dt.Rows[0][7].ToString();
        //    string EmpLast= dt.Rows[0][8].ToString();
        //    string EmpProgram = dt.Rows[0][9].ToString();

        //    DataAccess da = new DataAccess();
        //    da.CreateFromNotification(EmpID, sender, status,
        //        EmpLoc, EmpProgram, 
        //        EmpFirst, EmpMiddle, EmpLast,
        //        Mobile, Landline, TimeCall);

        //    return dt.Rows[0];
        //}
           
        public byte[] GetFile(string File)
        {
            if (File == "") return null;
            FileStream stream = new FileStream(File, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            byte[] doc = reader.ReadBytes((int)stream.Length);
            reader.Close();
            stream.Close();

            return doc;
        }

        public string RemoveClinics(string sender)
        {
            string[] clinics = sender.Split(';');
            string SenderNoClinic = "";
            for (int i = 0; i < clinics.Length; i++)
            {
                if (!clinics[i].ToUpper().Contains("CLINIC"))
                    SenderNoClinic = SenderNoClinic + ";" + clinics[0];
            }
            return SenderNoClinic;
        }
        public string GetHREmails(string strLocation, string AllEmails, bool AvoidClinic)
        {
            if (strLocation == null || strLocation == "") return AllEmails;
            Exposure.DataBase db = new Exposure.DataBase();

            Variables vari = new Variables();
            DataTable HR = vari.getHR();


            AllEmails = AllEmails + ";" + db.GetHR_Info2(strLocation, HR, AvoidClinic);
            return AllEmails;
        }
        public string GetConfirmEmails(string rep1, string rep2, string loc)
        {
            if (!rep2.ToUpper().Contains("CONFIRMED")) return "";

            string sender = "";
            if (rep1 == null) rep1 = "";

            if (!rep1.ToUpper().Contains("CONFIRMED")
              && rep2.ToUpper().Contains("CONFIRMED"))
            {
                sender = ConfigurationManager.AppSettings["Confirmed"].ToString();
                sender = GetHREmails(loc, sender, true);

                //Exposure.Notifications notif = new Exposure.Notifications();
                //string[] Supervisors = notif.GetMgrEmails(EmpID);

            }
            return sender;
        }
        public string AppendTime(string Remarks)
        {
            Funciones fx = new Funciones();
            string hoy = fx.Today();
            DateTime date1 = Convert.ToDateTime(hoy);
            string mm = date1.Month.ToString();
            string dd = date1.Day.ToString();
            string time1 = date1.ToString("hh:mm tt");
            Remarks = mm + "/" + dd + " " + time1 + " : " + Remarks;
            return Remarks;
        }
        public Duplicate IsDuplicated(string EmpID)
        {
            Duplicate retorno = new Duplicate();
            retorno.RepType = "";
            retorno.Symptom = "";
            retorno.Remarks = "NEW";
            retorno.len1 = 0;
            retorno.len2 = 0;
            retorno.len3 = 0;

            string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(SqlConn);
            sqlcon.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.Text;

            string sql = "select Remarks, MoreDetails, ";
            sql = sql + " EmpLocation, RepType, ";
            sql = sql + " ext1, ext2, ext3, ";
            sql = sql + " File1, File2, File3, EmpMobile, Landline, DateCreated ";

            sql = sql + " FROM DeclarationForms (nolock) ";
            sql = sql + " WHERE EmpID = '" + EmpID + "'";
            sql = sql + " AND STATUS <> 'Notified FTW' "; 
            cmd.CommandText = sql;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                try
                {
                    retorno.Remarks = dr[0].ToString();
                    retorno.Symptom = dr[1].ToString();

                    retorno.Location = dr[2].ToString();
                    retorno.RepType = dr[3].ToString();

                    retorno.ext1 = dr[4].ToString().Trim();
                    retorno.ext2 = dr[5].ToString().Trim();
                    retorno.ext3 = dr[6].ToString().Trim();

                    if (dr[4].ToString().Trim() != "")
                    {
                        byte[] data = (byte[])dr[7];
                        retorno.len1 = data.Length;
                    }

                    if (dr[5].ToString().Trim() != "")
                    {
                        byte[] data = (byte[])dr[8];
                        retorno.len2 = data.Length;
                    }

                    if (dr[6].ToString().Trim() != "")
                    {
                        byte[] data = (byte[])dr[9];
                        retorno.len3 = data.Length;
                    }

                    retorno.EmpMobile = dr[10].ToString();
                    retorno.Landline = dr[11].ToString();


                }
                catch (Exception ex) { string ss = ex.ToString(); }
            }
            sqlcon.Close();
            sqlcon.Dispose();

            return retorno;
        }
        public string Today()
        {
            var timeToConvert = DateTime.Now;  //whereever you're getting the time from
            var est = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            return TimeZoneInfo.ConvertTime(timeToConvert, est).ToString();
        }

        public string TodayDate()
        {
            string dd = Today();
            int pos1 = dd.IndexOf(" ");
            dd = dd.Substring(0, pos1);
            return dd.Trim();
        }
   
        public string GetText(string msg)
        {
            msg = Regex.Replace(msg, "<.*?>", String.Empty);
            msg = msg.Replace("\r", "");
            msg = msg.Replace("\n", "");
            msg = msg.Replace("&nbsp;", "");
            msg = msg.Replace("*", "");
            msg = msg.Replace(":", "");
            msg = msg.Replace("?", "");
            msg = msg.Replace("(Yes/No)", "");

            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            msg = reg.Replace(msg, "");
            msg = msg.Replace("\r\n", "");

            msg = msg.Replace("'", "�");
            msg = msg.Replace("  ", " ");
            //wherever there are 2 spaces - 1 space

            msg = msg.Replace("*", "");
            return msg;
        }
        public string GetValue(string msg, string start, string end)
        {
            string valor = "";
            start = start.Replace(":", "");
            end = end.Replace(":", "");

            int pos1 = msg.IndexOf(start);
            int largo = start.Length;
            if (pos1 < 0) return "";
            msg = msg.Substring(pos1 + largo);

            pos1 = msg.IndexOf(end);
            try
            {
                valor = msg.Substring(0, pos1);
            }
            catch (Exception) { }

            valor = valor.Replace(":", "");
            valor = valor.Replace("'", "''");
            valor = valor.Replace("&nbsp;", "");
            valor = valor.Trim();
            return valor;
        }
        public string MoreData(string field1, string val1)
        {
            if (field1 != "" && val1 != "")
                return ", " + field1 + " = '" + val1 + "' ";

            return "";
        }
           
        public string[] getAttachments(EmailMessage email, int max1, string sender)
        {
            string[] File = new string[max1];
            string filename = "";
            int cual = 0;
            FileAttachment fileAttach = null;
            int count = email.Attachments.Count;

            for (int i = 0; i < max1; i++) File[i] = "";

            for (int i = 0; i < count; i++)
            {
                if (cual == max1) break;

                int size1 = email.Attachments[i].Size;
                fileAttach = email.Attachments[i] as FileAttachment;

                try
                {
                    if (fileAttach == null) continue;

                    filename = fileAttach.Name;
                    if (filename == "") continue;

                    if (size1 < 10000
                    && !filename.Contains(".pdf")
                    && !filename.Contains(".xlsx"))
                        continue;

                    if (filename.Contains("_MCE")
                       || filename.Contains("_FTW"))
                        filename = filename.Replace(" ", "");

                    filename = filename.Replace(".PDF", "pdf");
                    filename = filename.Replace(".Pdf", "pdf");
                    filename = filename.Trim();

                    filename = CleanFileName(filename);

                    string Path = ConfigurationManager.AppSettings["TempFolder"].ToString();
                    filename = Path + "\\" + filename;

                    if (filename.IndexOf("Outlook-") > 0) continue;
                    if (filename.IndexOf("logo.gif") > 0) continue;
                    if (filename.IndexOf(".") < 0) continue;
                    if (filename.IndexOf("mage0") > 0) continue;
                    if (size1 < 8000) continue;
                    filename = filename.Replace(" -", "-");
                    
                    //if (filename.Length < 5) continue;

                    //string ext = filename.Substring(filename.Length - 5);
                    //if (ext.Contains(".jpg")) continue;
                    //if (ext.Contains(".gif")) continue;
                    //if (ext.Contains(".bmp")) continue;
                    //if (ext.Contains(".img")) continue;
                    //if (ext.Contains("icon")) continue;

                    File[cual] = filename;
                }
                catch (Exception es)
                {
                    string ss = es.ToString();
                    continue;
                }
                
                if (filename == "") continue;

                try
                {
                    System.IO.File.Delete(filename);
                }
                catch (Exception) { }
                
                try
                {
                    FileStream theStream = new FileStream(filename, FileMode.OpenOrCreate,
                                                         FileAccess.ReadWrite);
                    fileAttach.Load(theStream);
                    File[cual++] = filename;
                    log.Info("Downloaded " + filename);
                    
                    theStream.Close();
                    theStream.Dispose();
                }
                catch(Exception es)
                {
                    log.Info(filename + " "+es.ToString());
                    Exposure.SendEmails sendAck = new Exposure.SendEmails();
                    string body = "The file name "+ filename+ " has a wrong character, it will not be processed";
                    sendAck.SendEmail("Wrong File Name", body, sender, "");

                }
               
            }
            return File;
        }

        public void KillWord()
        {
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.Contains("WINWORD")
                ||  process.ProcessName.Contains("winword"))
                {
                    process.Kill();
                }
            }
        }

        public void KillExcel()
        {
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.Contains("EXCEL")
                || process.ProcessName.Contains("excel"))
                {
                    process.Kill();
                }
            }
        }

        //To Delete Rows from the Attached Excel file if the EmpId's are NOT VALID.
        public void deleteFromExcel(string File1, List<string> Missing)
        {
            if (Missing.Count > 0)
            {
                try
                {
                    Excel.Application ExcelApp = new Excel.Application();
                    Excel.Workbook excelWorkbook = ExcelApp.Workbooks.Open(File1, ReadOnly: false);
                    //Excel.Workbook ExcelWB = ExcelApp.Workbooks.Open(File1);
                    ExcelApp.Visible = false;

                    Excel._Worksheet excelWorkbookWorksheet = excelWorkbook.Sheets[2];

                    Excel.Range usedRange = excelWorkbookWorksheet.UsedRange;
                    foreach (var a in Missing)
                    {
                        foreach (Excel.Range r in usedRange)
                        {
                            if (Convert.ToString(r.Value2) == a.ToString())
                            {
                                r.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                                //continue;
                                break;
                            }
                        }
                    }

                    excelWorkbook.Save();
                    excelWorkbook.Close(false);
                    ExcelApp.Application.Quit();

                    // cleanup:
                    if (ExcelApp != null)
                    {
                        Process[] pProcess;
                        pProcess = System.Diagnostics.Process.GetProcessesByName("Excel");
                        pProcess[0].Kill();
                    }
                }
                catch(Exception ex)
                {
                    log.Error("Error while performing deletion in the Delete from Excel function : " + ex.ToString());
                }
            }
        }

        public string GetEIDFile(string[] Files, string EmpID)
        {
            if (Files == null) return "";
            string FileName = "";
            log.Info("GetEIDFile");

            for (int i = 0; i < Files.Length; i++)
            {
                if (Files[i].Contains(EmpID)
                    && Files[i].ToUpper().Contains(".PDF"))
                {
                    FileName = Files[i];
                    break;
                }
            }

            if (FileName == "") return "";
            int pos1 = FileName.LastIndexOf(".");
            if (pos1 < 1) return "";
            string extension = FileName.Substring(pos1 + 1);
            if (extension.Length > 4) return "";
            if (!extension.ToUpper().Contains("PDF")) return "";
            return FileName;
        }

        public void SendAcknowledge(string Location, string subject, string sender, string senderName, string EmpName, bool UpdateDB, string FileName, bool AvoidClinic)
        {
            sender = GetHREmails(Location, sender, AvoidClinic);
            string Reply = reply(senderName, EmpName);
            
            Exposure.SendEmails sendAck = new Exposure.SendEmails();
            string[] Files = new string[1];
            Files[0] = FileName;

            //log.Info("ACKNOWLEDGE TO " + sender);
            //if (!UpdateDB)
            //    sendAck.GeneralEmailPROD(subject, Reply, Files, sender);
            //else
                 sendAck.SendEmail(subject, Reply, sender, FileName);
        }
        
        public string reply(string sender, string employee)
        {
            string msg = "<br>Hi " + sender + ",<br><br>";

            msg = msg + "We have received your report for <b>" + employee + ".</b> This will be endorsed to our Active One partner for rapid assessment, hence kindly advise the concerned employee to anticipate their call.</br></br>";

            msg = msg + "<b>For UPDATES/ADDITIONAL INFORMATION on the reported COVID-19 Case:</b> Accomplish the prescribed template AGAIN with the update/additional information in the corresponding field or use the REMARKS field, and send to PH_HealthAlert@concentrix.com and hr.autoresponse@concentrix.com.</br></br>";

            msg = msg + "If you have questions, clarifications or additional information, please file a ticket through ";
            msg = msg + "<b><a href='https://helpdesk.concentrix.com/'>HelpDesk</a></b>";
            msg = msg + " or via <b>MyHelp</b> on the <b>ConcentrixONE App.</b>";

            msg = msg + "</br></br>This is an automated response. Please do not reply to this email.</br></br>";

            msg = msg + "Thanks,<br>CNX HR Auto Response";
            return msg;
        }

        public void SendAcknowledgeTask3(string Location, string subject, string sender, string senderName, string EmpName, Dictionary<string, string> EmpList)
        {
            sender = GetHREmails(Location, sender, true);
            string Reply = replyTask3(senderName, EmpName, EmpList);
            Exposure.SendEmails sendAck = new Exposure.SendEmails();
            sendAck.SendEmail(subject, Reply, sender, "");
        }

        public string replyTask3(string sender, string employee, Dictionary<string, string> EmpList)
        {
            string msg = "<br>Hi " + sender + ",<br><br>";

            msg = msg + "We have received your Contact Tracing List for <b>" + employee + ".</b> This will be endorsed to our Active One partner for rapid assessment, hence kindly advise the concerned employee to anticipate their call.</br></br>";

            msg = msg + "Close Contact(s):</br></br>";

            msg = msg + "EmpId &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; EmpName</br>";

            foreach (var item in EmpList)
            {
                msg = msg + item.Key + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Value + "</br>";
            }

            msg = msg + "</br></br>This is an automated response. Please do not reply to this email.</br></br>";

            msg = msg + "Thanks,<br>CNX HR Auto Response";
            return msg;
        }
    }
}
