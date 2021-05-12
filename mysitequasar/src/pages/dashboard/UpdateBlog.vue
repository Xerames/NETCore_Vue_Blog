<template>
  <q-page class="q-pa-sm">
    <div class="row q-col-gutter-md">
      <div class="col-8">
        <q-card>
          <q-card-section>
            <div class="text-h6">
              <span>Update Blog</span>
            </div>
          </q-card-section>
          <q-separator inset></q-separator>
          <q-card-section class="q-pt-none">
            <q-banner
              class="text-white q-mb-md"
              :class="success"
              v-if="this.show"
            >
              <div class="row items-center">
                <div class="col-2">
                  <i :class="isError"></i>
                </div>
                <div class="col-10">
                  <div class="q-ma-xs" v-if="this.message != null">
                    {{ message }}
                  </div>
                  <div class="q-ma-xs" v-else>
                    <div v-for="(error, index) in errors" :key="index">
                      {{ error }}
                    </div>
                  </div>
                </div>
              </div>
            </q-banner>
            <q-form class="q-gutter-md">
              <q-list>
                <q-item>
                  <q-item-section>
                    <q-input
                      dense
                      outlined
                      v-model="blog.title"
                      :rules="[
                        val =>
                          this.$v.blog.title.required || 'Title is required'
                      ]"
                      label="Title"
                    />
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section>
                    <q-file
                      clearable
                      v-model="image"
                      accept="image/png,image/jpeg"
                      label="Select Image"
                      :disable="image != null"
                    >
                      <template v-slot:prepend>
                        <q-icon name="cloud_upload" />
                      </template>
                    </q-file>
                  </q-item-section>
                </q-item>
                <div class="row justify-center">
                  <div class="col-3" v-if="previewimage">
                    <div class="row justify-center">
                      <label class="text-dark q-mb-xs">Selected Image</label>
                    </div>
                    <div class="row justify-center">
                      <q-img
                        v-if="previewimage"
                        :src="apiurl + previewimage"
                        style="max-width:200px;"
                      />
                      <q-btn @click="deleteImage()" label="DeleteImage"></q-btn>
                    </div>
                  </div>
                  <div class="col-3" v-if="currentimage">
                    <div class="row justify-center">
                      <label class="text-dark q-mb-xs">Current Image</label>
                    </div>
                    <div class="row justify-center">
                      <q-img
                        :src="apiurl + currentimage"
                        v-if="currentimage != null"
                        style="max-width:200px;"
                      />
                    </div>
                  </div>
                </div>
                <q-item>
                  <q-item-section>
                    <q-editor v-model="blog.content" min-height="15rem" />
                    <small v-if="this.$v.blog.content.$invalid" class="text-red"
                      >Content is required</small
                    >
                  </q-item-section>
                </q-item>
                <q-item>
                  <q-item-section>
                    <q-select
                      v-model="blog.categoryId"
                      :options="categories"
                      emit-value
                      map-options
                      option-value="id"
                      option-label="categoryName"
                      label="Category Name"
                    />
                  </q-item-section>
                </q-item>
              </q-list>
            </q-form>
          </q-card-section>
          <q-card-section>
            <div class="text-h6 text-center">
              <span>Select Tags</span>
            </div>
            <q-item>
              <div class="row q-gutter-sm">
                <div v-for="tag in tags" :key="tag.id">
                  <label class="b-contain">
                    <span>{{ tag.name }}</span>
                    <input
                      type="checkbox"
                      :value="tag.id"
                      v-model="blog.selectedtags"
                    />
                    <div class="b-input"></div>
                  </label>
                </div>
              </div>
            </q-item>
            <div class="row q-ml-md">
              <small v-if="$v.blog.selectedtags.$invalid" class="text-red"
                >Please select tag</small
              >
            </div>
          </q-card-section>
          <q-card-actions align="center" class="text-teal">
            <q-btn
              label="Update Blog"
              @click="UpdateBlog"
              :disabled="$v.blog.$invalid"
              color="primary"
            />
          </q-card-actions>
        </q-card>
      </div>
      <div class="col-4">
        <q-card>
          <q-card-section class="q-pt-none">
            <q-form>
              <q-list>
                <div class="text-h6 text-center q-pa-md">
                  <span>Add Tag</span>
                </div>
                <q-banner
                  class="text-white q-mb-md"
                  :class="success"
                  v-if="this.tagshow"
                >
                  <div class="row items-center">
                    <div class="col-2">
                      <i :class="isError"></i>
                    </div>
                    <div class="col-10">
                      <div class="q-ma-xs" v-if="this.tagmessage != null">
                        {{ tagmessage }}
                      </div>
                      <div class="q-ma-xs" v-else>
                        <div v-for="(error, index) in tagerrors" :key="index">
                          {{ error }}
                        </div>
                      </div>
                    </div>
                  </div>
                </q-banner>
                <q-item>
                  <q-item-section>
                    <q-input
                      dense
                      outlined
                      v-model="tag.name"
                      :rules="[
                        val =>
                          this.$v.tag.name.required || 'Tag Name is required'
                      ]"
                      label="Tag Name"
                    />
                  </q-item-section>
                </q-item>
              </q-list>
            </q-form>
          </q-card-section>
          <q-card-actions align="center" class="text-teal">
            <q-btn
              label="Add Tag"
              @click="AddTag"
              :disabled="$v.tag.name.$invalid"
              color="primary"
            />
          </q-card-actions>
        </q-card>
      </div>
    </div>
  </q-page>
</template>

<script>
import { required } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      blog: {
        id: null,
        content: "",
        title: "",
        categoryId: "",
        blogTags: [],
        selectedtags: [],
        image: ""
      },
      tag: {
        name: ""
      },
      image: null,
      currentimage: null,
      previewimage: null,
      show: false,
      message: null,
      errors: [],
      tagshow: false,
      tagmessage: null,
      tagerrors: []
    };
  },
  validations: {
    blog: {
      title: {
        required
      },
      content: {
        required
      },
      categoryId: {
        required
      },
      selectedtags: {
        required
      }
    },
    tag: {
      name: {
        required
      }
    }
  },
  created() {
    this.$store.dispatch("category/getCategories");
    this.$store.dispatch("tag/getTags");
    this.getBlog();
  },
  computed: {
    categories() {
      return this.$store.getters["category/getCategories"];
    },
    tags() {
      return this.$store.getters["tag/getTags"];
    },
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    },
    success() {
      return this.message != null || this.tagmessage != null
        ? "bg-green"
        : "bg-red";
    },
    isError() {
      return this.errors.length > 0 || this.tagerrors.length > 0
        ? "fas fa-exclamation-circle fa-2x"
        : "fas fa-check-circle fa-2x";
    }
  },
  watch: {
    image(newValue, oldValue) {
      if (this.image != null) {
        this.setPreviewImage();
      } else {
        this.previewimage = null;
      }
    }
  },
  methods: {
    UpdateBlog() {
      var payload = {
        id: this.blog.id,
        title: this.blog.title,
        content: this.blog.content,
        categoryId: this.blog.categoryId,
        description: this.blog.description,
        image:
          this.previewimage == null ? this.currentimage : this.previewimage,
        tagsids: this.blog.selectedtags
      };
      this.$store
        .dispatch("blog/updateBlog", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          setTimeout(() => this.$router.push("/dashboard/blogs"), 3000);
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
          this.deleteImage();
        });
    },
    AddTag() {
      var payload = {
        name: this.tag.name
      };
      this.$store
        .dispatch("tag/addTag", payload)
        .then(response => {
          this.tagshow = true;
          this.tag.name = "";
          this.tagmessage = response.message;
          setTimeout(() => this.resetTag(), 2000);
        })
        .catch(error => {
          this.show = true;
          this.tagerrors = error;
          setTimeout(() => this.resetTag(), 2000);
        });
    },
    getBlog() {
      this.$store
        .dispatch("blog/getBlogWithCategoryAndTags", this.$route.params.id)
        .then(response => {
          this.blog = response;
          this.currentimage = response.image;
          const arr = new Array();
          for (let i = 0; i < this.blog.blogTags.length; i++) {
            arr.push(this.blog.blogTags[i].tagId);
          }
          this.blog.selectedtags = arr;
        });
    },
    checkselected(id) {
      return this.blog.blogTags.some(item => item.tagId === id);
    },
    onChange(value, $event) {
      let checked = $event.target.checked;
      if (checked) {
        this.blog.selectedtags.push(value);
      } else {
        let index = this.blog.selectedtags.findIndex(x => x == value);
        this.blog.selectedtags.splice(index, 1);
      }
    },
    setPreviewImage() {
      //this.previewimage = URL.createObjectURL(this.image);
      const data = new FormData();
      data.append("Image", this.image);
      this.$store.dispatch("blog/uploadimage", data).then(response => {
        this.previewimage = response;
        this.blog.image = response;
      });
    },
    deleteImage() {
      var payload = {
        image: this.blog.image
      };
      this.$store.dispatch("blog/deleteimage", payload).then(response => {
        if (response.success) {
          this.blog.image = null;
          this.previewimage = null;
          this.image = null;
        }
      });
    },
    resetTag() {
      (this.tag.name = null),
        (this.tagshow = false),
        (this.tagmessage = null),
        (this.tagerrors = []);
    },
    resetBlog() {
      (this.image = null),
        (this.currentimage = null),
        (this.previewimage = null),
        (this.blog.content = ""),
        (this.blog.title = ""),
        (this.blog.image = ""),
        (this.blog.categoryId = null),
        (this.blog.selectedtags = [])((this.showmodal = false));
      (this.show = false), (this.message = null), (this.errors = []);
    }
  },
  beforeRouteLeave(to, from, next) {
    if (this.previewimage != null) {
      this.deleteImage();
      next();
    } else {
      next();
    }
  }
};
</script>
<style scoped>
.q-chip__content {
  display: block;
  text-align: center;
}
</style>
