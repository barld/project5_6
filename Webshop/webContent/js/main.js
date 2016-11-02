window.onload = function () {
	//Object.keys(popularGames).forEach(function(key){
	//	var game = new Game(popularGames[key]);
	//	gamesList.push({game});
    //});

    LoadProducts();

    var gamesList = [];

	var app = new Vue({
		el: "#app",
		data:{
			popularGames: gamesList,
			message: "Hallo",
			productDetails: false,
			chosenProduct: "",
			chosenProductFilled: false,
			showLogin: false
		},
		methods: {
		    getProductPlatform: function(){
		        LoadProductsByPlatform();
		    },
			closePopup: function(){
				document.body.addEventListener("keydown", function(e){
					if(e.keyCode === 27){
						this.productDetails = false;
						this.chosenProductFilled = false;
					}
				});
			},
			showProductDetails: function(product){
				this.productDetails = true;
				var game = new Game({id: product.game.data.id, name: product.game.data.name, image: product.game.image, platforms: product.game.data.platforms});
				this.chosenProduct = game;
				this.chosenProductFilled = true;
			},
			showLoginScreen: function(){
			    this.showLogin = true;
			},
			ajaxLogin: function (e) {
			    e.preventDefault();
			    Login();
			},
			storeUsername: function () {
			    localStorage.setItem("login_email", document.getElementById("login_email").value);
			},
			searchGame: function () {
			    var searchValue = document.getElementById("game_search").value;
			    
                if (searchValue.length > 3) {
                    SearchGame(searchValue);
                }
			}
		}
	});

	displayUsername();
}