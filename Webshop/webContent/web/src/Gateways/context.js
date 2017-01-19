import ShoppingCartGateway from "./ShoppingCartGateway";

export default class Context{
    constructor(){
        this.ShoppingCart = new ShoppingCartGateway();
    }
}
