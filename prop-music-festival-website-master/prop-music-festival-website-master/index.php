<!DOCTYPE html>

<html>

<head>
	<link rel="stylesheet" type="text/css" href="./css/base-page-style.css">
	<link rel="stylesheet" type="text/css" href="./css/modals.css">
	<link rel="stylesheet" type="text/css" href="./css/index.css">
</head>

<body>
	<!-- include navigation -->
	<?php include('components/navigation.php') ?>

	<!-- main content -->
	<div class="home-landing-img-container">
		<div class="darken">
			<h1 id="heading">Music is bringing us together. <span style="color:#ee7600">Soundwave.</span></h1>
			<button class="btn-border-backgroundless" id="btn-explore">Explore</button>
		</div>
	</div>

	<div class="event-card-container" >
		<div class="event-card" id="event-card-one">
			<div class="darken">
				<div class="element">
					<img class="event-card-image" src="./resources/guitar.svg">
					<h2 class="event-card-text">Learn more</h2>
				</div>
			</div>
		</div>

		<div class="event-card" id="event-card-two">
			<div class="darken">
				<div class="element">
					<img class="event-card-image" src="./resources/disco-ball.svg">
					<h2 class="event-card-text">Learn more</h2>
				</div>
			</div>
		</div>

		<div class="event-card" id="event-card-three">
			<div class="darken">
				<div class="element">
					<img class="event-card-image" src="./resources/speakers.svg">
					<h2 class="event-card-text">Learn more</h2>
				</div>
			</div>
		</div>
	</div>

	<!-- include authentication modals -->
	<?php include('components/auth-modals/modals-auth.php') ?>

	<!-- include scripts -->
	<script type="text/javascript" src="./js/index.js"></script>
</body>

</html>