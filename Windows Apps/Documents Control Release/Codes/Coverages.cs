using System;
using System.Data;
using System.Windows.Forms;

namespace GPS.Codes
{
    public partial class Coverages : Form
    {
        public Coverages()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Coverages_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dtCoverages.tsCoverage' table. You can move, or remove it, as needed.
            this.tsCoverageTableAdapter.Fill(this.dtCoverages.tsCoverage);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tsCoverageTableAdapter.Update(this.dtCoverages.tsCoverage);

            MessageBox.Show("Data Updated");

            DataTable dt = Variables.GetDT("select id, CoverageDescr, CoverageCode from tsCoverage order by CoverageDescr");

            Variables.lstCoverage.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                Variables.lstCoverage.Add(new listado()
                {
                    Value = System.Convert.ToInt32(dr[0].ToString()),
                    Text = dr[1].ToString(),
                    Code = dr[2].ToString()

                });
            }

        }
    }
}
