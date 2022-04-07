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
    public partial class RentaAjuste : Form
    {
        public RentaAjuste()
        {
            InitializeComponent();
        }

        private void RentaAjuste_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        { 
            Funciones fx = new Funciones();

            string sql = " select id, Periodo, Minimo, Maximo, Tasa, Restar, Sumar ";
            sql = sql + " from Renta_Detalle_Junio a, Renta ";
            sql = sql + " where a.Cod_renta = Renta.cod_renta ";
            sql = sql + " and activo = True ";

            if (cb1.Text != "" && cb1.Text != "Todos")
                sql = sql + " and Periodo ='Pago " + cb1.Text + "' ";

            sql = sql + " order by renta.cod_renta, Minimo ";

            DataTable dt = fx.GetData(sql, "");
            dgRenta1.DataSource = dt;

            dgRenta1.Columns[0].Visible = false;
            dgRenta1.Columns[1].DefaultCellStyle.BackColor = Color.Wheat;
            dgRenta1.Columns[1].ReadOnly = true;

            for (int i = 2; i < 7; i++)
                dgRenta1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            
            sql = " select id, Periodo, Minimo, Maximo, Tasa, Restar, Sumar ";
            sql = sql + " from Renta_Detalle_Diciembre a, Renta ";
            sql = sql + " where a.Cod_renta = Renta.cod_renta ";
            sql = sql + " and activo = True ";

            if (cb1.Text != "" && cb1.Text != "Todos")
                sql = sql + " and Periodo ='Pago " + cb1.Text + "' ";

            sql = sql + " order by renta.cod_renta, Minimo ";

            dt = fx.GetData(sql, "");
            dgRenta2.DataSource = dt;

            dgRenta2.Columns[0].Visible = false;
            dgRenta2.Columns[1].DefaultCellStyle.BackColor = Color.Wheat;
            dgRenta2.Columns[1].ReadOnly = true;

            for (int i = 2; i < 7; i++)
                dgRenta2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar();
        }
    }
}
