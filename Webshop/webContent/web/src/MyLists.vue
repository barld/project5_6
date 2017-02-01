<template>
    <div>
        <h1>My Lists</h1>
        <table v-for="listTitle in MyLists" class="table table-condensed">
            <thead>
                <tr>
                    <th>{{listTitle.TitleOfList}} <label v-if="listTitle.TitleOfList === 'Wish List'">Check to make this list public: <input @click="togglePrivate(listTitle._id)" v-if="listTitle.TitleOfList === 'Wish List'" type="checkbox" v-model="checked"></input></label></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="list in listTitle.Games">
                    <td>{{list.GameTitle}}</td>
                </tr>
            </tbody>
        </table>
        <button style="display: block;" @click="mylists">Refresh list(s)</button>
    </div>
</template>

<script>
    export default{
        data: function () {
            return {
                MyLists: null,
                checked: true
            }
        },
        methods: {
            mylists: function () {
                var base = this;
                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/RetrieveMyLists/");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.send();


                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    if (JSON.parse(xhr.response) != null) {
                        base.MyLists = JSON.parse(xhr.response);
                    }
                }
            },
            togglePrivate: function (id) {
                var base = this;
                var xhr = new XMLHttpRequest();
                xhr.open("POST", "/api/user/ToggleSharedWishList/");

                // The RequestHeader can be any, by the server accepted, file
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                var listInformation = {_id: id};
                xhr.send(JSON.stringify(listInformation));


                // Function to fire off when the server has send a response
                xhr.onload = function () {
                    if (JSON.parse(xhr.response) != null) {
                        base.checked = !JSON.parse(xhr.response);
                        if (base.checked) {
                            var baseURL = window.location.hostname;
                            window.prompt("Deel deze link met anderen: ", `${baseURL}:8080/share_list.html?id=${id}`);
                        }
                    }
                }
            }
        },
        watch: {
            MyLists: function (value) {
                this.checked = !value[0].IsPrivate;
            }
        },
        created:function () {
            this.mylists();
        }
    }
</script>