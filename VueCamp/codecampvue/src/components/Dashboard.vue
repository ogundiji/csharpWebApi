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