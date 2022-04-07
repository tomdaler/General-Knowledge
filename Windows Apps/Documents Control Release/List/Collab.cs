using System;
using System.Windows.Forms;

namespace GPS.List
{
    public partial class Collab : Form
    {
        public Collab()
        {
            InitializeComponent();
        }

        private void Collab_Load(object sender, EventArgs e)
        {
            string texto = "-- All --";
            filtros1.LoadData(texto);
            string opcion = Variables.Report;
            label3.Text = opcion;

            switch (opcion)
            {
                case "Collab":       ddlStatus.DataSource = Variables.ToDatatable(Variables.lstContacts, texto);     break;
                case "Owner":        ddlStatus.DataSource = Variables.ToDatatable(Variables.lstContacts, texto);     break;
                case "Responsibles": ddlStatus.DataSource = Variables.ToDatatable(Variables.lstResponsibles, texto); break;
            }
                       
            ddlStatus.DisplayMember = "Text";
            ddlStatus.ValueMember = "Value";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dGrid1.Visible = false;

            string sql = "SELECT t1.ID, t1.DocNumber Number, DocTitle Title, CreateDate Created, ";
            sql = sql + " t2.DocType Type, t3.CoverageDescr Coverage, t4.Code Depto, t6.DocStatus Status ";

            string opcion = Variables.Report;
            switch (opcion)
            {
                case "Collab":       sql = sql + ",  t7.Name Collaborator "; break;
                case "Owner":        sql = sql + ",  t7.Name Owner "; break;
                case "Responsibles": sql = sql + ",  t7.Title Responsible "; break;
            }

            sql = sql + " FROM tuDocDesc t1, tsDocumentType t2, tsCoverage t3, tsDepartments t4, tsDocStatus t6 ";

            switch (opcion)
            {
                case "Collab":       sql = sql + ", tsContacts t7,      tuDocCollab t8 "; break;
                case "Owner":        sql = sql + ", tsContacts t7,      tuDocCollab t8 "; break;
                case "Responsibles": sql = sql + ", tsResponsables t7,  tuResponsibleGroups t8  "; break;
            }

            sql = sql + " where t1.DocType = t2.ID ";
            sql = sql + " and t1.Coverage = t3.ID ";
            sql = sql + " and t1.Department = t4.id ";
            sql = sql + " and t1.DocStatus = t6.ID ";
            sql = sql + " and t8.DocNumber = t1.ID ";

            switch (opcion)
            {
                case "Collab":       sql = sql + " and t8.Contact = t7.ID  "; break;
                case "Owner":        sql = sql + " and t8.Contact = t7.ID  "; break;
                case "Responsibles": sql = sql + " and t8.GroupName = t7.ID "; break;
            }

            string orgs = filtros1.orgs;
            string types = filtros1.types;
            string dpto = filtros1.dpto;
            string coverage = filtros1.coverage;

            string status = ddlStatus.SelectedValue.ToString();

            if (types != "0")    sql = sql + " and t2.id =" + types;
            if (coverage != "0") sql = sql + " and t3.id =" + coverage;
            if (dpto != "0")     sql = sql + " and t4.id =" + dpto;
            if (orgs != "0")     sql = sql + " and t1.organization =" + orgs;

            if (status != "0") sql = sql + " and t7.id =" + status;

            dGrid1.DataSource = Variables.GetDT(sql);
            //***************************************

            dGrid1.Visible = true;

            dGrid1.Columns[0].Width = 40;
            dGrid1.Columns[1].Width = 140;
            dGrid1.Columns[2].Width = 250;

            dGrid1.Columns[3].Width = 80;
            dGrid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dGrid1.Columns[4].Width = 90;
            dGrid1.Columns[5].Width = 80;

            dGrid1.Columns[6].Width = 50;
            dGrid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dGrid1.Columns[8].Width = 150;

            lbMSG.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
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
