using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
	protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		string KeyValue = GridView1.DataKeys[e.RowIndex].Value.ToString();
		if (KeyValue != "0")
			return; // key value of 0 indicates the insert row

		System.Web.UI.WebControls.SqlDataSource ds = (System.Web.UI.WebControls.SqlDataSource)this.FindControl(this.GridView1.DataSourceID);
		System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ds.ConnectionString);
		conn.Open();
		string s = ds.InsertCommand;
		System.Data.SqlClient.SqlCommand c = new System.Data.SqlClient.SqlCommand(s, conn);

		System.Data.SqlClient.SqlParameter p;
		foreach (System.Collections.DictionaryEntry x in e.NewValues)
		{
			p = new System.Data.SqlClient.SqlParameter("@" + x.Key, x.Value);
			c.Parameters.Add(p);
		}
		c.ExecuteNonQuery();
	}

	void AddGlyph(GridView grid, GridViewRow item)
	{
		if (grid.AllowSorting == false)
			return;
		Label glyph = new Label();
		glyph.EnableTheming = false;
		glyph.Font.Name = "webdings";
		glyph.Font.Size = FontUnit.XSmall;
		glyph.Text = (grid.SortDirection == SortDirection.Ascending ? "5" : " 6");

		// Find the column you sorted by
		for (int i = 0; i < grid.Columns.Count; i++)
		{
			string colExpr = grid.Columns[i].SortExpression;
			if (colExpr != "" && colExpr == grid.SortExpression)
			{
				item.Cells[i].Controls.Add(glyph);
			}
		}
	}

	protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
			AddGlyph(GridView1, e.Row);
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string KeyValue = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
			if (KeyValue == "0" && EditIndex == -1)		// we are not editing
				e.Row.Attributes.Add("isadd", "1");
			string RowID = Convert.ToString(System.Web.UI.DataBinder.Eval(e.Row.DataItem, "CategoryID"));
			string Url = "Details.aspx?ID=" + RowID;		// create the Url to be executed when the "+" is clicked (YOU WILL CUSTOMIZE THIS TO YOUR NEEDS)
			e.Row.Attributes.Add("href", Url);		// link to details
			e.Row.Attributes.Add("open", "0");			// used by the detail table expander/contracter
			e.Row.Attributes.Add("hascontent", "0");	// used to prevent excessive callbacks to the server
		}
	}

	public int EditIndex = -1;
	protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
	{
		EditIndex = e.NewEditIndex;
	}

}
