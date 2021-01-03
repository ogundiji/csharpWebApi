<template >
   
    <div class="vue-tempalte">
       
      <p>hello user</p>
    </div>
</template>
<script>
import { mapGetters } from 'vuex'
export default {
    name:'dashboard',
    computed:{
        ...mapGetters({
            authenticated:'auth/authenticate',
            user:'auth/user'

        })
    }
}
</script>
  <style scoped>
   body {
    background:orange !important;
    min-height: 100vh;
    display: flex;
    font-weight: 400;
}
        </style>