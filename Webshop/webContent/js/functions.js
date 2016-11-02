var formHandler = new FormHandler();
var router = new Router();

﻿function Login() {
    var userInformation = { email: document.getElementById("login_email").value, password: document.getElementById("login_password").value };

    formHandler.startCall({
        requestHeader: 'application/json',
        method: 'GET',
        url: 'http://localhost:8080/api/user/login/',
        data: {email: formHandler.getValue('email'), password: formHandler.getValue('password')},
        ajaxFunction: function (data) {
            var feedback = JSON.parse(data);

            if (!feedback.Success) {
                document.getElementById("loginFeedback").innerHTML = feedback.Message;
            } else {
                document.getElementById("loginFeedback").innerHTML = "Login successful!";
            }
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

function LoadProducts() {
    formHandler.startCall({
        requestHeader: 'application/json',
        method: 'GET',
        url: 'http://localhost:8080/api/product/all/',
        data: formHandler,
        ajaxFunction: function (data) {
            console.log(data);
        }
    });
}

function LoadProductsByPlatform() {
    setTimeout(function () {
        console.log(router.getProductByPlatform());
    }, 250);
}