using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Planilla
{
    public class ComboboxItem
    {
      
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    public class listado
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
    }

    public class listado2
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
    }


    class Variables
    {
        public static string SELECT = "SELECT Year(Fecha2)*100+Month(Fecha2), '' as Fecha ";

        public static string DESDE = " and  (Year(fecha2)*100+Month(fecha2)) >=";
        public static string HASTA = " and  (Year(fecha2)*100+Month(fecha2)) <=";

        public static string GROUP = " GROUP BY Year(Fecha2)*100+Month(Fecha2) ";
        public static string ORDER = " ORDER BY Year(Fecha2)*100+Month(Fecha2) DESC ";

        public static List<listado> AFP = new List<listado>();
        public static List<listado2> Tipo = new List<listado2>();
       
        public static DataTable dtNew = new DataTable();

        public static DataGridViewCellStyle numero = new DataGridViewCellStyle();
        public static int NIVEL;
        public static ComboBox  Meses;
        public static void LoadCodes()
        {
            numero.Format = "N";
            numero.ForeColor = Color.Green;
            numero.Alignment = DataGridViewContentAlignment.MiddleCenter;


            Funciones fx = new Funciones();
         
            System.Data.IDataReader dr2 = fx.GetReader("Select cod_afp, AFP from AFP");
            while (dr2.Read())
               AFP.Add(new listado() { Value = System.Convert.ToInt32(dr2[0].ToString()), Text = dr2[1].ToString() });

            System.Data.IDataReader dr3 = fx.GetReader("Select tipo_pago, tipo from tipo_pago where activo=True");
            while (dr3.Read())
              Tipo.Add(new listado2() { Value = dr3[0].ToString(), Text = dr3[1].ToString() });
                              
            dtNew.Columns.Add("cod", typeof(Int32));
            dtNew.Columns.Add("Nombre", typeof(string));
            dtNew.Columns.Add("Tipo", typeof(string));
            dtNew.Columns.Add("Activo", typeof(bool));
            dtNew.Columns.Add("Neto", typeof(double));
            dtNew.Columns.Add("Salario", typeof(double));
            dtNew.Columns.Add("Horas", typeof(Int32));
            dtNew.Columns.Add("ValorHora", typeof(double));
            dtNew.Columns.Add("Grupo", typeof(byte));
            dtNew.Columns.Add("Adelanto", typeof(double));
            dtNew.Columns.Add("Descuento", typeof(double));
            dtNew.Columns.Add("Bono", typeof(double));
            dtNew.Columns.Add("Inas1", typeof(int));
            dtNew.Columns.Add("Inas2", typeof(int));

            Meses = new ComboBox();
            Meses.Items.Insert(0, "Enero");
            Meses.Items.Insert(1, "Febrero");
            Meses.Items.Insert(2, "Marzo");
            Meses.Items.Insert(3, "Abril");
            Meses.Items.Insert(4, "Mayo");
            Meses.Items.Insert(5, "Junio");
            Meses.Items.Insert(6, "Julio");
            Meses.Items.Insert(7, "Agosto");
            Meses.Items.Insert(8, "Septiembre");
            Meses.Items.Insert(9, "Octubre");
            Meses.Items.Insert(10, "Noviembre");
            Meses.Items.Insert(11, "Diciembre");

            //    dtNew = fx.GetData("Select top 1 from pla_detalle", "");
            //    System.Data.DataRow dr = (System.Data.DataRow)dtNew.NewRow();
        }
    }
}
