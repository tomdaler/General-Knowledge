<?php

        $db = "(DESCRIPTION=(ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 170.14.162.219)(PORT = 1521)))(CONNECT_DATA=(SID=CTRD)))";

        @conn = @ocilogon("qctest","nowc0nn3ct", $db);
	
        if ($conn) {

		$query = "SELECT EMAIL_ADDR, AD_UPN FROM MIIS_BASE_CURR WHERE AD_SAMACCOUNT = :userID ";


                echo "<script type='text/javascript'>alert('QUERY SET');</script>"; 

		$stmt = oci_parse($conn, $query);
		if ($stmt) {
			oci_bind_by_name($stmt, ':UserID', trim(strtolower($userid)));
			if (oci_execute($stmt, OCI_DEFAULT)) {
				if ($row = oci_fetch_row($stmt)) {
					$EMAIL2  = $row[0];
					$DOMAIN2 = $row[1];
				}
				else {
					$msg = "Login Succeeded.. BUT.. your Employee ID is not part of the information in QuickConnect";
				}
			}
			else {
				$err = oci_error($stmt);
				$msg = $err['message'];
			}
			oci_free_statement($stmt);
		}
		else {
			$err = oci_error($stmt);
			$msg = $err['message'];
		}
		oci_close($conn);
	}
	else {
		$err = oci_error($conn);
		$msg = $err['message'];
	}


    echo "<script type='text/javascript'>alert('$EMAIL2');</script>"; 
    echo "<script type='text/javascript'>alert('$DOMAIN2');</script>"; 

   

?>