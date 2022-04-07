<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainCT.aspx.cs" Inherits="WebApplication1.MainCT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>COVID19 Case Report Contact Tracing List</title>

    <script src="scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <link  href="scripts/jquery-ui-1.8.17.custom.css" rel="stylesheet" type="text/css" />
    <script src="scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
  
    <script type="text/javascript">
        $(document).ready(function () {

            var d = new Date();
            var utc = d.getTime() + (d.getTimezoneOffset() * 60000);
            var today = new Date(utc + (3600000*9));
          
            var dd1 = today.getDate();
            var mm1 = today.getMonth() + 1;
            var yyyy1 = today.getFullYear()

            $(".datepicker").datepicker({
                showOn: "button",
                dateFormat: 'mm/dd/yy',
                buttonImage: "scripts/calendar.gif",
                maxDate: new Date(yyyy1, mm1, dd1),
                buttonImageOnly: true
            });

            var dd2 = dd1; if (dd2 < 9) dd2 = "0" + dd1;
            var mm2 = mm1; if (mm2 < 9) mm2 = "0" + mm1;
            var today1 = mm2 + '/' + dd2 + '/' + yyyy1;

            $("#From").val(today1);
            $("#To").val(today1);
            document.getElementById('HF1').value = today1;
            document.getElementById('HF2').value = today1;

        });


        $(function () {
            $("#From").datepicker();
            $("#To").datepicker();
        });
 
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }

        document.onkeydown = function (e) {
            // disables F5
            if (e.keyCode === 116) {
                return false;
            }
        };

        function GetText() {
            var e = document.getElementById("ddl1");
            var texto = e.options[e.selectedIndex].value;
            document.getElementById('Hidden1').value = texto;
        }

        function SetFirst() {
            var e = document.getElementById("ddl1");
            e.selectedIndex = 0;
        }

        function SetHidden1() {
             document.getElementById('HF1').value = document.getElementById("From").value;
        }

         function SetHidden2() {
            document.getElementById('HF2').value = document.getElementById("To").value;
        }
    </script>
    
    

    <style>
        h1 {
            background-color: #4db6ac;
        }

        table {
            margin: auto;
        }

        .auto-style1 {
            width: 101%;
        }

        date1 {  width: 50px;}

        .auto-style2 {
            margin-top: 0px;
        }

    </style>

</head>
<body style="width: 2100px">
    <form id="form1" runat="server">

        <div>
          <h1>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 33%">&nbsp;&nbsp;<label><span style="color: white; font-size: 18px;">COVID19 Case Report Contact Tracing List</span></label>
                        </td>

                        <td style="width: 33%" align="center">
                            <a href="#" class="brand-logo center">
                                <img class="material-icons" src="cnx-logo.jpg" /></a>
                        </td>

                        <td style="width: 32%" align="right">

                            <asp:Label ID="Label3" CssClass="left" Font-Size="Medium" runat="server" ForeColor="White">--</asp:Label>
                        </td>
                        <td style="width: 1%" align="right"></td>

                    </tr>
                    <asp:HiddenField ID="Hidden1" runat="server" />
                    <asp:HiddenField ID="HF1" runat="server" />
                    <asp:HiddenField ID="HF2" runat="server" />
                </table>
            </h1>
        </div>

        <div style="margin-left: auto; margin-right: auto; text-align: center;">

            <table style="align-content: center" class="auto-style1">
                <tr>

                    <td style="vertical-align: top;"><b>Search Emp.ID / Name :</b>                       
                    </td>

                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Height="17px" ToolTip="Type Name"
                            BackColor="#CCCCCC"></asp:TextBox>

                    </td>

                    <td style="vertical-align: top;">&nbsp;&nbsp;<b>Status:</b>
                    </td>

                    <td style="vertical-align: top;">
                        <asp:DropDownList ID="ddl1" runat="server" onchange="GetText()">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="All">All</asp:ListItem>
                            <asp:ListItem Value="New">New</asp:ListItem>
                            <asp:ListItem Value="Updated">Updated</asp:ListItem>
                            <asp:ListItem Value="For CT">For CT</asp:ListItem>
                            <asp:ListItem Value="On-going CT">On-going CT</asp:ListItem>
                            
                            <asp:ListItem Value="Notified OSH/LGU">Notified OSH/LGU</asp:ListItem>
                            <asp:ListItem Value="Notified A1 Assessment">Notified A1 Assessment</asp:ListItem>
                            <asp:ListItem Value="Notified A1 SC Assessment">Notified A1 SC Assessment</asp:ListItem>

                            <asp:ListItem Value="Notified 7th Day of SQ">Notified 7th Day of SQ</asp:ListItem>
                            <asp:ListItem Value="Notified 10th Day of SQ">Notified 10th Day of SQ</asp:ListItem>

                            <asp:ListItem Value="Notified 1st Swab">Notified 1st Swab</asp:ListItem>
                            <asp:ListItem Value="Notified 2nd Swab">Notified 2nd Swab</asp:ListItem>

                            <asp:ListItem Value="Notified UTC-DM">Notified UTC-DM</asp:ListItem>
                            <asp:ListItem Value="Notified UTC-FTW">Notified UTC-FTW</asp:ListItem>

                            <asp:ListItem Value="Notified FTW">Notified FTW</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                    <td style="vertical-align: top;">&nbsp;
                        <asp:Button ID="Button4" runat="server" OnClick="BtSearch_Click" Text="Search" Width="85px" />
                    </td>

                    <td style="vertical-align: top;width:1000px">&nbsp;&nbsp;
                        From:
                        <input type="text" id="From" style="width:75px" class="datepicker" onchange="SetHidden1();" />
                        To:
                        <input type="text" id="To" style="width:75px" class="datepicker" onchange="SetHidden2();" />
                       <asp:Button ID="BtLast" runat="server" Text="Download Report" OnClick="BtNewRep_Click" />

                        <asp:Button ID="Button6" runat="server" Text="New Cases" OnClick="Button2_Click" Visible="False" />
                   
                    </td>
                    
                </tr>
            </table>
        </div>


        <br />

        <div>
            <br />
            <asp:GridView ID="dg" runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="ID"
                AllowSorting="True"
                PageSize="100"
                OnRowCommand="dg_RowCommand"
                OnPageIndexChanging="dg_PageIndexChanging"
                AllowPaging="True" OnSorting="dg_Sorting" 
                OnRowDataBound="dg_RowDataBound" >

                <HeaderStyle BackColor="#CCFFFF" />

                <PagerStyle HorizontalAlign="Right" />

                <AlternatingRowStyle BackColor="#dcecf4" />

                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                    
                    <asp:BoundField DataField="CT_DateCreated" HeaderText="&nbsp;Date&nbsp;Created&nbsp;" SortExpression="CT_DateCreated"  />
                  
                    <asp:BoundField DataField="CT_Email" HeaderText="Email" SortExpression="CT_Email"/> 
                    
                    <asp:BoundField DataField="CT_WD_ID" HeaderText="Index Case ID" SortExpression="CT_WD_ID" />
                    
                    <asp:BoundField DataField="CT_EmpNameCNX" HeaderText="&nbsp;Index&nbsp;Case&nbsp;Name&nbsp;" SortExpression="CT_EmpNameCNX"/>
                    

                    <asp:ButtonField ButtonType="Link" HeaderText="Employee ID" DataTextField="CT_EmpID" Text='Text' CommandName="Detail" SortExpression="CT_EmpID" />

                    <asp:BoundField DataField="CT_FirstName" HeaderText="First Name" SortExpression="CT_FirstName"></asp:BoundField>
                    <asp:BoundField DataField="CT_MiddleName" HeaderText="Middle Name" SortExpression="CT_MiddleName"></asp:BoundField>
                    <asp:BoundField DataField="CT_LastName" HeaderText="Last Name" SortExpression="CT_LastName"></asp:BoundField>
                   
                    <asp:BoundField DataField="CT_EmpLocation" HeaderText="Site" SortExpression="CT_EmpLocation">
                        <ItemStyle Width="2000px"></ItemStyle>
                    </asp:BoundField>
                    
                    
                    <asp:BoundField DataField="CT_EmpProgram" HeaderText="Program" SortExpression="CT_EmpProgram" />
                    <asp:BoundField DataField="CT_RepType" HeaderText="Report Type" SortExpression="CT_RepType" />

                    <asp:BoundField DataField="VS" HeaderText="Vaccination Status"  />
                    
                    <asp:BoundField DataField="CT_QuarantineStartDate" HeaderText="Quarantine Start Date" SortExpression="CT_QuarantineStartDate" />
                    <asp:BoundField DataField="CT_QuarantineEndDate" HeaderText="Quarantine End Date" SortExpression="CT_QuarantineEndDate" />
                    <asp:BoundField DataField="CT_dtExtEnd" HeaderText="Quarantine New End Date" SortExpression="CT_dtExtEnd" />
                                       
                    <asp:BoundField DataField="CT_DOH" HeaderText="DOH Category" SortExpression="CT_DOH" />
                    <asp:BoundField DataField="CT_Recommendation" HeaderText="Recommendation" SortExpression="CT_RepType" />
                    <asp:BoundField DataField="CT_dtFTW" HeaderText="Date of FTW" SortExpression="CT_dtFTW" />

                    <asp:BoundField DataField="CT_Status" HeaderText="Status" SortExpression="CT_Status" />
                    <asp:BoundField DataField="CT_DateModified" HeaderText="&nbsp;Date&nbsp;Modified&nbsp;" SortExpression="CT_DateModified" />

                    <asp:ButtonField ButtonType="Image" CommandName="ViewDoc" Text="View" ImageUrl="~/Look.ico" />

                </Columns>
            </asp:GridView>

            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="NoData" runat="server" Font-Bold="True" Text="Theres is no data for this condition" Font-Size="X-Large"></asp:Label>
            </div>

            <br />
            <br />
        </div>
    </form>
</body>
</html>
