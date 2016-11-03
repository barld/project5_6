<template>
    <div class="popup login_screen" id="login_screen"><!-- Start login screen -->
        <div class="close_btn">
            <i class="fa fa-times fa-5x" aria-hidden="true"></i>
        </div>
        <div class="inner_padding">
            <h1>Login</h1>
            <form action="" method="post">
                <div class="twelve columns">
                    <label for="login_email">Uw email:</label><input class="u-full-width" type="email" placeholder="test@mail.nl" name="login_email" id="login_email">
                </div>
                <div class="twelve columns">
                    <label for="login_password">Uw wachtwoord</label>
                    <input class="u-full-width" type="password" placeholder="Wachtwoord" name="login_password" id="login_password">
                </div>
                <button class="button-primary login_button" type="submit" value="Log in" @click="login">Log in</button>
            </form>
            <div id="loginFeedback"></div>
        </div>
    </div><!-- End login screen -->
</template>

<script>
    export default{
        data: function() {
            return {
                showLogin: false
            }
        },
        methods:{
            login: function(e){
                e.preventDefault();
                var userInformation = {email:document.getElementById("login_email").value, password:
                        document.getElementById("login_password").value};
                this.getData(userInformation);
            },
            getData: function (userInformation) {
                console.log(userInformation);
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/login/");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.cartLines = JSON.parse(xhr.response);
                    console.log(xhr.response);
                };

                xhr.send(userInformation);
            }
        }
    }
</script>