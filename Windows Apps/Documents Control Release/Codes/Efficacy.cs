using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GPS.Codes
{
    public partial class Efficacy : Form
    {
        public Efficacy()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Efficacy_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dtEffOwner.tsEfficacyOwner' table. You can move, or remove it, as needed.
            this.tsEfficacyOwnerTableAdapter.Fill(this.dtEffOwner.tsEfficacyOwner);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tsEfficacyOwnerTableAdapter.Update(this.dtEffOwner.tsEfficacyOwner);

            SqlConnection conn = new SqlConnection(Variables.StrConn);
            conn.Open();
            Variables.lstEffOwner.Clear();
            Variables.LoadCodes(conn, 1);
            conn.Close();

            MessageBox.Show("Data Updated");

        }
    }
}
