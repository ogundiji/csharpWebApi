<template>
     <div class="vue-tempalte">
        <div class="vertical-center">
         <div class="inner-block2"> 
            <table class="table table-bordered table-striped table-hover">
            <thead>
            <tr>
                <th>Speakers Name</th>
                <th>Blog</th>
                <th>Company</th>
                <th>Github</th>
                <th>Twitter</th>
                <th></th>
            </tr>
            </thead>
            <tbody>
                <tr v-for="(status, index) in statusList" :key="index">
                  
                  <td>{{ status.firstName +"-"+ status.lastName}}</td>
                  <td>{{ status.blogUrl }}</td>
                  <td>{{ status.company }}</td>
                  <td>{{ status.gitHub }}</td>
                  <td>{{ status.twitter }}</td>
                  <td>
                       <button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(status)">Edit</button>
                        <button type="button" class="btn btn-submit btn-primary" @click="processDelete(status.speakerId)">Delete</button>
                  </td>
                </tr>
            </tbody>
            </table>
           
             
        </div>
        
        
        </div>   
    </div>
</template>
<script>
import axios from 'axios';
export default { 
              data() {
                return {
                    statusList: null,
                    responseMessage:'',
                    showModal:false
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },
    watch:{
         '$store.state.objectToUpdate':function () {
            this.getAllSpeaker();
        }
    },
    mounted () {
        this.getAllSpeaker();
     },
     methods: {
        processRetrieve : function (Status) {
            this.$store.state.objectToUpdate = Status;
         },
         processDelete: function (id) {
             axios.delete(`/api/Speakers/${id}`)
                 .then(response => {
                     if (response.data.responseCode == '200') {
                         alert("speaker successfully deleted");
                         this.getAllSpeaker();
                     }
                 }).catch(e => {
                            this.errors.push(e)
                        });
         },
         getAllSpeaker: function () {
             axios
            .get('/api/Speakers')
            .then(response => (this.statusList = response.data))
         }
    }
    
};
</script>