import Vue from 'vue'
import adminContext from "./Gateways/adminContext";

Vue.component('admin_panel', require('./Admin/AdminPanel.vue'));
Vue.component('admin_products', require('./Admin/Product/ProductOverview.vue'));
Vue.component('admin_add_products', require('./Admin/Product/AddProduct.vue'));
Vue.component('user_admin_screen', require('./admin/UserAdminScreen.vue'));

window.context = new adminContext();

new Vue({
    el: '#app'
});

function Send(data, url, method){
    return new Promise(function(resolve, reject){
        var xhr = new XMLHttpRequest();
        xhr.open(method, url);

        xhr.onload = function(){
            if(xhr.status === 200){
                resolve(xhr.response);
                console.log(JSON.stringify(xhr.responseText));
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