var campingSpotCapacity;
var campingPlacesLeft;
var campingSpotGlobal;

let loadCampingSpotBooking = () => {
	$('#error-msg-camping-overview').empty();
	$('#modal-camping-overview').show();
	var eventId = getCurrentEventId();
	getBookedCampingSpot(eventId);
}

// Gets the current event id
function getCurrentEventId() {
	return window.location.search.split('id=')[1]
}

function getBookedCampingSpot(eventId) {
	var command = "getCurrentlyBookedCampingSpot";
	$.ajax({
	    type: 'Post',
	    url: './db/event-booking.php',
	    data: {
	    	function: command,
	    	eventId: eventId
	    },
	    success: (camping) => {
	    	if(camping != "non-existent") {
	    		campingSpotGlobal = JSON.parse(camping);
	    		campingSpotCapacity = campingSpotGlobal.Capacity;
	    		loadCampingSpotDetails(campingSpotGlobal);
	    		getCampingSpotTickets(eventId);
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

function loadCampingSpotDetails(campingSpot) {
	$('#camping-spot-number').text(campingSpot.Id);
	$('#camping-spot-capacity').text(campingSpot.Capacity);
	$('#camping-spot-cost').text(campingSpot.Price + " €");	
}

function getCampingSpotTickets(eventId) {
	var command = "getAllCampingSpotsForBooking";
	$.ajax({
	    type: 'Post',
	    url: './db/event-booking.php',
	    data: {
	    	function: command,
	    	eventId: eventId
	    },
	    success: (tickets) => {
	    	if(tickets != "non-existent") {
	    		var campingSpotTickets = JSON.parse(tickets);
	    		loadCampingSpotTickets(campingSpotTickets);
	    		loadNumberOfPlacesLeft(campingSpotCapacity - campingSpotTickets.length);
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}
   
function loadCampingSpotTickets(campingSpotTickets) {
	$('#camping-spot-ticket-container').empty();
	// Add section title
	$('#camping-spot-ticket-container').append('<label class="modal-label"><b>Camping spot booked for: </b></label>')
	// Add each camping spot ticket and it's owner
	$.each(campingSpotTickets, function(i, ticket){
		$('#camping-spot-ticket-container').append('<h4 class="text-block">CS № ' + ticket.CampingSpotId +', Ticket № ' + ticket.Id + ', Owner: ' + ticket.Email + '</h4>');
	});
}

function loadNumberOfPlacesLeft(placesLeft) {
	campingPlacesLeft = placesLeft;
	if(placesLeft === 0) {
		$('#btn-camping-overview-book-spot').html('Close');
	}

	$('#camping-spot-places-left').text(placesLeft);
	addTicketNumberInputFields(placesLeft)
}

// Dynamically appends new ticket number fields based on camping spot capacity
function addTicketNumberInputFields(numberOfFields) {
	$('#camping-spot-ticket-input-container').empty();
	if(numberOfFields > 0) {
		$('#camping-spot-ticket-input-container').append('<label class="modal-label"><b>Ticket number/s</b></label>');
	}
	for(i = 0; i < numberOfFields; i++) {
		$('#camping-spot-ticket-input-container').append('<input type="text" class="ticketNumbers" placeholder="Enter Ticket Number">')
	}
}

$('#btn-camping-overview-book-spot').click(function(e) {
	if(campingPlacesLeft == 0) {
		$('#modal-camping-overview').hide();
	} else {
		bookTicketNumbersToCamping();
	}
});

function bookTicketNumbersToCamping() {
	var ticketNumbers = getVisitorTicketNumbers();

	if(ticketNumbers.length > 0) {
		let eventId = getCurrentEventId();
		$('#error-msg-camping-overview').empty();
		$.each(ticketNumbers, function(position, ticketNumber) {
			bookCampingSpotForTicketNumber(ticketNumber, eventId);
		});
	}
}

// Get values from dynamic ticket number fields
function getVisitorTicketNumbers() {
	let ticketNumbers = []
	$('.ticketNumbers').each(function(i, ticketNumber) {
		if(ticketNumber.value !== "") {
			ticketNumbers.push(ticketNumber.value);
		}
	});
	return ticketNumbers;
} 

// Book camping spot for certain ticket number
function bookCampingSpotForTicketNumber(ticketNumber, eventId) {
	var command = "bookCampingSpot";
	$.ajax({
	    type: 'Post',
	    url: './db/event-booking.php',
	    data: {
	    	function: command,
	    	eventId: eventId,
	    	ticketNumber: ticketNumber,
	    	campingSpotId: campingSpotGlobal.Id
	    },
	    success: (event) => {
	    	if(event.startsWith("Camping") || event.startsWith("Ticket")) {
	    		$('#error-msg-camping-overview').append('<h4 class="error-msg text-center">' + event + '</h4>');
	    	}
	    	if(event == "success") {
	    		$('#error-msg-camping-overview').append('<h4 class="success-msg-center text-center">' +  "Camping spot booked for " + ticketNumber + '</h4>');
	    	}
	    	getBookedCampingSpot(getCurrentEventId());
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}
