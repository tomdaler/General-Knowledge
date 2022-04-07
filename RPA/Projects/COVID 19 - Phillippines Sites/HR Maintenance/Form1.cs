using System;
using System.Data;
using System.Windows.Forms;

namespace HR_Maintenance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const string FileName = "hr.xml";

        private void Form1_Load(object sender, EventArgs e)
        {
            string NEW = "S" + DateTime.Now.ToShortDateString() + ".xml";
            NEW = NEW.Replace("/","");
            try { System.IO.File.Delete(NEW); }
            catch (Exception) { }

            System.IO.File.Copy(FileName, NEW);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(FileName);
            dg.DataSource = dataSet.Tables[0];
            dg.Columns[0].Width = 150;
            dg.Columns[2].Width = 600;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dg.DataSource;
            dt.WriteXml(FileName);

            System.IO.File.Copy(FileName, "c:\\temp\\keep\\HR.xml",true);
            System.IO.File.Copy(FileName, "c:\\temp\\keep_DEV\\HR.xml",true);
                       
            System.Environment.Exit(0);
        }
    }
}
