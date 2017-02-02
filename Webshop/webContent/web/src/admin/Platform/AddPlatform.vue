<template>
    <div id="addPlatform" class="container">
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

        <button class="admin_button button-primary line_break" v-if="!success" @click="createPlatform">Maak platform</button>
        <button @click="$emit('close')" class="admin_button button-primary line_break">Terug</button>

        <div v-if="success" class="success">{{ message }}</div>
    </div>
</template>

<script>
    export default{
        data: function(){
            return{
                PlatformTitle: "",
                Brand: "",
                Description: "",
                Abbreviation: "",
                success: false,
                successMessage: ""
            }
        },
        methods:{
            createPlatform: function(){
                this.checkLenghts();
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("POST", "/api/platform/");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    console.log(xhr.response);
                    base.message = "U heeft succesvol een platform toegevoegd! U wordt nu teruggestuurd.";
                    base.success = true;

                    setTimeout(function(){
                        base.$emit("close");
                        base.success = false;
                    }, 3000);
                };

                xhr.send(JSON.stringify({PlatformTitle: this.PlatformTitle, Brand: this.Brand, Description: this.Description, Abbreviation: this.Abbreviation}));
            },
            checkLenghts: function(){
                if(this.PlatformTitle.length  < 2 || this.Brand.length < 2 || this.Description.length < 2  || this.Abbreviation.length < 2){
                    alert("Elk veld moet minstens 3 tekens bevatten!");
                    throw("The minimum length for values is 3.");
                }
            }
        }
    }
</script>