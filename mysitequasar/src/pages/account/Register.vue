<template>
  <div class="container">
    <div class="row justify-center q-ma-xl">
      <q-card class="q-pa-xl borderorange" bordered style="width:500px">
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
        <q-form>
          <div class="row justify-center q-gutter-md">
            <div class="col-12">
              <q-input
                label-color="orange"
                color="orange"
                v-model="registercredentials.Username"
                :rules="[
                  val =>
                    $v.registercredentials.Username.required ||
                    'Username is required'
                ]"
                label="Username"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center q-gutter-md">
            <div class="col-12">
              <q-input
                label-color="orange"
                color="orange"
                type="email"
                ref="email"
                label="Email"
                v-model="registercredentials.Email"
                :rules="[
                  val =>
                    $v.registercredentials.Email.required ||
                    'Email is required',
                  val =>
                    $v.registercredentials.Email.email ||
                    'Invalid email address'
                ]"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center q-gutter-md">
            <div class="col-12">
              <q-input
                label-color="orange"
                color="orange"
                v-model="registercredentials.FirstName"
                :rules="[
                  val =>
                    $v.registercredentials.FirstName.required ||
                    'First Name is required'
                ]"
                label="First Name"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center q-gutter-md">
            <div class="col-12">
              <q-input
                label-color="orange"
                color="orange"
                v-model="registercredentials.LastName"
                :rules="[
                  val =>
                    $v.registercredentials.LastName.required ||
                    'Last Name is required'
                ]"
                label="Last Name"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center q-gutter-md">
            <div class="col-12">
              <q-input
                label-color="orange"
                color="orange"
                v-model="registercredentials.Password"
                label="Password"
                :rules="[
                  val =>
                    $v.registercredentials.Password.required ||
                    'Password is required',
                  val =>
                    $v.registercredentials.Password.minLength ||
                    'Password must be atleast 9',
                  val =>
                    $v.registercredentials.Password.oneUpperCase ||
                    'Password must be one upper case and lower case',
                  val =>
                    $v.registercredentials.Password.oneLowerCase ||
                    'Password must be one lower case and upper case',
                  val =>
                    $v.registercredentials.Password.oneNumber ||
                    'Password must be one number'
                ]"
                :type="isPwd ? 'password' : 'text'"
              >
                <template v-slot:append>
                  <q-icon
                    :name="isPwd ? 'visibility_off' : 'visibility'"
                    class="cursor-pointer"
                    @click="isPwd = !isPwd"
                  />
                </template>
              </q-input>
            </div>
          </div>
          <div class="row justify-center q-gutter-md">
            <div class="col-12">
              <q-input
                label-color="orange"
                color="orange"
                v-model="registercredentials.ConfirmPassword"
                :rules="[
                  val =>
                    $v.registercredentials.ConfirmPassword.required ||
                    'Confirm Password is required',
                  val =>
                    $v.registercredentials.ConfirmPassword.sameAs ||
                    'Passwords must be match'
                ]"
                label="Confirm Password"
                :type="isPwd ? 'password' : 'text'"
              >
                <template v-slot:append>
                  <q-icon
                    :name="isPwd ? 'visibility_off' : 'visibility'"
                    class="cursor-pointer"
                    @click="isPwd = !isPwd"
                  />
                </template>
              </q-input>
            </div>
          </div>
          <div class="row justify-center">
            <div class="col text-center q-mt-md">
              <q-btn
                label="REGISTER"
                @click.prevent="register"
                :disabled="$v.$invalid"
                outline
                color="orange"
              ></q-btn>
              <q-btn
                class="q-ml-md"
                label="I HAVE ACCOUNT"
                to="/login"
                outline
                color="deep-purple-6"
              ></q-btn>
            </div>
          </div>
        </q-form>
      </q-card>
    </div>
  </div>
</template>

<script>
import { required, email, sameAs, minLength } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      registercredentials: {
        Username: "",
        Email: "",
        FirstName: "",
        LastName: "",
        Password: "",
        ConfirmPassword: ""
      },
      isPwd: true,
      show: false,
      message: null,
      errors: []
    };
  },
  validations: {
    registercredentials: {
      Username: {
        required
      },
      Email: {
        required,
        email
      },
      FirstName: {
        required
      },
      LastName: {
        required
      },
      Password: {
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
        sameAs: sameAs("Password")
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
  methods: {
    register() {
      this.$store
        .dispatch("user/register", this.registercredentials)
        .then(response => {
          this.message = response.message;
          this.show = true;
        }).catch(error=>{
          this.show = true;
          this.errors=error;
        });
    }
  }
};
</script>

<style lang="scss" scoped>
.borderorange {
  border-color: orange;
}
</style>
