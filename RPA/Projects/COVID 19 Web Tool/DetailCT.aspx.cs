using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class DetailCT : System.Web.UI.Page
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

            string sql = "select '','',CT_EmpMobile, CT_Landline, CT_Residence, '', ";
            sql = sql + "  CT_Whereabouts, '', CT_Moredetails, ";
            sql = sql + "  '', CT_QuarantineStartDate, CT_QuarantineEndDate,  ";

            sql = sql + "  CT_ReportSource, CT_lastbadge, CT_empId, CT_firstname, CT_middlename, CT_lastname, ";
            sql = sql + "  CT_BestTime, CT_Transport, '',  ";

            sql = sql + "  CT_ForCTracing, CT_CloseContact, CT_ContactTracing,  ";
            sql = sql + "  CT_TestType, CT_PCR, CT_TotPCR, CT_dtTest, CT_dtRelease, ";
            sql = sql + "  CT_Faculty, CT_CurrFaculty, CT_dtConfinement,  ";
            sql = sql + "  CT_ICU, CT_dtFirstSymptoms, CT_Category, CT_DOH, ";
            sql = sql + "  CT_Severity, CT_BHERT, CT_Recommendation, '' ";
            sql = sql + "  CT_dtCTA1Start, ";
            sql = sql + "  CT_dtCTA1End ,";

            sql = sql + "  CT_dtCTA1Start, CT_dtCTA1End ";

            sql = sql + "  FROM DeclarationformsCT ";

            sql = sql + " where id =" + id;
           
            using (SqlCommand cmd = new SqlCommand(sql, sqlcon))
            {
                using (SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.Default))
                {
                    if (dr.Read())
                    {
                       // Supervisor.Text = fx.getValue2(dr[0]); // supervisor
                       // NextLevel.Text = fx.getValue2(dr[1]);

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
                       
                        L14.Text = dr["CT_ReportSource"].ToString();
                        Address.Text = dr["CT_Residence"].ToString();

                        LastBadge.Text = dr["CT_LastBadge"].ToString();

                        EmpID.Text = dr["CT_EmpID"].ToString();
                        employee.Text = " - " + dr["CT_firstname"].ToString() + " " + dr["CT_middlename"].ToString() + " " + dr["CT_lastname"].ToString();

                        BestTime.Text = dr["CT_BestTime"].ToString();
                        Transport.Text = dr["CT_Transport"].ToString();
                       
                        ForCTracing.Text = dr["CT_ForCTracing"].ToString();
                       // CloseContact.Text = dr["CT_CloseContact"].ToString();

                        ContactTracing.Text = dr["CT_ContactTracing"].ToString();
                        TestType.Text = dr["CT_TestType"].ToString();

                        PCR.Text = dr["CT_PCR"].ToString();
                        TotPCR.Text = dr["CT_TotPCR"].ToString();

                        dtTest.Text = dr["CT_dtTest"].ToString();
                        dtRelease.Text = dr["CT_dtRelease"].ToString();

                        //=======================
                        Faculty.Text = dr["CT_Faculty"].ToString();
                        CurrFaculty.Text = dr["CT_CurrFaculty"].ToString();

                        dtConfinement.Text = dr["CT_dtConfinement"].ToString();
                        ICU.Text = dr["CT_ICU"].ToString();

                        //=======================
                        dtFirstSymptoms.Text = dr["CT_dtFirstSymptoms"].ToString();

                        Vaccination.Text = fx.Vaccination(dr["CT_EmpID"].ToString());

                        Category.Text = dr["CT_Category"].ToString();

                        DOH.Text = dr["CT_DOH"].ToString();
                        Severity.Text = dr["CT_Severity"].ToString();

                        //=======================
                        BHERT.Text = dr["CT_BHERT"].ToString();
                        Recommendation.Text = dr["CT_Recommendation"].ToString();

                        //dtA1Assessment.Text = dr["CT_dtA1Assessment"].ToString();
                        //dtCTDeliveryStart.Text = dr["CT_dtCTA1Start"].ToString();

                        //=======================
                       // dtCTDeliveryEnd.Text = dr["CT_dtCTA1End"].ToString();
                        dtCTA1Start.Text = dr["CT_dtCTA1Start"].ToString();

                        dtCTA1End.Text = dr["CT_dtCTA1End"].ToString();
                    }
                }
            }
        }
    }
}