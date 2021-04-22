<div id="modal-book-camping" class="modal">
	<form class="modal-content animate">
		<div class="imgcontainer">
			<span onclick="document.getElementById('modal-book-camping').style.display='none'" class="close"
				title="Close Modal" id="close-modal-book-camping">&times;</span>
		</div>

		<div class="container" id="buy-ticket-container">
			<h2 class="title">Book Camping Spot</h2>
			<hr>
			<!-- price -->
			<div>
				<label class="modal-label-inline"><b>Camping spot price: </b></label>
				<h4 style="display: inline-block;" id="camping-spot-price"></h4>
			</div>

			<!-- camping spot capacity -->
			<div>
				<label class="modal-label-inline"><b>Choose spot capacity: </b></label>
				<select id="select-camping-spot-capacity"></select>
			</div>

			<!-- enter ticket number/s for a camping spot -->
			<div id="ticket-number-container"></div>
	
			<div class="error-msg text-center" id="error-msg-book-camping">
				<!-- error messages will appear here -->
			</div>

			<!-- buttons -->
			<div class="wrapper-center">
				<button id="btn-skip" class="btn-skip">Skip</button>
				<button id="btn-book-spot" class="modal-button-inline">Book</button>
			</div>

			<!-- footer -->
			<div class="text-center">You can always come back later!</div>
		</div>
	</form>
</div>

<!-- include javascript file -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript" src="./js/auth-modals.js"></script>
<script type="text/javascript" src="./js/modal-book-camping.js"></script>
