window.onload = function(){

	// var element = {
	// 	type: "div",
	// 	properties:{
	// 		id: "product-parent",
	// 		class: "container"
	// 	},
	// 	constructElement: function(){
	// 		return this.properties.id;
	// 	}
	// }



	// console.log(element.constructElement());

	// Create a sample product list
	var popularProducts = {
		1: new Product(1, "Zeeland Rescue: Barld", 25.00, "GameBoy Color"),
		2: new Product(2, "Pokemon Blue", 25.00, "GameBoy Color"),
		3: new Product(3, "Pokemon Yellow", 30.00, "GameBoy Color"),
		4: new Product(4, "Pokemon Mystery Dungeon", 45.00, "3DS")
	}

	
	console.log(JSON.parse(JSON.stringify(popularProducts)));

	var app = new Vue({
		el: "#games_overview",
		data:{
			popularProducts: popularProducts
		}
	});

	// Loop through the object
	// Object.keys(productList).forEach(function(key){
	// });
}