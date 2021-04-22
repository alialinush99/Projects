<!DOCTYPE html>

<html>

<head>
	<link rel="stylesheet" type="text/css" href="./css/modals.css">
	<link rel="stylesheet" type="text/css" href="./css/base-page-style.css">
	<link rel="stylesheet" type="text/css" href="./css/events.css">
</head>

<body>
	<!-- include navigation -->
	<?php include('components/navigation.php') ?>

	<!-- main content -->
	<div class="events-content">

		<!-- current events -->
		<div id="current-events-container">
			<h3 class="section-title">Current events:</h3>
			<!-- events -->
		</div>

		<!-- upcoming events -->
		<div id="upcoming-events-container">
			<h3 class="section-title">Upcoming events:</h3>
			<!-- events -->
		</div>

		<!-- past events -->
		<div id="past-events-container">
			<h3 class="section-title">Past events:</h3>
			<!-- events -->
		</div>
	</div>


	<!-- include authentication modals -->
	<?php include('components/auth-modals/modals-auth.php') ?>

	<!-- include scripts -->
	<script src="https://rawgit.com/moment/moment/2.2.1/min/moment.min.js"></script>
	<script type="text/javascript" src="./js/events.js"></script>
</body>

</html>