<!DOCTYPE html>

<html>

<head>
	<link rel="stylesheet" type="text/css" href="./css/modals.css">
	<link rel="stylesheet" type="text/css" href="./css/base-page-style.css">
	<link rel="stylesheet" type="text/css" href="./css/contact.css">
</head>

<body>
	<!-- include navigation -->
	<?php include('components/navigation.php') ?>

	<!-- main content -->
	<div class="contact-content">
		<div class="darken">
			<div class="landing-element">
				<h1>Want to contact us?</h1>
				<h2>Do not hesitate!</h2>
			</div>
		</div>
	</div>
	<div class="contact-information-container">
		<!-- contact form -->
	  	<form id="cf">
	  		<h2 class="title">Send an email</h2>
			<input type="text" class="cf-field" id="input-name" placeholder="Name">
			<input type="email" class="cf-field" id="input-email" placeholder="Email address">
			<input type="text" class="cf-field" id="input-subject" placeholder="Subject">
			<input type="text" class="cf-field" id="input-message" placeholder="Message">
		  	<input type="submit" value="Submit" id="input-submit">
		</form>

		<!-- google maps -->
		<div class="mapouter">
			<div class="gmap_canvas">
				<iframe width="400" height="300" id="gmap_canvas" src="https://maps.google.com/maps?q=fontys%20r1&t=&z=15&ie=UTF8&iwloc=&output=embed" frameborder="0" scrolling="no" marginheight="0" marginwidth="0">
				</iframe>
			</div>
		</div>
	</div>

	<!-- include authentication modals -->
	<?php include('components/auth-modals/modals-auth.php') ?>
</body>

</html>