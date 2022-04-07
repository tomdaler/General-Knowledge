using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MercadoLibre
{
    public class Functions
    {

        //  public DataTable Transpose(DataTable dt)
        //{
        //    DataTable newDt = new DataTable();
        //    //Select indexes
        //    var indexes = dt.Rows.Cast<DataRow>().Select(row => dt.Rows.IndexOf(row));
        //    //Select the columns
        //    var newCols = indexes.Select(i => "Row" + i);
        //    //Add columns
        //    foreach (var newCol in newCols)
        //    {
        //        newDt.Add(newCol);
        //    }
        //    //Select rows
        //    var newRows = dt.Rows.Cast<DataColumn>().Select(col =>
        //    {
        //        row = newDt.NewRow();
        //        foreach (int i in indexes)
        //        {
        //            row[i] = dt.Rows[i][col.Name];
        //        }
        //        return row;
        //    });
        //    //Add row to new datatable
        //    foreach (var row in newRows)
        //    {
        //        newDt.Add(row);
        //    }
        //    return newDt;
        //}

        public DataTable GetChat(string CaseNo)
        {
           DataTable dt =  GetDataTable2("Select EmpName, Chat from ML_Chats where caseno=" + CaseNo);
           return dt;
        }

        public DataTable Pivot(DataTable tbl)
        {
            var tblPivot = new DataTable();
            tblPivot.Columns.Add(tbl.Columns[0].ColumnName);
            for (int i = 1; i < tbl.Rows.Count; i++)
            {
                tblPivot.Columns.Add(Convert.ToString(i));
            }
            for (int col = 0; col < tbl.Columns.Count; col++)
            {
                var r = tblPivot.NewRow();
                r[0] = tbl.Columns[col].ToString();
                for (int j = 1; j < tbl.Rows.Count; j++)
                    r[j] = tbl.Rows[j][col];

                tblPivot.Rows.Add(r);
            }
            return tblPivot;
        }
        public string TODAY(int opcion)
        {
            DateTime dtt = DateTime.Now;
            string anio = dtt.Year.ToString();
            string mes = dtt.Month.ToString("D2");
            string dia = dtt.Day.ToString("D2");
            string retorno="";
            if (opcion == 1) retorno = mes + "-" + dia + "-" + anio;
            if (opcion == 2) retorno = anio + "-" + mes + "-" + dia;
            return retorno;

        }

        public DataTable Transpose(DataTable dt)
        {
            DataTable dtNew = new DataTable();
            string[] titulos = new string[300];

            int i1 = 1;
            foreach (DataRow dr in dt.Rows)
            {
                titulos[i1++] = dr[0].ToString();
            }

            //adding columns    
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(titulos[i]);
            }
            dtNew.Columns[0].ColumnName = " ";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
               // dtNew.Columns[i + 1].ColumnName = dt.Rows[i].ItemArray[0].ToString();
            }

            //Adding Row Data
            for (int k = 1; k < dt.Columns.Count; k++)
            {
                DataRow r = dtNew.NewRow();
                r[0] = dt.Columns[k].ToString();
                for (int j = 1; j <= dt.Rows.Count; j++)
                    r[j] = dt.Rows[j - 1][k];
                dtNew.Rows.Add(r);
            }

            return dtNew;
        }

        public string GetAuthorization()
        {
            string Login = HttpContext.Current.Request.LogonUserIdentity.Name; // sa\\tdal6392
           string User = GetData("Select [Name] from ML_TLs where SO='" + Login + "'");
            
            return User;
        }

        public DataTable GetDataTable2(string sql)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            SqlConnection conn = new SqlConnection(StrConn);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlCommand comando = new SqlCommand(sql, conn);
                System.Data.IDataReader dr = comando.ExecuteReader();

                dt = new DataTable();
                dt.Load(dr);


                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = comando;
                //da.Fill(dt);
            }
            catch (Exception)
            {
                try
                {
                    conn.Close();
                }
                catch (Exception) { }
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable GetDataTable(string sql)
        {
            string err = "";
            string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            DataTable table = new DataTable();
            SqlConnection sqlcon = new SqlConnection(SqlConn);

            int i = 0;
            while (i < 3)
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(sql, sqlcon);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);
                    cmd.Dispose();
                    sqlcon.Dispose();
                    sqlcon.Close();
                    return table;
                }
                catch (Exception es2)
                {
                    err = es2.ToString();
                    if (err.Contains("but then an error occurred during the login process"))
                    {
                        err = "A connection was established with the server, but an error occurred during the login process. (provider: SSL Provider, error: 0 - An existing connection was forcibly closed by the remote host.) ---> System.ComponentModel.Win32Exception (0x80004005): An existing connection was forcibly closed by the remote host";
                        System.Threading.Thread.Sleep(15 * 1000);
                        i++;
                    }
                    else
                        i = 5;
                }
            }

            try
            {
                sqlcon.Dispose();
            }
            catch (Exception) { }

           
            return null;
        }

        public string GetAccess(string Login)
        {
            string sql = "Select [Name] from ML_TLs where SO='" + Login + "' "; // and level =0";

            string strCon = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(strCon);
            sqlcon.Open();

            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            var strData = cmd.ExecuteScalar();
            if (strData == null) strData = "";
            sqlcon.Dispose();
            sqlcon.Close();

            return Convert.ToString(strData);
        }

        public string GetData(string sql)
        {
            string strCon = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(strCon);
            sqlcon.Open();

            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            var strData = cmd.ExecuteScalar();
            if (strData == null) strData = "";
            sqlcon.Dispose();
            sqlcon.Close();

            return Convert.ToString(strData);
        }

        public void UpdateData(string sql)
        {
            string strCon = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(strCon);
            sqlcon.Open();

            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            sqlcon.Dispose();
            sqlcon.Close();
        }
    }
}