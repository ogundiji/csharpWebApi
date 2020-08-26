import axios from 'axios'
const qs =require('qs')

export default {
    namespaced:true,
    state:{
        token:'',
        user:''
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
         SET_TOKEN(state,token){
             state.token=token;
         },
         SET_USER(state,data){
           state.user=data
         }
    },
    actions:{
       async signIn({dispatch},credentials){
         let response = await axios.post('/token',qs.stringify(credentials),{
          headers: { 'Content-Type': 'application/x-www-form-urlencoded'}
         });
         
         dispatch('attempt',response.data.access_token)
          
       },
       async attempt({commit},token,credentials){
         //the commit set the token, so the method SET_TOKEN can only be used in mutation
             commit('SET_TOKEN',token)

             try{
               let response= await axios.get('api/Account/ViewSingleUser',credentials.email,{
                 headers:{
                   'Authorization':'Bearer '+ token
                 }
               })
               commit('SET_USER',response.data)
             }catch(e){
                console.log('Login Failed')
                commit('SET-TOKEN',null)
                commit('SET-USER',null)
             }
       }
    }
}