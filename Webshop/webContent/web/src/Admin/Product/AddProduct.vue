<template>
    <div>
        <h3>Maak een nieuw product</h3>
        <div v-show="platformsLoaded" class="row">
            {{ platforms }}
        </div>
    </div>
</template>
<script>
    export default{
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
        }
    }
</script>