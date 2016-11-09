import Vue from 'vue'
//import App from './App.vue'
//import ShoppingcartMenu from './ShoppingcartMenu.vue'
import shoppingCart from './shoppingCart'

Vue.component('userlogedinnav', require('./UserLogedinNav.vue'));
Vue.component('userlogedoutnav', require('./UserLogedoutNav.vue'));
Vue.component('usernav', require('./UserNav.vue'));
Vue.component('popup', require('./popup.vue'));
Vue.component('shopcart', require('./ShoppingcartMenu.vue'));
Vue.component('platform_menu', require('./PlatformMenu.vue'));
Vue.component('login', require('./LoginScreen.vue'));
Vue.component('register', require('./RegisterScreen.vue'));
Vue.component('productbox', require('./ProductBox.vue'));
Vue.component('search', require('./SearchScreen.vue'));
Vue.component('product', require('./ProductScreen.vue'));
Vue.component('shoppingcart_screen', require('./shoppingCartScreen.vue'));

window.shoppingcart = new shoppingCart();

new Vue({
    el: '#app',
    data :{
        show_login:false,
        show_register:false,
        show_products: false,
        show_product_detail: false,
        show_shoppingcart_screen: false,
        LogedIn:false,
        shoppingcart: shoppingcart,
        showDetails: false,
        on_product_section: true,
        detailsOf:{}
    },
    methods:{
        open_shoppingcart_screen:function(){
            this.show_shoppingcart_screen = true;
            this.on_product_section = false;
        },
        close_shoppingcart_screen:function(){
            this.show_shoppingcart_screen = false;
            this.on_product_section = true;
        },
        showProductDetails:function(product){
            this.show_product_detail = true;
            console.log(product.GameTitle);
        },
        showLogin:function(){
            console.log('test');
            this.show_login = true;
        },
        closeLogin:function () {
            this.show_login = false;
        },
        login_failed:function () {
            console.log('failed');
        },
        login_success:function () {
            this.LogedIn = true;
            this.closeLogin();
        },
        logedout:function () {
            this.LogedIn = false;
        },
        showRegister:function(){
            this.show_register = true;
        },
        close_register:function () {
            this.show_register = false;
            this.check_login();
        },
        check_login: function () {
            var base = this;

            var xhr = new XMLHttpRequest();
            xhr.open("GET", "/api/user/status/");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                var result = JSON.parse(xhr.response);
                base.LogedIn = result.IsLogedIn;
            };

            xhr.send();
        },
        show_details: function (game) {
            this.detailsOf = game;
            this.showDetails = true;
        },
        back_to_overview: function () {
            this.showDetails = false;
        },
        show_products: function(){
            this.show_products = true;
        },
        hide_products: function(){
            this.show_products = false;
        },
        show_games: function (games) {

        }
    },
    created: function () {
        this.check_login();
    }
});
