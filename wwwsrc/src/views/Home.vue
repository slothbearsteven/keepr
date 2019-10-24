<template>
  <div class="home">
    <h1>Welcome Home {{user.username}}</h1>
    <div v-if="user.id">Create a keep!</div>
    <br />
    <keeps />
  </div>
</template>

<script>
import Keeps from "../components/Keeps.vue";
export default {
  name: "home",
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  methods: {},
  components: {
    Keeps
  }
};
</script>