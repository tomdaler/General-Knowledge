<?php
            
          

if (strstr($_SERVER['HTTP_USER_AGENT'], 'iPhone;') || strstr($_SERVER['HTTP_USER_AGENT'], 'iPod;') || // iPhone and iPod touch
			strstr($_SERVER['HTTP_USER_AGENT'], 'iPhone Simulator;') ||
			strstr($_SERVER['HTTP_USER_AGENT'], 'Android') // Android Phones
		) { // goto Smart Phone specific page
        include('iPhoneLogin.php');
	exit();
}
if (strstr($_SERVER['HTTP_USER_AGENT'], 'BlackBerry') ||
			strstr($_SERVER['HTTP_USER_AGENT'], 'Opera Mini') || // Any device using opera mini
			strstr($_SERVER['HTTP_USER_AGENT'], 'Mobile') || // Any Movile device
			strstr($_SERVER['HTTP_USER_AGENT'], 'Symbian') // Any Symbian OS device (Nokia) device
		) { // goto simple page
        include('simpleLogin.php');
        exit();
}
?>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head>
	
	<link rel='stylesheet' type='text/css' href='css/login.css'>

	

	<style type="text/css">
		body {
			
			background-repeat: no-repeat;
			background-attachment: fixed;
			background-position: center;
		}
		html, body, #wrapper {
			align: center;
			margin: 0;
			padding: 0;
			border: none;
			text-align: center;
		}
		#wrapper {
			margin: 0 auto;
			text-align: left;
			vertical-align: middle;
                     align: center;
		}
	</style>



<script type="text/javascript">

   function CheckCookie() {

    cname = 'Login';
    cvalue = 0;

    // 1 hour
    var d = new Date();
    d.setTime(d.getTime() + (3660 * 1000));
    var expires = "expires="+d.toUTCString();

    // READ cookie
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i=0; i<ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1);
        if (c.indexOf(name) != -1) 

             cvalue =  c.substring(name.length,c.length);
             //alert(cvalue);
    }

    cvalue++;
    alert(" new value "+cvalue);             
    document.cookie = cname + "=" + cvalue + "; " + expires;
    
    if (cvalue >2)
          {
              alert("Account blocked");
              return false;
          }
    
    return true;
}
</script>


</head>
<body>

<?php
global $loginErrorMsg;

$selMsg = "";

if(@$_REQUEST["expiryOptions"]!= '' && @$_REQUEST["expiryOptions"]!== false)
	$options = split(",", @$_REQUEST["expiryOptions"], 10);
if(count(@$options) > 0)
{
	$selMsg = "";
	if(array_search("1825", $options)!== false)
	{
		if($options[0] == "1825")
 			$selMsg = "<option class='' value='1825' selected='selected'>until I log out</option>";
		else
 			$selMsg = "<option class='' value='1825'>until I log out</option>";
	}
	if(array_search("30", $options)!== false)
	{
		if($options[0] == "30")
 			$selMsg = $selMsg . "<option class='' value='30' selected='selected'>for 30 days</option>";
		else
 			$selMsg = $selMsg . "<option class='' value='30'>for 30 days</option>";
	}
	if(array_search("7", $options)!== false)
	{
		if($options[0] == "7")
 			$selMsg = $selMsg . "<option class='' value='7' selected='selected'>for 7 days</option>";
		else
 			$selMsg = $selMsg . "<option class='' value='7'>for 7 days</option>";
	}
	if(array_search("1", $options)!== false)
	{
		if($options[0] == "1")
 			$selMsg = $selMsg . "<option class='' value='1' selected='selected'>for 24 hours</option>";
		else
 			$selMsg = $selMsg . "<option class='' value='1'>for 24 hours</option>";
	}
	if(array_search("0", $options)!== false)
	{
		if($options[0] == "0")
 			$selMsg = $selMsg . "<option class='' value='0' selected='selected'>until I close my browser</option>";
		else
 			$selMsg = $selMsg . "<option class='' value='0'>until I close my browser</option>";
	}
}

function printForm($authType, $name, $selectDomain)
{
	global $selMsg;
	$rURL64 = base64_encode(@$_REQUEST['rURL']);
	$site = htmlspecialchars(@$_REQUEST["site"]);
	$expiryOptions = htmlspecialchars(@$_REQUEST["expiryOptions"]);
	$auth = htmlspecialchars(@$_REQUEST["auth"]);
?>
	<div class="dhtmlgoodies_aTab">
	<center>
	<h4>Use your <font color="blue"><?php echo $name; ?></font> credentials to login.</h4>
	<br>
	<b><font color="red"><?php global $loginErrorMsg; echo $loginErrorMsg; ?></font></b>
	<br>
	<form name="form<?php echo $authType; ?>" method="post" onSubmit="document.form<?php echo $authType;?>.action= 'doLogin.php' + window.location.hash">
	<input type="hidden" name="rURL64" value="<?php echo $rURL64; ?>">
	<input type="hidden" name="site" value="<?php echo $site; ?>">
	<input type="hidden" name="expiryOptions" value="<?php echo $expiryOptions; ?>">
	<input type="hidden" name="auth" value="<?php echo $auth; ?>">
	<input type="hidden" name="authUsed" value="<?php echo $authType; ?>">
	<table cellpadding='2' cellspacing='1' border='0' class='datatable'>
<?php
	if ($selectDomain)
	{
?>
		<tr>
			<th class='leftheader'>Network Login ID:</th>
			<td class='cell'><input type="text" name="username" maxlength="30"></td>
		</tr>
		<tr>
			<th class='leftheader'>Password:</th>
			<td class='cell'><input type="password" name="password" maxlength="30"></td>
		</tr>
		<tr>
			<th class='leftheader'>Domain:</th>
			<td class='cell'>
				<select  class='select' name='domain'>
					<option value='ASPAC'>ASPAC</option>
					<option value='EMEA'>EMEA</option>
                    <option value='EXTRANET'>EXTRANET</option>
					<option value='INDIA'>INDIA</option>
					<option value='NA' selected>NA</option>
					<option value='PHIL'>PHIL</option>
					<option value='SA'>SA</option>
					<option value='americas'>Americas</option>
					<option value='stream-ad'>Stream-AD</option>
					<option value='emea-stream'>EMEA-Stream</option>
					<option value='apac'>APAC</option>
					<option value='etelecare'>ETELECARE</option>										
					<option value='usa'>USA</option>					
					<option value='phl'>PHL</option>					
					<option value='lac'>LAC</option>						
				</select>
			</td>
		</tr>
<?php
	} else {
?>
		<tr>
			<th class='leftheader'>Employee ID#:</th>
			<td class='cell'><input type="text" name="username" maxlength="30"></td>
		</tr>
		<tr>
			<th class='leftheader'>MyCVG Portal Password:</th>
			<td class='cell'><input type="password" name="password" maxlength="30"></td>
		</tr>




<?php
	}
	if ($selMsg != "")
	{
?>
		<tr>
		<th class='leftheader'>Save Login:</th>
		<td><select class='select' name='expiry'><?php echo $selMsg;?></select></td>
		</tr>

<?php
	}
?>

	</table>
       <div align="right">

       <a href="EmailDomain.html">Forgot Domain</a>

 <input type="button" onclick="alert(

          'When logging in via this portal you should utilize your primary login ID. \n'+
          'If you are Stream Legacy and have been given a Convergys ID you should use that ID.\n\n'+  
 
          ' - User Name \n'+
          '    Enter just your user name, do not enter any domain details\n\n'+
 
          ' - Password\n'+
          '    Enter the password associated to the entered User Name\n\n'+
 
          ' - Domain\n'+
          '     If you are Stream legacy the following should provide assistance \n'+
          '     selecting the correct domain based on your location:\n\n'+

          'Americas-Stream\n\n'+

          'Latin America: Dominican Republic, El Salvador, and Honduras only \n'+
          'North America, but excludes: Colonnade, Jacksonville, and Rio Rancho.\n\n'+

          'LAC-Stream (Latin America: Nicaragua only) \n'+
          'USA-Stream (Colonnade, Jacksonville, and Rio Rancho only) \n'+
          'APAC-Stream \n\n'+

          'China: Suzhou \n'+
          'India: Mumbai \n'+
          'Philippines: Quezon City, Mall of Asia (Pasay City), and some of Shaw.\n\n'+

          'PHL-Stream (Philippines: Alabang, Cebu1, Cebu2, Cebu3, Clark, Libis, Makati, and some of Shaw) \n'+
          'EMEA-Stream (EMEA; Excludes: Altrincham, Belfast, Middleton, Stockport) \n'+
          'LBMSLN-Stream (EMEA; Only: Altrincham, Belfast, Middleton, Stockport) '


           );" 
         
        
        value="Help" />        


        <input type="submit" class='submit' name='submit' onclick='return CheckCookie();' value="Login">
	

	</form>
	</center>
	</div

<?php
}
?>

	<table id="wrapper" width='40%'>
		<tr>
			
			<td  align='center'>
				<h2>Please Log In</h2>
				
				<div id="dhtmlgoodies_tabView">
					<?php
					printForm("activeDir", "Windows Network", true);
					?>
				</div>
			</td>
		</tr>
	</table>

<script type="text/javascript">
initTabs(Array('Windows Network'),0,400,200);
<?php
$auth = @$_REQUEST["auth"];
if ($auth != "")
{
	if ($auth == "mycvg") { echo 'deleteTab(false,0);'; }
	if ($auth == "activeDir") { echo 'deleteTab(false,1);'; }
	if ($auth == "activeDirSelected") { echo 'showTab(0);'; }
	if ($auth == "mycvgSelected") { echo 'showTab(1);'; }
}
if (@$authUsed != "")
{
	if ($authUsed == "activeDir") { echo 'showTab(0);'; }
	if ($authUsed == "mycvg") { echo 'showTab(1);'; }
}
?>
</script>

</body>
</html>
<?php
generateFooter();
?>
