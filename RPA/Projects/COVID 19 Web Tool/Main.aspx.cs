using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WebApplication1
{
    //  https://orliwv024.na.convergys.com/Exposure/Main.aspx
    //http://admin.convergys.com/COVID19CaseReport/Main.aspx
    public partial class Main1 : System.Web.UI.Page
    {
        int col_view = 19;

        bool Loading = false;
        bool TODAY1 = true;
        string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

        string sql1 = @"SELECT TOP 1000 ext1, FTW, extension, SWAB, SQ, Assessment, InitialSwab, RTPCR, MC,  [ID], '' as VS, [DateCreated], RepType, Email, EmpID, FirstName, LastName, MiddleName, EmpLocation, EmpProgram, DOH, Recommendation, TestType, Status, quarantineStartDate, quarantineEndDate, dtExtEnd, DateModified, dtFTW, LGULetter FROM  DeclarationForms  ";
        static string where1 = " WHERE DateCreated > dateadd(dd,-120,cast(getdate() as date)) ";
        static string where2 = where1;

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
                if (userID[i].ToUpper() == Login.ToUpper()) User = userName[i];
            }
            return User;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Loading = true;

            if (!Page.IsPostBack)
            {
                string user = GetAuthorization();
                
                if (user != "")
                    Label3.Text = "Welcome, " + user;
                else
                {
                    string Login = HttpContext.Current.Request.LogonUserIdentity.Name; // sa\\tdal6392
                    Label3.Text = "Welcome, " + Login;

                    Response.Write("<script>alert('You Are Not Authorized');</script>");
                    dg.Visible = false;
                    Button4.Visible = false;
                    BtLast.Visible = false;
                    return;
                }
                
                Session["sort"] = " ORDER BY DateCreated DESC ";
                Session["sql"] = sql1 + where1;

                LoadInfo();
            }

            Loading = false;
        }

        private void LoadInfo()
        {
            string sql = "";
            bool Retrieve = false;
            try
            {
                sql = Session["sql"].ToString();
                if (sql == "" || sql == null) return; // Retrieve = true;
            }
            catch (Exception)
            {
                Retrieve = true;
            }

            if (Retrieve)
            {
                Session["sql"] = sql1 + where1;
                Session["sort"] = " ORDER BY DateCreated DESC ";
                sql = sql1;
                // RetrieveUser();
            }

            SqlConnection sqlcon = new SqlConnection(SqlConn);
            sqlcon.Open();
          
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlcon;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql + " " + Session["sort"].ToString();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable employees = new DataTable();
            adapter.Fill(employees);

            sqlcon.Close();

            Loading = true;
            Cache.Insert("GridData", employees, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 360000, 0));

            Common fx = new Common();
            fx.LoadVaccineStatus(employees,false);
         

            dg.DataSource = employees;
            dg.DataBind();

            if (employees.Rows.Count > 0)
            {
                dg.Visible = true;
                NoData.Visible = false;
            }
            else
            {
                try
                {
                    //Response.Write("<script language=javascript>alert('There is no Data');</script>");
                }
                catch (Exception)
                { }
                dg.Visible = false;
                NoData.Visible = true;
            }

            Loading = false;
        }

        protected void LoadVaccineStatus(DataTable dt)
        {
            string list1 = "";
            string comma = "";
            foreach (DataRow row in dt.Rows)
            {
                list1 = list1 + comma+ "'" + ((string)row["EmpID"]) + "'";
                comma = ",";
            }

            string sql = "select EmpID, VaccinationStatus  FROM  DeclarationFormsVaccinationStatus  WHERE Empid in (" + list1 + ")";

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

                Cache.Insert("VaccineStatus", dtVaccine, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 360000, 0));
            }
            catch (Exception es) {
                dtVaccine = null;
                string es2 = es.ToString();
                int i = 1; }

            sqlcon.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string place = Place2();
            Exposure.Program rep = new Exposure.Program();
            if (!rep.Generate(1, place, "", "", TODAY1))
            {
                Response.Write("<script language=javascript>alert('No Rows to Retrieve');</script>");
                return;
            }

            string exportar = "temp\\file1.xlsx";
            Response.Redirect(exportar);
        }

        protected string Place()
        {
            string pageurl = System.Environment.MachineName + "/temp/file1.xlsx";

            pageurl = pageurl.Replace("ORLIWV021/", "/EXPOSURE/");
            pageurl = pageurl.Replace("ORLIWV024/", "/EXPOSURE/");

            if (pageurl == "SSL0C03ADM3252/temp/file1.xlsx")
                pageurl = @"C:\Proyectos\Exposure\WebApplication1\Temp\file1.xlsx";

            return pageurl;
        }
               
        protected string Place2()
        {
            string pageurl = System.Environment.MachineName;
            pageurl = @"C:\Proyectos\Exposure\WebApplication1\Temp\file1.xlsx";

            if (Directory.Exists("E:\\WebApps")) //024 dev
                pageurl = "E:\\WebApps\\Exposure\\TEMP\\file1.xlsx";

            if (Directory.Exists("D:\\Web"))  // 021
                pageurl = "D:\\Web\\Exposure\\TEMP\\file1.xlsx";

            return pageurl;
        }

        protected void Show()
        {
            string pageurl = Place();

            if (pageurl.Substring(0, 2) == "C:")
                pageurl = "http://localhost:59263/temp/file1.xlsx";

            string script = "window.open('" + pageurl + "','_blank', 'toolbar=yes,scrollbars=yes' ); ";
            ScriptManager.RegisterStartupScript(this, GetType(), pageurl, script, true);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string place = Place2();
            Exposure.Program rep = new Exposure.Program();
            if (!rep.Generate(2, place, "", "", TODAY1))
            {
                Response.Write("<script language=javascript>alert('No Rows to Retrieve');</script>");
                return;
            }

            string exportar = "temp\\file1.xlsx";
            Response.Redirect(exportar);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string place = Place2();
            Exposure.Program rep = new Exposure.Program();
            if (!rep.Generate(3, place, "", "", TODAY1))
            {
                Response.Write("<script language=javascript>alert('No Rows to Retrieve');</script>");
                return;
            }

            string exportar = "temp\\file1.xlsx";
            Response.Redirect(exportar);
        }

        protected void dg_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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

            if (e.CommandName == "Detail")
            {
                PopUp(ID);
                return;
            }

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

                SqlConnection sqlcon = new SqlConnection(SqlConn);
                sqlcon.Open();

                string sql = "SELECT File1, ext1, File2, ext2, File3, ext3, ";
                sql = sql + " FTW, EXTENSION, SWAB, SQ, Assessment, InitialSwab, RTPCR, MC, LGULetter ";
                sql = sql + " FROM declarationForms  where ID = " + ID;

                using (SqlCommand cmd = new SqlCommand(sql, sqlcon))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.Default))
                    {
                        if (dr.Read())
                        {
                            int j = 0;
                            for (int i = 1; i < 4; i++)
                            {
                                try
                                {
                                    byte[] fileData = (byte[])dr.GetValue(j++);
                                    if (fileData == null) continue;

                                    string ext = dr.GetString(j++);
                                    if (ext == "" || ext == null) continue;

                                    string fileName = "report" + i.ToString() + "." + ext;
                                    ViewDoc(fileData, fileName);
                                }
                                catch (Exception es)
                                {
                                    string ss = es.ToString();
                                    continue;
                                }
                            }

                            ShowFile2(dr.GetValue(6), "FTW.pdf");
                            ShowFile2(dr.GetValue(7), "MCE.pdf");
                            ShowFile2(dr.GetValue(8), "MCS.pdf");
                            ShowFile2(dr.GetValue(9), "SQ.pdf");

                            ShowFile2(dr.GetValue(10), "Assessment.pdf");
                            ShowFile2(dr.GetValue(11), "InitialSwab.pdf");
                            ShowFile2(dr.GetValue(12), "RTPCR.pdf");
                            ShowFile2(dr.GetValue(13), "MC.pdf");
                            ShowFile2(dr.GetValue(14), "LGU.pdf");
                        }
                    }
                }

                sqlcon.Close();
            }
        }

        protected void ShowFile2(object doc1, string FileName)
        {
            if (doc1 is null || doc1.ToString() == "") return;
            try
            {
                Byte[] doc = (byte[])doc1;
                ViewDoc(doc, FileName);
            }
            catch (Exception) { }
        }

        protected void ShowFile(Byte[] doc, string FileName)
        {
            
            if (doc is null || doc.ToString() == "") return;
            try
            {
                ViewDoc(doc, FileName);
            }
            catch (Exception) { }
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

        protected void dg_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

            if (Loading) return;
            dg.PageIndex = e.NewPageIndex;
            LoadInfo();
        }

        protected void BtSearch_Click(object sender, EventArgs e)
        {
            // String test = MSG.Value; //  Request.Form["MSG"].ToString();
            // string getTextValues = Request.Form["MSG"].ToString();

            string where = Hidden1.Value.ToString().Trim();

            string sql = sql1;
            Session["sort"] = " ORDER BY DateCreated DESC";

            if (where == "")
            {
                sql = sql + where1;
            }
            else
            {
                if (where != "All")
                   sql = sql + where2 + " AND status = '" + where + "' ";
                else
                   sql = sql + where2;
                
            }

            string search = TextBox1.Text;
            string empid = Regex.Replace(search, @"[^\d-+()]", "");

            if (empid.Length > 5)
                sql = sql + " AND EmpID='" + empid + "' ";
            else
            {
                if (search != "")
                {
                    sql = sql + " AND FirstName like '%" + TextBox1.Text + "%' ";
                    sql = sql + " OR LastName like '%" + TextBox1.Text + "%' ";
                    sql = sql + " OR MiddleName like '%" + TextBox1.Text + "%' ";
                }
            }

            
            HttpContext.Current.Session["sql"] = sql;
            LoadInfo();
        }

        protected void BtNewRep_Click(object sender, EventArgs e)
        {
            string From = HF1.Value;
            string To = HF2.Value;

            if (From == "") From = To;
            if (To == "") To = From;

            if (From != "")
            {
                DateTime fec = Convert.ToDateTime(From);
                From = fec.ToString("yyyy-MM-dd") + " 00:00";
            }

            if (To != "")
            {
                DateTime fec = Convert.ToDateTime(To);
                To = fec.ToString("yyyy-MM-dd") + " 23:59";
            }
            string place = Place2();
            //DateTime fec = DateTime.Now.AddDays(1);
            //string file1 = fec.ToString("MM-dd-yyyy");

            Exposure.Program rep = new Exposure.Program();
            string hoy = rep.Today();
            if (To != "") hoy = HF2.Value;
            hoy = hoy.Replace("/", "-");

            string file1 = "COVID19 Case Report - " + hoy;
            place = place.Replace("file1", file1);

            if (!rep.Generate(15, place, From, To, TODAY1))
            {
                Response.Write("<script language=javascript>alert('No Rows to Retrieve');</script>");
                return;
            }
            string exportar = "temp\\" + file1 + ".xlsx";
            Response.Redirect(exportar);
        }

        protected void dg_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            DataTable dataTable = (DataTable)Cache["GridData"];

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                string Field1 = e.SortExpression;
                string whichWay = "ASC";
                if (HttpContext.Current.Session[Field1] != null)
                {
                    whichWay = HttpContext.Current.Session[Field1].ToString();
                    if (whichWay == "ASC")
                        whichWay = "DESC";
                    else
                        whichWay = "ASC";
                }

                HttpContext.Current.Session[Field1] = whichWay;
                dataView.Sort = Field1 + " " + whichWay;
                dg.DataSource = dataView;
                dg.DataBind();
                Session["sort"] = " ORDER BY " + Field1 + " " + whichWay;
            }
        }

        protected void dg_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            //  e.Row.Height = Unit.Pixel(15);
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                string key = dg.DataKeys[e.Row.RowIndex].Values[0].ToString();
                DataTable dt2 = (DataTable)Cache["GridData"];
                string where = "ID=" + key;
                DataRow[] result = dt2.Select(where);

                // FILES
                if (result.Length > 0)
                {
                    // VACINE STATUS
                    Common fx = new Common();
                    string EmpId = result[0]["EmpID"].ToString();
                    e.Row.Cells[11].Text = fx.Vaccination(EmpId);

                    string ext1 = result[0][0].ToString();
                    string FTW  = result[0][1].ToString();
                    string EXTENSION = result[0][2].ToString();
                    string SWAB      = result[0][3].ToString();
                                        
                    string SQ = result[0][4].ToString();
                    string Assessment = result[0][5].ToString();
                    string InitialSwab = result[0][6].ToString();
                    string RTPCR = result[0][7].ToString();
                    string MC = result[0][8].ToString();
                    string LGU = result[0][28].ToString();


                    if ((ext1 != null && ext1.Trim() != "")
                        || 
                        (FTW != null && FTW.Trim()!="")
                         ||
                        (EXTENSION != null && EXTENSION.Trim() != "")
                         ||
                        SWAB != "" || SQ.Trim() != ""  || Assessment != "" ||
                        InitialSwab != "" || RTPCR != "" || MC != "" 
                        ||  LGU.Trim()!= "")
                        
                    {

                        try
                        {
                            e.Row.Cells[col_view].Controls[0].Visible = true;
                            foreach (System.Web.UI.Control ctrl in e.Row.Cells[col_view].Controls)
                            {
                                if (ctrl.GetType().BaseType == typeof(ImageButton))
                                    ((ImageButton)ctrl).ToolTip = "View Attachments";
                            }
                        }
                        catch (Exception) { }
                    }
                    else
                    {
                        try
                        {
                            e.Row.Cells[col_view].Controls[0].Visible = false;
                        }
                        catch (Exception) { }
                    }
                }
            }
        }
    }
}