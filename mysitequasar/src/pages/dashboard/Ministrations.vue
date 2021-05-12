<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Ministrations"
        :data="ministrations"
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
                @click="getMinistration(props.row.id)"
                icon="edit"
              />
              <q-btn
                dense
                color="red"
                @click="deleteMinistration(props.row.id)"
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
            <span v-if="this.ministration.id > 0">Update Ministration</span>
            <span v-else>Add New Ministration</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetMinistration"
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
                    v-model="ministration.title"
                    :rules="[
                      val =>
                        this.$v.ministration.title.required ||
                        'Title is required'
                    ]"
                    label="Title"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="ministration.description"
                    :rules="[
                      val =>
                        this.$v.ministration.description.required ||
                        'Description is required'
                    ]"
                    label="Description"
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
                  <small
                    v-if="$v.image.$invalid && this.ministration.id == null"
                    class="text-red"
                    >Image is required</small
                  >
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
                      style="width:80px;height:80px"
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
                      style="width:80px;height:80px"
                    />
                  </div>
                </div>
              </div>
            </q-list>
          </q-form>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn
            label="Update"
            @click="UpdateMinistration"
            :disabled="$v.ministration.$invalid"
            color="primary"
            v-if="this.ministration.id > 0"
          />
          <q-btn
            label="Add"
            @click="AddMinistration"
            :disabled="$v.ministration.$invalid || $v.image.$invalid"
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
      ministration: {
        id: null,
        title: "",
        description: "",
        image: ""
      },
      image: null,
      currentimage: null,
      previewimage: null,
      show: false,
      message: null,
      errors: [],
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
          name: "Description",
          required: true,
          label: "Description",
          align: "left",
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
    ministration: {
      description: {
        required
      },
      title: {
        required
      }
    },
    image: {
      required
    }
  },
  created() {
    this.$store.dispatch("ministration/getMinistrations");
  },
  computed: {
    ministrations() {
      return this.$store.getters["ministration/getMinistrations"];
    },
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
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
    exportTable() {
      // naive encoding to csv format
      const content = [this.columns.map(col => wrapCsvValue(col.label))]
        .concat(
          this.ministrations.map(row =>
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
      const status = exportFile("ministrations.csv", content, "text/csv");
      if (status !== true) {
        this.$q.notify({
          message: "Browser denied file download...",
          color: "negative",
          icon: "warning"
        });
      }
    },
    setPreviewImage() {
      //this.previewimage = URL.createObjectURL(this.image);
      const data = new FormData();
      data.append("Image", this.image);
      this.$store.dispatch("ministration/uploadimage", data).then(response => {
        this.previewimage = response;
        this.ministration.image = response;
      });
    },
    deleteImage() {
      var payload = {
        image: this.ministration.image
      };
      this.$store
        .dispatch("ministration/deleteimage", payload)
        .then(response => {
          if (response.success) {
            this.ministration.image = null;
            this.previewimage = null;
            this.image = null;
          }
        });
    },
    AddMinistration() {
      var payload = {
        title: this.ministration.title,
        description: this.ministration.description,
        image: this.ministration.image
      };
      this.$store
        .dispatch("ministration/addMinistration", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.previewimage = null;
          this.resetMinistration();
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
          this.deleteImage();
        });
    },
    UpdateMinistration() {
      var payload = {
        id: this.ministration.id,
        title: this.ministration.title,
        description: this.ministration.description,
        image: this.previewimage == null ? this.currentimage : this.previewimage
      };
      this.$store
        .dispatch("ministration/updateMinistration", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.currentimage = this.previewimage;
          this.previewimage = null;
          this.resetMinistration();
        }).catch(error=>{
          this.show = true;
          this.errors=error;
          this.deleteImage();
        });
    },
    deleteMinistration(id) {
      this.$store.dispatch("ministration/deleteMinistration", id);
    },
    getMinistration(id) {
      if (id > 0) {
        this.showmodal = true;
        this.$store
          .dispatch("ministration/getMinistration", id)
          .then(response => {
            this.ministration = response;
            this.currentimage = response.image;
          });
      }
    },
    resetMinistration() {
      if (this.previewimage != null) {
        this.deleteImage();
      }
      (this.ministration.id = null),
        (this.image = null),
        (this.currentimage = null),
        (this.previewimage = null),
        (this.ministration.title = ""),
        (this.ministration.description = ""),
        (this.ministration.image = ""),
        (this.showmodal = false);
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
