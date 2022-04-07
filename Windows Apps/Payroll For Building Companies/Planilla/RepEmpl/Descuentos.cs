using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planilla.RepEmpl
{
    public partial class Descuentos : Form
    {
        public Descuentos()
        {
            InitializeComponent();
        }

        private void Descuentos_Load(object sender, EventArgs e)
        {
            string sql = "select nombre_usual as Nombre, Descuento, RazonDesc as Razon from Empleado where Descuento>0";
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
