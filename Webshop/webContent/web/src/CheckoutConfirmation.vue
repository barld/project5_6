<template>
    <div><!-- Start container -->
        <div class="container">
        	<div class="row">
                <div class="ten.columns offset-by-two-columns">
                	<h4 align="center">Winkelwagen |  
                	Informatie | Betalingswijze | <strong>Bevestiging</strong></h4>
                </div>
            </div>
        </div>
        <div class="container">
        	<div class="row">
        		<div class="six columns">
        		<div class="element-container">
        			
        				
					<h3 align="center">Gegevens</h3>
					<hr>
						<table class="u-full-width">
        				<tbody>
        					<tr>
        						<td><strong>Voornaam</strong></td>
                                <td>{{ checkoutInformation.voornaam }}</td>
        					</tr>
        					<tr>
        						<td><strong>Achternaam</strong></td>
        						<td>{{ checkoutInformation.achternaam }}</td>
        					</tr>
        					<tr>
        						<td><strong>Postcode</strong></td>
        						<td>{{ checkoutInformation.postcode }}</td>
        					</tr>
        					<tr>
        					 	<td><strong>Huisnummer</strong></td>
        					 	<td>{{ checkoutInformation.huisnummer }}</td>
        					</tr>
        					<tr>
        						<td><strong>Straat</strong></td>
        						<td>{{ checkoutInformation.straat }}</td>
        					</tr>
        					<tr>
        						<td><strong>Stad</strong></td>
        						<td>{{ checkoutInformation.stad }}</td>
        					</tr>
        					<tr>
        						<td><strong>Land</strong></td>
        						<td>{{ checkoutInformation.land }}</td>
        					</tr>
        				</tbody>
        			</table>
        		</div>
        		</div>

        		<div class="six columns">
        			<div class="element-container">
        				<h3 align="center">Bestelling</h3>
        				<hr>
        				<table style="margin-left: 10px; margin-right: 15px;" class="u-full-width">
        					<thead>
	        					<tr>
									<th width="25%">&nbsp;</th>
									<th width="30%">Naam</th>
									<th width="20%">Aantal</th>
									<th width="25%">Prijs</th>
								</tr>
        					</thead>
        					<tbody>
        						<tr v-for="cartline in cart.CartLines">
									<td><img width="50" class="table_thumbnail" v-bind:src="cartline.Product.Image[0]"></td>
									<td>{{cartline.Product.GameTitle}}</td>
									<td>{{cartline.Amount}}</td>
									<td>€{{((cartline.SubTotal/100)).toFixed(2)}}
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td></td>
									<td><strong>Totaal:</strong> €{{(cart.TotalPrice/100).toFixed(2)}}</td>
								</tr>
								<tr>
									<td colspan="4"><button @click="order" class="button-primary">Order!</button></td>
								</tr>
        					</tbody>
        				</table>
						
        			</div>
        		</div>
        	</div>
        </div>
    </div><!-- End container -->
</template>

<script>
    export default{
        data(){
            return{
                cart: {},
				checkoutInformation:{},
            }
        },
        methods:{
			changedShoppingCart:function (sc) {
                this.cart = sc.cart;
            },
            update:function () {
                window.context.ShoppingCart.UpdateCart(this.cart);
            },
			order: function() {
				this.$emit("order");
				window.context.Order.CheckOut();
			}
        },
        created:function(){
            window.context.ShoppingCart.registerOnChangedshoppingCart(this.changedShoppingCart);
            this.changedShoppingCart(window.context.ShoppingCart);
			this.checkoutInformation = window.context.Order.CheckoutInformation;
        }
    }
</script>