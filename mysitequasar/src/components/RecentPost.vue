<template>
  <div>
    <div class="sidebar-title q-ml-md text-deep-purple-6">Recent Posts</div>
    <q-card-section>
      <q-list class="q-pa-xs">
        <q-item v-for="blog in last5blogs" :key="blog.id">
          <q-item-section top thumbnail class="q-ml-none">
            <img :src="apiurl + blog.image" />
          </q-item-section>
          <q-item-section>
            <q-item-label
              class="hovereffect"
              @click="goBlogDetail(blog.slug)"
              >{{ blog.title }}</q-item-label
            >
            <q-item-label>{{ formatDate(blog.createdOn) }}</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-card-section>
  </div>
</template>

<script>
import { date } from "quasar";
export default {
  created() {
    this.$store.dispatch("blog/getLast5Blogs");
  },
  computed: {
    last5blogs() {
      return this.$store.getters["blog/getLast5Blogs"];
    },
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    }
  },
  methods: {
    formatDate(blogdate) {
      return date.formatDate(blogdate, "DD/MM/YYYY HH:mm");
    },
    goBlogDetail(slug) {
      this.$router.push({ name: "blog-detail", params: { slug: slug } });
    }
  }
};
</script>

<style lang="scss" scoped>
.hovereffect {
  cursor: pointer;
}
</style>
