<?php

$dyn_table = '';

if(isset($_POST['name'])) 
{ 
  $name = $_POST['name'];
  if ($name != '')
   {
      if(preg_match("/^[  a-zA-Z]+/", $name))
      { 
        $dyn_table = '<table border="1" cellpadding="10">';        
  
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
