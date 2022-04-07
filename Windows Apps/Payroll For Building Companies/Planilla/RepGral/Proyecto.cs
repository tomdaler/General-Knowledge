using System;
using System.Windows.Forms;

namespace Planilla.RepGral
{
    public partial class Proyecto : Form
    {
        public Proyecto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select a.Proyecto, ";
            sql = sql + "Sum(Bruto) as MontoBruto, Sum(Neto) as MontoNeto, Sum(MonISSS) as ISSS, Sum(MonAFPEmpl) as AFP_E, Sum(MonAFPPatrono) as AFP_P, Sum(MonRenta) as Renta, Sum(cuota_sind) as Sindicato ";

            sql = sql + " FROM  planillas p, pla_detalle d, proyecto a ";
            sql = sql + " where p.cod_pla = d.cod_pla ";
            sql = sql + " and a.cod_proy = p.cod_proy ";

            sql = sql + Variables.DESDE + rangoCtl1.Fec1();
            sql = sql + Variables.HASTA + rangoCtl1.Fec2();

            sql = sql + " group by a.Proyecto ";
           

            Funciones fx = new Funciones();
            fx.Presentar(dgObs, 1, sql);
            
        }
    }
}
