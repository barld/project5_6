<template>
    <div class="product_detail_title_root_container">
        <div class="container product_detail_title">
            <header>
                <h1>{{product.GameTitle}}</h1>
                <span>Voor de {{product.Platform.PlatformTitle}}</span>
            </header>
        </div>
        <div class="container">
            <div class="four columns product_detail_image_container">
                <img v-bind:src="product.Image[2] ? product.Image[2] : product.Image[0]" class="detail_image"/>
            </div>
            <div class="four columns product_detail_info_container">
                <section class="product_detail_info">
                    <div v-if="product.Publisher.length > 1">
                        <h2>Uitgevers</h2>
                        <ul>
                            <li v-for="publisher in product.Publisher">- {{publisher}}</li>
                        </ul>
                    </div>
                    <div v-else>
                        <h2 >Uitgever</h2>
                        <span>{{product.Publisher[0]}}</span>
                    </div>
                    <h2>Genres</h2>
                    <ul>
                        <li v-for="genre in product.Genres">- {{genre.Name}}</li>
                    </ul>
                    <h2>Aantal spelers</h2>
                    <span>{{product.MinPlayers}} - {{product.MaxPlayers}} spelers</span>
                    <h2>Minimale leeftijd</h2>
                    <span>{{product.RatingPEGI}} jaar</span>
                </section>
            </div>
            <div class="four columns product_detail_cart_container">
                <h2>Prijs: €{{(product.Price/100).toFixed(2)}}</h2>
                <div class="product_detail_buttonbox">
                <button @click.prevent="add_to_cart">Voeg toe</button>
                <br/>
                <button @click.prevent="close">Sluit scherm</button>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="12 columns product_detail_description_container">
                <h2>Omschrijving</h2>
                <p>{{product.Description}}
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
        },
        methods:{
            add_to_cart:function () {
                window.shoppingcart.addToCart(this.product);
            },
            close:function(){
                this.$emit("close");
            }
        }
    }

</script>