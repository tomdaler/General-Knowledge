using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Planilla.RepDetalle
{
    public partial class Vacaciones : Form
    {
        public Vacaciones()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select DUI, nombre_usual as Nombre, Proyecto, d.Bruto, d.Neto, d.Adelanto, codigo as Planilla, Fecha1, Fecha2 ";
            sql = sql + " FROM Empleado e, proyecto , planillas p, pla_detalle d";
            sql = sql + " where p.cod_pla = d.cod_pla ";
            sql = sql + " and e.cod_proy = proyecto.cod_proy ";
            sql = sql + " and d.cod_empl = e.cod_empl ";
            sql = sql + " and d.tipo_pago= 'V' ";

            sql = sql + Variables.DESDE + rangoCtl1.Fec1();
            sql = sql + Variables.HASTA + rangoCtl1.Fec2();

            sql = sql + " order by fecha2 desc ";

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");

            if (dt.Rows.Count > 0)
            {
                dgObs.DataSource = dt;
                dgObs.Visible = true;
                dgObs.Columns[1].Width = 130;
            }
            else
            {
                dgObs.Visible = false;
                MessageBox.Show("No hay data para este rango de fechas");
            }
        }
    }
}
