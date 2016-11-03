import Vue from 'vue'
//import App from './App.vue'
//import ShoppingcartMenu from './ShoppingcartMenu.vue'

Vue.component('shopcart', require('./ShoppingcartMenu.vue'));
Vue.component('menu', require('./PlatformMenu.vue'));

new Vue({
  el: '#app',
  //render: h => {h(App)}
});
