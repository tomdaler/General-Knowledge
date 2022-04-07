<%@ Page AutoEventWireup="true" CodeFile="Master.aspx.cs" Inherits="Master" Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Untitled Page</title>

	<script src="GridViewHelper.js" type="text/javascript"></script>

	<link href="GridViewHelper.css" rel="STYLESHEET" type="text/css" />
	<link href="AppStyle.css" rel="STYLESHEET" type="text/css" />

	<script src="GridViewHelper.js" type="text/javascript"></script>

	<link href="GridViewHelper.css" rel="STYLESHEET" type="text/css" />
	<link href="AppStyle.css" rel="STYLESHEET" type="text/css" />

	<script src="GridViewHelper.js" type="text/javascript"></script>

	<link href="GridViewHelper.css" rel="STYLESHEET" type="text/css" />
	<link href="AppStyle.css" rel="STYLESHEET" type="text/css" />

	<script src="GridViewHelper.js" type="text/javascript"></script>

	<link href="GridViewHelper.css" rel="STYLESHEET" type="text/css" />
	<link href="AppStyle.css" rel="STYLESHEET" type="text/css" />

	<script src="GridViewHelper.js" type="text/javascript"></script>

	<link href="GridViewHelper.css" rel="STYLESHEET" type="text/css" />
	<link href="AppStyle.css" rel="STYLESHEET" type="text/css" />
</head>
<body>
	<form id="form1" runat="server">
		<div>
			&nbsp;
			<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowCreated="GridView1_RowCreated" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" DataKeyNames="CategoryID">
				<Columns>
					<asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/Cancel.gif" DeleteImageUrl="~/Images/Delete.gif" EditImageUrl="~/Images/Edit.gif" SelectImageUrl="~/Images/Plus.gif" ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" UpdateImageUrl="~/Images/Update.gif">
						<HeaderStyle BackColor="#E0E0E0" HorizontalAlign="Left" VerticalAlign="Top" />
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
					</asp:CommandField>
					<asp:TemplateField HeaderText="CategoryName" SortExpression="CategoryName">
						<EditItemTemplate>
							<asp:TextBox ID="EditCategoryName" runat="server" Columns="20" MaxLength="15" Text='<%# Bind("CategoryName") %>'></asp:TextBox>
							<asp:RequiredFieldValidator ID="CategoryNameRequiredFieldValidator" runat="server" ControlToValidate="EditCategoryName" Display="Dynamic" ErrorMessage="Can not be blank" SetFocusOnError="True"></asp:RequiredFieldValidator>
						</EditItemTemplate>
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
						<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
						<ItemTemplate>
							<asp:Label ID="CategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Description" SortExpression="Description">
						<EditItemTemplate>
							<asp:TextBox ID="EditDescription" runat="server" Columns="20" MaxLength="50" Text='<%# Bind("Description") %>'></asp:TextBox>
							<asp:RequiredFieldValidator ID="DescriptionRequiredFieldValidator" runat="server" ControlToValidate="EditDescription" Display="Dynamic" ErrorMessage="Can not be blank" SetFocusOnError="True"></asp:RequiredFieldValidator>
						</EditItemTemplate>
						<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
						<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
						<ItemTemplate>
							<asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
				<HeaderStyle BackColor="#E0E0E0" />
			</asp:GridView>
			<br />
			<br />
			<br />
			<br />
			<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>" DeleteCommand="DELETE FROM [Categories] WHERE [CategoryID] = @CategoryID" InsertCommand="INSERT INTO [Categories] ([CategoryName], [Description]) VALUES (@CategoryName, @Description)" SelectCommand="SELECT '0' as [CategoryID], '' as [CategoryName], '' as [Description] UNION SELECT [CategoryID], [CategoryName], convert(varchar(100),[Description])  as [Description] FROM [Categories] ORDER BY [CategoryName]" UpdateCommand="UPDATE [Categories] SET [CategoryName] = @CategoryName, [Description] = @Description WHERE [CategoryID] = @CategoryID">
				<DeleteParameters>
					<asp:Parameter Name="CategoryID" Type="Int32" />
				</DeleteParameters>
				<UpdateParameters>
					<asp:Parameter Name="CategoryName" Type="String" />
					<asp:Parameter Name="Description" Type="String" />
					<asp:Parameter Name="CategoryID" Type="Int32" />
				</UpdateParameters>
				<InsertParameters>
					<asp:Parameter Name="CategoryName" Type="String" />
					<asp:Parameter Name="Description" Type="String" />
				</InsertParameters>
			</asp:SqlDataSource>
		</div>
	</form>

	<script type="text/javascript">
		GridViewHelper.Init(document.all.GridView1, 100, 0);
	</script>

</body>
</html>
