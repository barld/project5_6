<template>
    <div class="row">
        <h1>Test</h1>
        <div class="container">
            <div class="ten.columns offset-by-two-columns">
                <div class="element-container">
                    <div class="panel">
                        <admin_products v-show="productsLoaded" :products="products"></admin_products>
                        <table style="margin-top: 20px;" class="u-full-width">
                            <thead>
                            <th style="width: 40%">Username</th>
                            <th style="width: 20%">Gender</th>
                            <th style="width: 20%">Edit</th>
                            <th style="width: 20%; margin-right: 10px">Delete</th>
                            </thead>
                            <tbody>
                            <tr>
                                <td><p>JFConstancia@hotmail.com</p></td>
                                <td><p>Male</p></td>
                                <td><a href="#"><button class="button-primary">Edit</button></a></td>
                                <td><a href="#"><button class="button-primary">Delete</button></a></td>
                            </tr>
                            <tr>
                                <td><p>info@superict.nl</p></td>
                                <td><p>Male</p></td>
                                <td><a href="#"><button class="button-primary">Edit</button></a></td>
                                <td><a href="#"><button class="button-primary">Delete</button></a></td>
                            </tr>
                            <tr>
                                <td><p>hallo@420.com</p></td>
                                <td><p>Female</p></td>
                                <td><a href="#"><button class="button-primary">Edit</button></a></td>
                                <td><a href="#"><button class="button-primary">Delete</button></a></td>
                            </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default{
        data: function(){
            return{
                products:[],
                productsLoaded: true
            }
        },
        methods:{
            getAllProducts: function(){
                console.log('Test!');
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("GET", "/api/product/all");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    base.products = JSON.parse(xhr.response);
                    base.productsLoaded = true;
                };

                xhr.send();
            }
        },
        created: function(){
            console.log('Created!');
            this.getAllProducts();
        }
    }
</script>