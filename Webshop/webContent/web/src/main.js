import Vue from 'vue'
import Context from "./Gateways/context";
//import App from './App.vue'
//import ShoppingcartMenu from './ShoppingcartMenu.vue'

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
Vue.component('admin_screen', require('./Adminscreen.vue'));

window.context = new Context();

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
        IsAdmin: false,
        shoppingcart: window.context.shoppingcart,
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
            this.IsAdmin = false;
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
                console.log(base.user_status);
                base.LogedIn = base.user_status.IsLogedIn;
                if(base.user_status.Role == "Admin"){
                    base.IsAdmin = true;
                }
            };

            xhr.send();
        },
        check_admin: function(){
            if(typeof user_status ==! undefined && user_status.Role == 0){
                window.alert("true");
                return true;
            }else{
                window.alert("false");                
                return false;
            }
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
