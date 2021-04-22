var REGEX_EMAIL = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
var MIN_PASSWORD_LENGTH = 6;

let allowLogin = {
	email: false,
	password: false
};

// Adding on change listeners
$('#login-email').change(() => {
	validateEmail($('#login-email'));
});

$('#login-password').change(() => {
	validatePassword($('#login-password'));
});

// Function for logging in the user
$('#btnlogin').click(function(event) {
    event.preventDefault();
    console.log(allowLogin);
    if (allowLogin.email && allowLogin.password) {
        var loginInfo = {
            email: $('#login-email').val(),
            password: $('#login-password').val()
        }
        $.ajax({
            type: 'POST',
            url: './db/login.php',
            data: {
                email: loginInfo.email,
                password: loginInfo.password,
            },
            success: (result) => {
                if (result === 'non-existent') {
                    showErrorMessage("Email or password is incorrect");
                } else if (result === 'logged') {

                    // Update last login time
                    $.ajax({
                            type: 'POST',
                            url: './db/update-last-login.php',
                            data: { newLoginTime: getFormattedCurrentDateTime()},
                            success: (result) => {
                                console.log('success: ', result);

                                // Show logged in view
                                $('#login-container').animate({ opacity: '0' }, () => {
                                    $('#login-container').hide();
                                });
                                $('#success-login').show();
                                $('#success-login').animate({ opacity: 1 }, () => {
                                    setTimeout(() => { window.location.href = 'index.php' }, 1000);
                                });
                            },
                            error: (result) => {
                                console.log('error', result);
                            }
                        });
                }

            },
            error: (result) => {
                console.log('error', result);
            }
        })
        removeErrorMessages();
    } else {
        showErrorMessage("Please fix all errors before submiting!");
        console.log('disallow submit');
    }
});

// Function for validating an email address
var validateEmail = (emailField) => {
    if (REGEX_EMAIL.test(emailField.val())) {
        // Email field matches an email format
        emailField.removeClass('invalid');
        removeErrorMessages();
        allowLogin.email = true;
    } else {
        // Email field doesn't match an email format
        emailField.addClass('invalid');
        showErrorMessage("Email must be valid!");
        allowLogin.email = false;
    }
}

// Function for validating a password
var validatePassword = (passwordField) => {
    if (passwordField.val().length < MIN_PASSWORD_LENGTH) {
        // Password is invalid
        passwordField.addClass('invalid');
        showErrorMessage("Password must contain at least 6 symbols!");
        allowLogin.password = false;
    } else {
        // Password is valid
        passwordField.removeClass('invalid');
        removeErrorMessages();
        allowLogin.password = true;
    }
}

// Function for showing error messages
var showErrorMessage = (text) => {
    $('#error-msg-login').html(text);
}

// Function for cleaning up error field
var removeErrorMessages = () => {
    $('#error-msg-login').html("");
}

// Get formatted local time (mysql standard)
function getFormattedCurrentDateTime() {
    return new Date().toJSON().slice(0, 19).replace('T', ' ');
}