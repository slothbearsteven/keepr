<template>
  <div class="MyKeeps">
    Hello there
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
          {{keep.views}} {{keep.kept}}
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
  methods: {},
  components: {}
};
</script>


<style scoped>
</style>