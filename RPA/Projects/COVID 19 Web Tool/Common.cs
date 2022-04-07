using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace WebApplication1
{
    public class Common
    {

        public void LoadVaccineStatus(DataTable dt, bool CT)
        {
            string EmpID = "EmpID";
            if (CT) EmpID = "CT_EmpID";

            string list1 = "";
            string comma = "";
            foreach (DataRow row in dt.Rows)
            {
                list1 = list1 + comma + "'" + ((string)row[EmpID]) + "'";
                comma = ",";
            }

            string sql = "select EmpID, VaccinationStatus  FROM  DeclarationFormsVaccinationStatus  WHERE Empid in (" + list1 + ")";
            string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            DataTable dtVaccine = new DataTable();
            SqlConnection sqlcon = new SqlConnection(SqlConn);
            sqlcon.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlcon;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtVaccine);

                HttpContext.Current.Cache.Insert("VaccineStatus", dtVaccine, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 360000, 0));
            }
            catch (Exception es)
            {
                dtVaccine = null;
                string es2 = es.ToString();
                int i = 1;
            }

            sqlcon.Close();
        }


        public string Vaccination(string EmpID)
        {
            DataTable dtVaccine = (DataTable)HttpContext.Current.Cache["VaccineStatus"];
            if (dtVaccine == null)  return "";

            string where = "EmpID ='" + EmpID +"'";

            DataRow[] rstVaccine = dtVaccine.Select(where);
            if (rstVaccine.Length > 0)
                return rstVaccine[0][1].ToString();
            return "";
        }

        public string getValue2(object valor)
        {
            try
            {
                string valor2 = valor.ToString();
            }
            catch (Exception) { return ""; }
            return valor.ToString();
        }

        public string GetAuthorization()
        {
            string Login = HttpContext.Current.Request.LogonUserIdentity.Name; // sa\\tdal6392
            //string users = ReadXml(usuario);
            string s1 = ConfigurationManager.AppSettings["Users1"].ToString();
            string s2 = ConfigurationManager.AppSettings["Users2"].ToString();

            string[] userID = s1.Split(',');
            string[] userName = s2.Split(',');

            string User = "";

            for (int i = 0; i < userID.Length; i++)
            {
                if (userID[i] == Login) User = userName[i];
            }
            return User;
        }

    }
}