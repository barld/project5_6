<template>
    <div>
        <div v-if="show_admin_screen">
            <h1 align="center">Admin Panel</h1>
            <div class="row">
                <div class="ten.columns offset-by-two-columns">
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
                                <tr v-for="user in users">
                                    <td><p>{{user.Email}}</p></td>
                                    <td><p>{{user.Gender}}</p></td>
                                    <td><p>{{user.AccountRole}}</p></td>
                                    <td><a href="#"><button class="button-primary">Edit</button></a></td>
                                    <td><a href="#"><button class="button-primary">Delete</button></a></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default{
        data:function(){
            return{
                show_admin_screen: true,
                show_admin_plot_menu: false,
                plot_number: 0,
                users:[]
            }
        },
        methods:{
            ShowPlot:function(val){
                if(Number.isInteger(val) && val > 0 && val < 4){
                    this.show_admin_screen = false;
                    this.plot_number = val;
                }
            },
            ClosePlots:function(){
                this.show_admin_screen = true;
                this.plot_number = 0;
            }
        },
        created:function () {
            window.context.AdminUser.GetAllUser(users => this.users = users);
        }
    }
</script>