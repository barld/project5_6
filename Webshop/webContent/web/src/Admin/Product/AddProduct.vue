<template>
    <div id="addProduct">
        <h3>Maak een nieuw product</h3>
        <div v-show="platformsLoaded" class="row">
            <div class="row">
                <div class="six columns">
                    <label for="GameTitle"><b>Titel</b></label><input type="text" v-model="GameTitle" id="GameTitle">
                </div>
                <div class="six columns">
                    Beschikbaar op:
                    <form action="">
                        <div v-for="platform in platforms">
                            <label><input v-bind:id="platform.PlatformTitle" v-model="GamePlatform" type="radio" v-bind:value="platform"> {{ platform.PlatformTitle }}</label>
                        </div>
                    </form>
                    <!--<div v-for="platform in platforms">-->
                    <!--<label><input v-bind:id="platform.PlatformTitle" v-model="GamePlatform" type="checkbox" v-bind:value="platform.PlatformTitle"> {{ platform.PlatformTitle }}</label>-->
                    <!--</div>-->
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameRatingPegi"><b>PEGI</b></label><input type="text" v-model="GameRatingPegi" id="GameRatingPegi">
                </div>
                <div class="six columns">
                    Genres:
                    <div v-for="genre in genres">
                        <label><input v-bind:id="genre.Name" v-model="GameGenre" type="checkbox" v-bind:value="genre"> {{ genre.Name }}</label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="twelve columns">
                    <label for="GameImages"><b>Afbeeldingen</b><input type="text" v-model="GameImageValue"></label>
                </div>
            </div>
            <div class="row">
                <div class="twelve columns">
                    <label for="GameDescription"><b>Omschrijving</b></label>
                    <textarea class="u-full-width" name="GameDescription" v-model="GameDescription" id="GameDescription" cols="100"></textarea>
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameMinPlayers"><b>Minimum spelers</b></label><input type="number" v-model="GameMinPlayers" id="GameMinPlayers">
                </div>
                <div class="six columns">
                    <label for="GameMaxPlayers"><b>Maximum spelers</b></label><input type="number" v-model="GameMaxPlayers" id="GameMaxPlayers">
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameEAN"><b>EAN</b></label><input type="number" v-model="GameEAN" id="GameEAN" v-bind:value="GameEAN">
                </div>
                <div class="six columns">
                    <label for="GamePrice"><b>Prijs</b></label><input type="number" v-model="GamePrice" id="GamePrice">
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <input type="checkbox" v-model="VRReady" value="true">VR compatible<br>
                </div>
                <div class="six columns">
                    <input type="date" v-model="GameReleaseDate">
                </div>
            </div>
            <button @click="createGame" class="button-primary">Maak product</button>
            <div v-if="success" class="success">{{ successMessage }}</div>
        </div>
        <a href="/admin.html">Terug naar de producten</a>
    </div>
</template>
<script>
    export default{
        props:['platforms', 'publishers', 'genres'],
        data: function(){
            return{
                success: false,
                successMessage: "U heeft succesvol een product toegevoegd!",
                platformsLoaded: true,
                GameTitle: "",
                GamePlatform: [],
                GameRatingPegi: "",
                GamePublisher: [],
                GameGenre: [],
                GameImages: [],
                GameImageValue: null,
                GameMinPlayers: 0,
                GameMaxPlayers: 0,
                GameDescription: "",
                GameEAN: parseInt(window.crypto.getRandomValues(new Uint32Array(1))),
                GamePrice: 0,
                GameIsVRCompatible: "",
                GameReleaseDate: this.GameReleaseDate
            }
        },
        methods:{
            createGame: function(){

                this.GameImages.push(this.GameImageValue);

                var game = {
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
                    ReleaseDate: "01-01-2018"
                };

                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("POST", "/api/product/");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    console.log(xhr.response);
                    base.message = "U heeft succesvol een product toegevoegd!";
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