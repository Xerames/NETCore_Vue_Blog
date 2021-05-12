<template>
  <div class="container">
    <div class="row justify-center q-pa-lg q-col-gutter-md">
      <div class="col-lg-4 col-sm-12 col-xs-12">
        <Sidebar></Sidebar>
      </div>
      <div class="col-lg-8 col-sm-12 col-xs-12">
        <q-card>
          <div class="row justify-center q-mt-xl">
            <h5>Profile Settings</h5>
          </div>
          <div class="row justify-center">
            <q-banner
              class="text-white q-mb-md col-6"
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
          </div>
          <div class="row justify-center q-mx-auto">
            <div class="col-6">
              <q-input
                color="black"
                v-model="user.firstName"
                :rules="[
                  val => $v.user.firstName.required || 'First Name is required'
                ]"
                label="First Name"
                class="q-mb-xs"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center q-mx-auto">
            <div class="col-6">
              <q-input
                color="black"
                v-model="user.lastName"
                :rules="[
                  val => $v.user.lastName.required || 'Last Name is required'
                ]"
                label="Last Name"
                class="q-mb-xs"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center q-mx-auto">
            <div class="col-6">
              <q-input
                color="black"
                v-model="user.email"
                :rules="[
                  val => $v.user.email.required || 'Email is required',
                  val => $v.user.email.email || 'Invalid email address'
                ]"
                label="Email"
                class="q-mb-xs"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center">
            <div class="col text-center q-mt-md q-mb-md">
              <q-btn
                label="UPDATE"
                @click.prevent="Update"
                :disabled="$v.$invalid"
                outline
              ></q-btn>
            </div>
          </div>
        </q-card>
      </div>
    </div>
  </div>
</template>

<script>
import Sidebar from "components/account/Sidebar.vue";
import { required, email } from "vuelidate/lib/validators";
export default {
  components: {
    Sidebar
  },
  data() {
    return {
      message: null,
      show: false,
      errors: [],
      user: {}
    };
  },
  validations: {
    user: {
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
    }
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
  created() {
    this.$store.dispatch("user/GetProfileData").then(response => {
      this.user = response;
    });
  },
  methods: {
    Update() {
      var payload = {
        id: this.user.id,
        email: this.user.email,
        firstName: this.user.firstName,
        lastName: this.user.lastName
      };
      this.$store
        .dispatch("user/updateUser", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          setTimeout(() => (this.show = false), 3000);
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
          this.$store.dispatch("user/getUserWithRoles");
          setTimeout(() => (this.show = false), 3000);
        });
    }
  }
};
</script>

<style lang="scss" scoped></style>
