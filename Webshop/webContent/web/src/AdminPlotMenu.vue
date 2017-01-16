<template>
    <div class="three columns product">
        <div style="margin: 0 auto;">
            <h1>Plot menu</h1>
            <button>Plot 1</button>
            <button>Plot 2</button>
            <button>Plot 3</button>
        </div>
    </div>
</template>
<script>
    export default{
        data(){
            return{
                msg:'hello vue'
            }
        },
        components:{
            'other-component':OtherComponent,
            HeaderComponent,
        }
    }
</script>
