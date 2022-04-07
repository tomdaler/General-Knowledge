using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GPS.Codes
{
    public partial class Responsible : Form
    {
        public Responsible()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Responsible_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.tsResponsables' table. You can move, or remove it, as needed.
            this.tsResponsablesTableAdapter.Fill(this.dataSet1.tsResponsables);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tsResponsablesTableAdapter.Update(this.dataSet1.tsResponsables);

            SqlConnection conn = new SqlConnection(Variables.StrConn);
            conn.Open();
            Variables.lstResponsibles.Clear();
            Variables.LoadCodes(conn, 2);
            conn.Close();

            MessageBox.Show("Data Updated");
        }
    }
}
