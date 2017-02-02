<template>
    <div>
        <div id="gameOverview" v-if="showList">
            <h1>Products</h1>
            <table class="centerTable">
                <tr>
                    <th><b>Titel</b></th>
                    <th><b>Platform</b></th>
                    <th><b>Prijs</b></th>
                </tr>
                <tr v-for="product in products">
                    <td>{{ product.GameTitle }}</td>
                    <td>{{ product.Platform.PlatformTitle }}</td>
                    <td>{{ stylePrice(product.Price) }}</td>
                    <td><button class="tiny_admin_button button-primary" @click="editGame(product)">Aanpassen</button></td>
                    <td><button class="tiny_admin_button button-primary" @click="deleteGame(product.EAN)">Verwijderen</button></td>
                </tr>
            </table>
            <br>
            <button class="admin_button button-primary line_break" @click="showProductsAddMenu()">New Game</button>
            <button class="admin_button button-primary line_break" @click="showGenresAddMenu()">New Genre</button>
            <button class="admin_button button-primary line_break" @click="showPlatformsAddMenu">New Platform</button>
            <button class="admin_button button-primary line_break" @click="$emit('close')">Close</button>
        </div>
        <admin_edit_products v-if="showGameEdit" :game="game" @close="showGameEdit = false; showList = true; refreshList();"></admin_edit_products>
        <admin_add_products v-if="addProducts" @close="addProducts = false; showList = true, refreshList()" :platforms="platforms" :publishers="publishers" :genres="genres"></admin_add_products>
        <admin_add_platform v-if="showAddPlatform" @close="showAddPlatform = false; showList = true"></admin_add_platform>
        <admin_add_genre v-if="showAddGenre" @close="showAddGenre = false; showList = true"></admin_add_genre>
    </div>
</template>
<script>
    export default{

        data: function(){
            return{
                products:[],
                platforms:[],
                publishers:[],
                genres:[],
                productsLoaded: false,
                addProducts: false,
                editProducts: false,
                showAddGenre: false,
                showAddPlatform: false,
                statisticsScreen: false,
                game: null,
                showGameEdit:false,
                showList:true
            }
        },
        methods:{
            deleteGame: function(ean){
                var deleteConfirmation = window.confirm('Weet u zeker dat u deze game wilt verwijderen?');

                if(deleteConfirmation){
                    var base = this;
                    var xhr = new XMLHttpRequest();
                    xhr.open('DELETE', `/api/product/?ean=${ean}`);

                    xhr.onload = function(){
                        console.log(xhr.response);
                        base.refreshList();
                    };

                    xhr.send();
                }
            },
            editGame(game_product){
                console.log(game_product);
                this.game = game_product;
                console.log(this.game);
                this.showGameEdit = true;
                this.showList = false;
            },
            getAllProducts: function(){
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("GET", "/api/product/all");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    base.products = JSON.parse(xhr.response);
                };

                xhr.send();
            },
            getAllPlatforms: function(){
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("GET", "/api/platform/all");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    base.platforms = JSON.parse(xhr.response);
                };

                xhr.send();
            },
            getAllPublishers: function(){
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("GET", "/api/publishers/all");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    base.publishers = JSON.parse(xhr.response);
                };

                xhr.send();
            },
            getAllGenres: function(){
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("GET", "/api/genre/all");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    base.genres = JSON.parse(xhr.response);
                };

                xhr.send();
            },
            showProductsAddMenu: function(){
                this.getAllPlatforms();
                this.getAllGenres();
                this.addProducts = true;
                this.showList = false;
            },
            showGenresAddMenu: function(){
                this.showAddGenre = true;
                this.showList = false;
            },
            showPlatformsAddMenu: function(){
                this.showAddPlatform = true;
                this.showList = false;
            },
            refreshList:function(){
                this.getAllProducts();
                this.getAllPlatforms();
                this.getAllGenres();
            },
            stylePrice(val){
                let decimalVal = val/100;
                let stringVal;
                if(Math.floor(val/100) === decimalVal){
                    stringVal= "€" + decimalVal + ".-";
                }else if(Math.floor(val/10)=== decimalVal*10) {
                    stringVal= "€" + (val / 100) + "0";
                } else{
                    stringVal= "€" + (val / 100);
                }

                return stringVal.replace('.',',')
            }
        },
        created: function(){
            this.refreshList();
        }
    }
</script>