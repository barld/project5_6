<template>
    <div id="editGame">
        <h3>Product aanpassen</h3>
        <div v-show="platformsLoaded" class="row">
            <div class="row">
                <div class="six columns">
                    <label for="GameTitle"><b>Titel</b></label><input type="text" v-model="GameTitle" id="GameTitle" v-bind:value="game.GameTitle">
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameRatingPegi"><b>PEGI</b></label><input type="text" v-model="GameRatingPegi" id="GameRatingPegi" v-bind:value="game.RatingPEGI">
                </div>
            </div>
            <div class="row">
                <div class="twelve columns">
                    <div v-for="(image, index) in game.Image">
                        <label for="GameImages" v-if="index == 0"><b>Afbeeldingen</b><input id="GameImages" type="text" v-model="GameImageValue" v-bind:value="game.Image[index]"></label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="twelve columns">
                    <label for="GameDescription"><b>Omschrijving</b></label>
                    <textarea class="u-full-width" name="GameDescription" v-model="GameDescription" id="GameDescription" cols="100" v-bind:value="game.Description"></textarea>
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameMinPlayers"><b>Minimum spelers</b></label><input type="number" v-model="GameMinPlayers" id="GameMinPlayers" v-bind:value="game.MinPlayers">
                </div>
                <div class="six columns">
                    <label for="GameMaxPlayers"><b>Maximum spelers</b></label><input type="number" v-model="GameMaxPlayers" id="GameMaxPlayers" v-bind:value="game.MaxPlayers">
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameEAN"><b>EAN</b></label><input type="number" v-model="GameEAN" id="GameEAN" v-bind:value="game.GameEAN">
                </div>
                <div class="six columns">
                    <label for="GamePrice"><b>Prijs</b></label><input type="number" v-model="GamePrice" id="GamePrice" v-bind:value="game.Price">
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameReleaseDate"><b>Releasedate</b></label><input type="text" v-model="GameReleaseDate" id="GameReleaseDate" v-bind:value="game.ReleaseDate">
                </div>
            </div>
            <button @click="editGame" class="button-primary">Product aanpassen</button>
            <div v-if="success" class="success">{{ successMessage }}</div>
        </div>
        <a href="/admin.html">Terug naar de producten</a>
    </div>
</template>
<script>
    export default{
        props:['platforms', 'publishers', 'genres', 'game'],
        data: function(){
            return{
                success: false,
                successMessage: "U heeft successvol een product aangepast!",
                platformsLoaded: true,
                GameTitle: this.game.GameTitle,
                GamePlatform: this.game.Platform,
                GameRatingPegi: this.game.RatingPEGI,
                GamePublisher: [],
                GameGenre: [],
                GameImages: [],
                GameImageValue: String(this.game.Image),
                GameMinPlayers: this.game.MinPlayers,
                GameMaxPlayers: this.game.MaxPlayers,
                GameDescription: this.game.Description,
                GameEAN: this.game.EAN,
                GamePrice: this.game.Price,
                GameIsVRCompatible: "",
                GameReleaseDate: this.game.ReleaseDate
            }
        },
        methods:{
            editGame: function(){

                this.GameImages.push(this.GameImageValue);

                var game = {
                    _id: this.game._id,
                    GameTitle: this.GameTitle,
                    Platform: this.GamePlatform,
                    RatingPegi: this.GameRatingPegi,
                    Publisher: [],
                    Genres: this.GameGenre,
                    Image: this.GameImages,
                    MinPlayers: this.GameMinPlayers,
                    MaxPlayers: this.GameMaxPlayers,
                    Description: this.GameDescription,
                    EAN: this.GameEAN,
                    Price: this.GamePrice,
                    IsVRCompatible: false,
                    ReleaseDate: this.GameReleaseDate
                };

//                delete game.Platform._id;
//                delete game.Image._id;

                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("PUT", "/api/product/");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    console.log(xhr.response);
                    base.message = "Het product is aangepast";
                    base.success = true;

                    setTimeout(function(){
                        base.success = false;
                    }, 3000);
                };

                xhr.send(JSON.stringify(game));
            },
            hide: function(){
                this.show = false;
            }
        }
    }
</script>