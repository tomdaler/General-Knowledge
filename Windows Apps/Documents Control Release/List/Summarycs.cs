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
    public partial class Summarycs : Form
    {
        public Summarycs()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string fec1 = DT1.Value.ToString("yyyy-MM-dd");
            string fec2 = DT2.Value.ToString("yyyy-MM-dd");

            string sql = "SELECT  t2.EfficacyOwner, COUNT(*) as Starting ";
            sql = sql + " FROM tuDocDesc t1, tsEfficacyOwner t2 ";
            sql = sql + " where t1.EfficacyOwner = t2.ID ";

            sql = sql + " and dtAssigned <= '" + fec1 +"'";
            sql = sql + " and ( dtRetired is null or dtRetired > '" + fec1 + "' )";

            sql = sql + " group by t2.EfficacyOwner  order by t2.EfficacyOwner";
            DataTable dt1 = Variables.GetDT(sql);


            sql = "SELECT t2.EfficacyOwner, COUNT(*) as Starting ";
            sql = sql + " FROM tuDocDesc t1, tsEfficacyOwner t2 ";
            sql = sql + " where t1.EfficacyOwner = t2.ID ";

            sql = sql + " and dtAssigned <= '" + fec2 +"' "; 
            sql = sql + " and ( dtRetired is null or dtRetired > '" + fec2 + "' )";

            sql = sql + " group by t2.EfficacyOwner  order by t2.EfficacyOwner";

            DataTable dt2 = Variables.GetDT(sql);

            dt1.Columns.Add("Current", typeof(System.Int32));
            dt1.Columns.Add("Perc", typeof(System.String));

            foreach (DataRow row in dt1.Rows)
            {
                string owner = row[0].ToString();
                int val1 = System.Convert.ToInt32(row[1].ToString());
                foreach (DataRow row2 in dt2.Rows)
                {
                    if (owner == row2[0].ToString())
                    {
                        int val2 = System.Convert.ToInt32(row2[1].ToString());
                        float perc = (val1 - val2) * 100 / val1;

                        row["Current"] = System.Convert.ToInt32(row2[1].ToString());
                        row["Perc"] = perc.ToString() + " %";                        
                    }
                }                                        
            }

            dGrid1.DataSource = dt1;
            dGrid1.Visible = true;
        }
    }
}
