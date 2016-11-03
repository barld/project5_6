<template>
    <ul class="submenu">
        <li v-for="cartLine in cartLines">
            {{cartLine.Amount}}x {{cartLine.Product.GameTitle}}
        </li>
    </ul>
</template>

<script>
    export default {
        data: function () {
            return {
                cartLines: []
            }
        },
        methods: {
            getData: function () {
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("GET", "/api/shoppingcart");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);


                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.cartLines = JSON.parse(xhr.response).CartLines;
                };

                xhr.send();
            }
        },
        created: function () {
            this.getData();
        }
    }
</script>
