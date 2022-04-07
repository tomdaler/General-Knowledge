using Planilla.Planillas;
using System;
using System.Data;
using System.Windows.Forms;

namespace Planilla
{
    public partial class PlanillaAjuste : Form
    {
        public PlanillaAjuste()
        {
            InitializeComponent();
        }

        private void LoadAll()
        {
            string sql = "select DUI, NIT, Proyecto, Nombre_usual as Nombre, t.Tipo,  Valor, d.Renta, Aplicado ";
            sql = sql + " From Empleado e, Ajuste_Renta d, tipo_pago t, Proyecto y ";
            sql = sql + " where e.Cod_empl = d.Cod_empl ";
            sql = sql + " and t.tipo_pago = e.tipo_pago    ";
            sql = sql + " and y.cod_proy = e.cod_proy ";

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");

            DataRow drNew = dt.NewRow();

            if (dt.Rows.Count > 0)
            {
                dgPla.DataSource = dt;

                dgPla.Visible = true;

                for (int i = 0; i < dgPla.Columns.Count; i++)
                {
                    if (i != 6) dgPla.Columns[i].ReadOnly = true;
                }

                dgPla.Columns[4].Width = 110;
                dgPla.Columns[5].Width = 50;

                for (int i = 5; i < dgPla.Columns.Count; i++)
                {
                    dgPla.Columns[i].DefaultCellStyle = Variables.numero;
                }
            }
        }
        private void PlanillaAjuste_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void bt22_Click(object sender, EventArgs e)
        {
            Funciones fx = new Funciones();
            fx.UpdateData("delete from Ajuste_Renta");

            FxCalculo fx2 = new FxCalculo();
            fx2.Reajuste();

            LoadAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Funciones fx = new Funciones();
            fx.UpdateData("delete from Ajuste_Renta");
            dgPla.Visible = false;
        }
    }
}
