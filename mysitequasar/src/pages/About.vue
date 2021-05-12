<template>
  <q-page class="container">
    <div class="row q-pa-xl justify-center items-start q-col-gutter-md" v-for="about in abouts" :key="about.id">
        <div class="col col-lg-6 col-md-6">
          <q-img :src="apiurl+about.image"></q-img>
        </div>
        <div class="col-md">
          <div v-html="about.description">
          </div>
        </div>
    </div>
  </q-page>
</template>

<script>
export default {
  created() {
    this.$store.dispatch("about/getAbouts");
  },
  computed: {
    abouts() {
      return this.$store.getters["about/getAbouts"];
    },
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    }
  }
};
</script>
