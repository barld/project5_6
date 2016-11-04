<template xmlns:v-on="http://www.w3.org/1999/xhtml">
    <li class="floating_menu_component" id="user_login_nav">
        <a href="#"><i class="fa fa-user-circle" aria-hidden="true"></i> Log in / Registreren</a>
        <ul class="submenu">
            <li><a href="#" v-on:click.prevent="showLogin"><i class="fa fa-sign-in" aria-hidden="true"></i> Inloggen</a></li>
            <li><a href="#" v-on:click.prevent="showRegister"><i class="fa fa-pencil-square" aria-hidden="true"></i> Registreren</a></li>
        </ul>
    </li>
</template>

<script>
    export default{
        methods:{
            showLogin:function () {
                this.$emit('showLogin');
            },
            showRegister:function () {
                this.$emit('showRegister');
            }
        }
    }
</script>