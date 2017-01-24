<template>
    <div class="container margin-top"><!-- Start container -->
        <div class="container">
        	<div class="row">
                <div class="ten.columns offset-by-two-columns">
                    <h4 align="center">Winkelwagen | <strong>Informatie</strong> | Betalingswijze | Bevestiging</h4>
                </div>
                <hr>
            </div>
        </div>
        <div class="container">
        	<div class="row">
	            <form action="#">
	                <div class="six columns">
	                	<label for="voornaam">Voornaam</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Voornaam" id="voornaam">
      					<label for="achternaam">Achternaam</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Achternaam" id="achternaam">
      					<label for="postcode">Postcode</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Postcode" id="postcode">
      					<label for="huisnummer">Huisnummer</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Huisnummer" id="huisnummer">
      					<label for="straat">Straat</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Straat" id="straat">
	                </div>
	                <div class="six columns">
	                	<label for="stad">Stad</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Stad" id="stad">
      					<label for="land">Land</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Land" id="land">
      					<label for="email">E-mail</label>
      					<input class="u-full-width" required="required" type="email" placeholder="Email" id="email">
      					<label for="tel">Telefoon nummer</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Telefoonnummer" id="tel">
	                </div>
            </div>
            <hr>
            <br>
	      	<input type="submit" class="button-primary" @click="payment" value="Verder naar betalingswijze!">
	        </form>
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
            payment:function(){
                var IsComplete  = true;

                var inputs = [
                    document.getElementById("voornaam").value,
                    document.getElementById("achternaam").value,
                    document.getElementById("postcode").value,
                    document.getElementById("huisnummer").value,
                    document.getElementById("straat").value,
                    document.getElementById("stad").value,
                    document.getElementById("land").value,
                    document.getElementById("email").value,
                    document.getElementById("tel").value,
                ]

                inputs.forEach(function(input){
                    if(input == ""){
                        IsComplete = false;
                    }
                })

                if(IsComplete){
                    this.$emit("payment", inputs);
                }else{
                    alert('Please fill in all fields');
                }
                
            },
        }
    }
</script>