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
Vue.component('product_details', require('./ProductDetailsScreen.vue'));
Vue.component('shoppingcart_screen', require('./shoppingCartScreen.vue'));

window.shoppingcart = new shoppingCart();

new Vue({
    el: '#app',
    data :{
        show_login:false,
        show_register:false,
        show_products: false,
        show_product_details: false,
        show_shoppingcart_screen: false,
        on_product_section: true,
        LogedIn:false,
        shoppingcart: shoppingcart,
        chosen_detail_product:null,
        user_status: {}
    },
    methods:{
        close_product_details:function(){
            this.show_product_details = false;
        },
        open_shoppingcart_screen:function(){
            this.show_shoppingcart_screen = true;
            this.on_product_section = false;
        },
        close_shoppingcart_screen:function(){
            this.show_shoppingcart_screen = false;
            this.on_product_section = true;
        },
        open_product_details:function(product){
            this.chosen_detail_product = product;
            this.show_product_details = true;
            console.log(product.Genres);
        },
        showLogin:function(){
            this.show_login = true;
        },
        closeLogin:function () {
            this.show_login = false;
        },
        login_failed:function () {
            console.log('failed');
        },
        login_success:function () {
            this.closeLogin();
            this.check_login();
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
                base.user_status = JSON.parse(xhr.response);
                base.LogedIn = base.user_status.IsLogedIn;
            };

            xhr.send();
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
