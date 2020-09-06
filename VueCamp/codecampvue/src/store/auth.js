import axios from 'axios'

const qs =require('qs')

export default {
    namespaced:true,
    state:{
        token:'',
        user:'',
        username:''
    },
    getters:{
       authenticate(state){
         return state.token && state.user
       },
       user(state){
        return state.user
       }
    },
    mutations:{
         //since it will update the state, we need the state a parameter and the value you want to pass
         SETTOKEN(state,payload){
             state.token=payload.token;
         },
         SETUSER(state,data){
           state.user=data
         }

    },
    actions:{
       async signIn({ dispatch },credentials){
         let response = await axios.post('token',qs.stringify(credentials),{
          headers: { 'Content-Type': 'application/x-www-form-urlencoded'}
         });
         
         
         var username=credentials.UserName.replace('@gmail.com','');
         var payload={'token':response.data.access_token,username:username}
        
         dispatch('attempt',payload)
          
       },
       async attempt({ commit, state },payload){
         //the commit set the token, so the method SET_TOKEN can only be used in mutation
             if(payload){
              commit('SETTOKEN',payload)
             }
             
             if(!state.token){
               return
             }
             try{
              
              let response=await axios.get(`api/Account/${payload.username}`)

              commit('SETUSER',response.data)

             }catch(e){
                console.log('Login Failed '+e)
                commit('SETTOKEN',null)
                commit('SETUSER',null)
             }
       }
      }
}
