<template>
  <q-layout view="hhh lpR fff">
    <q-header elevated class="q-pa-xs bg-deep-purple-6">
      <q-toolbar class="container">
        <q-toolbar-title
          v-for="siteinformation in siteinformations"
          :key="siteinformation.id"
        >
          <q-img
            @click.prevent="$router.push('/')"
            class="logo"
            width="72px"
            height="72px"
            :src="apiurl + siteinformation.logo"
          >
          </q-img>
        </q-toolbar-title>
        <div class="gt-sm">
          <q-btn flat label="HOME">
            <router-link
              exact
              :to="{ name: 'Home' }"
              class="absolute full-width full-height"
            ></router-link>
          </q-btn>
          <q-btn flat label="ABOUT">
            <router-link
              exact
              :to="{ name: 'About' }"
              class="absolute full-width full-height"
            ></router-link>
          </q-btn>
          <q-btn flat label="BLOG">
            <router-link
              exact
              :to="{ name: 'Blog' }"
              class="absolute full-width full-height"
            ></router-link>
          </q-btn>
          <q-btn v-if="!isloggedin" flat label="LOGIN">
            <router-link
              exact
              :to="{ name: 'Login' }"
              class="absolute full-width full-height"
            ></router-link>
          </q-btn>
          <q-btn v-if="!isloggedin" flat label="REGISTER">
            <router-link
              exact
              :to="{ name: 'Register' }"
              class="absolute full-width full-height"
            ></router-link>
          </q-btn>
          <q-btn-dropdown
            v-if="isloggedin && userdata != null"
            class="glossy q-ml-md"
            color="purple"
            :label="FirstNameandLastName"
          >
            <div class="q-pa-md">
              <div class="column items-center">
                <q-avatar size="72px">
                  <img
                    :src="apiurl + userdata.user.photo"
                    v-if="userdata.user.photo != null"
                    class="q-mx-auto"
                  />
                  <img class="q-mx-auto" :src="defaultphoto" v-else />
                </q-avatar>
                <q-list>
                  <q-item
                    to="/dashboard/contacts"
                    clickable
                    v-if="
                      isloggedin &&
                        userdata != null &&
                        userdata.roles.includes('Admin')
                    "
                  >
                    <q-item-section>
                      <q-item-label>Dashboard</q-item-label>
                    </q-item-section>
                  </q-item>
                  <q-item to="/profile" clickable v-close-popup>
                    <q-item-section>
                      <q-item-label>Profile</q-item-label>
                    </q-item-section>
                  </q-item>
                </q-list>
                <q-btn
                  color="primary"
                  label="Logout"
                  push
                  @click.prevent="logout"
                  size="sm"
                  class="q-mt-md"
                  v-close-popup
                />
              </div>
            </div>
          </q-btn-dropdown>
        </div>

        <div class="lt-md">
          <q-btn
            flat
            dense
            round
            icon="menu"
            aria-label="Menu"
            @click="leftDrawerOpen = !leftDrawerOpen"
          />
        </div>
      </q-toolbar>
    </q-header>
    <q-drawer
      v-model="leftDrawerOpen"
      bordered
      :width="250"
      content-class="bg-deep-purple-6"
    >
      <q-list class="text-white">
        <q-item-label header class="text-white">
          Menu
        </q-item-label>
        <q-item to="/" exact clickable  v-close-popup>
          <q-item-section>
            <q-item-label>Home</q-item-label>
          </q-item-section>
        </q-item>
        <q-item to="/about" exact clickable  v-close-popup>
          <q-item-section>
            <q-item-label>About</q-item-label>
          </q-item-section>
        </q-item>
        <q-item to="/blog" exact clickable  v-close-popup>
          <q-item-section>
            <q-item-label>Blog</q-item-label>
          </q-item-section>
        </q-item>
        <q-item v-if="!isloggedin" to="/login" exact clickable  v-close-popup>
          <q-item-section>
            <q-item-label>Login</q-item-label>
          </q-item-section>
        </q-item>
        <q-item v-if="!isloggedin" to="/register" exact clickable  v-close-popup>
          <q-item-section>
            <q-item-label>Register</q-item-label>
          </q-item-section>
        </q-item>
        <q-item v-if="isloggedin" clickable @click.prevent="logout"  v-close-popup>
          <q-item-section>
            <q-item-label>Logout</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-drawer>
    <q-page-container>
      <router-view />
    </q-page-container>

    <q-footer elevated class="bg-deep-purple-6 ">
      <div class="container">
        <div class="row justify-center text-center q-pa-md text-weight-bolder">
          <div
            class="col-md-6 col-sm-12 col-xs-12"
            v-for="contact in contacts"
            :key="contact.id"
          >
            <p class="text-yellow">Contact Us</p>
            <address class="q-mb-md">
              {{ contact.address }}
            </address>
            <p><i class="fas fa-envelope"></i> {{ contact.email }}</p>
            <p><i class="fas fa-phone-alt"></i> {{ contact.phone }}</p>
            <p><i class="fab fa-whatsapp"></i> {{ contact.whatsapp }}</p>
          </div>
          <div
            class="col-md-6 col-sm-12 col-xs-12"
            v-for="contact in contacts"
            :key="contact.id + 1"
          >
            <p class="text-yellow">Follow Us</p>
            <a :href="'//facebook.com/' + contact.twitter"
              ><i class="fab fa-facebook fa-2x q-mr-md" style="color:white"></i
            ></a>
            <a :href="'//instagram.com/' + contact.instagram"
              ><i class="fab fa-instagram fa-2x q-mr-md" style="color:white"></i
            ></a>
            <a :href="'//twitter.com/' + contact.instagram"
              ><i class="fab fa-twitter fa-2x" style="color:white"></i
            ></a>
            <div class="row justify-center text-center q-ma-md">
              <div class="col-6">
                <p>©{{ new Date().getFullYear() }} Copyright Yalçın Can</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </q-footer>
  </q-layout>
</template>

<script>
export default {
  data() {
    return {
      leftDrawerOpen: false
    };
  },
  created() {
    this.$store.dispatch("contact/getContacts");
    this.$store.dispatch("siteinformation/getSiteInformations");
  },
  computed: {
    contacts() {
      return this.$store.getters["contact/getContacts"];
    },
    siteinformations() {
      return this.$store.getters["siteinformation/getSiteInformations"];
    },
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    },
    userdata() {
      return this.$store.getters["user/getUserdata"];
    },
    isloggedin() {
      return this.$store.getters["user/getloggedIn"];
    },
    defaultphoto() {
      return this.$store.getters["siteinformation/getDefaultPhoto"];
    },
    FirstNameandLastName() {
      return this.userdata.user.firstName + " " + this.userdata.user.lastName;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("user/logout");
    }
  }
};
</script>

<style lang="scss" scoped>
.q-toolbar {
  padding: 0;
}
.q-router-link--exact-active {
  color: #673ab7 !important;
}

.q-drawer .q-router-link--exact-active {
  color: yellow !important;
}

.router-link-active {
  border-bottom: 2px solid white;
}

.list-unstyled {
  list-style-type: none;
}

.logo:hover{
  cursor: pointer;
}
</style>
