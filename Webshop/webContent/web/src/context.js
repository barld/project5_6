import ShoppingCartGateway from "./ShoppingCartGateway";
import DatabaseOperationsGateway from "./DatabaseOperationsGateway"
import StatisticsGateway from "./StatisticsGateway";

export default class Context{
    constructor(){
        this.ShoppingCart = new ShoppingCartGateway();
        this.DatabaseOperations = new DatabaseOperationsGateway();
        this.Statistics = new StatisticsGateway();
    }
}
