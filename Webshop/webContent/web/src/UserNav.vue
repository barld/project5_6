<script></script>
<template>
    <userlogedinnav v-if="logedIn"></userlogedinnav>
    <userlogedoutnav v-on:showLogin="showLogin" v-on:showRegister="showRegister" v-else></userlogedoutnav>
</template>
<script>
    export default{
        props:['logedIn'],
        methods:{
            showLogin : function () {
                this.$emit('showlogin');
            },
            showRegister: function () {
                this.$emit('showregister')
            }
        }
    }

</script>