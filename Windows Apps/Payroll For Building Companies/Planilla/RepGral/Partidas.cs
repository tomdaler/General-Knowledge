using System;
using System.Collections;
using System.Windows.Forms;

namespace Planilla.RepTotal
{
    public partial class Partidas : Form
    {
        public Partidas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT c.PARTIDAS, COUNT(*) AS CANTIDAD, AVG(a.Valor) as VALOR, SUM(a.VALOR* CANTIDAD) AS TOTAL ";
            sql = sql + " FROM PLA_PARTIDAS a, PLANILLAS b, PARTIDAS c";
            sql = sql + " WHERE a.COD_PLA = b.COD_PLA ";
            sql = sql + " and a.cod_partida = c.cod_partida ";

            sql = sql + Variables.DESDE + fechas.Fec1();
            sql = sql + Variables.HASTA + fechas.Fec2();

            sql = sql + " GROUP BY c.Partidas ";

            Funciones fx = new Funciones();
            dgObs.DataSource = fx.GetData(sql, "");
            if (dgObs.Rows.Count > 0)
            {
                dgObs.Visible = true;
                dgObs.Columns[0].Width = 220;

                dgObs.Columns[1].Width = 80;
                dgObs.Columns[2].Width = 80;
                dgObs.Columns[3].Width = 80;

                dgObs.Columns[1].DefaultCellStyle = Variables.numero;
                dgObs.Columns[2].DefaultCellStyle = Variables.numero;
                dgObs.Columns[3].DefaultCellStyle = Variables.numero;

            }
            else
            {
                dgObs.Visible = false;
                MessageBox.Show("No hay datos para el rango de fechas seleccionado");
            }
        }
    }
}
