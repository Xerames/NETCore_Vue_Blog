<template>
  <div class="container">
    <div class="row justify-center q-pa-lg q-col-gutter-md">
      <div class="col-lg-4 col-sm-12 col-xs-12">
        <Sidebar></Sidebar>
      </div>
      <div class="col-lg-8 col-sm-12 col-xs-12">
        <q-card>
          <div class="row justify-center q-mt-xl">
            <h5>Change Password</h5>
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
                v-model="CurrentPassword"
                label="Current Password"
                type="password"
                class="q-mb-xs"
                :rules="[
                  val =>
                    $v.CurrentPassword.required ||
                    'Current Password is required'
                ]"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center q-mx-auto">
            <div class="col-6">
              <q-input
                color="black"
                v-model="NewPassword"
                label="New Password"
                type="password"
                class="q-mb-xs"
                :rules="[
                  val => $v.NewPassword.required || 'New Password is required',
                  val =>
                    $v.NewPassword.minLength ||
                    'New Password must be atleast 9',
                  val =>
                    $v.NewPassword.oneUpperCase ||
                    'New Password must be one upper case and lower case',
                  val =>
                    $v.NewPassword.oneLowerCase ||
                    'New Password must be one lower case and upper case',
                  val =>
                    $v.NewPassword.oneNumber ||
                    'New Password must be one number'
                ]"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center q-mx-auto">
            <div class="col-6">
              <q-input
                color="black"
                v-model="ConfirmPassword"
                label="Confirm Password"
                type="password"
                class="q-mb-xs"
                :rules="[
                  val =>
                    $v.ConfirmPassword.required ||
                    'Confirm Password is required',
                  val => $v.ConfirmPassword.sameAs || 'Passwords must be match'
                ]"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center">
            <div class="col text-center q-mt-md q-mb-md">
              <q-btn
                label="CHANGE PASSWORD"
                @click.prevent="ChangePassword"
                outline
                :disabled="$v.$invalid"
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
import { required, sameAs, minLength } from "vuelidate/lib/validators";
export default {
  components: {
    Sidebar
  },
  data() {
    return {
      show: false,
      message: null,
      errors: [],
      CurrentPassword: "",
      NewPassword: "",
      ConfirmPassword: ""
    };
  },
  validations: {
    CurrentPassword: {
      required
    },
    NewPassword: {
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
    ConfirmPassword: {
      required,
      sameAs: sameAs("NewPassword")
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
  methods: {
    ChangePassword() {
      var payload = {
        CurrentPassword: this.CurrentPassword,
        NewPassword: this.NewPassword,
        ConfirmPassword: this.ConfirmPassword
      };
      this.$store
        .dispatch("user/changePassword", payload)
        .then(response => {
          this.show = true;
          this.message =
            response.message + " Logging out please login again...";
          setTimeout(
            () => this.$store.dispatch("user/logout"),
            this.$router.push("/"),
            4000
          );
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
        });
    }
  }
};
</script>

<style lang="scss" scoped></style>
