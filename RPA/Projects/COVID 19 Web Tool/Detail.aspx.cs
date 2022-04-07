using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace WebApplication1
{
    public partial class Detail : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            Common fx = new Common();
            string user = fx.GetAuthorization();
            if (user == "")
            {
                Response.Write("<script>alert('You Are Not Authorized');</script>");
                Response.Write("<script>window.close();</script>");
                return;
            }

            string id = Request.QueryString["id"];

            string SqlConn = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            SqlConnection sqlcon = new SqlConnection(SqlConn);
            sqlcon.Open();

            string sql = "select EmpTeamLeader,EmpNextLevelMgr, EmpMobile, Landline, EmpAddress, DateContact,";
            sql = sql + " Whereabouts, InitialRemarks, Moredetails, ";
            sql = sql + " Remarks, QuarantineStartDate, QuarantineEndDate, ";
           // sql = sql + " CASE WHEN employeeClient='Y' or employeeClient='y' THEN 'Employee' ELSE 'Client' END , ";

            sql = sql + " ReportSource, lastbadge, empId, firstname, middlename, lastname, ";
            sql = sql + " BestTime, Transport, Residence, ";


            sql = sql + " ForCTracing, CloseContact, ContactTracing, ";
            sql = sql + " TestType, PCR, TotPCR, dtTest, dtRelease, ";
            sql = sql + " Faculty, CurrFaculty, dtConfinement, ";
            sql = sql + " ICU, dtFirstSymptoms, Category, DOH, ";
            sql = sql + " Severity, BHERT, Recommendation, dtA1Assessment, ";
            sql = sql + " dtCTDeliveryStart,dtCTDeliveryEnd, dtCTA1Start, dtCTA1End, dtLGULetter ";


            sql = sql + " FROM Declarationforms";
            sql = sql + " where id =" + id;

            using (SqlCommand cmd = new SqlCommand(sql, sqlcon))
            {
                using (SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.Default))
                {

                    if (dr.Read())
                    {
                        Supervisor.Text = fx.getValue2(dr[0]); // supervisor
                        NextLevel.Text = fx.getValue2(dr[1]);

                        L3.Text = fx.getValue2(dr[2]); //mobile
                        L4.Text = fx.getValue2(dr[3]);
                        try
                        {
                            // address
                            Address.Text = dr["EmpAddress"].ToString();
                        }
                        catch (Exception) { }


                        L6.Text = fx.getValue2(dr[5]);  // dt
                        L7.Text = fx.getValue2(dr[6]);
                        L8.Text = fx.getValue2(dr[7]);
                        L9.Text = fx.getValue2(dr[8]);
                        L10.Text = fx.getValue2(dr[9]);
                        L11.Text = fx.getValue2(dr[10]);
                        L12.Text = fx.getValue2(dr[11]);
                        L13.Text = "EMPLOYEE"; // dr.GetString(12).ToString();
                        L14.Text = dr["ReportSource"].ToString();
                        
                        LastBadge.Text = dr["LastBadge"].ToString();

                        EmpID.Text = dr["EmpID"].ToString();
                        employee.Text = " - " + dr["firstname"].ToString() +" "+ dr["middlename"].ToString() + " "+dr["lastname"].ToString();
                        
                        BestTime.Text = dr["BestTime"].ToString();
                        Transport.Text = dr["Transport"].ToString();
                        Residence.Text = dr["Residence"].ToString();


                        ForCTracing.Text = dr["ForCTracing"].ToString();
                        CloseContact.Text = dr["CloseContact"].ToString();

                        ContactTracing.Text = dr["ContactTracing"].ToString();
                        TestType.Text = dr["TestType"].ToString();

                        PCR.Text = dr["PCR"].ToString();
                        TotPCR.Text = dr["TotPCR"].ToString();

                        dtTest.Text = dr["dtTest"].ToString();
                        dtRelease.Text = dr["dtRelease"].ToString();

                        //=======================
                        Faculty.Text = dr["Faculty"].ToString();
                        CurrFaculty.Text = dr["CurrFaculty"].ToString();

                        dtConfinement.Text = dr["dtConfinement"].ToString();
                        ICU.Text = dr["ICU"].ToString();

                        //=======================
                        dtFirstSymptoms.Text = dr["dtFirstSymptoms"].ToString();

                        Vaccination.Text = fx.Vaccination(dr["EmpID"].ToString()) ;

                        Category.Text = dr["Category"].ToString();

                        DOH.Text = dr["DOH"].ToString();
                        Severity.Text = dr["Severity"].ToString();

                        //=======================
                        BHERT.Text = dr["BHERT"].ToString();
                        Recommendation.Text = dr["Recommendation"].ToString();

                        dtA1Assessment.Text = dr["dtA1Assessment"].ToString();
                        dtCTDeliveryStart.Text = dr["dtCTDeliveryStart"].ToString();


                        //=======================
                        dtCTDeliveryEnd.Text = dr["dtCTDeliveryEnd"].ToString();
                        dtCTA1Start.Text = dr["dtCTA1Start"].ToString();

                        dtCTA1End.Text = dr["dtCTA1End"].ToString();

                        object var1 = dr["dtLGULetter"];
                        if (var1 != null)
                            LGU.Text = dr["dtLGULetter"].ToString();
                    }
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //string pageurl = System.Environment.MachineName;
            //string id = Request.QueryString["id"];
            string query = "CloseContact.aspx?id=" + EmpID.Text + employee.Text;
            query = query.Replace("'", "");

            //Alerta.Show(query);

            // string msg = "<script>window.open('Detail.aspx?id=" + ID + "', '_blank','toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes');</script>";
          
            string msg = "<script>window.open('"+ query + "', '_blank','toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes');</script>";

            
            ClientScript.RegisterStartupScript(GetType(), "test", msg);
        }
    }
}