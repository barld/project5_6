/**
 * Created by barld on 23-1-2017.
 */

export default
    class OrderGateway {

        constructor() {
            this._cInfo = undefined;
        }

        set CheckoutInformation(info) {
            console.log(info);
            this._cInfo = info;
        }

        get CheckoutInformation() {
            return this._cInfo;
        }

        CheckOut(){
            let base = this;

            let xhr = new XMLHttpRequest();
            xhr.open("POST", "/api/order/submit");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                //base.user_status = JSON.parse(xhr.response);
            };
            xhr.send(JSON.stringify(this.CheckoutInformation));

            window.context.ShoppingCart.clearCart();
            this.CheckoutInformation = undefined;
        }

        GetOrders(onNewOrders){
            var base = this;

            var xhr = new XMLHttpRequest();
            xhr.open("GET", "/api/user/orders/");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                onNewOrders(JSON.parse(xhr.response));
            };

            xhr.send();
        }
    }