<template>
  <div>
    <div class="container">
      <div class="row q-col-gutter-lg">
        <div class="col-lg-8 col-sm-12 ">
          <BlogDetail v-if="blog.blog != null" :blog="blog.blog"></BlogDetail>
          <comments-and-replies
            v-if="blog.blog != null"
            :comments="blog.blog.comments"
            :totalcomments="blog.totalComments"
            :blogid="blog.blog.id"
          ></comments-and-replies>
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
import BlogDetail from "components/BlogDetail.vue";
import CommentsAndReplies from "components/CommentsAndReplies.vue";
export default {
  components: {
    Search,
    Category,
    BlogDetail,
    RecentPost,
    CommentsAndReplies
  },
  created() {
    this.getBlogDetail();
  },
  watch: {
    "$route.params.slug"(to, from) {
      this.getBlogDetail();
    },
  },
  methods: {
    getBlogDetail() {
      this.$store.dispatch("blog/getBlogDetail", this.$route.params.slug);
    }
  },
  computed: {
    blog() {
      return this.$store.getters["blog/getBlogDetail"];
    }
  }
};
</script>

<style lang="scss" scoped></style>
