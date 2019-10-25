<template>
  <div class="Vaults">
    <div class="container">
      <div class="row">
        <div class="col-12">
          <form @submit.prevent="createVault">
            <input type="text" placeholder="name..." v-model="newVault.Name" required />
            <input type="text" placeholder="description...." v-model="newVault.Description" />
            <br />
            <button class="btn btn-success my-2" type="submit">Create Vault</button>
          </form>
        </div>
      </div>
      <div class="row justify-content-around">
        <div class="col-6" v-for="vault in vaults" :key="vault.id">
          <router-link :to="{name:'vault' , params: {vaultId: vault.id}}">{{vault.name}}</router-link>
          <br />
          {{vault.description}}
          <button class="btn btn-danger" @click="deleteVault(vault.id)">Delete</button>
        </div>
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
    return {
      newVault: {
        Name: "",
        Description: ""
      }
    };
  },
  computed: {
    vaults() {
      return this.$store.state.vaults;
    },
    user() {
      return this.$store.state.user;
    }
  },
  methods: {
    deleteVault(vaultId) {
      this.$store.dispatch("deleteVault", vaultId);
    },
    createVault() {
      this.$store.dispatch("createVault", this.newVault);
    }
  },
  components: {}
};
</script>


<style scoped>
</style>