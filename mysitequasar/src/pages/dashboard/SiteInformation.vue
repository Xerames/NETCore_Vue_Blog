<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Site Informations"
        :data="siteinformations"
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
                @click="getSiteInformation(props.row.id)"
                icon="edit"
              />
              <q-btn
                dense
                color="red"
                @click="deleteSiteInformation(props.row.id)"
                icon="delete"
              />
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-Logo="props">
          <q-td :props="props">
            <q-img
              height="72px"
              width="72px"
              :src="apiurl + props.row.logo"
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
            <span v-if="this.siteinformation.id > 0"
              >Update Site Information</span
            >
            <span v-else>Add New Site Information</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetSiteInformation"
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
                    v-model="siteinformation.title"
                    :rules="[
                      val =>
                        this.$v.siteinformation.title.required ||
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
                    v-model="siteinformation.description"
                    label="Description"
                    :rules="[
                      val =>
                        this.$v.siteinformation.description.required ||
                        'Description is required'
                    ]"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="siteinformation.keywords"
                    :rules="[
                      val =>
                        this.$v.siteinformation.keywords.required ||
                        'Keywords is required'
                    ]"
                    label="Keywords"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-file
                    clearable
                    v-model="logo"
                    accept="image/png,image/jpeg"
                    label="Select Image"
                    :disable="logo != null"
                  >
                    <template v-slot:prepend>
                      <q-icon name="cloud_upload" />
                    </template>
                  </q-file>
                  <small
                    v-if="$v.logo.$invalid && this.siteinformation.id == null"
                    class="text-red"
                    >Image is required</small
                  >
                </q-item-section>
              </q-item>
              <div class="row justify-center">
                <div class="col-3" v-if="previewlogo">
                  <div class="row justify-center">
                    <label class="text-dark q-mb-xs">Selected Logo</label>
                  </div>
                  <div class="row justify-center">
                    <q-img
                      v-if="previewlogo"
                      :src="apiurl + previewlogo"
                      style="width:80px;height:80px"
                    />
                    <q-btn @click="deleteImage()" label="DeleteImage"></q-btn>
                  </div>
                </div>
                <div class="col-3" v-if="currentlogo">
                  <div class="row justify-center">
                    <label class="text-dark q-mb-xs">Current Logo</label>
                  </div>
                  <div class="row justify-center">
                    <q-img
                      :src="apiurl + currentlogo"
                      v-if="currentlogo != null"
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
            @click="UpdateSiteInformation"
            color="primary"
            :disabled="$v.siteinformation.$invalid"
            v-if="this.siteinformation.id > 0"
          />
          <q-btn
            label="Add"
            @click="AddSiteInformation"
            :disabled="$v.siteinformation.$invalid || $v.logo.$invalid"
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
      siteinformation: {
        id: null,
        description: "",
        title: "",
        keywords: "",
        logo: ""
      },
      logo: null,
      currentlogo: null,
      previewlogo: null,
      filter: "",
      mode: "list",
      show: false,
      message: null,
      errors: [],
      columns: [
        {
          name: "Logo",
          align: "left",
          label: "Logo",
          field: "logo",
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
          name: "Keywords",
          align: "left",
          label: "Keywords",
          field: "keywords",
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
    siteinformation: {
      description: {
        required
      },
      title: {
        required
      },
      keywords: {
        required
      }
    },
    logo: {
      required
    }
  },
  created() {
    this.$store.dispatch("siteinformation/getSiteInformations");
  },
  computed: {
    siteinformations() {
      return this.$store.getters["siteinformation/getSiteInformations"];
    },
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    }
  },
  watch: {
    logo(newValue, oldValue) {
      if (this.logo != null) {
        this.setPreviewLogo();
      } else {
        this.previewlogo = null;
      }
    }
  },
  methods: {
    exportTable() {
      // naive encoding to csv format
      const content = [this.columns.map(col => wrapCsvValue(col.label))]
        .concat(
          this.siteinformations.map(row =>
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
      const status = exportFile("siteinformations.csv", content, "text/csv");
      if (status !== true) {
        this.$q.notify({
          message: "Browser denied file download...",
          color: "negative",
          icon: "warning"
        });
      }
    },
    setPreviewLogo() {
      const data = new FormData();
      data.append("Image", this.logo);
      this.$store
        .dispatch("siteinformation/uploadimage", data)
        .then(response => {
          this.previewlogo = response;
          this.siteinformation.logo = response;
        });
    },
    deleteImage() {
      var payload = {
        image: this.siteinformation.logo
      };
      this.$store
        .dispatch("siteinformation/deleteimage", payload)
        .then(response => {
          if (response.success) {
            this.siteinformation.image = null;
            this.previewlogo = null;
            this.logo = null;
          }
        });
    },
    AddSiteInformation() {
      var payload = {
        description: this.siteinformation.description,
        keywords: this.siteinformation.keywords,
        logo: this.siteinformation.logo,
        title: this.siteinformation.title
      };
      this.$store
        .dispatch("siteinformation/addSiteInformation", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.previewlogo = null;
          this.resetSiteInformation();
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
          this.deleteImage();
        });
    },
    UpdateSiteInformation() {
      var payload = {
        id: this.siteinformation.id,
        description: this.siteinformation.description,
        keywords: this.siteinformation.keywords,
        logo: this.previewlogo == null ? this.currentlogo : this.previewlogo,
        title: this.siteinformation.title
      };
      this.$store
        .dispatch("siteinformation/updateSiteInformation", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.currentlogo = this.previewlogo;
          this.previewlogo = null;
          this.resetSiteInformation();
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
        });
    },
    deleteSiteInformation(id) {
      this.$store.dispatch("siteinformation/deleteSiteInformation", id);
    },
    getSiteInformation(id) {
      if (id > 0) {
        this.showmodal = true;
        this.$store
          .dispatch("siteinformation/getSiteInformation", id)
          .then(response => {
            this.siteinformation = response;
            this.currentlogo = response.logo;
          });
      }
    },
    resetSiteInformation() {
      if (this.previewlogo != null) {
        this.deleteImage();
      }
      (this.siteinformation.id = null),
        (this.logo = null),
        (this.currentlogo = null),
        (this.previewlogo = null),
        (this.siteinformation.description = ""),
        (this.siteinformation.title = ""),
        (this.siteinformation.keywords = ""),
        (this.siteinformation.logo = ""),
        (this.showmodal = false);
      (this.show = false), (this.message = null), (this.errors = []);
    }
  },
  beforeRouteLeave(to, from, next) {
    if (this.previewlogo != null) {
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
