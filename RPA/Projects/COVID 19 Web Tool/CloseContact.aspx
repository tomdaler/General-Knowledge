<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CloseContact.aspx.cs" Inherits="WebApplication1.CloseContact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
    
      #customers {
        font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
        border-collapse: collapse;
        width: 100%;
      }

      #customers td, #customers th {
         border: 1px solid #ddd;
         padding: 8px; 
      }
 
      #customers tr:nth-child(even){background-color: #dcecf4;}

      #customers tr:hover {background-color: #ddd;}

      #customers th {
          padding-top: 12px;
          padding-bottom: 12px;
          text-align: left;
          background-color: #4CAF50;
          color: white;
        }

        .auto-style1 {
            width: 165px;
        }
        .auto-style2 {
            width: 718px;
           
        }

        .auto-style11 {
            width: 165px;
            background-color: #dcecf4;
        }
        .auto-style22 {
            width: 718px;
            background-color: #dcecf4;
           
        }

    </style>

</head>
<body>
     <form id="form1" runat="server">
        
       <br/>
             <p>
      <asp:Label ID="Titulo" runat="server" Font-Bold="True" Text="Title" Font-Size="X-Large" ></asp:Label>
         </p>
         
 
   
<asp:GridView ID="dg" runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="ID"
                PageSize="100"
                OnRowCommand="dg_RowCommand"
                OnRowDataBound="dg_RowDataBound" CssClass="auto-style2">

                <HeaderStyle BackColor="#CCFFFF" />

                <PagerStyle HorizontalAlign="Right" />

                <AlternatingRowStyle BackColor="#dcecf4" />

                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" />
                    <asp:BoundField DataField="CT_DateCreated" ControlStyle-Width="300px"  HeaderText="Date Reported"  >
                        <ItemStyle Width="500px" Wrap="false" />
                    </asp:BoundField>
             
                     <asp:BoundField DataField="CT_Email" HeaderText="Sender"  />
                                
                    <asp:BoundField DataField="CT_EmpID" HeaderText="Employee ID"  />
                  
                    <asp:BoundField DataField="EmpName"  HeaderText="Employee Name"  >
                    <ItemStyle Width="500px" Wrap="false" />
                    </asp:BoundField>

                    <asp:BoundField DataField="CT_ContactTracing" HeaderText="Contact Tracing" ></asp:BoundField>
                    <asp:BoundField DataField="CT_DOH" HeaderText="DOH Category" SortExpression="CT_DOH">
                  <ItemStyle Width="500px" Wrap="false" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="CT_Recommendation" HeaderText="Recommendation" >
                    <ItemStyle Width="500px" Wrap="false" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="CT_QuarantineStartDate" HeaderText="Quarantine Start Date" />
                    <asp:BoundField DataField="CT_QuarantineEndDate" HeaderText="Quarantine End Date"  />
                    <asp:BoundField DataField="CT_dtFTW" HeaderText="Date FTW"  />
                    
                    <asp:BoundField DataField="CT_dtCTA1Start" HeaderText="Date Start CT A1 Assessment">
                          <ItemStyle Width="500px" Wrap="false" />
                    </asp:BoundField>

                    <asp:BoundField DataField="CT_dtCTA1End"   HeaderText="Date End CT A1 Assessment">  
                      <ItemStyle Width="500px" Wrap="false" />
                    </asp:BoundField>

                    <asp:ButtonField ButtonType="Image" CommandName="ViewDoc" Text="View" ImageUrl="~/Look.ico" />
                </Columns>
            </asp:GridView>
    </form>
     <p>
         &nbsp;</p>
     <p>
         &nbsp;</p>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="NoData" runat="server" Font-Bold="True" Text="Theres is no data for this Employee" Font-Size="X-Large"></asp:Label>
</body>
</html>
