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

    static DeleteProduct(ProductId){
        Send(ProductId, '/api/product/game/delete/id', 'DELETE');
    }

    static InsertProduct(product){
        Send(product, '/api/product/game', 'POST');
    }

    static InsertUser(use){
        Send(user, 'api/user', 'POST');
    }
}

var product =
{
    "GameTitle" : "Battlefield 1",
    "Platform" : {
        "_id" : "587c6b859742de257459c362",
        "PlatformTitle" : "Playstation 4",
        "Brand" : "SONY",
        "Description" : "Japan for the win!",
        "Abbreviation" : "PS4"
    },
    "RatingPEGI" : 13,
    "Publisher" : [
        "EA"
    ],
    "Genres" : [
        {
            "Name" : "Action",
            "Description" : "Boom boom!"
        }
    ],
    "Image" : [
        "https://content.pulse.ea.com/content/battlefield-portal/nl_NL/news/battlefield-1/battlefield-1-beta-thank-you/_jcr_content/featuredImage/renditions/rendition1.img.jpg"
    ],
    "MinPlayers" : 1,
    "MaxPlayers" : 12,
    "Description" : "Full of action!",
    "EAN" : Math.random(),
    "Price" : 4500,
    "IsVRCompatible" : false,
    "ReleaseDate" : "2017-01-16T06:43:17"
};

var user = {
    "Password" : "maarten",
    "Email" : "maarten@maarten.nl",
    "Gender" : 0,
    "AccountRole" : 1,
    "IsActive" : true
};

DatabaseOperations.InsertProduct(product);
DatabaseOperations.InsertUser(user);