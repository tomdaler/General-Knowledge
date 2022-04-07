<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cases.aspx.cs" Inherits="MercadoLibre.Cases" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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

        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
        <pre id="tw-target-text" class="tw-data-text tw-text-large XcVN5d tw-ta" data-placeholder="Traducción" dir="ltr" style="border-style: none; border-color: inherit; border-width: medium; font-family: inherit; unicode-bidi: isolate; font-size: 28px; line-height: 36px; background-color: rgb(248, 249, 250); padding: 2px 0.14em 2px 0px; position: relative; margin: -2px 0px; resize: none; overflow: hidden; text-align: left; width: 301px; white-space: pre-wrap; overflow-wrap: break-word; color: rgb(32, 33, 36); font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; top: 0px; left: 0px;"><span class="Y2IQFc" lang="pt">   Insira o número do caso</span></pre>
        <p>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txCase" runat="server" style="margin-top: 0px" Width="101px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Carregar Informação" OnClick="Button1_Click" />
        </p>
        <br />
        <br />

        <table>
            <tr>
                <td style="width:15%"></td>

                <td style="width:70%">
                      <asp:Label ID="Agent" runat="server" Font-Size="Large" Font-Bold="True"></asp:Label>
                </td>

                <td style="width:15%"></td>

            </tr>
            <tr>
                <td style="width:15%"></td>

                <td style="width:70%">
                      <asp:Label ID="Chat" runat="server" Font-Size="Large" BorderStyle="Outset"></asp:Label>
                </td>

                <td style="width:15%"></td>
            </tr>
        </table>
      

    </form>
</body>
</html>
