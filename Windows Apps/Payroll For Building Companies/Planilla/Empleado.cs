using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Planilla
{
    public partial class Empleado : Form
    {
        string EmplID = "0";

        string PrevDUI = "";
        string PrevNIT = "";
        string PrevISSS = "";
        string PrevAFP = "";

        public Empleado()
        {
            InitializeComponent();
        }



        private void button9_Click(object sender, EventArgs e)
        {
            string sql = "select cod_empl, Apellidos, DUI, NIT,ISSS, AFP, nombre_usual  as Nombre, Empleado.[Telefono], Empleado.[Celular],Avisar, Tipo, Empleado.Activo as Activo ";
            sql = sql + " from empleado,  Tipo_Pago   ";
            sql = sql + " where Empleado.tipo_pago = tipo_pago.tipo_pago ";

            string ape = "";
            if (txDUI.Text.Trim() != "")
                sql = sql + " and DUI ='" + txDUI.Text.Trim() + "' ";
            else
            {
                if (txNIT.Text.Trim() != "")
                    sql = sql + " and NIT ='" + txNIT.Text.Trim() + "' ";
                else
                {
                    if (cbEstado.Text == "Activos") sql = sql + " and Empleado.activo = True ";
                    if (cbEstado.Text == "Inactivos") sql = sql + " and Empleado.activo = 0 ";

                  
                    ape = txApe0.Text.Trim();
                    //if (ape.Length > 2) ape = ape.Substring(0, ape.Length - 1);

                    //if (txApe0.Text.Trim() != "")
                    //    sql = sql + " and apellidos > '" + ape + "' ";
                }
            }

           // sql = "select * from empleado where apellidos > 'Ayal' ";

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "");

            if (ape!="" && dt.Rows.Count>0)
            {
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    string ape1 = dr[0].ToString();
                    if (ape1.IndexOf(ape) < 0) dr.Delete();
                }
            }
            dt.AcceptChanges();

            if (dt.Rows.Count > 0)
            {
                dGrid1.DataSource = dt;
                dGrid1.Visible = true;
                dGrid1.Columns[0].Visible = false;
                dGrid1.Columns[1].Visible = false;
            }
            else
            {
                dGrid1.Visible = false;
                MessageBox.Show("No se hallo data con este criterio");
            }
        }

        private void Empleado_Load(object sender, EventArgs e)
        {
            // si no es admin ni planillero no actualize
            if (Variables.NIVEL >1)
            {
                BtnActualizar.Enabled = false;
                tabctrlDocEdit.TabPages[3].Enabled = false;
            }

            cbBanco.Items.Insert(0, "A");
            cbBanco.Items.Insert(1, "C");

            cbTip3.Items.Insert(0, "A");
            cbTip3.Items.Insert(1, "C");
            cbTip3.SelectedIndex = 0;

            string sql = "select cod_proy, Proyecto from Proyecto order by Proyecto";
            Funciones fx = new Funciones();
            DataTable dt = fx.GetData(sql, "Todos");

          
            cbAFP.DataSource = Convert(Variables.AFP);
            cbAFP.DisplayMember = "tx";
            cbAFP.ValueMember = "cod";

            cbAFP3.DataSource = Convert(Variables.AFP);
            cbAFP3.DisplayMember = "tx";
            cbAFP3.ValueMember = "cod";


            cbTipo.DataSource = Convert2(Variables.Tipo);
            cbTipo.DisplayMember = "tx";
            cbTipo.ValueMember = "cod";

            cbTipo3.DataSource = Convert2(Variables.Tipo);
            cbTipo3.DisplayMember = "tx";
            cbTipo3.ValueMember = "cod";

            lbSueldo3.Visible = true;
            txSueldo3.Visible = true;
        }

        private DataTable Convert(List<listado> lista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cod", typeof(int));
            dt.Columns.Add("tx", typeof(string));
      
            foreach (listado l in lista)
            {
                ComboboxItem item = new ComboboxItem();

                item.Text = l.Text;
                item.Value = l.Value;
                dt.Rows.Add(item.Value, item.Text);
           }

            return dt;
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


        private void tabctrlDocEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabctrlDocEdit.SelectedIndex == 2)
            {
                if (EmplID == "0") return;

                string sql = "select Proyecto, Codigo, Status as Procesado, Fecha1, Fecha2, Aguinaldo, Bruto, Neto, Grupo, Adelanto, Descuento, Bono ";

                sql = sql + " From Planillas p, Pla_Detalle d, Proyecto y ";
                sql = sql + " where p.cod_pla = d.cod_pla ";
                sql = sql + " and   p.cod_proy = y.cod_proy ";
                sql = sql + " and cod_empl = " + EmplID;
                sql = sql + " order by Fecha1 ";

                Funciones fx = new Funciones();
                DataTable dt = fx.GetData(sql, "");

                if (dt.Rows.Count > 0)
                {
                    dg1.DataSource = dt;
                    dg1.Visible = true;

                    for (int i=3; i< dg1.Columns.Count;i++)
                        dg1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                    dg1.Visible = false;
            }

            if (tabctrlDocEdit.SelectedIndex == 3)
            {
             
            }
        }


        private void dGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row < 0) return;
            EmplID = dGrid1.Rows[row].Cells[0].Value.ToString();
            lbEmpl.Text = EmplID;

            Funciones fx = new Funciones();
            DataTable dt = fx.GetData("select * from empleado where cod_empl = "+ EmplID, "");

            System.Data.DataRow dr = dt.Rows[0];
            //txDUI2.Text = dr["DUI"].ToString();
            txNIT2.Text = dr["NIT"].ToString();
            txISSS.Text = dr["ISSS"].ToString();
            txAFP.Text = dr["AFP"].ToString();

            txNom.Text = dr["Nombres"].ToString();
            txApe.Text = dr["Apellidos"].ToString();
            txUsual.Text = dr["Nombre_Usual"].ToString();
            txDir1.Text = dr["Dir1"].ToString();
            txDir2.Text = dr["Dir2"].ToString();

            //txCuota.Text = dr["Cuota_Sind"].ToString();
            txHoras.Text = dr["Horas"].ToString();
            txSueldo.Text = dr["Sueldo"].ToString();


            txTel.Text = dr["Telefono"].ToString();
            txCel.Text = dr["Celular"].ToString();
            txAvisar.Text = dr["Avisar"].ToString();
            txVHora.Text = dr["ValorHora"].ToString();
            txCta.Text = dr["Cta_Banco"].ToString();
            cbBanco.Text = dr["Tip_Cta"].ToString();

            string ingre = dr["Ingreso"].ToString();

            int pos1 = ingre.IndexOf(" ");
            if (pos1 > 1) ingre = ingre.Substring(0, pos1);
            lbIngreso.Text = ingre;
                        
            string ss = dr["Activo"].ToString();
            if (ss == "True") ckActivo.Checked = true;
            else ckActivo.Checked = false;

            ss = dr["Renta"].ToString();
            if (ss == "True") ckRenta1.Checked = true;
            else ckRenta1.Checked = false;


            ss = dr["ISSS"].ToString();
            if (ss == "True") ckISSS1.Checked = true;
            else ckISSS1.Checked = false;
                       
            string cod = dr["cod_afp"].ToString();
            cbAFP.SelectedValue = cod;

            cod = dr["tipo_pago"].ToString();
            cbTipo.SelectedValue = cod;
            Mostrar();


            txDUI2.Text = dr["DUI"].ToString();
            txNIT2.Text = dr["NIT"].ToString();
            txISSS.Text = dr["ISSS"].ToString();

            txNom.Text = dr["Nombres"].ToString();
            txApe.Text = dr["Apellidos"].ToString();
            txUsual.Text = dr["Nombre_Usual"].ToString();

            txDir1.Text = dr["Dir1"].ToString();
            txDir2.Text = dr["Dir2"].ToString();

            txObs.Text = dr["Observacion"].ToString();

            txDesc.Text = dr["descuento"].ToString();
            txRazon.Text = dr["RazonDesc"].ToString();


            tabctrlDocEdit.SelectedIndex = 1;
            BtnActualizar.Enabled = true;

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dg1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            string afp = cbAFP.SelectedValue.ToString();
            
            string cte = cbBanco.Text.ToString();
                        
            string tipo = cbTipo.SelectedValue.ToString();

            Funciones fx = new Funciones();
            string varios = fx.Pagos(1, txHoras, txSueldo, txVHora);
            if (varios == "-1") return;

            string sql = " update empleado set ";
            sql = sql + " DUI = '" + txDUI2.Text + "', ";
            sql = sql + " NIT = '" + txNIT2.Text + "', ";
            sql = sql + " ISSS = '" + txISSS.Text + "', ";
            sql = sql + " AFP = '" + txAFP.Text + "', ";

            sql = sql + varios;
            
            string valor = fx.Check(txDesc);
            if (valor == "-1")
                {
                    MessageBox.Show("Horas debe ser numerico");
                    return;
                }
            
            sql = sql + " Descuento= " + valor + ", ";
            sql = sql + " RazonDesc ='" + txRazon.Text + "', ";

            sql = sql + " nombres = '" + txNom.Text + "', ";
            sql = sql + " apellidos = '" + txApe.Text + "', ";
            sql = sql + " nombre_usual= '" + txUsual.Text + "', ";

            sql = sql + " telefono = '" + txTel.Text + "', ";
            sql = sql + " avisar = '" + txAvisar.Text + "', ";
            sql = sql + " celular = '" + txCel.Text + "', ";

            DateTime fec = DateTime.Today;
            string hoy = fec.ToString();
            int pos1 = hoy.IndexOf(" ");
            hoy = hoy.Substring(0, pos1);

            sql = sql + " modificado ='" + hoy + "', ";
            
            

            //sql = sql + " cod_sind = " + sind + ", ";
            sql = sql + " cod_afp = " + afp + ", ";

            if (ckISSS1.Checked) sql = sql + " cod_isss = 1, ";
            else sql = sql + " cod_isss = 0, ";


            sql = sql + " tipo_pago = '" + tipo + "', ";

            sql = sql + " cta_banco ='" + txCta.Text.ToString() + "', ";
            sql = sql + " tip_cta ='" + cte + "', ";
            sql = sql + " observacion ='" + txObs.Text + "', ";

            if (ckActivo.Checked) sql = sql + " Activo = 1, ";
            else  sql = sql + " Activo = 0, ";

            if (ckRenta1.Checked) sql = sql + " Renta = 1, ";
            else sql = sql + " Renta = 0, ";

            
            sql = sql + " Dir1 = '" + txDir1.Text + "', ";
            sql = sql + " Dir2 = '" + txDir2.Text + "' ";

            sql = sql + " where  cod_empl = " + EmplID;

            fx.UpdateData(sql);

            MessageBox.Show("Grabado");
            PrevDUI = "";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (txDUI3.Text.Trim()=="")
            {
                MessageBox.Show("Digite el DUI");
                return;
            }
            if (txNIT3.Text.Trim() == "")
            {
                MessageBox.Show("Digite el NIT");
                return;
            }

            if (txNom3.Text.Trim() == "")
            {
                MessageBox.Show("Digite el Nombre");
                return;
            }

            if (txApe3.Text.Trim() == "")
            {
                MessageBox.Show("Digite el Apellido");
                return;
            }

            if (txUsual3.Text.Trim() == "")
            {
                MessageBox.Show("Digite el Nombre Usual");
                return;
            }

            if (txTel3.Text.Trim() == "")
            {
                MessageBox.Show("Digite el Telefono");
                return;
            }

            //=====================


            Funciones fx = new Funciones();
            string varios = fx.Pagos(2, txHoras3, txSueldo3, txVHora3);
            if (varios == "-1") return;


            //====================


            string sql1 = "select nombre_usual from empleado where DUI ='" + txDUI3.Text.Trim() + "' ";
            string nombre = fx.GetDataValue(sql1);
            if (nombre != null)
            {
                MessageBox.Show("El DUI ya lo tiene asignado en la base de datos el empleado " + nombre);
                return;
            }

            sql1 = "select nombre_usual from empleado where NIT ='" + txNIT3.Text.Trim() + "' ";
            nombre = fx.GetDataValue(sql1);
            if (nombre != null)
            {
                MessageBox.Show("El NIT ya lo tiene asignado en la base de datos el empleado " + nombre);
                return;
            }

            sql1 = "select nombre_usual from empleado where ISSS ='" + txISSS3.Text.Trim() + "' ";
            nombre = fx.GetDataValue(sql1);
            if (nombre != null)
            {
                MessageBox.Show("El ISSS ya lo tiene asignado en la base de datos el empleado " + nombre);
                return;
            }

            sql1 = "select nombre_usual from empleado where AFP ='" + txAFP3.Text.Trim() + "' ";
            nombre = fx.GetDataValue(sql1);
            if (nombre != null)
            {
                MessageBox.Show("El AFP ya lo tiene asignado en la base de datos el empleado " + nombre);
                return;
            }


            string afp = cbAFP3.SelectedValue.ToString();
            string cte = cbTip3.Text.ToString();

            string proy31 = cbProy31.SelectedValue.ToString();
            string proy32 = cbProy32.SelectedValue.ToString();
            string proy33 = cbProy33.SelectedValue.ToString();


            string tipo = cbTipo3.SelectedValue.ToString();

            string sql = "INSERT INTO EMPLEADO ( DUI, NIT, ISSS, AFP, Nombres, Apellidos, Nombre_usual, Dir1, Dir2, Renta, cod_isss, cod_sind, cod_afp, cta_banco, tip_cta, tipo_pago, sueldo, ValorHora, Horas, grupo, cod_proy, cod_proy2, cod_proy3, telefono, celular, avisar, ingreso, activo, Modificado ) values ( ";

            sql = sql + " '" + txDUI3.Text.Trim() + "',";
            sql = sql + " '" + txNIT3.Text.Trim() + "',";
            sql = sql + " '" + txISSS3.Text.Trim() + "',";
            sql = sql + " '" + txAFP3.Text.Trim() + "',";

            sql = sql + " '" + txNom3.Text.Trim() + "',";
            sql = sql + " '" + txApe3.Text.Trim() + "',";
            sql = sql + " '" + txUsual3.Text.Trim() + "',";
            sql = sql + " '" + txDir11.Text.Trim() + "',";
            sql = sql + " '" + txDir22.Text.Trim() + "',";

            if (ckRenta3.Checked) sql = sql + " 1,";
            else sql = sql + " 0,";

            if (ckISSS3.Checked) sql = sql + " 1,";
            else sql = sql + " 0,";

            if (ckSind3.Checked) sql = sql + "1,";
            else sql = sql + "0,";
            
            sql = sql + afp + ",";
            sql = sql + "'"+ txCta3.Text.Trim() + "',";
            sql = sql + "'" + cte + "',";

            sql = sql + "'" + tipo + "',";

            //sql = sql + cuota + ",";
            //sql = sql + Sueldo + ",";
            //sql = sql + vHora + ",";
            //sql = sql + horas + ",";
            //sql = sql + lbGrupo + ", ";

            sql = sql + proy31 + ",";
            sql = sql + proy32 + ",";
            sql = sql + proy33 + ",";

            sql = sql + "'" + txTel3.Text + "',";
            sql = sql + "'" + txCel3.Text + "',";
            sql = sql + "'" + txAvisar3.Text + "',";

            DateTime fec = DateTime.Today;
            string hoy = fec.ToString();
            int pos1 = hoy.IndexOf(" ");
            hoy = hoy.Substring(0, pos1);

            sql = sql + "'" + hoy + "', '" +hoy+"',1) ";
            
            fx.UpdateData(sql);

            MessageBox.Show("Creado Nuevo Empleado");
        }

        private void txDUI2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txDUI2_Leave(object sender, EventArgs e)
        {
            CheckLeave("DUI", txDUI2);
        }

        private void txDUI2_Enter(object sender, EventArgs e)
        {
             PrevDUI = txDUI2.Text;
        }

        private void txISSS_Enter(object sender, EventArgs e)
        {
            PrevISSS = txISSS.Text.Trim();
        }

        private void CheckLeave(string Cual, TextBox tx)
        {
            string sql = "select nombre_usual from empleado where ISSS ='" + txISSS.Text.Trim() + "' ";
            string msg = "El ISSS ya lo tiene asignado en la base de datos el empleado ";
            string variable = PrevISSS;

            if (Cual == "DUI")
            {
                sql = "select nombre_usual from empleado where DUI ='" + txDUI.Text.Trim() + "' ";
                msg = "El DUI ya lo tiene asignado en la base de datos el empleado ";
                variable = PrevDUI;
            }

            if (Cual == "NIT")
            {
                sql = "select nombre_usual from empleado where DUI ='" + txNIT.Text.Trim() + "' ";
                msg = "El NIT ya lo tiene asignado en la base de datos el empleado ";
                variable = PrevNIT;
            }

            if (Cual == "AFP")
            {
                sql = "select nombre_usual from empleado where DUI ='" + txAFP.Text.Trim() + "' ";
                msg = "El AFP ya lo tiene asignado en la base de datos el empleado ";
                variable = PrevAFP;
            }


            if (variable != tx.Text.Trim() && tx.Text.Trim() != "")
            {
                Funciones fx = new Funciones();
                string nombre = fx.GetDataValue(sql);
                if (nombre != null)
                {
                    MessageBox.Show(msg + nombre);
                    BtnActualizar.Enabled = false;
                }
                else
                {
                    PrevDUI = "";
                    PrevISSS = "";
                    PrevNIT = "";
                    PrevAFP = "";
                    BtnActualizar.Enabled = true;
                }

            }
        }

        private void txISSS_Leave(object sender, EventArgs e)
        {
            CheckLeave("ISSS", txISSS);
        }

        private void txNIT2_Enter(object sender, EventArgs e)
        {
            PrevNIT = txNIT2.Text.Trim();
        }

        private void txAFP_Enter(object sender, EventArgs e)
        {
            PrevAFP = txAFP.Text.Trim();
        }

        private void txNIT2_Leave(object sender, EventArgs e)
        {
            CheckLeave("NIT", txNIT);
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void Mostrar()
        { 
            string ss = cbTipo.SelectedValue.ToString();

           
            bool a1 = false;
            bool t1 = false;

            
            if (ss == "A") a1 = true;
            if (ss == "V") a1 = true;
            if (ss == "E") a1 = true;
            if (ss == "T") t1 = true;

            txSueldo.Visible = a1;
            lbSueldo.Visible = a1;

          

            txHoras.Visible = t1;
            lbHoras.Visible = t1;
            txVHora.Visible = t1;
            lbVHora.Visible = t1;

            panel5.Visible = true;

            if (a1)
            {
                panel5.Visible = true;
                
            }

            if (ss == "E") panel5.Visible = false;
            
        }

        private void cbTipo3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ss = cbTipo3.SelectedValue.ToString();
            bool o1 = false;
            bool a1 = false;
            bool t1 = false;

            if (ss == "O") o1 = true;
            if (ss == "A") a1 = true;
            if (ss == "V") a1 = true;
            if (ss == "E") a1 = true;
            if (ss == "T") t1 = true;

            txSueldo3.Visible = a1;
            lbSueldo3.Visible = a1;

            txGrupo3.Visible = o1;
            lbGrupo3.Visible = o1;

            txHoras3.Visible = t1;
            lbHoras3.Visible = t1;
            txVHora3.Visible = t1;
            lbVHora3.Visible = t1;

            panel10.Visible = true;
            cbProy32.Visible = false;
            cbProy33.Visible = false;

            if (a1)
            {
                panel10.Visible = true;
                cbProy32.Visible = false;
                cbProy33.Visible = false;
            }

            if (ss == "E") panel10.Visible = false;
            l32.Visible = cbProy32.Visible;
            l33.Visible = cbProy33.Visible;

            if (ss =="V" || ss=="P" || ss=="O" || ss=="T" || ss == "G")
            {
                cbProy32.Visible = true;
                cbProy33.Visible = true;
            }

            l32.Visible = cbProy32.Visible;
            l33.Visible = cbProy33.Visible;
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
    
}
