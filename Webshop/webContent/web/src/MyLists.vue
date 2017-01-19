<template>
    <div v-if="user_status.Email">
        <h1>My Lists</h1>
        <table v-for="listTitle in MyLists" class="table table-condensed">
            <thead>
                <tr>
                    <th>{{listTitle.TitleOfList}}</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <div v-for="list in listTitle.Games">
                        <td>{{list.GameTitle}}</td>
                    </div>
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
                show_favourites: true,
                MyLists: null
            }
        },
        methods:
        {
            mylists: function()
            {
                var base = this
                console.log(this.user_status);
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
                        console.log(base.MyLists);
                    }
                }
            }
        },
        watch : {
            user_status : function (value) {
            this.mylists();
            }
      }
    }
</script>