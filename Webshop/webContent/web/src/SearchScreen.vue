<template>
    <div>
        <input v-model="searchValue" list="search_suggestions" type="search" id="game_search" name="game_search" class="u-full-width" placeholder="Zoeken" @keyup="searchGame"/>
        <datalist id="search_suggestions">
            <option value="0">0</option>
            <option value="1">1</option>
            <option value="2">2</option>
        </datalist>
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

                if(this.searchValue.length > 3){
                    var xhr = new XMLHttpRequest();
                    xhr.open("POST", "/api/product/search");

                    // The RequestHeader can be any, by the server accepted, file
                    xhr.setRequestHeader('Content-type', "Application/JSON", true);

                    // Function to fire off when the server has send a response
                    xhr.onload = function () {
                        var result = JSON.parse(xhr.response);

                        for(var i = 0; i < result.length; i ++){
                            var gameTitle = result[i].GameTitle;
                            document.getElementById("search_suggestions").innerHTML = "<option value=' " + result[i].EAN + "'>" + result[i].GameTitle + "</option>";
                        }

                        document.getElementById("search_suggestions").focus();
                    };

                    xhr.send(JSON.stringify({value: this.searchValue}));
                }
            }
        }
    }
</script>