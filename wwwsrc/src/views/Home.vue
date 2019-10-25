<template>
  <div class="home">
    <h1>Welcome Home {{user.username}}</h1>
    <div v-if="user.id">
      Create a keep!
      <br />
      <form @submit.prevent="createKeep">
        <input type="text" placeholder="name..." v-model="newKeep.Name" required />
        <input type="text" placeholder="Image Url" v-model="newKeep.ImgUrl" />
        <input type="text" placeholder="description...." v-model="newKeep.Description" />
        <div class="form-group">
          <label for="Privacy">Private:</label>
          <select class="form-control" id="Privacy" v-model="bool">
            <option>No</option>
            <option>Yes</option>
          </select>
        </div>
        <br />
        <button class="btn btn-success my-2" type="submit">Create Keep</button>
      </form>
    </div>
    <br />
    <keeps />
  </div>
</template>

<script>
import Keeps from "../components/Keeps.vue";
export default {
  name: "home",
  data() {
    return {
      newKeep: {
        Name: "",
        ImgUrl: "",
        Description: "",
        Private: false
      },
      bool: ""
    };
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  methods: {
    createKeep() {
      if (this.bool == "Yes") {
        this.newKeep.Private = true;
      }
      this.$store.dispatch("createKeep", this.newKeep);

      this.newKeep = {
        Name: "",
        ImgUrl: "",
        Description: "",
        Private: false
      };
    }
  },
  components: {
    Keeps
  }
};
</script>