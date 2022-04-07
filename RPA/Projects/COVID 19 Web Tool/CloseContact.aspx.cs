using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{

    public class Alerta
    {
        public static void Show(string message)
        {
            string cleanMessage = message.Replace("'", "\\'");
            string script = "<script type=\"text/javascript\">";
            script = script + " alert('" + cleanMessage + "'); ";
            script = script + " </script>";

            Page page = HttpContext.Current.CurrentHandler as Page;

            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(Alerta), "alert", script);
            }
        }
    }
       
    public partial class CloseContact : System.Web.UI.Page
    {
        int col_view = 13;
        bool Loading = false;

        private string GetAuthorization()
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

        protected string getValue2(object valor)
        {
            try
            {
                string valor2 = valor.ToString();
            }
            catch (Exception) { return ""; }
            return valor.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Alerta.Show("LOad");
            //return;

            string user = GetAuthorization();
            if (user == "")
            {
                Response.Write("<script>alert('You Are Not Authorized');</script>");
                Response.Write("<script>window.close();</script>");
                return;
            }

            // Alerta.Show("Este es un mensaje que llama a un pop up");

            string id = Request.QueryString["id"];
            Titulo.Text = "CONTACT TRACING LIST - INDEX CASE: "+ id;

           
            int pos1 = id.IndexOf(" ");
            id = id.Substring(0, pos1);
                       
            string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            SqlConnection sqlcon = new SqlConnection(SqlConn);
            sqlcon.Open();

            string sql = "SELECT CT_MedCert, ID, CT_DateCreated, CT_EmpID, CT_FirstName+ ' '+ CT_MiddleName+' '+ CT_LastName as 'EmpName',";
            sql = sql + "CT_ContactTracing, CT_DOH,";
            sql = sql + "CT_Recommendation,CT_QuarantineStartDate,CT_QuarantineEndDate, ";
            sql = sql + "CT_dtFTW, CT_Email, CT_dtCTA1Start, CT_dtCTA1End   ";
            sql = sql + " FROM declarationformsCT ";
            sql = sql + " where CT_WD_ID ='" + id+"' ";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable employees = new DataTable();
            adapter.Fill(employees);
            sqlcon.Close();

            if (employees.Rows.Count==0)
            {
                NoData.Visible = true;
                return;
            }

            NoData.Visible = false;
            Cache.Insert("GridData2", employees, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 360000, 0));
            dg.DataSource = employees;
            dg.DataBind();
        }

        protected void dg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Loading) return;

            int index = 0;
            string ID = "";

            try
            {
                index = Convert.ToInt32(e.CommandArgument);
                ID = dg.DataKeys[index].Value.ToString();
            }
            catch (Exception es) { string ss = es.ToString(); return; }


            if (e.CommandName == "ViewDoc")
            {
                for (int i = 1; i < 4; i++)
                {
                    try
                    {
                        string file1 = Server.MapPath(".\\Temp\\report" + i.ToString() + ".*");
                        System.IO.File.Delete(file1);
                    }
                    catch (Exception) { }
                }

                string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                using (SqlCommand cmd = new SqlCommand("select CT_MedCert From DeclarationFormsCT  where ID=" + ID, sqlcon))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.Default))
                    {
                        if (dr.Read())
                        {
                            try
                            {
                                byte[] fileData = (byte[])dr.GetValue(0);
                                if (fileData == null) return;

                                string fileName = "report1.pdf";
                                ViewDoc(fileData, fileName);
                            }
                            catch (Exception es)
                            {
                                string ss = es.ToString();
                            }
                        }
                    }
                }
                sqlcon.Close();
            }
        }

        protected void PopUp(string ID)
        {
            string msg = "<script>window.open('Detail.aspx?id=" + ID + "', '_blank','toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes');</script>";
            ClientScript.RegisterStartupScript(GetType(), "test", msg);
        }

        protected void ViewDoc(byte[] fileData, string FileName)
        {
            if (fileData.Length < 100) return;

            int posi = FileName.LastIndexOf(".");
            if (posi < 0) return;

            string ToSave = Server.MapPath(".\\Temp\\" + FileName);

            using (System.IO.FileStream fs = new System.IO.FileStream(ToSave, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
            {
                using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
                {
                    bw.Write(fileData);
                    bw.Close();

                    string mm = System.Environment.MachineName;
                    string pageurl = @"orliwv021.na.convergys.com/Exposure/temp/" + FileName;

                    if (mm == "SSL0C03ADM3252")
                        pageurl = "http://localhost:" + HttpContext.Current.Request.Url.Port + "/temp/" + FileName;
                    else
                        pageurl = System.Environment.MachineName + "/temp/" + FileName;

                    pageurl = pageurl.Replace("ORLIWV021/", "");
                    pageurl = pageurl.Replace("ORLIWV024/temp", "/Exposure/temp");

                    //pageurl = ToSave;

                    if (FileName.Contains(".doc"))
                    {
                        System.Threading.Thread.Sleep(500);
                        string ss = "var e = $.Event('keydown',{keyCode: 13});";
                        ScriptManager.RegisterStartupScript(this, GetType(), FileName, ss, true);
                    }

                    string script = "window.open('" + pageurl + "','_blank');";
                    ScriptManager.RegisterStartupScript(this, GetType(), FileName, script, true);
                }
            }
        }

        protected void dg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                string key = dg.DataKeys[e.Row.RowIndex].Values[0].ToString();
                DataTable dt2 = (DataTable)Cache["GridData2"];
                string where = "ID=" + key;
                DataRow[] result = dt2.Select(where);

                string vv = result[0][0].GetType().ToString();

                if (vv == "System.DBNull")
                    e.Row.Cells[col_view].Controls[0].Visible = false;
               else
                    e.Row.Cells[col_view].Controls[0].Visible = true;
            }
        }
    }
}