<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Categories"
        :data="categories"
        :hide-header="mode === 'grid'"
        :columns="columns"
        :grid="mode == 'grid'"
        :filter="filter"
        :pagination.sync="pagination"
      >
        <template v-slot:top-right="props">
          <q-btn
            @click="showmodal = true"
            outline
            color="primary"
            label="Add New"
            class="q-mr-xs"
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
                @click="getCategory(props.row.id)"
                icon="edit"
              />
              <q-btn
                dense
                color="red"
                @click="deleteCategory(props.row.id)"
                icon="delete"
              />
            </div>
          </q-td>
        </template>
      </q-table>
    </q-card>
    <q-dialog v-model="showmodal" persistent>
      <q-card style="width: 600px; max-width: 60vw;">
        <q-card-section>
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
          <div class="text-h6">
            <span v-if="this.category.id > 0">Update Category</span>
            <span v-else>Add New Category</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetCategory"
            ></q-btn>
          </div>
        </q-card-section>
        <q-separator inset></q-separator>
        <q-card-section class="q-pt-none">
          <q-form class="q-gutter-md">
            <q-list>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="category.categoryName"
                    :rules="[
                      val =>
                        this.$v.category.categoryName.required ||
                        'Category Name is required'
                    ]"
                    label="Category Name"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="category.description"
                    :rules="[
                      val =>
                        this.$v.category.description.required ||
                        'Description is required'
                    ]"
                    label="Description"
                  />
                </q-item-section>
              </q-item>
            </q-list>
          </q-form>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn
            label="Update"
            @click="UpdateCategory"
            :disabled="$v.$invalid"
            color="primary"
            v-if="this.category.id > 0"
          />
          <q-btn
            label="Add"
            @click="AddCategory"
            :disabled="$v.$invalid"
            color="primary"
            v-else
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script>
import { exportFile } from "quasar";
import { required } from "vuelidate/lib/validators";
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
      showmodal: false,
      category: {
        id: null,
        categoryName: "",
        description: ""
      },
      show: false,
      message: null,
      errors: [],
      filter: "",
      mode: "list",
      columns: [
        {
          name: "Category Name",
          align: "left",
          label: "Category Name",
          field: "categoryName",
          sortable: true
        },
        {
          name: "Description",
          align: "left",
          label: "Description",
          field: "description",
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
    category: {
      categoryName: {
        required
      },
      description: {
        required
      }
    }
  },
  created() {
    this.$store.dispatch("category/getCategories");
  },
  computed: {
    categories() {
      return this.$store.getters["category/getCategories"];
    },
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
    exportTable() {
      // naive encoding to csv format
      const content = [this.columns.map(col => wrapCsvValue(col.label))]
        .concat(
          this.categories.map(row =>
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
      const status = exportFile("categories.csv", content, "text/csv");
      if (status !== true) {
        this.$q.notify({
          message: "Browser denied file download...",
          color: "negative",
          icon: "warning"
        });
      }
    },
    AddCategory() {
      var payload = {
        categoryName: this.category.categoryName,
        description: this.category.description
      };
      this.$store
        .dispatch("category/addCategory", payload)
        .then(response => {
          if (response.success){
            this.show = true;
            this.message = response.message;
            this.resetCategory();
          }
        })
        .catch(error => {
          console.log(error);
          this.show = true;
          this.errors = error;
        });
    },
    UpdateCategory() {
      var payload = {
        id: this.category.id,
        categoryName: this.category.categoryName,
        description: this.category.description
      };
      this.$store
        .dispatch("category/updateCategory", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.resetCategory();
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
        });
    },
    deleteCategory(id) {
      this.$store.dispatch("category/deleteCategory", id);
    },
    getCategory(id) {
      if (id > 0) {
        this.showmodal = true;
        this.$store.dispatch("category/getCategory", id).then(response => {
          this.category = response;
        });
      }
    },
    resetCategory() {
      (this.category.id = null),
        (this.category.categoryName = ""),
        (this.category.description = ""),
        (this.showmodal = false);
      (this.show = false), (this.message = null), (this.errors = []);
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
