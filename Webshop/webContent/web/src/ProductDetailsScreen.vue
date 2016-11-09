<template>
    <div class="container margin-top"><!-- Start container -->
        <div class="ten columns">
            <div class="product_detail_image_container">
                <img v-bind:src="product.Image[2]" class="detail_image"/>
            </div>
            <div class="product_detail_info_container">
                <header class="product_detail_title">
                    <h1>{{product.GameTitle}}</h1>
                    <span>Voor de {{product.Platform.PlatformTitle}}</span>
                </header>
                <section class="product_detail_info">
                    <h2>Omschrijving</h2>
                    <p>{{product.Description}}

                    <h2>Genres</h2>
                    <ul>
                        <li v-for="genre in product.Genres">- {{genre.Name}}</li>
                    </ul>

                    <h2>Price</h2>
                    <p>{{(product.Price/100).toFixed(2)}}</p>
                </section>
            </div>
        </div>
    </div>
</template>
<script>
    export default{
        props:['product'],
        data: function(){
            return{
                cart:[]
            }
        }
    }
</script>