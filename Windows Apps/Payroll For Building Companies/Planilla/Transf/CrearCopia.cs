using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planilla.Transf
{
    public partial class CrearCopia : Form
    {
        public CrearCopia()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete("Planilla2.mdb");
            }
            catch (Exception) { }

            File.Copy("Planilla.mdb", "Planilla2.mdb", true);
            MessageBox.Show("Copiado");
        }

        public void UpdateData(string sql)
        {
            string strDSN = ConfigurationManager.AppSettings["Lugar2"].ToString();
            //string strDSN = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = .\\Planilla.mdb";

            OleDbConnection myConn = new OleDbConnection(strDSN);
            myConn.Open();
            OleDbCommand cmd = new OleDbCommand(sql, myConn);
            cmd.ExecuteNonQuery();
            myConn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "delete from pla_detalle where cod_pla in ";
            sql = sql = " (select cod_pla from planillas where status = True )";
            UpdateData(sql);

            UpdateData("delete from planillas where status = True");

            MessageBox.Show("Borrado planillas procesadas en copia");

        }
    }
}
