using System;
using System.Data;
using System.Windows.Forms;

namespace Planilla.RepTotal
{
    public partial class AFP_Mes : Form
    {
        public AFP_Mes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ademas = ", a.AFP ";

            string sql = Variables.SELECT + ademas;
            sql = sql + ",Sum(Bruto) as MontoBruto, Sum(Neto) as MontoNeto, Sum(MonISSS) as ISSS, Sum(MonAFPEmpl) as AFP_E, Sum(MonAFPPatrono) as AFP_P, Sum(MonRenta) as Renta, Sum(cuota_sind) as Sindicato ";

            sql = sql + " FROM  planillas p, pla_detalle d, AFP a ";

            sql = sql + " WHERE p.cod_pla = d.cod_pla ";
            sql = sql + " AND   a.cod_afp = d.cod_afp ";

            sql = sql + Variables.DESDE + rangoCtl1.Fec1();
            sql = sql + Variables.HASTA + rangoCtl1.Fec2();

            sql = sql + Variables.GROUP + ademas;
            sql = sql + Variables.ORDER + ademas;

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");
            if (dt.Rows.Count > 0)
            {
                dgObs.DataSource = fx.TableFecha(dt);
                dgObs.Visible = true;
                dgObs.Columns[1].Width = 130;
                dgObs.Columns[0].Visible = true;

                DataGridViewCellStyle estilo = new DataGridViewCellStyle();
                estilo.Format = "C";
                estilo.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                for (int i = 2; i < dgObs.Columns.Count; i++)
                {
                    dgObs.Columns[i].DefaultCellStyle = estilo;
                    dgObs.Columns[i].Width = 90;
                }
            }
            else
                dgObs.Visible = false;
        }
    }
}
