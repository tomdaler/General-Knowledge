using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GPS.Codes
{
    public partial class Contacts : Form
    {
        public Contacts()
        {
            InitializeComponent();
        }

        private void Contacts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dtContacts.tsContacts' table. You can move, or remove it, as needed.
            this.tsContactsTableAdapter.Fill(this.dtContacts.tsContacts);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tsContactsTableAdapter.Update(this.dtContacts.tsContacts);
            
            SqlConnection conn = new SqlConnection(Variables.StrConn);
            conn.Open();
            Variables.lstContact.Clear();
            Variables.LoadCodes(conn, 4);
            conn.Close();

            MessageBox.Show("Data Updated");
        }

        private void tsContactsBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
