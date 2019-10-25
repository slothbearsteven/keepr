<template>
  <div class="Vault">
    <div class="container-fluid">
      <div class="row justify-content-center">
        <div class="col-6">
          <h1>{{vault.name}}</h1>
        </div>
      </div>
      <div class="row justify-content-center">
        <div class="col-8">
          <h5>{{vault.description}}</h5>
        </div>
      </div>
      <div class="row justify-content-around">
        <div class="col-4" v-for="keep in keeps" :key="keep.id">
          {{keep.name}}
          <div class="row">
            <div class="col-12">
              <img :src="keep.imgUrl" alt="Keep Image cannot be displayed" />
            </div>
          </div>
          {{keep.description}}
          <br />
          <button class="btn btn-dark" @click="ViewsUp(keep)">Closer Look</button>
          <br />
          Views: {{keep.views}} Kept:{{keep.kept}}
          <br />
          <div v-if="user.id != keep.userId"></div>
          <div v-else>
            <button class="btn btn-danger" @click="deleteKeep(keep)">Delete</button>
          </div>
          <button class="btn btn-warning" @click="removekeep(vault.id,keep.id)">Remove from Vault</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "Vault",
  mounted() {
    let vaultId = this.$route.params.vaultId;
    this.$store.dispatch("getVault", vaultId);
  },
  data() {
    return {};
  },
  computed: {
    vault() {
      return this.$store.state.currentVault;
    },
    keeps() {
      return this.$store.state.keeps;
    },
    user() {
      return this.$store.state.user;
    }
  },
  methods: {
    removekeep(vaultId, keepId) {
      let payload = { vaultId, keepId };

      this.$store.dispatch("removeKeep", payload);
    }
  },
  components: {}
};
</script>


<style scoped>
</style>