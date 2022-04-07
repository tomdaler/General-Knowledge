
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Planilla
{
    class Funciones_Pla
    {
        const int Salario = 5;
        const int Horas = 6;
        const int ValHora = 7;
        const int Grupo = 8;
        const int Adelanto = 9;
        const int Descuento = 10;
        const int Bono = 11;


        public void UpdateGridCells(int cual, string GEmpl, string GSalario, DataGridView dgPla, string NewVal, DataGridView dg, int row, string Gcodi, int COL)
        {
            if (Variables.NIVEL > 2) return;

            if (GEmpl == "") return;

            string empl = GEmpl;
            string Valor = GSalario;

            Valor = Valor.Replace(",", "");
            if (NewVal == Valor) return;
            
            if (dg != null)
            {
                try
                {
                    Valor = dg.Rows[row].Cells[COL].Value.ToString();
                    empl = dg.Rows[row].Cells[0].Value.ToString();
                }
                catch (Exception)
                {
                    return;
                }
            }

            NewVal = "";
            string where = Valor + " where cod_pla=" + Gcodi + " and cod_empl=" + empl;

            Funciones fx = new Funciones();

            DataTable dt = Variables.dtNew;

            string nombre = dt.Columns[COL].ColumnName;

            string sql = "Update pla_detalle set " + nombre + " = " + where;

            for (int i = 0; i < dgPla.Rows.Count; i++)
            {
                string empl2 = dgPla.Rows[i].Cells[0].Value.ToString();
                if (empl2 == empl)
                {
                    string tipo = dgPla.Rows[i].Cells[3].Value.ToString();
                    if (tipo != "Eventual" && tipo != "Vacacion")
                    {
                        dgPla.Rows[i].Cells[COL].Value = Valor;
                    }
                }
            }
        }

        public DataTable Filter(DataTable dt, string opcion)
        {
            DataTable dt2 = null;
            try
            {
                dt2 = dt.AsEnumerable()
                   .Where(i => i.Field<String>("Tipo") == opcion)
                  .OrderByDescending(i => i.Field<String>("Nombre"))
                  .CopyToDataTable();
            }
            catch (Exception) { }
            return dt2;
        }
        public bool SiEstaAsignado(string empl, string tipo_pago, DataGridView dg)
        {
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                string emp = dg.Rows[i].Cells[0].Value.ToString();
                if (emp == empl)
                {
                    i++;
                    if (tipo_pago == "'E'") MessageBox.Show("Empleado ya esta asignado, Fila " + i.ToString());

                    if (tipo_pago == "'L'") MessageBox.Show("Empleado ya esta asignado para pago de Liquidacion, Fila " + i.ToString());

                    if (tipo_pago == "'C'") MessageBox.Show("Empleado ya esta asignado para pago de Vacacion, Fila " + i.ToString());
                    return true;
                }
            }

            if (tipo_pago == "'E") return false;

            string sql = "select p.Codigo from planillas p,  pla_detalle d ";
            sql = sql + " where p.Status = 0 ";
            sql = sql + " and  p.cod_pla = d.cod_pla ";
            sql = sql + " and cod_empl=" + empl + " and tipo_pago=" + tipo_pago;

            Funciones fx = new Funciones();
            string vv = fx.GetDataValue(sql);
            if (vv == null) return false;

            if (vv.Length > 1)
            {

                if (tipo_pago == "'L'")
                    MessageBox.Show("Empleado ya esta asignado para Liquidacion en planilla " + vv);

                if (tipo_pago == "'C'")
                    MessageBox.Show("Empleado ya esta asignado para Pago de Vaciones en planilla " + vv);

                return true;
            }

            return false;
        }

        public void Agregue_Fila_Grid(DataGridView dg, string empl, string nombre, string tipo, string pagar, string descu)
        {
            dg.Visible = true;

            DataTable dt = (DataTable)dg.DataSource;
            if (dt == null) dt = Variables.dtNew;

            System.Data.DataRow dr = dt.NewRow();

            dr[0] = empl;
            dr[1] = nombre;
            dr[2] = tipo;
            dr[3] = 1;
            dr[Salario] = pagar;
            dr[Descuento] = descu;

            for (int i = 6; i < 12; i++) dr[i] = 0;

            dr[Descuento] = descu;

            dt.Rows.Add(dr);
            dg.DataSource = dt;
        }

        public void ReadOnly(DataGridView dg, int col)
        {
            dg.Columns[col].Visible = true;
            dg.Columns[col].ReadOnly = true;
            dg.Columns[col].DefaultCellStyle.BackColor = Color.Wheat;
        }

        public void UpdateField(DataGridView dg, int col)
        {
            dg.Columns[col].ReadOnly = false;
            dg.Columns[col].DefaultCellStyle.BackColor = Color.White;
        }
        public void SetGrid2(DataGridView dg)
        {
            dg.Visible = true;
            dg.Columns[0].Visible = false;

            dg.Columns[12].Visible = false;
            dg.Columns[13].Visible = false;


            for (int i = 1; i < 5; i++) ReadOnly(dg, i);
            
            for (int i = 2; i < dg.Columns.Count; i++)
            {
                dg.Columns[i].Width = 70;
                dg.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            DataGridViewCellStyle NetoStyle = new DataGridViewCellStyle();
            NetoStyle.Format = "N";
            NetoStyle.ForeColor = Color.Green;
            NetoStyle.BackColor = Color.Wheat;

           
            dg.Columns[Salario - 1].DefaultCellStyle = NetoStyle;
            dg.Columns[Salario - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
     
            dg.Columns[Salario].DefaultCellStyle = Variables.numero;
        }

        public void InsertPlaDetalle(string Gcodi, string codEmpl, string Monto, string Descuento, string tipo)
        {
            Funciones fx = new Funciones();
            string sql = "insert into pla_detalle(cod_pla, cod_empl, Bruto, horas, valorHora, tipo_pago, activo, APLICA_isss, cod_afp, cod_sind, aplica_renta, grupo, Descuento ) ";
            sql = sql + " select " + Gcodi + ", cod_empl, " + Monto + ", 0, 0, " + tipo + " cod_isss, cod_afp, cod_sind, renta, 0, " + Descuento + " FROM EMPLEADO where cod_empl =" + codEmpl;

            fx.UpdateData(sql);
        }

    }
}
