using System;
using System.Windows.Forms;

namespace Planilla
{
    public partial class Pla_Nueva : Form
    {
        public Pla_Nueva()
        {
            InitializeComponent();
        }

        private void Pla_Nueva_Load(object sender, EventArgs e)
        {
            
          
        }

     
        private void LoadPlanillas()
        { 
            Funciones fx = new Funciones();
            
            string sql = "Select top 15 cod_pla, Codigo, Fecha1, Fecha2, Status as Terminada, Aguinaldo,Sindicato from PLanillas order by cod_pla desc";

            System.Data.DataTable dt2 = fx.GetData(sql, "");

            dGrid1.DataSource = dt2;
            dGrid1.Visible = true;
            panel2.Visible = true;

            if (dt2.Rows.Count>0)
                dGrid1.Columns[0].Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
                        
            string cod = txCod.Text.Trim();
            if (cod=="")
            {
                MessageBox.Show("Digite un codigo");
                return;
            }
            if (cod.Length>10)
            {
                MessageBox.Show("Digite un codigo mas corto");
                return;
            }

          
            string fec1 = dateTimePicker1.Value.ToString();

            DateTime ff = System.Convert.ToDateTime(fec1);
            DateTime fec2 = ff.AddDays(30);

            string fec11 = ff.ToString();
            string fec22 = fec2.ToString();

            int pos1 = fec11.IndexOf(" ");
            fec11 = fec11.Substring(0, pos1);

            pos1 = fec22.IndexOf(" ");
            fec22 = fec22.Substring(0, pos1);//
            label8.Text = fec22;


            string sql = "INSERT INTO Planillas (Fecha1, Fecha2, Codigo, Aguinaldo,Sindicato, Dias) values (";
            
            sql = sql + fec11 + ", ";
            sql = sql + fec22 + ", ";
            sql = sql + "'" + txCod.Text.Trim() + "', ";

            if (ck.Checked) sql = sql + "1,";
            else sql = sql + "0,";

            if (ck2.Checked) sql = sql + "1,";
            else sql = sql + "0,";

            sql = sql + "30)";

            Funciones fx = new Funciones();
            fx.UpdateData(sql);


            sql = "select max(cod_pla) from planillas";
            string codPla = fx.GetDataValue(sql);

            string tipo_pago = "tipo_pago, activo,";

            if (ck.Checked) tipo_pago = "F,";
            
            sql = "insert into pla_detalle(cod_pla, cod_empl, Bruto,  horas, valorHora,   tipo_pago,  activo, cod_afp, cod_sind, aplica_renta, aplica_isss, grupo, Descuento ) ";
            sql = sql + " select " + codPla +    ", cod_empl, Sueldo, horas, valorHora,"+ tipo_pago + "  1,   cod_afp, cod_sind, renta,        cod_isss,    grupo, Descuento FROM EMPLEADO  ";
            sql = sql + " where tipo_pago<>'E' ";

            // LIQUIDAR , AGUINALDOS SOLO ACTIVOS
            if (tipo_pago == "L," || tipo_pago =="F,") sql = sql + "and Empleado.Activo=1";

            fx.UpdateData(sql);

            LoadPlanillas();
            MessageBox.Show("Agregada");
        }
    }
}
