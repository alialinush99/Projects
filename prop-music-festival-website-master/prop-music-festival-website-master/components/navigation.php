<?php
  session_start();
  $activePage = htmlspecialchars( isset($_GET["active"]) ? $_GET["active"] : 0 ) ;
?>
<header>
  <nav>
    <a href="index.php?active=1" <?php if($activePage == 1) echo "class=\"active\""; ?>> Home </a>
    <a href="events.php?active=2" <?php if($activePage == 2) echo "class=\"active\""; ?>> Events </a>
    <a href="contact.php?active=3" <?php if($activePage == 3) echo "class=\"active\""; ?>> Contact </a>
    <?php 
      if(isset($_SESSION['email'])) {
         echo "<a href=\"profile.php?active=4\"" . ($activePage == 4 ? "class=\"active\"" : "") . "> Profile </a>";
      }
    ?>
  </nav>
  <?php
    if(isset($_SESSION['email'])) {
      echo "<a href=\"db/logout.php\" class=\"btn-logout\" id=\"logout\"> Logout </a>";
    } else {
      echo "<a href=\"#\" class=\"btn-login\" id=\"login\"> Login </a>";
    }
  ?>
</header>