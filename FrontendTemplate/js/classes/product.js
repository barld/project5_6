class Game{
	constructor(data = {}){
		this.data = data;
		this.id = data.id;
		this.name = data.name;
		this.image = data.image;
		this.platforms = data.platforms;
	}
}