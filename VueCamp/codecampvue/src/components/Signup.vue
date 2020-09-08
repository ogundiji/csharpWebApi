<template>
    <div class="vue-tempalte">
        <form @submit.prevent="checkform">
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
                      <input type="text" class="form-control form-control-lg" v-model="credentials.Lastname" />
                    </div>
                </div>
                 <div class="col-md-6">
                     <div class="form-group">
                        <label>First Name</label>
                        <input type="text" class="form-control form-control-lg" v-model="credentials.Firstname" />
                     </div>
                </div>
            </div>
        
            <div class="form-group">
                <label>Email address</label>
                <input type="email" class="form-control form-control-lg" v-model="credentials.Email"/>
            </div>

            <div class="form-group">
                <label>Password</label>
                <input type="password" class="form-control form-control-lg" v-model="credentials.Password" />
            </div>
            <div class="form-group">
                <span>
                    <input type="checkbox"  v-model="credentials.active"/> <label>active</label>
                </span>
                
            </div>

            <button type="submit" class="btn btn-dark btn-lg btn-block">Sign Up</button>

            <p class="forgot-password text-right">
                Already registered 
                <router-link :to="{name: 'login'}">sign in?</router-link>
            </p>
        </form>
    </div>
</template>

<script>

   import { mapActions } from 'vuex'
   
    export default {
        data() {
            return {
                errors:[],
                credentials:{
                    Firstname:'',
                    Lastname:'',
                    Email:'',
                    Password:'',
                    active:true
                }
            }
        },
        methods:{
              ...mapActions({
               signUp:'auth/signUp'
           }),
            checkform(){
                if(this.credentials.Firstname && 
                this.credentials.Lastname && this.credentials.Email
                 && this.credentials.Password && this.credentials.active){

                     console.log(this.credentials);
                     this.signUp(this.credentials);
                 }
                 
                this.errors=[];
                if(!this.credentials.Firstname){
                  this.errors.push('First name required')
                }

                if(!this.credentials.Lastname){
                  this.errors.push('Last name required')
                }
                if(!this.credentials.Email){
                  this.errors.push('Email is required')
                }

                if(!this.credentials.Password){
                  this.errors.push('password is required')
                }

                if(!this.credentials.active){
                  this.errors.push('active not checked');
                }     
            }  
        }
    }
</script>
