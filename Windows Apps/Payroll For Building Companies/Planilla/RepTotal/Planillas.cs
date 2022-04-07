using System;
using System.Data;
using System.Windows.Forms;

namespace Planilla.RepTotal
{
    public partial class Planillas : Form
    {
        public Planillas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select Codigo, y.Proyecto as Proyecto, Fecha1, Fecha2, ";
            sql = sql + "Sum(Bruto) as Bruto, Sum(Neto) as Neto, Sum(MonISSS) as ISSS, Sum(MonAFPEmpl) as AFP_E, Sum(MonAFPPatrono) as AFP_P, Sum(MonRenta) as Renta, Sum(cuota_sind) as Sindicato ";

            sql = sql + " FROM  proyecto y, planillas p, pla_detalle d ";
            sql = sql + " where p.cod_pla = d.cod_pla ";
            sql = sql + " and p.Cod_proy = y.cod_proy ";

            sql = sql + Variables.DESDE + desde1.Fec1();

            //sql = sql + " and fecha1 Between Date() and Date()-360 ";

            sql = sql + " group by Codigo, Fecha1, Fecha2, y.Proyecto ";
            sql = sql + " order by Fecha1 desc ";
            
            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");
            if (dt.Rows.Count > 0)
            {
                dgObs.DataSource = dt;
                dgObs.Visible = true;
                dgObs.Columns[1].Width = 130;

                DataGridViewCellStyle estilo = new DataGridViewCellStyle();
                estilo.Format = "C";
                estilo.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //estilo.ForeColor = Color.Green;

                for (int i = 0; i < 5; i++)
                {
                    dgObs.Columns[i].Width = 90;
                }

                for (int i = 4; i < dgObs.Columns.Count; i++)
                {
                    dgObs.Columns[i].DefaultCellStyle = estilo;
                    dgObs.Columns[i].Width = 90;
                }
            }
            else
            {
                dgObs.Visible = false;
                MessageBox.Show("No se encontro datos");
            }
        }
    }
}
