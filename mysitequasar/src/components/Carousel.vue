<template>
  <div class="q-pa-md">
    <q-carousel animated v-model="slider" arrows navigation infinite>
      <q-carousel-slide  v-for="slider in sliders" :key="slider.id"
        :name="slider.id"
        :img-src="apiurl+slider.image"
      />
    </q-carousel>
  </div>
</template>

<script>
export default {
  data() {
    return {
      slider:1
    };
  },
   created() {
    this.$store.dispatch("slider/getSliders");
  },
  computed: {
    sliders() {
      return this.$store.getters["slider/getSliders"];
    },
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    }
  }
};
</script>

<style lang="scss" scoped></style>
