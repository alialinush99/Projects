<?php
	session_start();
	// make connection to db
	include('config.php');

	function getPerformersForEvent($conn, $event_id) {
		$sql = "SELECT * FROM LineUp WHERE EventId ='{$event_id}'";
		if($result = $conn->query($sql)) {
			if($result->num_rows > 0) {
				$performers = array();
				while($row = mysqli_fetch_assoc($result)){
				   $performers[] = $row;
				}
				echo json_encode($performers);
			} else {
			echo "non-existent";
			}
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
		$conn->close();
	}

	$event_id = mysqli_real_escape_string($conn, $_POST['eventId']);
	echo getPerformersForEvent($conn, $event_id);
?>