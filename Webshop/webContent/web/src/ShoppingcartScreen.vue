<template>
    <div class="container margin-top"><!-- Start container -->
        <div class="container"></div>
        <div class="ten columns"><h1 align="center">Jouw winkelwagen.</h1></div>
        <div class="ten columns">
            <table class="u-full-width">
                <thead>
                    <tr>
                        <th>&nbsp;</th>
                        <th>Naam</th>
                        <th width="30">Aantal</th>
                        <th width="230">Prijs</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><img class="img-thumbnail" src="images/battlefield.jpg"></td>
                        <td>Battlefield</td>
                        <td><input type="number" value="2" min="0" max="20"></td>
                        <td>59.95</td>
                    </tr>
                    <tr>
                        <td><img class="img-thumbnail" src="images/rome.jpg"></td>
                        <td>Rome Total War</td>
                        <td><input type="number" value="1" min="0" max="20"></td>
                        <td>5.95</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td><strong>Totaal:</strong> 125.85,-</td>
                    </tr>
                </tbody>
            </table>
            <div class="form-btn" align="right">
                <input type="submit" align="right" class="button-primary" value="Proceed to checkout!">
            </div>
        </div>
    </div><!-- End container -->
</template>
<style>
    body{
        background-color:#ff0000;
    }
</style>
<script>
    import HeaderComponent from './components/header.vue'
    import OtherComponent from './components/other.vue'
    export default{
        data(){
            return{
                cart: {}
            }
        }
    }
</script>
