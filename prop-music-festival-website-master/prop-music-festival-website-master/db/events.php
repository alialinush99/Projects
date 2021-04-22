<?php
	session_start();
	// make connection to db
	include('config.php');

	function getEventById($conn, $event_id) {
		$event = "";
		$sql = "SELECT * FROM Event_ WHERE EventId ='{$event_id}'";
		if($result = $conn->query($sql)) {
		  if($result->num_rows > 0) {
			while($row = mysqli_fetch_assoc($result)){
			   $event = $row;
			}
			echo json_encode($event);
		  } else {
		    echo "non-existent";
		  }
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
		$conn->close();
	}

	function getAllEvents($conn) {
		$sql = "SELECT * FROM Event_ ORDER BY EndDate ASC";
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

	function getEventTicketAvailability($conn, $event_id) {
		$eventAvailability = "";
		$sql = "SELECT Event_.NrTickets AS TicketPool,
			Count(Ticket.Id) AS BookedTickets FROM Event_
			INNER JOIN UserBooking ON UserBooking.EventId = Event_.EventId 
			INNER JOIN Ticket ON UserBooking.Id = Ticket.UserBookingId 
			WHERE Event_.EventId ='{$event_id}'";

		if($result = $conn->query($sql)) {
		  if($result->num_rows > 0) {
			while($row = mysqli_fetch_assoc($result)){
			   $eventAvailability = $row;
			}
			echo json_encode($eventAvailability);
		  } else {
		    echo "non-existent";
		  }
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
		$conn->close();
	}

	if (isset($_POST['function'])) {
		if($_POST['function'] == "getAllEvents") {
			echo getAllEvents($conn);
		}
		
		if($_POST['function'] == "getEventById") {
			$event_id = mysqli_real_escape_string($conn, $_POST['eventId']);
			echo getEventById($conn, $event_id);
		}

		if($_POST['function'] == "getEventTicketAvailability") {
			$event_id = mysqli_real_escape_string($conn, $_POST['eventId']);
			getEventTicketAvailability($conn, $event_id);
		}
	}
?>