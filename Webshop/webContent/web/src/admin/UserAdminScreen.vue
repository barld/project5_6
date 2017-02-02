<template>
    <div class="panel">
        <table style="margin-top: 20px;" class="u-full-width">
            <thead>
                <th style="width: 32%">Username</th>
                <th style="width: 17%">Gender</th>
                <th style="width: 17%">Role</th>
                <th style="width: 17%">Edit</th>
                <th style="width: 17%; margin-right: 10px">Delete</th>
            </thead>
            <tbody>
                <user_admin_row :user="user" v-for="user in users" ></user_admin_row>
            </tbody>
        </table>
        <button class="admin_button button-primary line_break" @click="$emit('close')">close</button>
    </div>
</template>

<script>
    export default{
        data: function () {
            return {
                users: []
            }
        },
        methods: {
            onUserListChanged: function (users) {
                this.users = users
            },
        },


        created: function () {
            window.context.AdminUser.RegisterOnUsersListChanged(this.onUserListChanged);
            window.context.AdminUser.UpdateAllUser();
        }
    }
</script>