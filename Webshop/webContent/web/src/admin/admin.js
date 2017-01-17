window += function Send(data, url, method){
    return new Promise(function(resolve, reject){
        var xhr = new XMLHttpRequest();
        xhr.open(method, url);

        xhr.onload = function(){
            if(xhr.status === 200){
                resolve(xhr.response);
                console.log(JSON.stringify(xhr.responseText));
            }else{
                reject(`An error occurred: ${xhr.statusText}`)
            }
        };

        xhr.onerror = function(){
            reject('Probably a network error!');
        };

        xhr.send(JSON.stringify(data));
    });
}