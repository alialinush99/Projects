<div id="modal-event-booking-overview" class="modal">
	<form class="modal-content animate">
		<div class="imgcontainer">
			<span onclick="document.getElementById('modal-event-booking-overview').style.display='none'" class="close"
				title="Close Modal">&times;</span>
		</div>
		<!-- booking overview content-->
		<div class="container" id="event-booking-overview-container">
			<h2 class="title">Event Overview</h2>
			<hr>

			<!-- base event details -->
			<div id="event-booking-overview-event-details">
				<!-- basic event details are shown here -->
			</div>

			<!-- tickets -->
			<div id="event-booking-overview-event-tickets">
				<!-- event tickets are shown here -->
			</div>

			<div id="event-booking-overview-cs-tickets">
				<!-- camping spot tickets are shown here -->
			</div>
	
			<div class="error-msg text-center" id="error-msg-login">
				<!-- error messages will appear here -->
			</div>

			<!-- buttons -->
			<div class="wrapper-center" id="event-booking-overview-button-container">
				<!-- <button id="btn-skip" class="btn-skip">Skip</button> -->
			</div>
		</div>
	</form>
</div>

<!-- include javascript file -->
<script src="https://rawgit.com/moment/moment/2.2.1/min/moment.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript" src="./js/modal-event-booking-overview.js"></script>