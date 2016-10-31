function Login() {
    var userInformation = { email: document.getElementById("login_email").value, password: document.getElementById("login_password").value };
    var formHandler = new FormHandler();
    console.log(userInformation.username);
    console.log(userInformation.password);

    formHandler.addValue('username', userInformation.username);
    formHandler.addValue('password', userInformation.password);

    formHandler.startCall({
        requestHeader: 'application/json',
        method: 'POST',
        url: 'http://localhost:8080/api/user/Login',
        data: JSON.stringify(userInformation),
        ajaxFunction: function (data) {
            console.log(data)
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