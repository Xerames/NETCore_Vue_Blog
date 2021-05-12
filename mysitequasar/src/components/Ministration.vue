<template>
  <div>
    <div class="row justify-center items-start text-deep-purple-6 text-center">
      <h5>Ministrations</h5>
    </div>
    <div class="q-pa-xl row justify-center items-start q-col-gutter-md">
      <div
        class="col-lg-3 col-md-6 col-sm-10 col-xs-10"
        v-for="ministration in ministrations"
        :key="ministration.id"
      >
        <q-card>
          <div class="row justify-center q-pa-xs">
            <q-img
              class="q-mt-md"
              width="100px"
              height="100px"
              :src="apiurl + ministration.image"
            />
          </div>
          <q-card-section>
            <div class="text-h6 text-center">{{ ministration.title }}</div>
          </q-card-section>
          <q-card-section class="q-pt-none text-center">
            {{ ministration.description }}
          </q-card-section>
        </q-card>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  created() {
    this.$store.dispatch("ministration/getMinistrations");
  },
  computed: {
    ministrations() {
      return this.$store.getters["ministration/getMinistrations"];
    },
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    }
  }
};
</script>

<style scoped></style>
