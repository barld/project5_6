<template>
    <popup v-on:close="close" >
        <h1>Registreren</h1>
        <h2 style="color: red;" v-show="showwarning">{{warning_message}}</h2>
        <div class="twelve columns">
            <label for="register_email">Email</label>
            <input v-model="email" class="u-full-width" type="email" placeholder="test@example.com" id="register_email">
        </div>
        <div class="twelve columns">
            <label for="register_password">Wachtwoord</label>
            <input v-model="password" class="u-full-width" type="password" placeholder="Wachtwoord" id="register_password">
        </div>
        <div class="twelve columns">
            <label for="register_password">Geslacht</label>
            <select id="register_gender">
                <option value="0">Man</option>
                <option value="1">Vrouw</option>
            </select>
        </div>
        <button class="button-primary register_button" type="submit" value="Registreren" @click="register">Registreren</button>
    </popup>
</template>

<script>
    export default{
        data: function() {
            return {
                email:'',
                password:'',
                showwarning:false,
                warning_message:''
            }
        },
        methods:{
            register: function(e){
                e.preventDefault();
                var userInformation = {email:this.email, password:this.password};
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/register/");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    if(JSON.parse(xhr.response).Succes){
                        base.$emit('close');
                    }else{
                        base.warning_message = JSON.parse(xhr.response).message;
                        base.showwarning = true;
                    }
                };

                xhr.send(JSON.stringify(userInformation));
            },
            close: function () {
                this.$emit('close')
            }
        }
    }
</script>