<?php

$dyn_table = '';

if(isset($_POST['name'])) 
{ 
  $name = $_POST['name'];
  if ($name != '')
   {
      if(preg_match("/^[  a-zA-Z]+/", $name))
      { 


      $dyn_table .= "<table><thead><tr>";

      $dyn_table .= "<th onclick='sort_table(people, 0, asc1); asc1 *= -1; asc2 = 1; asc3 = 1; asc4 = 1;'>Name</th>";
      $dyn_table .= "<th onclick='sort_table(people, 1, asc2); asc2 *= -1; asc3 = 1; asc4 = 1; asc1 = 1;'>Surname</th>";
      $dyn_table .= "<th onclick='sort_table(people, 2, asc3); asc3 *= -1; asc4 = 1; asc1 = 1; asc2 = 1;'>Age</th>";
      $dyn_table .= "<th onclick='sort_table(people, 3, asc4); asc4 *= -1; asc1 = 1; asc2 = 1; asc3 = 1;'>City</th>";
      $dyn_table .= "</tr></thead>";


      $dyn_table .= "<tbody id='people'>";
 
      $dyn_table .= "<tr><td>Raja</td><td>Dey</td><td>18</td><td>NY</td></tr>";
      $dyn_table .= "<tr><td>Alan</td><td>Dale</td><td>40</td><td>LA</td></tr>";
      $dyn_table .= "<tr><td>Tom</td><td>Horn</td><td>5</td><td>SS</td></tr>";
      $dyn_table .= "<tr><td>Mike</td><td>Nash</td><td>23</td><td>HK</td></tr>";

      $dyn_table .= "</tbody></table>";

     }
   }
}

?>


<html> 
  <head> 
    <meta  http-equiv="Content-Type" content="text/html;  charset=iso-889-1"> 
    <title>Search  Contacts</title> 

    <LINK href="css/sortTable.css" rel="stylesheet" type="text/css">

    <script type="text/javascript">
        
        var people, asc1 = 1, asc2 = 1, asc3 = 1, asc4 = 1;           


        window.onload = function () {
            people = document.getElementById("people");
        }

       function sort_table(tbody, col, asc){
          var rows = tbody.rows, rlen = rows.length, arr = new Array(), i, j, cells, clen;
         
         // fill the array with values from the table
          for(i = 0; i < rlen; i++){
           cells = rows[i].cells;
           clen = cells.length;
           arr[i] = new Array();
           for(j = 0; j < clen; j++){
             arr[i][j] = cells[j].innerHTML;
            }
         }
    
       // sort the array by the specified column number (col) and order (asc)
        arr.sort(function(a, b){
           return (a[col] == b[col]) ? 0 : ((a[col] > b[col]) ? asc : -1*asc);
        });
         for(i = 0; i < rlen; i++){
           arr[i] = "<td>"+arr[i].join("</td><td>")+"</td>";
        }
        tbody.innerHTML = "<tr>"+arr.join("</tr><tr>")+"</tr>";
      }
    </script>
  

  </head> 
  <p><body> 
    <h>Search  Contacts Details</h> 
    <p>You  may search either by first or last name</p> 

    <form  method="post" action="search-sort2.php?go" id="searchform"> 
      <input  type="text" name="name"> 
      <input  type="submit" name="submit" value="Search"> 
    </form> 
   
<?php   
    echo $dyn_table; 
?>


</html>
