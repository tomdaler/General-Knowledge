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
    public partial class Eventuales : Form
    {
        public Eventuales()
        {
            InitializeComponent();
        }

        private void Eventuales_Load(object sender, EventArgs e)
        {
            string sql = "select DUI, nombre_usual as Nombre, Proyecto, d.Bruto, d.Neto, d.Adelanto, codigo as Planilla, Fecha1, Fecha2 ";
            sql = sql + " FROM Empleado e, proyecto , planillas p, pla_detalle d";
            sql = sql + " where p.cod_pla = d.cod_pla ";
            sql = sql + " and e.cod_proy = proyecto.cod_proy ";
            sql = sql + " and d.cod_empl = e.cod_empl ";
            sql = sql + " and ( d.tipo_pago= 'E' ) ";

            sql = sql + " and fecha1 Between Date() and Date()-360 ";
            // sql = sql + " and fecha1 Between #3/1/2004# And #6/30/2010#";
            sql = sql + " order by fecha1 desc ";

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");

            if (dt.Rows.Count > 0)
            {
                dgObs.DataSource = dt;
                dgObs.Visible = true;
                dgObs.Columns[1].Width = 130;
            }
            else
                dgObs.Visible = false;
        }
    }
}
