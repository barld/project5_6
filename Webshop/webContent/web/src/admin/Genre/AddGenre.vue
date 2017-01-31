<template>
    <div id="addGenre" v-if="show" class="container">
        <h1>Genre toevoegen</h1>
        <div class="row">
            <div class="six columns">
                <label for="Name">Naam</label><input id="Name" type="text" v-model="Name">
            </div>
            <div class="six columns">
                <label for="Description">Beschrijving</label><textarea id="Description" cols="30" rows="10" v-model="Description"></textarea>
            </div>
        </div>
        <button @click="createGenre">Maak genre</button>

        <div v-if="success" class="success">{{ message }}</div>
    </div>
</template>

<script>
    export default{
        props:['show'],
        data: function(){
            return{
                Name: "",
                Description: "",
                success: false,
                message: ""
            }
        },
        methods:{
            createGenre: function(){
                this.checkLenghts();
                var base = this;
                var xhr = new XMLHttpRequest();

                xhr.open("POST", "/api/genre/");
                xhr.setRequestHeader('Content-type', "Application/JSON", true);

                xhr.onload = function () {
                    console.log(xhr.response);
                    base.message = "U heeft succesvol een genre toegevoegd!";
                    base.success = true;

                    setTimeout(function(){
                        base.success = false;
                    }, 3000);
                };

                xhr.send(JSON.stringify({Name: this.Name, Description: this.Description}));
            },
            checkLenghts: function(){
                if(this.Name.length  < 2 || this.Description.length < 2){
                    alert("Elk veld moet minstens 3 tekens bevatten!");
                    throw("The minimum length for values is 3.");
                }
            }
        }
    }
</script>