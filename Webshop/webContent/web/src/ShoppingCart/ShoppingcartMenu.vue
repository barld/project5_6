<template>
    <li class="floating_menu_component">
        <a href="#"><i class="fa fa-shopping-basket" aria-hidden="true"></i> Winkelwagen</a>
        <div>
            <ul class="submenu scrollable">
                <li v-for="cartLine in cart.CartLines">
                    {{cartLine.Amount}}x {{cartLine.Product.GameTitle}} ({{cartLine.Product.Platform.Abbreviation}})  &euro;{{(cartLine.Product.Price*cartLine.Amount/100.0).toFixed(2)}}
                    <br /><button @click.prevent="sub(cartLine.Product)">-</button><button @click.prevent="add(cartLine.Product)">+</button>
                </li>
                <li>Totaal: &euro;{{(cart.TotalPrice/100.0).toFixed(2)}}</li>
                <li>
                    <a class="button afrekenen" @click.prevent="shopping_screen" href="#">Details</a>
                </li>
            </ul>
        </div>
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
            shopping_screen:function () {
                this.$emit("shopping_screen");
            },
            getData: function (s) {
                this.cart = s.cart;
            },
            add:function (product) {
                window.context.ShoppingCart.addToCart(product);
            },
            sub:function (product) {
                window.context.ShoppingCart.removeFromCart(product);
            }
        },
        created: function () {
            window.context.ShoppingCart.registerOnChangedshoppingCart(this.getData);
        }
    }
</script>
