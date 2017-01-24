/**
 * Created by barld on 24-1-2017.
 */
import context from "./context";
import AdminUserGateway from "./AdminUserGateway";

export default
    class adminContext extends context{
        get AdminUser() {
            return this._AdminUser;
        }

        constructor(){
            super();
            this._AdminUser = new AdminUserGateway();
        }
    }