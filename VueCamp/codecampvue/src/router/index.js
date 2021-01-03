import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '../store'
import login from '../components/Login.vue'

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
    component:()=>import('../components/Dashboard.vue'),
    beforeEnter:(to,from, next)=>{
      if(!store.getters['auth/authenticated']){
        return next({
          name:login
        })
      }
      next()
   }
  },
  {
      path:'/camp',
      name:'camp',
      component:()=>import('../components/Camp.vue')
  },
  {
     path:'/talk',
     name:'talk',
     component:()=>import('../components/Talk.vue')
  },
  {
     path:'/speaker',
     name:'speaker',
     component:()=>import('../components/Speakers.vue')
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