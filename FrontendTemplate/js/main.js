window.onload = function(){

	// Sampledata
	var popularGames = {
		1:{
			id: 20172017,
			name: "FIFA 2017",
			image: "https://s.s-bol.com/imgbase0/imagebase3/large/FC/6/2/7/7/9200000059237726.jpg",
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
		}
	};

	var gamesList = [];

	Object.keys(popularGames).forEach(function(key){
		var game = new Game(popularGames[key]);
		console.log(game);
		gamesList.push({game});
	});

	// gamesList.forEach(game => console.log(game));

	var app = new Vue({
		el: "#app",
		data:{
			popularGames: gamesList,
			message: "Hallo"
		}
	});
}