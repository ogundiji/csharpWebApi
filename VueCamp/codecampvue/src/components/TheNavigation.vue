<template v-if="authenticated">
     <nav class="navbar shadow bg-white rounded justify-content-between flex-nowrap flex-row fixed-top">
      <div class="container">
        <a class="navbar-brand float-left">
           Dashboard
        </a>
       <ul class="nav navbar-nav flex-row float-right">
          <li class="nav-item">
            <router-link class="btn btn-outline-primary" to="/camp">Camp</router-link>
          </li>
          <li class="nav-item">
            <router-link class="btn btn-outline-primary" to="/talk">Talk</router-link>
          </li>
          <li class="nav-item">
            <router-link class="btn btn-outline-primary" to="/speaker">Speaker</router-link>
          </li>
          <li class="nav-item">
            <router-link class="btn btn-outline-primary" to="/">Sign out</router-link>
          </li>
        </ul>
      </div>
    </nav>
</template>
<script>
import { mapGetters, mapActions } from 'vuex'
export default {
    computed:{
        ...mapGetters({
            authenticated:'auth/authenticate',
            user:'auth/user'
        })
    },
    methods:{
        ...mapActions({
            signOutAction:'auth/signOut'
        }),
        signOut(){
            this.signOutAction().then(()=>{
                this.router.replace({
                    name:'signup'
                })
            })
        }
    }
}
</script>