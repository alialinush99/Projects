<?php
  session_start();
  // make connection to db
  include('config.php');
  include('common.php');

  $email = $_SESSION['email'];
  $new_login_time = mysqli_real_escape_string($conn, $_POST['newLoginTime']);

 //if(userExists($conn, $email)) {
  $sql = "UPDATE User_ SET LastLogin = '{$new_login_time}' WHERE Email = '{$email}'";
  if ($conn->query($sql) === TRUE) {
      echo "success";
  } else {
      echo "Error while updating last login" . $conn->error;
    // }
  // } else {
  //   echo "Error: user doesn't exist" . $conn->error;
  }
  $conn->close();
?>