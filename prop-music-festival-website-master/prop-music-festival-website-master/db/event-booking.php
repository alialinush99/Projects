<?php
	session_start();
	// make connection to db
	include('config.php');

	function getCurrentUser($conn) {
		if(isset($_SESSION['email'])) {
			$email = $_SESSION['email'];

			$sql = "SELECT UserId FROM User_ WHERE Email ='{$email}'";
			if($result = $conn->query($sql)) {
			  if($result->num_rows > 0) {

			  	// Create or retrieve user booking 
			    $user_id = mysqli_fetch_assoc($result)["UserId"];

			    switch ($_POST['function']) {
						case 'getAllBookedEvents': getAllBookedEvents($conn, $user_id);break;

						default: createUserBookingOrRetrieve($conn, $user_id); break;
				}
			  } else {
			    echo "non-existent";
			  }
			} else {
			  echo "Error: " . $sql . "<br>" . $conn->error;
			}
		} else {
			echo "not-logged-in";
		}
	}

	function getAllBookedEvents($conn, $user_id) {
		$sql = "SELECT Event_.EventId, Title, Description, StartDate, EndDate, TicketPrice, Address FROM Event_ 
		INNER JOIN UserBooking AS ub ON ub.UserId = '{$user_id}' AND ub.EventId = Event_.EventId
		ORDER BY EndDate ASC";
		if($result = $conn->query($sql)) {
		  if($result->num_rows > 0) {
		    $events = array();
			while($row = mysqli_fetch_assoc($result)){
			   $events[] = $row;
			}
			echo json_encode($events);
		  } else {
		    echo "non-existent";
		  }
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
		$conn->close();
	}


	function createUserBookingOrRetrieve($conn, $user_id) {
		$event_id = mysqli_real_escape_string($conn, $_POST['eventId']);

		if ($result = $conn->query("SELECT Id FROM UserBooking WHERE UserId = '{$user_id}' AND EventId = '{$event_id}'")) {
    		if($result->num_rows == 0) {
				// Create new user booking for event
    			$sql = "INSERT INTO UserBooking (UserId, EventId) VALUES ('{$user_id}', '{$event_id}')";
				if ($conn->query($sql) === TRUE) {
					$user_booking_id = mysqli_insert_id($conn);

					switch ($_POST['function']) {
						case 'buyTickets': createTicket($conn, $event_id, $user_booking_id); break;
						case 'getAllTickets': getAllTicketsForUserBooking($conn, $event_id, $user_booking_id); break;
						case 'getAllCampingSpotsForBooking': getAllCampingSpotsForBooking($conn, $user_booking_id); break;
						case 'bookCampingSpot': bookCampingSpot($conn, $user_booking_id); break;
						case 'getCurrentlyBookedCampingSpot': echo "non-existent"; break; 
					}
				  	
				} else {
				  echo "Error: " . $sql . "<br>" . $conn->error;
				}
    		} else {
    			// Retrieve existing user booking id
    			$user_booking_id = mysqli_fetch_assoc($result)["Id"];

    			switch ($_POST['function']) {
					case 'buyTickets': createTicket($conn, $event_id, $user_booking_id); break;
					case 'getAllTickets': getAllTicketsForUserBooking($conn, $event_id, $user_booking_id); break;
					case 'getAllCampingSpotsForBooking': getAllCampingSpotsForBooking($conn, $user_booking_id); break;
					case 'bookCampingSpot': bookCampingSpot($conn, $user_booking_id); break;
					case 'getCurrentlyBookedCampingSpot': getCurrentlyBookedCampingSpot($conn, $user_booking_id); break; 
				}
    		}
		}
	}

	function getAllTicketsForUserBooking($conn, $event_id, $user_booking_id) {
		$sql = "SELECT Ticket.Id, Ticket.Email, Event_.TicketPrice  FROM Ticket
			INNER JOIN UserBooking as ub ON ub.Id = Ticket.UserBookingId
			INNER JOIN Event_ ON Event_.EventId = ub.EventId
		 WHERE UserBookingId = '{$user_booking_id}'";

		if($result = $conn->query($sql)) {
		  if($result->num_rows > 0) {
		    $tickets = array();
			while($row = mysqli_fetch_assoc($result)){
			   $tickets[] = $row;
			}
			echo json_encode($tickets);
		  } else {
		    echo "non-existent";
		  }
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
		$conn->close();
	}

	function getAllCampingSpotsForBooking($conn, $user_booking_id) {
		$sql = "SELECT CampingSpotTicket.CampingSpotId, Ticket.Id, Ticket.Email FROM CampingSpotTicket
			INNER JOIN Ticket ON Ticket.Id = CampingSpotTicket.TicketId
		 	WHERE CampingSpotTicket.UserBookingId = '{$user_booking_id}'";

		if($result = $conn->query($sql)) {
		  if($result->num_rows > 0) {
		    $tickets = array();
			while($row = mysqli_fetch_assoc($result)){
			   $tickets[] = $row;
			}
			echo json_encode($tickets);
		  } else {
		    echo "non-existent";
		  }
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
		$conn->close();
	}

	// function createTicket($conn, $event_id, $user_booking_id) {
	// 	if (isset($_POST['email'])) {
	// 		$email = mysqli_real_escape_string($conn, $_POST['email']);
	// 		// Check if ticket for this user is present for the current user booking
	// 		if ($result = $conn->query("SELECT Id FROM Ticket WHERE Email = '{$email}' AND UserBookingId = '{$user_booking_id}'")) {
	//     		if($result->num_rows == 0) {
	//     			// Buy ticket
	//     			$sql = "INSERT INTO Ticket (Email, UserBookingId) VALUES ('{$email}', '{$user_booking_id}')";
	// 				if ($conn->query($sql) === TRUE) {
	// 				  echo "success";
	// 				} else {
	// 				  echo "Error: " . $sql . "<br>" . $conn->error;
	// 				}
	//     		} else {
	//     			echo "Ticket already bought for {$email}";
	//     		}
	// 		}
	// 	}
	// 	$conn->close();
	// }

	function createTicket($conn, $event_id, $user_booking_id) {
		if (isset($_POST['email'])) {
			$email = mysqli_real_escape_string($conn, $_POST['email']);

			// Buy ticket
			$sql = "INSERT INTO Ticket (Email, UserBookingId) VALUES ('{$email}', '{$user_booking_id}')";
			if ($conn->query($sql) === TRUE) {
			  echo "success";
			} else {
			  echo "Error: " . $sql . "<br>" . $conn->error;
			}
		}
		$conn->close();
	}

	function bookCampingSpot($conn, $user_booking_id)  {
		if (isset($_POST['ticketNumber']) && isset($_POST['campingSpotId'])) {
			$ticket_number = mysqli_real_escape_string($conn, $_POST['ticketNumber']);
			$camping_spot_id = mysqli_real_escape_string($conn, $_POST['campingSpotId']);

			// Check if ticket for this user is present for the current user booking
			if ($result = $conn->query("SELECT Id FROM CampingSpotTicket WHERE TicketId = '{$ticket_number}' AND 
				UserBookingId = '{$user_booking_id}' AND CampingSpotId = '{$camping_spot_id}'")) {
	    		
	    		if($result->num_rows == 0) {
	    			// Buy ticket
	    			$sql = "INSERT INTO CampingSpotTicket(UserBookingId, TicketId, CampingSpotId) 
	    				VALUES ('{$user_booking_id}', '{$ticket_number}', '{$camping_spot_id}')";
					if ($conn->query($sql) === TRUE) {
					  echo "success";
					} else {
					  echo "Ticket № {$ticket_number} doesn't exists";
					}
	    		} else {
	    			echo "Camping spot is already reserved for ticket № {$ticket_number}";
	    		}
			}
		}
		$conn->close();
	}

	function getCurrentlyBookedCampingSpot($conn, $user_booking_id) {
		$sql = "SELECT CampingSpot.Id, CampingSpot.Capacity, CampingSpot.Price FROM CampingSpot
			INNER JOIN CampingSpotTicket ON CampingSpotTicket.CampingSpotId = CampingSpot.Id
		 	WHERE CampingSpotTicket.UserBookingId = '{$user_booking_id}' LIMIT 1";

		if($result = $conn->query($sql)) {
		  if($result->num_rows > 0) {
		    $camping_spot = "";
			while($row = mysqli_fetch_assoc($result)){
			   $camping_spot = $row;
			}
			echo json_encode($camping_spot);
		  } else {
		    echo "non-existent";
		  }
		} else {
		  echo "Error: " . $sql . "<br>" . $conn->error;
		}
		$conn->close();
	}

	if (isset($_POST['function'])) {
		getCurrentUser($conn);
	}
?>