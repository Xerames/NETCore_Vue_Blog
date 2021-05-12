import Vue from "vue";
import VueRouter from "vue-router";

import routes from "./routes";

Vue.use(VueRouter);

/*
 * If not building with SSR mode, you can
 * directly export the Router instantiation;
 *
 * The function below can be async too; either use
 * async/await or return a Promise which resolves
 * with the Router instance.
 */

export default function(/* { store, ssrContext } */) {
  const Router = new VueRouter({
    scrollBehavior: () => ({ x: 0, y: 0 }),
    routes,

    // Leave these as they are and change in quasar.conf.js instead!
    // quasar.conf.js -> build -> vueRouterMode
    // quasar.conf.js -> build -> publicPath
    mode: "history"
  });
  Router.beforeEach((to, from, next) => {
    if (to.meta.requiresAuth) {
      var token = window.localStorage.getItem("token");
      if (token != null) {
        var decodedtoken = JSON.parse(atob(token.split(".")[1]));
        var role =
          decodedtoken[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ];
        if (role != "Admin") {
          next("/accessdenied");
        } else {
          next();
        }
      } else {
        next("/login");
      }
    } else {
      next();
    }

    if (to.meta.requiresLogin) {
      var token = window.localStorage.getItem("token");
      if (token != null) {
        next();
      } else {
        next("/login");
      }
    }
  });
  return Router;
}
