<?php

  $userid = @$_POST['userid'];
  $DOMAIN2= ""; 

  if (strlen($userid) != 9)
  {   
     exit();
  }


 /* $regex = "^\\d{9}$";
  if (preg_match($regex, $userid)) 
  {
  }
  else
  {
     exit();
  }*/


  function getDBConn() {
  	$db = "(DESCRIPTION=(ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 170.14.162.219)(PORT = 1521)))(CONNECT_DATA=(SID=CTRD)))";
  	return @oci_connect("qctest", "nowc0nn3ct", $db);
  }
  
  $conn = getDBConn();

        if ($conn) {

		$query = "SELECT EMAIL_ADDR, AD_UPN FROM MIIS_BASE_CURR WHERE EMPLID = :userid ";                     
		$stmt = oci_parse($conn, $query);

		if ($stmt) {
			oci_bind_by_name($stmt, ':userid', trim(strtolower($userid)));
			if (oci_execute($stmt, OCI_DEFAULT)) {
				if ($row = oci_fetch_row($stmt)) {
                                       	$EMAIL2 = $row[0];
					$DOMAIN2 = $row[1];

					$pos = strpos($DOMAIN2, '@');
					if ($pos>0)  $DOMAIN2 = substr($DOMAIN2, $pos+1);
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

    /* SEND EMAIL */

   $to = $EMAIL2;      

   /*$headers = "From: $email_from \r\n"; */

   $headers = 'MIME-Version: 1.0' . "\r\n";
   $headers .= 'Content-type: text/html; charset=iso-8859-1' . "\r\n";
   $headers .= 'From: $email_from \r\n';

   $email_from = 'Tomas.Dale@Convergys.com';
   $email_subject = "Your Domain";
   
   $email_body = "Your Domain to access quick connect is   <strong>" . $DOMAIN2 . "</strong>";


   $retval = mail($to,$email_subject,$email_body,$headers);  
    
   /* RESET COOKIE */
     
   $NAME1 = 'Login';
   IF (isset($_COOKIE[$NAME1]))
       {
          setcookie($NAME1, 0 , time()+3660 ); 
       }
     
   echo "<script>alert('Message Sent Successfully');</script>";
     
   header("Location: https://qc2web1.na.convergys.com/loginTest/login.php"); 
   exit();
?>