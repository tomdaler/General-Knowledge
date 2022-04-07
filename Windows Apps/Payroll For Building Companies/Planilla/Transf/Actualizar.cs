using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planilla.Transf
{
    public partial class Actualizar : Form
    {
        public Actualizar()
        {
            InitializeComponent();
        }

        private void Actualizar_Load(object sender, EventArgs e)
        {

        }

        private DataTable LoadUpdate(int option)
        {
            DateTime fec = picker.Value;

            Funciones fx = new Funciones();
            string fecha = fx.WhereDateAccess(fec);

            string sql = "select DUI, NIT, nombre_usual as Nombre, Modificado from empleado ";

            if (option == 2) sql = "select * from empleado ";
            sql = sql + " where modificado >= " + fecha + " ";

            return fx.GetData(sql, "2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = LoadUpdate(1);
            if (dt.Rows.Count > 0)
            {
                dg1.DataSource = dt;
                dg1.Visible = true;
                dg1.Columns[1].Width = 130;
            }
            else
                dg1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = LoadUpdate(2);
            foreach(System.Data.DataRow dr in dt.Rows)
            {
                string empl = dr["cod_empl"].ToString();
                           

                string sql = " update empleado set ";
                sql = sql + " DUI = '" + dr["DUI"].ToString() + "', ";
                sql = sql + " NIT = '" + dr["NIT"].ToString() + "', ";
                sql = sql + " ISSS = '" + dr["ISSS"].ToString() + "', ";
                sql = sql + " AFP = '" + dr["AFP"].ToString() + "', ";
                sql = sql + " Descuento = " + dr["Descuento"].ToString() + ", ";
                //sql = sql + varios;

                sql = sql + " RazonDesc ='" + dr["RazonDesc"].ToString() + "', ";

                sql = sql + " nombres = '" + dr["nombres"].ToString() + "', ";
                sql = sql + " apellidos = '" + dr["apellidos"].ToString() + "', ";
                sql = sql + " nombre_usual= '" + dr["nombre_usual"].ToString() + "', ";

                sql = sql + " telefono = '" + dr["telefono"].ToString() + "', ";
                sql = sql + " avisar = '" + dr["avisar"].ToString() + "', ";
                sql = sql + " celular = '" + dr["celular"].ToString() + "', ";

                DateTime fec = DateTime.Today;
                string hoy = fec.ToString();
                int pos1 = hoy.IndexOf(" ");
                hoy = hoy.Substring(0, pos1);

                sql = sql + " modificado ='" + hoy + "', ";
                sql = sql + " cod_proy = " + dr["cod_proy"].ToString() + ", ";
                sql = sql + " cod_proy2 = " + dr["cod_proy2"].ToString() + ", ";
                sql = sql + " cod_proy3 = " + dr["cod_proy3"].ToString() + ", ";
               
                sql = sql + " cod_sind = " + dr["cod_sind"].ToString() + ", ";

                sql = sql + " cod_afp = " + dr["cod_afp"].ToString() + ", ";
                sql = sql + " cod_isss = " + dr["cod_isss"].ToString() + ", ";
                sql = sql + " tipo_pago = '" + dr["tipo_pago"].ToString() + "', ";


                sql = sql + " cta_banco ='" + dr["cta_banco"].ToString() + "', ";
                sql = sql + " tip_cta ='" + dr["tip_cta"].ToString() + "', ";
                sql = sql + " observacion ='" + dr["observacion"].ToString() + "', ";

                sql = sql + " activo =" + dr["activo"].ToString() + ", ";
                //sql = sql + " renta =" + dr["renta"].ToString() + ", ";

                sql = sql + " Dir1 = '" + dr["Dir1"].ToString() + "', ";
                sql = sql + " Dir2 = '" + dr["Dir1"].ToString() + "' ";

                sql = sql + " where  cod_empl = " + empl;

                Funciones fx = new Funciones();
                fx.UpdateData(sql);

            }
            MessageBox.Show("Actualizados");
        }
    }
}
