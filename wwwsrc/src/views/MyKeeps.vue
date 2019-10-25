<template>
  <div class="MyKeeps">
    Here's all the Keeps you've made, including private ones!
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
          Views: {{keep.views}} Kept:{{keep.kept}}
          <br />
          <button class="btn btn-danger" @click="deleteKeep(keep)">Delete</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "MyKeeps",
  mounted() {
    this.$store.dispatch("getKeepsByUser");
  },
  data() {
    return {};
  },
  computed: {
    keeps() {
      return this.$store.state.keeps;
    }
  },
  methods: {
    deleteKeep(keep) {
      this.$store.dispatch("deleteKeep", keep);
    }
  },
  components: {}
};
</script>


<style scoped>
</style>