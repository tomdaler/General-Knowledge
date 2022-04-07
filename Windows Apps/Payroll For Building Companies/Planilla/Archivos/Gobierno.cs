using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planilla.Archivos
{
    public partial class Gobierno : Form
    {
        public Gobierno()
        {
            InitializeComponent();
        }

        private void Gobierno_Load(object sender, EventArgs e)
        {
            Funciones fx = new Funciones();
            DataTable dt = fx.GetData("select * from gobierno", "");

            tx2.Text = dt.Rows[0][0].ToString();
            tx3.Text = dt.Rows[0][1].ToString();

            tx4.Text = dt.Rows[0][3].ToString();
            txEvent.Text = dt.Rows[0][2].ToString();
         
            DataTable dt2 = fx.GetData("select * from renta where activo=true", "");
            dgRenta1.DataSource = dt2;
            dgRenta1.Columns[0].Visible = false;
            dgRenta1.Columns[5].Visible = false;

            dgRenta1.Columns[1].DefaultCellStyle.BackColor = Color.Wheat;
            dgRenta1.Columns[2].DefaultCellStyle.BackColor = Color.Wheat;

            dgRenta1.Columns[1].ReadOnly = true;
            dgRenta1.Columns[2].ReadOnly = true;

        

            for (int i =2; i<dgRenta1.Columns.Count; i++)

            dgRenta1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        

            string sql = " select id, Periodo, Minimo, Maximo, Tasa, Restar, Sumar ";
            sql = sql + " from Renta_Detalle, Renta ";
            sql = sql + " where Renta_Detalle.Cod_renta = Renta.cod_renta ";
            sql = sql + " and activo = True ";
            sql = sql + " order by renta.cod_renta, Minimo ";

            dt2 = fx.GetData(sql, "");
            dgRenta2.DataSource = dt2;

            dgRenta2.Columns[0].Visible = false;
            dgRenta2.Columns[1].DefaultCellStyle.BackColor = Color.Wheat;
            dgRenta2.Columns[1].ReadOnly = true;

            for (int i=2; i<7;i++)
                dgRenta2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sql = "update gobierno set ";
            sql = sql + " ,AFP =" + tx2.Text;
            sql = sql + " ,AFP_Patrono =" + tx3.Text;
            sql = sql + " ,Sueldo_Min =" + tx4.Text;
            sql = sql + " ,Renta = " + txEvent.Text;

            Funciones fx = new Funciones();
            fx.UpdateData(sql);

            Close();
        }
    }
}
