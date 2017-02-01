<template>
    <userlogedinnav v-if="logedin" v-on:logedout="logedout" @show_account_page="show_account_page" v-on:showFavourites="showFavourites" :status="status" ></userlogedinnav>
    <userlogedoutnav
            v-on:showLogin="showLogin"
            v-on:showRegister="showRegister"
            v-on:success="login_success"
            v-on:failed="login_failed"
            v-on:showFavourites="showFavourites"
            v-else>
    </userlogedoutnav>
</template>
<script>
    export default{
        props:['logedin', 'status'],
        methods:{
            showLogin : function () {
                this.$emit('showlogin');
            },
            showRegister: function () {
                this.$emit('showregister');
            },
            login_success: function () {
                this.$emit('login_succes');
            },
            login_failed: function () {
                this.$emit('login_failed');
            },
            logedout: function () {
                this.$emit('logedout');
            },
            showFavourites: function(){
                this.$emit('showFavourites');
            },
            show_account_page:function(){
                //alert("usernav");
                this.$emit("show_account_page");
            }

        }
    }
</script>