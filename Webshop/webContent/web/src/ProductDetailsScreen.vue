<template>
    <div class="container margin-top"><!-- Start container -->
        <div class="ten columns">
            <div class="product_detail_image_container">
                <img v-bind:src="product.Image[2]" class="detail_image"/>
            </div>
            <div class="product_detail_info_container">
                <h1>{{product.GameTitle}}</h1>
                <h2>Omschrijving</h2>
                <p>{{product.Description}}
                <h3>Gernes</h3>
                <span v-for="gerne in product.Genres">- {{gerne.Name}}</span><br/>
                <h3>Price</h3>
                <p>{{(product.Price/100).fixedTo(2)}}</p>
            </div>
        </div>
    </div>
</template>
<script>
    export default{
        props:['product'],
        data: function(){
            return{
                product:[]
            }
        }
    }
</script>