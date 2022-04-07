using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Windows.Forms;

namespace Planilla.RepDetalle
{
    public partial class Partidas : Form
    {
        public Partidas()
        {
            InitializeComponent();
        }

        private void Partidas_Load(object sender, EventArgs e)
        {
            ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT CODIGO, FECHA2, nombre_usual as Nombre, d.Partidas, c.Valor, Cantidad, c.Valor* Cantidad as Total, Unidad ";
            sql = sql + " FROM EMPLEADO e, pla_partidas b, Partidas c, planillas d ";

            sql = sql + " WHERE e.cod_empl = b.cod_empl ";
            sql = sql + " and b.cod_partida = c.cod_partida ";
            sql = sql + " and b.cod_pla = d.cod_pla ";
        
            sql = sql + Variables.DESDE + fechas.Fec1();
            sql = sql + Variables.HASTA + fechas.Fec2();

            Funciones fx = new Funciones();
            dgObs.DataSource = fx.GetData(sql, "");
            if (dgObs.Rows.Count > 0)
                dgObs.Visible = true;
            else
            {
                dgObs.Visible = false;
                MessageBox.Show("No hay datos para el rango de fechas seleccionado");
            }
        }
    }
}
