using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;

namespace GPS
{
     public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class listado
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
    }

    class Variables
    {        
        public static string StrConn = ConfigurationManager.ConnectionStrings["GPS.Properties.Settings.ConnectionString"].ConnectionString;  
        //"Data Source = CDCDWV06; Initial Catalog = DCMdb; User Id = gpsapp; Password=gpsP@$$w0rd;Timeout=30";

        public static string DocNumber;
        public static string Report;
        public static string Usuario = Environment.UserName;

        public static List<listado> lstOrgs = new List<listado>();
        public static List<listado> lstDpto = new List<listado>();
        public static List<listado> lstContact = new List<listado>();
        public static List<listado> lstTypes = new List<listado>();
        public static List<listado> lstCoverage = new List<listado>();
        public static List<listado> lstStatus = new List<listado>();
        public static List<listado> lstEfficacy = new List<listado>();
        public static List<listado> lstContacts = new List<listado>();
        public static List<listado> lstResponsibles = new List<listado>();
        public static List<listado> lstEffOwner = new List<listado>();

        public static List<listado> Documents = new List<listado>();
        public static bool bDocLoaded = false;

        public static string emailTo = "Tomas.Dale@concentrix.com";
        public void Sending(string Msg)
        {
            // SEND EMAIL

            SmtpClient client = null;
            MailMessage message = null;

            try
            {
                client = new SmtpClient("cvgmx1.convergys.com");
                client.UseDefaultCredentials = false;

                message = new MailMessage();
                message.From = new MailAddress(Variables.emailTo);
                message.Subject = "GPS ERROR - "+ Environment.UserName;
                message.Body = Msg;
                message.IsBodyHtml = true;

                string input = Variables.emailTo;
                string[] to = input.Split(';');

                foreach (var address in to)
                {
                    if (!string.IsNullOrEmpty(address)) message.To.Add(new MailAddress(address));
                }

                client.Send(message);
            }
            catch (SmtpException ex)
            {
                //log.Error(ex.ToString());
                Variables SEND = new Variables();
                SEND.Sending(ex.ToString());
            }
            catch (Exception ex)
            {
                Variables SEND = new Variables();
                SEND.Sending(Environment.UserName+ " "+ ex.ToString());
                //log.Error(ex.ToString());
            }
            finally
            {
                if (message != null) message.Dispose();
                if (client != null) client.Dispose();
            }
            // regis, loaded
        }

        public static Boolean LoadCodes(SqlConnection conn, int opcion)
        {
            System.Data.IDataReader dr = null;
            SqlCommand comando = new SqlCommand();

            if (opcion == 1) comando.CommandText = "select id, EfficacyOwner      from tsEfficacyOwner order by EfficacyOwner";
            if (opcion == 2) comando.CommandText = "select id, title              from tsResponsables order by title";
            if (opcion == 3) comando.CommandText = "select id, description, code  from tsDepartments order by description";
            if (opcion == 4) comando.CommandText = "select id, name               from tsContacts order by name ";
            if (opcion == 5) comando.CommandText = "select *                      from tsDocStatus";
            if (opcion == 6) comando.CommandText = "select *                      from tsEfficacyStatus";
            if (opcion == 7) comando.CommandText = "select id, org_descr, orgcode from tsOrgList order by org_descr";
            if (opcion == 8) comando.CommandText = "select id, DocType, DocCode   from tsDocumentType order by DOcType";
            if (opcion == 9) comando.CommandText = "select id, CoverageDescr, CoverageCode from tsCoverage order by CoverageDescr";

            try
            {
                comando.Connection = conn;
                dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    string ss= dr[1].ToString();
                    if (opcion == 1) Variables.lstEffOwner.Add(new listado() { Value = System.Convert.ToInt32(dr[0].ToString()), Text = dr[1].ToString() }); ;
                    if (opcion == 2) Variables.lstResponsibles.Add(new listado() { Value = System.Convert.ToInt32(dr[0].ToString()), Text = dr[1].ToString() }); ;

                    if (opcion == 3)
                    {
                        Variables.lstDpto.Add(new listado()
                        {
                            Value = System.Convert.ToInt32(dr[0].ToString()),
                            Text = dr[1].ToString(),
                            Code = dr[2].ToString()
                        });
                    }

                    if (opcion == 4) Variables.lstContacts.Add(new listado() { Value = System.Convert.ToInt32(dr[0].ToString()), Text = dr[1].ToString() });
                    if (opcion == 5) Variables.lstStatus.Add(new listado() { Value = System.Convert.ToInt32(dr[0].ToString()), Text = dr[1].ToString() }); ;
                    if (opcion == 6) Variables.lstEfficacy.Add(new listado() { Value = System.Convert.ToInt32(dr[0].ToString()), Text = dr[1].ToString() }); ;
                    if (opcion == 7)
                    {
                        Variables.lstOrgs.Add(new listado()
                        {
                            Value = System.Convert.ToInt32(dr[0].ToString()),
                            Text = dr[1].ToString(),
                            Code = dr[2].ToString()
                        });
                    }

                    if (opcion == 8)
                    {
                        Variables.lstTypes.Add(new listado()
                        {
                            Value = System.Convert.ToInt32(dr[0].ToString()),
                            Text = dr[1].ToString(),
                            Code = dr[2].ToString()
                        }); ;
                    }

                    if (opcion == 9)
                    {
                        Variables.lstCoverage.Add(new listado()
                        {
                            Value = System.Convert.ToInt32(dr[0].ToString()),
                            Text = dr[1].ToString(),
                            Code = dr[2].ToString()
                        }); ;
                    }
                }
            }
            catch (Exception es)
            {
                Variables SEND = new Variables();
                SEND.Sending("Error when loading " + comando.CommandText + " " + es.ToString());
                MessageBox.Show("Error when loading data, " + comando.CommandText);
                return false;
            }
            finally
            {
                if ((dr != null) && (!dr.IsClosed))
                    dr.Close();
            }
            return true;
        }


        public static DataTable GetDT(string sql)
        {
            SqlConnection conn = new SqlConnection(Variables.StrConn);
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
            catch (Exception ex)
            {
                Variables SEND = new Variables();
                SEND.Sending(ex.ToString());
                string es = ex.ToString();
                MessageBox.Show(es);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public static List<listado> GetDocuments(string sql)
        {
            SqlConnection conn = new SqlConnection(Variables.StrConn);
            List<listado> lista = new List<listado>();

            try
            {
                conn.Open();
                SqlCommand comando = new SqlCommand(sql, conn);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comando;

                DataTable dt11 = new DataTable();
                dataAdapter.Fill(dt11);
                foreach (DataRow dr in dt11.Rows)
                {
                    lista.Add(new listado()
                    {

                        Value = System.Convert.ToInt32(dr[0].ToString()),
                        Code = dr[1].ToString().Trim(),
                        Text = dr[2].ToString()
                    }); ;

                    //string nombre = dr[0].ToString();
                    //listBox7.Items.Add(nombre);
                }
            }
            catch (Exception ex)
            {
                Variables SEND = new Variables();
                SEND.Sending(ex.ToString());
                string es = ex.ToString();
                MessageBox.Show(es);
            }
            finally
            {
                conn.Close();
            }
            return lista;

        }

        public static DataTable ToDatatable(List<listado> list, string texto)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            dt.Columns.Add("Code");

            if (texto != "")
            {
                dt.Rows.Add("0", texto);
            }

            foreach (var item in list)
            {
                dt.Rows.Add(item.Value, Convert.ToString(item.Text));
            }
            return dt;
        }
    }
}
