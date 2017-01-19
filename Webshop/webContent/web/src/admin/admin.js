function Send(data, url, method){
    return new Promise(function(resolve, reject){
        var xhr = new XMLHttpRequest();
        xhr.open(method, url);

        xhr.onload = function(){
            if(xhr.status === 200){
                resolve(xhr.response);
                console.log(JSON.stringify(xhr.responseText));
            }else{
                reject(`An error occurred: ${xhr.statusText}`);
            }
        };

        xhr.onerror = function(){
            reject('Probably a network error!');
        };

        xhr.send(JSON.stringify(data));
    });
}

class DatabaseOperations{
    static EditProduct(product){
        Send(product, '/api/product/game/edit', 'PUT');
    }

    static DeleteProduct(product){
        Send(product, '/api/product/game/delete/', 'DELETE');
    }

    static InsertProduct(product){
        Send(product, '/api/product/game', 'POST');
    }

    static InsertUser(user){
        Send(user, 'api/user/register', 'POST');
    }
}
