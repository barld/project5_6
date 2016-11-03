// LoadProducts();
//
// var gamesList = [];
//
// var app = new Vue({
// 	el: "#app",
// 	data:{
// 		popularGames: gamesList,
// 		message: "Hallo",
// 		productDetails: false,
// 		chosenProduct: "",
// 		chosenProductFilled: false,
// 		showLogin: false,
//         showRegister: false
// 	},
// 	methods: {
// 		getProductPlatform: function(){
// 		    LoadProductsByPlatform();
// 		},
// 		showProductDetails: function(product){
// 			this.productDetails = true;
// 			var game = new Game({id: product.game.data.id, name: product.game.data.name, image: product.game.image, platforms: product.game.data.platforms});
// 			this.chosenProduct = game;
// 			this.chosenProductFilled = true;
// 		},
// 		showLoginScreen: function(){
// 			this.showLogin = true;
// 		},
// 		ajaxLogin: function (e) {
// 		    e.preventDefault();
// 		    var userInformation = { email: document.getElementById("login_email").value, password: document.getElementById("login_password").value };
//
// 		    formHandler.startCall({
// 		        requestHeader: 'application/json',
// 		        method: 'POST',
// 		        url: 'http://localhost:8080/api/user/login/',
// 		        data: { email: userInformation.email, password: userInformation.password },
// 		        ajaxFunction: function (data) {
// 		            var feedback = JSON.parse(data);
//
// 		            if (feedback.Success) {
// 		                document.getElementById("loginFeedback").innerHTML = "Login successful!";
// 		                document.getElementById("login_screen").style.display = "none";
// 		                document.getElementById("user_login_nav").style.display = "none";
// 		                document.getElementById("user_profile").style.display = "block";
// 		            } else {
// 		                document.getElementById("loginFeedback").innerHTML = feedback.Message;
// 		            }
// 		        }
// 		    });
// 		},
// 		storeUsername: function () {
// 			localStorage.setItem("login_email", document.getElementById("login_email").value);
// 		},
// 		searchGame: function () {
// 			var searchValue = document.getElementById("game_search").value;
//
//             if (searchValue.length > 3) {
//                 SearchGame(searchValue);
//             }
// 		}
// 	}
// });
//
// displayUsername();