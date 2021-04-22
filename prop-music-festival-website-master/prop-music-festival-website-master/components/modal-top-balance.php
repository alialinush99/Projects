<div id="modal-top-balance" class="modal">
	<form class="modal-content animate">
		<div class="imgcontainer">
			<span onclick="document.getElementById('modal-top-balance').style.display='none'" class="close"
				title="Close Modal">&times;</span>
		</div>
		<!-- top up balance content-->
		<div class="container" id="top-balance-container">
			<h2 class="title">Top Up Balance</h2>
			<hr>
			<!-- current balance -->
			<div>
				<label class="modal-label-inline"><b>Current balance: </b></label>
				<h4 class="text-inline" id="top-up-balance-current-balance"></h4>
			</div>

			<!-- amount to add -->
			<label class="modal-label"><b>Amount: </b></label>
			<input type="number" min="0.00" step="0.01" placeholder="Enter your amount here" required id="top-up-balance-amount">
	
			<div class="error-msg text-center" id="error-msg-login">
				<!-- error messages will appear here -->
			</div>

			<!-- buttons -->
			<div>
				<button type="button" id="btn-top-balance" class="modal-button-block">Add to balance</button>
			</div>
		</div>
	</form>
</div>

<!-- include javascript file -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript" src="./js/modal-top-balance.js"></script>