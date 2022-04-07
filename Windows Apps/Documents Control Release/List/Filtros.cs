using System;
using System.Windows.Forms;

namespace GPS.List
{
    public partial class Filtros : UserControl
    {

        public string orgs { get; set; }
        public string dpto { get; set; }
        public string types { get; set; }
        public string coverage { get; set; }

        public string textoIni { get; set; }


        public Filtros()
        {
            InitializeComponent();
        }

        private void Filtros_Load(object sender, EventArgs e)
        {
        }

        public void LoadData(string texto)
        {
            orgs = "0";
            dpto = "0";
            types = "0";
            coverage = "0";

            ddlOrg.DataSource = Variables.ToDatatable(Variables.lstOrgs, texto);
            ddlOrg.DisplayMember = "Text";
            ddlOrg.ValueMember = "Value";

            ddlCoverage.DataSource = Variables.ToDatatable(Variables.lstCoverage, texto);
            ddlCoverage.DisplayMember = "Text";
            ddlCoverage.ValueMember = "Value";

            ddlType.DataSource = Variables.ToDatatable(Variables.lstTypes, texto);
            ddlType.DisplayMember = "Text";
            ddlType.ValueMember = "Value";

            dpDepto.DataSource = Variables.ToDatatable(Variables.lstDpto, texto);
            dpDepto.DisplayMember = "Text";
            dpDepto.ValueMember = "Value";

        }

        private void ddlCoverage_SelectedIndexChanged(object sender, EventArgs e)
        {
            coverage = ddlCoverage.SelectedValue.ToString();
            if (coverage == "System.Data.DataRowView") coverage = "0";


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dpDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            dpto = dpDepto.SelectedValue.ToString();
            if (dpto == "System.Data.DataRowView") dpto = "0";

        }

        private void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {

            orgs = ddlOrg.SelectedValue.ToString();
            if (orgs == "System.Data.DataRowView") orgs = "0";

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            types = ddlType.SelectedValue.ToString();
            if (types == "System.Data.DataRowView") types = "0";

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
