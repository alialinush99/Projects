<div id="modal-buy-ticket" class="modal">
	<form class="modal-content animate">
		<div class="imgcontainer">
			<span onclick="document.getElementById('modal-buy-ticket').style.display='none'" class="close"
				title="Close Modal">&times;</span>
		</div>

		<div class="container" id="buy-ticket-container">
			<h2 class="title">Buy Tickets</h2>
			<hr>
			
			<!-- price -->
			<div>
				<label class="modal-label-inline"><b>Ticket price: </b></label>
				<h4 style="display: inline-block;" id="ticket-price"></h4>
			</div>

			<!-- ticket availability -->
			<div>
				<label class="modal-label-inline"><b>Ticket availability: </b></label>
				<h4 style="display: inline-block;" id="ticket-availability"></h4>
			</div>

			<!-- number of ticekts -->
			<div>
				<label for="email" class="modal-label-inline"><b>Select number of ticekts: </b></label>
				<select id="select-number-of-tickets">
					<option value="1">1</option>
			        <option value="2">2</option>
			        <option value="3">3</option>
			        <option value="4">4</option>
			        <option value="5">5</option>
				</select>
			</div>

			<!-- overall bill -->
			<div>
				<label class="modal-label-inline"><b>Overall price: </b></label>
				<h4 id="overall-bill" style="display: inline-block;">0 €</h4>
			</div>

			<div id="email-fields-container"></div>
	
			<div class="error-msg text-center" id="error-msg-buy-ticket">
				<!-- error messages will appear here -->
			</div>

			<!-- buttons -->
			<div>
				<button type="button" id="btn-buy" class="modal-button-block">Buy</button>
			</div>
		</div>
	</form>
</div>

<!-- include javascript file -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript" src="./js/modal-buy-ticket.js"></script>
