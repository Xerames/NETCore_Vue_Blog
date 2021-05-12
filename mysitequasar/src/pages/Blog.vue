<template>
  <div>
    <div class="container">
      <div class="row q-col-gutter-lg">
        <div class="col-lg-8 col-sm-12 col-xs-12">
          <Blog :blogs="blogs"></Blog>
          <div class="q-pa-lg flex flex-center">
            <q-pagination
              v-if="this.blogs.length > 0"
              :to-fn="page => ({ query: { page } })"
              v-model="query.page"
              :max="totalpage"
              :direction-links="true"
            >
            </q-pagination>
          </div>
        </div>
        <div class="col-lg-4 col-sm-12 col-xs-12">
          <q-card
            class="q-mt-xl q-mb-xl"
            :class="this.$q.screen.lt.sm ? 'q-ma-md' : ''"
          >
            <Search></Search>
            <Category></Category>
            <RecentPost></RecentPost>
          </q-card>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Search from "components/Search.vue";
import Category from "components/Category.vue";
import RecentPost from "components/RecentPost.vue";
import Blog from "components/Blog.vue";
export default {
  components: {
    Search,
    Category,
    Blog,
    RecentPost
  },
  data() {
    return {
      blogs: [],
      totalitems: null,
      query: {
        page: 1,
        pagesize: 3
      }
    };
  },
  computed: {
    totalpage() {
      return Math.ceil(this.totalitems / this.query.pagesize);
    }
  },
  created() {
    this.getBlogs();
    this.getBlogsByCategoryName();
    this.getBlogsByTagName();
    this.getBlogsBySearch();
  },
  watch: {
    $route() {
      this.getBlogs();
      this.getBlogsByCategoryName();
      this.getBlogsByTagName();
      this.getBlogsBySearch();
    },
    "$route.query"() {
      this.getBlogs();
      this.getBlogsByCategoryName();
      this.getBlogsByTagName();
      this.getBlogsBySearch();
    }
  },
  methods: {
    toQueryString(obj) {
      var parts = [];
      for (var property in obj) {
        var value = obj[property];
        if (value != null && value != undefined)
          parts.push(
            encodeURIComponent(property) + "=" + encodeURIComponent(value)
          );
      }
      return parts.join("&");
    },
    getBlogs() {
      if (
        this.$route.params.categoryname == null &&
        this.$route.params.tagname == null
      ) {
        this.query.page = this.$route.query.page || 1;
        this.$store
          .dispatch("blog/getBlogsWithCategory", this.toQueryString(this.query))
          .then(() => {
            this.blogs = this.$store.getters["blog/getBlogsWithCategory"];
            this.totalitems = this.$store.getters[
              "blog/getBlogsWithCategoryTotalCount"
            ];
          });
      }
    },
    getBlogsByCategoryName() {
      if (this.$route.params.categoryname != null) {
        this.query.page = this.$route.query.page || 1;
        var payload = {
          categoryname: this.$route.params.categoryname,
          query: this.toQueryString(this.query)
        };
        this.$store
          .dispatch("blog/getBlogsByCategoryName", payload)
          .then(response => {
            this.blogs = this.$store.getters["blog/getBlogsByCategoryName"];
            this.totalitems = this.$store.getters[
              "blog/getBlogsByCategoryNameTotalCount"
            ];
          });
      }
    },
    getBlogsByTagName() {
      if (this.$route.params.tagname != null) {
        this.query.page = this.$route.query.page || 1;
        var payload = {
          tagname: this.$route.params.tagname,
          query: this.toQueryString(this.query)
        };
        this.$store.dispatch("blog/getBlogsByTagName", payload).then(() => {
          this.blogs = this.$store.getters["blog/getBlogsByTagName"];
          this.totalitems = this.$store.getters[
            "blog/getBlogsByTagNameTotalCount"
          ];
        });
      }
    },
    getBlogsBySearch() {
      if (this.$route.query.search != null) {
        this.query.page = this.$route.query.page || 1;
        var payload = {
          searchquery: this.$route.query.search,
          query: this.toQueryString(this.query)
        };
        this.$store.dispatch("blog/searchBlogs", payload).then(response => {
          this.blogs = response.data;
          this.totalitems = response.totalItems;
        });
      }
    }
  }
};
</script>

<style lang="scss" scoped></style>
