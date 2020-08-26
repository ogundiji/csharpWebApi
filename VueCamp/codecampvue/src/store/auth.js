import axios from 'axios'
const qs =require('qs')

export default {
    namespaced:true,
    state:{
        token:''
    },
    mutations:{
         //since it will update the state, we need the state a parameter and the value you want to pass
         SET_TOKEN(state,token){
             state.token=token;
         }
    },
    actions:{
       async signIn({dispatch},credentials){
         let response = await axios.post('/token',qs.stringify(credentials),{
          headers: { 'Content-Type': 'application/x-www-form-urlencoded'}
         });
        
         dispatch('attempt',response.data.access_token)
          
       },
       async attempt({commit},token){
         //the commit set the token, so the method SET_TOKEN can only be used in mutation
             commit('SET_TOKEN',token)
       }
    }
}