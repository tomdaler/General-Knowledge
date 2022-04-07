<?php

$dyn_table = '';

if(isset($_POST['name'])) 
{ 
  $name = $_POST['name'];
  if ($name != '')
   {
      if(preg_match("/^[  a-zA-Z]+/", $name))
      { 

    $dyn_table = "<table id='t1'><thead><tr><th>Col 1 int</th><th>Col 2 str</th><th>Col 3 float</th><th>Col 3 date</th><th>Col 4 int</th></tr></thead>";

    $dyn_table.= "<tbody>"; 
    $dyn_table.= "<tr><td>1</td><td>A</td><td>1</td><td>1/13/1980</td><td>100</td></tr>";
    $dyn_table.= "<tr><td>2</td><td>C</td><td>1.1</td><td>1/13/2005</td><td>50</td></tr>";
    $dyn_table.= "<tr><td>3</td><td>D</td><td>1.11</td><td>1/13/1985</td><td>200</td></tr>";
    $dyn_table.= "<tr><td>11</td><td>B</td><td>11.</td><td>1/13/2006</td><td>100</td></tr>";   
    $dyn_table.= "<tr><td>12</td><td>E</td><td>11.11</td><td>1/13/1989</td><td>1989</td></tr>";
 
    $dyn_table.= "</tbody></table>";
 

     }
   }
}

?>


<html> 
  <head> 
    <meta  http-equiv="Content-Type" content="text/html;  charset=iso-889-1"> 
    <title>Search  Contacts</title> 

    <LINK href="css/sortTable.css" rel="stylesheet" type="text/css">
    <script type="text/JavaScript" src="javascript/sortTable.js"></script>
    <script type="text/JavaScript"> 


        // IMPORTANTE TIPO DE VARIABLE, EN ESTE CASO SON 5
        var t1 = new sortableTable("t1",0,"int,str,float,date, int");


        window.onload = function(){
        t1.init();
      }
    </script> 

  </head> 
  <p><body> 
    <h>Search  Contacts Details</h> 
    <p>You  may search either by first or last name</p> 

    <form  method="post" action="search-sort1.php?go" id="searchform"> 
      <input  type="text" name="name"> 
      <input  type="submit" name="submit" value="Search"> 
    </form> 
   
<?php   
    echo $dyn_table; 
?>

</html>
