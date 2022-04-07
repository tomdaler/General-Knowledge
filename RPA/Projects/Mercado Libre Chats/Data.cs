using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Automatiza
{
    static class Data
    {
        public static DataTable dtLOB = new DataTable();
        public static DataTable dtLOBDaily = new DataTable();
        
        public static DataTable dtAgents = new DataTable();
        public static DataTable dtAgentsDaily = new DataTable();

        public static DataTable dtTL = new DataTable();
        public static DataTable dtTracker = new DataTable();

        
        //DataTable dt = Data.getLOB();
        //string where1 = "EMPLID = '" + EMPLID + "'";

        //DataRow[] result = dt2.Select(where1);
        //int count1 = dt2.Select(where1).Count();

        public static DataTable getLOB()
        {
            if (dtLOB == null || dtLOB.Rows.Count == 0)
            {
                dtLOB = GetDataTable("select * from ML_LOBs");
            }
            return dtLOB;
        }
        public static DataTable getLOBDaily()
        {
            string sql = "select * from ML_LOBs_Daily ";
            // sql = sql + " where date1 = Today2();

            if (dtLOBDaily == null || dtLOBDaily.Rows.Count == 0)
            {
                dtLOBDaily = GetDataTable(sql);
            }
            return dtLOBDaily;
        }
        public static DataTable getTL()
        {
            if (dtTL == null || dtTL.Rows.Count == 0)
            {
                dtTL = GetDataTable("select * from ML_TLs");
            }
            return dtTL;
        }
        public static DataTable getTracker()
        {
            if (dtTracker == null || dtTL.Rows.Count == 0)
            {
                dtTracker = GetDataTable("select * from ML_Tracker where Date1 ='"+ TODAY(2)+"' ");
            }
            return dtTracker;
        }
        public static DataTable getAgents()
        {
            if (dtAgents == null || dtAgents.Rows.Count == 0)
            {
                dtAgents = GetDataTable("select * from ML_Agents");
            }
            return dtAgents;
        }
        public static DataTable getAgentsDaily()
        {
            string sql = "select * from ML_Agents_Daily where Created='" + TODAY(2) + "'";

            if (dtAgentsDaily == null || dtAgentsDaily.Rows.Count == 0)
            {
                dtAgentsDaily = GetDataTable(sql);
            }
            return dtAgentsDaily;
        }
        public static DataTable GetDataTable(string sql)
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

                conn.Close();

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
        public static void ExecuteNonQuery(string sql)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            SqlConnection conn = new SqlConnection(StrConn);
            try
            {
                conn.Open();
                SqlCommand dbComm = new SqlCommand(sql, conn);
                dbComm.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception) {
                try
                {
                    conn.Close();
                }
                catch (Exception) { }
            
            }
        }
        public static string TODAY(int opcion)
        {
            DateTime dtt = DateTime.Now;
            string anio = dtt.Year.ToString();
            string mes = dtt.Month.ToString("D2");
            string dia = dtt.Day.ToString("D2");
            string retorno = "";

            if (opcion == 1) retorno = mes + "-" + dia + "-" + anio;
            if (opcion == 2) retorno = anio + "-" + mes + "-" + dia;
            return retorno;

        }
        public static DataTable NewChat(string EmpName, DataTable dtAgents)
        {
            // inset in ML_Tracker

            string where1 = "EmpName = '" + EmpName + "' AND Date='"+ TODAY(2) + "'";
            DataRow[] result = dtAgents.Select(where1);
            if (result.Count() >0)
            {
                // update DB
                // update dtAgents
            }
            else
            {
                // insert DB
                // add in dtAgents

            }

            return dtAgents;
        }
    }
}
