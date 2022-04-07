using System;
using System.Windows.Forms;

namespace Planilla.RepTotal
{
    public partial class PartidasMes : Form
    {
        public PartidasMes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql =Variables.SELECT+ ", c.Partidas, COUNT(*) AS Cantidad, SUM(a.VALOR* CANTIDAD) AS Total ";

            sql = sql + " FROM PLA_PARTIDAS a, PLANILLAS b, PARTIDAS c";
            sql = sql + " WHERE a.COD_PLA = b.COD_PLA ";
            sql = sql + " and a.cod_partida = c.cod_partida ";

            sql = sql + Variables.DESDE + fechas.Fec1();
            sql = sql + Variables.HASTA + fechas.Fec2();

            sql = sql + Variables.GROUP + ", c.Partidas ";

            Funciones fx = new Funciones();
            fx.Presentar2(dgObs, 3, sql);
            if (dgObs.Visible)
            {
                dgObs.Columns[2].Width = 210;
                dgObs.Columns[3].DefaultCellStyle = Variables.numero;
                
            }

            //Funciones fx = new Funciones();
            //dgObs.DataSource = fx.GetData(sql, "");
            //if (dgObs.Rows.Count > 0)
            //{
            //    dgObs.Visible = true;
            //    dgObs.Columns[0].Width = 50;
            //    dgObs.Columns[1].Width = 50;
            //    dgObs.Columns[2].Width = 210;
            //    dgObs.Columns[3].Width = 60;
            //    dgObs.Columns[4].Width = 70;


            //    dgObs.Columns[3].DefaultCellStyle = Variables.numero;
            //    dgObs.Columns[4].DefaultCellStyle = Variables.numero;

            //}
            //else
            //{
            //    dgObs.Visible = false;
            //    MessageBox.Show("No hay datos para el rango de fechas seleccionado");
            //}

        }
    }
}
