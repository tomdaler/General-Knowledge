using System;
using System.Data;
using System.Windows.Forms;

namespace GPS.Codes
{
    public partial class DocTypes : Form
    {
        public DocTypes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DocTypes_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dtDocType.tsDocumentType' table. You can move, or remove it, as needed.
            this.tsDocumentTypeTableAdapter.Fill(this.dtDocType.tsDocumentType);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tsDocumentTypeTableAdapter.Update(this.dtDocType.tsDocumentType);

            MessageBox.Show("Data Updated");

            DataTable dt = Variables.GetDT("select id, DocType, DocCode from tsDocumentType order by DOcType");

            Variables.lstTypes.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                Variables.lstTypes.Add(new listado()
                {
                    Value = System.Convert.ToInt32(dr[0].ToString()),
                    Text = dr[1].ToString(),
                    Code = dr[2].ToString()

                });
            }

        }
    }
}
