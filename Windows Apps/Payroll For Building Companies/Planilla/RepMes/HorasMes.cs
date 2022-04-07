using System;
using System.Windows.Forms;

namespace Planilla.RepTotal
{
    public partial class HorasMes : Form
    {
        public HorasMes()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = Variables.SELECT+ ", sum(c.Horas) as Horas, sum(Bruto) as Bruto, Sum(Neto) as Neto ";
            sql = sql + " FROM  PLANILLAS b, Pla_detalle c";
            sql = sql + " WHERE c.tipo_pago = 'T' ";
            sql = sql + " and b.COD_PLA = c.COD_PLA ";

            sql = sql + Variables.DESDE + rangoCtl1.Fec1();
            sql = sql + Variables.HASTA + rangoCtl1.Fec2();

            sql = sql + Variables.GROUP;

            Funciones fx = new Funciones();
            fx.Presentar(dgObs, 1, sql);
        }
    }
}
