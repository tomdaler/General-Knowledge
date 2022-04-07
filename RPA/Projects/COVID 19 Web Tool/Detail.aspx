<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WebApplication1.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>




    <style>

        .’btn-link’ {
  background: none!important;
  border: none;
  padding: 0!important;
  /*optional*/
  font-family: arial, sans-serif;
  /*input has OS specific font-family*/
  color: #069;
  text-decoration: underline;
  cursor: pointer;
}



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
    <p>
 <p>

     &nbsp;&nbsp;&nbsp;
     <asp:Label ID="EmpID" runat="server" Text="Label Emp" Font-Underline="True" ForeColor="#006666" Font-Size="X-Large"></asp:Label>
     <asp:Label ID="employee" runat="server" Text="Label Emp" Font-Underline="True" ForeColor="#006666" Font-Size="X-Large"></asp:Label>
     <br />
     <br />

      <tr>


        <table id="customers">
            <tr>
                <td class="auto-style1"><b>Supervisor :</b>&nbsp;&nbsp;</td>
                <td class="auto-style2"><asp:Label ID="Supervisor" runat="server" Text="Label"></asp:Label></td>
            </tr>

            <tr>
                <td class="auto-style11"><b>Next Level Manager :</b></td>
                <td class="auto-style22"><asp:Label ID="NextLevel" runat="server" Text="Label"></asp:Label></td>
            </tr>

            <tr>
                <td class="auto-style1"><b>Mobile Number :</b>&nbsp;&nbsp;</td>
                <td class="auto-style2"><asp:Label ID="L3" runat="server" Text="Label"></asp:Label></td>
            </tr>

            <tr>
                <td class="auto-style11"><b>Alternative Number :</b></td>
                <td class="auto-style22"><asp:Label ID="L4" runat="server" Text="Label"></asp:Label></td>
            </tr>
  
             <tr>
               <td class="auto-style1"><b>Best Time to Call :</b></td>
               <td class="auto-style2"><asp:Label ID="BestTime" runat="server" Text="Label"></asp:Label></td>
           </tr>


            <tr>
                <td class="auto-style11"><b>Complete Address :</b></td>
                <td class="auto-style22"><asp:Label ID="Address" runat="server" Text="Label"></asp:Label></td>
            </tr>
    
            <tr>
                <td class="auto-style1"><b>Barangay Residence :</b></td>
                <td class="auto-style2"><asp:Label ID="Residence" runat="server" Text="Label"></asp:Label></td>
            </tr>

            <tr>
                <td class="auto-style11"><b>Date of Contact :</b></td>
                <td class="auto-style22"><asp:Label ID="L6" runat="server" Text="Label"></asp:Label></td>
            </tr>

            <tr>
                <td class="auto-style1"><b>Latest Whereabouts :</b></td>
                <td class="auto-style2"><asp:Label ID="L7" runat="server" Text="Label"></asp:Label></td>
            </tr>
   
             <tr>
               <td class="auto-style11"><b>Last Seen in the Office/Last Badge Entry :</b></td>
               <td class="auto-style22"><asp:Label ID="LastBadge" runat="server" Text="Label"></asp:Label></td>
           </tr>

             <tr>
               <td class="auto-style1"><b>Type of Transport :</b></td>
               <td class="auto-style2"><asp:Label ID="Transport" runat="server" Text="Label"></asp:Label></td>
           </tr>

            <tr>
                <td class="auto-style11"><b>Initial Remarks :</b></td>
                <td class="auto-style22"><asp:Label ID="L8" runat="server" Text="Label"></asp:Label></td>
           </tr>

            <tr>
               <td class="auto-style1"><b>Details :</b></td>
               <td class="auto-style2"><asp:Label ID="L9" runat="server" Text="Label"></asp:Label></td>
            </tr>
          
          <tr>
              <td class="auto-style11"><b>Remarks :</b></td>
              <td class="auto-style22"><asp:Label ID="L10" runat="server" Text="Label"></asp:Label></td>
          </tr>

          <tr>
              <td class="auto-style1"><b>Quarantine Start Date :</b></td>
              <td class="auto-style2"><asp:Label ID="L11" runat="server" Text="Label"></asp:Label></td>
          </tr>
    
          <tr>
              <td class="auto-style11"><b>Quarantine End Date :</b></td>
              <td class="auto-style22"><asp:Label ID="L12" runat="server" Text="Label"></asp:Label></td>
          </tr>
 
           <tr>
               <td class="auto-style1"><b>Employee/Client :</b></td>
               <td class="auto-style2"><asp:Label ID="L13" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
           <tr>
               <td class="auto-style11"><b>Remarks/Call Out :</b></td>
               <td class="auto-style22"><asp:Label ID="L14" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
            
          
           <tr>
               <td class="auto-style1"><b>For Contact Tracing :</b></td>
               <td class="auto-style2"><asp:Label ID="ForCTracing" runat="server" Text="Label"></asp:Label>
                   
               </td>
           </tr>
   
           <tr>
               <td class="auto-style11"><asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Close Contact :" CssClass="’btn-link’" Font-Size="Medium" />
               </td>
               <td class="auto-style22"><asp:Label ID="CloseContact" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
         
           <tr>
               <td class="auto-style1"><b>Contact Tracing Remarks :</b></td>
               <td class="auto-style2"><asp:Label ID="ContactTracing" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
           <tr>
               <td class="auto-style11"><b>Type of Test :</b></td>
               <td class="auto-style22"><asp:Label ID="TestType" runat="server" Text="Label"></asp:Label></td>
           </tr>

          
           <tr>
               <td class="auto-style1"><b>PCR Test Result :</b></td>
               <td class="auto-style2"><asp:Label ID="PCR" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
           <tr>
               <td class="auto-style11"><b>Total PCR Test :</b></td>
               <td class="auto-style22"><asp:Label ID="TotPCR" runat="server" Text="Label"></asp:Label></td>
           </tr>

          
           <tr>
               <td class="auto-style1"><b>Date of Testing :</b></td>
               <td class="auto-style2"><asp:Label ID="dtTest" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
           <tr>
               <td class="auto-style11"><b>Release Date :</b></td>
               <td class="auto-style22"><asp:Label ID="dtRelease" runat="server" Text="Label"></asp:Label></td>
           </tr>

          
           <tr>
               <td class="auto-style1"><b>Facility  :</b></td>
               <td class="auto-style2"><asp:Label ID="Faculty" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
           <tr>
               <td class="auto-style11"><b>Current Facility :</b></td>
               <td class="auto-style22"><asp:Label ID="CurrFaculty" runat="server" Text="Label"></asp:Label></td>
           </tr>

          
           <tr>
               <td class="auto-style1"><b>Date of Confinement :</b></td>
               <td class="auto-style2"><asp:Label ID="dtConfinement" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
           <tr>
               <td class="auto-style11"><b>ICU :</b></td>
               <td class="auto-style22"><asp:Label ID="ICU" runat="server" Text="Label"></asp:Label></td>
           </tr>

          
           <tr>
               <td class="auto-style1"><b>Date First Symptom :</b></td>
               <td class="auto-style2"><asp:Label ID="dtFirstSymptoms" runat="server" Text="Label"></asp:Label></td>
           </tr>
   

             <tr>
               <td class="auto-style11"><b>Vaccination Status :</b></td>
               <td class="auto-style22"><asp:Label ID="Vaccination" runat="server" Text="Label"></asp:Label></td>
           </tr>

           <tr>
               <td class="auto-style1"><b>General Category :</b></td>
               <td class="auto-style2"><asp:Label ID="Category" runat="server" Text="Label"></asp:Label></td>
           </tr>

          
           <tr>
               <td class="auto-style11"><b>DOH Category :</b></td>
               <td class="auto-style22"><asp:Label ID="DOH" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
           <tr>
               <td class="auto-style1"><b>Severity :</b></td>
               <td class="auto-style2"><asp:Label ID="Severity" runat="server" Text="Label"></asp:Label></td>
           </tr>

          
           <tr>
               <td class="auto-style11"><b>BHERT :</b></td>
               <td class="auto-style22"><asp:Label ID="BHERT" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
           <tr>
               <td class="auto-style1"><b>Recommendation :</b></td>
               <td class="auto-style2"><asp:Label ID="Recommendation" runat="server" Text="Label"></asp:Label></td>
           </tr>

          
           <tr>
               <td class="auto-style11"><b>Date A1 Assessment :</b></td>
               <td class="auto-style22"><asp:Label ID="dtA1Assessment" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
           <tr>
               <td class="auto-style1"><b>Date CT Delivery Start :</b></td>
               <td class="auto-style2"><asp:Label ID="dtCTDeliveryStart" runat="server" Text="Label"></asp:Label></td>
           </tr>
        
          
           <tr>
               <td class="auto-style11"><b>Date CT Delivery End :</b></td>
               <td class="auto-style22"><asp:Label ID="dtCTDeliveryEnd" runat="server" Text="Label"></asp:Label></td>
           </tr>
   
           <tr>
               <td class="auto-style1"><b>Date A1 CT Start :</b></td>
               <td class="auto-style2"><asp:Label ID="dtCTA1Start" runat="server" Text="Label"></asp:Label></td>
           </tr>
        
          
           <tr>
               <td class="auto-style11"><b>Date CT A1 End :</b></td>
               <td class="auto-style22"><asp:Label ID="dtCTA1End" runat="server" Text="Label"></asp:Label></td>
           </tr>

             <tr>
               <td class="auto-style1"><b>Date LGU Letter :</b></td>
               <td class="auto-style2"><asp:Label ID="LGU" runat="server" Text="Label"></asp:Label></td>
           </tr>

         </table>
        
    </form>
</body>
</html>
