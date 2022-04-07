using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace GPS.Documents
{
    public partial class Import : Form
    {
        public string errores;
        public Import()
        {
            InitializeComponent();
        }

        string sqlOwner = "update tuDocDesc set EfficacyOwner =VVV, UpdatedOn = GetDate(), Updatedby ='UUU'  where ID =DDD"; 

        string sqlNewReview1 = "update tuDocDesc set ExpectedReviewDate ='VVV' where id = DDD";
        string sqlNewReview2 = "update tuEfficacyDetails set ExpectedReviewDate ='VVV'  where id = (select max(id) from tuEfficacyDetails where docNumber = DDD)";
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Select file to be upload";
            fDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                lbFile.Text = fDialog.FileName.ToString();               
            }

            if (lbFile.Text.Trim() != "")
            {
                button2.Visible = true;
                button4.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {               
            string sConnecStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + lbFile.Text + ";Extended Properties=Excel 12.0;";
            OleDbConnection CONN = new OleDbConnection(sConnecStr);

            CONN.Open();
            OleDbDataAdapter theDataAdapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", CONN);
            DataSet theDS = new DataSet();
            DataTable dt = new DataTable();
            theDataAdapter.Fill(dt);
            this.dgv1.DataSource = dt.DefaultView;
            CONN.Close();

            dgv1.Visible = true;
            dgv1.Columns[1].Width = 2000;
            button2.Visible = false;
            button4.Visible = true;
        }

        private void Import_Load(object sender, EventArgs e)
        {
            label1.Text= Variables.Report;
            lbFile.Text = "";

            if (!Variables.bDocLoaded)
                {
                    Variables.Documents = Variables.GetDocuments("select id, DocNumber, DocTitle from tuDocDesc");
                }
              Variables.bDocLoaded = true;           
        }

        private bool DioError(string valor, int opcion, string docu, int row)
        {
            if (opcion == 1)
            {
                try
                {
                    valor = GetOwner(valor, row);
                    int dt = int.Parse(valor);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" Incorrect value integer for " + docu + " document " + docu + " on row " + (row + 1).ToString());
                    Variables SEND = new Variables();
                    SEND.Sending(" Error in DioError function for opcion = " + opcion.ToString() + "  row " + (row + 1).ToString() + "  " + Variables.Usuario + " " + ex.ToString());
                    return true;
                }
            }

            if (opcion == 2)
            {
                try
                {
                    DateTime dt = DateTime.Parse(valor);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Incorrect value integer for " + docu + " document " + docu + " on row " + (row + 1).ToString());
                    Variables SEND = new Variables();
                    SEND.Sending(" Error in DioError function for opcion = " + opcion.ToString() + "  row " + (row + 1).ToString()+ "  "+ Variables.Usuario + " " + ex.ToString());
                    return true;
                }
            }
            return false;
        }

        private string OnlyDate(string valor)
        {
            int pos1 = valor.IndexOf(" ");
            if (pos1>0) valor = valor.Substring(0, pos1);
            valor = valor.Trim();
            return valor;
        }

        private string GetContact(string contacto, int i)
        {
            var sss = Variables.lstContact;

            contacto = contacto.Trim();
            listado resultado2 = Variables.lstContact.Find(Y => Y.Text == contacto);
            if (resultado2 != null)
            {
                contacto = resultado2.Code.ToString();
            }
            else
            {
                if (errores == "") errores = "Cannot find contact " + contacto + " on row " + (i + 1).ToString();
                return "";
            }
            return contacto;
        }

        private string GetDocuID(string docu, int i)
        {
            listado resultado2 = Variables.Documents.Find(Y => Y.Code == docu);
            if (resultado2 != null)
            {
                docu = resultado2.Value.ToString();
            }
            else
            {
                MessageBox.Show("Process couldn't find document " + docu + " on row " + (i + 1).ToString());
                return "";
            }
            return docu;
        }

        private string GetOwner(string valor, int row)
        {
            listado resultado = Variables.lstEffOwner.Find(Y => Y.Text == valor);
            if (resultado != null)
            {
                valor = resultado.Value.ToString();
            }
            else
            {
                MessageBox.Show("Process couldn't find in database owner with name " + valor + " on row " + (row + 1).ToString());
                return "";
            }
            return valor;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int opcion = 1; // import data

            if (Variables.Report == "UPDATE REVIEW DATE") opcion=2;

            if (Variables.Report == "UPDATE CONTACT")
            {
                UpdateContacts();
                return;;
            }

            string sql = "";
            string valor2 = "";
            string valor = "";
            SqlConnection conn = new SqlConnection(Variables.StrConn);

            try
            {
                conn.Open();
                SqlCommand dbComm = new SqlCommand();
                dbComm.Connection = conn;

                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    string docu = dgv1.Rows[i].Cells[0].Value.ToString().Trim();
                    valor = dgv1.Rows[i].Cells[1].Value.ToString().Trim();

                    if (docu != "" && valor != "")
                    {
                        docu = GetDocuID(docu, i);
                        if (docu == "") return;

                        if (valor2 != valor)
                        {
                            if (DioError(valor, opcion, docu, i)) return;

                            if (opcion == 1) valor = GetOwner(valor, i);
                            if (opcion == 2) valor = OnlyDate(valor);
                            if (valor == "") return;
                        }

                        valor2 = valor;
                        
                        if (opcion == 1) sql = sqlOwner;
                        if (opcion == 2) sql = sqlNewReview1;

                        sql = sql.Replace("VVV", valor);
                        sql = sql.Replace("UUU", Variables.Usuario);
                        sql = sql.Replace("DDD", docu);
                        dbComm.CommandText = sql;
                        object vvalor = dbComm.ExecuteNonQuery();

                        if (vvalor.ToString() != "1")
                        {
                            MessageBox.Show("Process could't update information on row " + (i + 1).ToString());
                        }

                        if (opcion == 2)
                        {
                            sql = sqlNewReview2;

                            sql = sql.Replace("VVV", valor);
                            sql = sql.Replace("UUU", Variables.Usuario);
                            sql = sql.Replace("DDD", docu);
                            dbComm.CommandText = sql;
                            vvalor = dbComm.ExecuteNonQuery();

                            if (vvalor.ToString() != "1")
                            {
                                MessageBox.Show("Process could't update information on row " + (i + 1).ToString());
                            }
                        }
                    } //if (refe != "" && valor != "")
                } // for
            }
            catch (Exception ex)
            {
                MessageBox.Show("  import update " +sql + ' ' + ex.ToString());
                Variables SEND = new Variables();
                SEND.Sending(" error in Update Import  " + Variables.Usuario+ " "+ sql + " "+ ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            MessageBox.Show("Data Updated");
        }

        private void UpdateContacts()
        {
            string delete = " Delete from tuDocCollab where DocNumber in (";
            List<listContacto> lista = new List<listContacto>();

            errores = "";
            string comma = "";
            string contacto = "";

            for (int row = 0; row < dgv1.Rows.Count; row++)
            {
                string docu = dgv1.Rows[row].Cells[0].Value.ToString().Trim();
                string valor = dgv1.Rows[row].Cells[1].Value.ToString().Trim();

                if (docu != "" && valor != "")
                {
                    docu = GetDocuID(docu, row);
                    if (docu == "") return;

                    string[] valores = valor.Split(',').Select(str => str.Trim()).ToArray();

                    for (int k = 0; k < valores.Length; k++)
                    {
                        contacto = GetContact(valores[k], row);
               
                        if (contacto != "")
                        {
                            listContacto nuevo2 = new listContacto();
                            nuevo2.doc = docu;
                            nuevo2.contact = contacto;
                            lista.Add(nuevo2);
                        }
                    }

                    delete = delete + comma + docu;
                    comma = ",";
                }
            }

            if (errores!="")
            {
                MessageBox.Show(errores);
                return;
            }
            DataAccess up = new DataAccess();

            delete = delete + ")";
            up.Actualize(delete);

            for (int i = 0; i<lista.Count; i++)
            {
                listContacto nuevo = new listContacto();
                nuevo = lista[i];
                string sql = "insert into tuDocCollab (DocNumber, Contact) values (";
                sql = sql + nuevo.doc + "," + nuevo.contact + ")";
                up.Actualize(sql);
            }

            MessageBox.Show("Finished updated");
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class listContacto
    {
        public string doc;
        public string contact;
    }
}
