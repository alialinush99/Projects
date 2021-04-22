<!DOCTYPE html>

<html>

<head>
	<link rel="stylesheet" type="text/css" href="./css/base-page-style.css">
	<link rel="stylesheet" type="text/css" href="./css/modals.css">
	<link rel="stylesheet" type="text/css" href="./css/profile.css">
	<link rel="stylesheet" type="text/css" href="./css/modal-event-booking-overview.css">
</head>

<body>
	<!-- include navigation -->
	<?php include('components/navigation.php') ?>

	<!-- main content -->
	<div class="profile-content">

		<!-- user details-->
		<div class="row" id="user-details">
			<!-- user info -->
			<div class="column" id="user-info-container">
				<img src="resources/avatar_icon.png">
				<div id="info-label">
					<h2 id="user-name"></h2>
					<h5 id="user-email"></h5>
				</div>
			</div>
			<!-- balance -->
			<div class="column" id="user-balance-container">
				<img src="resources/dollar.png">
				<div id="balance-label">
					<h2  id="user-balance"></h2>
					<!-- <a href="#" class="btn-orange" id="btn-add-balance">Add</a> -->
					<h5>Can be used for every next event</h5> 
				</div>
			</div>
		</div>

		<h6 class="hidden" id="selected-event"></h6>
		<!-- tickets -->
		<div id="tickets-container">
			<!-- current events -->
			<div id="current-events-container">
				<h3 class="tickets-section-title">Booked currently held events:</h3>
				<!-- events -->
			</div>

			<!-- upcoming events -->
			<div id="upcoming-events-container">
				<h3 class="tickets-section-title">Booked upcoming events:</h3>
				<!-- events -->
			</div>

			<!-- past events -->
			<div id="past-events-container">
				<h3 class="tickets-section-title">Booked past events:</h3>
				<!-- events -->
			</div>
		</div>
	</div>

	<!-- include modals -->
	<?php include('components/auth-modals/modals-auth.php') ?>
	<?php include('components/modal-top-balance.php') ?> 
	<?php include('components/modal-camping-spot-booking-overview.php') ?>
	<?php include('components/modal-book-camping.php') ?>
	<?php include('components/modal-event-booking-overview.php') ?>

	<!-- include js file -->
	<script src="https://rawgit.com/moment/moment/2.2.1/min/moment.min.js"></script>
	<script type="text/javascript" src="./js/profile.js"></script>
</body>

</html>