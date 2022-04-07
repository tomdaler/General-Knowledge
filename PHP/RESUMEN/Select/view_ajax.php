<?php
	include 'database.php';
	$sql = "SELECT * FROM crud";
	$result = $conn->query($sql);

	if ($result->num_rows > 0) {
		while($row = $result->fetch_assoc()) {
?>	
		<tr>
			<td><?=$row['name'];?></td>
			<td><?=$row['email'];?></td>
			<td><?=$row['phone'];?></td>
			<td><?=$row['city'];?></td>
			<td><button type="button" class="btn btn-success btn-sm update" data-toggle="modal" data-keyboard="false" data-backdrop="static" data-target="#update_country"
			data-id="<?=$row['id'];?>"
			data-name="<?=$row['name'];?>"
			data-email="<?=$row['email'];?>"
			data-phone="<?=$row['phone'];?>"
			data-city="<?=$row['city'];?>"
			">Edit</button></td>
		</tr>
<?php	
	}
	}
	else {
		echo "<tr >
		<td colspan='5'>No Result found !</td>
		</tr>";
	}
	mysqli_close($conn);
?>
