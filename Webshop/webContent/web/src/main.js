import Vue from 'vue'
import Context from "./Gateways/context";

//import App from './App.vue'
//import ShoppingcartMenu from './ShoppingcartMenu.vue'

import './components'

window.context = new Context();

new Vue({
    el: '#app',
    data :{
        event_bus: new Vue(),
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
        },
        login_success:function () {
            this.closeLogin();
            this.check_login();
            this.event_bus.$emit("user_logged_in",1);
        },
        logedout:function () {
            this.LogedIn = false;
            this.IsAdmin = false;
            this.event_bus.$emit("user_logged_out",1);
        },
        showFavourites: function(){
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
                    //base.IsAdmin = true;
                    base.show_product_section();
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

        },
        show_order_detail: function(order){
            this.show_account = false;
            this.show_detail = true;
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