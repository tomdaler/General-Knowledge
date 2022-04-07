using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MercadoLibre
{
    //  https://orliwv024.na.convergys.com/Exposure/Main.aspx
    //http://admin.convergys.com/Meli/Meli.aspx

    // VPN fortclient  brspbf41todbp1p/Meli/Meli.aspx

    public partial class MeLi : System.Web.UI.Page
    {
        bool Loading = false;
        string sql1 = "";
        string where1 = "";
        int BtnChat = 7;
        string SqlAgents = "SELECT t2.LOB as LobName, t4.Name as TLName, t3.* from ML_LOBs t2, ML_Agents t3, ML_TLs t4 where t3.TL=t4.id AND t4.LOB = t2.ID ";

        #region INICIO
        protected void Page_Load(object sender, EventArgs e)
        {

            Loading = true;

            if (!Page.IsPostBack)
            {
                LoadList();

                Functions fx = new Functions();
                string Login = HttpContext.Current.Request.LogonUserIdentity.Name; // sa\\tdal6392

                if (Login != "SA\\tdal6392")
                {
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
                }

                Session["sort"] = " ORDER BY DateCreated DESC ";
                Session["sql"] = sql1 + where1;

            }

            Loading = false;
        }

        private void LoadByLOB(string where)
        {
            Functions fx = new Functions();
            if (where.Trim() != "") where = "where id in " + where;
            string sql = "select LOB_Name as LOB, TotAgents, Online, Offline, Descanso, Preso, Doente, Feriado, ";
            sql = sql + " Fechados From ML_LOBs_Daily " + where;
            DataTable dtLOB = fx.GetDataTable(sql);

            Loading = false;

            if (dtLOB == null) return;

            //dg.DataSource = dtLOB;
            //dg.DataBind();

            DataTable dtLOB2 = Totales(dtLOB);
            int ss = dtLOB2.Rows.Count;

            DataTable dtLOB3 = fx.Transpose(dtLOB2);
            GV1.DataSource = dtLOB3;
            GV1.DataBind();
            GV1.Visible = true;
        }

        private void LoadList()
        {
            if (ddlLOB.Items.Count > 1) return;
            Functions fx = new Functions();

            string sql = "select id, LOB from ML_LOBs";
            ddlLOB.DataSource = fx.GetDataTable(sql); // it returns a DataTable
            ddlLOB.DataTextField = "LOB";
            ddlLOB.DataValueField = "id";
            ddlLOB.DataBind();


            sql = "select id,Name from ML_TLs order by name";
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


            // datepicker.Text = "01-01-2021";

            //foreach (DataRow dr in dtTL.Rows)
            //{
            //    dpTL.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            //}
        }

        public DataTable Totales(DataTable dt1)
        {
            int MaxCol = 9;
            int[] totales = new int[MaxCol];
            for (int i = 0; i < MaxCol; i++) totales[i] = 0;

            foreach (DataRow dr in dt1.Rows)
            {
                for (int j = 1; j < MaxCol; j++)
                    totales[j] = totales[j] + Convert.ToInt32(dr[j].ToString());

            }

            DataRow dr2 = dt1.NewRow();

            dr2[0] = "TOTALES";
            for (int k = 1; k < MaxCol; k++)
                dr2[k] = totales[k].ToString();


            dt1.Rows.Add(dr2);

            //Ativos.Text = tot2.ToString();
            //Fechado.Text = tot5.ToString();
            //Abandonado.Text = tot6.ToString();
            return dt1;
        }
        #endregion

        #region Sort

        protected void dg2_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt1 = (DataTable)Cache["Employee"];

            if (dt1 != null)
            {
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
        }

        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (Loading) return;

            string TL = hdTL.Value.ToString();
            string Status = hdStatus.Value.ToString();
            string Minutos = hdMinutos.Value.ToString();

            TL = ddlTL.SelectedValue.ToString();
            Status = ddlStatus.SelectedValue.ToString();

            bool All = true;
            string Selected = "(";
            string where1 = "";
            foreach (ListItem item in ddlLOB.Items)
            {
                if (item.Selected)
                {
                    where1 = where1 + Selected + item.Value.ToString();
                    Selected = ",";
                }
                else
                    All = false;
            }

            where1 = where1 + ")";
            string msg = "";

            if (where1 == ")" && TL == "" && Status == "")
            {
                msg = "<script>alert('Seleccione alguna condicion para continuar');</script>";
                Response.Write(msg);
                return;
            }

            string sql = "SELECT t3.nome, t2.LOB as LobName, t4.TL, t1.* ";

            sql = sql + " FROM ML_Agents_Daily t1, ML_LOBs t2, ML_Agents t3, ML_TLs t4 ";
            sql = sql + " WHERE t1.LOB = t2.id ";
            sql = sql + " AND t1.EmpID = t3.Empid ";
            sql = sql + " AND t1.TL = t4.id ";

            //====

            ////sql = "select t2.LOB as LobName, t4.TL, t3.* ";
            ////sql = sql + " from ML_LOBs t2, ML_Agents t3, ML_TLs t4 ";
            ////sql = sql + " where t4.LOB = t2.ID";



            sql = SqlAgents;

            if (TL != "") sql = sql + " AND t4.id='" + TL + "' ";
            if (where1 != ")") sql = sql + " AND T2.id in " + where1;
            if (Status != "") sql = sql + " and t1.status ='" + Status + "' ";

            if (today.Checked)
            {
                string hoy = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                //    sql = sql + " AND CONVERT(DATETIME, CONVERT(VARCHAR, lastupdate, 112)) > '" + hoy + "'";
                string dia = "'"+DateTime.Now.Day.ToString()+ " -%'";
                sql = sql + " AND LastUpdate like "+dia;
            }

            if (Minutos != "")
            {
                sql = sql + " and (chat1>=" + Minutos + " or chat2>=" + Minutos + " or chat3>=" + Minutos + ") ";
            }
            LoadByTL(sql);

            //msg = "<script>alert('" + sql + "');</script>";
            //Response.Write(msg);


            if (where1 == ")") return;

            if (All) where1 = "";
            Session["LOB"] = where1;
            LoadByLOB(where1);
        }

        string GetAgents(string LOB, string TL)
        {
            string sql = SqlAgents;

            if (LOB != "") sql = sql + " AND t2.LOB ='" + LOB + "'";
            if (TL != "") sql = sql + " AND t4.id ='" + TL + "'";

            // CAMBIAR
            Functions FX = new Functions();
            //sql = sql + " AND CREATED =" + FX.TODAY(2);

            return sql;
        }

        protected void LoadByTL(string sql)
        {
            //string script = "window.onload = function() { Esconder(); };";
            //ClientScript.RegisterStartupScript(this.GetType(), "YourJavaScriptFunctionName", script, true);

            //lbLOB.Visible = false;

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

            LastRow();
        }

        protected void GV1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GV1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string LOB = e.SortExpression;

            string sql = GetAgents(LOB, "");

            //lbLOB.Text = "LOB : " + LOB;
            //lbLOB.Visible = true;

            Functions fx = new Functions();
            DataTable dtEmployee = fx.GetDataTable(sql);
            if (dtEmployee == null)
            {
                dg2.Visible = false;
                return;
            }

            Cache.Insert("Employee", dtEmployee, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 360000, 0));

            dg2.DataSource = dtEmployee;
            dg2.DataBind();
            dg2.Visible = true;

            LastRow();

            string script = "window.onload = function() { Mostrar(); };";
            ClientScript.RegisterStartupScript(this.GetType(), "YourJavaScriptFunctionName", script, true);

        }

        protected void LastRow()
        {
            int rows = dg2.Rows.Count - 1;
            if (rows < 0) return;
            string estado = dg2.Rows[rows].Cells[3].Text;
            if (estado == "Online") dg2.Rows[rows].Cells[1].ForeColor = System.Drawing.Color.Green;
            if (estado == "Offline") dg2.Rows[rows].Cells[1].ForeColor = System.Drawing.Color.Red;
            if (estado == "Pause") dg2.Rows[rows].Cells[1].ForeColor = System.Drawing.Color.Orange;

            for (int i = 1; i < 4; i++)
            {
                int pos1 = BtnChat + i - 1;
                string btn = "Button" + i.ToString();
                Button ss = (Button)dg2.Rows[rows].Cells[pos1].FindControl(btn);
                string chatid = ss.Text.Trim();

                if (chatid == "" || chatid == "0")
                    dg2.Rows[rows].Cells[pos1].Visible = false;
                else
                    dg2.Rows[rows].Cells[pos1].Visible = true;
            }

        }

        protected void GV1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void dg2_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            string comando = e.CommandName;
            if (comando != "Actions" && comando != "Chat1" && comando != "Chat2" && comando != "Chat3") return;

            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string ID = dg2.DataKeys[index].Value.ToString();

                string machine = System.Environment.MachineName;
                string hostname = "'http://BRSPBF4ITODBP1P/Meli/";

                if (machine == "SSLTDALELT") hostname = "'http://localhost:58825/";

                string msg = "";

                if (comando == "Actions")
                    msg = "<script>window.open(" + hostname + "Actions.aspx?id=" + ID + "', '_blank','toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes');</script>";

                if (comando.IndexOf("Chat") > -1)
                {
                    string sql = "select ChatID1,ChatID2,ChatID3 from ML_Agents where empid=" + ID;
                    Functions fx = new Functions();
                    DataTable dt = fx.GetDataTable(sql);
                    if (dt == null) return;
                    string chatNo = "";
                    if (comando == "Chat1") chatNo = dt.Rows[0]["ChatID1"].ToString();
                    if (comando == "Chat2") chatNo = dt.Rows[0]["ChatID2"].ToString();
                    if (comando == "Chat3") chatNo = dt.Rows[0]["ChatID3"].ToString();
                    if (chatNo == "") return;

                    //chatNo = "139274688";
                    msg = "https://administrator-webchat.adminml.com/supervisor/loadTranscript/"+chatNo;
                    msg = "<script>window.open('" + msg+"', '_blank','toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes');</script>";

                }

                if (comando == "Actions")
                    msg = "<script>window.open(" + hostname + "Actions.aspx?id=" + ID + "', '_blank','toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes');</script>";

                
                ClientScript.RegisterStartupScript(GetType(), "test", msg);
            }
            catch (Exception es) { string ss = es.ToString(); return; }
        }

        protected void dg2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
                {
                    int rows = e.Row.RowIndex - 1;
                    if (rows < 0) return;

                    string status = dg2.Rows[rows].Cells[3].Text.ToString();

                    if (status == "Online") dg2.Rows[rows].Cells[1].ForeColor = System.Drawing.Color.Green;
                    if (status == "Offline") dg2.Rows[rows].Cells[1].ForeColor = System.Drawing.Color.Red;
                    if (status == "Pause") dg2.Rows[rows].Cells[1].ForeColor = System.Drawing.Color.Orange;

                    for (int i = 1; i < 4; i++)
                    {
                        int pos1 = BtnChat + i - 1;
                        string btn = "Button" + i.ToString();
                        Button ss = (Button)dg2.Rows[rows].Cells[pos1].FindControl(btn);
                        string chatid = ss.Text.Trim();

                        if (chatid == "" || chatid == "0")
                            dg2.Rows[rows].Cells[pos1].Visible = false;
                        else
                            dg2.Rows[rows].Cells[pos1].Visible = true;
                    }
                }
            }
            catch (Exception) { }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string msg = "https://administrator-webchat.adminml.com/supervisor/loadTranscript/";
            string CaseNo = txChat.Text.Trim();
            //CaseNo = "121586814";
            msg = "<script>window.open('" + msg + CaseNo + "'";
            msg = msg + ", '_blank');</script>";
            Response.Write(msg);
        }
    }
}