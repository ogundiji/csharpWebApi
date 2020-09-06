import Vue from 'vue'
import VueRouter from 'vue-router'
//import store from '../store'

Vue.use(VueRouter)

  const routes = [
  {
    path: '/',
    name: 'signup',
    component: () => import('../components/Signup.vue')
  },
  {
    path: '/login',
    name: 'login',
    component: () => import('../components/Login.vue')
  },
  {
    path:'/dashboard',
    name:'dashboard',
    component:()=>import('../components/Dashboard.vue')
  },

  {
    path: '/forgot-password',
    name: 'forgot-password',
    component: () => import('../components/ForgotPassword.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router


// beforeEnter:(to,from,next)=>{
//   if(!store.getters['auth/authenticate']){
//     return next({
//       name:'login'
//     })
//   }
// next()

// }