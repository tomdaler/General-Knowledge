using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace GPS.Documents
{
    public partial class CreateDoc : Form
    {

        public CreateDoc()
        {
            InitializeComponent();
        }
        
        private void CreateDoc_Load(object sender, EventArgs e)
        {
            filtros1.LoadData("Select One");

            ddlEffOwner.DataSource = Variables.ToDatatable(Variables.lstEffOwner, "Select One");
            ddlEffOwner.DisplayMember = "Text";
            ddlEffOwner.ValueMember = "Value";
        }

        private string GetCodigo(string orgs, string depto, string coverage, string types)
        {
            int orgs2 = System.Convert.ToInt32(orgs);
            listado result = Variables.lstOrgs.Find(x => x.Value == orgs2);

            int dpto2 = System.Convert.ToInt32(depto);
            listado result2 = Variables.lstDpto.Find(x => x.Value == dpto2);

            int cov2 = System.Convert.ToInt32(coverage);
            listado result3 = Variables.lstCoverage.Find(x => x.Value == cov2);
            string valor3 = String.Format("{0:00}", System.Convert.ToInt32( result3.Code));

            int type2 = System.Convert.ToInt32(types);
            listado result4 = Variables.lstTypes.Find(x => x.Value == type2);

            string codigo = result.Code.Trim() + '-' + result2.Code.Trim() + '-' + valor3 + '-' + result4.Code.Trim() + '-';
            
            string sql = "select  Max(DocNumber) from tuDocDesc where DocNumber like '"+codigo+"%' ";

            SqlConnection conn = new SqlConnection(Variables.StrConn);
            conn.Open();
            SqlCommand dbComm = new SqlCommand(sql, conn);
            string vvalor = dbComm.ExecuteScalar().ToString();
            conn.Close();

            if (vvalor == null || vvalor =="")
                codigo = codigo + "001";
            else
            {
                vvalor = vvalor.Trim();
                int largo = vvalor.Length;
                string cod2 = vvalor.Substring(0, largo - 3);
                if (cod2 !=codigo)
                {
                    codigo = codigo + "001";
                }
                else
                {
                    string sequencia = vvalor.Substring(largo - 3);
                    int seq = System.Convert.ToInt32(sequencia)+1;
                    sequencia = String.Format("{0:000}", seq);
                    codigo = codigo + sequencia;
                }
            }            
            return codigo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string orgs = filtros1.orgs;
            string types = filtros1.types;
            string dpto = filtros1.dpto;
            string coverage = filtros1.coverage;

            if (coverage == "0")
            {
                MessageBox.Show("Select a Coverage");
                return;
            }

            if (orgs == "0")
            {
                MessageBox.Show("Select an Organization");
                return;
            }

            if (types == "0")
            {
                MessageBox.Show("Select a Document Type");
                return;
            }

            if (dpto == "0")
            {
                MessageBox.Show("Select a Department");
                return;
            }

            string txTitles = txTitle.Text;
            string txDesc = txDescr.Text;

            if (txTitles.Trim() == "")
            {
                MessageBox.Show("Type a Title");
                return;
            }

            if (txDesc.Trim() == "")
            {
                MessageBox.Show("Type a Descrption");
                return;
            }

            string CODIGO = GetCodigo(orgs, dpto, coverage, types);

            string msg = "Insert information with code : " + CODIGO + " ?";
            if (MessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;

            // EXPECTED REVIEW DATE IN 6 MONTHS
            DateTime dt = DateTime.Today;
            dt = dt.AddDays(182);
            string fecha = dt.ToShortDateString();

            string descr = txDescr.Text;
            string titulo = txTitle.Text;
            titulo = Regex.Replace(titulo, "'", "''");
            descr = Regex.Replace(descr, "'", "''");

            string sql = "insert into tuDocDesc(DocTitle, DocNumber, DocDescr, CreateDate, Organization, Coverage, Department, DocType, DocStatus, EfficacyStatus, Version, sharePointLink, CreatedBy, UpdatedBy, EfficacyOwner, ExpectedReviewDate) values ( ";
            sql = sql + "'" + titulo + "', ";
            sql = sql + "'" + CODIGO + "', ";
            sql = sql + "'" + descr + "', GetDate(),  ";

            sql = sql + orgs + ", ";
            sql = sql + coverage + ", ";
            sql = sql + dpto + ", ";
            sql = sql + types + ",1,3,0, '";

            string share = txShare.Text.Trim();
            share = Regex.Replace(share, "'", "''");
            
            sql = sql + share + "', '";
            sql = sql + Variables.Usuario + "',' ";
            sql = sql + Variables.Usuario + "', ";

            sql = sql + ddlEffOwner.SelectedValue.ToString()+",'";
            sql = sql + fecha + "' ) ";

            
            SqlConnection conn = new SqlConnection(Variables.StrConn);
            conn.Open();
            SqlCommand dbComm = new SqlCommand(sql, conn);
            object vvalor = dbComm.ExecuteNonQuery();

            dbComm.CommandText = "Select max(id) from tuDocDesc";
            vvalor = dbComm.ExecuteScalar();
            string numero = vvalor.ToString();
            Variables.DocNumber = numero;

            dbComm.CommandText = "insert into tuDocStatus(Comment, DocNumber, DocStatus, CreatedBy)  values( '', " + numero + ", 1, '"+ Variables.Usuario+"')  ";
            vvalor = dbComm.ExecuteNonQuery();

            sql = "insert into tuEfficacyDetails(Version, EffStatus, DocNumber, ExpectedReviewDate, CreatedBy ) ";
            sql = sql + " values ( 0,3, " + numero + ",'" + fecha + "','"+Variables.Usuario+"' ) ";
                
            dbComm.CommandText = sql;
            vvalor = dbComm.ExecuteNonQuery();

            conn.Close();
            //MessageBox.Show("Document Added : "+CODIGO);
            Variables.bDocLoaded = false;

            Documents.ddlExpect f2 = new Documents.ddlExpect();
            f2.MdiParent = this.ParentForm;
            f2.Show();

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
