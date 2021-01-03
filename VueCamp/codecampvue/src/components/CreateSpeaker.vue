<template>
     <div class="vue-tempalte">
          <div class="vertical-center">
          <div class="inner-block2"> 
            <div v-if="errors" class="has-error"> {{ [errors] }}</div>
            <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
            <form @submit.prevent="checkForm">
                <h3>Create Speaker</h3>
                <div class="row">
                    <div class="col-md-6 col-xs-6">
                        <div class="form-group">
                            <label>Firstname</label>
                            <input type="text" class="form-control form-control-lg" v-model="postBody.Firstname" />
                        </div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="form-group">
                            <label>Lastname</label>
                            <input type="text" class="form-control form-control-lg" v-model="postBody.Lastname" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 col-xs-6">
                        <div class="form-group">
                            <label>BlogUrl</label>
                            <input type="text" class="form-control form-control-lg" v-model="postBody.BlogUrl" />
                        </div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="form-group">
                            <label>Company</label>
                            <input type="text" class="form-control form-control-lg" v-model="postBody.company" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 col-xs-6">
                        <div class="form-group">
                            <label>CompanyUrl</label>
                            <input type="text" class="form-control form-control-lg" v-model="postBody.CompanyUrl" />
                        </div>
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <div class="form-group">
                            <label>Github</label>
                            <input type="text" class="form-control form-control-lg" v-model="postBody.Github" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 col-xs-6">
                        <div class="form-group">
                            <label>Twitter</label>
                            <input type="text" class="form-control form-control-lg" v-model="postBody.Twitter" />
                        </div>
                    </div>
                </div>     
                <button type="submit" class="btn btn-dark btn-lg btn-block">{{ submitorUpdate }}</button>
            </form>
          </div>
          </div>
    </div>
</template>
<script>
import axios from 'axios';
export default {
  data() {
        return {
            errors: null,
            responseMessage:'',
            submitorUpdate : 'Submit',
            canProcess : true,
            postBody: {
                Firstname:'',
                Lastname:'',
                BlogUrl:'',
                company:'',
                CompanyUrl:'',
                Github:'',
                Twitter:''
            }
        }
   },
   watch:{
       '$store.state.objectToUpdate': function() {
                console.log(this.$store.state.objectToUpdate);
                this.postBody.Firstname = this.$store.state.objectToUpdate.Firstname,
                this.postBody.Lastname= this.$store.state.objectToUpdate.Lastname,
                this.postBody.BlogUrl = this.$store.state.objectToUpdate.BlogUrl,
                this.postBody.company = this.$store.state.objectToUpdate.company,
                this.postBody.CompanyUrl = this.$store.state.objectToUpdate.CompanyUrl,
                this.postBody.Github = this.$store.state.objectToUpdate.Github,
                this.postBody.Twitter = this.$store.state.objectToUpdate.Twitter,
                this.submitorUpdate = 'Update';
   }
   },
   methods:{
      checkForm: function (e) {
              
                if (this.postBody.Firstname && this.postBody.Lastname) {
                    e.preventDefault();
                    this.canProcess = false;
                    this.postPost();
                }
                else {
                    this.errors = [];
                    this.errors.push('Supply all the required field');
                }
            },
    postPost() {
                if (this.submitorUpdate == 'Submit') {
                    axios.post(`/api/Speakers/CreateSpeaker`, this.postBody)
                        .then(response => {
                            this.responseMessage = response.data.responseDescription; 
                            this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.postBody.Firstname = '';
                                this.postBody.Lastname='';
                                this.postBody.BlogUrl='';
                                this.postBody.company='';
                                this.postBody.CompanyUrl='';
                                this.postBody.Github='';
                                this.postBody.Twitter='';
                                this.$store.state.objectToUpdate = "create";
                            }
                        })
                        .catch(e => {
                            console.log(e);
                            //this.errors.push(e)
                        });
                }
                if (this.submitorUpdate == 'Update') {
                    axios.put(``, this.postBody)
                        .then(response => {
                            this.responseMessage = response.data.responseDescription; this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.submitorUpdate = 'Submit'
                                this.postBody.Firstname = '';
                                this.postBody.Lastname='';
                                this.postBody.BlogUrl='';
                                this.postBody.company='';
                                this.postBody.CompanyUrl='';
                                this.postBody.Github='';
                                this.postBody.Twitter='';

                                this.$store.state.objectToUpdate = "update";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        }        
}
</script>