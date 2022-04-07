using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planilla.RepMes
{
    public partial class Proyecto : Form
    {
        public Proyecto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ademas = ", a.Proyecto ";

            string sql = Variables.SELECT + ademas;
            sql = sql + ", Sum(Bruto) as MontoBruto, Sum(Neto) as MontoNeto, Sum(MonISSS) as ISSS, Sum(MonAFPEmpl) as AFP_E, Sum(MonAFPPatrono) as AFP_P, Sum(MonRenta) as Renta, Sum(cuota_sind) as Sindicato ";

            sql = sql + " FROM  planillas p, pla_detalle d, proyecto a ";
            sql = sql + " where p.cod_pla = d.cod_pla ";
            sql = sql + " and a.cod_proy = p.cod_proy ";

            sql = sql + Variables.DESDE + desde1.Fec1();

            sql = sql + Variables.GROUP + ademas;
            sql = sql + Variables.ORDER + ademas;

            Funciones fx = new Funciones();
            fx.Presentar2(dgObs, 0, sql);
        }

        private void Proyecto_Load(object sender, EventArgs e)
        {

        }
    }
}
