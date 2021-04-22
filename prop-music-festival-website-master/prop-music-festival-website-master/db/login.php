<?php
	session_start();
	// make connection to db
	include('config.php');

	$email = mysqli_real_escape_string($conn, $_POST['email']);
	$password = mysqli_real_escape_string($conn, $_POST['password']);

	$sql = "SELECT * FROM User_ WHERE Email ='{$email}' AND Password_ ='{$password}'";
	if($result = $conn->query($sql)) {
	  if($result->num_rows > 0) {
	    $_SESSION['email'] = $email;
	    echo "logged";
	  } else {
	    echo "non-existent";
	  }
	} else {
	  echo "Error: " . $sql . "<br>" . $conn->error;
	}
?>