using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planilla.Transf
{
    public partial class CrearExcel : Form
    {
        public CrearExcel()
        {
            InitializeComponent();
        }

        private void CrearExcel_Load(object sender, EventArgs e)
        {
            string sql = "Select cod_Pla, codigo +' - '+ proyecto as codigo from planillas p, proyecto y where Status = False and p.cod_proy = y.cod_proy ";
            Funciones fx = new Funciones();
            cbPla.DataSource = fx.GetData(sql, "");

            cbPla.DisplayMember = "codigo";
            cbPla.ValueMember = "cod_pla";
        }

        private void LoadAll()
        {
            string codigo2 = cbPla.Text;
            int pos = codigo2.IndexOf(" -");
            string codigo = codigo2.Substring(0, pos);
            string proyecto = codigo2.Substring(pos + 3);

            string sql = "select DUI, Nombre_usual as Nombre, t.Tipo, d.Horas, Adelanto, d.Descuento ";
            sql = sql + " From Empleado e, pla_detalle d, tipo_pago t, planillas p ";
            sql = sql + " where e.Cod_empl = d.Cod_empl ";
            sql = sql + " and t.tipo_pago = e.tipo_pago    ";
            sql = sql + " and d.cod_pla = p.cod_pla ";
            sql = sql + " and p.codigo = '" + codigo + "' ";

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");

            DataRow dr = dt.NewRow();
            dr[0] = "CODIGO";
            dr[1] = proyecto;
            dr[2] = codigo;
            dt.Rows.InsertAt(dr, 0); 
            //dt.Rows.Add(dr);

            if (dt.Rows.Count > 0)
            {
                dgPla.DataSource = dt;

                DataGridViewCellStyle currencyCellStyle = new DataGridViewCellStyle();
                currencyCellStyle.Format = "N";
                currencyCellStyle.ForeColor = Color.Green;
                dgPla.Visible = true;

                for (int i = 0; i < dgPla.Columns.Count; i++)
                {
                    if (i != 6) dgPla.Columns[i].ReadOnly = true;
                }

                dgPla.Columns[3].Width = 80;
                dgPla.Columns[4].Width = 80;
                dgPla.Columns[5].Width = 80;

                for (int i = 3; i < dgPla.Columns.Count; i++)
                {
                    dgPla.Columns[i].DefaultCellStyle = currencyCellStyle;
                    dgPla.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void cbPla_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadAll();
        }
    }
}
