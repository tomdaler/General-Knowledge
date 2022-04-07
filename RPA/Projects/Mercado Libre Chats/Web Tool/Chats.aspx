<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chats.aspx.cs" Inherits="MercadoLibre.Chats" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       
 <div>
   
                <br />
   
                <table style="width: 100%; background-color: #4db6ac;">
                    <tr>
                        <td style="width:5%">
                        &nbsp;
                    </td>

                        <td style="width: 40%"><label><span style="color: white; font-size: 18px;">MERCADO LIBRE</span></label>
                        </td>

                        <td style="width: 40%" >
                            <a href="#" class="brand-logo center">
                                <img class="material-icons" src="Iconos\cnx-logo.jpg" /></a>
                        </td>

                        <td style="width: 30%">
                              <asp:Label ID="Label3" CssClass="left" Font-Size="Medium" runat="server" ForeColor="White">--</asp:Label>
                        </td>
                         
                        <td style="width:5%">
                        &nbsp;
                    </td>

                    </tr>

                </table>
     <br />

       
        <table style="width: 100%">
                    
         
            <tr>
                    <td style="width:30%;height:40px">
                        &nbsp;
                    </td>

                    <td style="width: 40%">
                            &nbsp;TL : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
            <asp:DropDownList ID="ddlTL" runat="server" onchange="SetTL()" Font-Size="Medium" Width="150px">   </asp:DropDownList>
                    </td>
             </tr>

            <tr>
                    <td style="width:30%;height:40px">
                        &nbsp;
                    </td>

                    <td style="width: 40%">
                            &nbsp;Status : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="ddlStatus" runat="server" onchange="SetStatus()" Font-Size="Medium" Width="150px">   </asp:DropDownList>
                    </td>
             </tr>

                        <tr>
                    <td style="width:30%;height:40px">
                        &nbsp;
                    </td>

                    <td style="width: 40%">
                            &nbsp;Duração do chat
 :&nbsp;
            <asp:DropDownList ID="ddlChat" runat="server" onchange="SetMinutos()" Font-Size="Medium" Width="150px">   </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button1" runat="server" Text="Carregar" OnClick="Button1_Click" />
                    &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
                    </td>
             </tr>

            
         </table>

        </div>

        <br/>

        <asp:GridView ID="dg2" runat="server"
             HorizontalAlign="Center" AllowSorting="True" 
             DataKeyNames="LoginName"
                AutoGenerateColumns="False" OnSorting="dg2_Sorting" OnRowCommand="dg2_RowCommand"

            >
            
                <HeaderStyle BackColor="#CCFFFF" />
                <PagerStyle HorizontalAlign="Right" />
                <AlternatingRowStyle BackColor="#dcecf4" />

                <Columns>
                   
                    <asp:BoundField  DataField="Nome" 
                        HeaderText="Nome" SortExpression="Nome" />

                    <asp:BoundField  DataField="TL2" 
                        HeaderText="TL" SortExpression="TL2" />

                  <asp:BoundField  DataField="Status2" 
                        HeaderText="Status" SortExpression="Status2" />

                     <asp:BoundField  DataField="Next"   HeaderText="Next"  />

                     <asp:BoundField  DataField="LoginTime"   HeaderText="Login" DataFormatString="{0:hh\:mm}"   />

                    <asp:BoundField  DataField="LastUpdate"   HeaderText="Last Update" DataFormatString="{0:hh\:mm}"    />
                    
                  <asp:TemplateField HeaderText="Chat1" SortExpression="Chat1">
                      <ItemTemplate>
                       <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="ChatID1"
                            Text='<%# Eval("Chat1") %>'  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' />
                       </ItemTemplate>
                 </asp:TemplateField>

                     <asp:TemplateField HeaderText="Chat2" SortExpression="Chat2">
                      <ItemTemplate>
                       <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="ChatID2"
                            Text='<%# Eval("Chat2") %>' CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'/>
                       </ItemTemplate>
                 </asp:TemplateField>

                     <asp:TemplateField HeaderText="Chat3" SortExpression="Chat3">
                      <ItemTemplate>
                       <asp:Button ID="Button3" runat="server" CausesValidation="false" CommandName="ChatID3"
                            Text='<%# Eval("Chat3") %>' CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'/>
                       </ItemTemplate>
                 </asp:TemplateField>
                                        
                </Columns>
    
        </asp:GridView>
        </form>
</body>
</html>
