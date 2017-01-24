export default
    class ShoppingCartGateway{
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

        UpdateCart(cart){
            var base = this;

            var xhr = new XMLHttpRequest();
            xhr.open("PUT", "/api/shoppingcart/");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);


            // Function to fire off when the server has send a response
            xhr.onload = function () {
                base._refreshData();
            };

            xhr.send(JSON.stringify(cart));
        }

        _triggerOnChangedshoppingCart(){
            this.OnChangedshoppingCartEvents.forEach(e => e(this));
        }

        removeFromCart(product){
            var base = this;

            var xhr = new XMLHttpRequest();
            xhr.open("DELETE", "/api/shoppingcart/Remove");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);


            // Function to fire off when the server has send a response
            xhr.onload = function () {
                base._refreshData();
            };

            xhr.send(JSON.stringify(product));
        }

        clearCart(){
            var base = this;

            var xhr = new XMLHttpRequest();
            xhr.open("DELETE", "/api/shoppingcart/All");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                base._refreshData();
            };

            xhr.send();
        }
    }