using System;
using System.Windows.Forms;

namespace Planilla.RepTotal
{
    public partial class PorHoras : Form
    {
        public PorHoras()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT b.Codigo, Fecha2, sum(c.Horas) as Horas, sum(Bruto) as Bruto, Sum(Neto) as Neto ";
            sql = sql + " FROM  PLANILLAS b, Pla_detalle c";
            sql = sql + " WHERE c.tipo_pago = 'T' ";
            sql = sql + " and b.COD_PLA = c.COD_PLA ";

            sql = sql + Variables.DESDE + fechas.Fec1();
            sql = sql + Variables.HASTA + fechas.Fec2();

            sql = sql + " group by b.Codigo, Fecha2";

            Funciones fx = new Funciones();
            fx.Presentar(dgObs, 0,sql);
        }
    }
}
