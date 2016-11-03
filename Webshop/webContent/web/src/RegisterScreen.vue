<template>
    <div class="popup">
        <div class="close_btn">
            <i class="fa fa-times fa-5x" aria-hidden="true"></i>
        </div>
        <div class="inner_padding">
            <h1>Registreren</h1>
            <div class="twelve columns">
                <label for="register_email">Email</label>
                <input class="u-full-width" type="email" placeholder="test@example.com" id="register_email">
            </div>
            <div class="twelve columns">
                <label for="register_password">Wachtwoord</label>
                <input class="u-full-width" type="password" placeholder="Wachtwoord" id="register_password">
            </div>
            <div class="twelve columns">
                <label for="register_password">Geslacht</label>
                <select id="register_gender">
                    <option value="0">Man</option>
                    <option value="1">Vrouw</option>
                </select>
            </div>
            <button class="button-primary register_button" type="submit" value="Registreren" @click="register">Registreren</button>
        </div>
    </div>
</template>

<script>
    export default{
        data: function() {
            return {
            }
        },
        methods:{
            register: function(e){
                e.preventDefault();
                var userInformation = {email:document.getElementById("register_email").value, password:
                document.getElementById("register_password").value};
                this.getData(userInformation);
            },
            getData: function (userInformation) {
                console.log(userInformation);
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/register/");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    console.log(xhr.response);
                };

                xhr.send(JSON.stringify(userInformation));
            }
        }
    }
</script>