<template>
  <div class="container">
    <div class="row justify-center q-ma-xl">
      <q-card class="q-pa-xl borderdeeppurple" bordered style="width:500px">
        <q-banner v-if="errors.length > 0" class="bg-red text-white q-mb-md">
          <div class="row items-center">
            <div class="col-2 ">
              <i class="fas fa-exclamation-circle fa-2x"></i>
            </div>
            <div class="col-10">
              <div
                class="q-ma-xs"
                v-for="(error, index) in errors"
                :key="index"
              >
                {{ error }}
              </div>
            </div>
          </div>
        </q-banner>
        <q-form>
          <div class="row justify-center q-gutter-md">
            <div class="col-12">
              <q-input
                color="deep-purple-6"
                label-color="deep-purple-6"
                ref="Username"
                v-model="logincredentials.Username"
                label="Username"
                :rules="[
                  val =>
                    $v.logincredentials.Username.required ||
                    'Username is required'
                ]"
              ></q-input>
            </div>
          </div>
          <div class="row justify-center q-gutter-md">
            <div class="col-12">
              <q-input
                color="deep-purple-6"
                label-color="deep-purple-6"
                ref="Password"
                v-model="logincredentials.Password"
                label="Password"
                :type="isPwd ? 'password' : 'text'"
                :rules="[
                  val =>
                    $v.logincredentials.Password.required ||
                    'Password is required'
                ]"
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
                label="LOGIN"
                ref="loginbtn"
                @click.prevent="login"
                :disabled="$v.$invalid"
                outline
                color="deep-purple-6"
              ></q-btn>
              <q-btn
                class="q-ml-md"
                label="FORGET PASSWORD"
                to="/forgetpassword"
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
import { required } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      logincredentials: {
        Username: "",
        Password: ""
      },
      isPwd: true,
      errors: []
    };
  },
  validations: {
    logincredentials: {
      Username: {
        required
      },
      Password: {
        required
      }
    }
  },
  methods: {
    login() {
      this.$store
        .dispatch("user/login", this.logincredentials)
        .then(() => {
          this.$router.push("/");
        })
        .catch(error => {
          this.errors=error;
        });
    }
  }
};
</script>

<style lang="scss" scoped>
.borderdeeppurple {
  border-color: #673ab7;
}
</style>
