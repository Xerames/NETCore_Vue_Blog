<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Sliders"
        :data="sliders"
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
                @click="getSlider(props.row.id)"
                icon="edit"
              />
              <q-btn
                dense
                color="red"
                @click="deleteSlider(props.row.id)"
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
            <span v-if="this.slider.id > 0">Update Slider</span>
            <span v-else>Add New Slider</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetSlider"
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
                    v-model="slider.title"
                    :rules="[
                      val =>
                        this.$v.slider.title.required || 'Title is required'
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
                    v-model="slider.description"
                    :rules="[
                      val =>
                        this.$v.slider.description.required ||
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
                    :disable="image != null"
                    accept="image/png,image/jpeg"
                    label="Select Image"
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
            @click="UpdateSlider"
            color="primary"
            :disabled="$v.$invalid"
            v-if="this.slider.id > 0"
          />
          <q-btn
            label="Add"
            @click="AddSlider"
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
      slider: {
        id: null,
        description: "",
        title: "",
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
    slider: {
      description: {
        required
      },
      title: {
        required
      }
    }
  },
  created() {
    this.$store.dispatch("slider/getSliders");
  },
  computed: {
    sliders() {
      return this.$store.getters["slider/getSliders"];
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
          this.sliders.map(row =>
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
      const status = exportFile("sliders.csv", content, "text/csv");
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
      this.$store.dispatch("slider/uploadimage", data).then(response => {
        this.previewimage = response;
        this.slider.image = response;
      });
    },
    deleteImage() {
      var payload = {
        image: this.slider.image
      };
      this.$store.dispatch("slider/deleteimage", payload).then(response => {
        if (response.success) {
          this.slider.image = null;
          this.previewimage = null;
          this.image = null;
        }
      });
    },
    AddSlider() {
      var payload = {
        description: this.slider.description,
        image: this.slider.image,
        title: this.slider.title
      };
      this.$store
        .dispatch("slider/addSlider", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.previewimage = null;
          this.resetSlider();
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
          this.deleteImage();
        });
    },
    UpdateSlider() {
      var payload = {
        id: this.slider.id,
        title: this.slider.title,
        description: this.slider.description,
        image: this.previewimage == null ? this.currentimage : this.previewimage
      };
      this.$store
        .dispatch("slider/updateSlider", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.currentimage = this.previewimage;
          this.previewimage = null;
          this.resetSlider();
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
        });
    },
    deleteSlider(id) {
      this.$store.dispatch("slider/deleteSlider", id);
    },
    getSlider(id) {
      if (id > 0) {
        this.showmodal = true;
        this.$store.dispatch("slider/getSlider", id).then(response => {
          this.slider = response;
          this.currentimage = response.image;
        });
      }
    },
    resetSlider() {
      if (this.previewimage != null) {
        this.deleteImage();
      }
      (this.slider.id = null),
        (this.image = null),
        (this.currentimage = null),
        (this.previewimage = null),
        (this.slider.description = ""),
        (this.slider.title = ""),
        (this.slider.image = ""),
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
