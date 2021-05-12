<template>
  <div class="q-pa-xs">
    <div class="sidebar-title q-ml-md text-deep-purple-6">Categories</div>
    <q-card-section>
      <div class="categories q-ml-xs">
        <q-list class="rounded-borders" v-for="category in categories" :key="category.id">
          <q-item
            clickable
            v-ripple
            :to="{
              name: 'category-blogs',
              params: { categoryname: category.slug }
            }"
            active-class="my-menu-link"
          >
            <q-item-section>{{category.categoryName}} ({{category.blogs.length}})</q-item-section>
          </q-item>
        </q-list>
      </div>
    </q-card-section>
  </div>
</template>

<script>
export default {
  created() {
    this.$store.dispatch("category/getCategories");
  },
  computed: {
    categories() {
      return this.$store.getters["category/getCategories"];
    }
  }
};
</script>

<style lang="scss" scoped>

.my-menu-link{
  color: white;
  background: #673ab7;
}

</style>
