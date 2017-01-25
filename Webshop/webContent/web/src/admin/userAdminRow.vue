<template>
    <tr>
        <td><p>{{user.Email}}</p></td>
        <td><p>{{user.Gender}}</p></td>
        <td><p>{{user.AccountRole}}</p></td>
        <td><a href="#"><button class="button-primary">Edit</button></a></td>
        <td><a href="#" @click="deleteUser"><button class="button-primary">Delete</button></a></td>
    </tr>
</template>

<script>
    export default {
        props:['user'],
        methods:{
            deleteUser: function () {
                window.context.AdminUser.deleteUser(this.user);
            }
        }
    }
</script>