<%@ Page AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Details" Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Untitled Page</title>
	<script src="GridViewHelper.js" type="text/javascript"></script>
	<link href="GridViewHelper.css" rel="STYLESHEET" type="text/css" />
	<link href="AppStyle.css" rel="STYLESHEET" type="text/css" />
</head>
<body leftmargin="40" scroll="no" topmargin="0">
	<form id="form1" runat="server" style="overflow: hidden; ">
		<div style="overflow: hidden;">
			<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID" DataSourceID="SqlDataSource1" OnRowCreated="GridView1_RowCreated" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowSorting="True">
				<Columns>
					<asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/Cancel.gif" DeleteImageUrl="~/Images/Delete.gif" EditImageUrl="~/Images/Edit.gif" ShowDeleteButton="True" ShowEditButton="True" UpdateImageUrl="~/Images/Update.gif">
						<HeaderStyle BackColor="#E0E0E0" HorizontalAlign="Left" VerticalAlign="Top" />
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
					</asp:CommandField>
					<asp:TemplateField HeaderText="Name" SortExpression="ProductName">
						<EditItemTemplate>
							<asp:TextBox ID="EditProductName" runat="server" Columns="20" MaxLength="40" Text='<%# Bind("ProductName") %>'></asp:TextBox>
							<asp:RequiredFieldValidator ID="ProductNameRequiredFieldValidator" runat="server" ControlToValidate="EditProductName" Display="Dynamic" ErrorMessage="Can not be blank" SetFocusOnError="True"></asp:RequiredFieldValidator>
						</EditItemTemplate>
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
						<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
						<ItemTemplate>
							<asp:Label ID="ProductName" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Supplier" SortExpression="CompanyName">
						<EditItemTemplate>
							<asp:DropDownList ID="EditSupplierID" runat="server" DataSourceID="SupplierDS" DataTextField="CompanyName" DataValueField="SupplierID" SelectedValue='<%# Bind("SupplierID") %>'>
							</asp:DropDownList>
						</EditItemTemplate>
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
						<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
						<ItemTemplate>
							<asp:Label ID="SupplierName" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Qty/Unit" SortExpression="QuantityPerUnit">
						<EditItemTemplate>
							<asp:TextBox ID="EditQuantityPerUnit" runat="server" Columns="20" MaxLength="20" Text='<%# Bind("QuantityPerUnit") %>'></asp:TextBox>
							<asp:RequiredFieldValidator ID="QuantityPerUnitRequiredFieldValidator" runat="server" ControlToValidate="EditQuantityPerUnit" Display="Dynamic" ErrorMessage="Can not be blank" SetFocusOnError="True"></asp:RequiredFieldValidator>
						</EditItemTemplate>
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
						<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
						<ItemTemplate>
							<asp:Label ID="QuantityPerUnit" runat="server" Text='<%# Bind("QuantityPerUnit") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Price" SortExpression="UnitPrice">
						<EditItemTemplate>
							<asp:TextBox ID="EditUnitPrice" runat="server" Columns="10" Text='<%# Bind("UnitPrice") %>'></asp:TextBox>
						</EditItemTemplate>
						<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
						<HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
						<ItemTemplate>
							<asp:Label ID="UnitPrice" runat="server" Text='<%# Bind("UnitPrice") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="On hand" SortExpression="UnitsInStock">
						<EditItemTemplate>
							<asp:TextBox ID="EditUnitsInStock" runat="server" Text='<%# Bind("UnitsInStock") %>'></asp:TextBox>
						</EditItemTemplate>
						<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
						<HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
						<ItemTemplate>
							<asp:Label ID="UnitsInStock" runat="server" Text='<%# Bind("UnitsInStock") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="On order" SortExpression="UnitsOnOrder">
						<EditItemTemplate>
							<asp:TextBox ID="EditUnitsOnOrder" runat="server" Columns="5" Text='<%# Bind("UnitsOnOrder") %>'></asp:TextBox>
							<asp:RangeValidator ID="UnitsOnOrderRangeValidator" runat="server" ControlToValidate="EditUnitsOnOrder" Display="Dynamic" ErrorMessage="Out of range" MaximumValue="10000" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
						</EditItemTemplate>
						<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
						<HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
						<ItemTemplate>
							<asp:Label ID="UnitsOnOrder" runat="server" Text='<%# Bind("UnitsOnOrder") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Reord lvl" SortExpression="ReorderLevel">
						<EditItemTemplate>
							<asp:TextBox ID="EditReorderLevel" runat="server" Columns="5" Text='<%# Bind("ReorderLevel") %>'></asp:TextBox>
							<asp:RangeValidator ID="ReorderLevelRangeValidator" runat="server" ControlToValidate="EditReorderLevel" Display="Dynamic" ErrorMessage="Out of range" MaximumValue="10000" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
						</EditItemTemplate>
						<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
						<HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
						<ItemTemplate>
							<asp:Label ID="ReorderLevel" runat="server" Text='<%# Bind("ReorderLevel") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:CheckBoxField DataField="Discontinued" HeaderText="Disc'd" SortExpression="Discontinued" />
				</Columns>
				<HeaderStyle BackColor="#E0E0E0" />
			</asp:GridView>
			<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>" DeleteCommand="DELETE FROM [Products] WHERE [ProductID] = @ProductID" InsertCommand="INSERT INTO [Products] ([ProductName], [SupplierID], [CategoryID], [QuantityPerUnit], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued]) VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, convert(money,@UnitPrice), @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued)" SelectCommand="SELECT '0' as [ProductID], '' as [ProductName], '' as [CompanyName], '0' as SupplierID, '0' as [QuantityPerUnit], '0.00' as [UnitPrice], '0' as [UnitsInStock], '0' as [UnitsOnOrder], '0' as [ReorderLevel], '0' as [Discontinued] UNION SELECT [ProductID], [ProductName], Suppliers.CompanyName, Suppliers.SupplierID, [QuantityPerUnit], convert(varchar(20), [UnitPrice], 0) as [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued] FROM [Products] left join Suppliers on Suppliers.Supplierid=Products.SupplierID WHERE ([CategoryID] = @CategoryID) ORDER BY [ProductName]" UpdateCommand="UPDATE [Products] SET [ProductName] = @ProductName, [SupplierID] = @SupplierID, [QuantityPerUnit] = @QuantityPerUnit, [UnitPrice] = @UnitPrice, [UnitsInStock] = @UnitsInStock, [UnitsOnOrder] = @UnitsOnOrder, [ReorderLevel] = @ReorderLevel, [Discontinued] = @Discontinued WHERE [ProductID] = @ProductID">
				<DeleteParameters>
					<asp:Parameter Name="ProductID" Type="Int32" />
				</DeleteParameters>
				<UpdateParameters>
					<asp:Parameter Name="ProductName" Type="String" />
					<asp:Parameter Name="SupplierID" Type="Int32" />
					<asp:Parameter Name="QuantityPerUnit" Type="String" />
					<asp:Parameter Name="UnitPrice" Type="Decimal" />
					<asp:Parameter Name="UnitsInStock" Type="Int16" />
					<asp:Parameter Name="UnitsOnOrder" Type="Int16" />
					<asp:Parameter Name="ReorderLevel" Type="Int16" />
					<asp:Parameter Name="Discontinued" Type="Boolean" />
					<asp:Parameter Name="ProductID" Type="Int32" />
				</UpdateParameters>
				<SelectParameters>
					<asp:QueryStringParameter Name="CategoryID" QueryStringField="ID" Type="Int32" />
				</SelectParameters>
				<InsertParameters>
					<asp:Parameter Name="ProductName" Type="String" />
					<asp:Parameter Name="SupplierID" Type="Int32" />
					<asp:Parameter Name="CategoryID" Type="Int32" />
					<asp:Parameter Name="QuantityPerUnit" Type="String" />
					<asp:Parameter Name="UnitPrice" Type="Decimal" />
					<asp:Parameter Name="UnitsInStock" Type="Int16" />
					<asp:Parameter Name="UnitsOnOrder" Type="Int16" />
					<asp:Parameter Name="ReorderLevel" Type="Int16" />
					<asp:Parameter Name="Discontinued" Type="Boolean" />
				</InsertParameters>
			</asp:SqlDataSource>
			<asp:SqlDataSource ID="SupplierDS" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>" SelectCommand="SELECT 0 as [SupplierID], '' as [CompanyName]  UNION SELECT [SupplierID], [CompanyName] FROM [Suppliers] ORDER BY [CompanyName]"></asp:SqlDataSource>
		</div>
	</form>

	<script type="text/javascript">
		GridViewHelper.Init(document.all.GridView1, 100, 0);
		var ToolTips = new Array("", "Product name", "Supplier", "Quantity per unit", "Unit price", "Units on hand", "Units on order", "Reorder level", "Discontinued");
		GridViewHelper.AddToolTips(document.all.GridView1, ToolTips);
	</script>

</body>
</html>
