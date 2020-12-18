import axios from 'axios'

const qs =require('qs')

export default {
    namespaced:true,
    state:{
        token:'',
        user:'',
        username:'',
        message:''
    },
    getters:{
       authenticate(state){
         return state.token && state.user
       },
       user(state){
        return state.user
       },
       notification(state){
         return state.message
       }
    },
    mutations:{
         //since it will update the state, we need the state a parameter and the value you want to pass
         SETTOKEN(state,payload){
             state.token=payload.token;
         },
         SETUSER(state,data){
           state.user=data
         },
         SETMESSAGE(state,data){
           state.message=data
         }

    },
    actions:{
      async signUp({ dispatch },model){
         let response=await axios.post('api/Account/register',model);

       
         dispatch('process',response);
      },
      async process({ commit },response){
            if(response){
              commit('SETMESSAGE',response)
            }
      },
      async forgotpassword(_,model){
         await (await axios.post('api/Account/ForgotPassword',model)).
         then(resp=>{
          
           if(resp.data=='Successfull')
           this.$router.push({ name:'forgot-password'})
         })
      },
       async signIn({ dispatch },credentials){
         let response = await axios.post('token',qs.stringify(credentials),{
          headers: { 'Content-Type': 'application/x-www-form-urlencoded'}
         });
         
         
         var username=credentials.UserName;
         var payload={
           'token':response.data.access_token,
           username:username
          };
        
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
               let p=btoa(payload.username);
              let response = await axios.get(`/api/Account/${p}`)
              
              commit('SETUSER',response.data)
              
              
              
             }catch(e){
                console.log('Login Failed '+e)
                commit('SETTOKEN',null)
                commit('SETUSER',null)
             }
       }
      }
}
