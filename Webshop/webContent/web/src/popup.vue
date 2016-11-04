<template>
    <div class="popup" ><!-- Start login screen -->
        <div class="close_btn" v-on:click="close">
            <i class="fa fa-times fa-5x" aria-hidden="true"></i>
        </div>
        <div class="inner_padding">
            <slot></slot>
        </div>
    </div>
</template>

<script>
    export default{
        methods:{
            close: function () {
                this.$emit('close');
            }
        }
    }
</script>