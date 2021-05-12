import Vue from "vue";
import axios from "axios";

export default ({ store, Vue }) => {
  Vue.prototype.$axios = axios;
  store.$axios = axios;
  axios.defaults.baseURL = "http://localhost:52951/api/";
};
