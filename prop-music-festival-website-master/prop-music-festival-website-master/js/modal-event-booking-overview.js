
const loadEventBookingOverview = () => {
	clearModal();
	let currentEventId = getCurrentEventId();
	getEventDetails(currentEventId);
	getEventBookingTickets(currentEventId);
	getEventBookingCampingSpotTickets(currentEventId);
	addEventBookingOverviewButtons(true);

	$('#btn-open-camping-spot-booking').click(function() {
		$('#modal-event-booking-overview').hide();
		loadAvailableCampingSpotData();
	});
}

function clearModal() {
	$('#event-booking-overview-event-details').empty();
	$('#event-booking-overview-event-tickets').empty();
	$('#event-booking-overview-cs-tickets').empty();
	$('#event-booking-overview-button-container').empty();
}

$('#event-booking-overview-button-container').on('click', '#btn-view-event-page', function() {
	window.location.href = "https://localhost/prop-music-festival-website/event-details.php?id=" + getCurrentEventId();
});


function getEventDetails(eventId) {
	var command = "getEventById";
	$.ajax({
	    type: 'Post',
	    url: './db/events.php',
	    data: {
	    	function: command,
	    	eventId: eventId
	    },
	    success: (event) => {
        	var eventParsed = JSON.parse(event);
        	addEventBasicDetails(eventParsed);
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

var addEventBasicDetails = (eventDetails) => {
	// Add event name
	$("#event-booking-overview-event-details").append(
		'<div>' +
			'<label class="modal-label-inline"><b>Event name: </b></label>' +
			'<h4 class="text-inline">' + eventDetails.Title + '</h4>' + 
		'</div>'
	);

	// Add event location
	$("#event-booking-overview-event-details").append(
		'<div>' +
			'<label class="modal-label-inline"><b>Location: </b></label>' +
			'<h4 class="text-inline">' + eventDetails.Address + '</h4>' + 
		'</div>'
	);

	// Add event duration dates
	$("#event-booking-overview-event-details").append(
		'<div>' +
			'<label class="modal-label-inline"><b>Duration: </b></label>' +
			'<h4 class="text-inline">' + parseStringToDate(eventDetails.StartDate) + " - " + parseStringToDate(eventDetails.EndDate) + '</h4>' + 
		'</div>'
	);
} 

// Parse datetime string to date
function parseStringToDate(datetime) {
	return moment(new Date(Date.parse(datetime))).format("DD/MM/YYYY");
}

function getEventBookingTickets(eventId) {
	var command = "getAllTickets";
	$.ajax({
	    type: 'Post',
	    url: './db/event-booking.php',
	    data: {
	    	function: command,
	    	eventId: eventId
	    },
	    success: (tickets) => {
	    	if(tickets != "non-existent") {
	    		var ticektsParsed = JSON.parse(tickets);
	    		addEventTickets(ticektsParsed);
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

var addEventBookingOverviewButtons = (showOptionForCampSpotBooking) => {
	if(showOptionForCampSpotBooking) {
		$('#event-booking-overview-button-container').append('<button type="button" id="btn-view-event-page" class="btn-skip">View event</button>');
		$('#event-booking-overview-button-container').append('<button type="button" id="btn-open-camping-spot-booking" class="modal-button-inline">Book camping</button>');
	} else {
		$('#event-booking-overview-button-container').append('<button type="button" id="btn-view-event-page" class="modal-button-inline">View event</button>');
	}
}

var addEventTickets = (eventTickets) => {
	// Add section title
	$('#event-booking-overview-event-tickets').append('<label class="modal-label"><b>Tickets booked: </b></label>')
	// Add each event ticket and it's owner
	$.each(eventTickets, function(i, ticket){
		$('#event-booking-overview-event-tickets').append('<h4 class="text-block"> Ticket № ' + ticket.Id + ', Owner: ' + ticket.Email + ", Price: " + ticket.TicketPrice + ' €</h4>');
	});
}

function getEventBookingCampingSpotTickets(eventId) {
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
	    		var camoingSpotTickets = JSON.parse(tickets);
	    		addCampingSpotTickets(camoingSpotTickets);
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

var addCampingSpotTickets = (campingSpotTickets) => {
	// Add section title
	$('#event-booking-overview-cs-tickets').append('<label class="modal-label"><b>Camping spot booked: </b></label>')
	// Add each camping spot ticket and it's owner
	$.each(campingSpotTickets, function(i, ticket){
		$('#event-booking-overview-cs-tickets').append('<h4 class="text-block">CS № ' + ticket.CampingSpotId +', Ticket № ' + ticket.Id + ', Owner: ' + ticket.Email + '</h4>');
	});
}