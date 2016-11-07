<template>
    <ul class="submenu">
        <li v-for="cartLine in cartLines">
            {{cartLine.Amount}}x {{cartLine.Product.GameTitle}}
        </li>
    </ul>
</template>

<script>
    export default {
        props:['shoppingcart'],
        data: function () {
            return {
                cartLines: []
            }
        },
        methods: {
            getData: function (s) {
                this.cartLines = s.cartLines;
            }
        },
        created: function () {
            this.shoppingcart.registerOnChangedshoppingCart(this.getData);
        }
    }
</script>
