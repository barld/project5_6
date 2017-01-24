/**
 * Created by barld on 24-1-2017.
 */

export default
    class AdminUserGateway{

        GetAllUser(callback){
            var xhr = new XMLHttpRequest();
            xhr.open("GET", "/api/userAdmin/AllUsers/");

            // The RequestHeader can be any, by the server accepted, file
            xhr.setRequestHeader('Content-type', "Application/JSON", true);

            // Function to fire off when the server has send a response
            xhr.onload = function () {
                callback(JSON.parse(xhr.response));
            };

            xhr.send();
        }
    }