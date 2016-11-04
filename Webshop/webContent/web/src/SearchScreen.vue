<template>
    <div>
        <input v-model="searchValue" type="search" id="game_search" name="game_search" class="u-full-width" placeholder="Zoeken" @keyup="searchGame"/>
        <datalist></datalist>
    </div>
</template>

<script>
    export default{
        data:function(){
            return{
                searchValue: "",
                searchResult:[],
            }
        },
        methods:{
            searchGame:function(){
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/product/search");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    this.searchValue = JSON.parse(xhr.response);
                    console.log(this.searchValue);
                };

                xhr.send(JSON.stringify({value: this.searchValue}));
            }
        }
    }
</script>