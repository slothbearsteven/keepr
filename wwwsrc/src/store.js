import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'
import AuthService from './AuthService'
import { generateKeyPairSync } from 'crypto'

Vue.use(Vuex)

let baseUrl = location.host.includes('localhost') ? 'https://localhost:5001/' : '/'

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    keeps: [],
    vaults: [],
    currentKeep: {},
    currentVault: {},
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    resetState(state) {
      //clear the entire state object of user data
      state.user = {}
      state.keeps = []
      state.currentKeep = {}
      state.currentVault = {}
      state.vaults = []
    },
    setKeeps(state, keeps) {
      state.keeps = keeps
    },
    setCurrentKeep(state, keep) {
      state.currentKeep = keep
    },
    setVaults(state, vaults) {
      state.vaults = vaults
    },
    setCurrentVault(state, vault) {
      state.currentVault = vault
    }
  },
  actions: {
    async register({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Register(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async login({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Login(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async logout({ commit, dispatch }) {
      try {
        let success = await AuthService.Logout()
        if (!success) { }
        commit('resetState')
        router.push({ name: "login" })
      } catch (e) {
        console.warn(e.message)
      }
    },

    //SECTION keeps
    async getKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("/keeps")
        commit('setKeeps', res.data)
      } catch (e) {
        console.error(e)
      }
    },
    async createKeep({ commit, dispatch }, keepData) {
      try {
        await api.post('/keeps', keepData)
        dispatch('getKeeps')
      } catch (e) {
        console.error(e)
      }
    },
    async getKeepsByUser({ commit, dispatch }) {
      try {
        let res = await api.get("/keeps/user")
        commit('setKeeps', res.data)
      } catch (e) {
        console.error(e)
      }
    },
    async getKeep({ commit, dispatch }, keepId) {
      try {
        let res = await api.get("keeps/" + keepId)
        commit('setCurrentKeep', res.data)
      } catch (error) {
        console.error(error)
      }
    },


    async editKeep({ commit, dispatch }, keep) {
      try {
        let res = await api.put("keeps/" + keep.id, keep)
        dispatch('getKeep', keep.id)
        router.push({ path: "/keep/" + keep.id });
      } catch (error) {
        console.error(error)
      }
    },
    async getKeepsbyVault({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get('vaults/' + vaultId + '/keeps')
        commit('setKeeps', res.data)
      } catch (e) {
        console.error(e)
      }
    },

    async deleteKeep({ commit, dispatch }, keep) {
      try {
        if (keep.private) {
          let res = await api.delete('/keeps/' + keep.id)
          dispatch('getKeepsByUser')
        }
        else {
          let res = await api.delete('/keeps/' + keep.id)
          dispatch('getKeeps')
        }
      }

      catch (e) {
        console.error(e)
      }
    },

    async addKeep({ commit, dispatch }, payload) {
      try {
        let change = await api.put('/vaults/' + payload.vaultId + '/addKeep/' + payload.keep.id, payload.keep)
        dispatch('getKeepsbyVault')
      } catch (e) {
        console.error(e)
      }
    },

    async removeKeep({ commit, dispatch }, payload) {
      try {
        debugger
        let change = await api.put('/vaults/' + payload.vaultId + '/removeKeep/' + payload.keepId)
        dispatch('getKeepsByVault')
      } catch (e) {
        console.error(e)
      }
    },


    //SECTION vaults

    async getVaults({ commit, dispatch }) {
      try {
        let res = await api.get("/vaults")
        commit('setVaults', res.data)
      } catch (e) {
        console.error(e)
      }
    },
    async createVault({ commit, dispatch }, vaultData) {
      try {
        await api.post('/vaults', vaultData)
        dispatch('getVaults')
      } catch (e) {
        console.error(e)
      }
    },

    async getVault({ commit, dispatch }, vaultId) {
      try {

        let res = await api.get("vaults/" + vaultId)
        commit('setCurrentVault', res.data)
        dispatch('getKeepsbyVault', vaultId)
      } catch (error) {
        console.error(error)
      }
    },

    async deleteVault({ commit, dispatch }, vaultId) {
      try {
        let res = await api.delete('/vaults/' + vaultId)
        dispatch('getVaults')
      }

      catch (e) {
        console.error(e)
      }
    }

  }
})
