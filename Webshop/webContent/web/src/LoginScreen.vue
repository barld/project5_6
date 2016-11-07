<script>
//    import Vue from 'vue'
//    import popup from './popup.vue'
//    Vue.component('popup', popup);
</script>

<template>
    <popup v-on:close="close" id="login_screen" class="login_screen">
        <h1>Login</h1>
        <h2 style="color: red;" v-show="showwarning">{{warning_message}}</h2>
        <form action="" method="post">
            <div class="twelve columns">
                <label for="login_email">Uw email:</label>
                <input v-model="login_email" class="u-full-width" type="email" placeholder="test@mail.nl" name="login_email" id="login_email">
            </div>
            <div class="twelve columns">
                <label for="login_password">Uw wachtwoord</label>
                <input v-model="login_password" class="u-full-width" type="password" placeholder="Wachtwoord" name="login_password" id="login_password">
            </div>
            <button class="button-primary login_button" type="submit" value="Log in" @click="login">Log in</button>
        </form>
        <div id="loginFeedback"></div>
    </popup><!-- End login screen -->
</template>

<script>
    export default{
        data: function() {
            return {
                login_email: '',
                login_password: '',
                showwarning:false,
                warning_message:''
            }
        },
        methods:{
            login: function(e){
                e.preventDefault();
                var userInformation = {email:this.login_email, password: this.login_password};
                this.getData(userInformation);
            },
            getData: function (userInformation) {
                userInformation = JSON.stringify(userInformation);
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/login/");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    var result = JSON.parse(xhr.response);
                    if(result.Success)
                        base.$emit('success');
                    else{
                        base.showwarning = true;
                        base.warning_message = result.Message;
                        base.$emit('failed');
                    }
                };

                xhr.send(userInformation);
            },
            close: function () {
                this.$emit('close')
            }
        }
    }
</script>