import ShoppingCartGateway from "./ShoppingCartGateway";
import DatabaseOperationsGateway from "./DatabaseOperationsGateway"

export default class Context{
    constructor(){
        this.ShoppingCart = new ShoppingCartGateway();
        this.DatabaseOperations = new DatabaseOperationsGateway();
    }
}
