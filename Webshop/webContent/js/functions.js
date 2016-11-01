var formHandler = new FormHandler();

function Login() {
    var userInformation = { username: document.getElementById("login_email").value, password: document.getElementById("login_password").value };

    formHandler.addValue('username', userInformation.username.value);
    formHandler.addValue('password', userInformation.password.value);

    formHandler.startCall({
        requestHeader: 'application/json',
        method: 'POST',
        url: 'http://localhost:8080/api/user/login',
        data: formHandler,
        ajaxFunction: function () {
            console.log(userInformation.username, userInformation.password);
            formHandler.removeValue('username');
            formhandler.removeValue('password');
        }
    });
}

// Display the username in the username textbox (saved with localStorage)
function displayUsername() {
    if (localStorage.getItem("login_email") !== null) {

        // Check if the element exists (need to wrap this up within a class to make it cleaner) and set the text of the textbox
        if (typeof (document.getElementById('login_email')) != 'undefined' && document.getElementById('login_email') != null) {
            document.getElementById("login_email").value = localStorage.getItem("login_email");
            document.getElementById("login_password").focus();
        }
    }
}

function SearchGame(query) {
    formHandler.addValue('query', query);
    formHandler.startCall({
        requestHeader: 'application/json',
        method: 'GET',
        url: 'http://localhost:8080/index.html',
        data: formHandler,
        ajaxFunction: function () {
            console.log(query);
            formHandler.removeValue('query');
        }
    });
}