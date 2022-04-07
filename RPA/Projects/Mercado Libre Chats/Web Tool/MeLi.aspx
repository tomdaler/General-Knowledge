<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeLi.aspx.cs" Inherits="MercadoLibre.MeLi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MeLi Dashboad</title>
       
    <script src="scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.multiselect.min.js" type="text/javascript"></script>
    
    <link href="css/jquery-ui-1.8.17.custom.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery.multiselect.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    $(document).ready(function () {
        $(".multiselect").multiselect();
    });
    </script>


    <script type="text/javascript">

        
        $(document).ready(function () {

            
            if (window.history.replaceState) {
                window.history.replaceState(null, null, window.location.href);
            }

            document.onkeydown = function (e) {
                // disables F5
                if (e.keyCode === 116) {
                    return false;
                }
            };

        }

    </script>

    <style>
        h1 {
            background-color: #4db6ac;
        }

        table {
            margin: 0 auto;
            text-align: left;
        }
 
        div.ui-datepicker
         {
             font-size: medium;
        }
        #minutos {
            width: 35px;
        }
        #minutos0 {
            width: 70px;
        }
        .auto-style1 {
            width: 21%;
            height: 40px;
        }
        .auto-style2 {
            width: 40%;
            height: 40px;
        }
        .auto-style3 {
            height: 20px;
            width: 21%;
        }
    </style>

</head>

<body onload="Esconder();">
    
    <form id="form1" runat="server">
        
 <div>
   
                <table style="background-color: #4db6ac;" class="ui-accordion">
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

                    <asp:HiddenField ID="hdStatus" runat="server" />
                    <asp:HiddenField ID="hdTL" runat="server" />
                    <asp:HiddenField ID="hdMinutos" runat="server" />
                </table>
        </div>

        <br />
     
        <table style="width: 100%">
                    <tr>
                        <td class="auto-style3">
                        &nbsp;
                    </td>

                        <td style="width: 40%">
                            <asp:ListBox ID="ddlLOB" runat="server"  
             CssClass="multiselect" 
             SelectionMode ="Multiple" 
             TabIndex="3" 
             ForeColor="Black" />
                <br />        
                </td>
            </tr>
         
            <tr>
                    <td class="auto-style1">
                        &nbsp;
                    </td>

                    <td class="auto-style2">
                            &nbsp;TL : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
            <asp:DropDownList ID="ddlTL" runat="server" onchange="SetTL()" Font-Size="Medium" Width="150px">   </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Chat No.
                    &nbsp;
                            <asp:TextBox ID="txChat" runat="server" Width="81px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;<asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Button" />
                    </td>
             </tr>

            <tr>
                    <td class="auto-style1">
                        &nbsp;
                    </td>

                    <td style="width: 40%">
                            &nbsp;Status : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="ddlStatus" runat="server" onchange="SetStatus()" Font-Size="Medium" Width="150px">   </asp:DropDownList>
                    </td>
             </tr>

                        <tr>
                    <td class="auto-style1">
                        &nbsp;
                    </td>

                    <td style="width: 40%">
                            &nbsp;Minima Duração do chat
 :&nbsp;
                    <input type="number" id="minutos" min="0" max="30" 
                        onchange="document.getElementById('hdMinutos').value = 
                        document.getElementById('minutos').value;"/>
                               
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="today" runat="server" Checked="True" Text="Hoy" />
                            &nbsp;&nbsp;
      <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Carregar" style="height: 26px" />
                      
                        </td>
             </tr>
         </table>

        <br />
 
        <script type="text/javascript">
    $(document).ready(function () {
    var date = new Date();
    var currentMonth = date.getMonth();
    var currentDate = date.getDate();
    var currentYear = date.getFullYear(); 

    $(".datepicker").datepicker({
        showOn: "button",
        dateFormat: 'dd/mm/yy',
        buttonImage: "Iconos/calendar.gif",
        beforeShowDay: $.datepicker.noWeekends,
        minDate: new Date(currentYear, currentMonth, currentDate),
        beforeShowDay: $.datepicker.noWeekends,
        buttonImageOnly: true
    });
})

</script>
        
            <asp:GridView ID="GV1" 
                runat="server" AllowSorting="True"
                HorizontalAlign="Center" 
                OnRowDataBound="GV1_RowDataBound" OnSorting="GV1_Sorting"
                OnSelectedIndexChanged="GV1_SelectedIndexChanged" 
                CellPadding="5" CellSpacing="5" >
                <AlternatingRowStyle BackColor="#dcecf4" />
                <HeaderStyle BackColor="#4DB6AC" />
                <RowStyle HorizontalAlign="Center" />
            </asp:GridView>

        
            &nbsp;&nbsp;&nbsp;&nbsp;
        
            <div style="width: 100%">

         <asp:GridView ID="dg2" 
                runat="server"
                DataKeyNames="EmpID"
                AutoGenerateColumns="False"
                AllowSorting="True"
                PageSize="100"
                CssClass="auto-style2" OnSorting="dg2_Sorting" 
             Font-Size="Medium" CellSpacing="2" 
             OnRowCommand="dg2_RowCommand1" 
             OnRowDataBound="dg2_RowDataBound" Width="100%"  >

                <HeaderStyle BackColor="#CCFFFF" />
                <PagerStyle HorizontalAlign="Right" />
                <AlternatingRowStyle BackColor="#dcecf4" />

                <Columns>
                    
                    <asp:ButtonField Text="Açoes" CommandName="Actions" ButtonType="Button" />
                    
                    <asp:BoundField  DataField="Nome" 
                        HeaderText="Nome" SortExpression="Nome" />

                    <asp:BoundField  DataField="TLName" 
                        HeaderText="TL" SortExpression="TL" />

                  <asp:BoundField  DataField="Status" 
                        HeaderText="Status" SortExpression="Status" />

                    <asp:BoundField  DataField="Status2" 
                        HeaderText="Status 2" SortExpression="Status2" />

                  <asp:BoundField  DataField="LoginTime"  
                         HeaderText="Login" DataFormatString="{0:hh\:mm}"   />

                    <asp:BoundField  DataField="LastUpdate"   
                        HeaderText="Last Update" DataFormatString="{0:hh\:mm}"    />
                   
        
                <asp:TemplateField HeaderText="Chat1"   ItemStyle-HorizontalAlign="Center"    >
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server"
                      Text='<%# Eval("Chat1") %>'   
                      ControlStyle-ForeColor="Blue"
                      CommandArgument="<%# Container.DataItemIndex %>"
                      ButtonType     ="link"     
                      CommandName    ="Chat1" />
               </ItemTemplate> 
               </asp:TemplateField>
                    
                <asp:TemplateField HeaderText="Chat2"    ItemStyle-HorizontalAlign="Center"    >
                <ItemTemplate>
                    <asp:Button ID="Button2" runat="server"
                      Text='<%# Eval("Chat2") %>'   
                      CommandArgument="<%# Container.DataItemIndex %>"
                      ControlStyle-ForeColor="Blue"
                      ButtonType     ="link"     
                      CommandName    ="Chat2" />
               </ItemTemplate> 
               </asp:TemplateField>

                <asp:TemplateField HeaderText="Chat3"   ItemStyle-HorizontalAlign="Center"    >
                <ItemTemplate>
                    <asp:Button ID="Button3" runat="server"
                      Text='<%# Eval("Chat3") %>'   
                      ControlStyle-ForeColor="Blue"
                      CommandArgument="<%# Container.DataItemIndex %>"
                      ButtonType     ="link"     
                      CommandName    ="Chat3" />
               </ItemTemplate> 
               </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
       
        </div>
        
    </form>

    </body>



</html>
