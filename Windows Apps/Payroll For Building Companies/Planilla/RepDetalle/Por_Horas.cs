using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planilla.RepDetalle
{
    public partial class Por_Horas : Form
    {
        public Por_Horas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT DUI, nombre_usual as Nombre, b.Codigo,  Fecha2, c.Horas, Bruto, Neto ";
            sql = sql + " FROM Empleado e, PLANILLAS b, Pla_detalle c";
            sql = sql + " WHERE c.tipo_pago = 'T' ";
            sql = sql + " and b.COD_PLA = c.COD_PLA ";
            sql = sql + " and e.cod_empl = c.cod_empl ";

            sql = sql + Variables.DESDE + fechas.Fec1();
            sql = sql + Variables.HASTA + fechas.Fec2();

            Funciones fx = new Funciones();
            dgObs.DataSource = fx.GetData(sql, "");
            if (dgObs.Rows.Count > 0)
                dgObs.Visible = true;
            else
            {
                dgObs.Visible = false;
                MessageBox.Show("No hay datos para el rango de fechas seleccionado");
            }
        }
    }
}
