<popup v-on:close="close" id="login_screen" class="login_screen">
    {{ product }}
</popup>

<script>
    export default{
        data: function(){
            return{
                product:[]
            }
        }
    }
</script>