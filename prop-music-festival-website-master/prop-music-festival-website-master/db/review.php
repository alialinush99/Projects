<?php
	session_start();
	// make connection to db
	include('config.php');

	function saveReview($conn, $review) {
		$sql = "REPLACE Review (EventId, UserId, UserName, Stars, Review, PublishDate) 
			VALUES ('{$review ->{'EventId'}}', '{$review ->{'UserId'}}', '{$review ->{'UserName'}}',
			'{$review ->{'Stars'}}', '{$review ->{'Review'}}', '{$review ->{'PublishDate'}}')";

		if ($conn->query($sql) === TRUE) {
		  echo "success";
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
		$conn->close();
	}

	function getReviewsForEvent($conn, $event_id) {
		$sql = "SELECT * FROM Review WHERE EventId = '{$event_id}'";
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
		$conn->close();
	}

	if (isset($_POST['function'])) {
		if($_POST['function'] == "getReviewsForEvent") {
			$event_id = mysqli_real_escape_string($conn, $_POST['eventId']);
			echo getReviewsForEvent($conn, $event_id);
		}
		
		if($_POST['function'] == "saveReview") {
			$review = json_decode($_POST['review']);
			echo saveReview($conn, $review);
		}
	}
?>