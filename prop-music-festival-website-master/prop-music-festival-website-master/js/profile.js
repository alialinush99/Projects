var numberOfUpcomingEvents = 0;
var numberOfPastEvents = 0;
var numberOfCurrentEvents = 0;

// Show top balance modal when the button is clicked
$('#btn-add-balance').click(function() {
	$('#modal-top-balance').show();
});

$('#tickets-container').on('click', ".btn-event-details" , function(e) {
	let eventId = $(this).parent().find(".hidden").text();
	window.history.pushState({}, null, 'https://localhost/prop-music-festival-website/profile.php?active=4&id=' + eventId);
	$('#modal-event-booking-overview').show();
	loadEventBookingOverview();
});

$(document).ready(function() {
	setUserData();
});

// Function for setting user information fields
var setUserData = () => {
	$.ajax({
	    type: 'Get',
	    url: './db/user-data.php',
	    success: (result) => {
	        if (result === 'non-existent') {
	            // Todo implement
	        } else { 	
	        	var parsedResult = JSON.parse(result);
	        	$.each(parsedResult, function(i, user) {
				    $("#user-name").text(user.FirstName +  " " + user.LastName);
		            $("#user-email").text(user.Email);
		            $("#user-balance").text('Balance: ' + user.Balance + ' €');
		            getUserBookedEvents(user.UserId);
				});
	        }
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}


// EVENTS
function getUserBookedEvents(userId) {
	var command = "getAllBookedEvents";
	$.ajax({
	    type: 'Post',
	    url: './db/event-booking.php',
	    data: {
	    	function: command
	    },
	    success: (events) => {
	    	if(events.startsWith("Error") || events == "non-existent") {
	    		showPlaceholderForUpcoming();
	    		showPlaceholderForPast();
	    	} else {
	    		var eventsParsed = JSON.parse(events);
	        	var now = new Date();
	        	// Get each event
	        	$.each(eventsParsed, function(index, event) {
	        		let eventDateTime = new Date(Date.parse(event.EndDate));
	        		// Check if event is upcoming/current or past
	        		if(eventDateTime.getTime() > now.getTime()) {
	        			if(new Date(Date.parse(event.StartDate)).getTime() < now.getTime()) {
	        				numberOfCurrentEvents++;
	        				addCurrentEvents(event);
	        			} else {
	        				numberOfUpcomingEvents++;
	        				addUpcomingEvents(event);
	        			}
	        		} else {
	        			numberOfPastEvents++;
	        			addPastEvents(event);
	        		}
	        	});

	        	if(numberOfCurrentEvents == 0) {
	        		showPlaceholderForCurrent();
	        	}

	        	if(numberOfUpcomingEvents == 0) {
	        		showPlaceholderForUpcoming();
	        	}

	        	if(numberOfPastEvents == 0) {
	        		showPlaceholderForPast();
	        	}
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Add event card to current events
function addCurrentEvents(event) {
	$("#current-events-container").append(getEventCard(event));
}

// Add event card to upcoming events
function addUpcomingEvents(event) {
	$("#upcoming-events-container").append(getEventCard(event));
}

// Add event card to past events
function addPastEvents(event) {
	$("#past-events-container").append(getEventCard(event));
}

// Create event card
function getEventCard(event) {
	return '<div class="event-container">' +
			'<div class="darken">' +
				'<h1 class="event-name">' + event.Title + '</h1>' +
				'<h3 class="event-dates">' + parseStringToDate(event.StartDate) + ' - ' + parseStringToDate(event.EndDate) + '</h3>' +
				'<h3 class="event-location">' + event.Address + '</h3>' +
				'<h3 class="hidden">' + event.EventId + '</h3>' +
				'<button type="button" class="btn-event-details" id="btn-event-details">Details</a>' +
			'</div>' +
		'</div>'
}

// Parse datetime string to date
function parseStringToDate(datetime) {
	return moment(new Date(Date.parse(datetime))).format("DD/MM/YYYY");
}

// Shows placeholder string when current events are missing
function showPlaceholderForCurrent() {
	$("#current-events-container").append('<h4 class="text-placeholder">You haven\'t booked any currently held events<h4>');
}

// Shows placeholder string when upcoming events are missing
function showPlaceholderForUpcoming() {
	$("#upcoming-events-container").append('<h4 class="text-placeholder">You haven\'t booked any upcoming events currently<h4>');
}

// Shows placeholder string when past events are missing
function showPlaceholderForPast() {
	$("#past-events-container").append('<h4 class="text-placeholder">You haven\'t booked any previous events<h4>');
}