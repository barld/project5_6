<template>
    <div>
        <keep-alive>
            <component v-bind:is="currentView">
            </component>
        </keep-alive>
    </div>
</template>

<script>
    export default{
        data: function(){
            return{
                products:[],
                currentView: "t1",
                cc: 0
            }
        },
        components:{
            t1: require("./ProductScreen.vue"),
            t2: require("./ProductDetailsScreen.vue"),
        },
        methods: {
            getData: function () {
                var base = this;

                var xhr = new XMLHttpRequest();
                xhr.open("GET", "/api/product/all");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    base.products = JSON.parse(xhr.response);
                    console.log(base.products);
                };

                xhr.send();
            },
            showProductDetails(product){
                this.cc++;
                this.cc %= 2;
                this.currentView = 't'+(this.cc+1);
                console.log(product.GameTitle);
            }
        },
        created: function(){
            this.getData();
        }
    }
</script>