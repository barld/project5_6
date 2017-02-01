import Vue from 'vue'
import adminContext from "./Gateways/adminContext";

Vue.component('admin_panel', require('./Admin/AdminPanel.vue'));
Vue.component('admin_products', require('./Admin/Product/ProductOverview.vue'));
Vue.component('admin_add_products', require('./Admin/Product/AddProduct.vue'));
Vue.component('user_admin_screen', require('./admin/UserAdminScreen.vue'));
Vue.component('user_admin_row', require('./admin/userAdminRow.vue'));
Vue.component('statistics_admin_screen', require('./admin/Statistics/AdminPlotMenu.vue'));
Vue.component('statistics_plot_1', require('./admin/Statistics/AdminPlot1.vue'));
Vue.component('statistics_plot_2', require('./admin/Statistics/AdminPlot2.vue'));
Vue.component('statistics_plot_3', require('./admin/Statistics/AdminPlot3.vue'));

window.context = new adminContext();

new Vue({
    el: '#app',
    data: {
        user_status: {}
    },
    methods:{
        check_login: function () {
            var base = this;

            var xhr = new XMLHttpRequest();
            xhr.open("GET", "/api/user/status/");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                base.user_status = JSON.parse(xhr.response);
            };

            xhr.send();
        },
    },
    created : function () {
        this.check_login();
    }
});

function Send(data, url, method){
    return new Promise(function(resolve, reject){
        var xhr = new XMLHttpRequest();
        xhr.open(method, url);

        xhr.onload = function(){
            if(xhr.status === 200){
                resolve(xhr.response);
            }else{
                reject(`An error occurred: ${xhr.statusText}`);
            }
        };

        xhr.onerror = function(){
            reject('Probably a network error!');
        };

        xhr.send(JSON.stringify(data));
    });
}

class DatabaseOperations{
    static EditProduct(product){
        Send(product, '/api/product/edit', 'PUT');
    }

    static DeleteProduct(ean){
        Send(ean, `/api/product/?ean=${ean}`, 'DELETE');
    }

    static InsertProduct(product){
        Send(product, '/api/product/', 'POST');
    }

    static InsertUser(user){
        Send(user, 'api/user/register', 'POST');
    }
}