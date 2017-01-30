<!--suppress ALL -->
<template >
    <tr>
        <td v-if="displayEdit" ><input type="text" v-model="user.Email"/></td>
        <td v-else><p>{{user.Email}}</p></td>
        <td v-if="displayEdit" >
            <select v-model="user.Gender">
                <option value="Male">Male</option>
                <option value="Female">Female</option>
                <option value="Unknown">Unknown</option>
            </select>
        </td>
        <td v-else><p>{{user.Gender}}</p></td>
        <td v-if="displayEdit" >
            <select v-model="user.AccountRole">
                <option value="Admin">Admin</option>
                <option value="User">User</option>
            </select>
        </td>
        <td v-else><p>{{user.AccountRole}}</p></td>
        <td v-if="displayEdit"><button @click="saveUser" class="button-primary" >Save</button></td>
        <td v-else><button @click="editUser" class="button-primary" >Edit</button></td>
        <td><button @click="deleteUser" class="button-primary">Delete</button></td>
    </tr>
</template>

<script>
    export default {
        props:['user'],
        data:function () {
            return{
                displayEdit: false
            }
        },
        methods:{
            deleteUser: function () {
                window.context.AdminUser.DeleteUser(this.user);
            },
            editUser: function () {
                this.displayEdit = true;
            },
            saveUser: function () {
                this.displayEdit = false;
                window.context.AdminUser.UpdateUser(this.user)
            }
        }
    }
</script>