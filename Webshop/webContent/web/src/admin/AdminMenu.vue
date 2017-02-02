<template>
    <div>
        <div class="twelve columns centerbox">
            <div id="buttonbox" v-if="choosenMenu == 0" >
                <h1 class="centerbox">Admin Environment</h1>
                <button class="admin_button button-primary line_break" @click="choosenMenu = 1">User Management</button>
                <button class="admin_button button-primary line_break" @click="choosenMenu = 2">Product Management</button>
                <button class="admin_button button-primary line_break" @click="choosenMenu = 3">Statistics</button>
            </div>
            <div id="screenbox">
                <user_admin_screen @close="choosenMenu = 0" v-if="choosenMenu == 1"></user_admin_screen>
                <admin_products @close="choosenMenu = 0" v-if="choosenMenu == 2"></admin_products>
                <statistics_admin_screen @close="choosenMenu = 0" v-if="choosenMenu == 3"></statistics_admin_screen>
            </div>
        </div>
    </div>
</template>
<script>
    export default{
        data: function(){
            return{
                choosenMenu: 0
            }
        },
        methods: {
            log: function() {
                console.log("event");
            }
        },
        created: function(){
        },
        mounted: function(){
        }
    }
</script>