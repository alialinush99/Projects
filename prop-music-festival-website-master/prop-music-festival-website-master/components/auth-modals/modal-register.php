<div id="modal-register" class="modal">
	<form class="modal-content animate">
		<div class="imgcontainer">
			<span onclick="document.getElementById('modal-register').style.display='none'" class="close"
				title="Close Modal">&times;</span>
		</div>

		<div class="container">
			<h2 class="title">Register</h2>
			<hr>
			<!-- user data input fields -->
			<div>
				<label for="email" class="modal-label"><b>Email</b></label>
				<input type="text" placeholder="Enter Email" name="email" autocomplete="email" required id="register-email">
			</div>
			<div>
				<label for="fname" class="modal-label"><b>First Name</b></label>
				<input type="text" placeholder="Enter Your First Name" name="fname" autocomplete="first-name" required id="register-first-name">
			</div>
			<div>
				<label for="lname" class="modal-label"><b>Last Name</b></label>
				<input type="text" placeholder="Enter Your Last Name" name="lname" autocomplete="last-name" required id="register-last-name">
			</div>
			<div>
				<label for="password" class="modal-label"><b>Password</b></label>
				<input type="password" placeholder="Enter Password" name="password" autocomplete="new-password" required id="register-password">
			</div>
			<div>
				<label for="confirm-password" class="modal-label"><b>Confirm Password</b></label>
				<input type="password" placeholder="Confirm Password" name="confirm-password" autocomplete="new-password" required id="register-confirm-password">
			</div>
			<div class="error-msg text-center" id="error-msg-register">
				<!-- error messages will appear here -->
			</div>
			<div>
				<button id="btnregister" class="modal-button-block">Register</button>
			</div>
			<div class="text-center">
				Have account already? <span onclick="hideAndOpen('register', 'login')"><a href="#"> Login!</a></span>
			</div>
		</div>
	</form>
</div>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript" src="./js/modal-register.js"></script>