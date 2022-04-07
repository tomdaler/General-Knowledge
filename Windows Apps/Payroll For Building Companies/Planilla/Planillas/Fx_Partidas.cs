using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Planilla.Planillas
{
    class Fx_Partidas
    {
        const int Activos = 3;
        const int Netos = 4;

        const int Salario = 5;
        const int Horas = 6;
        const int ValHora = 7;
        const int Grupo = 8;
        const int Adelanto = 9;
        const int Descuento = 10;
        const int Bono = 11;

        const int CantPart = 6;  // posicion de cantidad de grid Partidas

        public string UpdatePartida(int COL, string prevVal, DataGridViewRow dr)
        {
            string id = dr.Cells["id"].Value.ToString();
            string pla = dr.Cells["cod_pla"].Value.ToString();
            string empl = dr.Cells["cod_empl"].Value.ToString();
            string part = dr.Cells["cod_partida"].Value.ToString();

            string cant1 = dr.Cells["Cantidad"].Value.ToString();

            string ubic = dr.Cells["Ubicacion"].Value.ToString(); //8

            string sql = "update pla_partidas set ";

            if (COL == CantPart)
                sql = sql + " cantidad = " + cant1;
            else
                sql = sql + " ubicacion = '" + ubic +"' " ;

            if (id == "0")
            {
                sql = sql + " WHERE cod_pla  =" + pla;
                sql = sql + " and cod_empl   =" + empl;
                sql = sql + " and cod_partida=" + part;

                if (COL == CantPart)
                {
                    sql = sql + " and cantidad   =" + prevVal;
                    sql = sql + " and ubicacion  ='" + ubic + "'";
                }
            }
            else
                sql = sql + " WHERE id = " + id;

            Funciones fx = new Funciones();
            fx.UpdateData(sql);
           
            if (COL == CantPart)
            {
                double valor = Convert.ToDouble(dr.Cells["Valor"].Value.ToString());
                valor = valor * Convert.ToDouble(cant1);
                return valor.ToString();
            }
            return "";
        }

        public string DeletePartida(string total1, string total2, DataGridViewRow dr)
        {
            string id = dr.Cells["id"].Value.ToString();
            string pla = dr.Cells["cod_pla"].Value.ToString();
            string empl = dr.Cells["cod_empl"].Value.ToString();
            string part = dr.Cells["cod_partida"].Value.ToString();
            string cant = dr.Cells["Cantidad"].Value.ToString();
            string ubic = dr.Cells["Ubicacion"].Value.ToString();

            string sql = "Delete from pla_partidas ";
            if (id == "0")
            {
                sql = sql + " where cod_pla=" + pla;
                sql = sql + " and cod_Empl=" + empl;
                sql = sql + " and cod_partida=" + part;
                sql = sql + " and Cantidad=" + cant;
                sql = sql + " and Ubicacion = '" + ubic + "' ";
            }
            else
                sql = sql + " where id =" + id;

            Funciones fx = new Funciones();
            fx.UpdateData(sql);

            // TOTAL;
            
            total2 = total2.Replace(",", "");
            total1 = total1.Replace(",", "");
            double total3 = Convert.ToDouble(total2) - Convert.ToDouble(total1);
            return total3.ToString("#,##0.00");
        }

        public double Total(string cant, string valor)
        {
            // VERIFIQUE CANT ES DOUBLE()
            double cant1;
            double valor1;
            try
            {
                cant1 = Convert.ToDouble(cant);
            }
            catch (Exception)
            {
                MessageBox.Show("Digite un Numero en Cantidad");
                return -1;
            }

            try
            {
                valor1 = Convert.ToDouble(valor);
            }
            catch (Exception)
            {
                MessageBox.Show("Escoja una Partida");
                return -1;
            }

            return cant1 * valor1;
           
        }

        public void SetPartidas(DataGridView dg)
        {
            dg.Columns[0].Visible = false; //id
            dg.Columns[1].Visible = false; // cod_pla
            dg.Columns[2].Visible = false; // cod_empl
            dg.Columns[3].Visible = false; // cod_part

            dg.Columns[4].Width = 190;

            dg.Columns[5].Width = 55;
            dg.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dg.Columns[6].Width = 55;
            dg.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dg.Columns[7].Width = 55;
            dg.Columns[7].DefaultCellStyle = Variables.numero;

            dg.Columns[8].Width = 290;

            Funciones_Pla fxPla = new Funciones_Pla();

            for (int i = 4; i < dg.Columns.Count - 1; i++)
            {
                if (i != 6) fxPla.ReadOnly(dg, i);
                else fxPla.UpdateField(dg, i);
            }
        }

        public void SetPartidas2(DataGridView dg)
        {
            dg.Columns[Salario].ReadOnly = false;
            dg.Columns[Salario].DefaultCellStyle.BackColor = Color.White;

            dg.Columns[ValHora].ReadOnly = false;
            dg.Columns[ValHora].DefaultCellStyle.BackColor = Color.White;
        }
        public string SumarTotal(DataGridView dg)
        {
            double suma = 0;
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                suma = suma + System.Convert.ToDouble(dg.Rows[i].Cells[7].Value.ToString());
            }
            return suma.ToString("#,##0.00");
           
        }


        public DataTable NewDT()
        {
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("id", typeof(Int32));
            dtNew.Columns.Add("cod_pla", typeof(Int32));
            dtNew.Columns.Add("cod_empl", typeof(Int32));
            dtNew.Columns.Add("cod_partida", typeof(Int32));
            dtNew.Columns.Add("Partida", typeof(string));
            dtNew.Columns.Add("Cantidad", typeof(double));
            dtNew.Columns.Add("Valor", typeof(double));
            dtNew.Columns.Add("Total", typeof(double));
            dtNew.Columns.Add("Ubicacion", typeof(string));
            return dtNew;
        }
    }
}
