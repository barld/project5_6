<template>
    <div v-show="showLogin" class="popup login_screen" id="login_screen"><!-- Start login screen -->
        <div class="close_btn">
            <i class="fa fa-times fa-5x" aria-hidden="true"></i>
        </div>
        <div class="inner_padding">
            <h1>Login</h1>
            <form action="" method="post">
                <div class="twelve columns">
                    <label for="login_email">Uw email:</label><input class="u-full-width" type="email" placeholder="test@mail.nl" name="login_email" id="login_email" @blur="storeUsername">
                </div>
                <div class="twelve columns">
                    <label for="login_password">Uw wachtwoord</label>
                    <input class="u-full-width" type="password" placeholder="Wachtwoord" name="login_password" id="login_password">
                </div>
                <button class="button-primary login_button" type="submit" value="Log in">Log in</button>
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
            getData: function () {
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("GET", "/api/shoppingcart");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);


                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.cartLines = JSON.parse(xhr.response).CartLines;
                };

                xhr.send();
            }
        }
    }
</script>