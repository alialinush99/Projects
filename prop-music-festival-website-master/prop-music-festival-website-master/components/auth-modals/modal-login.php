<div id="modal-login" class="modal">
	<form class="modal-content animate">
		<div class="imgcontainer">
			<span onclick="document.getElementById('modal-login').style.display='none'" class="close"
				title="Close Modal">&times;</span>
		</div>

		<div class="container" id="login-container">
			<h2 class="title">Login</h2>
			<hr>
			<!-- user data input fields -->
			<div>
				<label for="email" class="modal-label"><b>Email</b></label>
				<input type="text" placeholder="Enter Email" name="email" autocomplete="email" required id="login-email">
			</div>
			<div>
				<label for="psw" class="modal-label"><b>Password</b></label>
				<input type="password" placeholder="Enter Password" name="password" autocomplete="current-password" required id="login-password">
			</div>
			<div class="error-msg text-center" id="error-msg-login">
				<!-- error messages will appear here -->
			</div>
			<div>
				<button id="btnlogin" class="modal-button-block">Login</button>
			</div>
			<div class="text-center">
				Don't have account yet? <span onclick="hideAndOpen('login', 'register')"><a href="#"> Create one!</a></span>
			</div>
		</div>
		<div class="success-msg" id="success-login">
			<div class="checkmark">&#10003;</div>
			<div class="subtext">Logged in successfuly!</div>
	</div>
	</form>
</div>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script type="text/javascript" src="./js/modal-login.js"></script>