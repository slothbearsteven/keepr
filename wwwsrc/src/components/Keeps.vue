<template>
  <div class="Keeps">
    <div class="container-fluid">
      <div class="row justify-content-center">
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
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "Keeps",
  mounted() {
    this.$store.dispatch("getKeeps");
  },
  data() {
    return {};
  },
  computed: {
    keeps() {
      return this.$store.state.keeps;
    },
    user() {
      return this.$store.state.user;
    }
  },
  methods: {
    ViewsUp(keep) {
      keep.views++;
      this.$store.dispatch("editKeep", keep);
    },

    deleteKeep(keep) {
      this.$store.dispatch("deleteKeep", keep);
    }
  },
  components: {}
};
</script>


<style scoped>
</style>