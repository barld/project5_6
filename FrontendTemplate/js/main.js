class Product{
	constructor(id, name, price, platform){
		this.id = id;
		this.name = name;
		this.price = price;
		this.platform = platform;
	}

	get Id(){
		return this.id;
	}

	get Name(){
		return this.name;
	}

	get Price(){
		return this.price;
	}

	get Platform(){
		return this.platform;
	}
}

window.onload = function(){
	var shoppingCart = {data: [
			new Product(1, "Pokemon Red", 25.00, "GameBoy Color"),
			new Product(1, "Pokemon Blue", 25.00, "GameBoy Color"),
			new Product(1, "Pokemon Yellow", 30.00, "GameBoy Color")
		]};
}