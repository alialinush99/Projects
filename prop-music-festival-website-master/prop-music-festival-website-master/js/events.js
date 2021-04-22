var numberOfUpcomingEvents = 0;
var numberOfPastEvents = 0;
var numberOfCurrentEvents = 0;

$(document).ready(function() {
	getAllEvents();

	$(".events-content").on("click", ".btn-learn-more", function(){
		window.location.href = './event-details.php?id=' + this.id;
	});
});

// Get all events
var getAllEvents = () => {
	var command = "getAllEvents";
	$.ajax({
	    type: 'Post',
	    url: './db/events.php',
	    data: {
	    	function: command
	    },
	    success: (events) => {
	    	if(events.startsWith("Error")) {
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

// Add event card to upcoming events
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
    console.log(event);
	return '<div class="event-container">' +
			'<div class="darken">' +
				'<h1 class="event-name">' + event.Title + '</h1>' +
				'<h3 class="event-dates">' + parseStringToDate(event.StartDate) + ' - ' + parseStringToDate(event.EndDate) + '</h3>' +
				'<h3 class="event-location">' + event.Address + '</h3>' +
				'<a href="#" class="btn-learn-more" id="' + event.EventId + '">View</a>' +
			'</div>' +
		'</div>'
}

// Parse datetime string to date
function parseStringToDate(datetime) {
	return moment(new Date(Date.parse(datetime))).format("DD/MM/YYYY");
}

// Shows placeholder string when current events are missing
function showPlaceholderForCurrent() {
	$("#current-events-container").append('<h4 class="text-placeholder">No events currently held<h4>');
}

// Shows placeholder string when upcoming events are missing
function showPlaceholderForUpcoming() {
	$("#upcoming-events-container").append('<h4 class="text-placeholder">No upcoming events currently<h4>');
}

// Shows placeholder string when past events are missing
function showPlaceholderForPast() {
	$("#past-events-container").append('<h4 class="text-placeholder">No past events available<h4>');
}