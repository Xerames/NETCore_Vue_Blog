<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-card-section>
        <div class="text-h6">
          <span>Update Tag</span>
        </div>
      </q-card-section>
      <q-separator inset></q-separator>
      <q-card-section class="q-pt-none">
        <q-banner class="text-white q-mb-md" :class="success" v-if="this.show">
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
                  v-model="tag.name"
                  label="Tag Name"
                  :rules="[
                    val => this.$v.tag.name.required || 'Tag Name is required'
                  ]"
                />
              </q-item-section>
            </q-item>
          </q-list>
        </q-form>
      </q-card-section>
      <q-card-actions align="right" class="text-teal">
        <q-btn
          label="Update"
          @click="UpdateTag"
          :disabled="$v.$invalid"
          color="primary"
        />
      </q-card-actions>
      <q-table
        title="Blog Tags"
        :data="tag.blogTags"
        :hide-header="mode === 'grid'"
        :columns="columns"
        :grid="mode == 'grid'"
        :filter="filter"
        :pagination.sync="pagination"
      >
        <template v-slot:top-right="props">
          <q-input
            outlined
            dense
            debounce="300"
            v-model="filter"
            placeholder="Search"
          >
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </q-input>
          <q-btn
            flat
            round
            dense
            :icon="props.inFullscreen ? 'fullscreen_exit' : 'fullscreen'"
            @click="props.toggleFullscreen"
            v-if="mode === 'list'"
          >
            <q-tooltip :disable="$q.platform.is.mobile" v-close-popup
              >{{
                props.inFullscreen ? "Exit Fullscreen" : "Toggle Fullscreen"
              }}
            </q-tooltip>
          </q-btn>

          <q-btn
            flat
            round
            dense
            :icon="mode === 'grid' ? 'list' : 'grid_on'"
            @click="
              mode = mode === 'grid' ? 'list' : 'grid';
              separator = mode === 'grid' ? 'none' : 'horizontal';
            "
            v-if="!props.inFullscreen"
          >
            <q-tooltip :disable="$q.platform.is.mobile" v-close-popup
              >{{ mode === "grid" ? "List" : "Grid" }}
            </q-tooltip>
          </q-btn>
        </template>
        <template v-slot:body-cell-action="props">
          <q-td :props="props">
            <div class="q-gutter-sm">
              <q-btn
                dense
                color="red"
                @click="deleteBlogFromTag(props.row.blogId, props.row.tagId)"
                icon="delete"
              />
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-Title="props">
          <q-td :props="props">
            {{ props.row.blog.title }}
          </q-td>
        </template>
        <template v-slot:body-cell-Content="props">
          <q-td :props="props" v-html="props.row.blog.content"> </q-td>
        </template>
      </q-table>
    </q-card>
  </q-page>
</template>

<script>
import { required } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      showmodal: false,
      tag: {
        id: null,
        name: ""
      },
      filter: "",
      mode: "list",
      show: false,
      message: null,
      errors: [],
      columns: [
        {
          name: "Title",
          align: "left",
          label: "Title",
          field: row => row.blog.title,
          sortable: true
        },
        {
          name: "Content",
          align: "left",
          label: "Content",
          field: row => row.blog.content,
          sortable: true
        },
        {
          name: "action",
          align: "left",
          label: "Actions",
          field: "action"
        }
      ],
      pagination: {
        rowsPerPage: 10
      }
    };
  },
  validations: {
    tag: {
      name: {
        required
      }
    }
  },
  created() {
    this.id = this.$route.params.id;
    this.$store.dispatch("tag/getTagWithBlogs", this.id).then(response => {
      this.tag = response;
    });
  },
  computed: {
    success() {
      return this.message != null ? "bg-green" : "bg-red";
    },
    isError() {
      return this.errors.length > 0
        ? "fas fa-exclamation-circle fa-2x"
        : "fas fa-check-circle fa-2x";
    }
  },
  methods: {
    UpdateTag() {
      var payload = {
        id: this.tag.id,
        name: this.tag.name
      };
      this.$store
        .dispatch("tag/updateTag", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          setTimeout(() => this.$router.push("/dashboard/tags"), 3000);
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
        });
    },
    deleteBlogFromTag(blogid, tagid) {
      var payload = {
        blogid: blogid,
        tagid: tagid
      };
      this.$store.dispatch("tag/deleteBlogFromTag", payload).then(response => {
        if (response.success) {
          let index = this.tag.blogTags.findIndex(b => b.blog.id == blogid);
          if (index > -1) {
            this.tag.blogTags.splice(index, 1);
          }
        }
      });
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
