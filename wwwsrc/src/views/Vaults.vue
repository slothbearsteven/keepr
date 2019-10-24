<template>
  <div class="Vaults">
    <div class="container">
      <div class="row">
        <div class="col-6" v-for="vault in vaults" :key="vault.id">{{vault.name}}</div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "Vaults",
  mounted() {
    this.$store.dispatch("getVaults");
  },
  data() {
    return {};
  },
  computed: {
    vaults() {
      return this.$store.state.vaults;
    }
  },
  methods: {},
  components: {}
};
</script>


<style scoped>
</style>