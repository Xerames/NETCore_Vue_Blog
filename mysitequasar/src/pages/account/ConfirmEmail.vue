<template>
  <div class="container q-pa-md">
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
  </div>
</template>

<script>
export default {
  data() {
    return {
      emailconfirminfo: {
        userId: null,
        token: null
      },
      message: null,
      errors: [],
      show: false
    };
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
    this.emailconfirminfo.userId = this.$route.params.userId;
    this.emailconfirminfo.token = this.$route.params.token;
    this.$store
      .dispatch("user/confirmemail", this.emailconfirminfo)
      .then(response => {
        this.show = true;
        this.message = response.message;
      })
      .catch(error => {
        this.show = true;
        this.errors = error;
      });
  }
};
</script>

<style lang="scss" scoped>
.borderteal {
  border-color: #009688;
}
</style>
