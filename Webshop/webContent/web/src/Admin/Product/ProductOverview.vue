<template>
    <div>
        <h1>Products</h1>
        <table>
            <tr>
                <th><b>Titel</b></th>
                <th><b>Platform</b></th>
                <th><b>Prijs</b></th>
            </tr>
            <tr v-for="product in products">
                <td>{{ product.GameTitle }}</td>
                <td>{{ product.Platform.PlatformTitle }}</td>
                <td>{{ product.Price }}</td>
                <td><a href="#" class="button-primary" @click="editGame(product)">Aanpassen</a></td>
                <td><a href="#" class="button-primary" @click="deleteGame(product.GameTitle)">Verwijderen</a></td>
            </tr>
        </table>
    </div>
</template>
<script>
    export default{
        props:['products'],
        data: function(){
            return{
            }
        },
        methods:{
            deleteGame: function(game_title){
                var deleteConfirmation = window.confirm('Weet u zeker dat u deze game wilt verwijderen?');

                if(deleteConfirmation){
                    var xhr = new XMLHttpRequest();
                    xhr.open('DELETE', `/api/product/?ean=${game_title}`);

                    xhr.onload = function(){
                        console.log(xhr.response);
                    };

                    xhr.send();
                }
            },
            editGame(game){
                game.GameTitle = this.GameTitle;
                game.Platform.PlatformTitle = this.PlatformTitle;
                game.Price = this.ProductPrice;

                var xhr = new XMLHttpRequest();
                xhr.open('PUT', `/api/product/game`);

                xhr.onload = function(){
                    console.log(xhr.response);
                };

                xhr.send(game);
            }
        }
    }
</script>