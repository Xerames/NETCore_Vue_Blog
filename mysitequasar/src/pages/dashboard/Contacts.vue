<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Contacts"
        :data="contacts"
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
                @click="getContact(props.row.id)"
                icon="edit"
              />
              <q-btn
                dense
                color="red"
                @click="deleteContact(props.row.id)"
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
            <span v-if="this.contact.id > 0">Update Contact</span>
            <span v-else>Add New Contact</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetContact"
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
                    v-model="contact.address"
                    :rules="[
                      val =>
                        this.$v.contact.address.required ||
                        'Address is required'
                    ]"
                    label="Address"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="contact.phone"
                    :rules="[
                      val =>
                        this.$v.contact.phone.required || 'Phone is required',
                      val =>
                        this.$v.contact.phone.numeric ||
                        'Phone should be numeric',
                      val =>
                        this.$v.contact.phone.minLength ||
                        'Min lenght should be 11',
                      val =>
                        this.$v.contact.phone.maxLength ||
                        'Max lenght should be 11'
                    ]"
                    label="Phone"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="contact.email"
                    :rules="[
                      val =>
                        this.$v.contact.email.required || 'Email is required',
                      val =>
                        this.$v.contact.email.email || 'Invalid email address'
                    ]"
                    label="Email"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="contact.whatsapp"
                    :rules="[
                      val =>
                        this.$v.contact.whatsapp.required ||
                        'Whatsapp is required',
                      val =>
                        this.$v.contact.whatsapp.numeric ||
                        'Whatsapp should be numeric',
                      val =>
                        this.$v.contact.whatsapp.minLength ||
                        'Min lenght should be 11',
                      val =>
                        this.$v.contact.whatsapp.maxLength ||
                        'Max lenght should be 11'
                    ]"
                    label="Whatsapp"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="contact.facebook"
                    :rules="[
                      val =>
                        this.$v.contact.facebook.required ||
                        'Facebook is required'
                    ]"
                    label="Facebook"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="contact.twitter"
                    :rules="[
                      val =>
                        this.$v.contact.twitter.required ||
                        'Twitter is required'
                    ]"
                    label="Twitter"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="contact.instagram"
                    :rules="[
                      val =>
                        this.$v.contact.instagram.required ||
                        'Instagram is required'
                    ]"
                    label="Instagram"
                  />
                </q-item-section>
              </q-item>
            </q-list>
          </q-form>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn
            label="Update"
            @click="UpdateContact"
            :disabled="$v.$invalid"
            color="primary"
            v-if="this.contact.id > 0"
          />
          <q-btn
            label="Add"
            @click="AddContact"
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
import {
  required,
  email,
  minLength,
  maxLength,
  numeric
} from "vuelidate/lib/validators";
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
      contact: {
        id: null,
        address: "",
        email: "",
        phone: "",
        whatsapp: "",
        facebook: "",
        twitter: "",
        instagram: ""
      },
      filter: "",
      mode: "list",
      show: false,
      message: null,
      errors: [],
      columns: [
        {
          name: "Address",
          align: "left",
          label: "Address",
          field: "address",
          sortable: true
        },
        {
          name: "Email",
          align: "left",
          label: "Email",
          field: "email",
          sortable: true
        },
        {
          name: "Phone",
          required: true,
          label: "Phone",
          align: "left",
          field: "phone",
          sortable: true
        },
        {
          name: "Whatsapp",
          align: "left",
          label: "WhatsApp",
          field: "whatsapp",
          sortable: true
        },
        {
          name: "Facebook",
          align: "left",
          label: "Facebook",
          field: "facebook",
          sortable: true
        },
        {
          name: "Twitter",
          align: "left",
          label: "Twitter",
          field: "twitter",
          sortable: true
        },
        {
          name: "Instagram",
          align: "left",
          label: "Instagram",
          field: "instagram",
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
    contact: {
      address: {
        required
      },
      email: {
        required,
        email
      },
      phone: {
        required,
        minLength: minLength(11),
        maxLength: maxLength(11),
        numeric
      },
      whatsapp: {
        required,
        minLength: minLength(11),
        maxLength: maxLength(11),
        numeric
      },
      facebook: {
        required
      },
      twitter: {
        required
      },
      instagram: {
        required
      }
    }
  },
  created() {
    this.$store.dispatch("contact/getContacts");
  },
  computed: {
    contacts() {
      return this.$store.getters["contact/getContacts"];
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
          this.contacts.map(row =>
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
      const status = exportFile("contacts.csv", content, "text/csv");
      if (status !== true) {
        this.$q.notify({
          message: "Browser denied file download...",
          color: "negative",
          icon: "warning"
        });
      }
    },
    AddContact() {
      var payload = {
        address: this.contact.address,
        email: this.contact.email,
        phone: this.contact.phone,
        whatsapp: this.contact.whatsapp,
        facebook: this.contact.facebook,
        twitter: this.contact.twitter,
        instagram: this.contact.instagram
      };
      this.$store
        .dispatch("contact/addContact", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.resetContact();
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
        });
    },
    UpdateContact() {
      var payload = {
        id: this.contact.id,
        address: this.contact.address,
        email: this.contact.email,
        phone: this.contact.phone,
        whatsapp: this.contact.whatsapp,
        facebook: this.contact.facebook,
        twitter: this.contact.twitter,
        instagram: this.contact.instagram
      };
      this.$store
        .dispatch("contact/updateContact", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.resetContact();
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
        });
    },
    deleteContact(id) {
      this.$store.dispatch("contact/deleteContact", id);
    },
    getContact(id) {
      if (id > 0) {
        this.showmodal = true;
        this.$store.dispatch("contact/getContact", id).then(response => {
          this.contact = response;
        });
      }
    },
    resetContact() {
      (this.contact.id = null),
        (this.contact.address = ""),
        (this.contact.email = ""),
        (this.contact.phone = ""),
        (this.contact.whatsapp = ""),
        (this.contact.facebook = ""),
        (this.contact.twitter = ""),
        (this.contact.instagram = "");
      this.showmodal = false;
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
