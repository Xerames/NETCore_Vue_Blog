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
                v-model="Email"
                label-color="teal"
                color="teal"
                label="Email"
                :rules="[
                  val => $v.Email.required || 'Email is required',
                  val => $v.Email.email || 'Invalid email address'
                ]"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center">
            <div class="col text-center q-mt-md">
              <q-btn
                label="RESET PASSWORD"
                @click="ForgetPassword"
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
import { required, email } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      Email: "",
      show: false,
      message: null,
      errors: []
    };
  },
  validations: {
    Email: {
      required,
      email
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
    ForgetPassword() {
      var payload = {
        Email: this.email
      };
      this.$store
        .dispatch("user/forgetPassword", payload 
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
