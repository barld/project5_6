window.onload = function () {

	// Sampledata
	var popularGames = {
		1:{
			id: 20172017,
			name: "FIFA 2017",
			image: "https://s.s-bol.com/imgbase0/imagebase3/large/FC/6/2/7/7/9200000059237726.jpg",
			description: "EA brengt nu een geweldige game op de markt.",
			pegi_rating: "",
			min_players: 1,
			max_players: 4,
			platforms:{
				1:{
					name: "PC",
					price: 60.00, 
					release_date: "29-09-2016"
				},
				2:{
					name: "PlayStation 4",
					price: 65.00, 
					release_date: "31-09-2016"
				},
				3:{
					name: "3DS",
					price: 45.00, 
					release_date: "15-10-2016"
				},
			},
		},
		2:{
			id: 3929029,
			name: "Battlefield 1",
			image: "https://upload.wikimedia.org/wikipedia/en/f/fc/Battlefield_1_cover_art.jpg",
			description: "Nog een geweldig spel van EA",
			pegi_rating: "",
			min_players: 1,
			max_players: 30,
			platforms:{
				1:{
					name: "PC",
					price: 65.00, 
					release_date: "29-09-2016"
				},
				2:{
					name: "PlayStation 4",
					price: 65.00, 
					release_date: "31-09-2016"
				},
				3:{
					name: "Wii U",
					price: 45.00, 
					release_date: "15-10-2016"
				},
			}
		},
		3:{
			id: 3929029,
			name: "Battlefield 1",
			image: "https://upload.wikimedia.org/wikipedia/en/f/fc/Battlefield_1_cover_art.jpg",
			platforms:{
				1:{
					name: "PC",
					price: 65.00, 
					release_date: "29-09-2016"
				},
				2:{
					name: "PlayStation 4",
					price: 65.00, 
					release_date: "31-09-2016"
				},
				3:{
					name: "Wii U",
					price: 45.00, 
					release_date: "15-10-2016"
				},
			}
		},
		4:{
			id: 3929029,
			name: "Pokemon Sun/Moon",
			image: "https://s.aolcdn.com/hss/storage/midas/4196e577c51940e12ffb2ecbc9106bb6/203799629/pokemonsunmoon.png",
			description: "EEN NIEUW POKEMON SPEL, DIE MOET IEDEREEN.",
			pegi_rating: "",
			min_players: 1,
			max_players: 4,
			platforms:{
				1:{
					name: "DS",
					price: 45.00, 
					release_date: "23-11-2016"
				}
			}
		}
	};

	var gamesList = [];

	Object.keys(popularGames).forEach(function(key){
		var game = new Game(popularGames[key]);
		gamesList.push({game});
	});

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
		methods:{
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
			ajaxLogin: function(){
			}
		}
	});

	app.closePopup();

	var userInformation = {username: "Andy", password: "password"};
	var formHandler = new FormHandler();

	formHandler.addValue('username', userInformation.username);
	formHandler.addValue('password', userInformation.password);

	formHandler.startCall({
	    requestHeader: 'application/json',
        method: 'GET',
	    url: 'http://localhost:8080/api/product',
	    ajaxFunction: function () {
	        console.log("Hoi, Barld! Dit is een callback function!");
	    }
	});
}