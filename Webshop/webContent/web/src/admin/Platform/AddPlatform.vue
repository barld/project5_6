<template>
    <div v-if="show" class="container">
        <h1>Platform toevoegen</h1>
        <div class="row">
            <div class="six columns">
                <label for="PlatformTitle">Platform titel</label><input type="text" id="PlatformTitle" v-model="PlatformTitle">
            </div>
            <div class="six columns">
                <label for="Brand">Merk</label><input type="text" id="Brand" v-model="Brand">
            </div>
        </div>
        <div class="row">
            <div class="six columns">
                <label for="Description">Beschrijving</label><textarea name="" id="Description" cols="30" rows="10" v-model="Description"></textarea>
            </div>
            <div class="six columns">
                <label for="Abbreviation">Afkorting</label><input id="Abbreviation" type="text" v-model="Abbreviation">
            </div>
        </div>

        <button @click="createPlatform">Maak platform</button>

        <div v-if="success" class="success">{{ message }}</div>
    </div>
</template>

<script>
    export default{
        props:['show'],
        data: function(){
            return{
                PlatformTitle: null,
                Brand: null,
                Description: null,
                Abbreviation: null,
                success: false,
                successMessage: null
            }
        },
        methods:{
            createPlatform: function(){
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("POST", "/api/platform/");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    console.log(xhr.response);
                    base.message = "U heeft succesvol een platform toegevoegd!";
                    base.success = true;

                    setTimeout(function(){
                        base.success = false;
                    }, 3000);
                };

                xhr.send(JSON.stringify({PlatformTitle: this.PlatformTitle, Brand: this.Brand, Description: this.Description, Abbreviation: this.Abbreviation}));
            }
        }
    }
</script>