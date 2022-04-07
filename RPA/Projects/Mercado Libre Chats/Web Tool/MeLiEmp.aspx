<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeLiEmp.aspx.cs" Inherits="MercadoLibre.MeLiEmp" %>

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
        .auto-style1 {
            width: 10%;
            height: 32px;
        }
        .auto-style2 {
            width: 30%;
            height: 32px;
        }
        .auto-style3 {
            width: 20%;
            height: 32px;
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

                    <asp:HiddenField ID="hdStatus" runat="server" />
                    <asp:HiddenField ID="hdStatus2" runat="server" />

                    <asp:HiddenField ID="hdLOB" runat="server" />
                    <asp:HiddenField ID="hdTL" runat="server" />

                    <asp:HiddenField ID="hdEmp" runat="server" />
                    <asp:HiddenField ID="hdLevel" runat="server" />

                    <asp:HiddenField ID="hdTL2" runat="server" />

                </table>
            

        <br /><br />

            <table style="width:100%">
                
                <tr>
                  
                    <td style="width:10%"/>
                    <td style="width:30%">
                        <h1>Empregado de atualização</h1></td>
                    <td style="width:10%"/>
                     <td style="width:40%"><h1>Criar / Atualizar TL</h1></td>
                     <td style="width:10%"/>
                </tr>

               <tr><td></td></tr>
               <tr><td></td></tr>

                <tr>
                    <td style="width:10%"/>
                    <td style="width:30%">
                        Empleado:&nbsp;&nbsp;
                         <asp:DropDownList ID="ddlEmp" runat="server" onchange="return SetEmp();" Font-Size="Medium" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged"/>
                    </td>
                    <td style="width:10%"/>
                     <td style="width:30%">
                         TL NOME:&nbsp;
                        <asp:TextBox ID="txTL" runat="server" MaxLength="20"/>
                    &nbsp;
                         <asp:DropDownList ID="ddlTL2" runat="server"  onchange="return SetTL2();"  Font-Size="Medium">
                         </asp:DropDownList>
                    </td>
                    <td style="width:20%"/>
               </tr>
            
        <tr>
                    <td class="auto-style1"/>
                    <td class="auto-style2">
                        TL:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp; <asp:DropDownList ID="ddlTL" runat="server" onchange="SetTL()" Font-Size="Medium"/>
                    </td>
                    <td class="auto-style1"/>
                     <td class="auto-style2">
                         LOB:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                         <asp:DropDownList ID="ddlLOB" runat="server"  onchange="SetLOB()" Font-Size="Medium" />
                    </td>
                    <td class="auto-style3"/>
       </tr>

                <tr>
                    <td style="width:10%"/>
                    <td style="width:30%">
                        Status:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; 
                        <asp:DropDownList ID="ddlStatus" runat="server" onchange="SetStatus()"  Font-Size="Medium"/>
                    </td>
                    <td style="width:10%"/>
                     <td style="width:30%">
                         Nivel:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                         <asp:DropDownList ID="ddlLevel" runat="server"  onchange="return SetLevel();"  Font-Size="Medium"/>
                    </td>
                    <td style="width:20%"/>
              </tr>


         <tr>
                    <td style="width:10%"/>
                    <td style="width:30%">
                        &nbsp;</td>
                    
                    <td style="width:10%"/>
                     <td style="width:30%">
                         S.O.:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                        
                         <asp:TextBox ID="txSO" runat="server" Font-Size="Medium"></asp:TextBox>
                        
                    </td>
                    <td style="width:20%"/>
              </tr>

        <tr>
                    <td style="width:10%"/>
                    <td style="width:30%"/>
                       
                    <td style="width:10%"/>

                     <td style="width:30%">
                        Corto :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                        
                         <asp:TextBox ID="txTL2" runat="server" MaxLength="3" Width="22px" Font-Size="Medium"></asp:TextBox>
                     </td>
                     <td style="width:20%"/>
                </tr>


        <tr><td></td></tr>
        
        <tr>
                    <td style="width:10%"/>
                    <td style="width:30%">
                        <asp:Button ID="btnEmp" runat="server" Text="Atualizar" OnClick="btnEmp_Click" Font-Size="Medium" /> 
                        <asp:Label ID="EmpAct" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="width:10%"/>

                     <td style="width:30%">
                        <asp:Button ID="btnTL" runat="server" Text="Atualizar" OnClick="btnTL_Click" Font-Size="Medium"/>
                         &nbsp;&nbsp;
                         <asp:Label ID="TLAct" runat="server" Text=""></asp:Label>
                     </td>
                     <td style="width:20%"/>
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

             function SetLOB() {
                 var e = document.getElementById("ddlLOB");
                 var valor = e.options[e.selectedIndex].value;
                 document.getElementById('hdLOB').value = valor;
                 return false;
             }

             function SetEmp() {
                 var e = document.getElementById("ddlEmp");
                 var e2 = document.getElementById("EmpAct");
                 e2.innerText = "";

                 var valor = e.options[e.selectedIndex].value;
                 document.getElementById('hdEmp').value = valor;
                 return false;
             }

             function SetTL() {
                 var e = document.getElementById("ddlTL");
                 var valor = e.options[e.selectedIndex].value;
                 document.getElementById('hdTL').value = valor;
                 return false;
             }

             function SetT2L() {
                 var e = document.getElementById("ddlTL2");
                 var valor = e.options[e.selectedIndex].value;
                 document.getElementById('hdTL2').value = valor;
                 return false;
             }

             function SetLevel() {
                 var e = document.getElementById("ddlLevel");
                 var texto = e.options[e.selectedIndex].value;
                 document.getElementById('hdLevel').value = texto;
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
