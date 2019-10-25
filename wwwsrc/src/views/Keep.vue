<template>
  <div class="Keep">
    <div class="container-fluid">
      <div class="row justify-content-center">
        <div class="col-4">{{keep.name}}</div>
        <small class="col-2">{{keep.userId}}</small>
      </div>
      <div class="row justify-content-center">
        <div class="col-8">
          <img :src="keep.imgUrl" alt="keep's image cannot be loaded at this time" />
        </div>
      </div>
      <div class="row justify-content-around">
        <div class="col-3">Views:{{keep.views}}</div>
        <div class="col-3">Kept:{{keep.kept}}</div>
      </div>
      <div class="row justify-content-center">
        <div class="col-10">{{keep.description}}</div>
      </div>
      <div class="row justify-content-center">
        <div class="col-4">
          <span class="dropdown show">
            <button
              class="btn btn-info btn-block dropdown-toggle top-margin"
              data-toggle="dropdown"
            >
              <span v-if="currentVault != {} || ''">Vault: {{currentVault.name}}</span>
              <span v-else>(No Vault Selected)</span>
            </button>
            <div class="dropdown-menu">
              <a
                v-for="vault in vaults"
                :key="vault.id"
                class="dropdown-item"
                @click="changeCurrentVault(vault.id)"
              >{{vault.name}}</a>
            </div>
          </span>
        </div>
        <div class="col-2">
          <button class="btn btn-success" @click="addkeep(currentVault.id,keep)">Add to Vault</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "Keep",
  mounted() {
    let keepId = this.$route.params.keepId;
    this.$store.dispatch("getKeep", keepId);

    this.$store.dispatch("getVaults");
  },
  data() {
    return {};
  },
  computed: {
    keep() {
      return this.$store.state.currentKeep;
    },
    vaults() {
      return this.$store.state.vaults;
    },
    currentVault() {
      return this.$store.state.currentVault;
    }
  },
  methods: {
    changeCurrentVault(vaultId) {
      this.$store.dispatch("getVault", vaultId);
    },
    addkeep(vaultId, keep) {
      keep.kept++;
      this.$store.dispatch("editKeep", keep);

      let payload = {
        vaultId,
        keep
      };
      this.$store.dispatch("addKeep", payload);
    }
  },
  components: {}
};
</script>


<style scoped>
</style>