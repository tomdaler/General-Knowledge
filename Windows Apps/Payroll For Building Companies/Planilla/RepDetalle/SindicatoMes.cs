using System;
using System.Windows.Forms;

namespace Planilla.RepDetalle
{
    public partial class SindicatoMes : Form
    {
        public SindicatoMes()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select DUI, nombre_usual , ";
            sql = sql + "Sum(Bruto) as MontoBruto, Sum(Neto) as MontoNeto, Sum(d.cuota_sind) as Sindicato ";

            sql = sql + " FROM  planillas p, pla_detalle d, Empleado e ";

            sql = sql + " WHERE p.cod_pla = d.cod_pla ";
            sql = sql + " AND   d.cod_empl = d.cod_empl ";

            sql = sql + " and  Year(fecha2)=" + fechas.anio();
            sql = sql + " and  Month(fecha2) =" + fechas.mes();

            sql = sql + " group by DUI, nombre_usual ";
           
            Funciones fx = new Funciones();
            fx.Presentar(dgObs, 2, sql);
        }
    }
}
