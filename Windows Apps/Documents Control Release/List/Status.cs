using System;
using System.Windows.Forms;

namespace GPS.List
{
    public partial class Status : Form
    {
        public Status()
        {
            InitializeComponent();
        }

        private void Status_Load(object sender, EventArgs e)
        {
            string texto = "-- All --";
            filtros1.LoadData(texto);

            ddlStatus.DataSource = Variables.ToDatatable(Variables.lstStatus, texto);
            ddlStatus.DisplayMember = "Text";
            ddlStatus.ValueMember = "Value";

            ddlEfficacy.DataSource = Variables.ToDatatable(Variables.lstEfficacy, texto);
            ddlEfficacy.DisplayMember = "Text";
            ddlEfficacy.ValueMember = "Value";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dGrid1.Visible = false;

            string sql = "SELECT t1.ID, t1.DocNumber Number, DocTitle Title, CreateDate Created, ";
            sql = sql + " t2.DocType Type, t3.CoverageDescr Coverage, t4.Code Depto, t6.DocStatus Status, t7.EfficacyStatus, t1.CreatedBy ";

            sql = sql + " FROM tuDocDesc t1, tsDocumentType t2, tsCoverage t3, tsDepartments t4, tsDocStatus t6, tsEfficacyStatus t7 ";
            sql = sql + " where t1.DocType = t2.ID ";
            sql = sql + " and t1.Coverage = t3.ID ";
            sql = sql + " and t1.Department = t4.id ";

            sql = sql + " and t1.DocStatus = t6.ID ";
            sql = sql + " and t1.EfficacyStatus = t7.ID ";

            string orgs = filtros1.orgs;
            string types = filtros1.types;
            string dpto = filtros1.dpto;
            string coverage = filtros1.coverage;

            string status = ddlStatus.SelectedValue.ToString();
            string status2 = ddlEfficacy.SelectedValue.ToString();

            if (types != "0") sql = sql + " and t2.id =" + types;
            if (coverage != "0") sql = sql + " and t3.id =" + coverage;
            if (dpto != "0") sql = sql + " and t4.id =" + dpto;
            if (orgs != "0") sql = sql + " and t1.organization =" + orgs;
            if (status != "0") sql = sql + " and t6.id =" + status;
            if (status2 != "0") sql = sql + " and t1.EfficacyStatus =" + status2;

            dGrid1.DataSource = Variables.GetDT(sql);
            //***************************************

            dGrid1.Visible = true;

            dGrid1.Columns[0].Width = 40;
            dGrid1.Columns[1].Width = 120;
            dGrid1.Columns[2].Width = 230;

            dGrid1.Columns[3].Width = 80;
            dGrid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dGrid1.Columns[4].Width = 80;

            dGrid1.Columns[5].Width = 80;

            dGrid1.Columns[6].Width = 50;
            dGrid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dGrid1.Columns[7].Width = 60;
            lbMSG.Visible = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
