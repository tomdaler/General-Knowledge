using System;
using System.Data;
using System.Windows.Forms;

namespace GPS.Codes
{
    public partial class Dpt : Form
    {
        public Dpt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Dpt_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dtDpt.tsDepartments' table. You can move, or remove it, as needed.
            this.tsDepartmentsTableAdapter.Fill(this.dtDpt.tsDepartments);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tsDepartmentsTableAdapter.Update(this.dtDpt.tsDepartments);

            
            MessageBox.Show("Data Updated");

            DataTable dt = Variables.GetDT("select id, description, code from tsDepartments order by description");

            Variables.lstDpto.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                Variables.lstDpto.Add(new listado() { Value = System.Convert.ToInt32(dr[0].ToString()),
                                                      Text = dr[1].ToString(),
                                                      Code = dr[2].ToString() }); 
            }
        }
    }
}
