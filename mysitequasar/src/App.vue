<template>
  <div id="q-app">
    <router-view />
  </div>
</template>
<script>
export default {
  created() {
    this.$store.dispatch("user/getToken");
    if (this.loggedIn) {
      this.$store.dispatch("user/getUserWithRoles");
    }
  },
  computed: {
    loggedIn() {
      return this.$store.getters["user/getloggedIn"];
    }
  }
};
</script>
