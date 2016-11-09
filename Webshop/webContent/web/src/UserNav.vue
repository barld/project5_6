<script></script>
<template>
    <userlogedinnav v-if="logedin" v-on:logedout="logedout" :status="status"></userlogedinnav>
    <userlogedoutnav
            v-on:showLogin="showLogin"
            v-on:showRegister="showRegister"
            v-on:success="login_success"
            v-on:failed="login_failed"
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
                console.log('log out');
                this.$emit('logedout');
            }
        }
    }

</script>