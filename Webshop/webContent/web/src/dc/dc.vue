<template>
    <div>
        <keep-alive>
            <component v-bind:is="currentView">

            </component>
        </keep-alive>
        <button @click="click">click</button>
    </div>
</template>

<script>
    export default {
        data: function ()
        { return{
                currentView: 't1',
                cc: 0
            }
        },
        components:{
            t1: require('../PlatformMenu.vue'),
            t2: require('../ShoppingcartMenu.vue')
        },
        methods:{
            click: function () {
                this.cc++;
                this.cc = this.cc%2;
                this.currentView = 't'+(this.cc+1);
            }
        }

    }
</script>