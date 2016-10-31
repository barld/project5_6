class Game{
	constructor(data = {}){
		this.data = data;
		this.id = data.id;
		this.name = data.name;
		this.image = data.image;
		this.description = data.description;
		this.pegi_rating = data.pegi_rating;
		this.min_players = data.min_players;
		this.max_players = data.max_players;
		this.platforms = data.platforms;
	}
}