<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Users"
        :data="users"
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
                @click="getUser(props.row.user.id)"
                label="Edit"
              />
              <q-btn
                dense
                color="purple"
                @click="getAssignedRoles(props.row.user.id)"
                label="Assign Role"
              />
              <q-btn
                dense
                color="green"
                @click="getUserForChangePassword(props.row.user.id)"
                label="Change Password"
              />
              <q-btn
                dense
                color="red"
                @click="deleteUser(props.row.user.id)"
                label="Remove"
              />
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-Roles="props">
          <q-td :props="props">
            <div class="q-gutter-sm">
              <q-badge
                v-for="role in props.row.roles"
                :key="role.id"
                color="teal"
                size="sm"
              >
                {{ role }}
              </q-badge>
            </div>
          </q-td>
        </template>
      </q-table>
    </q-card>
    <q-dialog v-model="edituser" persistent>
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
            <span>Update User</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetUser"
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
                    v-model="user.userName"
                    :rules="[
                      val =>
                        this.$v.user.userName.required || 'Username is required'
                    ]"
                    label="Username"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="user.firstName"
                    :rules="[
                      val =>
                        this.$v.user.firstName.required ||
                        'First Name is required'
                    ]"
                    label="First Name"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="user.lastName"
                    :rules="[
                      val =>
                        this.$v.user.lastName.required ||
                        'Last Name is required'
                    ]"
                    label="Last Name"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    v-model="user.email"
                    label="Email"
                    :rules="[
                      val => $v.user.email.required || 'Email is required',
                      val => $v.user.email.email || 'Invalid email address'
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
            @click="UpdateUserByAdmin"
            :disabled="$v.user.$invalid"
            color="primary"
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <q-dialog v-model="showassignedroles" persistent>
      <q-card style="width: 600px; max-width: 60vw;">
        <q-card-section>
          <div class="text-h6">
            <span>Assigned Roles</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetUser"
            ></q-btn>
            <div
              class="q-pa-md"
              v-for="userrole in userroles"
              :key="userrole.roleId"
            >
              <q-checkbox
                type="checkbox"
                color="deep-purple-7"
                v-model="userrole.exist"
              />
              {{ userrole.roleName }}
            </div>
          </div>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn label="Assign Role" @click="AssignRole" color="primary" />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <q-dialog v-model="changepassword" persistent>
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
            <span>Change Password</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetUser"
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
                    type="password"
                    v-model="changepassworduser.newpassword"
                    :rules="[
                      val =>
                        $v.changepassworduser.newpassword.required ||
                        'New Password is required',
                      val =>
                        $v.changepassworduser.newpassword.minLength ||
                        'New Password must be atleast 9',
                      val =>
                        $v.changepassworduser.newpassword.oneUpperCase ||
                        'New Password must be one upper case and lower case',
                      val =>
                        $v.changepassworduser.newpassword.oneLowerCase ||
                        'New Password must be one lower case and upper case',
                      val =>
                        $v.changepassworduser.newpassword.oneNumber ||
                        'New Password must be one number'
                    ]"
                    label="New Password"
                  />
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section>
                  <q-input
                    dense
                    outlined
                    type="password"
                    v-model="changepassworduser.confirmpassword"
                    :rules="[
                      val =>
                        $v.changepassworduser.confirmpassword.required ||
                        'Confirm Password is required',
                      val =>
                        $v.changepassworduser.confirmpassword.sameAs ||
                        'Passwords must be match'
                    ]"
                    label="Confirm Password"
                  />
                </q-item-section>
              </q-item>
            </q-list>
          </q-form>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn
            label="Change Password"
            @click="changePasswordByAdmin"
            :disabled="$v.changepassworduser.$invalid"
            color="primary"
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script>
import { exportFile } from "quasar";
import { required, email, sameAs, minLength } from "vuelidate/lib/validators";
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
      user: {
        id: "",
        userName: "",
        firstName: "",
        lastName: "",
        email: ""
      },
      changepassworduser: {
        id: "",
        newpassword: "",
        confirmpassword: ""
      },
      userroles: [],
      show: false,
      message: null,
      errors: [],
      edituser: false,
      showassignedroles: false,
      changepassword: false,
      filter: "",
      mode: "list",
      columns: [
        {
          name: "userName",
          align: "left",
          label: "Username",
          field: row => row.user.userName,
          sortable: true
        },
        {
          name: "email",
          align: "left",
          label: "Email",
          field: row => row.user.email,
          sortable: true
        },
        {
          name: "firstName",
          required: true,
          label: "FirstName",
          align: "left",
          field: row => row.user.firstName,
          sortable: true
        },
        {
          name: "lastName",
          align: "left",
          label: "LastName",
          field: row => row.user.lastName,
          sortable: true
        },
        {
          name: "Roles",
          align: "left",
          label: "Roles",
          field: row => row.roles,
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
    user: {
      userName: {
        required
      },
      email: {
        required,
        email
      },
      firstName: {
        required
      },
      lastName: {
        required
      }
    },
    changepassworduser: {
      newpassword: {
        required,
        minLength: minLength(9),
        oneUpperCase: password => {
          return /[A-Z]/.test(password);
        },
        oneLowerCase: password => {
          return /[a-z]/.test(password);
        },
        oneNumber: password => {
          return /[0-9]/.test(password);
        }
      },
      confirmpassword: {
        required,
        sameAs: sameAs("newpassword")
      }
    }
  },
  created() {
    this.$store.dispatch("user/getUserswithRoles");
  },
  computed: {
    users() {
      return this.$store.getters["user/getUsersdata"];
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
          this.users.map(row =>
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
      const status = exportFile("users.csv", content, "text/csv");
      if (status !== true) {
        this.$q.notify({
          message: "Browser denied file download...",
          color: "negative",
          icon: "warning"
        });
      }
    },
     UpdateUserByAdmin() {
      var payload = {
        id: this.user.id,
        userName: this.user.userName,
        email: this.user.email,
        firstName: this.user.firstName,
        lastName: this.user.lastName
      };
      this.$store
        .dispatch("user/updateUserByAdmin", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.$store.dispatch("user/getUserswithRoles");
          this.resetUser();
        })
        .catch(error => {
          this.show = true;
          if (error!=null){
            this.errors = error;
          }
        });
    },
    deleteUser(userid) {
      this.$store.dispatch("user/deleteUser", userid);
    },
    getUser(id) {
      if (id != null) {
        this.edituser = true;
        this.$store.dispatch("user/getUser", id).then(response => {
          this.user = response;
        });
      }
    },
    getAssignedRoles(id) {
      if (id != null) {
        this.showassignedroles = true;
        this.$store.dispatch("role/getAssignedRoles", id).then(response => {
          this.userroles = response;
        });
      }
    },
    getUserForChangePassword(id) {
      if (id != null) {
        this.changepassword = true;
        this.changepassworduser.id = id;
      }
    },
    changePasswordByAdmin() {
      var payload = {
        id: this.changepassworduser.id,
        newpassword: this.changepassworduser.newpassword,
        confirmpassword: this.changepassworduser.confirmpassword
      };
      this.$store
        .dispatch("user/passwordChangeByAdmin", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.resetUser();
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
        });
    },
    AssignRole() {
      this.$store.dispatch("role/roleAssign", this.userroles).then(response => {
        if (response.success) {
          this.resetUser();
          this.$store.dispatch("user/getUserswithRoles");
        }
      });
    },
    resetUser() {
      this.user.id = "";
      this.user.firstName = "";
      this.user.email = "";
      this.user.lastName = "";
      this.user.userName = "";
      this.edituser = false;
      this.showassignedroles = false;
      this.changepassword = false;
      this.show = false;
      this.message = null;
      this.errors = [];
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
