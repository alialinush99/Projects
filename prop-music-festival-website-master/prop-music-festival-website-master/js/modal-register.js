var MIN_PASSWORD_LENGTH_REG = 6;
var REGEX_EMAIL_REG = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
var REGEX_TEXT =/^[a-zA-Z]+$/;


let allowRegistration = {
	email: false,
	password: false,
	confirmPassword: false,
	firstName: false,
	lastName: false
}

// Adding on change listeners
$('#register-email').change(() => {
	validateEmailReg($('#register-email'));
});

$('#register-password').change(() => {
	validatePasswordReg($('#register-password'));
});

$('#register-confirm-password').change(() => {
	validateConfirmPassword($('#register-confirm-password'), $('#register-password'));
});

$('#register-first-name').change(() => {
	validateFirstName($('#register-first-name'));
});

$('#register-last-name').change(() => {
	validateLastName($('#register-last-name'));
});

$('#btnregister').click(function(event) {
    event.preventDefault();
    if (checkFieldFlags()) {
        var user = {
                email: $('#register-email').val(),
                password: $('#register-password').val(),
                firstName: $('#register-first-name').val(),
                lastName: $('#register-last-name').val()
        };
        console.log(user);
        $.ajax({
            type: 'POST',
            url: './db/register.php',
            data: {
                email: user.email,
                password: user.password,
                firstName: user.firstName,
                lastName: user.lastName
            },
            success: (result) => {
                if (result === 'taken') {
                    showErrorMessage("Email already in use");
                } else if (result === 'success') {
                    // Update last login time
                    $.ajax({
                            type: 'POST',
                            url: './db/update-last-login.php',
                            data: { newLoginTime: getFormattedCurrentDateTime()},
                            success: (result) => {
                                window.location.href = "index.php";
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
        removeErrors();
    } else {
        showErrors("Please fix all errors before submiting!");
        console.log('disallow submit');
    }
});

// Function for validating an email address
var validateEmailReg = (emailField) => {
    if (REGEX_EMAIL_REG.test(emailField.val())) {
        // Email field matches the email format
        emailField.removeClass('invalid');
        removeErrors();
        allowRegistration.email = true;
    } else {
        // Email field doesn't match the email format
        emailField.addClass('invalid');
        showErrors("Email must be valid!");
        allowRegistration.email = false;
    }
}

// Function for validating a password
var validatePasswordReg = (passwordField) => {
    if (passwordField.val().length < MIN_PASSWORD_LENGTH_REG) {
        // Password field is invalid
        passwordField.addClass('invalid');
        showErrors("Password must contain at least 6 symbols!");
        allowRegistration.password = false;
    } else {
        // Password field is valid
        passwordField.removeClass('invalid');
        removeErrors();
        allowRegistration.password = true;
    }
}

// Function for validating a confirm password
var validateConfirmPassword = (passwordField, confirmPasswordField) => {
    if (confirmPasswordField.val() == passwordField.val()) {
        // Paswords match
    	passwordField.removeClass('invalid');
        removeErrors();
        allowRegistration.confirmPassword = true;
    } else {
        // Passwords don't match
        confirmPasswordField.addClass('invalid');
        showErrors("Passwords do not match!");
        allowRegistration.confirmPassword = false;
    }
}

// Function for validating first name field
var validateFirstName = (textField) => {
	if(REGEX_TEXT.test(textField.val())) {
        // Text field is valid
		textField.removeClass('invalid');
		removeErrors("");
		allowRegistration.firstName = true;
	} else {
        // Text field is invalid
		textField.addClass('invalid');
		showErrorse("First name should be correct text string");
		allowRegistration.firstName = false;
	}
}

// Function for validating last name field
var validateLastName = (textField) => {
    if(REGEX_TEXT.test(textField.val())) {
        // Text field is valid
        textField.removeClass('invalid');
        removeErrors("");
        allowRegistration.lastName = true;
    } else {
        // Text field is invalid
        textField.addClass('invalid');
        showErrors("Last name should be correct text string");
        allowRegistration.lastName = false;
    }
}


// Function for checking field flags
var checkFieldFlags = () => {
    return allowRegistration.email 
        && allowRegistration.password
        && allowRegistration.confirmPassword 
        && allowRegistration.firstName
        && allowRegistration.lastName
}

// Function for showing error messages
var showErrors = (text) => {
    $('#error-msg-register').html(text);
}

// Function for cleaning up error field
var removeErrors = () => {
    $('#error-msg-register').html("");
}

// Update last login time
function updateLastLoginTime() {
    $.ajax({
            type: 'POST',
            url: './db/update-last-login.php',
            data: {
                newLoginTime: getFormattedDate(),
            },
            success: (result) => {
                // TODO: Think of handler for success
            },
            error: (result) => {
                console.log('error', result);
            }
        })
}

// Get formatted local time (mysql standard)
function getFormattedCurrentDateTime() {
    return new Date().toJSON().slice(0, 19).replace('T', ' ');
}