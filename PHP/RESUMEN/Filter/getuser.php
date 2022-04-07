<!DOCTYPE html>
<html>
<head>
<style>
table {
  width: 100%;
  border-collapse: collapse;
}

table, td, th {
  border: 1px solid black;
  padding: 5px;
}

th {text-align: left;}
</style>
</head>
<body>

<?php
$q = intval($_GET['q']);

echo "veamos";

echo "<table>
<tr>
<th>Firstname</th>
<th>Lastname</th>
<th>Age</th>
<th>Hometown</th>
<th>Job</th>
</tr>";


  echo "<tr>";
  echo "<td>Tomas</td>";
  echo "<td>Dale</td>";
  echo "<td>56</td>";
  echo "<td>Sta Tecla</td>";
  echo "<td>Engineer</td>";
  echo "</tr>";


echo "</table>";
?>
</body>
</html>
