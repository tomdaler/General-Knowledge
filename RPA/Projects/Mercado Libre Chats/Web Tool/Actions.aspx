<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Actions.aspx.cs" Inherits="MercadoLibre.Actions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MeLi Dashboad</title>

    <script src="scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <link  href="scripts/jquery-ui-1.8.17.custom.css" rel="stylesheet" type="text/css" />
    <script src="scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
 
    <style>
     
        table {
            margin: 0 auto;
            text-align: left;
        }

        /* Style the tab */
        .tab {
            overflow: hidden;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
        }

            /* Style the buttons that are used to open the tab content */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #ddd;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                    background-color: #ccc;
                }

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            border: 1px solid #ccc;
            border-top: none;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
 

         
                <table style="width: 100%; background-color: #4db6ac;">
                    <tr>
                        <td style="width:5%">
                        &nbsp;
                    </td>

                        <td style="width: 40%"><label><span style="color: white; font-size:18px;">MERCADO LIBRE</span></label>
                        </td>

                        <td style="width: 40%" >
                            <a href="#" class="brand-logo center">
                                <img class="material-icons" src="Iconos\cnx-logo.jpg" /></a>
                        </td>

                        <td style="width: 30%">
                              <asp:Label ID="Label3" CssClass="left" Font-Size="Large" runat="server" ForeColor="White">--</asp:Label>
                        </td>
                         
                        <td style="width:5%">
                        &nbsp;
                    </td>

                    </tr>

                  
                </table>
        <br />
      

                    <div style="align-content:center">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbEmp" runat="server" Text="Empleado : aaaaa     TL : TD"></asp:Label>
                    &nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
&nbsp;&nbsp;&nbsp;&nbsp; Reasign TD:&nbsp;
                        <asp:DropDownList ID="ddlTL" runat="server" OnSelectedIndexChanged="ddlTL_SelectedIndexChanged"> </asp:DropDownList>
                        <br />
                        <br />
&nbsp;&nbsp;&nbsp;&nbsp; Status&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"> </asp:DropDownList>
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="Atualizar" OnClick="Button1_Click" />
 &nbsp;&nbsp;


                        <asp:Label ID="updated" runat="server" Text="."></asp:Label>
 </div>

                <p>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;</p>
        
        <div style="align-content:center">
      
      
        <asp:GridView ID="GV1" 
                runat="server"
                DataKeyNames="CaseNo"
                AutoGenerateColumns="False"
                CssClass="auto-style2" 
                Font-Size="Medium" CellSpacing="7" 
                OnRowDataBound="GV1_RowDataBound" CellPadding="7" OnRowCommand="GV1_RowCommand" >

                <HeaderStyle BackColor="#CCFFFF" />
                <PagerStyle HorizontalAlign="Right" />
                <AlternatingRowStyle BackColor="#dcecf4" />

                <Columns>
                    
                    <asp:ButtonField ButtonType="Link" HeaderText="Case Number"
                          DataTextField="CaseNo" Text='Text' CommandName="CaseNo" />
                    <asp:BoundField  DataField="Created"  HeaderText="Tempo" />
                    <asp:BoundField  DataField="Status"    HeaderText="Status" />
                   
                  
                </Columns>
           </asp:GridView>

        </div>

        
        <br />

        <table>

            <tr>
                <td style="width:15%"></td>

                <td style="width:70%">
                      <asp:Label ID="Chat" runat="server" Font-Size="Large" BorderStyle="Outset"></asp:Label>
                </td>

                <td style="width:15%"></td>
            </tr>
        </table>

             <script>
         //$(document).ready(function () {

             function SetStatus() {
                 var e = document.getElementById("ddlStatus");
                 var valor = e.options[e.selectedIndex].value;
                 document.getElementById('hdStatus').value = valor;
                 return false;
             }

         function SetStatus() {
                 var e = document.getElementById("ddlStatus2");
                 var valor = e.options[e.selectedIndex].value;
                 document.getElementById('hdStatus2').value = valor;
                 return false;
             }

             if (window.history.replaceState) {
                 window.history.replaceState(null, null, window.location.href);
             }

             document.onkeydown = function (e) {
                 // disables F5
                 if (e.keyCode === 116) {
                     return false;
                 }
             };

         //}
            

       </script>



      </form>
</body>
</html>