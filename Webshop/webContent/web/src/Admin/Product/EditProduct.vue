<template>
    <div id="editGame">
        <h3>Product aanpassen</h3>
        <div v-show="platformsLoaded" class="row">
            <div class="row">
                <div class="six columns">
                    <label for="GameTitle"><b>Titel</b></label><input type="text" v-model="GameTitle" id="GameTitle" >
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameRatingPegi"><b>PEGI</b></label><input type="text" v-model="GameRatingPegi" id="GameRatingPegi" >
                </div>
            </div>
            <div class="row">
                <div class="twelve columns">
                    <div v-for="(image, index) in game.Image">
                        <label for="GameImages" v-if="index == 0"><b>Afbeeldingen</b><input id="GameImages" type="text" v-model="GameImageValue" ></label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="twelve columns">
                    <label for="GameDescription"><b>Omschrijving</b></label>
                    <textarea class="u-full-width" name="GameDescription" v-model="GameDescription" id="GameDescription" cols="100" ></textarea>
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameMinPlayers"><b>Minimum spelers</b></label><input type="number" v-model="GameMinPlayers" id="GameMinPlayers" >
                </div>
                <div class="six columns">
                    <label for="GameMaxPlayers"><b>Maximum spelers</b></label><input type="number" v-model="GameMaxPlayers" id="GameMaxPlayers" >
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameEAN"><b>EAN</b></label><input type="number" v-model="GameEAN" id="GameEAN" >
                </div>
                <div class="six columns">
                    <label for="GamePrice"><b>Prijs</b></label><input type="text" v-model="GamePrice" id="GamePrice" >
                </div>
            </div>
            <div class="row">
                <div class="six columns">
                    <label for="GameReleaseDate"><b>Releasedate</b></label><input type="date" v-model="GameReleaseDate" id="GameReleaseDate" >
                </div>
            </div>
            <button @click="editGame" class="button-primary adminButton">Product aanpassen</button>
            <button class="button-primary adminButton" @click="$emit('close')">Terug</button>
            <div v-if="success" class="success">{{ successMessage }}</div>
        </div>
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
                GamePrice: this.stylePrice(this.game.Price),
                GameIsVRCompatible: "",
                GameReleaseDate: this.getDate(this.game.ReleaseDate)
            }
        },
        methods:{
            editGame: function(){

                this.GameImages.push(this.GameImageValue);

                var price = this.getPrice(this.GamePrice);
                if(price === false){
                    window.alert("Geen geldige prijs ingevuld. De prijs is of een heel getal of een getal met 2 decimalen gescheiden met een komma.");
                    return;
                }

                if(this.GameMinPlayers < 1 || this.GameMaxPlayers < 1){
                    window.alert("Het maximaal of minimaal aantal spelers kan niet kleiner zijn dan nul.");
                    return;
                }
                if(this.GameMinPlayers > this.GameMaxPlayers){
                    window.alert("De maximaal aantal spelers kan niet kleiner zijn dan het minimum.");
                    return;
                }
                //console.log(price);

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
                    Price: price,
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
                        base.$emit("close");
                    }, 3000);
                };

                xhr.send(JSON.stringify(game));
            },
            hide: function(){
                this.show = false;
            },
            getPrice(val){
                let regex = /^\d+(\,\d{2}){0,1}$/;
                if(regex.test(val)){
                    val = val.replace(',','.');
                    return val * 100;
                } else{
                    return false;
                }
            },
            stylePrice(val){
                let decimalVal = val/100;
                let stringVal;
                if(Math.floor(val/100) === decimalVal){
                    stringVal=  decimalVal+"";
                }else if(Math.floor(val/10)=== decimalVal*10) {
                    stringVal= (val / 100) + "0";
                } else{
                    stringVal=  (val / 100) + "";
                }

                return stringVal.replace('.',',')
            },
            getDate(val){
                    let d = new Date(val);
                    return d.getFullYear() + "-" + ('0'+(d.getMonth() +1)).slice(-2)+"-" + ('0' + d.getDate()).slice(-2);
            }
        }
    }
</script>