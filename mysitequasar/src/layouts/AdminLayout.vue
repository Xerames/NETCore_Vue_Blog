<template>
  <q-layout view="lHh LpR lFf">
    <q-header
      reveal
      :class="$q.dark.isActive ? 'header_dark' : 'header_normal'"
    >
      <q-toolbar>
        <q-btn
          @click="left = !left"
          flat
          round
          dense
          icon="menu"
          class="q-mr-sm"
        />
        <q-space></q-space>
        <q-btn
          class="q-mr-xs"
          flat
          round
          @click="$q.dark.toggle()"
          :icon="$q.dark.isActive ? 'nights_stay' : 'wb_sunny'"
        />
        <q-btn
          flat
          round
          dense
          icon="fas fa-sign-out-alt"
          to="/"
        />
      </q-toolbar>
    </q-header>
    <q-drawer
      class="left-navigation text-dark"
      show-if-above
      v-model="left"
      side="left"
      elevated
    >
      <div
        class="full-height"
        :class="$q.dark.isActive ? 'drawer_dark' : 'drawer_normal'"
      >
        <div style="height: calc(100% - 117px);padding:10px;">
          <q-toolbar>
            <q-toolbar-title class="text-center">Dashboard</q-toolbar-title>
          </q-toolbar>
          <hr />
          <q-scroll-area style="height:100%;">
            <q-list padding>
              <q-item
                active-class="tab-active"
                to="/dashboard/contacts"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Contacts
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/abouts"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Abouts
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/blogs"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Blogs
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/categories"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Categories
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/tags"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Tags
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/sliders"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Sliders
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/ministrations"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Ministrations
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/users"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Users
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/roles"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Roles
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/logs"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Logs
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/siteinformation"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Site Information
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/comments"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Comments
                </q-item-section>
              </q-item>
              <q-item
                active-class="tab-active"
                to="/dashboard/replies"
                class="q-ma-sm navigation-item"
                clickable
                v-ripple
              >
                <q-item-section>
                  Replies
                </q-item-section>
              </q-item>
            </q-list>
          </q-scroll-area>
        </div>
      </div>
    </q-drawer>

    <q-page-container>
      <q-page class="row no-wrap">
        <div class="col">
          <div class="full-height">
            <q-scroll-area class="col q-pr-sm full-height" visible>
              <router-view />
            </q-scroll-area>
          </div>
        </div>
      </q-page>
    </q-page-container>
  </q-layout>
</template>

<script>
export default {
  data() {
    return {
      left: false
    };
  },
  methods: {
    logoutNotify() {
      this.$q.notify({
        message: "Logged out"
      });
    }
  }
};
</script>

<style lang="scss" scoped>
.drawer_normal {
  background-color: #010101f2;
  color: white;
}

.drawer_dark {
  background-color: #010101f2;
}

.navigation-item {
  border-radius: 5px;
  text-align: center;
}

.tab-active {
  background-color: green;
}

body {
  background: #f1f1f1 !important;
}

.header_normal {
  background: linear-gradient(
    145deg,
    rgb(32, 106, 80) 15%,
    rgb(21, 57, 102) 70%
  );
}

.header_dark {
  background: linear-gradient(145deg, rgb(61, 14, 42) 15%, rgb(14, 43, 78) 70%);
}
</style>
