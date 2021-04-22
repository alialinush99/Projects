<?php
	session_start();
	// make connection to db
	include('config.php');

	// Check if current user exists
	function userExists($conn, $email) {
		$check_existing_email = "SELECT UserId FROM User WHERE Email = '{$email}'";
  		if ($result = $conn->query($check_existing_email)) {
    		echo $result->num_rows > 0;
		} else {
			echo false;
		}
	}

	function fetchMultiple($conn, $sql) {
		if($result = $conn->query($sql)) {
		  if($result->num_rows > 0) {
		    $data = array();
			while($row = mysqli_fetch_assoc($result)){
			   $data[] = $data;
			}
			echo json_encode($data);
		  } else {
		    echo "non-existent";
		  }
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
	}

	function isUserLoggedIn() {
		if(isset($_SESSION['email'])) {
			echo "logged-in";
		} else {
			echo "not-logged-in";
		}
	}

	if (isset($_POST['function'])) {
		switch($_POST['function']) {
			case "isUserLoggedIn": json_encode(isUserLoggedIn()); break;
		}
	}

?>