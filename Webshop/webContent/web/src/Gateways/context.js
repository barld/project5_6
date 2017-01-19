/**
 * Created by barld on 19-1-2017.
 */
import ShoppingCartGateway from "./ShoppingCartGateway";

export default class Context{
    constructor(){
        this.ShoppingCart = new ShoppingCartGateway();
    }
}
