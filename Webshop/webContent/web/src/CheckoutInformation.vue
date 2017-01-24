<template>
    <div><!-- Start container -->
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
      					<input class="u-full-width" required="required" type="text" placeholder="Voornaam" id="voornaam" v-model="formData.voornaam">
      					<label for="achternaam">Achternaam</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Achternaam" id="achternaam" v-model="formData.achternaam">
      					<label for="postcode">Postcode</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Postcode" id="postcode" v-model="formData.postcode">
      					<label for="huisnummer">Huisnummer</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Huisnummer" id="huisnummer" v-model="formData.huisnummer">
      					<label for="straat">Straat</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Straat" id="straat" v-model="formData.straat">
	                </div>
	                <div class="six columns">
	                	<label for="stad">Stad</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Stad" id="stad" v-model="formData.stad">
      					<label for="land">Land</label>
      					<input class="u-full-width" required="required" type="text" placeholder="Land" id="land" v-model="formData.land">
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
        data: function () {
            return {
                formData: {
                    voornaam: "",
                    achternaam: "",
                    postcode: "",
                    huisnummer: "",
                    straat: "",
                    stad: "",
                    land: "",
                }
            }
        },
        methods: {
            payment: function () {
                let IsComplete = this.formData.voornaam !== "" && this.formData.achternaam !== "" && this.formData.postcode !== ""
                    && this.formData.huisnummer !== "" && this.formData.straat !== "" && this.formData.stad !== "" && this.formData.land !== "";


                if (IsComplete) {
                    window.context.Order.CheckoutInformation = this.formData;
                    this.$emit("payment");
                } else {
                    alert('Please fill in all fields');
                }

            },
        }
    }
</script>