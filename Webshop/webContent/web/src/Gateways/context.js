import ShoppingCartGateway from "./ShoppingCartGateway";
import StatisticsGateway from "./StatisticsGateway";

export default class Context{
    constructor(){
        this.ShoppingCart = new ShoppingCartGateway();
        this.Statistics = new StatisticsGateway();
    }
}
