<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Blogs"
        :data="blogs"
        :hide-header="mode === 'grid'"
        :columns="columns"
        :grid="mode == 'grid'"
        :filter="filter"
        :pagination.sync="pagination"
      >
        <template v-slot:top-right="props">
          <q-btn
            outline
            color="primary"
            label="Add New"
            class="q-mr-xs"
            to="addblog"
          />
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
          <q-btn
            color="primary"
            icon-right="archive"
            label="Export to csv"
            no-caps
            @click="exportTable"
          />
        </template>
        <template v-slot:body-cell-action="props">
          <q-td :props="props">
            <div class="q-gutter-sm">
              <q-btn
                dense
                color="primary"
                :to="{
                  name: 'DashboardUpdateBlog',
                  params: { id: props.row.id }
                }"
                icon="edit"
              />
              <q-btn
                dense
                color="red"
                @click="deleteBlog(props.row.id)"
                icon="delete"
              />
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-Image="props">
          <q-td :props="props">
            <q-img
              height="72px"
              width="72px"
              :src="apiurl + props.row.image"
            ></q-img>
          </q-td>
        </template>
        <template v-slot:body-cell-Content="props">
          <q-td :props="props" v-html="props.row.content"></q-td>
        </template>
      </q-table>
    </q-card>
  </q-page>
</template>

<script>
import { exportFile } from "quasar";
function wrapCsvValue(val, formatFn) {
  let formatted = formatFn !== void 0 ? formatFn(val) : val;
  formatted =
    formatted === void 0 || formatted === null ? "" : String(formatted);
  formatted = formatted.split('"').join('""');
  return `"${formatted}"`;
}
export default {
  data() {
    return {
      filter: "",
      mode: "list",
      columns: [
        {
          name: "Image",
          align: "left",
          label: "Image",
          field: "image",
          sortable: true
        },
        {
          name: "Title",
          align: "left",
          label: "Title",
          field: "title",
          sortable: true
        },
        {
          name: "Content",
          required: true,
          label: "Content",
          align: "left",
          field: "content",
          sortable: true
        },
        {
          name: "CategoryName",
          required: true,
          label: "Category Name",
          align: "left",
          field: row => row.category.categoryName,
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
  created() {
    this.$store.dispatch("blog/getBlogsWithCategory");
  },
  computed: {
    blogs() {
      return this.$store.getters["blog/getBlogsWithCategory"];
    },
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    }
  },
  methods: {
    exportTable() {
      // naive encoding to csv format
      const content = [this.columns.map(col => wrapCsvValue(col.label))]
        .concat(
          this.blogs.map(row =>
            this.columns
              .map(col =>
                wrapCsvValue(
                  typeof col.field === "function"
                    ? col.field(row)
                    : row[col.field === void 0 ? col.name : col.field],
                  col.format
                )
              )
              .join(",")
          )
        )
        .join("\r\n");
      const status = exportFile("blogs.csv", content, "text/csv");
      if (status !== true) {
        this.$q.notify({
          message: "Browser denied file download...",
          color: "negative",
          icon: "warning"
        });
      }
    },
    deleteBlog(id) {
      this.$store.dispatch("blog/deleteBlog", id);
    },
    resetBlog() {
      (this.blog.id = null),
        (this.image = null),
        (this.currentimage = null),
        (this.previewimage = null),
        (this.blog.title = ""),
        (this.blog.content = ""),
        (this.blog.categoryName = ""),
        (this.showmodal = false);
    }
  },
  filters: {
    truncate: function(text, length, suffix) {
      if (text.length > length) {
        return text.substring(0, length) + suffix;
      } else {
        return text;
      }
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
