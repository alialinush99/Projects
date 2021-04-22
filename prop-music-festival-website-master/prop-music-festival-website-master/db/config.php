<?php
  // Database connection properties
  $servername = "studmysql01.fhict.local";
  $username = "dbi401372";
  $password = "Xsy2X]XjdL";
  $db = "dbi401372";

  // Create connection
  $conn = new mysqli($servername, $username, $password, $db) or die($conn);

  // Check connection
  if ($conn->connect_error) {
      die("Connection failed: " . $conn->connect_error);
  }
?>
