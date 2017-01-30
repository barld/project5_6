<template>
    <div>
        <div v-if="showProducts">
            <table class="u-full-width">
                <thead>
                    <tr>
                        <!--<th><img  class="thumbnail" v-bind:src="product.Image" alt=""></th>-->
                        <thead>
                            <th>Afbeelding</th>
                            <th>Titel</th>
                            <th>Prijs</th>
                            <th>Platform</th>
                        </thead>
                        <tbody v-for="product in products.Games">
                            <tr>
                                <td><img class="thumb" v-bind:src="product.Image" alt=""></td>
                                <td>{{ product.GameTitle }}</td>
                                <td>&euro;{{(product.Price / 100).toFixed(2)}} </td>
                                <td>{{ product.Platform.PlatformTitle }}</td>
                            </tr>
                        </tbody>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
</template>

<script>
    export default{
        props: [],
        data: function() {
            return {
                products: [],
                showProducts: false
            }
        },
        methods:
        {
        },
        created: function(){
            var xhr = new XMLHttpRequest();

            xhr.open('GET', `/api/user/sharedwishlist/?id=${location.search.split('id=')[1]}`);
            xhr.setRequestHeader('Content-type', 'Application/JSON', true);

            var base = this;

            xhr.onload = function(){
                base.products = JSON.parse(xhr.response);
                base.showProducts = true;
            }

            xhr.send();
        }
    }
</script>

<style>
    .thumb{
        width:150px;
        height:150px;
    }
</style>