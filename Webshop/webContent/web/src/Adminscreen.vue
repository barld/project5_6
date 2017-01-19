<template>
    <div class="container">
        <div class="ten.columns offset-by-two-columns"><h1 align="center">Admin Panel</h1></div>
            <div class="row">
                <div class="container">
                    <div class="ten.columns offset-by-two-columns">
                            <div class="panel">
                                <table style="margin-top: 20px;" class="u-full-width">
                                    <thead>
                                        <th style="width: 40%">Username</th>
                                        <th style="width: 20%">Gender</th>
                                        <th style="width: 20%">Edit</th>
                                        <th style="width: 20%; margin-right: 10px">Delete</th>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><p>JFConstancia@hotmail.com</p></td>
                                            <td><p>Male</p></td>
                                            <td><a href="#"><button class="button-primary">Edit</button></a></td>
                                            <td><a href="#"><button class="button-primary">Delete</button></a></td>
                                        </tr>
                                        <tr>
                                            <td><p>info@superict.nl</p></td>
                                            <td><p>Male</p></td>
                                            <td><a href="#"><button class="button-primary">Edit</button></a></td>
                                            <td><a href="#"><button class="button-primary">Delete</button></a></td>
                                        </tr>
                                        <tr>
                                            <td><p>hallo@420.com</p></td>
                                            <td><p>Female</p></td>
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
    </div><!-- End container -->
</template>

<script>
    export default{
        data:function(){
            return{
            }
        },
        methods:{}
    }
</script>