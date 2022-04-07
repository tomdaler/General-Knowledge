using System;
using System.Windows.Forms;

namespace Planilla.RepTotal
{
    public partial class PlanillaMes : Form
    {
        public PlanillaMes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string sql = Variables.SELECT;
            sql = sql + ",Sum(Bruto) as MontoBruto, Sum(Neto) as MontoNeto, Sum(MonISSS) as ISSS, Sum(MonAFPEmpl) as AFP_E, Sum(MonAFPPatrono) as AFP_P, Sum(MonRenta) as Renta, Sum(cuota_sind) as Sindicato ";

            sql = sql + " FROM  planillas p, pla_detalle d ";
            sql = sql + " where p.cod_pla = d.cod_pla ";

            sql = sql + Variables.DESDE + desde1.Fec1();

            sql = sql + Variables.GROUP + Variables.ORDER;

            Funciones fx = new Funciones();
            fx.Presentar2(dgObs, 0, sql);
        }
    }
}
