// Get the modals
var loginModal = document.getElementById('modal-login');
var registerModal = document.getElementById('modal-register');
var darkenBackgroundLogin = document.getElementById('modal-bg-login');
var darkenBackgroundRegister = document.getElementById('modal-bg-register');
$('#success-login').hide();

// When the user clicks anywhere outside of the modals, close them
window.onclick = function (event) {
    if (event.target === loginModal) {
        loginModal.style.display = "none";
    }
    if (event.target === registerModal) {
        registerModal.style.display = "none";
    }
}

const hideAndOpen = (oldModalStr, newModalStr) => {
    let oldModal = oldModalStr === 'login' ? loginModal : registerModal;
    let newModal = newModalStr === 'login' ? loginModal : registerModal;
    oldModal.style.display = "none";
    newModal.style.display = "block";
}


// When text in the footer is clicked show the other modal
var registerText = document.getElementById('register');
if (registerText !== null && registerText !== undefined) {
    registerText.addEventListener("click", function (event) {
        console.log('event');
        loginModal.style.display = "none";
        registerModal.style.display = "block";
    }, false);
}

var loginText = document.getElementById('login');
if (loginText !== null && loginText !== undefined) {
    loginText.addEventListener("click", function (event) {
        registerModal.style.display = "none";
        loginModal.style.display = "block";
    }, false);
}
