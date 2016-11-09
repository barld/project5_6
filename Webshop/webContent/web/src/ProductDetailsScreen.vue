<popup v-on:close="close" id="login_screen" class="login_screen">
    <div class="container margin-top"><!-- Start container -->
        <div class="ten columns">
            <div class="product_image_container">
                <img v-bind:src="product.Image[2]"/>
            </div>
            <div class="product_info_container">
                <h1>{{product.GameTitle}}</h1>
                <h2>Omschrijving</h2>
                <p>{{product.Price}}</p>
                <h3>Gernes</h3>
                <span v-for="gerne in product.Genres">- {{gerne.Name}}</span><br/>
            </div>
        </div>
    </div>
</popup>

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