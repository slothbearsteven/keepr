import Vue from 'vue'
import Router from 'vue-router'
// @ts-ignore
import Home from './views/Home.vue'
// @ts-ignore
import Login from './views/Login.vue'
// @ts-ignore
import Keep from './views/Keep.vue'
// @ts-ignore
import Vaults from './views/Vaults.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/keep/:keepId',
      name: 'keep',
      component: Keep
    },
    {
      path: '/vaults',
      name: 'vaults',
      component: Vaults
    }
  ]
})
