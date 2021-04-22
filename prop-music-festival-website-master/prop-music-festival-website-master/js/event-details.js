var isLoggedIn = false;

$(document).ready(function() {
	getEvent(getEventId());
	getPerformers(getEventId());
	checkIfUserIsLoggedIn();
});

// Get current event id from url
function getEventId() {
	return window.location.search.split('id=')[1]
}

// Get current event
function getEvent(eventId) {
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
        	loadGeneralInformation(eventParsed);
        	getTicketAvaialbility(eventParsed.EventId);
        	loadReviewSection(eventParsed);
        	disableBuyButtonIfPastEvent(eventParsed);
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Loads the general information about the event
function loadGeneralInformation(event) {
	$('#general-info-section-content').append('<div class="details"> Event name: ' + event.Title + '</div>');
	$('#general-info-section-content').append('<div class="details"> Duration: ' + parseStringToDate(event.StartDate) + ' - ' + parseStringToDate(event.EndDate) + '</div>');
	if(!eventHasPassed(event)) {
			$('#general-info-section-content').append('<div class="details" id="ticket-availability-container">Ticket availability: </div>');
	}
	$('#general-info-section-content').append('<div class="details"> Description: ' + event.Description + '</div>');
	$('#event-location').html("Event location: <b>" + event.Address + "</b>");
}

// Sets the ticket availability for the current event
function getTicketAvaialbility(eventId) {
	var command = "getEventTicketAvailability";
	$.ajax({
	    type: 'Post',
	    url: './db/events.php',
	    data: {
	    	function: command,
	    	eventId: eventId,
	    },
	    success: (eventAvailability) => {
	    	if(eventAvailability != "non-existent") {
	    		var eventAvailabilityParsed = JSON.parse(eventAvailability);

	    		var availabilityString = getAvailabilityString(eventAvailabilityParsed.TicketPool, eventAvailabilityParsed.BookedTickets)
	    		$('#ticket-availability-container').append('<div style="display:inline-block" id="ticket-availability-field">' + availabilityString + '</div>');

	    		switch(availabilityString) {
	    			case "High": $('#ticket-availability-field').css("color","green"); break;
	    			case "Medium": $('#ticket-availability-field').css("color","#ee7600"); break;
	    			case "Low": $('#ticket-availability-field').css("color","red"); break;
	    		}
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Loads review section
function loadReviewSection(event) {
	if(eventIsInTheFuture(event)) {
		$('#reviews-section').hide();
	}
}

function checkIfUserIsLoggedIn() {
	var command = "isUserLoggedIn";
	$.ajax({
	    type: 'Post',
	    url: './db/common.php',
	    data: {
	    	function: command
	    },
	    success: (result) => {
        	if(result == "not-logged-in") {
        		isLoggedIn = false;
        		$('#btn-submit-review').css("background", "#878787");
        		$('#btn-submit-review').attr("disabled", true);
        		$('#review-form').append('<h4 style="font-weight: normal">Login to enable submission of reviews</h4>')
        	} else {
        		isLoggedIn = true;
        	}

        	setButtonBuyTicketClickListener();
	    },
	    error: (result) => { console.log('error', result); }
	});
}

function setButtonBuyTicketClickListener() {
	if(isLoggedIn) {
		$('#btn-buy-tickets').click(function() {
			loadBuyTicketModal();
		});
	} else {
		$('#btn-buy-tickets').click(function() {
			alert("Please login first in order to buy tickets for this event");
		});
	}
}

function disableBuyButtonIfPastEvent(event) {
	if(eventHasPassed(event)) {
		$('#btn-buy-tickets').attr("disabled", true);
		$('#btn-buy-tickets').css("background", "#878787");
	}
}

function eventHasPassed(event) {
	var now = new Date();
	let eventDateTime = new Date(Date.parse(event.EndDate));
	// Check if event has passed
	if(eventDateTime.getTime() <= now.getTime()) {
		return true;
	}
	return false;
}

function eventIsInTheFuture(event) {
	var now = new Date();
	let eventDateTime = new Date(Date.parse(event.StartDate));
	// Check if event has passed
	if(eventDateTime.getTime() > now.getTime()) {
		return true;
	}
	return false;
}

// Parse datetime string to date
function parseStringToDate(datetime) {
	return moment(new Date(Date.parse(datetime))).format("DD/MM/YYYY");
}

function getPerformers(eventId) {
	$.ajax({
	    type: 'Post',
	    url: './db/performers.php',
	    data: {
	    	eventId: eventId
	    },
	    success: (performers) => {
        	var performersParsed = JSON.parse(performers);
        	loadPerformersForEvent(performersParsed);
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

function loadPerformersForEvent(performers) {
	$('#line-up-section').append('<ul>');
	$.each(performers, function(index, performer) {
		$('#line-up-section').append('<li class="event-performer">' + performer.Artist + '</li>');
	});
	$('#line-up-section').append('</ul>');
}