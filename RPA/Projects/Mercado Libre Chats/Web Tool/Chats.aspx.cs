using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MercadoLibre
{
    public partial class Chats : System.Web.UI.Page
    {
        bool Loading = false;
        string sql1 = "";
        string where1 = "";

        #region inicio
        protected void Page_Load(object sender, EventArgs e)
        {

            Loading = true;

            if (!Page.IsPostBack)
            {
                LoadList();

                Functions fx = new Functions();
                string Login = HttpContext.Current.Request.LogonUserIdentity.Name; // sa\\tdal6392

                string user = fx.GetAccess(Login);

                if (user != "")
                    Label3.Text = "Bem-vindo, " + user;
                else
                {
                    Label3.Text = "Bem-vindo, " + Login;

                    Login = Login.Replace("\\", ".");
                    //Login = Login.Replace(".", "\\");

                    string msg = "<script>alert('User " + Login + " Is Not Authorized');</script>";
                    Response.Write(msg);

                    string script = "window.opener = 'Self';window.open('','_parent',''); window.close();";
                    ScriptManager.RegisterStartupScript(Page, typeof(string), "Close Window", script, true);
                }

                Session["sort"] = " ORDER BY DateCreated DESC ";
                Session["sql"] = sql1 + where1;

            }

            Loading = false;
        }

        protected void LoadList()
        {
            Functions fx = new Functions();
            string sql = "select id,Name from ML_TLs order by name";
            DataTable dtTL = fx.GetDataTable(sql);
            ddlTL.DataSource = dtTL;
            ddlTL.DataTextField = "Name";
            ddlTL.DataValueField = "id";
            ddlTL.DataBind();
            ddlTL.Items.Insert(0, new ListItem("Todos", ""));
            ddlTL.Items[0].Selected = true;


            ddlStatus.Items.Insert(0, new ListItem("Todos", ""));
            ddlStatus.Items.Insert(1, new ListItem("Online", "online"));
            ddlStatus.Items.Insert(2, new ListItem("Offline", "offline"));
            ddlStatus.Items.Insert(3, new ListItem("Descanso", "descanso"));
            ddlStatus.Items.Insert(4, new ListItem("Training KM", "training_km"));
            ddlStatus.Items.Insert(5, new ListItem("Help Low", "help_low_priority"));
            ddlStatus.Items.Insert(6, new ListItem("Evento", "evento"));
            ddlStatus.Items.Insert(7, new ListItem("Post Chat", "post_chat"));
            ddlStatus.Items.Insert(8, new ListItem("Help Excl.", "help_exclusive"));
            ddlStatus.Items.Insert(9, new ListItem("Coaching", "coaching"));
            ddlStatus.Items.Insert(10, new ListItem("Training AC", "training_ac"));
            ddlStatus.Items.Insert(11, new ListItem("Expert Tr.", "expert_trainer"));
            ddlStatus.Items[0].Selected = true;

            ddlChat.Items.Insert(0, new ListItem("Todos", ""));
            ddlChat.Items.Insert(1, new ListItem("Mais de 1 minuto", "1"));
            ddlChat.Items.Insert(2, new ListItem("Mais de 2 minutos", "2"));
            ddlChat.Items.Insert(3, new ListItem("Mais de 3 minutos", "3"));
            ddlChat.Items.Insert(4, new ListItem("Mais de 4 minutos", "4"));
            ddlChat.Items.Insert(5, new ListItem("Mais de 5 minutos", "5"));
            ddlChat.Items.Insert(6, new ListItem("Mais de 6 minutos", "6"));
            ddlChat.Items[0].Selected = true;

        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            string tl = ddlTL.SelectedValue.ToString();
            string st = ddlStatus.SelectedValue.ToString();
            string min = ddlChat.SelectedValue.ToString();

            string sql = "SELECT t2.Name as TL2, t1.*  FROM[MeLi].[dbo].[ML_Agents] t1, ML_TLs t2  where t1.TL = t2.id ";
            if (tl != "") sql = sql + " and t1.TL=" + tl;
            if (st != "") sql = sql + " and t1.status2='" + st + "'";
            if (min!="") sql = sql + " and (chat1>="+min+" or chat2>="+ min + " or chat3>=" + min + ") ";

            Functions fx = new Functions();
            DataTable dtEmployee = fx.GetDataTable2(sql);
            if (dtEmployee == null)
            {
                dg2.Visible = false;
                return;
            }

            Cache.Insert("Employee", dtEmployee, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 360000, 0));

            dg2.DataSource = dtEmployee;
            dg2.DataBind();
            dg2.Visible = true;


        }

        protected void dg2_Sorting(object sender, GridViewSortEventArgs e)
        { 
            DataTable dt1 = (DataTable)Cache["Employee"];

            DataView dataView = new DataView(dt1);
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
            dg2.DataSource = dataView;
            dg2.DataBind();
        }


        void ViewChat(string chatID)
        {
            string msg = "https://administrator-webchat.adminml.com/supervisor/loadTranscript/";
            //string chatID = "121586814";
            msg = "<script>window.open('" + msg + chatID + "'";
            msg = msg + ", '_blank');</script>";
            Response.Write(msg);
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            

        }

        protected void dg2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Loading) return;
            if (e.CommandArgument.ToString() == "") return;

            string chat = e.CommandName;
            if (chat != "ChatID1" && chat != "ChatID2" && chat != "ChatID3") return;

            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string LogName = dg2.DataKeys[index].Value.ToString();

                DataTable dt2 = (DataTable)Cache["Employee"];
                string where = "LoginName='" + LogName + "'";
                DataRow[] result = dt2.Select(where);
                if (result.Length == 0) return;

                string chatid = result[0][chat].ToString();
                if (chatid != "" && chatid != "0") ViewChat(chatid);
            }
            catch (Exception) { }
        }
    }
}