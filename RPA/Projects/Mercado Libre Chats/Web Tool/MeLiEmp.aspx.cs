using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MercadoLibre
{
    public partial class MeLiEmp : System.Web.UI.Page
    {
        bool Loading = false;
        protected void Page_Load(object sender, EventArgs e)
        {
     
            if (!Page.IsPostBack)
            {
                Loading = true;
                LoadCombos();

                Functions fx = new Functions();
                string Login = HttpContext.Current.Request.LogonUserIdentity.Name; // sa\\tdal6392
                string user = fx.GetAccess(Login);
               
                if (user != "")
                    Label3.Text = "Welcome, " + user;
                else
                {
                    Label3.Text = "Welcome, " + Login;
                    Login = Login.Replace("\\", ".");

                    string msg = "<script>alert('User " + Login + " Is Not Authorized');</script>";
                    Response.Write(msg);

                    string script = "window.opener = 'Self';window.open('','_parent',''); window.close();";
                    ScriptManager.RegisterStartupScript(Page, typeof(string), "Close Window", script, true);
                }
            }
            Loading = false;
        }

        private void LoadCombos()
        {
            if (ddlLOB.Items.Count > 1) return;
            Functions fx = new Functions();

            ddlLOB.Items.Insert(0, new ListItem("", ""));

            ddlTL.Items.Insert(0, new ListItem("", ""));
            ddlTL2.Items.Insert(0, new ListItem("", ""));

            ddlStatus.Items.Insert(0, new ListItem("", ""));
            
            ddlEmp.Items.Insert(0, new ListItem("", ""));

            ddlStatus.Items.Insert(1, new ListItem("Ativo", "Ativo"));
            ddlStatus.Items.Insert(1, new ListItem("Preso", "Preso"));
            ddlStatus.Items.Insert(1, new ListItem("Doente", "Doente"));
            ddlStatus.Items.Insert(1, new ListItem("Feriado", "Feriado"));

            ddlStatus.Items[0].Selected = true;


            ddlLevel.Items.Insert(0, new ListItem("", "-1"));
            ddlLevel.Items.Insert(1, new ListItem("0", "0"));
            ddlLevel.Items.Insert(2, new ListItem("1", "1"));
            ddlLevel.Items.Insert(3, new ListItem("2", "2"));
            ddlLevel.Items[0].Selected = true;

            string sql = "select id, LOB from ML_LOBs";
            DataTable dtLOB = fx.GetDataTable(sql);
            foreach (DataRow dr in dtLOB.Rows)
            {
                ddlLOB.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }

            sql = "select id,Name from ML_TLs order by Name";
            DataTable dtTL = fx.GetDataTable(sql);

            foreach (DataRow dr in dtTL.Rows)
            {
                ddlTL.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                ddlTL2.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }

            sql = "select EmpID,Nome from ML_Agents order by Nome";
            DataTable dtEmpl = fx.GetDataTable(sql);

            foreach (DataRow dr in dtEmpl.Rows)
            {
                ddlEmp.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }


        }

        protected void btnEmp_Click(object sender, EventArgs e)
        {
            if (Loading) return;

            string empid = hdEmp.Value.ToString();
            string TL = hdTL.Value.ToString();
            string status = hdStatus.Value.ToString();
            string msg;
            if (empid=="")
            {
                msg = "<script>alert('Selecciona un empleado');</script>";
                Response.Write(msg);
                return;
            }

            if (TL == "" && status =="")
            {
                msg = "<script>alert('Selecciona nueva condicion al empleado');</script>";
                Response.Write(msg);
                return;
            }

            string sql = "update ML_Agents set ";
            string LOB = "";
            Functions fx = new Functions();

            if (TL != "")
            {
                
                LOB = fx.GetData("select LOB from ML_TLs where id=" + TL);
            }

            if (TL != "")
            {
                sql = sql + " TL = " + TL + ", LOB = " + LOB;
                if (status != "") sql = sql + ",";
            }

            if (status != "") sql = sql + " Status ='" + status+"' ";

            sql = sql + " where empID =" + empid;
            fx.UpdateData(sql);

            EmpAct.Text = " Atualizado";
            //msg = "<script>alert('Empleado Actualizado');</script>";
            //Response.Write(msg);
        }

        protected void btnTL_Click(object sender, EventArgs e)
        {
            if (Loading) return;

            string TL = txTL.Text.Trim();
            string TL2 = hdTL2.Value.ToString();
            string LOB = hdLOB.Value.ToString();
            string Level = hdLevel.Value.ToString();
            string SO = txSO.Text.Trim();
            string msg;

            if (TL == "" && TL2 =="0")
            {
                msg = "<script>alert('Digite nome de TL');</script>";
                Response.Write(msg);
                return;
            }
            if (LOB == "0")
            {
                msg = "<script>alert('Seleccione LOB');</script>";
                Response.Write(msg);
                return;
            }

            if (SO == "")
            {
                msg = "<script>alert('Digite S.O.');</script>";
                Response.Write(msg);
                return;
            }
            if (Level== "-1")
            {
                msg = "<script>alert('Escoja Nivel');</script>";
                Response.Write(msg);
                return;
            }

            Functions fx = new Functions();
            string sql;

            if (TL2 == "0")
            {
                sql = "insert into ML_TLs (Name, LOB, Level, SO) values ( ";
                sql = sql = "'" + TL + "'," + LOB + "," + Level + ",'" + SO + "')";
            }
            else
            {
                sql = "update ML_TLs setv";
                sql = sql + "LOB = " + LOB + ",";
                sql = sql + "SO = '" + SO + "' ";
                sql = sql + " where EmpID=" + TL2;
            }

            fx.UpdateData(sql);

            TLAct.Text = "Atualizado";

            //msg = "<script>alert('Actualizado Empleado');</script>";
            //Response.Write(msg);
        }

        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}