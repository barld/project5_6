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