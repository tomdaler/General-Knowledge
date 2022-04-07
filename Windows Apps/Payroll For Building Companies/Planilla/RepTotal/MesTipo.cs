using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Planilla.RepTotal
{
    public partial class MesTipo : Form
    {
        public MesTipo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ademas = ", a.Tipo ";
            string sql = Variables.SELECT + ademas;
            sql = sql + ",Sum(Bruto) as MontoBruto, Sum(Neto) as MontoNeto, Sum(MonISSS) as ISSS, Sum(MonAFPEmpl) as AFP_E, Sum(MonAFPPatrono) as AFP_P, Sum(MonRenta) as Renta, Sum(cuota_sind) as Sindicato ";

            sql = sql + " FROM  planillas p, pla_detalle d, tipo_pago a ";
            sql = sql + " where p.cod_pla = d.cod_pla ";
            sql = sql + " and a.tipo_pago = d.Tipo_pago";

            sql = sql + Variables.DESDE + desde1.Fec1();
           
            sql = sql + Variables.GROUP + ademas;
            sql = sql + Variables.ORDER + ademas;
            
            Funciones fx = new Funciones();
            fx.Presentar2(dgObs, 0, sql);
        }
            
    }
}
