<template>
    <div><!-- Start container -->
        <div class="container">
        	<div class="row">
                <div class="ten.columns offset-by-two-columns">
                	<h4 align="center"><a style="color: inherit; text-decoration:none" href="#">Winkelwagen</a> |  
                	<a href="#" style="color: inherit; text-decoration:none">Informatie</a> | <strong>Betalingswijze</strong> | Bevestiging</h4>
                </div>
                <hr>
            </div>
        </div>
        <div class="container">
        	<br>
        	<div class="row">
        		<!-- <div class="one column">&nbsp;</div> -->
        		<div class="five columns">
        			<a href="#" @click="confirmation"><img class="u-full-width" src="/images/paypal.png"></a>
        		</div>
        		<div class="one column">&nbsp;</div>
        		<div class="five columns">
        			<a href="#" @click="confirmation"><img class="u-full-width" src="/images/ideal.png"></a>
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
        methods:{
            confirmation:function(){
                this.$emit("confirmation");
            },
        }
    }
</script>