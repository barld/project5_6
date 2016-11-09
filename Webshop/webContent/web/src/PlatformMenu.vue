<template>
    <ul class="submenu">
        <li v-for="menuItem in menuItems">
            <a href="#" @click.prevent="getProducts(menuItem.PlatformTitle)">{{ menuItem.PlatformTitle }}</a>
        </li>
    </ul>
</template>

<script>
    export default{
        data: function(){
            return{
                menuItems:[],
                games:[]
            }
        },
        methods: {
            getData: function () {
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("GET", "/api/platform/all");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.menuItems = JSON.parse(xhr.response);
                };

                xhr.send();
            },
            getProducts: function (platform) {
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("GET", "/api/product/gamebyplatform/" + platform);

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.games = JSON.parse(xhr.response);
                };

                xhr.send();
            }
        },
        created: function(){
            this.getData();
        }
    }
</script>