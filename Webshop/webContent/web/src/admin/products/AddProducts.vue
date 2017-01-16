<template>
    <label for="gameTitle"></label><input id="GameTitle" type="text" v-model="GameTitle" />
    <label for="gameTitle"></label><input id="Platform" type="text" v-model="Platform" />
    <label for="gameTitle"></label><input id="RatingPEGI" type="text" v-model="RatingPEGI" />
    <label for="gameTitle"></label><input id="Publisher" type="text" v-model="Publisher" />
    <label for="gameTitle"></label><input id="Genres" type="text" v-model="Genres" />
    <label for="gameTitle"></label><input id="Image" type="text" v-model="Image" />
    <label for="gameTitle"></label><input id="MinPlayers" type="text" v-model="MinPlayers" />
    <label for="gameTitle"></label><input id="MaxPlayers" type="text" v-model="MaxPlayers" />
    <label for="gameTitle"></label><input id="Description" type="text" v-model="Description" />
    <label for="gameTitle"></label><input id="Id" type="text" v-model="Id" />
    <label for="gameTitle"></label><input id="EAN" type="text" v-model="EAN" />
    <label for="gameTitle"></label><input id="Price" type="text" v-model="Price" />
    <label for="gameTitle"></label><input id="IsVRCompatible" type="text" v-model="IsVRCompatible" />
    <label for="gameTitle"></label><input id="ReleaseDate" type="text" v-model="ReleaseDate" />
</template>

<script>
    export default{
        data: function(){
            return{
            }
        },
        methods: {
            getProducts: function (platform) {
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("GET", "/api/product/gamebyplatform/?pt=" + platform);

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.games = JSON.parse(xhr.response);
                    base.$emit('show_games', base.games)
                };

                xhr.send();
            }
        }
    }
</script>