<?php

        exit();


	$msg = "";
	$rURL = base64_decode(@$_POST['rURL64']);
	$challenge = base64_decode(@$_POST['challenge64']);

	$authUsed = @$_POST['authUsed'];
	$userid = @$_POST['username'];
	$password = @$_POST['password'];
	$expiryOption = @$_POST['expiry'];

	$ret = false;

        /* TOM	*/
	$userid = str_replace(' ','', $userid);
    
        $pos = strpos($userid, '\\');
        if ($pos>0)  $userid = substr($userid, $pos+1 );

        $pos = strpos($userid, '@');
        if ($pos>0)  $userid = substr($userid, 0, $pos);

        $password = rtrim($password);
     	


        include("doMSFTLogin.php");

                       
	if ($ret && $username != '' && $eid != '')
	{
		$fp = fopen("keys/private.pem", "r");
		$priv_key = fread($fp, 8192);
		fclose($fp);

		$res = openssl_get_privatekey($priv_key);
		$responseData = $challenge . $username . $email . $eid;
		$hash = md5($responseData);
		openssl_private_encrypt($hash, $response, $res);
		openssl_free_key($res);

		if (strstr($rURL, "?"))
			$url = $rURL . "&username=";
		else
			$url = $rURL . "?username=";
		header("Location: " . $url . urlencode($username)
					. "&email=" . urlencode($email)
					. "&eid=" . $eid
					. "&response=" . urlencode(base64_encode($response))
					. "&login_expiry=" . $expiryOption);

                 /* DELETE COOKIE - TOM */
		 $NAME1 = 'Login';
		 IF (isset($_COOKIE[$NAME1]))
		       {
		          setcookie($NAME1, 0 , time()-100 ); 
		       }


		exit();
	}


	if ($ret) // The user is authenticated but no employee ID...
	{
		$msg = "Login Succeeded.. BUT.. your Employee ID is not part of the information on this server... please use another mechanism to authenticate (e.g. ConvergysNow)";
		$_REQUEST['logintype'] = "mycvg";
	}

  echo "<script type='text/javascript'>alert('$rURL');</script>";

	$_REQUEST['challenge'] = $challenge;
	$_REQUEST['rURL'] = $rURL;
	include("login.php");
?>




