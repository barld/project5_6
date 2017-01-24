<template>
    <div id="mobile_bar">
        <i class="fa fa-bars fa-3x right menu_lines" aria-hidden="true" @click="Toggle"></i>
        <div id="mobile_nav_content" v-if="visible">
            <a href="#" class="button afrekenen"><i class="fa fa-shopping-cart" aria-hidden="true"></i>Details winkelwagen</a>
            <a href="#" class="button afrekenen"><i class="fa fa-user" aria-hidden="true"></i> Mijn profiel</a>
            <a href="#" class="button afrekenen"><i class="fa fa-envelope-open" aria-hidden="true"></i>Mijn bestellingen</a>
        </div>
    </div>
</template>

<script>
    export default{
        data: function(){
            return{
                visible: false
            }
        },
        methods:{
            Toggle:function(){
                (this.visible === false) ? this.visible = true : this.visible = false
            }
        }
    }
</script>