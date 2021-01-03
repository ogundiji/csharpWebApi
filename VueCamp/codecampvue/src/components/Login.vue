<template>
    <div class="vue-tempalte">
         <div class="vertical-center">
         <div class="inner-block"> 
            <form @submit.prevent="submit">
                <h3>Sign In</h3>

                <div class="form-group">
                    <label>Email address</label>
                    <input type="email" class="form-control form-control-lg" v-model="credentials.UserName" />
                </div>

                <div class="form-group">
                    <label>Password</label>
                    <input type="password" class="form-control form-control-lg" v-model="credentials.Password" />
                </div>

                <button type="submit" class="btn btn-dark btn-lg btn-block">Sign In</button>
                <p class="forgot-password text-left mt-2 mb-4">
                    yet to register
                    <router-link :to="{name: 'signup'}">sign up?</router-link>
                </p>

                <p class="forgot-password text-right mt-2 mb-4">
                    <router-link to="/forgot-password">Forgot password ?</router-link>
                </p>

                <div class="social-icons">
                    <ul>
                        <li><a href="#"><i class="fa fa-google"></i></a></li>
                        <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                        <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                    </ul>
                </div>

            </form>
         </div>
         </div>
    </div>
</template>

<script>
    
    import { mapActions } from 'vuex'
    import { mapGetters } from 'vuex'

    export default {
        data() {
            return {
                credentials:{
                    UserName:'',
                    Password:'',
                    grant_type:'password'
                }
            }
        },
         computed:{ 
            ...mapGetters({
              authenticate:'auth/authenticate'
         })
        },
        methods:{
           ...mapActions({
               signIn:'auth/signIn'
           }),
           submit(){
               this.signIn(this.credentials).then(()=>{
                  this.$router.push({
                      name:'dashboard'
                  })
               })
           }
        }
    }
</script>
<style scoped>
 body {
    background: #2554FF !important ;
    min-height: 100vh;
    display: flex;
    font-weight: 400;
  }
</style>
