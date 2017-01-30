/**
 * Created by barld on 24-1-2017.
 */

export default
    class AdminUserGateway{

        constructor(){
            this._userListChangedActions = [];
        }

        RegisterOnUsersListChanged(action){
            this._userListChangedActions.push(action);
        }

        _executeOnUsersListChanged(users){
            this._userListChangedActions.forEach(action => action(users));
        }

        UpdateAllUser(){
            let base = this;
            let xhr = new XMLHttpRequest();
            xhr.open("GET", "/api/userAdmin/AllUsers/");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                base._executeOnUsersListChanged(JSON.parse(xhr.response));
            };

            xhr.send();
        }

        DeleteUser(user){
            let base = this;
            let xhr = new XMLHttpRequest();
            xhr.open("DELETE", "/api/userAdmin/user/");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                base.UpdateAllUser();
            };

            xhr.send(JSON.stringify(user));
        }

        UpdateUser(user){
            let base = this;
            let xhr = new XMLHttpRequest();
            xhr.open("PUT", "/api/userAdmin/user/");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                base.UpdateAllUser();
            };

            xhr.send(JSON.stringify(user));
        }
    }