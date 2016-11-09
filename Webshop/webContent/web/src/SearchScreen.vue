<template>
    <div class="container">
        <input v-model="searchValue" list="GameSearchList" type="search" id="game_search" name="game_search" class="u-full-width" placeholder="Zoeken" @keyup="searchGame"/>
        <datalist id="GameSearchList">
            <option v-for="game in searchResult" :value="game.GameTitle">{{ game.Platform.PlatformTitle }}</option>
        </datalist>

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
                searchResults: false
            }
        },
        methods:{
            searchGame:function(){

                if(this.searchValue.length >= 3){
                    var base = this;
                    var xhr = new XMLHttpRequest();

                    xhr.open("POST", "/api/product/search");

                    // The RequestHeader can be any, by the server accepted, file
                    xhr.setRequestHeader('Content-type', "Application/JSON", true);

                    // Function to fire off when the server has send a response
                    xhr.onload = function () {
                        base.searchResult = JSON.parse(xhr.response);
                    };

                    xhr.send(JSON.stringify({value: this.searchValue}));
                    this.searchResults = true;
                    this.hide_products();
                }
                else
                {
                    this.searchResults = false;
                }
            },
            show_details: function (game) {
                this.$emit('show_details', game)
            },
            hide_products: function(){
                this.$emit('hide_products');
            }
        }
    }
</script>