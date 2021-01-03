import Vue from 'vue'
import Vuex from 'vuex'
import auth from  './auth'

Vue.use(Vuex);

export default new Vuex.Store({
    state:{
        objectToUpdate:null
    },
    getters:{
      
    },
    mutations:{
        // updateBalSheet(state, newObj) {
        //     state.objectToUpdate = newObj
        //   }
    },
    actions:{
        // updateBalSheet(context) {
        //       context.commit('updateBalSheet', newObj)
            
        //   }
    },
    modules:{
        auth
    }
})