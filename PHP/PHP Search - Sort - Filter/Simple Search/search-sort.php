<?php

$dyn_table = '';

if(isset($_POST['name'])) 
{ 
  $name = $_POST['name'];
  if ($name != '')
   {
      if(preg_match("/^[  a-zA-Z]+/", $name))
      { 
        $dyn_table = '<table id="t1" border="1" cellpadding="10">';        

        $dyn_table .= "<thead><tr><th>Col 1</th><th>Col 2</th></tr></thead>";

  
        $dyn_table .= '<tr><td>SQL   </td><td>1</td><tr>';
        $dyn_table .= '<tr><td>Oracle</td><td>3</td><tr>';
        $dyn_table .= '<tr><td>MySQL </td><td>2</td><tr>';

        $dyn_table .= '</table>';
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
      var t1 = new sortableTable("t1",0,"int,str,float,date");
        window.onload = function(){
        t1.init();
      }
    </script> 

  </head> 
  <p><body> 
    <h>Search  Contacts Details</h> 
    <p>You  may search either by first or last name</p> 

    <form  method="post" action="search.php?go"  id="searchform"> 
      <input  type="text" name="name"> 
      <input  type="submit" name="submit" value="Search"> 
    </form> 
   
<?php   
    echo $dyn_table; 
?>

</html>
