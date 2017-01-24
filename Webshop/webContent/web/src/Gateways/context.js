import ShoppingCartGateway from "./ShoppingCartGateway";
import OrderGateway from "./OrderGateway";

export default class Context{
    get Order() {
        return this._Order;
    }
    get ShoppingCart() {
        return this._ShoppingCart;
    }
    constructor(){
        this._ShoppingCart = new ShoppingCartGateway();
        this._Order = new OrderGateway();
    }
}
