<template>
    <ul class="submenu">
        <li v-for="menuItem in menuItems">
            <a v-bind:href="'/#platform/' + menuItem._id">{{ menuItem.PlatformTitle }}</a>
        </li>
    </ul>
</template>

<script>
    export default{
        data: function(){
            return{
                menuItems:[]
            }
        },
        methods: {
            getData: function () {
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("GET", "/api/platforms");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.menuItems = JSON.parse(xhr.response).MenuItems;
                };

                xhr.send();
            }
        }
    }
</script>