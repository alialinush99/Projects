var availableCampingSpot;

$('#btn-skip').click(function() {
	$('#modal-book-camping').hide();
	$('#error-msg-book-camping').empty();
});

$('#close-modal-book-camping').click(function() {
	$('#error-msg-book-camping').empty();
});


const loadAvailableCampingSpotData = () => {
	checkIfCampingSpotBookingExists(getCurrentEventId());
}

// Checks if there is an already booked camping spot from the current user
function checkIfCampingSpotBookingExists(eventId) {
	var command = "getAllCampingSpotsForBooking";
	$.ajax({
	    type: 'Post',
	    url: './db/event-booking.php',
	    data: {
	    	function: command,
	    	eventId: eventId
	    },
	    success: (result) => {
	    	if(result != "non-existent") {
	    		$('#modal-book-camping').hide();
	    		loadCampingSpotBooking();
	    	} else {
	    		$('#modal-book-camping').show();

				// Listens for changes in the "select" element values
				$('#select-camping-spot-capacity').on('change', function() {
					var campingSpotCapacity = $('#select-camping-spot-capacity').val();
					showTicketNumberFields(parseInt(campingSpotCapacity));
					getAvailableCSWithCapacity(campingSpotCapacity, getCurrentEventId());
				});

				getAvailableCSCapacities(getCurrentEventId());
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Get all available camping spot capacities
function getAvailableCSCapacities(eventId) {
	var command = "getCampingSpotCapacities";
	$.ajax({
	    type: 'Post',
	    url: './db/camping-spots.php',
	    data: {
	    	function: command,
	    	eventId: eventId
	    },
	    success: (capacities) => {
	    	if(capacities != 'non-existent') {
	    		capacitiesParsed = JSON.parse(capacities);
	    		$('#select-camping-spot-capacity').empty();
	    		$.each(capacitiesParsed, function(i, capacity) {
					$('#select-camping-spot-capacity').append( $('<option>', { value: capacity, text: parseInt(capacity)}));
					if(i == 0) {
	    				showTicketNumberFields(capacity);
	    				$('#select-camping-spot-capacity').val(capacity).trigger('change');
	    			}
				});
	    	} else {
	    		$('#modal-book-camping').hide();
	    	}	
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Get available camping spot with the selected capacity
function getAvailableCSWithCapacity(campingSpotCapacity, eventId) {
	var command = "getCampingSpotWithCapacity";
	$.ajax({
	    type: 'Post',
	    url: './db/camping-spots.php',
	    data: {
	    	function: command,
	    	eventId: eventId,
	    	capacity: campingSpotCapacity
	    },
	    success: (campingSpot) => {
	    	console.log("camping spot", campingSpot);
	    	if(campingSpot != 'non-existent') {
	    		campingSpotParsed = JSON.parse(campingSpot);
	    		availableCampingSpot = campingSpotParsed;
	    		$('#camping-spot-price').text(availableCampingSpot.Price + " €");
	    	}	
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Gets the current event id
function getCurrentEventId() {
	return window.location.search.split('id=')[1]
}

// Dynamically appends new ticket number fields based on camping spot capacity
function showTicketNumberFields(numberOfFields) {
	$('#ticket-number-container').empty();
	if(numberOfFields > 0) {
		$('#ticket-number-container').append('<label class="modal-label"><b>Ticket number/s</b></label>');
	}
	for(i = 0; i < numberOfFields; i++) {
		$('#ticket-number-container').append('<input type="text" class="ticketNumber" placeholder="Enter Ticket Number">')
	}
}

$('#btn-book-spot').click(function(event) {
	event.preventDefault();

	var ticketNumbers = getTicketNumbers();

	if(ticketNumbers.length > 0) {
		let eventId = getCurrentEventId();
		$('#error-msg-book-camping').empty();
		chargeForCampingSpot(ticketNumbers);
	}
});

// Get values from dynamic ticket number fields
function getTicketNumbers() {
	let ticketNumbers = []
	$('.ticketNumber').each(function(i, ticketNumber) {
		if(ticketNumber.value !== "") {
			ticketNumbers.push(ticketNumber.value);
		}
	});
	return ticketNumbers;
} 

// Charge user account with the ticket costs
function chargeForCampingSpot(ticketNumbers) {
	var amountToCharge = parseInt(availableCampingSpot.Price);

	// Get current balance
	var command = "getBalance";
	$.ajax({
	    type: 'Post',
	    url: './db/balance.php',
	    data: {
	    	function: command
	    },
	    success: (currentBalance) => {
        	var currentBalanceParsed = JSON.parse(currentBalance);
        	var currentBalanceAsInt = parseInt(currentBalanceParsed.Balance);
        	// Check if user has enough money to pay for the camping spot
        	// if(currentBalanceAsInt >= amountToCharge) {
        		// Update balance
        		updateUserBalance(currentBalanceAsInt - amountToCharge, ticketNumbers);
        		
        	// } else {
        	// 	$('#error-msg-book-camping').append('<h4 class="error-msg text-center">Your balance is insufficient for this purchase!</h4>');
        	// }
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Updates user balance
function updateUserBalance(newBalance, ticketNumbers) {
	$.ajax({
	    type: 'Post',
	    url: './db/balance.php',
	    data: { amount: newBalance },
	    success: () => {
	    	// Book camping spot for each email
    		$.each(ticketNumbers, function(position, ticketNumber) {
				bookCampingSpot(ticketNumber, getCurrentEventId());
			});
			loadAvailableCampingSpotData();
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Book camping spot for certain ticket number
function bookCampingSpot(ticketNumber, eventId) {
	var command = "bookCampingSpot";
	$.ajax({
	    type: 'Post',
	    url: './db/event-booking.php',
	    data: {
	    	function: command,
	    	eventId: eventId,
	    	ticketNumber: ticketNumber,
	    	campingSpotId: availableCampingSpot.Id
	    },
	    success: (event) => {
	    	if(event.startsWith("Camping") || event.startsWith("Ticket")) {
	    		$('#error-msg-book-camping').append('<h4 class="error-msg text-center">' + event + '</h4>');
	    	}
	    	if(event == "success") {
	    		$('#error-msg-book-camping').append('<h4 class="success-msg-center text-center">' +  "Camping spot has been booked for " + ticketNumber + '</h4>');
	    		updateCampingSpotStatus();
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

function updateCampingSpotStatus() {
	var command = "updateCampingSpotStatus";
	$.ajax({
	    type: 'Post',
	    url: './db/camping-spots.php',
	    data: {
	    	function: command,
	    	campingSpotId: availableCampingSpot.Id
	    },
	    success: (event) => {
	    	console.log("status changed");
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}