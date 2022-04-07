using log4net;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace ConsoleApp1
{
    public class Download_Read
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        string FILENAME = ConfigurationManager.AppSettings["TempFolder"].ToString() + "\\" + "Excel.xlsx";

        public DataTable GetFile(EmailMessage email)
        {
            Download(email);
            return ReadExcel("",0,"");
        }

        public string Download(EmailMessage email)
        {
            FileAttachment file1 = null;
            string theFile = "";
            Funciones fx2 = new Funciones();

            string[] File = fx2.getAttachments(email, 100, email.Sender.Address.ToString());
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

            connStringBuilder.DataSource = filename;
            connStringBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            log.Info(FILENAME);

            connStringBuilder.Add("Extended Properties", "Excel 8.0;HDR=NO;IMEX=1");

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");

            DbConnection connection = factory.CreateConnection();

            try
            {
                connection.ConnectionString = connStringBuilder.ConnectionString;
                connection.Open();
            }
            catch(Exception es)
            {
                return null;
            }
            //var sheet1 = connection.GetSchema("Tables").Rows[0]["TABLE_NAME"];

            DbCommand selectCommand = factory.CreateCommand();

            string sheet1 = "";
            try
            {
               sheet1= connection.GetSchema("Tables").Rows[SheetNo]["TABLE_NAME"].ToString();
            }
            catch(Exception)
            {
                SheetNo = 0;
                sheet1 = connection.GetSchema("Tables").Rows[SheetNo]["TABLE_NAME"].ToString();
            }
        
            string sql = "SELECT * FROM [" + sheet1 + "]";
            log.Info("READING EXCEL "+sql);
            
            selectCommand.CommandText = sql;
            selectCommand.Connection = connection;

            DbDataAdapter adapter = factory.CreateDataAdapter();

            adapter.SelectCommand = selectCommand;
            DataSet data = new DataSet();
            adapter.Fill(data);
            DataTable dt = data.Tables[0];

            connection.Close();
            connection.Dispose();

            //string ss = dt.Rows[1][1].ToString();
            return dt;
        }

        public DataTable ReadExcelTask3n4(string filename, int SheetNo)
        {

            OleDbConnectionStringBuilder connStringBuilder = new OleDbConnectionStringBuilder();
            if (filename == "") filename = FILENAME;

            connStringBuilder.DataSource = filename;
            connStringBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            log.Info(FILENAME);

            connStringBuilder.Add("Extended Properties", "Excel 8.0;HDR=NO;IMEX=1");

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");

            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = connStringBuilder.ConnectionString;
            connection.Open();

            //var sheet1 = connection.GetSchema("Tables").Rows[0]["TABLE_NAME"];

            DbCommand selectCommand = factory.CreateCommand();

            string sheet1 = "";
            try
            {
                sheet1 = connection.GetSchema("Tables").Rows[SheetNo]["TABLE_NAME"].ToString();
            }
            catch (Exception)
            {
                SheetNo = 1;
                sheet1 = connection.GetSchema("Tables").Rows[SheetNo]["TABLE_NAME"].ToString();
            }


            string sql = "SELECT * FROM [" + sheet1 + "]";
            log.Info(sql);

            selectCommand.CommandText = sql;
            selectCommand.Connection = connection;

            DbDataAdapter adapter = factory.CreateDataAdapter();

            adapter.SelectCommand = selectCommand;
            DataSet data = new DataSet();
            adapter.Fill(data);
            DataTable dt = data.Tables[0];

            connection.Close();
            string ss = dt.Rows[1][1].ToString();
            return dt;
        }
    }
}
