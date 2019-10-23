import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'
import AuthService from './AuthService'
import { generateKeyPairSync } from 'crypto'

Vue.use(Vuex)

let baseUrl = location.host.includes('localhost') ? '//localhost:5000/' : '/'

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
        await api.put('/keeps', keepData)
        dispatch('getKeeps')
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
    async getKeepsbyVault({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get('vaults/' + vaultId + '/keeps')
        commit('setKeeps', res.data)
      } catch (e) {
        console.error(e)
      }
    },

    async deleteKeep({ commit, dispatch }, keepId) {
      try {
        let res = await api.delete('/keeps/' + keepId)
        dispatch('getKeeps')
      }

      catch (e) {
        console.error(e)
      }
    },

    async addKeep({ commit, dispatch }, vaultId, keep) {
      try {
        let change = await api.put('/vault/' + vaultId + '/' + keep.Id, keep)
        dispatch('getKeepsbyVault')
      } catch (e) {
        console.error(e)
      }
    },

    async removeKeep({ commit, dispatch }, vaultId, keepId) {
      try {
        let change = await api.put('/vault/' + vaultId + '/' + keepId)
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
        await api.put('/vaults', vaultData)
        dispatch('getVaults')
      } catch (e) {
        console.error(e)
      }
    },

    async getVault({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId)
        commit('setCurrentVault', res.data)
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
