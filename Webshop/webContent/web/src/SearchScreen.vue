<template>
    <div class="container">
        <input v-model="searchValue" list="GameSearchList" type="search" id="game_search" name="game_search" class="u-full-width" placeholder="Zoeken" @keyup="stringChange"/>
        <datalist id="GameSearchList">
            <option v-for="game in searchResult" :value="game.GameTitle">{{ game.Platform.PlatformTitle }}</option>
        </datalist>
        <a href="#" @click.prevent="toggle_advanced_search">Geavanceerd zoeken</a>
        <div class="row advanced_search" v-show="advancedSearch">
            <div class="inner_padding">
                <div class="six columns">
                    <h3>Bedrag</h3>
                    <label for="minB">minimaal bedrag</label>
                    <input v-model.number="min" id="minB" type="range" min="0" max="150" @change="searchGame"> {{min}}
                    <label for="maxB">maximaal bedrag</label>
                    <input v-model.number="max" id="maxB" type="range" min="0" max="150" @change="searchGame"> {{max}}
                </div>
                <div class="six columns">
                    <h3>platformen</h3>
                    <div v-for="platform in platforms">
                        <input type="checkbox" :value="platform.PlatformTitle" v-model="checkedplatforms" :id="'pc_'+platform"  />
                        <label style="display: inline" :for="'pc_'+platform">{{platform.PlatformTitle}}</label>
                    </div>
                </div>
                <button @click="searchGame">zoeken</button>
            </div>
        </div>

        <div v-show="searchResults">
            <productbox @show_details="show_details" v-for="product in searchResult" v-bind:product="product"></productbox>
        </div>

    </div>
</template>

<script>
    export default{
        data:function(){
            return{
                searchValue: "",
                searchResult:[],
                searchResults: false,
                min: 0,
                max: 0,
                platforms: [],
                checkedplatforms: [],
                advancedSearch: false
            }
        },
        methods:{
            stringChange:function () {
                if(this.searchValue.length >= 3){
                    this.searchGame()
                }
                else{
                    this.searchResults = false;
                }
            },
            getPlatforms: function () {
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("GET", "/api/platform/all");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.platforms = JSON.parse(xhr.response);
                };

                xhr.send();
            },
            searchGame:function(){
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("POST", "/api/product/search");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.searchResult = JSON.parse(xhr.response);
                };

                xhr.send(
                    JSON.stringify(
                        {
                            Title: this.searchValue,
                            PriceLt:this.max*100,
                            PriceGt:this.min*100,
                            Platforms:this.checkedplatforms
                        }
                    )
                );
                this.searchResults = true;
                this.hide_products();

            },
            show_details: function (game) {
                this.$emit('show_details', game)
            },
            hide_products: function(){
                this.$emit('hide_products');
            },
            toggle_advanced_search: function(){
                this.advancedSearch === false ? this.advancedSearch = true : this.advancedSearch = false;
            }
        },
        created:function () {
            this.getPlatforms();
        }
    }
</script>