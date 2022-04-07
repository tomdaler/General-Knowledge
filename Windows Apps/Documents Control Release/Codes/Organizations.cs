using System;
using System.Data;
using System.Windows.Forms;

namespace GPS.Codes
{
    public partial class Organizations : Form
    {
        public Organizations()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Organizations_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dtOrgs.tsOrgList' table. You can move, or remove it, as needed.
            this.tsOrgListTableAdapter.Fill(this.dtOrgs.tsOrgList);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tsOrgListTableAdapter.Update(this.dtOrgs.tsOrgList);
            MessageBox.Show("Data Updated");

            DataTable dt = Variables.GetDT("select id, org_descr, orgcode from tsOrgList order by org_descr");

            Variables.lstOrgs.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                Variables.lstOrgs.Add(new listado() { Value = System.Convert.ToInt32(dr[0].ToString()),
                                                      Text = dr[1].ToString(),
                                                      Code = dr[2].ToString() }); ;
            }

        }
    }
}
