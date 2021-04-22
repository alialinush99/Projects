<?php
	session_start();
	// make connection to db
	include('config.php');

	function getCampingSpotCapacities($conn) {
		$event_id = mysqli_real_escape_string($conn, $_POST['eventId']);

		$sql = "SELECT DISTINCT Capacity FROM CampingSpot WHERE EventId ='{$event_id}' AND Availability = 'A'
			ORDER BY Capacity ASC";
		if($result = $conn->query($sql)) {
		  if($result->num_rows > 0) {
		  	$json = array();
			while($row = mysqli_fetch_assoc($result)){
			   $json[] = $row['Capacity'];
			}
			echo json_encode($json);
		  } else {
		    echo "non-existent";
		  }
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
	}

	function getCampingSpotWithCapacity($conn) {
		$event_id = mysqli_real_escape_string($conn, $_POST['eventId']);
		$capacity = mysqli_real_escape_string($conn, $_POST['capacity']);

		$camping_spot_id = "";
		$sql = "SELECT * FROM CampingSpot WHERE EventId ='{$event_id}' AND Capacity = '{$capacity}' AND Availability = 'A' LIMIT 1";
		if($result = $conn->query($sql)) {
		  if($result->num_rows > 0) {
			while($row = mysqli_fetch_assoc($result)){
			   $camping_spot_id = $row;
			}
			echo json_encode($camping_spot_id);
		  } else {
		    echo "non-existent";
		  }
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
	}

	function updateCampingSpotStatus($conn, $camping_spot_id) {
		$sql = "UPDATE CampingSpot SET Availability = 'R' WHERE Id = '{$camping_spot_id}'";
		if ($conn->query($sql) === TRUE) {
			echo "success";
		} else {
			echo "Error: " . $sql . "<br>" . $conn->error;
		}
	}


	if (isset($_POST['function'])) {
		switch($_POST['function']) {
			case 'getCampingSpotCapacities': getCampingSpotCapacities($conn); break;
			case 'getCampingSpotWithCapacity': getCampingSpotWithCapacity($conn); break;
			case 'updateCampingSpotStatus': 
				$camping_spot_id = mysqli_real_escape_string($conn, $_POST['campingSpotId']);
				updateCampingSpotStatus($conn, $camping_spot_id); 
				break; 
		}
	}
?>