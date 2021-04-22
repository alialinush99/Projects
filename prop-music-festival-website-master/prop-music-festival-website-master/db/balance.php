<?php
	session_start();
	// make connection to db
	include('config.php');

	// Get current user balance
	function getCurrentBalance($conn, $email) {
		$balance = "";
		$sql = "SELECT Balance FROM User_ WHERE Email ='{$email}'";
		if($result = $conn->query($sql)) {
			if($result->num_rows > 0) {
				while($row = mysqli_fetch_assoc($result)) {
					$balance = $row;
				}
			} else {
				$balance = 0;
			}
		}
		return $balance;
	}

	// Add amount to current user's balance
	function insertBalance($conn, $email, $currentBalance) {
		$sql = "UPDATE User_ SET Balance = '{$currentBalance}' WHERE Email = '{$email}'";
		if ($conn->query($sql) === TRUE) {
			echo json_encode($currentBalance);
		} else {
			echo "Error: " . $sql . "<br>" . $conn->error;
		}
	}

	if (isset($_POST['function'])) {
		if(isset($_SESSION['email'])) {
			$email = $_SESSION['email'];
			echo json_encode(getCurrentBalance($conn, $email));
		} else {
			echo "not-logged-in";
		}
	} else {
		if(isset($_POST['amount'])) {
			if(isset($_SESSION['email'])) {
				$email = $_SESSION['email'];
				$amount = mysqli_real_escape_string($conn, $_POST['amount']);
				insertBalance($conn, $email, $amount);
				echo "";
			} else {
				echo "not-logged-in";
			}
		}
	}	
?>
