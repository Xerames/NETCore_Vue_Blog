<template>
  <div class="q-mt-xl">
    <q-list bordered class="text-center">
      <q-item class="row justify-center">
        <div class="column">
          <q-avatar size="72px" class="q-mb-md q-mx-auto">
            <img :src="apiurl + getUser.photo" v-if="getUser.photo != null" />
            <img :src="defaultphoto" v-else />
          </q-avatar>
          <div class="q-mb-xs">
            {{ getUser.firstName }} {{ getUser.lastName }}
          </div>
        </div>
      </q-item>
      <q-item to="/profile" clickable v-ripple>
        <q-item-section>Profile</q-item-section>
      </q-item>
      <q-item to="/changepassword" clickable v-ripple>
        <q-item-section>
          <q-item-label>Change Password</q-item-label>
        </q-item-section>
      </q-item>
      <q-item to="/changephoto" clickable v-ripple>
        <q-item-section>
          <q-item-label>Change Photo</q-item-label>
        </q-item-section>
      </q-item>
    </q-list>
  </div>
</template>

<script>
export default {
  computed: {
    getUser() {
      return this.$store.getters["user/getUser"];
    },
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    },
    defaultphoto() {
      return this.$store.getters["siteinformation/getDefaultPhoto"];
    }
  }
};
</script>

<style lang="scss" scoped>
.q-router-link--exact-active {
  color: white;
  background-color: #673ab7;
}
</style>
