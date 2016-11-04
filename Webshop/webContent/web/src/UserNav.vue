<script></script>
<template>
    <userlogedinnav v-if="logedin"></userlogedinnav>
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
        props:['logedin'],
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
            }
        }
    }

</script>