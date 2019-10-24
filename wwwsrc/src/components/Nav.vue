<template>
  <div class="Nav">
    <div class="container">
      <div class="row justify-content-around">
        <div class="col-3">
          <router-link :to="{name:'vaults'}">Vaults</router-link>
        </div>
        <div class="col-3">
          <router-link :to="{name:'home'}">Home</router-link>
        </div>
        <div class="col-3">
          <button v-if="user.id" @click="logout">logout</button>
          <router-link v-else :to="{name: 'login'}">Login</router-link>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "Nav",
  data() {
    return {};
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  components: {}
};
</script>


<style scoped>
</style>