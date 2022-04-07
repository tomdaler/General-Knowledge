using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Bing_Translator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            txtTranslatedText.Text = Traducir(1, txtTraslateFrom.Text);
        }


        private string Traducir(int opcion, string fromText)
        {
            string from1 = "en";
            string to1 = "en";
            string strTranslatedText = null;

            if (cb1.Text == "Spanish") from1 = "es";
            if (cb1.Text == "Portuguese") from1 = "pt";
            if (cb1.Text == "German") from1 = "de";
            if (cb1.Text == "French") from1 = "fr";
            if (cb1.Text == "Arab") from1 = "ar";
            if (cb1.Text == "Chinesse") from1 = "zh-CHS";

            if (cb2.Text == "Spanish") to1 = "es";
            if (cb2.Text == "Portuguese") to1 = "pt";
            if (cb2.Text == "German") to1 = "de";
            if (cb2.Text == "French") to1 = "fr";
            if (cb2.Text == "Arab") to1 = "ar";
            if (cb2.Text == "Chinesse") to1 = "zh-CHS";


            try {
                   TranslatorService.LanguageServiceClient client = new TranslatorService.LanguageServiceClient();
                   client = new TranslatorService.LanguageServiceClient();
                   strTranslatedText = client.Translate("6CE9C85A41571C050C379F60DA173D286384E0F2", fromText,  from1, to1);
                   return strTranslatedText;

                     //en english
                     //de german
                     //es espanol
                     //ar arab
                     //fr french
                     //pt portuguese
                     //zh-CHS  chinesse simplied traditions

                     //translationResult = client.Translate("", sourceText, "en", "de", "text/html", "general");
                  }
             catch (Exception ex) {
                   MessageBox.Show(ex.Message);
                 }
            return "";
        }

        #region Readfile
        private DataTable ReadFile()
        {
            string sql2 = "Select * FROM [Translate.csv]";
           
            string strPath = "c:\\";
            string Cn1 = "Driver={Microsoft Text Driver (*.txt; *.csv)}; DefaultDir=" + strPath;

            System.Data.Odbc.OdbcDataAdapter da = new OdbcDataAdapter(sql2, Cn1);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds, "Files");            
                dataGridView1.Visible = true;           
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            return ds.Tables[0];
           
        }
        #endregion


        private void Form1_Load(object sender, EventArgs e)
        {
            cb1.Items.Add("English");
            cb1.Items.Add("Spanish");
            cb1.Items.Add("Portuguese");
            cb1.Items.Add("German");
            cb1.Items.Add("French");
            cb1.Items.Add("Arab");
            cb1.Items.Add("Chinesse");

            cb2.Items.Add("English");
            cb2.Items.Add("Spanish");
            cb2.Items.Add("Portuguese");
            cb2.Items.Add("German");
            cb2.Items.Add("French");
            cb2.Items.Add("Arab");
            cb2.Items.Add("Chinesse");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ReadFile();
            dataGridView1.Visible = true;
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int conteo = 0;
            DataTable dt = ReadFile();
            foreach (DataRow dr in dt.Rows)
            {
                string ss = Traducir(2, dr[0].ToString());
                dataGridView1.Rows[conteo].Cells[1].Value = ss.ToString();
                conteo++;

               // string ss2;
              //  if ((conteo % 20) == 0)
              //  {
              //     MessageBox.Show("Message","Seguir"); //

               // }

            }
            button3.Visible = true;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            TextWriter sw = new StreamWriter(@"C:\\Translation.txt"); 
            int rowcount = dataGridView1.Rows.Count; 
            for (int i = 0; i < rowcount - 1; i++) 
            { 
                sw.WriteLine(dataGridView1.Rows[i].Cells[0].Value.ToString() + "\t" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "\t" + dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            
            sw.Close();     
            //Don't Forget Close the TextWriter Object(sw)   
            MessageBox.Show("Data Successfully Exported");
        }
    }
}
