<template>
  <div>
    <div
      class="q-mt-xl q-mb-xl q-gutter-lg"
      :class="this.$q.screen.lt.sm ? 'q-px-md' : ''"
    >
      <q-card v-for="blog in blogs" :key="blog.id">
        <img class="blog-img" :src="apiurl + blog.image"> 
        <div>
          <div class="text-h6 q-pt-md q-pl-md">{{ blog.title }}</div>
        </div>
        <div class="q-pl-md q-pt-md row">
          <time>{{ formatDate(blog.createdOn) }}</time>
          <span class="col">
            <q-badge class="q-mr-md float-right" outline color="deep-purple-6">
              {{ blog.category.categoryName }}
            </q-badge>
          </span>
        </div>
        <q-card-section>
          <div v-html="blog.content">
            {{blog.content}}
          </div>
        </q-card-section>
        <q-card-actions align="right">
          <q-btn :to="{ name: 'blog-detail', params: { slug: blog.slug } }" flat dense outline color="deep-purple-6" label="Read More" />
        </q-card-actions>
      </q-card>
    </div>
  </div>
</template>

<script>
import { date } from "quasar";
export default {
  props: ["blogs"],
  computed: {
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    }
  },
  methods: {
    formatDate(blogdate) {
      return date.formatDate(blogdate, "DD/MM/YYYY HH:mm");
    }
  }
};
</script>

<style lang="scss" scoped>
img {
  max-height: 400px;
}
</style>
