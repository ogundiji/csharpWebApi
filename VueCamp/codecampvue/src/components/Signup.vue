<template>
<div class="body">
    <div class="vue-tempalte">
         <div class="vertical-center">
         <div class="inner-block"> 
            <form @submit.prevent="checkform">
                <div class="alert alert-info" v-show="message!=null">
                    {{ message }}
                </div>
                <p v-if="errors.length">
                <b>Please correct the following error(s):</b>
                    <ul>
                        <li v-for="error in errors" v-bind:key="error">{{ error }}</li>
                    </ul>
                </p>
                <h3>Sign Up</h3>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                        <label>Last Name</label>
                        <input type="text" class="form-control form-control-lg" v-model="model.Lastname" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>First Name</label>
                            <input type="text" class="form-control form-control-lg" v-model="model.Firstname" />
                        </div>
                    </div>
                </div>
            
                <div class="form-group">
                    <label>Email address</label>
                    <input type="email" class="form-control form-control-lg" v-model="model.Email"/>
                </div>

                <div class="form-group">
                    <label>Password</label>
                    <input type="password" class="form-control form-control-lg" v-model="model.Password" />
                </div>
                <div class="form-group">
                    <span>
                        <input type="checkbox"  v-model="model.active"/> <label>active</label>
                    </span>
                    
                </div>

                <button type="submit" class="btn btn-dark btn-lg btn-block">Sign Up</button>

                <p class="forgot-password text-right">
                    Already registered 
                    <router-link :to="{name: 'login'}">sign in?</router-link>
                </p>
            </form>
         </div>
         </div>
    </div>
</div>
</template>

<script>

   import { mapActions } from 'vuex';
   import { mapGetters } from 'vuex'
   
    export default {
        data() {
            return {
                errors:[],
                message:null,
                model:{
                    Firstname:'',
                    Lastname:'',
                    Email:'',
                    Password:'',
                    active:true
                }
            }
        },
        computed:{
            ...mapGetters({
               notification:'auth/notification'
         })
        },
        methods:{
              ...mapActions({
                signUp:'auth/signUp'
           }),
            checkform(){
                if(this.model.Firstname && 
                this.model.Lastname && this.model.Email
                 && this.model.Password && this.model.active){
                         this.signUp(this.model).then(()=>{
                             var notify=JSON.stringify(this.notification);
                             if(notify){
                                 this.message="successfully createed the account";
                             }
                             this.model.Firstname='';
                             this.model.Lastname='';
                             this.model.Email='';
                             this.model.Password='';
                             this.model.active=false;
                         })
                     }
                  
                 
                 
                this.errors=[];
                if(!this.model.Firstname){
                  this.errors.push('First name required')
                }

                if(!this.model.Lastname){
                  this.errors.push('Last name required')
                }
                if(!this.model.Email){
                  this.errors.push('Email is required')
                }

                if(!this.model.Password){
                  this.errors.push('password is required')
                }

                if(!this.model.active){
                  this.errors.push('active not checked');
                }     
            }  
        }
    }
</script>
<style scoped>
 /* .body {
    background: #2554FF !important ;
    min-height: 100vh;
    display: flex;
    font-weight: 400;
  } */

</style>