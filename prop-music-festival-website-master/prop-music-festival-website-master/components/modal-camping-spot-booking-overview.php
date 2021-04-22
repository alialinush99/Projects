<div id="modal-camping-overview" class="modal">
	<form class="modal-content animate">
		<div class="imgcontainer">
			<span onclick="document.getElementById('modal-camping-overview').style.display='none'" class="close"
				title="Close Modal" id="close-modal-book-camping">&times;</span>
		</div>

		<div class="container" id="buy-ticket-container">
			<h2 class="title">Camping Spot Overview</h2>
			<hr>
			<!-- Camping spot number -->
			<div>
				<label class="modal-label-inline"><b>Camping spot number: </b></label>
				<h4 style="display: inline-block;" id="camping-spot-number"></h4>
			</div>

			<!-- camping spot capacity -->
			<div>
				<label class="modal-label-inline"><b>Capacity: </b></label>
				<h4 style="display: inline-block;" id="camping-spot-capacity"></h4>
			</div>

			<!-- camping spot price -->
			<div>
				<label class="modal-label-inline"><b>Cost price: </b></label>
				<h4 style="display: inline-block;" id="camping-spot-cost"></h4>
			</div>

			<!-- booked ticket numbers for a camping spot -->
			<div id="camping-spot-ticket-container"></div>

			<label class="modal-label-inline"><b>Places left in camping spot: </b></label>
				<h4 style="display: inline-block;" id="camping-spot-places-left"></h4>

			<!-- ticket numbers to add for a camping spot -->
			<div id="camping-spot-ticket-input-container"></div>
	
			<div class="error-msg text-center" id="error-msg-camping-overview">
				<!-- error messages will appear here -->
			</div>

			<!-- buttons -->
			<div class="wrapper-center">
				<button type="button" id="btn-camping-overview-book-spot" class="modal-button-inline">Book</button>
			</div>
		</div>
	</form>
</div>

<!-- include javascript file -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript" src="./js/modal-camping-spot-booking-overview.js"></script>