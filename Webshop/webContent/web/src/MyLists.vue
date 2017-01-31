<template>
    <div v-if="user_status.Email">
        <h1>My Lists</h1>
        <table v-for="listTitle in MyLists" class="table table-condensed">
            <thead>
                <tr>
                    <th>{{listTitle.TitleOfList}} <input @click="togglePrivate(listTitle._id)" v-if="listTitle.TitleOfList === 'Wish List'" type="checkbox" v-model="checked"></input></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="list in listTitle.Games">
                    <td>{{list.GameTitle}}</td>
                </tr>
            </tbody>
        </table>
        <button @click="mylists">Refresh list(s)</button>
    </div>
</template>

<script>
    export default{
        props: ['user_status'],
        data: function() {
            return {
                MyLists: null,
                checked: false
            }
        },
        methods:
        {
            mylists: function()
            {
                var base = this;
                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/RetrieveMyLists/");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                var userInformation = {email:this.user_status.Email};
                xhr.send(JSON.stringify(userInformation));

                
                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    if(JSON.parse(xhr.response) != null){
                        base.MyLists = JSON.parse(xhr.response);
                    }
                }
            },
            togglePrivate: function(id)
            {
                var base = this;
                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/ToggleWishList/");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                var listInformation = {_id:id};
                xhr.send(JSON.stringify(listInformation));

                
                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    if(JSON.parse(xhr.response) != null){
                        base.checked = JSON.parse(xhr.response);
                        if(!base.checked)
                        {
                            window.prompt("Druk op CTRL+C", `localhost:8080/api/user/sharedwishlist/?id=${id}`);
                        }
                    }
                }
            }
        },
        watch : {
            user_status : function (value) {
                this.mylists();
            },
            MyLists : function (value)
            {
                this.checked = value[0].IsPrivate;
            }
      }
    }
</script>