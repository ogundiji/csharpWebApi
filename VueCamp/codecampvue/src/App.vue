<template>
  <div class="vue-tempalte">
    <!-- Navigation -->
  

    <!-- Main -->
    <div class="App">
          <TheNavigation/>
          <router-view />
    </div>
  </div>
</template>
<script>
import TheNavigation from '@/components/TheNavigation'
export default {
  components:{
    TheNavigation
  }
}
</script>