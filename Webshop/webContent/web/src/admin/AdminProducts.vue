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
                <td>{{ product.Price / 100 }}</td>
                <td><button class="button-primary">Aanpassen</button></td>
                <td><button class="button-primary">Verwijderen</button></td>
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
        }
    }
</script>