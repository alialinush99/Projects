var eventParsed;
var numberOfTickets;

const loadBuyTicketModal= () => {
	$('#modal-buy-ticket').show();
	getCurrentEvent(getCurrentEventId());
}

// Listens for changes in the "select" element values
$('#select-number-of-tickets').on('change', function() {
	numberOfTickets = $('#select-number-of-tickets').val();
	setOverallPriceString(numberOfTickets);
	//showEmailFields(parseInt(numberOfTickets));
});

// Sets the overall price to element
function setOverallPriceString(numberOfTickets) {
	$('#overall-bill').html(getOverallPrice(numberOfTickets) + ' €');
}

// Returns the overall price to be payed
function getOverallPrice(numberOfTickets) {
	return parseFloat(numberOfTickets) * parseFloat(eventParsed.TicketPrice);
}

// Gets the current event id
function getCurrentEventId() {
	return window.location.search.split('id=')[1]
}

// Gets the current event
function getCurrentEvent(eventId) {
	var command = "getEventById";
	$.ajax({
	    type: 'Post',
	    url: './db/events.php',
	    data: {
	    	function: command,
	    	eventId: eventId
	    },
	    success: (event) => {
        	eventParsed = JSON.parse(event);
        	// Set ticket price
        	$('#ticket-price').html(eventParsed.TicketPrice + ' €');
        	getTicketAvaialbilityModal(getCurrentEventId());
        	// Set values for 1 person
        	setOverallPriceString(1);
			showEmailFields(1);
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Dynamically appends new email fields based on number of tickets
function showEmailFields(numberOfFields) {
	$('#email-fields-container').empty();
	if(numberOfFields > 0) {
		$('#email-fields-container').append('<label for="email" class="modal-label-inline"><b>Ticket/s for: </b></label>');
	}
	for(i = 0; i < numberOfFields; i++) {
		$('#email-fields-container').append('<input type="text" class="email" placeholder="Enter Email.." list="autocompleteOff" required="requried">')
	}
}

// Buy tickets
$('#btn-buy').click(function() {
	var emails = getEmails();

	if(emails.length > 0) {
		let eventId = getCurrentEventId();
		$('#error-msg-buy-ticket').empty();
		chargeForTickets(emails);
	}
});

// Get all email fields data
function getEmails() {
	let emails = []
	$('.email').each(function(i, email) {
		if(email.value !== "") {
			emails.push(email.value);
		} else {
			alert("Email field should be filled!");
			return false;
		}
	});
	return emails;
} 

// Charge user account with the ticket costs
function chargeForTickets(emails) {
	var amountToCharge = parseInt(numberOfTickets) * parseInt(eventParsed.TicketPrice);

	// Get current balance
	var command = "getBalance";
	$.ajax({
	    type: 'Post',
	    url: './db/balance.php',
	    data: {
	    	function: command
	    },
	    success: (currentBalance) => {
	    	if(currentBalance == "not-logged-in") {
	    		$('#error-msg-buy-ticket').append('<h4 class="error-msg text-center">Please login before buying tickets!</h4>');
	    	} else {
	    		var currentBalanceParsed = JSON.parse(currentBalance);
	        	var currentBalanceAsInt = parseInt(currentBalanceParsed.Balance);
	        	// // Check if user has enough money to pay for the tickets
	        	// if(currentBalanceAsInt >= amountToCharge) {
	        		// Update balance
	        		updateBalance(currentBalanceAsInt - amountToCharge, emails);
	        		
	        	// } else {
	        	// 	$('#error-msg-buy-ticket').append('<h4 class="error-msg text-center">Your balance is insufficient for this purchase!</h4>');
	        	// }
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Updates user balance
function updateBalance(newBalance, emails) {
	$.ajax({
	    type: 'Post',
	    url: './db/balance.php',
	    data: { amount: newBalance },
	    success: () => {
	    	// Buy ticket for each email
    		for(var i = 0; i < numberOfTickets; i++) {
				buyTicket(emails[0], getCurrentEventId());
			}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

function buyTicket(email, eventId) {
	var command = "buyTickets";
	$.ajax({
	    type: 'Post',
	    url: './db/event-booking.php',
	    data: {
	    	function: command,
	    	eventId: eventId,
	    	email: email
	    },
	    success: (event) => {
	    	if(event.startsWith("Ticket")) {
	    		$('#error-msg-buy-ticket').append('<h4 class="error-msg text-center">' + event + '</h4>');
	    	}
	    	if(event == "success") {
	    		$('#modal-buy-ticket').hide();
	    		loadAvailableCampingSpotData();
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Sets the ticket availability for the current event
function getTicketAvaialbilityModal(eventId) {
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
	    		$('#ticket-availability').html(availabilityString);
	    		console.log(availabilityString);

	    		switch(availabilityString) {
	    			case "High": $('#ticket-availability').css("color","green"); break;
	    			case "Medium": $('#ticket-availability').css("color","#ee7600"); break;
	    			case "Low": $('#ticket-availability').css("color","red"); break;
	    		}
	    	}
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Returns availability string based on event ticket pool and booked tickets
function getAvailabilityString(eventTicketPool, nrOfBookedTickets) {
	eventTicketPool = parseInt(eventTicketPool);
	nrOfBookedTickets = parseInt(nrOfBookedTickets);

	if(nrOfBookedTickets <= eventTicketPool - eventTicketPool * 0.6) {
		return "High";
	}

	if(nrOfBookedTickets >= eventTicketPool - eventTicketPool * 0.6 && 
		nrOfBookedTickets <= eventTicketPool - eventTicketPool * 0.4) {
		return "Medium";
	}

	if(nrOfBookedTickets >= eventTicketPool - eventTicketPool * 0.4) {
		return "Low";
	}

}
