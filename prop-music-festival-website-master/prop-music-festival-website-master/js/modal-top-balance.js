var balance = 0;

$(document).ready(function() {
	getCurrentBalance();
});

$('#btn-top-balance').click(function() {
	addToBalance($('#top-up-balance-amount').val());
	$("#user-balance").text(balance + " €");
	$('#top-up-balance-amount').val("");
});

var getCurrentBalance = () => {
	var command = "getBalance";
	$.ajax({
	    type: 'Post',
	    url: './db/balance.php',
	    data: {
	    	function: command
	    },
	    success: (currentBalance) => {
        	var currentBalanceParsed = JSON.parse(currentBalance);
        	balance = currentBalanceParsed.Balance;
			$("#top-up-balance-current-balance").text(balance + " €");
	    },
	    error: (result) => {
	        console.log('error', result);
	    }
	});
}

// Function for adding amount to user's balance
var addToBalance = (amount) => {
	if(amount > 0) {
		balance = parseFloat(amount) + parseFloat(balance);
		console.log(balance);
		$.ajax({
		    type: 'Post',
		    url: './db/balance.php',
		    data: {
		    	amount: balance
		    },
		    success: () => {
		    	getCurrentBalance();
		    },
		    error: (result) => {
		        console.log('error', result);
		    }
		});
	}
}