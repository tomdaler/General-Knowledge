using log4net;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Reflection;


namespace UpdateDatabase
{
    class DownLoadExcel
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        string FILENAME = ConfigurationManager.AppSettings["TempFile"].ToString();

        public DataTable GetFile(EmailMessage email)
        {
            Download(email);
            return ReadExcel("", 0, "");
        }

        public string Download(EmailMessage email)
        {
            FileAttachment file1 = null;
            string theFile = "";
           
            string[] File = getAttachments(email, 100, email.Sender.Address.ToString());
            for (int i = 0; i < File.Length; i++)
            {
                if (File[i].ToUpper().IndexOf(".XLSX") > 1)
                {
                    file1 = email.Attachments[i] as FileAttachment;
                    theFile = File[i];
                    break;
                }
            }

            if (file1 == null) return "";

            try
            {
                System.IO.File.Delete(FILENAME);
            }
            catch (Exception) { }

            FileStream theStream = new FileStream(FILENAME, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            file1.Load(theStream);
            StreamWriter write = new StreamWriter(theStream);
            write.Dispose();
            theStream.Dispose();

            return theFile;
        }

        public DataTable ReadExcel(string filename, int SheetNo, string SheetName)
        {
            OleDbConnectionStringBuilder connStringBuilder = new OleDbConnectionStringBuilder();
            if (filename == "") filename = FILENAME;
            log.Info(FILENAME);

            connStringBuilder.DataSource = filename;
            connStringBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            connStringBuilder.Add("Extended Properties", "Excel 8.0;HDR=NO;IMEX=1");

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            DbConnection connection = factory.CreateConnection();

            try
            {
                connection.ConnectionString = connStringBuilder.ConnectionString;
                connection.Open();
            }
            catch (Exception es)
            {
                string ss = es.ToString();
                log.Info(ss);
                return null;
            }
            //var sheet1 = connection.GetSchema("Tables").Rows[0]["TABLE_NAME"];

            DbCommand selectCommand = factory.CreateCommand();

            
            
            if (SheetName == "")
            {
                SheetName = connection.GetSchema("Tables").Rows[SheetNo]["TABLE_NAME"].ToString();
            }

            //try
            //{
               // for (int rowid =0; rowid<10;rowid++)
           //       SheetName = connection.GetSchema("Tables").Rows[rowid]["TABLE_NAME"].ToString();
            
            //}
            //catch (Exception) { }

            string sql = "SELECT * FROM [" + SheetName + "]";

            log.Info("READING EXCEL " + sql);

            selectCommand.CommandText = sql;
            selectCommand.Connection = connection;

            DbDataAdapter adapter = factory.CreateDataAdapter();

            adapter.SelectCommand = selectCommand;
            DataSet data = new DataSet();
            DataTable dt = null;
            try
            {

                adapter.Fill(data);
                dt = data.Tables[0];
                log.Info(dt.Rows.Count);
            }
            catch (Exception)
            {
                dt = null; 
            }
            connection.Close();
            connection.Dispose();

            //try
            //{
            //    int conteo = dt.Rows.Count;

            //    string ss2 = dt.Rows[1][0].ToString();
            //}
            //catch (Exception) { }

            return dt;
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
                catch (Exception es)
                {
                    log.Info(filename + " " + es.ToString());
                    //Exposure.SendEmails sendAck = new Exposure.SendEmails();
                    string body = "The file name " + filename + " has a wrong character, it will not be processed";
                    //sendAck.SendEmail("Wrong File Name", body, sender, "");

                }

            }
            return File;
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


    }
}
