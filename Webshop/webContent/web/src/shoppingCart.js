/**
 * Created by barld on 7-11-2016.
 */

export default
    class shoppingCart{
        //var cartLines = [];
        constructor(){
            this._refreshData();
            this.OnChangedshoppingCartEvents = [];
            this.cart = {};
        }

        _refreshData() {
            var base = this;

            var xhr = new XMLHttpRequest();
            xhr.open("GET", "/api/shoppingcart");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);


            // Function to fire off when the server has send a response
            xhr.onload = function () {
                base.cart = JSON.parse(xhr.response);
                base._triggerOnChangedshoppingCart();
            };

            xhr.send();
        }

        registerOnChangedshoppingCart(f)
        {
            this.OnChangedshoppingCartEvents.push(f);
        }

        addToCart(product){
            var base = this;

            var xhr = new XMLHttpRequest();
            xhr.open("POST", "/api/shoppingcart/Add");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);


            // Function to fire off when the server has send a response
            xhr.onload = function () {
                base._refreshData();
            };

            xhr.send(JSON.stringify(product));
        }

        _triggerOnChangedshoppingCart(){
            this.OnChangedshoppingCartEvents.forEach(e => e(this))
        }
    }