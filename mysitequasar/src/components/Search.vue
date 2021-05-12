<template>
  <div>
    <q-card-section>
      <div class="q-pa-md">
        <q-input v-model="search" placeholder="Search">
          <template v-slot:append>
            <q-btn  :to="{ name: 'Blog', query: { search: search } }" round dense color="deep-purple-6" icon="search"></q-btn>
          </template>
        </q-input>
      </div>
    </q-card-section>
  </div>
</template>

<script>
export default {
  data() {
    return {
      search: null,
    };
  }
};
</script>

<style lang="scss" scoped></style>
