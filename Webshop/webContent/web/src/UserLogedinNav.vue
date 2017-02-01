<template>
    <li class="floating_menu_component" id="user_profile">
        <a href="#" @click="show_account_page"><i class="fa fa-user-circle" aria-hidden="true"></i> {{status.Email}} ({{status.Role}})</a>
        <ul class="submenu">
            <li><a href="#" @click="logout"><i class="fa fa-sign-out"
                               aria-hidden="true"
            ></i> Uitloggen</a></li>
            <li v-if="status.Role == 'Admin'"><a href="/admin.html">Admin</a> </li>
        </ul>
    </li>
</template>

<script>
    export default{
        props: ['status'],
        methods:{
            logout:function () {
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("PUT", "/api/user/logout");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);


                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.$emit('logedout')
                };

                xhr.send();
            },
            showFavourites:function () {
                this.$emit('showFavourites');
            },
            show_account_page:function(){
                //alert("loggedinnav");
                this.$emit("show_account_page");
            }
        }
    }
</script>