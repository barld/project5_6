function getHashValue(){
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
}