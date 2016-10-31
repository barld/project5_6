function Login() {
    var userInformation = { username: document.getElementById("login_email").value, password: document.getElementById("login_password").value };
    var formHandler = new FormHandler();
    console.log(userInformation.username);
    console.log(userInformation.password);

    formHandler.addValue('username', userInformation.username.value);
    formHandler.addValue('password', userInformation.password.value);

    formHandler.startCall({
        requestHeader: 'application/json',
        method: 'POST',
        url: 'http://localhost:8080/index.html',
        data: formHandler,
        ajaxFunction: function () {
            console.log(userInformation.username, userInformation.password);
        }
    });
}

// Display the username in the username textbox (saved with localStorage)
function displayUsername() {
    if (localStorage.getItem("login_email")) {
        document.getElementById("login_email").value = localStorage.getItem("login_email");
        document.getElementById("login_password").focus();
    }
}