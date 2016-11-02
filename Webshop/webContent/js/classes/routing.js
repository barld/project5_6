var formHandler = new FormHandler();

function getHashValue() {
	if(window.location.hash){
		console.log(window.location.hash.substring(10));
	}
}

class Router {
    getProductId() {
        if (window.location.hash) {
            var productId = window.location.hash.substring(10);
            return productId;
        }
    }

    getProductByPlatform() {
        if (window.location.hash) {
            var productPlatform = window.location.hash.substring(10);
            formHandler.startCall({
                requestHeader: 'application/json',
                method: 'GET',
                url: 'http://localhost:8080/api/product/platform/' + productPlatform,
                data: formHandler,
                ajaxFunction: function (data) {
                    console.log(data);
                }
            });
        }
    }
}