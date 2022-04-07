using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GPS.Documents
{
    public partial class ddlExpect : Form
    {
        bool bLoadEfficacy = false;
        bool bLoadStatus = false;
        //bool bLoadRelated = false;
        bool bLoadContact = false;
        string GLOBALOWNER = "";

      

        public ddlExpect()
        {
            InitializeComponent();
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            tabctrlDocEdit.TabPages.RemoveAt(5);

            //tabctrlDocEdit.TabPages.Remove(4).Hide();

            //tabctrlDocEdit.TabPages(0).Hide();

           

            bLoadEfficacy = false;
            bLoadStatus = false;
            //bLoadRelated = false;
            bLoadContact = false;

            //dt3.MinDate = DateTime.Now.AddDays(-183);
            //dt3.MaxDate = DateTime.Now.AddDays(365);

            CleanCombos();

            string texto = "-- All --";
            filtros1.LoadData(texto);

            texto = "";
            //ddlOrg.DataSource = Variables.ToDatatable(Variables.lstOrgs, texto);
            //ddlOrg.DisplayMember = "Text";
            //ddlOrg.ValueMember = "Value";

            //ddlCoverage.DataSource = Variables.ToDatatable(Variables.lstCoverage, texto);
            //ddlCoverage.DisplayMember = "Text";
            //ddlCoverage.ValueMember = "Value";

            //ddlType.DataSource = Variables.ToDatatable(Variables.lstTypes, texto);
            //ddlType.DisplayMember = "Text";
            //ddlType.ValueMember = "Value";

            //ddlDepto.DataSource = Variables.ToDatatable(Variables.lstDpto, texto);
            //ddlDepto.DisplayMember = "Text";
            //ddlDepto.ValueMember = "Value";

            ddlStatus.DataSource = Variables.ToDatatable(Variables.lstStatus, texto);
            ddlStatus.DisplayMember = "Text";
            ddlStatus.ValueMember = "Value";


            ComboboxItem item2 = new ComboboxItem();
            item2.Text = "";
            item2.Value = 0;
            ddlStatus3.Items.Add(item2);
            ddlStatus4.Items.Add(item2);

            foreach (listado l in Variables.lstStatus)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = l.Text;
                item.Value = l.Value;
                ddlStatus3.Items.Add(item);
                ddlStatus4.Items.Add(item);
            }

            foreach (listado l in Variables.lstEfficacy)
            {
                int valor = l.Value;
                string textos = l.Text;

                if (valor != 1 && valor != 2 && valor != 4 && valor != 5)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = textos;
                    item.Value = valor;
                    ddlEff.Items.Add(item);
                }
            }

            ddlEff.SelectedIndex = 0;


            //ddlEffOwner.DataSource = Variables.ToDatatable(Variables.lstEffOwner, texto);
            //ddlEffOwner.DisplayMember = "Text";
            //ddlEffOwner.ValueMember = "Value";
            ComboboxItem item3 = new ComboboxItem();
            item3.Text = "";
            item3.Value = 0;

            ddlEffOwner.Items.Add(item3);


            foreach (listado i in Variables.lstEffOwner)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = i.Text;
                item.Value = i.Value;

                ddlEffOwner.Items.Add(item);
            }

            foreach (listado i in Variables.lstContacts)
            {
                listBox5.Items.Add(i.Text);
            }

            foreach (listado i in Variables.lstResponsibles)
            {
                listBox6.Items.Add(i.Text);
            }

            if (Variables.DocNumber != "") LoadDocument();
        }

       
        private void CleanCombos()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();

            try
            {
                listBox5.ClearSelected();
                listBox6.ClearSelected();
                listBox7.ClearSelected();
            }
            catch (Exception ex)
            {
                Variables SEND = new Variables();
                SEND.Sending("Procedure CleanCombos "+ ex.ToString());
            }
        }

        private void LoadDocument()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();

            try
            {
                listBox5.ClearSelected();
                listBox6.ClearSelected();
                listBox7.ClearSelected();
            }
            catch (Exception ex)
            {
                Variables SEND = new Variables();
                SEND.Sending("Procedure LoadDocument "+ex.ToString());
            }

            SqlConnection conn = new SqlConnection(Variables.StrConn);

            try
            {

                conn.Open();

                string sql = "SELECT * FROM tuDocDesc t1 where t1.ID>0 ";

                if (Variables.DocNumber != "")
                {
                    sql = sql + " and t1.id = " + Variables.DocNumber;
                }
                else
                {
                    if (txID2.Text.Trim() != "")
                    {
                        sql = sql + " and t1.id = " + txID2.Text.Trim();
                    }
                    else
                    {
                        if (txRef2.Text.Trim() != "")
                        {
                            sql = sql + " and t1.DocNumber = '" + txRef2.Text.Trim() + "' ";
                        }
                        else
                        {
                            if (txTitle2.Text.Trim() != "")
                            {
                                sql = sql + " and t1.DocTitle like '%" + txTitle2.Text.Trim() + "%'";
                            }
                            else
                                return;
                        }
                    }
                }

                SqlCommand comando = new SqlCommand(sql, conn);
                comando.CommandText = sql;

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = comando;

                string DOCUMENTO = "0";

                DataTable dt12 = new DataTable();
                dataAdapter.Fill(dt12);
                foreach (DataRow row in dt12.Rows)
                {
                    DOCUMENTO = row["ID"].ToString();
                    DocNum.Text = row["ID"].ToString();
                    lbRef.Text = row["DocNumber"].ToString();

                    txTitle.Text = row["DocTitle"].ToString();
                    txDescr.Text = row["DocDescr"].ToString();

                    string ss = row["CreateDate"].ToString();
                    int pos = ss.IndexOf(" ");
                    if (pos > 1) ss = ss.Substring(0, pos);
                    txCreated.Text = ss;

                    txShare.Text = row["SharePointLink"].ToString();
                    txPath.Text = row["Path"].ToString();
                    txAgentPub.Text = row["AgentPublished"].ToString();

                    int valor = System.Convert.ToInt32(row["DocStatus"].ToString());
                    listado resultado = Variables.lstStatus.Find(Y => Y.Value == valor);
                    if (resultado != null) lbStatus.Text = "Status: " + resultado.Text;

                    valor = System.Convert.ToInt32(row["EfficacyStatus"].ToString());
                    resultado = Variables.lstEfficacy.Find(Y => Y.Value == valor);
                    if (resultado != null) lbEff.Text = "Efficacy: " + resultado.Text;

                    txVersion.Text = row["Version"].ToString();

                    valor = System.Convert.ToInt32(row["Organization"].ToString());
                    resultado = Variables.lstOrgs.Find(Y => Y.Value == valor);
                    if (resultado != null) lbOrg.Text = resultado.Text;


                    valor = System.Convert.ToInt32(row["DocType"].ToString());
                    resultado = Variables.lstTypes.Find(Y => Y.Value == valor);
                    if (resultado != null) lbType.Text = resultado.Text;

                    valor = System.Convert.ToInt32(row["Department"].ToString());
                    resultado = Variables.lstDpto.Find(Y => Y.Value == valor);
                    if (resultado != null) lbDpto.Text = resultado.Text;

                    valor = System.Convert.ToInt32(row["Coverage"].ToString());
                    resultado = Variables.lstCoverage.Find(Y => Y.Value == valor);
                    if (resultado != null) lbCoverage.Text = resultado.Text;

                    ddlEffOwner.SelectedIndex = 0;

                    string asignado = row["EfficacyOwner"].ToString();
                    if (asignado != "")
                    {

                        valor = System.Convert.ToInt32(asignado.ToString());
                        listado l = Variables.lstEffOwner.Find(x => x.Value == System.Convert.ToInt32(valor));
                        if (l != null)
                        {
                            ddlEffOwner.SelectedIndex = ddlEffOwner.FindStringExact(l.Text.ToString());
                            GLOBALOWNER = ddlEffOwner.Text;
                            //ddlEffOwner.SelectedValue = valor.ToString();
                        }
                    }

                    tabctrlDocEdit.SelectedIndex = 1;
                }


                sql = " select DocNumber +' ' + DocTitle from tuDocDesc t1, tuRelatedDocs t2 ";
                sql = sql + " where t1.id = t2.RelatedDoc and  t2.PrimaryDoc = " + DOCUMENTO;
                comando.CommandText = sql;
                dataAdapter.SelectCommand = comando;
                DataTable dt16 = new DataTable();
                dataAdapter.Fill(dt16);
                foreach (DataRow row in dt16.Rows)
                {
                    listBox4.Items.Add(row[0].ToString());
                }

                bt1.Enabled = true;
                bt2.Enabled = true;
                bt3.Enabled = true;
                bt4.Enabled = true;
                bt5.Enabled = true;
                bt6.Enabled = true;
                bt7.Enabled = true;
                bt8.Enabled = true;


                bt11.Enabled = true;
                bt22.Enabled = true;
                bt33.Enabled = true;

                button1.Enabled = true;
            }
            catch (Exception ex)
            {
                string e1 = ex.ToString();
                MessageBox.Show(e1);
                Variables SEND = new Variables();
                SEND.Sending("Procedure Load Document part 2 "+ ex.ToString());

            }
            finally
            {
                conn.Close();
            }

        }

        private void UpdateDataBase(string sql, string Procedimiento)
        {
            SqlConnection conn = new SqlConnection(Variables.StrConn);
            try
            {
                conn.Open();

                SqlCommand dbComm = new SqlCommand(sql, conn);
                object vvalor = dbComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(sql + ' ' + ex.ToString());
                Variables SEND = new Variables();
                SEND.Sending(Procedimiento + " "+sql+ " "+ ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //string coverage = ddlCoverage.SelectedValue.ToString();
            //string orgs = ddlOrg.SelectedValue.ToString();
            //string types = ddlType.SelectedValue.ToString();
            //string dpto = ddlDepto.SelectedValue.ToString();

            string owner = ddlEffOwner.Text;
            if (owner == "")
                owner = "0";
            else
            {
                listado resultado = Variables.lstEffOwner.Find(Y => Y.Text == owner);
                owner = resultado.Value.ToString();
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

            string sql = "update tuDocDesc set ";

            string titulo = txTitles.Trim();
            string descr = txDesc.Trim();

            titulo = Regex.Replace(titulo, "'", "''");
            descr = Regex.Replace(descr, "'", "''");

            sql = sql + " DocTitle = '" + titulo + "', ";
            sql = sql + " DocDescr = '" + descr + "', ";
            // sql = sql + " Coverage = " + coverage + ", ";
            // sql = sql + " Organization = " + orgs + ", ";
            // sql = sql + " Department = " + dpto + ", ";
            // sql = sql + " DocType = " + types + ", ";

            string ss = txShare.Text.Trim();
            ss = ss.Replace("'", "''");
            sql = sql + " SharePointLink = ' " + ss + "', ";
            //=======
            ss = txAgentPub.Text.Trim();
            ss = ss.Replace("'", "''");
            sql = sql + " AgentPublished='" + ss + "', ";
            //=======            
            ss = txPath.Text.Trim();
            ss = ss.Replace("'", "''");
            sql = sql + " path='" + ss + "', ";
        
            sql = sql + " EfficacyOwner = " + owner + ", ";

            // RAYMOND
            if (GLOBALOWNER != owner)
                sql = sql + " dtAssigned = GetDate(), ";

            sql = sql + " UpdatedOn = GetDate() , ";
            sql = sql + " UpdatedBy = '" + Variables.Usuario + "' ";

            sql = sql + " where ID = " + Variables.DocNumber;

            Variables.bDocLoaded = false;
            UpdateDataBase(sql, sql);
            MessageBox.Show("Data Updated");
        }

        private void btnsSearchC_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < listBox5.Items.Count; x++)
            {
                if (listBox5.GetSelected(x))
                {
                    string item = listBox5.Items[x].ToString();
                    listado resultado = Variables.lstContacts.Find(Y => Y.Text == item);
                    string valor = resultado.Value.ToString();

                    listBox1.Items.Add(item);

                    string sql = "insert into tuDocCollab (DocNumber, Contact, CreatedBy) values ( " + Variables.DocNumber + "," + valor + ",'" + Variables.Usuario + "') ";
                    UpdateDataBase(sql, sql );
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < listBox5.Items.Count; x++)
            {
                if (listBox5.GetSelected(x))
                {
                    string item = listBox5.Items[x].ToString();
                    listado resultado = Variables.lstContacts.Find(Y => Y.Text == item);
                    string valor = resultado.Value.ToString();

                    listBox2.Items.Add(item);

                    string sql = "insert into tuDocOwner (DocNumber, Contact, CreatedBy) values ( " + Variables.DocNumber + "," + valor + ", '" + Variables.Usuario + "') ";
                    UpdateDataBase(sql, "Insert into tuDocOwner");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < listBox6.Items.Count; x++)
            {
                if (listBox6.GetSelected(x))
                {
                    string item = listBox6.Items[x].ToString();
                    listado resultado = Variables.lstResponsibles.Find(Y => Y.Text == item);
                    string valor = resultado.Value.ToString();

                    listBox3.Items.Add(item);

                    string sql = "insert into tuResponsibleGroups (DocNumber, GroupName, CreatedBy) values ( " + Variables.DocNumber + "," + valor + ",'" + Variables.Usuario + "' ) ";
                    UpdateDataBase(sql, "insert into tuResponsibleGroups");
                }
            }

            listBox6.ClearSelected();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < listBox7.Items.Count; x++)
            {
                if (listBox7.GetSelected(x))
                {
                    string item = listBox7.Items[x].ToString();
                    int pos1 = item.IndexOf(" ");
                    string valor = item.Substring(0, pos1);

                    listado resultado = Variables.Documents.Find(Y => Y.Code == valor);
                    valor = resultado.Value.ToString();

                    listBox4.Items.Add(item);

                    string sql = "insert into tuRelatedDocs (PrimaryDoc, RelatedDoc, CreatedBy ) values ( " + Variables.DocNumber + "," + valor + ",'" + Variables.Usuario + "' ) ";
                    UpdateDataBase(sql, "insert into tuRelatedDocs ");
                }
            }

            listBox7.ClearSelected();
        }

        private void btnUpdateContributors_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < listBox1.Items.Count; x++)
            {
                if (listBox1.GetSelected(x))
                {
                    string item = listBox1.Items[x].ToString();
                    listado resultado = Variables.lstContacts.Find(Y => Y.Text == item);
                    string valor = resultado.Value.ToString();

                    listBox1.Items.RemoveAt(x);
                    string sql = "delete from tuDocCollab where DocNumber=" + Variables.DocNumber + " and  Contact=" + valor;
                    UpdateDataBase(sql, "delete from tuDocCollab ");
                }
            }

            listBox1.ClearSelected();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < listBox2.Items.Count; x++)
            {
                if (listBox2.GetSelected(x))
                {
                    string item = listBox2.Items[x].ToString();
                    listado resultado = Variables.lstContacts.Find(Y => Y.Text == item);
                    string valor = resultado.Value.ToString();

                    listBox2.Items.RemoveAt(x);
                    string sql = "delete from tuDocOwner where DocNumber=" + Variables.DocNumber + " and  Contact=" + valor;
                    UpdateDataBase(sql, "delete from tuDocOwner ");
                }
            }

            listBox2.ClearSelected();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < listBox3.Items.Count; x++)
            {
                if (listBox3.GetSelected(x))
                {
                    string item = listBox3.Items[x].ToString();
                    listado resultado = Variables.lstResponsibles.Find(Y => Y.Text == item);
                    string valor = resultado.Value.ToString();

                    listBox3.Items.RemoveAt(x);
                    string sql = "delete from tuResponsibleGroups where DocNumber=" + Variables.DocNumber + " and  GroupName=" + valor;
                    UpdateDataBase(sql, "delete from tuResponsibleGroups");
                }
            }
            listBox3.ClearSelected();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < listBox4.Items.Count; x++)
            {
                if (listBox4.GetSelected(x))
                {
                    string item = listBox4.Items[x].ToString();
                    int pos1 = item.IndexOf(" ");
                    string valor = item.Substring(0, pos1);

                    listado resultado = Variables.Documents.Find(Y => Y.Code == valor);
                    valor = resultado.Value.ToString();

                    listBox4.Items.RemoveAt(x);
                    string sql = "delete from tuRelatedDocs where PrimaryDoc=" + Variables.DocNumber + " and  RelatedDoc=" + valor;
                    UpdateDataBase(sql, "delete from tuRelatedDocs ");
                }
            }

            listBox4.ClearSelected();
        }

        private void btnNewComment_Click(object sender, EventArgs e)
        {
            string comment = txCommentStatus.Text.Trim();
            comment = Regex.Replace(comment, "'", "''");

            string valor = ddlStatus.SelectedValue.ToString();
            string sql = "insert into tuDocStatus (DocNumber, CreateDate, DocStatus, Comment, CreatedBy) values ( ";
            sql = sql + Variables.DocNumber + " , GetDate(), " + valor + ",'" + comment + "','" + Variables.Usuario + "') ";
            UpdateDataBase(sql, "insert into tuDocStatus ");

            lbStatus.Text = "Status :" + ddlStatus.Text;

            DataTable dataTable = (DataTable)dGridStatus.DataSource;
            DataRow drToAdd = dataTable.NewRow();

            drToAdd[0] = DateTime.Now.ToString("MM-dd-yyyy");
            drToAdd[1] = Variables.Usuario;
            drToAdd[2] = ddlStatus.Text;
            drToAdd[3] = txCommentStatus.Text;

            dataTable.Rows.Add(drToAdd);
            dataTable.AcceptChanges();
            dGridStatus.DataSource = dataTable;

            dGridStatus.Columns[0].Width = 80;
            dGridStatus.Columns[1].Width = 80;
            dGridStatus.Columns[2].Width = 80;

            sql = "update tuDocDesc set ";

            sql = sql + " DocStatus = " + valor + ", ";

            // RAYMOND
            if (valor == "3") sql = sql + " dtRetired = GetDate() , ";

            sql = sql + " UpdatedOn = GetDate() , ";
            sql = sql + " UpdatedBy = '" + Variables.Usuario + "' ";
            sql = sql + " where ID = " + Variables.DocNumber;

            UpdateDataBase(sql, "update tuDocDesc btnNewComment_Click ");
        }
        private string GetDate(string dt, int i)
        {
            DateTime fec1 = System.Convert.ToDateTime(dt);
            
            string dt11 = fec1.Year.ToString() + "-" + String.Format("{0:00}", fec1.Month) + "-" + String.Format("{0:00}", fec1.Day);
            string dt11s = String.Format("{0:00}", fec1.Month) + "-" + String.Format("{0:00}", fec1.Day) + "-" + fec1.Year.ToString();
            if (i == 1) return dt11;
            return dt11s;
        }

        private void btnEfficacyUpdate_Click(object sender, EventArgs e)
        {
            string dt = dtExpected.Value.ToString();
            string dtComplete = GetDate(dt,1);
            string grComplete = GetDate(dt,2);

            dt = dtRevision.Value.ToString();
            string Revision = GetDate(dt, 1);
            string grReview = GetDate(dt, 2);

            if (Revision == "") return;
            int version = 0;

            string efficacy = ddlEff.Text;
            listado resultado = Variables.lstEfficacy.Find(Y => Y.Text == efficacy);
            efficacy = resultado.Value.ToString();

            int valor = System.Convert.ToInt32(CHK1.Checked);
            try
            {
                version = System.Convert.ToInt32(txVersion.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Version must be a number");
                Variables SEND = new Variables();
                SEND.Sending(ex.ToString());
                return;
            }

            string comentario = txCommentEff.Text.Trim();
            comentario = Regex.Replace(comentario, "'", "''");

            string sql = "insert into tuEfficacyDetails(Version, EffStatus, ExpectedReviewDate, RevCompleteDate, ";
            sql = sql + " RevisionRequired, Comments, DocNumber, CreatedBy) values ( ";

            sql = sql + version.ToString() + ",";
            sql = sql + efficacy;

            sql = sql + ", '" + dtComplete + "',";
            sql = sql + "'" + Revision + "',";

            sql = sql + valor.ToString() + ", ";
            sql = sql + "'" + comentario + "',";
            sql = sql + Variables.DocNumber + ",'";
            sql = sql + Variables.Usuario + "' ) ";

            lbEff.Text = "Efficacy : " + ddlEff.Text;
            UpdateDataBase(sql, "insert into tuEfficacyDetails ");

            sql = "update tuDocDesc set ";

            sql = sql + " ExpectedReviewDate ='" + dtComplete + "',";
            sql = sql + " RevCompleteDate='" + Revision + "',";
            // sql = sql + " EfficacyOwner = " + owner + ",";
            sql = sql + " RevisionRequired=" + valor.ToString() + ",";
            sql = sql + " Version=" + version.ToString() + ",";

            sql = sql + " EfficacyStatus=" + efficacy + ",";
            sql = sql + " UpdatedOn = GetDate() , ";
            sql = sql + " EfficacyLastChangedBy = '" + Variables.Usuario + "', ";

            sql = sql + " UpdatedBy = '" + Variables.Usuario + "' ";

            sql = sql + " where id = " + Variables.DocNumber;

            UpdateDataBase(sql, sql );

            DataTable dataTable = (DataTable)dGridEfficacy.DataSource;
            DataRow drToAdd = dataTable.NewRow();

            string CHK11 = "NO";
            if (CHK1.Checked) CHK11 = "YES";

            drToAdd[0] = txVersion.Text.Trim();
            drToAdd[1] = ddlEff.Text;

            drToAdd[2] = grComplete;
            drToAdd[3] = grReview;
            
            drToAdd[4] = CHK11;
            drToAdd[5] = Variables.Usuario;
            //--- TOMAS
            drToAdd[6] = txCommentEff.Text.Trim();

            dataTable.Rows.Add(drToAdd);
            dataTable.AcceptChanges();
            dGridEfficacy.DataSource = dataTable;
        }

        private void tabctrlDocEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabctrlDocEdit.SelectedIndex == 2)
                {
                    DateTime dt = DateTime.Now.AddMonths(6);

                    dtRevision.Value = DateTime.Now;
                    dtExpected.Value = dt;

                    if (!bLoadEfficacy && Variables.DocNumber != "")
                    {
                        string sql = "select Version, t3.EfficacyStatus AS Status, ";
                        sql = sql + " Convert(varchar, t1.ExpectedReviewDate, 110) ExpectedReview, ";
                        sql = sql + " Convert(varchar, t1.RevCompleteDate, 110) Completed, ";
                        sql = sql + " CASE WHEN RevisionRequired = 0 then 'NO' ELSE 'YES' END Required , ";
                        //sql = sql + " (select EfficacyOwner from tsEfficacyOwner where ID = t1.EfficacyOwner) EffOwner, ";

                        sql = sql + " t1.CreatedBy, ";
                        sql = sql + " t1.Comments ";

                        sql = sql + " from tuEfficacyDetails t1, tsEfficacyStatus t3 ";
                        sql = sql + " where t1.EffStatus = t3.ID  ";
                        sql = sql + " and t1.DocNumber = " + Variables.DocNumber;

                        dGridEfficacy.DataSource = Variables.GetDT(sql);

                        bLoadEfficacy = true;
                    }

                    if (dGridEfficacy.Rows.Count>0)
                    {
                        dGridEfficacy.Columns[0].Width = 50;
                        dGridEfficacy.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        dGridEfficacy.Columns[1].Width = 90;

                        dGridEfficacy.Columns[2].Width = 90;
                        dGridEfficacy.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        dGridEfficacy.Columns[3].Width = 90;
                        dGridEfficacy.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        dGridEfficacy.Columns[4].Width = 65;
                        dGridEfficacy.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        dGridEfficacy.Columns[5].Width = 65;
                        dGridEfficacy.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }

                if (tabctrlDocEdit.SelectedIndex == 3)
                {
                    if (!bLoadContact && Variables.DocNumber != "")
                    {
                        string sql = "select name  from tsContacts t1, tuDocCollab t2  where t1.id = t2.contact and t2.DocNumber = " + Variables.DocNumber;
                        DataTable dt13 = new DataTable();

                        dt13 = Variables.GetDT(sql);
                        foreach (DataRow row in dt13.Rows)
                        {
                            listBox1.Items.Add(row[0].ToString());
                        }

                        sql = "select name  from tsContacts t1, tuDocOwner t2  where t1.id = t2.contact and t2.DocNumber = " + Variables.DocNumber;
                        dt13 = Variables.GetDT(sql);
                        foreach (DataRow row in dt13.Rows)
                        {
                            listBox2.Items.Add(row[0].ToString());
                        }

                        sql = "select title  from tsResponsables t1, tuResponsibleGroups t2  where t1.id = t2.GroupName and t2.DocNumber = " + Variables.DocNumber;
                        dt13 = Variables.GetDT(sql);
                        foreach (DataRow row in dt13.Rows)
                        {
                            listBox3.Items.Add(row[0].ToString());
                        }

                        bLoadContact = true;
                    }
                }

                if (tabctrlDocEdit.SelectedIndex == 4)
                {
                    if (!bLoadStatus && Variables.DocNumber != "")
                    {
                        string sql = " select Convert(varchar, createDate, 110) Date, t1.CreatedBy CreatedBy, t2.DocStatus Status, Comment ";
                        sql = sql + " from tuDocStatus t1, tsDocStatus t2  ";
                        sql = sql + " where t1.DocStatus = t2.ID and t1.DocNumber=" + Variables.DocNumber;
                        dGridStatus.DataSource = Variables.GetDT(sql);

                        bLoadStatus = true;
                    }

                    dGridStatus.Columns[0].Width = 80;
                    dGridStatus.Columns[1].Width = 100;
                    dGridStatus.Columns[2].Width = 100;
                }

                if (tabctrlDocEdit.SelectedIndex == 5)
                {
                    if (!Variables.bDocLoaded)
                    {
                        Variables.Documents = Variables.GetDocuments("select id, DocNumber, DocTitle from tuDocDesc");
                    }
                    Variables.bDocLoaded = true;
                }
            }
            catch (Exception ex)
            {
                Variables SEND = new Variables();
                SEND.Sending(ex.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = listBox4.SelectedItems.Count - 1; i >= 0; i--)
            {
                string item = listBox4.Items[listBox4.SelectedIndex].ToString();
                listBox4.Items.Remove(listBox4.SelectedItems[i]);
                int pos1 = item.IndexOf(" ");

                string valor = item.Substring(0, pos1);
                valor = valor.Trim();

                listado resultado = Variables.Documents.Find(Y => Y.Code == valor);
                Variables.DocNumber = resultado.Value.ToString();

                LoadDocument();
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string texto = "";
            //ddlOrg.DataSource = Variables.ToDatatable(Variables.lstOrgs, texto);
            //ddlOrg.DisplayMember = "Text";
            //ddlOrg.ValueMember = "Value";

            //ddlCoverage.DataSource = Variables.ToDatatable(Variables.lstCoverage, texto);
            //ddlCoverage.DisplayMember = "Text";
            //ddlCoverage.ValueMember = "Value";

            //ddlType.DataSource = Variables.ToDatatable(Variables.lstTypes, texto);
            //ddlType.DisplayMember = "Text";
            //ddlType.ValueMember = "Value";

            //ddlDepto.DataSource = Variables.ToDatatable(Variables.lstDpto, texto);
            //ddlDepto.DisplayMember = "Text";
            //ddlDepto.ValueMember = "Value";

            ddlStatus.DataSource = Variables.ToDatatable(Variables.lstStatus, texto);
            ddlStatus.DisplayMember = "Text";
            ddlStatus.ValueMember = "Value";

            ddlEff.DataSource = Variables.ToDatatable(Variables.lstEfficacy, texto);
            ddlEff.DisplayMember = "Text";
            ddlEff.ValueMember = "Value";
        }

        private void button9_Click_1(object sender, EventArgs e)
        {

            bLoadEfficacy = false;
            bLoadStatus = false;
            //bLoadRelated = false;
            bLoadContact = false;

            Variables.DocNumber = "";
            dGrid1.Visible = false;

            string stado = ddlStatus4.Text;
            if (stado == "")
                stado = "0";
            else
            {
                listado resultado = Variables.lstStatus.Find(Y => Y.Text == stado);
                if (resultado == null)
                    stado = "0";
                else
                    stado = resultado.Value.ToString();
            }

            string sql = "SELECT t1.ID, t1.DocNumber Number, DocTitle Title, CreateDate Created, ";
            sql = sql + " t2.DocType Type, t3.CoverageDescr Coverage, t4.Code Depto, t5.orgcode Org, t6.DocStatus Status, t7.EfficacyStatus ";

            sql = sql + " FROM tuDocDesc t1, tsDocumentType t2, tsCoverage t3, tsDepartments t4, tsOrgList t5, tsDocStatus t6, tsEfficacyStatus t7 ";
            sql = sql + " where t1.DocType = t2.ID ";
            sql = sql + " and t1.Coverage = t3.ID ";
            sql = sql + " and t1.Department = t4.id ";
            sql = sql + " and t1.organization = t5.id ";
            sql = sql + " and t1.DocStatus = t6.ID ";
            sql = sql + " and t1.EfficacyStatus = t7.ID ";

            if (stado != "0") sql = sql + " and t1.DocStatus =" + stado;

            if (txID2.Text.Trim() != "")
            {
                sql = sql + " and t1.id = " + txID2.Text.Trim();
            }
            else
            {
                if (txRef2.Text.Trim() != "")
                {
                    sql = sql + " and t1.DocNumber LIKE '%" + txRef2.Text.Trim() + "%' ";
                }
                else
                {
                    if (txTitle2.Text.Trim() != "")
                    {
                        sql = sql + " and t1.DocTitle like '%" + txTitle2.Text.Trim() + "%'";
                    }
                    else
                        if (stado == "0")
                    {
                        MessageBox.Show("Choose conditions to retrieve documents");
                        return;
                    }
                }
            }
            SqlConnection conn = new SqlConnection(Variables.StrConn);

            try
            {
                conn.Open();

                SqlCommand comando = new SqlCommand(sql, conn);
                System.Data.IDataReader dr = comando.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);

                conn.Close();
                //SqlDataAdapter dataAdapter = new SqlDataAdapter();
                //dataAdapter.SelectCommand = comando;

                //DataTable dt = new DataTable();
                //dataAdapter.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No Data, Select Other Conditions");
                }

                if (dt.Rows.Count == 1)
                {
                    Variables.DocNumber = dt.Rows[0][0].ToString();
                    LoadDocument();
                }

                if (dt.Rows.Count > 1)
                {
                    dGrid1.DataSource = dt;

                    dGrid1.Visible = true;

                    dGrid1.Columns[0].Width = 40;
                    dGrid1.Columns[1].Width = 140;
                    dGrid1.Columns[2].Width = 230;

                    dGrid1.Columns[3].Width = 80;
                    dGrid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dGrid1.Columns[4].Width = 90;

                    dGrid1.Columns[6].Width = 50;
                    dGrid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dGrid1.Columns[7].Width = 50;
                    dGrid1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            catch (Exception ex)
            {
                string e1 = ex.ToString();
                MessageBox.Show(e1);
                Variables SEND = new Variables();
                SEND.Sending(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void dGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex> -1 &&dGrid1.Rows[e.RowIndex].Cells[0].Value != null )
            {
                Variables.DocNumber = dGrid1.Rows[e.RowIndex].Cells[0].Value.ToString();
                LoadDocument();
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            string id = txID3.Text.Trim();

            if (id.All(char.IsDigit))
            {
                int ID2 = System.Convert.ToInt32(id);
                listado resultado = Variables.Documents.Find(x => x.Value == ID2);
                listBox7.Items.Clear();
                if (resultado.Value != 0)
                {
                    listBox7.Items.Add(resultado.Value.ToString() + ' ' + resultado.Text + ' ' + resultado.Code);
                }
                return;
            }

            string code = txRef3.Text.Trim();
            string title3 = txTitle3.Text.Trim();

            //if (txID3.Text.Trim!)
            //esult = Variables.lstOrgs.Find(x => x.Value == orgs2);
        }

        private void Load_Ref(string codigo)
        {
            listBox7.Items.Clear();
            if (codigo == "") return;
            for (int i = 0; i < Variables.Documents.Count; i++)
            {
                if (Variables.Documents[i].Code.Contains(codigo))
                    listBox7.Items.Add(Variables.Documents[i].Code + ' ' + Variables.Documents[i].Text);
            }
        }

        private void Load_Title(string titulo)
        {
            listBox7.Items.Clear();
            if (titulo == "") return;
            for (int i = 0; i < Variables.Documents.Count; i++)
            {
                if (Variables.Documents[i].Text.Contains(titulo))
                    listBox7.Items.Add(Variables.Documents[i].Code + ' ' + Variables.Documents[i].Text);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string orgs = filtros1.orgs;
            string types = filtros1.types;
            string dpto = filtros1.dpto;
            string coverage = filtros1.coverage;
            string stado = ddlStatus3.Text;
            if (stado == "")
                stado = "0";
            else
            {
                listado resultado = Variables.lstStatus.Find(Y => Y.Text == stado);
                if (resultado == null)
                    stado = "0";
                else
                    stado = resultado.Value.ToString();
            }

            // BY ID      
            string id = txID3.Text.Trim();
            if (id == "") id = "0";

            if (id.All(char.IsDigit) && id != "0")
            {
                listBox7.Items.Clear();
                int ID2 = System.Convert.ToInt32(id);
                for (int i = 0; i < Variables.Documents.Count; i++)
                {
                    if (Variables.Documents[i].Value == ID2)
                        listBox7.Items.Add(Variables.Documents[i].Code + ' ' + Variables.Documents[i].Text);
                }
                return;
            }

            string codigo = txRef3.Text.Trim();
            string titulo = txTitle3.Text.Trim();

            // ALL
            if (titulo == "" && codigo == "" && orgs == "0" && types == "0" && dpto == "0" && coverage == "0" && stado == "0")
            {
                listBox7.Items.Clear();
                foreach (listado item in Variables.Documents)
                {
                    listBox7.Items.Add(item.Code.ToString() + ' ' + item.Text);
                }
                return;
            }

            if (codigo != "" && orgs == "0" && types == "0" && dpto == "0" && coverage == "0" && stado == "0")
            {
                Load_Ref(codigo);
                return;
            }

            if (titulo != "" && orgs == "0" && types == "0" && dpto == "0" && coverage == "0" && stado == "0")
            {
                Load_Title(titulo);
                return;
            }

            if (orgs != "0" || types != "0" || dpto != "0" || coverage != "0" || stado != "0")
            {
                string sql = "SELECT t1.DocNumber Number, DocTitle Title FROM tuDocDesc t1 where id>0 ";
                if (types != "0") sql = sql + " and t1.DocType =" + types;
                if (coverage != "0") sql = sql + " and t1.coverage =" + coverage;
                if (dpto != "0") sql = sql + " and t1.department =" + dpto;
                if (orgs != "0") sql = sql + " and t1.organization =" + orgs;
                if (stado != "0") sql = sql + " and t1.DocStatus =" + stado;

                if (titulo != "") sql = sql + " and DocTitle like '%" + titulo + "%' ";
                if (codigo != "") sql = sql + " and DocNumber like '%" + codigo + "%' ";

                SqlConnection conn = new SqlConnection(Variables.StrConn);

                try
                {
                    conn.Open();
                    SqlCommand comando = new SqlCommand(sql, conn);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = comando;
                    DataTable dtx = new DataTable();
                    dataAdapter.Fill(dtx);
                    int s = dtx.Rows.Count;
                    listBox7.Items.Clear();

                    foreach (DataRow dr in dtx.Rows)
                    {
                        listBox7.Items.Add(dr[0].ToString() + ' ' + dr[1].ToString());
                    }
                }
                catch (Exception ex)
                {
                    string e1 = ex.ToString();
                    MessageBox.Show(e1);
                    Variables SEND = new Variables();
                    SEND.Sending(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox5.Items.Clear();
            string tt = textBox1.Text;
            int largo = tt.Length;

            foreach (listado i in Variables.lstContacts)
            {
                string nombre = i.Text;
                if (largo > 0)
                {
                    string parte = nombre.Substring(0, largo);
                    if (parte == tt) listBox5.Items.Add(nombre);
                }
                else
                {
                    listBox5.Items.Add(nombre);
                }
            }
        }

        private void txRef3_TextChanged(object sender, EventArgs e)
        {
            txTitle3.Text = "";
            Load_Ref(txRef3.Text.Trim());
        }

        private void txTitle3_TextChanged(object sender, EventArgs e)
        {
            txRef3.Text = "";
            Load_Title(txTitle3.Text.Trim());
        }

    }
}
