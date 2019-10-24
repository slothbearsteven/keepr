<template>
  <div class="Keeps">
    <div class="container-fluid">
      <div class="row justify-content-center" v-for="keep in keeps" :key="keep.id">
        <div class="col-4">
          {{keep.Name}}
          <div class="row">
            <div class="col-12">
              <img :src="keep.imgUrl" alt="Keep Image cannot be displayed" />
            </div>
          </div>
          {{keep.description}}
          <br />

          <!-- FIXME Keep views not increasing, fix views up method -->
          <router-link :to="{name:'keep' , params: {keepId: keep.id}}">
            <button class="btn btn-dark" @click="ViewsUp(keep)">Closer Look</button>
          </router-link>
          <br />
          {{keep.views}} {{keep.kept}}
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
    }
  },
  methods: {
    ViewsUp(keep) {
      keep.views = keep.views++;
      this.$store.dispatch("editKeep", keep);
    }
  },
  components: {}
};
</script>


<style scoped>
</style>