using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Automatiza
{
    static class Data
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<string> Logout = new List<string>();
        public static List<string> Pause = new List<string>();

        public static DataTable dtLOB = new DataTable();
        public static DataTable dtLOBDaily = new DataTable();

        public static DataTable dtAgents = new DataTable();
        public static DataTable dtAgentsDaily = new DataTable();

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
        public static DataTable getLOBDaily(bool Reload)
        {
            if (Reload || dtLOBDaily == null || dtLOBDaily.Rows.Count == 0)
            {
                // CAMBIAR
                string sql = "select * from ML_LOBs_Daily ";
                // sql = sql + " WHERE Date1 = TODAY(2);
                dtLOBDaily = GetDataTable(sql);
            }
            return dtLOBDaily;
        }
        public static DataTable getTracker()
        {
            if (dtTracker == null || dtTracker.Rows.Count == 0)
            {
                //CAMBIAR
                string sql = "  SELECT EmpID, status, CaseNo, created from ML_Tracker";
                sql = sql + " WHERE CAST(CREATED AS DATE) = CAST(GETDATE() AS DATE) order by ID desc ";

                dtTracker = GetDataTable(sql);
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

        public static DataTable getAgentsDaily(bool Reload)
        {
            if (Reload || dtAgentsDaily == null || dtAgentsDaily.Rows.Count == 0)
            {
                string sql = "SELECT id, EmpID FROM ML_Agents_Daily ";
                // CAMBIAR
                // sql = sql + " WHERE Created='" + Data.TODAY(2) + "'";
            
                dtAgentsDaily = GetDataTable(sql);
            }

            return dtAgentsDaily;
        }
        public static DataTable getAllAgentsDaily()
        {
            
            string sql = "select * from ML_Agents_Daily ";
            // CAMBIAR
            // sql = sql + " WHERE Created='" + Data.TODAY(2) + "'";
            sql = sql + " ORDER BY LOB ";

            return GetDataTable(sql);
        }

        #region Funciones

        public static string GetScalar(string sql)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            SqlConnection conn = new SqlConnection(StrConn);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlCommand comando = new SqlCommand(sql, conn);
                object valor = comando.ExecuteScalar();
                if (valor != null) return valor.ToString();
                return "";
            }
            catch (Exception es)
            {
                log.Info("Error " + es.ToString());

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
            return "";

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


                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = comando;
                //da.Fill(dt);
            }
            catch (Exception es)
            {
                log.Info("Error " + es.ToString());

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
        #endregion
    }
}
