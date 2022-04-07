using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MercadoLibre
{
    public partial class Cases : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Login = HttpContext.Current.Request.LogonUserIdentity.Name; // sa\\tdal6392
           
            Functions fx = new Functions();
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            string caseNo = txCase.Text;
            caseNo = caseNo.Trim();
            if (caseNo == "") return;

            Functions fx = new Functions();
            DataTable dt = fx.GetChat(caseNo);
            
            if (dt != null)
            {
                Agent.Text = "Agent :"+dt.Rows[0]["EmpName"].ToString();
                Chat.Text = dt.Rows[0]["Chat"].ToString();
            }
            else
            {
                Agent.Text = "";
                Chat.Text = "Case Number Not Found";
            }

            //Chat.Text = "Some NuGet packages were installed using a target framework different from the current target framework and may need to be reinstalled. Visit https://docs.nuget.org/docs/workflows/reinstalling-packages for more information.  Packages affected: Microsoft.CodeDom.Providers.DotNetCompilerPlatform	MercadoLibre";
        }
    }
}