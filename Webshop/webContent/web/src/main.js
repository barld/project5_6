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
Vue.component('mylists', require('./MyLists.vue'));
Vue.component('mobile_menu', require('./Mobile/MobileMenu.vue'));
Vue.component('mobile_logged_in', require('./Mobile/Mobile_LoggedIn.vue'));
Vue.component('admin_screen', require('./Adminscreen.vue'));
Vue.component('account_screen', require('./Accountscreen.vue'));
Vue.component('detail_screen', require('./Detailscreen.vue'));
Vue.component('checkout_information', require('./CheckoutInformation.vue'));
Vue.component('checkout_payment', require('./CheckoutPayment.vue'));
Vue.component('checkout_confirmation', require('./CheckoutConfirmation.vue'));
Vue.component('admin_panel', require('./Admin/AdminPanel.vue'));
Vue.component('adminplotmenu', require('./AdminPlotMenu.vue'));
Vue.component('adminplot1', require('./AdminPlot1.vue'));
Vue.component('adminplot2', require('./AdminPlot2.vue'));
Vue.component('adminplot3', require('./AdminPlot3.vue'));

window.context = new Context();

new Vue({
    el: '#app',
    data :{
        show_login:false,
        show_favourites: true,
        show_register:false,
        show_products: false,
        show_product_details: false,
        show_shoppingcart_screen: false,
        show_checkout_information: false,
        show_checkout_payment: false,
        show_checkout_confirmation: false,
        show_account: false,
        show_detail: false,
        on_product_section: false,
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
        showFavourites: function(){
            console.log('Main function! GULULU!');
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
                if(base.user_status.Role == "Admin"){
                    base.IsAdmin = true;
                } else if(base.user_status.Role == "User"){
                    base.show_product_section();
                    //base.show_account_page();
                } else {
                    base.show_product_section();
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

        },
        begin_checkout: function() {
            if(this.LogedIn){
                this.show_shoppingcart_screen = false;
                this.show_checkout_information = true;
            }else{
                alert('Please log in to purchase our products.');
            }
        },
        begin_payment: function() {
            this.show_checkout_information = false;
            this.show_checkout_payment = true;
        },
        begin_confirmation: function() {
            this.show_checkout_payment = false;
            this.show_checkout_confirmation = true;
        },
        begin_order: function() {
            this.show_checkout_confirmation = false;
            this.on_product_section = true;


            var ean_list = [];
            var amt_list = [];
            var items = window.context.ShoppingCart.cart.CartLines;

            items.forEach(function(item){
                ean_list.push(item.Product.EAN);
                amt_list.push(item.Amount);
            });

            var order = {
                EAN: ean_list,
                Amounts: amt_list,
                Email: this.tempstore_inputs[7],
                DeliveryCity: this.tempstore_inputs[5],
                DeliveryCountry: this.tempstore_inputs[6],
                DeliveryHousenumber: this.tempstore_inputs[3],
                DeliveryPostalCode: this.tempstore_inputs[2],
                DeliveryStreetname: this.tempstore_inputs[4],
                BillingCity: this.tempstore_inputs[5],
                BillingCountry: this.tempstore_inputs[6],
                BillingHousenumber: this.tempstore_inputs[3],
                BillingPostalCode: this.tempstore_inputs[2],
                BillingStreetname: this.tempstore_inputs[4]
            };

            var base = this;

            var xhr = new XMLHttpRequest();
            xhr.open("POST", "/api/order/submit");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                //base.user_status = JSON.parse(xhr.response);
            };
            xhr.send(JSON.stringify(order));

            this.shoppingcart.clearCart();
            this.tempstore_inputs = null;
        },
        get_orders: function(){
            var base = this;

            var xhr = new XMLHttpRequest();
            xhr.open("GET", "/api/user/orders/");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                base.tempstore_orders = JSON.parse(xhr.response);
            };

            xhr.send();

        },
        show_order_detail: function(order){
            this.show_account = false;
            this.show_detail = true;
            this.tempstore_order = order;
        },
        show_account_page: function(){
            this.show_account = true;
            this.show_detail = false;
            this.on_product_section = false;
            this.tempstore_order = null;
        },
        show_product_section: function(){
            this.on_product_section = true;
            this.show_account = false;
        }
    },
    created: function () {
        this.check_login();
    }
});