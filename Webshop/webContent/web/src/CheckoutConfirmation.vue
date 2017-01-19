<template>
    <div><!-- Start container -->
        <div class="container">
        	<div class="row">
                <div class="ten.columns offset-by-two-columns">
                	<h4 align="center"><a style="color: inherit; text-decoration:none" href="#">Winkelwagen</a> |  
                	<a href="#" style="color: inherit; text-decoration:none">Informatie</a> | <a style="color: inherit; text-decoration:none" href="#">Betalingswijze</a> | <strong>Bevestiging</strong></h4>
                </div>
            </div>
        </div>
        <div class="container">
        	<div class="row">
        		<div class="six columns">
        		<div class="element-container">
        			
        			<table class="u-full-width">
        				<h3 align="center">Gegevens</h3>
        				<hr>
        				<tbody>
        					<tr>
        						<td><strong>Voornaam</strong></td>
                                <td>{{ inputs[0] }}</td>
        					</tr>
        					<tr>
        						<td><strong>Achternaam</strong></td>
        						<td>{{ inputs[1] }}</td>
        					</tr>
        					<tr>
        						<td><strong>Postcode</strong></td>
        						<td>{{ inputs[2] }}</td>
        					</tr>
        					<tr>
        					 	<td><strong>Huisnummer</strong></td>
        					 	<td>{{ inputs[3] }}</td>
        					</tr>
        					<tr>
        						<td><strong>Straat</strong></td>
        						<td>{{ inputs[4] }}</td>
        					</tr>
        					<tr>
        						<td><strong>Stad</strong></td>
        						<td>{{ inputs[5] }}</td>
        					</tr>
        					<tr>
        						<td><strong>Land</strong></td>
        						<td>{{ inputs[6] }}</td>
        					</tr>
        					<tr>
        						<td><strong>E-mail</strong></td>
        						<td>{{ inputs[7] }}</td>
        					</tr>
        					<tr>
        						<td><strong>Telefoon nummer</strong></td>
        						<td>{{ inputs[8] }}</td>
        					</tr>
        				</tbody>
        			</table>
        		</div>
        		</div>

        		<div class="six columns">
        			<div class="element-container">
        				<h3 align="center">Bestelling</h3>
        				<hr>
        				<table class="u-full-width">
        					<thead>
	        					<tr>
	        						<th></th>
	        						<th>Naam</th>
	        						<th>Aantal</th>
	        						<th>Prijs</th>
	        					</tr>
        					</thead>
        					<tbody>
        						<tr>
        							<td><img class="img-thumbnail" src="/images/battlefield.jpg"></td>
	                                <td>Battlefield</td>
	                                <td>2</td>
	                                <td>59.95</td>
        						</tr>
        						<tr>
        							<td><img class="img-thumbnail" src="/images/rome.jpg"></td>
	                                <td>Rome Total War</td>
	                                <td>1</td>
	                                <td>5.95</td>
        						</tr>
        					</tbody>
        				</table>
        				<table class="u-full-width">
        					<tbody>
        						<tr>
        							<td><strong>Sub-totaal:</strong></td>
        							<td>125.85</td>
        						</tr>
        						<tr>
        							<td><strong>BTW (21%):</strong></td>
        							<td>27.06</td>
        						</tr>
        						<tr>
        							<td></td>
        							<td>____</td>
        						</tr>
        						<tr>
        							<td><strong>Totaal:</strong></td>
        							<td>125.85</td>
        						</tr>
        					</tbody>
        				</table>
        			</div>
        		</div>
        	</div>
        </div>
    </div><!-- End container -->
</template>

<script>
    export default{
        props:['inputs', 'shoppingcart'],
        data:function(){
            return{
            }
        },
        methods:{
        },
        created:function(){
            //alert("hello");
            console.log(this.shoppingcart);
        }
    }
</script>