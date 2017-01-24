<template>
    <div>
        <h3>Maak een nieuw product</h3>
        <div v-show="platformsLoaded" class="row">
            <div class="row">
                <div class="six columns">
                    <label for="GameTitle"><b>Titel</b></label><input type="text" :model="GameTitle" id="GameTitle">
                </div>
                <div class="six columns">
                    Beschikbaar op:
                    <div v-for="platform in platforms">
                        <label><input :model="GamePlatform" type="checkbox" value="genre.Name"> {{ platform.PlatformTitle }}</label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameRatingPegi"><b>PEGI</b></label><input type="text" :model="GameRatingPegi" id="GameRatingPegi">
                </div>
                <div class="six columns">
                    Genres:
                    <div v-for="genre in genres">
                        <label><input :model="GameGenres" type="checkbox" value="genre.Name"> {{ genre.Name }}</label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="twelve columns">
                    <label for="GameImages"><b>Afbeeldingen</b></label>
                </div>
            </div>
            <div class="row">
                <div class="twelve columns">
                    <label for="GameDescription"><b>Omschrijving</b></label>
                    <textarea class="u-full-width" name="GameDescription" :model="GameDescription" id="GameDescription" cols="100"></textarea>
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameMaxPlayers"><b>Maximum spelers</b></label><input type="number" :model="GameMaxPlayers" id="GameMaxPlayers">
                </div>
                <div class="six columns">
                    <label for="GameMinPlayers"><b>Minimum spelers</b></label><input type="number" :model="GameMinPlayers" id="GameMinPlayers">
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameEAN"><b>EAN</b></label><input type="number" :model="GameEAN" id="GameEAN">
                </div>
                <div class="six columns">
                    <label for="GamePrice"><b>Prijs</b></label><input type="number" :model="GamePrice" id="GamePrice">
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameVRCompatible"><b>VR ready</b></label>
                    <input type="checkbox" :model="VRReady" value="true">VR compatible<br>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    export default{
        props:['platforms', 'publishers', 'genres'],
        data: function(){
            return{
                platformsLoaded: true
            }
        },
        methods:{
            createGame: function(){
                var game = {
                    GameTitle: 'Test',
                    Platform:[],
                    RatingPegi: "",
                    Publisher:[],
                    Genres:[],
                    Image:[],
                    MinPlayers: 0,
                    MaxPlayers: 0,
                    Description: "",
                    EAN: 0,
                    Price:0.00,
                    IsVRCompatible: false,
                    ReleaseDate: "01-01-2018"
                }
            }
        },
        created: function(){
            console.log('Created!');
            console.log(this.platforms);
        }
    }
</script>