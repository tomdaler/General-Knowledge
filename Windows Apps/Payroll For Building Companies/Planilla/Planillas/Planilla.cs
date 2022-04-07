using Planilla.Planillas;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Planilla
{
    public partial class Planilla : Form
    {
        string Gcodi = "";
        string Gproy = "";
        string GEmpl = "";
        string GSalario = "";
        int GRow = 0;

        string fec1 = "";
        string fec2 = "";

        const int Tipos = 2;
        const int Activos = 3;
        const int Netos = 4;
        const int Salario = 5;
        const int Horas = 6;
        const int ValHora = 7;
        const int Grupo = 8;
        const int Adelanto = 9;
        const int Descuento = 10;
        const int Bono = 11;

        const int Inas1 = 12;
        const int Inas2 = 13;

        bool agui;
        int status;
        string dias = "";

        string NewVal = "";

        DataTable dtObra = new DataTable();
        DataTable dtPartidas = new DataTable();
        public Planilla()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
            string sql = "Select cod_pla, Codigo, Fecha1, Fecha2, Dias, Status as Procesado,Sindicato ";
            sql = sql + " from Planillas ";

            if (Pla_Cod.Text.Trim() != "")
                sql = sql + " where Codigo = '" + Pla_Cod.Text.Trim() + "'";
                       
            sql = sql + " order by fecha1 desc";

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");

            if (dt.Rows.Count > 0)
            {
                dGrid1.DataSource = dt; // dtSet.Tables["Developer"].DefaultView;
                dGrid1.Visible = true;
                dGrid1.Columns[0].Visible = false;

                dGrid1.Columns[Activos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dGrid1.Columns[Netos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dGrid1.Columns[Salario].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
                dGrid1.Visible = false;
        }

        private string Check(string tt)
        {
            tt = tt.Replace(",", "");
            double n;
            int n1;

            if (tt == "") tt = "0";
            if (double.TryParse(tt, out n)) return tt;

            if (int.TryParse(tt, out n1)) return tt;

            MessageBox.Show("Digite un valor numerico");
            return "-1";
        }


        private void GetGrupos(DataTable dt)
        {
            var newDt = dt.AsEnumerable()
              .GroupBy(r => r.Field<object>("Grupo")).ToList();

            cbGrupos.Items.Clear();

            foreach (var item in newDt.AsEnumerable())
            {
                string each = item.Key.ToString();

                if (each == "0") each = " Todos";
                else each = "   " + each;
                cbGrupos.Items.Add(each);
            }
        }


        private void LoadPartidas()
        {
            string sql = "select a.id, a.cod_pla, a.cod_empl, a.cod_partida, p.Partidas +' - '+ Unidad as Partida, a.Valor, Cantidad, (a.Valor * Cantidad) as Total, Ubicacion ";
            sql = sql + " from pla_partidas a, partidas p ";
            sql = sql + " where a.cod_partida = p.cod_partida ";

            sql = sql + " and a.cod_pla=" + Gcodi;
            sql = sql + " and a.cod_empl=" + GEmpl;

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");

            Fx_Partidas fx2 = new Fx_Partidas();
            if (dgPartidas.Rows.Count > 0) dgPartidas.DataSource = fx2.NewDT();

            if (dt.Rows.Count > 0)
            {
                dgPartidas.DataSource = dt;
                dgPartidas.Visible = true;

                lbTit2.Text = "Total";
                lbTotal.Text = fx2.SumarTotal(dgPartidas);

                fx2.SetPartidas(dgPartidas);
            }
            else dgPartidas.Visible = false;
        }

        private void Planilla_Load(object sender, EventArgs e)
        {
            string sql = "Select cod_empl, nombre_usual as Nombre from Empleado where tipo_pago='E' order by nombre_usual";
            Funciones fx = new Funciones();
            DataTable dt2 = fx.GetData(sql, "");
            cbEventual.DataSource = dt2;

            cbEventual.DisplayMember = "Nombre";
            cbEventual.ValueMember = "cod_empl";

            sql = "select cod_partida, Partidas+ ' / '+ Unidad  as partida, valor from Partidas where partidas <>' ' order by Partidas";
            dt2 = fx.GetData(sql, "");
            cbPartidas.DataSource = dt2;

            cbPartidas.DisplayMember = "partida";
            cbPartidas.ValueMember = "cod_partida";

            dtPartidas = fx.GetData("select * from Partidas", "");

           

            cbPla.Items.Insert(0, "Todos");
            cbPla.Items.Insert(1, "Activos");
            cbPla.Items.Insert(2, "Procesadas");
            cbPla.SelectedIndex = 0;
        }


        private void LoadPla()
        {
            tabCtl.SelectedIndex = 1;

            string sql = "Select * from pla_detalle  where cod_pla =" + Gcodi;
            Funciones fx = new Funciones();

            DataTable dt = fx.GetData(sql, "");
        }

        private void FueProcesado(bool process)
        {
           
            panel4.Visible = !process;
            panel5.Visible = !process;

            panel7.Visible = !process;
            panel9.Visible = !process;

            panel10.Visible = !process;

            btnBanco.Visible = process; // banco
            btnReActivar.Visible = process; // reactivar

            panel8.Visible = false;

            dgPla.ReadOnly = process;
            dg2.ReadOnly = process;
            dg4.ReadOnly = process;
            dg5.ReadOnly = process;
            dg6.ReadOnly = process;
            dg8.ReadOnly = process;
            dg9.ReadOnly = process;
            dg10.ReadOnly = process;
            dg11.ReadOnly = process;

            dgPartidas.ReadOnly = process;
            panel10.Visible = !process;
        }
        private void dGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row > -1 && dGrid1.Rows[row].Cells[1].Value != null)
            {

                // PROCESADO
                //===========
                string Procesado = dGrid1.Rows[row].Cells[6].Value.ToString();
                bool process = false;
                if (Procesado == "True") process = true;
                if (Variables.NIVEL == 4) process = true;
                FueProcesado(process);
                //========================


                Gcodi = dGrid1.Rows[e.RowIndex].Cells[0].Value.ToString();

                c1.Text = dGrid1.Rows[e.RowIndex].Cells[1].Value.ToString();
                c2.Text = c1.Text;
                c3.Text = c1.Text;
                c4.Text = c1.Text;
                c5.Text = c1.Text;
                c6.Text = c1.Text;

                c8.Text = c1.Text;
                c9.Text = c1.Text;
                c10.Text = c1.Text;

                Gproy = dGrid1.Rows[e.RowIndex].Cells[2].Value.ToString();
                p1.Text = Gproy;
                p2.Text = Gproy;
                p3.Text = Gproy;
                p4.Text = Gproy;
                p5.Text = Gproy;
                p6.Text = Gproy;

                p8.Text = Gproy;
                p9.Text = Gproy;
                p10.Text = Gproy;


                fec1 = dGrid1.Rows[e.RowIndex].Cells[3].Value.ToString();
                fec2 = dGrid1.Rows[e.RowIndex].Cells[4].Value.ToString();
                string fecs = fec1 + " - " + fec2;
                fecs = fecs.Replace("12:00:00 AM", "");
                f1.Text = fecs;
                f2.Text = fecs;
                f3.Text = fecs;
                f4.Text = fecs;
                f5.Text = fecs;
                f6.Text = fecs;
                //f7.Text = fecs;
                f8.Text = fecs;
                f9.Text = fecs;
                f10.Text = fecs;

                status = Convert.ToInt32(dGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());

                agui = Convert.ToBoolean(dGrid1.Rows[e.RowIndex].Cells[5].Value.ToString());
                dias = dGrid1.Rows[e.RowIndex].Cells[6].Value.ToString();

                tabCtl.SelectedIndex = 1;

                LoadDetalle();
            }
        }

        private void SetGrid(DataGridView dg)
        {
            Funciones_Pla fxPla = new Funciones_Pla();
            fxPla.SetGrid2(dg);
            dg.Visible = true;

            dg.Columns[Grupo].Visible = false;
            dg.Columns[ValHora].Visible = false;
        }

        private void LoadDetalle()
        {
            string sql = "select e.Cod_empl as Cod, Nombre_usual as Nombre, t.Tipo, d.Activo, Neto, Bruto as Salario, d.Horas, d.ValorHora, d.Grupo, d.Adelanto, d.Descuento, d.Bono, Inas1, Inas2 ";
            sql = sql + " From Empleado e, pla_detalle d, tipo_pago t ";
            sql = sql + " where e.Cod_empl = d.Cod_empl ";
            sql = sql + " and t.tipo_pago = d.tipo_pago    ";
            sql = sql + " and d.Cod_pla = " + Gcodi;

            Funciones_Pla fxPla = new Funciones_Pla();
            Funciones fx = new Funciones();

            DataTable dt = fx.GetData(sql, "");

            if (dt.Rows.Count > 0)
            {
                dgPla.DataSource = dt;
                dgPla.Columns[0].Visible = false;

                DataGridViewCellStyle currencyCellStyle = new DataGridViewCellStyle();
                currencyCellStyle.Format = "C";
                currencyCellStyle.ForeColor = Color.Green;

                dgPla.Columns[Netos].DefaultCellStyle = currencyCellStyle;
                dgPla.Columns[Salario].DefaultCellStyle = currencyCellStyle;

                for (int i = 1; i < Inas1; i++)
                {
                    if (i > 2)
                    {
                        dgPla.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgPla.Columns[i].Width = 80;
                    }

                    if (i != 3) fxPla.ReadOnly(dgPla, i);
                }

                dgPla.Columns[Inas1].Visible = false;
                dgPla.Columns[Inas2].Visible = false;
            }

            dgPla.Visible = true;
            GSalario = "";
            GRow = 0;
            lbNombre.Text = "";

            GetGrupos(dt);
            Funciones_Pla pla = new Funciones_Pla();

            dg2.DataSource = pla.Filter(dt, "Por Contrato");
            if (dg2.Rows.Count > 0)
            {
                SetGrid(dg2);
                dg2.Columns[Horas].HeaderText = "H.Extras";

                fxPla.ReadOnly(dg2, Salario);
            }
            else dg2.Visible = false;


            dg3.DataSource = pla.Filter(dt, "Por Variable");
            if (dg3.Rows.Count > 0)
            {
                SetGrid(dg3);
                dg3.Columns[Grupo].Visible = false;
            }
            else
                dg3.Visible = false;


            dg4.DataSource = pla.Filter(dt, "Eventual");
            if (dg4.Rows.Count > 0)
            {
                SetGrid(dg4);
                dg4.Columns[Horas].Visible = false;
            }
            else dg4.Visible = false;

            dg5.DataSource = pla.Filter(dt, "Por Horas");
            if (dg5.Rows.Count > 0)
            {
                SetGrid(dg5);

                dg5.Columns[Salario].Visible = false;
                fxPla.ReadOnly(dg5, ValHora);

                dg5.Columns[Inas1].Visible = true;
                dg5.Columns[Inas2].Visible = true;

            }
            else dg5.Visible = false;


            dtObra = pla.Filter(dt, "En Grupo");
            dg6.DataSource = dtObra;
            if (dg6.Rows.Count > 0)
            {
                dg6.Visible = true;
                cbGrupos.Visible = true;
                lbGR.Visible = true;

                fxPla.SetGrid2(dg6);

                dg6.Columns[Horas].Visible = false;
                dg6.Columns[ValHora].Visible = false;
            }
            else
            {
                dg6.Visible = false;
                cbGrupos.Visible = false;
                lbGR.Visible = false;
            }

            dg7.DataSource = pla.Filter(dt, "Por Contrato");
            if (dg7.Rows.Count > 0)
            {
                SetGrid(dg7);
                dg7.Visible = true;
                dg7.Columns[Tipos].Visible = false;
                dg7.Columns[Horas].Visible = false;
            }
            else dg7.Visible = false;


            dg8.DataSource = pla.Filter(dt, "Por Contrato");
            if (dg8.Rows.Count > 0)
            {
                SetGrid(dg8);
                dg8.Columns[Salario].HeaderText = "Monto";
                dg8.Columns[Horas].Visible = false;
                dg8.Columns[Adelanto].Visible = false;
                dg8.Columns[Bono].Visible = false;
            }
            else dg8.Visible = false;

            dg9.DataSource = pla.Filter(dt, "Por Contrato");
            if (dg9.Rows.Count > 0)
            {
                SetGrid(dg9);
                dg9.Columns[Salario].HeaderText = "Monto";
                dg9.Columns[Horas].Visible = false;
                dg9.Columns[Adelanto].Visible = false;
                dg9.Columns[Bono].Visible = false;
            }
            else dg9.Visible = false;
        }

        private void dg4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Funciones_Pla fx = new Funciones_Pla();
            fx.UpdateGridCells(4, GEmpl, GSalario, dgPla, NewVal, dg4, e.RowIndex, Gcodi, e.ColumnIndex);
            NewVal = "";
        }

        private void dg2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Funciones_Pla fx = new Funciones_Pla();
            fx.UpdateGridCells(2, GEmpl, GSalario, dgPla, NewVal, dg2, e.RowIndex, Gcodi, e.ColumnIndex);
            NewVal = "";
        }


        private void bt22_Click_1(object sender, EventArgs e)
        {
            if (Gcodi == "") return;

            Funciones fx = new Funciones();
            fx.UpdateData("Delete from pla_detalle where cod_pla =" + Gcodi);

            string proy = fx.GetDataValue("select cod_proy from planillas where cod_pla =" + Gcodi);

            string sql = "insert into pla_detalle(cod_pla, cod_empl, Bruto, horas, valorHora, tipo_pago, activo, cod_afp, cod_sind, aplica_renta, aplica_isss, grupo, Descuento ) ";

            sql = sql + " select " + Gcodi + ", cod_empl, Sueldo, horas, valorHora, tipo_pago, activo, cod_afp, cod_sind,  renta,      cod_isss,     grupo, Descuento FROM EMPLEADO ";
            sql = sql + " where (cod_proy =" + proy + " or cod_proy2 = " + proy + " or cod_proy3 = " + proy + " )";
            sql = sql + " and tipo_pago<>'E' ";

            fx.UpdateData(sql);

            LoadDetalle();
        }

        private void dg5_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            NewVal = dg5.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void dg5_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Funciones_Pla fx = new Funciones_Pla();
            fx.UpdateGridCells(5, GEmpl, GSalario, dgPla, NewVal, dg5, e.RowIndex, Gcodi, e.ColumnIndex);
            NewVal = "";
        }

        private void dg2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            NewVal = dg2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void tabctrlDocEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Gcodi == "") return;

            // SI FALTO DE ACTUALIZARSE EL EMPLEADO DE ULTIMA PARTIDA
            if (GSalario != "" && GEmpl != "")
            {
                if (GSalario == ".") GSalario = "0";

                Funciones_Pla fx = new Funciones_Pla();
                fx.UpdateGridCells(0, GEmpl, GSalario, dgPla, NewVal, null, GRow, Gcodi, Salario);

                GSalario = "";
            }

            int tab = tabCtl.SelectedIndex;

            if (tab == 7)
            {
                GEmpl = "";
                if (!dg7.Visible) return;

                Funciones_Pla fxPla = new Funciones_Pla();
                fxPla.ReadOnly(dg7, Salario);
                if (dg7.Rows.Count > 0)
                {
                    GRow = 0;
                    GEmpl = dg7.Rows[0].Cells[0].Value.ToString();
                    lbNombre.Text = dg7.Rows[0].Cells[1].Value.ToString();

                    LoadPartidas();
                    UpdateSalario();
                }
            }

            if (tab == 11) LoadAll();
        }

        private void LoadAll()
        {
            string sql = "select DUI, e.ISSS as c_ISSS, NIT, e.AFP as c_AFP, Nombre_usual as Nombre,  e.Activo, t.Tipo, AFP.AFP as AFP, Neto, Bruto, MonISSS as ISSS, d.Cuota_Sind as Sindicato, MonAFPEmpl as AFP_E, MonAFPPatrono as AFP_P, MonRenta as Renta, d.Adelanto, d.Descuento, d.Bono ";
            sql = sql + " From Empleado e, pla_detalle d, tipo_pago t, AFP ";
            sql = sql + " where e.Cod_empl = d.Cod_empl ";
            sql = sql + " and t.tipo_pago = d.tipo_pago    ";
            sql = sql + " and d.cod_afp = AFP.cod_afp ";
            sql = sql + " and d.Cod_pla = " + Gcodi;

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");
            dg11.ReadOnly = true;

            if (dt.Rows.Count > 0)
            {
                dg11.DataSource = dt;
                dg11.Visible = true;

                dg11.Columns[Netos].Width = 110;
                dg11.Columns[Salario].Width = 50;

                for (int i = 0; i < dg11.Columns.Count; i++)
                {
                    dg11.Columns[i].DefaultCellStyle = Variables.numero;

                    if (i == 7 || i == 8) dg11.Columns[i].Width = 70;

                    if (i > 9) dg11.Columns[i].Width = 60;
                }
                for (int i = 0; i < dg11.Rows.Count; i++)
                {
                    string neto = dg11.Rows[i].Cells[8].Value.ToString();
                    if (System.Convert.ToDouble(neto) < 1)
                    {
                        dg11.Rows[i].Cells[8].Style.ForeColor = Color.Red;
                    }
                }
            }
        }


        private void dgPla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string valor = dgPla.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string emp = dgPla.Rows[e.RowIndex].Cells[0].Value.ToString();

            if (valor == "False") valor = "0";
            else valor = "1";

            string where = valor + " where cod_pla=" + Gcodi + " and cod_empl=" + emp;

            Funciones fx = new Funciones();
            fx.UpdateData("update pla_detalle set activo= " + where);
        }

        private void dg3_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            NewVal = dg3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void dg4_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            NewVal = dg4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void dg6_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            NewVal = dg6.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void dg3_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            Funciones_Pla fx = new Funciones_Pla();
            fx.UpdateGridCells(3, GEmpl, GSalario, dgPla, NewVal, dg3, e.RowIndex, Gcodi, e.ColumnIndex);
            NewVal = "";
        }

        private void dg4_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            Funciones_Pla fx = new Funciones_Pla();
            fx.UpdateGridCells(4, GEmpl, GSalario, dgPla, NewVal, dg4, e.RowIndex, Gcodi, e.ColumnIndex);
            NewVal = "";
        }

        private void cbGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cual = cbGrupos.SelectedItem.ToString().Trim();

            if (cual != "Todos")
            {
                panel2.Visible = true;
                byte cual2 = System.Convert.ToByte(cual);

                DataTable dt2 = dtObra.AsEnumerable()
                  .Where(i => i.Field<byte>("Grupo") == cual2)
                 .OrderByDescending(i => i.Field<String>("Nombre"))
                 .CopyToDataTable();

                double suma = 0;
                foreach (DataRow dr in dt2.Rows)
                {
                    suma = suma + Convert.ToDouble(dr[Salario].ToString());
                }
                txTotal.Text = suma.ToString();
                dg6.DataSource = dt2;
            }
            else
            {
                dg6.DataSource = dtObra;
                panel2.Visible = false;
            }

            Funciones_Pla fxPla = new Funciones_Pla();
            fxPla.SetGrid2(dg6);

            dg6.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dg6.Columns[Salario].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dg6.Columns[Horas].Visible = false;
            dg6.Columns[ValHora].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string total = txTotal.Text;
            double total2 = Convert.ToDouble(total);
            int total3 = Convert.ToInt32(total2 * 100 / dg6.Rows.Count);
            total2 = Convert.ToDouble(total3) / 100;

            string sql = "update pla_detalle set Bruto = " + total2.ToString() + " where cod_pla=" + Gcodi + " and grupo =" + cbGrupos.SelectedItem.ToString().Trim();
            Funciones fx = new Funciones();
            fx.UpdateData(sql);

            int grupo = System.Convert.ToInt32(cbGrupos.SelectedItem.ToString());

            foreach (DataRow dr in dtObra.Rows)
            {
                int grupo2 = System.Convert.ToInt32(dr[Grupo].ToString());
                if (grupo2 == grupo) dr[Salario] = total2;
            }

            for (int i = 0; i < dgPla.Rows.Count; i++)
            {
                string grupo2 = dgPla.Rows[i].Cells[Grupo].Value.ToString();
                if (grupo2 == grupo.ToString()) dgPla.Rows[i].Cells[Salario].Value = total2;
            }

            byte cual2 = System.Convert.ToByte(grupo);

            DataTable dt2 = dtObra.AsEnumerable()
                .Where(i => i.Field<byte>("Grupo") == cual2)
               .OrderByDescending(i => i.Field<String>("Nombre"))
               .CopyToDataTable();

            dg6.DataSource = dt2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Funciones fx = new Funciones();
            string sql = "select cod_empl, nombre_usual from empleado where DUI='" + txDUI.Text + "'";
            DataTable dt = fx.GetData(sql, "");

            if (dt.Rows.Count == 0)
                lbNombre8.Text = "";
            else
            {
                lbNombre8.Text = dt.Rows[0][1].ToString();
                lbEmp8.Text = dt.Rows[0][0].ToString();
            }

            if (lbNombre8.Text.Length > 5) panel6.Visible = true;
            else panel6.Visible = false;
        }

        private void txDUI_TextChanged(object sender, EventArgs e)
        {
            lbNombre8.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string nombre = lbNombre8.Text;
            string empl = lbEmp8.Text;
            string tipo = "Liquidacion";
            string tipo2 = "'L',1,";

            Funciones_Pla fxPla = new Funciones_Pla();

            // verifica numerico
            string desc = Check(txDesc8.Text);
            string monto = Check(txMon8.Text);

            if (desc == "-1") return;
            if (monto == "-1") return;
            if (monto == "0")
            {
                MessageBox.Show("Digite un monto a pagar");
                return;
            }

            if (fxPla.SiEstaAsignado(empl, "'L'", dg8)) return;

            fxPla.InsertPlaDetalle(Gcodi, empl, monto, "0", tipo2);

            fxPla.Agregue_Fila_Grid(dgPla, empl, nombre, tipo, monto, desc);

            fxPla.Agregue_Fila_Grid(dg8, empl, nombre, tipo, monto, desc);

            dg8.Columns[Descuento].Visible = true;
        }


        private void button8_Click_1(object sender, EventArgs e)
        {
            string empl = cbEventual.SelectedValue.ToString();
            string monto = Check(txPago.Text);
            string nombre = cbEventual.Text;
            string tipo = "Eventual";
            string tipo2 = "'E',1,";

            //string descuento1 = "0";
            Funciones_Pla fxPla = new Funciones_Pla();

            if (monto == "-1" || monto == "0")
            {
                if (monto == "0") MessageBox.Show("Digite un monto a pagar");
                return;
            }

            if (fxPla.SiEstaAsignado(empl, "'E'", dg4)) return;

            fxPla.InsertPlaDetalle(Gcodi, empl, monto, "0", tipo2);

            fxPla.Agregue_Fila_Grid(dgPla, empl, nombre, tipo, monto, "0");

            fxPla.Agregue_Fila_Grid(dg4, empl, nombre, tipo, monto, "0");

            fxPla.SetGrid2(dg4);

            for (int i = 6; i < dg4.Columns.Count; i++) dg4.Columns[i].Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string nombre = lbNombre10.Text;
            string empl = lbEmp10.Text;
            string tipo = "Vacacion";
            string tipo2 = "'C',1,";
            string desc = "0";
            string monto = Check(txMonto10.Text);

            Funciones_Pla fxPla = new Funciones_Pla();

            if (monto == "-1") return;
            if (monto == "0")
            {
                MessageBox.Show("Digite un monto a pagar");
                return;
            }

            if (fxPla.SiEstaAsignado(empl, "'C''", dg10)) return;

            fxPla.InsertPlaDetalle(Gcodi, empl, monto, "0", tipo2);

            fxPla.Agregue_Fila_Grid(dgPla, empl, nombre, tipo, monto, desc);

            fxPla.Agregue_Fila_Grid(dg10, empl, nombre, tipo, monto, desc);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // REVISAR CALCULOS IMPUESTOS, NET
            FxCalculo fx = new FxCalculo();
            fx.Calcular(Gcodi);
            LoadDetalle();
            MessageBox.Show("Calculado");
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "delete from pla_detalle where cod_pla=" + Gcodi;
            Funciones fx = new Funciones();
            fx.UpdateData(sql);

            sql = "delete from planillas where cod_pla =" + Gcodi;
            fx.UpdateData(sql);
            Gcodi = "";

            dGrid1.Visible = false;
            dgPla.Visible = false;

            dg2.Visible = false;
            dg3.Visible = false;
            dg4.Visible = false;
            dg5.Visible = false;
            dg6.Visible = false;
            dg7.Visible = false;
            dg8.Visible = false;
            dg9.Visible = false;
            dg10.Visible = false;
            dg11.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Funciones fx = new Funciones();
            string total = fx.GetDataValue("Select sum(monRenta) from pla_detalle where cod_pla=" + Gcodi);
            if (total == "0")
            {
                MessageBox.Show("No se puede procesar porque no se ha calculado la Renta");
                return;
            }

            FueProcesado(true);

            // Actualize 'aplicado' en tabla ajuste_renta
            string sql = "UPDATE ajuste_renta set aplicado=True ";
            sql = sql + " WHERE aplicado=False ";
            sql = sql + " AND cod_empl in ";
            sql = sql + " ( Select cod_empl FROM pla_detalle ";
            sql = sql + " where cod_pla =" + Gcodi + ") ";

            fx.UpdateData(sql);

            fx.UpdateData("Update planillas set Status = True where cod_pla =" + Gcodi);
        }


        private void UpdateSalario()
        {
            GSalario = "0";
            if (lbTotal.Text != ".")
            {
                GSalario = lbTotal.Text;
                GSalario = GSalario.Replace(",", "");
            }

            string salario2 = dg7.Rows[GRow].Cells[Salario].Value.ToString();
            salario2 = salario2.Replace(",", "");

            if (GSalario != salario2)
            {
                dg7.Rows[GRow].Cells[Salario].Value = GSalario;
                Funciones_Pla fx = new Funciones_Pla();
                fx.UpdateGridCells(7, GEmpl, GSalario, dgPla, NewVal, dg7, GRow, Gcodi, Salario);
            }
        }

        private void dg7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (GRow != row) UpdateSalario();
            GRow = row;

            lbNombre.Text = dg7.Rows[GRow].Cells[1].Value.ToString();
            GEmpl = dg7.Rows[GRow].Cells[0].Value.ToString();
            lbTotal.Text = ".";

            LoadPartidas();
            //UpdateSalario();
        }

        private void dg6_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Funciones_Pla fx = new Funciones_Pla();
            fx.UpdateGridCells(6, GEmpl, GSalario, dgPla, NewVal, dg6, e.RowIndex, Gcodi, e.ColumnIndex);
            NewVal = "";
        }

        private void dg7_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= Adelanto)
            {
                Funciones_Pla fx = new Funciones_Pla();
                fx.UpdateGridCells(7, GEmpl, GSalario, dgPla, NewVal, dg7, e.RowIndex, Gcodi, e.ColumnIndex);
                NewVal = "";
            }
        }

            private void dg8_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
            {
                Funciones_Pla fx = new Funciones_Pla();
                fx.UpdateGridCells(8, GEmpl, GSalario, dgPla, NewVal, dg8, e.RowIndex, Gcodi, e.ColumnIndex);
                NewVal = "";
            }

            private void dg7_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
            {
                NewVal = dg7.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            private void dg8_CellBeginEdit_1(object sender, DataGridViewCellCancelEventArgs e)
            {
                NewVal = dg8.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            private void dg9_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
            {
                NewVal = dg9.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            private void dg10_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
            {
                NewVal = dg10.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            private void dg10_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            {
                Funciones_Pla fx = new Funciones_Pla();
                fx.UpdateGridCells(10, GEmpl, GSalario, dgPla, NewVal, dg10, e.RowIndex, Gcodi, e.ColumnIndex);
                NewVal = "";
            }

            private void button13_Click(object sender, EventArgs e)
            {
                // DIGITE CLAVE DE ADMIN
                string input = Microsoft.VisualBasic.Interaction.InputBox("", "Digite Clave", "", -1, -1);
                if (input != "Levitico")
                {
                    MessageBox.Show("Clave Incorrecta");
                    return;
                }

                Funciones fx = new Funciones();
                fx.UpdateData("update planillas set status = False where cod_pla=" + Gcodi);
                MessageBox.Show("Status Actualizado, recargue planilla");
            }

            private void dg9_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            {
                Funciones_Pla fx = new Funciones_Pla();
                fx.UpdateGridCells(9, GEmpl, GSalario, dgPla, NewVal, dg9, e.RowIndex, Gcodi, e.ColumnIndex);
                NewVal = "";
            }

            private void cbPartidas_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (Gcodi == "") return;
                if (GEmpl == "")
                {
                    MessageBox.Show("Escoja un Empleado");
                    return;
                }

                string where = "cod_partida=" + cbPartidas.SelectedValue.ToString();
                DataRow[] result = dtPartidas.Select(where);
                if (result.Length > 0) valor.Text = result[0][2].ToString();
            }

            private void button12_Click(object sender, EventArgs e)
            {
                Fx_Partidas fxPart = new Fx_Partidas();
                double tot1 = fxPart.Total(cant.Text, valor.Text);
                if (tot1 < 0.01) return;

                // INSERT IN pla_partidas
                //=======================
                string cod_partida = cbPartidas.SelectedValue.ToString();
                string ubicacion = ubi.Text;
                if (ubicacion == "") ubicacion = " ";

                Funciones fx = new Funciones();
                string sql = "insert into pla_partidas (cod_pla, cod_empl, cod_partida, cantidad, valor, ubicacion) values  (";
                sql = sql + Gcodi + "," + GEmpl + "," + cod_partida + "," + cant.Text + "," + valor.Text + ",'" + ubicacion + "') ";
                fx.UpdateData(sql);

                dgPartidas.Visible = true;


                // MODIFY dgPartidas
                //===================

                DataTable dt = (DataTable)dgPartidas.DataSource;
                if (dgPartidas.Rows.Count == 0) dt = fxPart.NewDT();

                DataRow dr = dt.NewRow();
                dr[0] = 0;
                dr["cod_pla"] = Convert.ToInt32(Gcodi);
                dr["cod_empl"] = Convert.ToInt32(GEmpl);
                dr["cod_partida"] = Convert.ToInt32(cod_partida);
                dr[4] = cbPartidas.Text;

                dr["Cantidad"] = Convert.ToDouble(cant.Text);
                dr["Valor"] = Convert.ToDouble(valor.Text);
                dr[7] = tot1;
                dr[8] = ubi.Text;

                dt.Rows.Add(dr);
                dgPartidas.DataSource = dt;

                fxPart.SetPartidas(dgPartidas);

                // TOTAL
                //========
                if (lbTotal.Text == ".") lbTotal.Text = "0";
                double tt = Convert.ToDouble(lbTotal.Text) + tot1;
                lbTotal.Text = tt.ToString("#,##0.00");
                lbTit2.Text = "Total";
            }

            private void lbTotal_TextChanged(object sender, EventArgs e)
            {
                GSalario = lbTotal.Text;
                if (GSalario == ".") return;
                if (GRow != 0)
                {
                    string salario = GSalario;
                    salario = salario.Replace(",", "");
                    dg7.Rows[GRow].Cells[Salario].Value = salario;
                }
            }

            private void dgPartidas_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode != Keys.Delete) return;

                DataGridViewRow dr = dgPartidas.Rows[dgPartidas.CurrentRow.Index];
                string total1 = dr.Cells[6].Value.ToString();
                string total2 = lbTotal.Text;

                Fx_Partidas fx = new Fx_Partidas();
                lbTotal.Text = fx.DeletePartida(total1, total2, dr);
            }

            private void dgPartidas_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
            {
                NewVal = dgPartidas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            private void dgPartidas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            {
                Fx_Partidas fxPart = new Fx_Partidas();
                string valor = fxPart.UpdatePartida(e.ColumnIndex, NewVal, dgPartidas.Rows[e.RowIndex]);
                NewVal = "";

                if (valor != "")
                {
                    dgPartidas.Rows[e.RowIndex].Cells[7].Value = valor;
                    lbTit2.Text = "Total";
                    lbTotal.Text = fxPart.SumarTotal(dgPartidas);
                }
            }

            private void dgPla_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }

        private void dGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }
