import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from 'axios'

require('@/store/subscriber')

import 'bootstrap/dist/css/bootstrap.min.css'
import '@/assets/css/main.css'

axios.defaults.baseURL='http://localhost:51301';



Vue.config.productionTip = false

store.dispatch('auth/attempt',localStorage.getItem('token'))
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')