using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace MercadoLibre
{
    public partial class Actions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Functions fx = new Functions();
            string Login = HttpContext.Current.Request.LogonUserIdentity.Name; // sa\\tdal6392
       
            if (Login != "SA\\tdal6392")
            {
                string user = fx.GetAccess(Login);
                if (user != "")
                    Label3.Text = "Welcome, " + user;
                else
                {
                    Label3.Text = "Welcome, " + Login;

                    Login = Login.Replace("\\", ".");
                    //Login = Login.Replace(".", "\\");

                    string msg = "<script>alert('User " + Login + " Is Not Authorized');</script>";
                    Response.Write(msg);

                    string script = "window.opener = 'Self';window.open('','_parent',''); window.close();";
                    ScriptManager.RegisterStartupScript(Page, typeof(string), "Close Window", script, true);
                }
            }
            if (Page.IsPostBack) return;

            string sql = "select id,Name from ML_TLs order by name";
            DataTable dtTL = fx.GetDataTable(sql);
            ddlTL.DataSource = dtTL;
            ddlTL.DataTextField = "Name";
            ddlTL.DataValueField = "id";
            ddlTL.DataBind();
            ddlTL.Items.Insert(0, new ListItem("", ""));
            ddlTL.Items[0].Selected = true;


            ddlStatus.Items.Insert(0, new ListItem("", ""));
            ddlStatus.Items.Insert(1, new ListItem("Online", "Online"));
            ddlStatus.Items.Insert(2, new ListItem("Offline", "Offline"));
            ddlStatus.Items.Insert(3, new ListItem("Descanso", "Descanso"));
            ddlStatus.Items.Insert(4, new ListItem("Preso", "Preso"));
            ddlStatus.Items.Insert(5, new ListItem("Doente", "Doente"));
            ddlStatus.Items.Insert(6, new ListItem("Feriado", "Feriado"));


            string id = Request.QueryString["id"];

            sql = "Select top(100) * from ML_Tracker  "; 
            sql = sql + "where empid = " + id;
            //sql = sql + " and date1='" + fx.TODAY(2) + "' ";

            sql = sql + " order by created desc";

            DataTable dt1 = fx.GetDataTable(sql);

            //int i = 0;
            //foreach (DataRow dr in dt1.Rows)
            //{
            //    //string time1 = dr[5].ToString();
            //    //if (time1.Length > 5) dt1.Rows[i][5] = time1.Substring(0, 5);
            //    i++;
            //}

            //dt1.AcceptChanges();

            if (dt1 == null)
                GV1.Visible = false;
            else
            {
                GV1.DataSource = dt1;
                GV1.DataBind();
                GV1.Visible = true;
            }

            sql = "select t1.nome, t2.name, t1.status2 from ML_Agents t1, ML_TLs t2 where t1.TL = t2.id and EmpID = " + id;
            DataTable dt2 = fx.GetDataTable(sql);
            if (dt2 == null) return;

            string nome = dt2.Rows[0]["nome"].ToString();
            string TL = dt2.Rows[0]["name"].ToString();

            string status2 = "";
            try { status2 = dt2.Rows[0]["Status2"].ToString(); }
            catch (Exception) { }
            lbEmp.Text = "<b>Empleado</b> :" + nome + "&nbsp;&nbsp;&nbsp;&nbsp;</br></br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>TL</b> :" + TL + "</br></br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Status</b> :" + status2;
        }

        void LoadChats()
        {
            string sql = "  select top(100) * from ML_Chats";
            Functions fx = new Functions();
            DataTable dt2 = fx.GetDataTable(sql);
            if (dt2 == null) return;


        }

        protected void GV1_RowDataBound(object sender, GridViewRowEventArgs e)
        {



        }

        protected void GV1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // if (Loading) return;
            if (e.CommandName != "CaseNo") return;

            int index = 0;
            string CaseNo = "";

            try
            {
                index = Convert.ToInt32(e.CommandArgument);
                CaseNo = GV1.DataKeys[index].Value.ToString();
            }
            catch (Exception es) { string ss = es.ToString(); return; }

            CaseNo = CaseNo.Trim();
            if (CaseNo == "") return;

            string msg = "https://administrator-webchat.adminml.com/supervisor/loadTranscript/";
            CaseNo = "121586814";
            msg = "<script>window.open('" + msg + CaseNo + "'";
            msg = msg + ", '_blank');</script>";
            Response.Write(msg);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string TL = ddlTL.SelectedValue.ToString();
            string status = ddlStatus.SelectedValue.ToString();
            if (TL == "" && status == "") return;

            string wTL = "";
            string wStatus = "";
            if (TL != "") wTL = " TL =" + TL;
            if (status != "") wStatus = " status2='" + status + "'";

            List<string> lista = new List<string>();

            if (wTL != "") lista.Add(wTL);
            if (wStatus != "") lista.Add(wStatus);
            string set1 = string.Join(",", lista);

            string sql = "update ML_Agents SET " + set1;
            string id = Request.QueryString["id"];
            sql = sql + " WHERE EmpID=" + id;
            Functions fx = new Functions();
            fx.UpdateData(sql);

            string msg = "document.getElementById('updated').innerHTML ='Data Updated';";
            msg = "alert('Updated');";
            msg = "<script>" + msg + "</script>";
            Response.Write(msg);

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string msg = "document.getElementById('updated').innerHTML ='';";
            msg = "<script>" + msg + "</script>";
            Response.Write(msg);
        }

        protected void ddlTL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string msg = "document.getElementById('updated').innerHTML ='';";
            msg = "<script>" + msg + "</script>";
            Response.Write(msg);
        }
    }
}