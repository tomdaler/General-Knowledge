using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPS.List
{
    public partial class Owner : Form
    {
        public Owner()
        {
            InitializeComponent();
        }

        private void Owner_Load(object sender, EventArgs e)
        {
            string texto = "-- All --";
            filtros1.LoadData(texto);

            ddlStatus.DataSource = Variables.ToDatatable(Variables.lstContacts, texto);
            ddlStatus.DisplayMember = "Text";
            ddlStatus.ValueMember = "Value";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            dGrid1.Visible = false;

            string sql = "SELECT t1.ID, t1.DocNumber Number, DocTitle Title,  CreateDate Created, ";
            sql = sql + " t2.DocType Type, t3.CoverageDescr Coverage, t4.Code Depto, t5.orgcode Org, t6.DocStatus Status, t7.Name Owner ";

            sql = sql + " FROM tuDocDesc t1, tsDocumentType t2, tsCoverage t3, tsDepartments t4, tsOrgList t5, tsDocStatus t6, tsContacts t7, tuDocOwner t8 ";
            sql = sql + " where t1.DocType = t2.ID ";
            sql = sql + " and t1.Coverage = t3.ID ";
            sql = sql + " and t1.Department = t4.id ";
            sql = sql + " and t1.organization = t5.id ";
            sql = sql + " and t1.DocStatus = t6.ID ";
            sql = sql + " and t8.Contact = t7.ID ";
            sql = sql + " and t8.DocNumber = t1.ID ";

            //string types = ddlType.SelectedValue.ToString();
            //string coverage = ddlCoverage.SelectedValue.ToString();
            //string dpto = dpDepto.SelectedValue.ToString();
            //string orgs = ddlOrg.SelectedValue.ToString();

            string orgs = filtros1.orgs;
            string types = filtros1.types;
            string dpto = filtros1.dpto;
            string coverage = filtros1.coverage;


            string status = ddlStatus.SelectedValue.ToString();

            if (types != "0") sql = sql + " and t2.id =" + types;
            if (coverage != "0") sql = sql + " and t3.id =" + coverage;
            if (dpto != "0") sql = sql + " and t4.id =" + dpto;
            if (orgs != "0") sql = sql + " and t5.id =" + orgs;
            if (status != "0") sql = sql + " and t7.id =" + status;


            SqlConnection conn = new SqlConnection(Variables.StrConn);

            try
            {

                conn.Open();

                SqlCommand comando = new SqlCommand(sql, conn);

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comando;


                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                dGrid1.DataSource = dt;

                dGrid1.Visible = true;

                dGrid1.Columns[0].Width = 40;
                dGrid1.Columns[1].Width = 140;
                dGrid1.Columns[2].Width = 250;

                dGrid1.Columns[3].Width = 80;
                dGrid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dGrid1.Columns[4].Width = 90;

                dGrid1.Columns[6].Width = 50;
                dGrid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dGrid1.Columns[7].Width = 50;
                dGrid1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dGrid1.Columns[9].Width = 150;
            }

            catch (Exception ex)
            {
                string e1 = ex.ToString();
                MessageBox.Show(e1);

            }
            finally
            {
                conn.Close();
            }

        }

        private void dGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dGrid1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                Variables.DocNumber = dGrid1.Rows[e.RowIndex].Cells[0].Value.ToString();
                Documents.txRef f2 = new Documents.txRef();
                f2.MdiParent = this.ParentForm;
                f2.Show();

                Close();
            }

        }

        private void dGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
