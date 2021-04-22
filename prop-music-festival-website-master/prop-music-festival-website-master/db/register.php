<?php
  session_start();
  // make connection to db
  include('config.php');

  $email = mysqli_real_escape_string($conn, $_POST['email']);
  $password = mysqli_real_escape_string($conn, $_POST['password']);
  $first_name = mysqli_real_escape_string($conn, $_POST['firstName']);
  $last_name = mysqli_real_escape_string($conn, $_POST['lastName']);

  $check_existing_email = "SELECT UserId FROM User_ WHERE Email = '{$email}'";
  if ($result = $conn->query($check_existing_email)) {
    if($result->num_rows == 0) {
      $sql = "INSERT INTO User_ (Email, Password_, FirstName, LastName, Balance, Role) 
      VALUES ('{$email}', '{$password}', '{$first_name}', '{$last_name}', 0, 'visitor')";
      if ($conn->query($sql) === TRUE) {
          $_SESSION['email'] = $email;
          echo "success";
      } else {
          echo "Error: " . $sql . "<br>" . $conn->error;
      }
    } else {
      echo "taken";
    }
  } else {
    echo "Error: " . $check_existing_email . "<br>" . $conn->error;
  }
  $conn->close();
?>
