using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace GPS.List
{


    public partial class Report : Form
    {

   
        public Report()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            string texto = "-- All --";
            filtros1.LoadData(texto);

            if (Variables.Report == "DateCreation") label1.Text = "Creation From";
            if (Variables.Report == "Complete") label1.Text = "Completed From";
            if (Variables.Report == "NextReview") label1.Text = "Next Review From";

            if (Variables.Report == "Retired")
            {
                ddlStatus.Visible = false;
                ddlEfficacy.Visible = false;
                ddlEfficacyOwner.Visible = false;

                label3.Visible = false;
                label9.Visible = false;
                label6.Visible = false;

                label1.Text = "Retired From";
            }

            List<listado> nuevo2 = new List<listado>();

            if (ddlStatus.Visible)
            {
                //  nuevo2 = Variables.lstStatus;
                foreach (listado nuevo3 in Variables.lstStatus)
                {                   
                    if (Variables.Report != "NextReview"  || nuevo3.Text != "Retired" )
                       nuevo2.Add(nuevo3);
                }

                ddlStatus.DataSource = Variables.ToDatatable(nuevo2, texto);
                ddlStatus.DisplayMember = "Text";
                ddlStatus.ValueMember = "Value";
            }


            if (ddlEfficacy.Visible)
            {
                ddlEfficacy.DataSource = Variables.ToDatatable(Variables.lstEfficacy, texto);
                ddlEfficacy.DisplayMember = "Text";
                ddlEfficacy.ValueMember = "Value";

                nuevo2 = Variables.lstEffOwner;
                //nuevo2.RemoveAt(0);
                ddlEfficacyOwner.DataSource = Variables.ToDatatable(nuevo2, texto);
                ddlEfficacyOwner.DisplayMember = "Text";
                ddlEfficacyOwner.ValueMember = "Value";
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            dGrid1.Visible = false;
            string sql = "";

            if (Variables.Report == "DateCreation")
            {
                sql = "SELECT t1.ID, t1.DocNumber Number, DocTitle Title,  CreateDate Created, ";
                sql = sql + " t2.DocType Type, t3.CoverageDescr Coverage, t4.Code Depto, t5.orgcode Org, t6.DocStatus Status, t7.EfficacyStatus, ";

                sql = sql + " CASE WHEN t1.EfficacyOwner IS NULL THEN '' ";
                sql = sql + "  ELSE (select EfficacyOwner from tsEfficacyOwner where ID = t1.EfficacyOwner )  END EfficacyOwner, ";
                sql = sql + " CASE WHEN t1.RevisionRequired = 0 then 'NO' ELSE 'YES' END Required, t1.CreatedBy, t1.EfficacyLastChangedBy EfficacyLastChangedBy";

                sql = sql + " FROM tuDocDesc t1, tsDocumentType t2, tsCoverage t3, tsDepartments t4, tsOrgList t5, tsDocStatus t6, tsEfficacyStatus t7 ";
                sql = sql + " where t1.DocType = t2.ID ";
                sql = sql + " and t1.Coverage = t3.ID ";
                sql = sql + " and t1.Department = t4.id ";
                sql = sql + " and t1.organization = t5.id ";
                sql = sql + " and t1.DocStatus = t6.ID ";
                sql = sql + " and t1.EfficacyStatus = t7.ID ";
            }

            if (Variables.Report == "Retired")
            {
                sql = "  SELECT t1.ID, t1.DocNumber Number, DocTitle Title,  t8.CreateDate Retired, t2.DocType Type, t3.CoverageDescr Coverage, t1.EfficacyLastChangedBy EfficacyLastChangeBy, t8.CreatedBy RetiredBy ";

                sql = sql + "  FROM tuDocDesc t1, tsDocumentType t2, tsCoverage t3, tsDepartments t4, tsOrgList t5, tuDocStatus t8 ";
                sql = sql + "  where t1.DocType = t2.ID ";
                sql = sql + "  and t1.Coverage = t3.ID ";
                sql = sql + "  and t1.Department = t4.id ";
                sql = sql + "  and t1.organization = t5.id ";

                sql = sql + "  and t1.id = t8.docnumber ";
            }

            if (Variables.Report != "DateCreation" && Variables.Report != "Retired")
            {
                sql = "select t1.ID, t1.DocNumber Number, DocTitle Title, t4.DocStatus Status, Version, t2.EfficacyStatus,   ";
                sql = sql + " convert(varchar, ExpectedReviewDate, 110) Expected, ";
                sql = sql + " convert(varchar, RevCompleteDate, 110) Complete, ";

                sql = sql + " CASE WHEN t1.EfficacyOwner IS NULL THEN '' ";
                sql = sql + "  ELSE (select EfficacyOwner from tsEfficacyOwner where ID = t1.EfficacyOwner )  END EfficacyOwner, ";
                sql = sql + " dtAssigned Assigned, ";
                sql = sql + " CASE WHEN t1.RevisionRequired = 0 then 'NO' ELSE 'YES' END Required, t1.EfficacyLastChangedBy EfficacyLastChangedBy CHANGE ";

                sql = sql + "  from tuDocDesc t1, tsEfficacyStatus t2, tsDocStatus t4 ";
                sql = sql + "  where t1.EfficacyStatus = t2.ID ";
                sql = sql + "   and t1.DocStatus = t4.ID ";
            }




            string orgs = filtros1.orgs;
            string types = filtros1.types;
            string dpto = filtros1.dpto;
            string coverage = filtros1.coverage;

            string owner2= ""; 

            string status = "3";
            string status2 = "0";

           
            if (ddlEfficacy.Visible) status2 = ddlEfficacy.SelectedValue.ToString();
            if (ddlEfficacyOwner.Visible) owner2 = ddlEfficacyOwner.SelectedValue.ToString();

            if (types != "0") sql = sql + " and t1.DocType =" + types;
            if (coverage != "0") sql = sql + " and t1.coverage =" + coverage;
            if (dpto != "0") sql = sql + " and t1.department =" + dpto;
            if (orgs != "0") sql = sql + " and t1.organization =" + orgs;

            if (ddlStatus.Visible) status = ddlStatus.SelectedValue.ToString();
            if (status != "0") sql = sql + " and t1.DocStatus =" + status;

            string owner = "0";
            if (ddlEfficacyOwner.Visible) owner = ddlEfficacyOwner.SelectedValue.ToString();
            if (owner != "0") sql = sql + " and t1.EfficacyOwner =" + owner;



            if (status2 != "0") sql = sql + " and t1.EfficacyStatus =" + status;
       

            string fec1 = DT1.Value.ToString("yyyy-MM-dd");
            string fec2 = DT2.Value.ToString("yyyy-MM-dd");

            if (Variables.Report == "DateCreation")
            {
                sql = sql + " AND CreateDate >=' " + fec1 + "' ";
                sql = sql + " AND CreateDate <=' " + fec2 + "' ";
            }

            if (Variables.Report == "Complete")
            {
                sql = sql + " AND RevCompleteDate >=' " + fec1 + "' ";
                sql = sql + " AND RevCompleteDate <=' " + fec2 + "' ";
            }

            if (Variables.Report == "NextReview")
            {
                sql = sql + " AND t1.DocStatus <> 3 ";
                sql = sql + " AND ExpectedReviewDate>=' " + fec1 + "' ";
                sql = sql + " AND ExpectedReviewDate<=' " + fec2 + "' ";
                if (owner2 != "0") sql = sql + " and t1.EfficacyOwner =" + owner2;

            }

            if (Variables.Report == "Retired")
            {
                sql = sql + " AND t8.CreateDate>=' " + fec1 + "' ";
                sql = sql + " AND t8.CreateDate<=' " + fec2 + "' ";
            }

            if (Variables.Report != "NextReview")
            {
                sql = sql.Replace("CHANGE", "");
                dGrid1.DataSource = Variables.GetDT(sql);
            }
            else
            {
                sql = sql.Replace("CHANGE", ", path as Draft, sharepointlink as Published, t1.id as docu, '' as Distributors, '' as Related  ");
                System.Data.DataTable dt = Variables.GetDT(sql);
                if (dt.Rows.Count == 0) return;

                // bring each distributor by row
                dt = LoadMoreInfo(dt, 15, 1);
                dt = LoadMoreInfo(dt, 16, 2);


                dt.Columns.Remove("docu");



                dGrid1.DataSource = dt;
               
            }


            //***************************************

            dGrid1.Visible = true;

            dGrid1.Columns[0].Width = 40;
            dGrid1.Columns[1].Width = 120;
            dGrid1.Columns[2].Width = 240;

            lbMSG.Visible = true;

            if (Variables.Report == "DateCreation" )
                {
                    dGrid1.Columns[3].Width = 80;
                    dGrid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dGrid1.Columns[4].Width = 90;

                    dGrid1.Columns[6].Width = 50;
                    dGrid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dGrid1.Columns[7].Width = 50;
                    dGrid1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dGrid1.Columns[9].Width = 150;
                }


            if (Variables.Report != "DateCreation" && Variables.Report != "Retired")
                {
                    dGrid1.Columns[3].Width = 100;
                    dGrid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dGrid1.Columns[4].Width = 50;
                    dGrid1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dGrid1.Columns[5].Width = 80;
                    dGrid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dGrid1.Columns[6].Width = 80;
                    dGrid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dGrid1.Columns[7].Width = 80;
                    dGrid1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dGrid1.Columns[8].Width = 80;
                    dGrid1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //       dGrid1.Columns[8].Width = 450;
                }

            if (Variables.Report == "NextReview")
            {
                dGrid1.Columns[12].Width = 800;
                dGrid1.Columns[13].Width = 800;
                dGrid1.Columns[14].Width = 300;
                dGrid1.Columns[15].Width = 300;
            }



        }

        
        public static string GetRelatedDocs(int docu, SqlConnection connection, int opcion)
        {
            SqlCommand command = null;
            string MsgErr = "";
            try
            {
                command = new SqlCommand();
                command.Connection = connection;
                if (opcion == 1) command.CommandText = "Get_RelatedDocForNewReview";
                if (opcion == 2) command.CommandText = "Get_ContactsForNewReview";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@docu", docu);

                var _info = command.ExecuteScalar();
                //connection.Close();

                if (_info == null) _info = "";
                return _info.ToString();
            }
            catch (Exception es)
            {
                MsgErr = "When searching  " + es.ToString();
            }
            finally
            {
                if (command != null)
                    command.Dispose();
            }
            return "";
        }

        private DataTable LoadMoreInfo(DataTable dt, int ColPos, int opcion)
        {
            // foreach (System.Data.DataColumn col in dt.Columns) col.ReadOnly = false;
            dt.Columns[ColPos].ReadOnly = false;
            dt.Columns[ColPos].MaxLength = 100;
            //int max1 = 100;

            System.Data.SqlClient.SqlConnection conn = new SqlConnection(Variables.StrConn);
            conn.Open();

            foreach (DataRow dr in dt.Rows)
            {
                string docu = dr[14].ToString();
                int docu2 = System.Convert.ToInt32(docu);

                try
                {
                    // store procedure
                    string vvalor = GetRelatedDocs(docu2, conn, opcion);
                   
                    if (vvalor != "")
                    {
                        int largo = vvalor.Length;
                        if (largo > 2) vvalor = vvalor.Substring(0, largo - 2);
                        dr[ColPos] = vvalor;
                    }
                }
                catch(Exception)
                {

                }                
            }

            conn.Close();
            return dt;
        }

     
        private void dGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dGrid1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                Variables.DocNumber = dGrid1.Rows[e.RowIndex].Cells[0].Value.ToString();
                Documents.ddlExpect f2 = new Documents.ddlExpect();
                f2.MdiParent = this.ParentForm;
                f2.Show();

                Close();
            }
        }        
    }
    
}
