<template>
    <div>
        <h3>Maak een nieuw product</h3>
        <div v-show="platformsLoaded" class="row">
            Test
        </div>
    </div>
</template>
<script>
    export default{
        data: function(){
            return{
                platforms: [],
                platformsLoaded: false
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
            },
            loadPlatforms: function(){

                var base = this;
                var xhr = new XMLHttpRequest();
                xhr.open('GET', `/api/platform/all`);

                xhr.onload = function(){
                    base.platforms = JSON.parse(xhr.response);
                    console.log(base.platforms);
                    //this.platformsLoaded = true;
                };

                xhr.send();
            },
            created: function(){
                console.log('Created!');
                this.loadPlatforms();
            }
        }
    }
</script>