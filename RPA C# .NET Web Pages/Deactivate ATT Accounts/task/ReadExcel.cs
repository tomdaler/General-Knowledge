
using log4net;
using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Reflection;

namespace Task
{
    class ReadExcel
    {
        private static log4net.ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable ReadExcelFile(string file)
        {

            //string conn = System.Configuration.ConfigurationManager.ConnectionStrings["Excel"].ConnectionString;
            string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties=Excel 8.0";

            conn = string.Format(conn, file);
            log.Info(conn);

            OleDbConnection connExcel = new OleDbConnection(conn);
            try
            {
                connExcel.Open();
            }
            catch (Exception ex)
            {
                log.Info(ex.ToString());
                return null;
            }

            try
            {
                string sheet = connExcel.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand command = new OleDbCommand("Select * from [" + sheet + "]", connExcel);
                System.Data.Common.DbDataReader dr = command.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);

                command.Dispose();
                connExcel.Close();
                connExcel.Dispose();
                connExcel.Close();
                return dt;
            }
            catch (Exception ex) { log.Info(ex.ToString()); }
            return null;


            }

        public DataTable ReadExcelFile2(string file)
        {
            OleDbConnectionStringBuilder connStringBuilder = new OleDbConnectionStringBuilder();
            connStringBuilder.DataSource = file;
            connStringBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";

            connStringBuilder.Add("Extended Properties", "Excel 8.0;HDR=NO;IMEX1");

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");

            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = connStringBuilder.ConnectionString;
            connection.Open();

            // var myTableName = connection.GetSchema("Tables").Rows[0]["TABLE_NAME"];

            DbCommand selectCommand = factory.CreateCommand();

            string sql = "SELECT * FROM [Sheet1$]";
            selectCommand.CommandText = sql;
            selectCommand.Connection = connection;

            DbDataAdapter adapter = factory.CreateDataAdapter();

            adapter.SelectCommand = selectCommand;
            DataSet data = new DataSet();
            adapter.Fill(data);
            DataTable dt = data.Tables[0];

            connection.Close();
            return dt;
        }
    }
}
