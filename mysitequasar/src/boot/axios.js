import Vue from "vue";
import axios from "axios";

export default ({ store, Vue }) => {
  Vue.prototype.$axios = axios;
  store.$axios = axios;
  axios.defaults.baseURL = "http://localhost:52951/api/";
  axios.interceptors.request.use(function(config) {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    return config;
  });
};