<template>
    <div>
        <h3>Maak een nieuw product</h3>
        <div v-show="platformsLoaded" class="row">
            <div class="six columns">
                <label for="GameTitle"><input type="text" :model="GameTitle" id="GameTitle" name="GameTitle"></label>
            </div>
        </div>
    </div>
</template>
<script>
    export default{
        props:['platforms'],
        data: function(){
            return{
                platformsLoaded: true
            }
        },
        methods:{
            createGame: function(){
                var game = {
                    GameTitle: 'Test',
                    Platform:[],
                    RatingPegi: "",
                    Publisher:[],
                    Genres:[],
                    Image:[],
                    MinPlayers: 0,
                    MaxPlayers: 0,
                    Description: "",
                    EAN: 0,
                    Price:0.00,
                    IsVRCompatible: false,
                    ReleaseDate: "01-01-2018"
                }
            }
        },
        created: function(){
            console.log('Created!');
            console.log(this.platforms);
        }
    }
</script>