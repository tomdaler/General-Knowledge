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
    public partial class Varios : Form
    {
        public Varios()
        {
            InitializeComponent();
        }

        private DataTable Convert2(List<listado2> lista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cod", typeof(string));
            dt.Columns.Add("tx", typeof(string));

            foreach (listado2 l in lista)
            {
                ComboboxItem item = new ComboboxItem();

                item.Text = l.Text;
                item.Value = l.Value;

                dt.Rows.Add(item.Value, item.Text);
            }

            return dt;
        }
        private void Varios_Load(object sender, EventArgs e)
        {
            string sql = "Select cod_proy, Proyecto from Proyecto order by proyecto";
            Funciones fx = new Funciones();
            DataTable dt2 = fx.GetData(sql, "Todos");

            cbProy.DataSource = dt2;
            cbProy.DisplayMember = "Proyecto";
            cbProy.ValueMember = "cod_proy";


            sql = "Select cod_afp, afp from AFP";
            dt2 = fx.GetData(sql, "Todos");

            cbAFP.DataSource = dt2;
            cbAFP.DisplayMember = "AFP";
            cbAFP.ValueMember = "cod_afp";


            sql = "Select cod_sind, Sindicato from Sindicato";
            dt2 = fx.GetData(sql, "Todos");

            cbSind.DataSource = dt2;
            cbSind.DisplayMember = "Sindicato";
            cbSind.ValueMember = "cod_sind";

            sql = "Select tipo_pago, tipo from tipo_pago where activo=True";
            dt2 = fx.GetData(sql, "Todos");

            cbTipo.DataSource = dt2;
            cbTipo.DisplayMember = "tipo";
            cbTipo.ValueMember = "tipo_pago";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sql = "SELECT DUI, NIT, nombre_usual as nombre, e.Activo, e.Telefono, Proyecto, Sindicato, a.AFP, Tipo ";

            sql = sql + " FROM EMPLEADO E, PROYECTO p, Sindicato s, AFP a, tipo_pago t";
            sql = sql + " WHERE E.cod_proy = p.cod_proy";
            sql = sql + " and e.cod_afp = a.cod_afp";
            sql = sql + " and e.cod_sind = s.cod_sind";
            sql = sql + " and e.tipo_pago = t.tipo_pago";

            string proy = cbProy.SelectedValue.ToString();
            string afp = cbAFP.SelectedValue.ToString();
            string tipo = cbTipo.SelectedValue.ToString();
            string sind = cbSind.SelectedValue.ToString();

            if (proy != "0") sql = sql + " and e.cod_proy="+ proy;
            if (afp  != "0") sql = sql + " and e.cod_afp=" + afp;
            if (tipo != "0") sql = sql + " and e.tipo_pago = '" + tipo + "' ";
            if (sind != "0") sql = sql + " and e.cod_sind=" + sind;

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");
            if (dt.Rows.Count > 0)
            {
                dgObs.DataSource = dt;
                dgObs.Visible = true;
            }
            else
                dgObs.Visible = false;
        }
    }
}
