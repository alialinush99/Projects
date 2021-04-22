<?php
	session_start();
	// make connection to db
	include('config.php');

	if(isset($_SESSION['email'])) {
		$email = $_SESSION['email'];

		$sql = "SELECT * FROM User_ WHERE Email ='{$email}'";
		if($result = $conn->query($sql)) {
		  if($result->num_rows > 0) {
		    $json = array();
			while($row = mysqli_fetch_assoc($result)){
			   $json[] = $row;
			}
			echo json_encode($json);
		  } else {
		    echo "non-existent";
		  }
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
	} else {
		echo "not-logged-in";
	}
?>