<template>
    <li class="floating_menu_component" id="user_profile">
        <a href="#"><i class="fa fa-user-circle" aria-hidden="true"></i> {{status.Email}} ({{status.Role}})</a>
        <ul class="submenu">
            <!-- Admin specific Items --->
            <li v-if="status.Role == 'Admin'">
                <a href="#" @click="showadminplots"><span class="fa fa-pie-chart" aria-hidden="true"></span> Statistieken</a>
            </li>

            <!-- Non Specific Items --->
            <li>
                <a href="#" @click="logout"><span class="fa fa-sign-out" aria-hidden="true"></span> Uitloggen</a>
            </li>
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
            showadminplots:function(){
                this.$emit('showadminplots');
            }
        }
    }
</script>