import Vue from 'vue'
//import App from './App.vue'
//import ShoppingcartMenu from './ShoppingcartMenu.vue'
import dc from './dc/dc.vue'

Vue.component('dc', dc);

Vue.component('userlogedinnav', require('./UserLogedinNav.vue'));
Vue.component('userlogedoutnav', require('./UserLogedoutNav.vue'));
Vue.component('usernav', require('./UserNav.vue'));
Vue.component('popup', require('./popup.vue'));
Vue.component('shopcart', require('./ShoppingcartMenu.vue'));
Vue.component('platform_menu', require('./PlatformMenu.vue'));
Vue.component('login', require('./LoginScreen.vue'));
Vue.component('register', require('./RegisterScreen.vue'));
Vue.component('search', require('./SearchScreen.vue'));

new Vue({
    el: '#app',
    data :{
        show_login:false,
        show_register:false,
        LogedIn:false,
    },
    methods:{
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
            console.log('success');
            this.closeLogin();
        },
        showRegister:function(){
            this.show_register = true;
        },
        closeRegister:function () {
            this.show_register = false;
        },

    }
});
