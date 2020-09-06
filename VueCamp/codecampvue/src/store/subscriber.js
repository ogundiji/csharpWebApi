import store from '@/store'
import axios from 'axios'

store.subscribe((mutation)=>{
    switch(mutation.type){
        case 'auth/SETTOKEN':
          if(mutation.payload!==null){
            axios.defaults.headers.common['Authorization']=`Bearer ${mutation.payload.token}`
            localStorage.setItem('token',mutation.payload.token);
          }else{
              axios.defaults.headers.common['Authorization']=null
              localStorage.removeItem('token');
          }

          break;
    }
})


