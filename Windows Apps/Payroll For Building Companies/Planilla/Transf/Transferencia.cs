using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Planilla
{
    public partial class Transferencia : Form
    {
        public Transferencia()
        {
            InitializeComponent();
        }

        private void Transferencia_Load(object sender, EventArgs e)
        {
            string sql = "Select cod_Pla, codigo +' - '+ proyecto as codigo from planillas p, proyecto y where Status = False and p.cod_proy = y.cod_proy ";
            Funciones fx = new Funciones();
            cbPla.DataSource = fx.GetData(sql, "");

            cbPla.DisplayMember = "codigo";
            cbPla.ValueMember = "cod_pla";
        }

        private void LoadAll()
        {
            string codigo = cbPla.Text;
            int pos = codigo.IndexOf(" -");
            codigo = codigo.Substring(0, pos);

            string sql = "select DUI, NIT, Proyecto, Nombre_usual as Nombre, t.Tipo, Bruto, d.Horas, Adelanto, d.Descuento ";
            sql = sql + " From Empleado e, pla_detalle d, tipo_pago t, proyecto y, planillas p ";
            sql = sql + " where e.Cod_empl = d.Cod_empl ";
            sql = sql + " and t.tipo_pago = e.tipo_pago    ";
            sql = sql + " and y.cod_proy = e.cod_proy ";
            sql = sql + " and d.cod_pla = p.cod_pla ";
            sql = sql + " and p.codigo = '" + codigo+ "' ";

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");

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

                dgPla.Columns[4].Width = 110;
                dgPla.Columns[5].Width = 50;

                for (int i = 5; i < dgPla.Columns.Count; i++)
                {
                    dgPla.Columns[i].DefaultCellStyle = currencyCellStyle;
                    dgPla.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!dgPla.Visible || dgPla.Rows.Count<1)
            {
                MessageBox.Show("Cargue una planilla para verificar primero");
                return;
            }

            Funciones fx = new Funciones();
            string codi = fx.GetDataValue("Select cod_pla from planillas where codigo ='" + cbPla.Text + "'");

            // BORRA PREVIA
            fx.UpdateData("Delete from pla_detalle where cod_pla=" + codi);

            DataTable dt = fx.GetData("select * from pla_detalle where codigo='" + cbPla.Text + "'", "2");

           
            foreach (System.Data.DataRow dr in dt.Rows)
            {
            
                // INSERT INTO PLA_DET
                string sql = "insert into pla_detalle(cod_pla, cod_empl, grupo, deducciones, horas, valorHora, tipo_pago, activo, cod_afp, cod_sind, aplica_renta, aplica_isss, Descuento ) ";

                sql = sql + "values (";
                sql = sql + codi + ",";
                sql = sql + dr["cod_empl"].ToString() + ","; // cod_empl
                sql = sql + dr["grupo"].ToString() + ","; // grupo
                sql = sql + dr["deducciones"].ToString() + ","; // deducciones
                sql = sql + dr["horas"].ToString() + ","; // deducciones
                sql = sql + dr["valorHora"].ToString() + ","; 
                sql = sql + "'"+ dr["tipo_pago"].ToString() + "',";

                // activo

                sql = sql +  dr["cod_afp"].ToString() + ",";
                sql = sql + dr["cod_sind"].ToString() + ",";
                sql = sql + dr["aplica_renta"].ToString() + ",";

                sql = sql + dr["aplica_isss"].ToString() + ",";
                sql = sql + dr["descuento"].ToString() + ") ";

                fx.UpdateData(sql);
            }
            MessageBox.Show("Actualizdo");
        }
    }
}
