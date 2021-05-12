import Vue from "vue";
import Vuex from "vuex";

import ministration from "./ministration";
import siteinformation from "./siteinformation";
import slider from "./slider";
import about from "./about";
import contact from "./contact";
import blog from "./blog";
import category from "./category";
import comment from "./comment";
import reply from "./reply";
import tag from "./tag";
import user from "./user";
import role from "./role";
import log from "./log";
Vue.use(Vuex);

/*
 * If not building with SSR mode, you can
 * directly export the Store instantiation;
 *
 * The function below can be async too; either use
 * async/await or return a Promise which resolves
 * with the Store instance.
 */

export default function(/* { ssrContext } */) {
  const Store = new Vuex.Store({
    modules: {
      ministration,
      siteinformation,
      slider,
      about,
      contact,
      blog,
      category,
      comment,
      reply,
      tag,
      user,
      role,
      log
    },

    // enable strict mode (adds overhead!)
    // for dev mode only
    strict: true,
  });

  return Store;
}
