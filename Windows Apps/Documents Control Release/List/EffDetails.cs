using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPS.List
{
    public partial class EffDetails : Form
    {
        public EffDetails()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EffDetails_Load(object sender, EventArgs e)
        {
            string texto = "-- All --";
            filtros1.LoadData(texto);

            ddlOwner.DataSource = Variables.ToDatatable(Variables.lstEffOwner, texto);
            ddlOwner.DisplayMember = "Text";
            ddlOwner.ValueMember = "Value";


            List<listado> nuevo2 = new List<listado>();
            nuevo2 = Variables.lstStatus;
            if (Variables.Report == "NextReview") nuevo2.RemoveAt(2);

            ddlStatus.DataSource = Variables.ToDatatable(nuevo2, texto);
            ddlStatus.DisplayMember = "Text";
            ddlStatus.ValueMember = "Value";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dGrid1.Visible = false;


            string sql = "SELECT dd.ID, dd.DocNumber,dd.DocTitle, dd.DocDescr, ee.version, ss.EfficacyStatus,ee.ExpectedReviewDate,";
	        sql = sql +" ee.RevisionRequired,ee.RevCompleteDate,ee.Comments,";
	        sql = sql + " ee.CreatedBy,ee.CreatedOn, t7.EfficacyOwner, dd.dtAssigned Assigned";
            sql = sql + " FROM tuEfficacyDetails ee, tsEfficacyStatus ss, tuDocDesc dd,tsDocumentType t2, tsCoverage t3, tsDepartments t4 , tsEfficacyOwner t7";
            sql = sql + " where ss.id = ee.Effstatus ";
            sql = sql + " and dd.id = ee.DocNumber ";

            //string sql = "SELECT t1.ID, t1.DocNumber Number, DocTitle Title, CreateDate Created, ";
            //sql = sql + " t2.DocType Type, t3.CoverageDescr Coverage, t4.Code Depto, t6.DocStatus Status, ";
            //sql = sql + " t7.EfficacyOwner, t1.dtAssigned Assigned ";

            //sql = sql + " FROM tuDocDesc t1, tsDocumentType t2, tsCoverage t3, tsDepartments t4, tsDocStatus t6, tsEfficacyOwner t7 ";
            //sql = sql + " where t1.DocType = t2.ID ";

            sql = sql + " and dd.Coverage = t3.ID ";
            sql = sql + " and dd.Department = t4.id ";
            sql = sql + " and dd.EfficacyOwner = t7.ID ";

            string orgs = filtros1.orgs;
            string types = filtros1.types;
            string dpto = filtros1.dpto;
            string coverage = filtros1.coverage;

            string status = ddlOwner.SelectedValue.ToString();

            if (types != "0") sql = sql + " and t2.id =" + types;
            if (coverage != "0") sql = sql + " and t3.id =" + coverage;
            if (dpto != "0") sql = sql + " and t4.id =" + dpto;
            if (orgs != "0") sql = sql + " and dd.organization =" + orgs;
            if (status != "0") sql = sql + " and t7.id =" + status;


            if (panel1.Visible)
            {
                string fec1 = DT1.Value.ToString("yyyy-MM-dd");
                string fec2 = DT2.Value.ToString("yyyy-MM-dd");
                sql = sql + " AND dtAssigned >=' " + fec1 + "' ";
                sql = sql + " AND dtAssigned <=' " + fec2 + "' ";
            }

            if (ddlStatus.Visible) status = ddlStatus.SelectedValue.ToString();
            if (status != "0") sql = sql + " and dd.DocStatus =" + status;


            dGrid1.DataSource = Variables.GetDT(sql);
            //***************************************

            dGrid1.Visible = true;

            dGrid1.Columns[0].Width = 40;
            dGrid1.Columns[1].Width = 140;
            dGrid1.Columns[2].Width = 250;

            dGrid1.Columns[3].Width = 80;
            dGrid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dGrid1.Columns[4].Width = 90;

            dGrid1.Columns[5].Width = 50;
            dGrid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dGrid1.Columns[6].Width = 50;
            dGrid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dGrid1.Columns[8].Width = 150;
            lbMSG.Visible = true;

        }
    }
}
