<template>
    <li class="floating_menu_component">
        <a href="#"><i class="fa fa-shopping-basket" aria-hidden="true"></i> Winkelwagen</a>
        <ul class="submenu">
            <li v-for="cartLine in cart.CartLines">
                {{cartLine.Amount}}x {{cartLine.Product.GameTitle}} ({{cartLine.Product.Platform.Abbreviation}})  &euro;{{(cartLine.Product.Price*cartLine.Amount/100.0).toFixed(2)}}
            </li>
            <li><u>Totaal: &euro;{{(cart.TotalPrice/100.0).toFixed(2)}}</u></li>
        </ul>

    </li>
</template>

<script>
    export default {
        props:['shoppingcart'],
        data: function () {
            return {
                cart: {}
            }
        },
        methods: {
            getData: function (s) {
                this.cart = s.cart;
            }
        },
        created: function () {
            this.shoppingcart.registerOnChangedshoppingCart(this.getData);
        }
    }
</script>
