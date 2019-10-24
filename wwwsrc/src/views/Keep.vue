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
    </div>
  </div>
</template>


<script>
export default {
  name: "Keep",
  mounted() {
    let keepId = this.$route.params.keepId;
    this.$store.dispatch("getKeep", keepId);
  },
  data() {
    return {};
  },
  computed: {
    keep() {
      return this.$store.state.currentKeep;
    }
  },
  methods: {},
  components: {}
};
</script>


<style scoped>
</style>