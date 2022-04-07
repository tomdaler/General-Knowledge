using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPS.Documents
{
    public partial class UpdateContributors : Form
    {
        public UpdateContributors()
        {
            InitializeComponent();
        }

        private void UpdateContributors_Load(object sender, EventArgs e)
        {
             ddl1.DataSource = Variables.ToDatatable(Variables.lstContacts, "----"); 
             ddl1.DisplayMember = "Text";
             ddl1.ValueMember = "Value";

            string sql = " SELECT CAST(ID AS nvarchar(4)) +'  ' +DocNumber + '  '+ DocTitle from tuDocDesc where DocStatus <>3 Order by DocTitle "; 
            DataTable dt = Variables.GetDT(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ck.Items.Add(dt.Rows[i][0].ToString());

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string list1 = "";
            string comma = "";
            foreach (string item in ck.SelectedItems)
            {
                int pos = item.IndexOf(" ");
                list1 = comma + item.Substring(0, pos);
                comma = ",";

            }
        }
    }
}
