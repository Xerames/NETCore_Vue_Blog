<template>
  <div class="container">
    <div class="row justify-center q-ma-xl">
      <q-card class="q-pa-xl borderteal" bordered style="width:500px">
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
        <q-form>
          <div class="row justify-center q-gutter-md">
            <div class="col-12">
              <q-input
                v-model="newpassword"
                label-color="teal"
                color="teal"
                label="New Password"
                :rules="[
                  val => $v.newpassword.required || 'New Password is required',
                  val =>
                    $v.newpassword.minLength ||
                    'New Password must be atleast 9',
                  val =>
                    $v.newpassword.oneUpperCase ||
                    'New Password must be one upper case and lower case',
                  val =>
                    $v.newpassword.oneLowerCase ||
                    'New Password must be one lower case and upper case',
                  val =>
                    $v.newpassword.oneNumber ||
                    'New Password must be one number'
                ]"
                type="password"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center q-gutter-md">
            <div class="col-12">
              <q-input
                v-model="confirmpassword"
                label-color="teal"
                color="teal"
                label="Confirm Password"
                :rules="[
                  val =>
                    $v.confirmpassword.required ||
                    'Confirm Password is required',
                  val => $v.confirmpassword.sameAs || 'Passwords must be match'
                ]"
                type="password"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center">
            <div class="col text-center q-mt-md">
              <q-btn
                label="RESET PASSWORD"
                @click="ResetPassword"
                :disabled="$v.$invalid"
                outline
                color="teal"
              ></q-btn>
            </div>
          </div>
        </q-form>
      </q-card>
    </div>
  </div>
</template>

<script>
import { required, sameAs, minLength } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      newpassword: "",
      confirmpassword: "",
      show: false,
      message: null,
      errors: []
    };
  },
  validations: {
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
    ResetPassword() {
      var payload = {
        Email: this.$route.params.email,
        Token: this.$route.params.token,
        NewPassword: this.newpassword,
        ConfirmPassword: this.confirmpassword
      };
      this.$store
        .dispatch("user/resetPassword", payload) 
        .then(response => {
          this.show = true;
          this.message = response.message;
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
        });
    }
  }
};
</script>

<style lang="scss" scoped>
.borderteal {
  border-color: #009688;
}
</style>
