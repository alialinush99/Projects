<!DOCTYPE html>

<html lang="en">

<head>
	<link rel="stylesheet" type="text/css" href="./css/base-page-style.css">
	<link rel="stylesheet" type="text/css" href="./css/modals.css">
	<link rel="stylesheet" type="text/css" href="./css/event-details.css">
</head>
<body>
	<!-- include navigation -->
	<?php include('components/navigation.php') ?>

	<!-- main content -->
	<div class="event-details-content">

		<img id="event-picture" src="resources/tomorrowland.jpg"> 

		<div class="row">
			<div class="col"> 
				<button id="btn-buy-tickets" class="button">Buy tickets</button>
			</div>
		</div>

		<div class="row">
			<!-- general information -->
			<div class="col" id="general-info-section">
				<h2 class="title"> General information </h2>
				<div id="general-info-section-content"></div>
			</div>

			<!-- line up -->
			<div class="col" id="line-up-section"> 
				<h2 class="title">Line Up</h2>
			</div>
		</div>

		<div class="row" id="reviews-section">
			<!-- reviews container -->
			<div class="col">
				<h2 class="title">Reviews</h2>
				<div id="review-container"></div>
			</div>

			<!-- leave review form -->
			<div class="col">
				<form id="review-form" action="index.html" method="post">
				  <h2 class="title">Write Your Review</h2>

				  <!-- rating bar -->
				  <?php include('components/rating-bar.php') ?>

				  <!-- review text form -->
				  <div class="form-group">
				    <textarea class="form-control" rows="10" placeholder="Your Reivew" name="review" id="review"></textarea>
				    <span id="reviewInfo" class="help-block pull-right">
				      <span id="remaining">999</span> Characters remaining
				    </span>
				  </div>

				  <button id="btn-submit-review" type="button">Submit</button>
				</form>
			</div>
		</div>

		<!-- map -->
		<h2 id="title-location">Location</h2>
		<div class="details" id="event-location"></div>
		<!-- <div class="mapouter">
			<div class="gmap_canvas">
				<iframe id="gmap_canvas" src="https://maps.google.com/maps?q=fontys%20r1&t=&z=15&ie=UTF8&iwloc=&output=embed" frameborder="0" scrolling="no" marginheight="0" marginwidth="0">
				</iframe>
			</div>
		</div> -->
	</div>

	<!-- include authentication modals -->
	<?php include('components/auth-modals/modals-auth.php') ?>

	<!-- include buy ticket modal --> 
	<?php include('components/modal-buy-ticket.php') ?>

	<!-- include camping spot booking overview modal --> 
	<?php include('components/modal-camping-spot-booking-overview.php') ?>

	<!-- include book camping spot modal --> 
	<?php include('components/modal-book-camping.php') ?>

	<!-- include js file -->
	<script src="https://rawgit.com/moment/moment/2.2.1/min/moment.min.js"></script>
	<script type="text/javascript" src="./js/event-details.js"></script>
	<script type="text/javascript" src="./js/event-details-reviews.js"></script>
</body>

</html>