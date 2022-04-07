using System;
using System.Windows.Forms;

namespace Rep1
{
    public partial class Desde: UserControl
    {
        public Desde()
        {
            InitializeComponent();
        }
        
        public string anio()
        {
            return N1.Value.ToString();
        }
        public string mes()
        {
            int mes = cbMes.SelectedIndex + 1;
            return mes.ToString();
        }
        public string Fec1()
        {
            int mes = cbMes.SelectedIndex + 1;
            int ani = Convert.ToInt32(N1.Value.ToString());
            int fec1 = ani * 100 + mes;
            return fec1.ToString();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            cbMes.Items.Insert(0, "Enero");
            cbMes.Items.Insert(1, "Febrero");
            cbMes.Items.Insert(2, "Marzo");
            cbMes.Items.Insert(3, "Abril");
            cbMes.Items.Insert(4, "Mayo");
            cbMes.Items.Insert(5, "Junio");
            cbMes.Items.Insert(6, "Julio");
            cbMes.Items.Insert(7, "Agosto");
            cbMes.Items.Insert(8, "Septiembre");
            cbMes.Items.Insert(9, "Octubre");
            cbMes.Items.Insert(10, "Noviembre");
            cbMes.Items.Insert(11, "Diciembre");
                       
            cbMes.SelectedIndex = 0;
           
            N1.Maximum = DateTime.Now.Year;
            N1.Value = DateTime.Now.Year;
            N1.Minimum = DateTime.Now.Year - 10;
        }
    }
}
