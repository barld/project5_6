import Vue from 'vue'
//import App from './App.vue'
//import ShoppingcartMenu from './ShoppingcartMenu.vue'
import dc from './dc/dc.vue'

Vue.component('dc', dc);

Vue.component('shopcart', require('./ShoppingcartMenu.vue'));
Vue.component('platform_menu', require('./PlatformMenu.vue'));
Vue.component('login', require('./LoginScreen.vue'));
Vue.component('register', require('./RegisterScreen.vue'));
Vue.component('search', require('./SearchScreen.vue'));
Vue.component('product', require('./ProductScreen.vue'));

new Vue({
  el: '#app',
  //render: h => {h(App)}
});
