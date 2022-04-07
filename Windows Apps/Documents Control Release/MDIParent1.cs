using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GPS
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

       
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

       
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }


        private void contactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Codes.Contacts Forms1 = new Codes.Contacts();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Codes.Dpt Forms1 = new Codes.Dpt();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void responsibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Codes.Responsible Forms1 = new Codes.Responsible();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void createDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Documents.CreateDoc Forms1 = new Documents.CreateDoc();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void organizationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Codes.Organizations Forms1 = new Codes.Organizations();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void searchDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.DocNumber = "";
            Documents.ddlExpect Forms1 = new Documents.ddlExpect();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

              

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            string usuario2 = Environment.UserName;
            if (usuario2.Length > 14) Variables.Usuario = usuario2.Substring(0, 14);

            SqlConnection conn = new SqlConnection(Variables.StrConn);

            try
            {
                conn.Open();

                for (int i = 1; i < 10; i++)
                     if (!Variables.LoadCodes(conn,i)) Environment.Exit(0);

                conn.Close();
            }
            catch (Exception ex)
            {
                string e1 = ex.ToString();
                Variables SEND = new Variables();
                SEND.Sending("Error when opening datatabse "+e1);

            }
        }

        private void OpenListStatus()
        {
            List.Status Forms1 = new List.Status();
            Forms1.MdiParent = this;
            Forms1.Show();
        }


        private void OpenListCollab()
        {
            List.Collab Forms1 = new List.Collab();
            Forms1.MdiParent = this;
            Forms1.Show();
        }


        private void OpenListReport()
        {
            List.Report Forms1 = new List.Report();
            Forms1.MdiParent = this;
            Forms1.Show();
        }
        private void byStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "Status";
            OpenListStatus();
        }

        private void byEfficacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "Efficacy";
            OpenListStatus();
        }

        private void byContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "Collab";
            OpenListCollab();
        }

        private void byOwnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "Owner";
            OpenListCollab();
        }

        private void byResponsibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "Responsibles";
            OpenListCollab();
        }

        private void byNextReviewDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "Complete";
            OpenListReport();
        }

        private void byNextReviewDateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Variables.Report = "NextReview";
            OpenListReport();
        }

        private void byCreationDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "DateCreation";
            OpenListReport();
        }

        private void efficacyOwnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Codes.Efficacy Forms1 = new Codes.Efficacy();
           Forms1.MdiParent = this;
           Forms1.Show();
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void documentTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
          Codes.DocTypes Forms1 = new Codes.DocTypes();
          Forms1.MdiParent = this;
          Forms1.Show();

        }

        private void coveragesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Codes.Coverages Forms1 = new Codes.Coverages();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void byEfficacyOwnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List.EffOwner Forms1 = new List.EffOwner();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void byRetirementDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "Retired";
            OpenListReport();
        }

        private void version10ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void summaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            List.Summarycs Forms1 = new List.Summarycs();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "IMPORT DATA";

            Documents.Import Forms1 = new Documents.Import();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void updateReviewDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "UPDATE REVIEW DATE";

            Documents.Import Forms1 = new Documents.Import();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void updateContributorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Report = "UPDATE CONTACT";

            if (Variables.lstContact.Count==0)
            {
                DataTable dt = Variables.GetDT("select id, name from tsContacts order by name");
                foreach (DataRow dr in dt.Rows)
                {
                    listado nuevo = new listado();
                    nuevo.Code = dr[0].ToString();
                    nuevo.Text = dr[1].ToString();
                    Variables.lstContact.Add(nuevo);
                }
            }

            Documents.Import Forms1 = new Documents.Import();
            Forms1.MdiParent = this;
            Forms1.Show();
        }

        private void efficacyDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List.EffDetails Forms1 = new List.EffDetails();
            Forms1.MdiParent = this;
            Forms1.Show();
        }
    }
}
