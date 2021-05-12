<template>
  <div class="container">
    <div class="row justify-center q-pa-lg q-col-gutter-md">
      <div class="col-lg-4 col-sm-12 col-xs-12">
        <Sidebar></Sidebar>
      </div>
      <div class="col-lg-8 col-sm-12 col-xs-12">
        <q-card>
          <div class="row justify-center q-mt-xl">
            <h5>Change Photo</h5>
          </div>
          <div class="row justify-center">
            <q-banner
              class="text-white q-mb-md col-6"
              :class="success"
              v-if="this.show"
            >
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
          <div class="row justify-center q-mx-auto">
            <div class="col-6">
              <q-file
                clearable
                filled
                v-model="photo"
                label="Select Photo"
                :rules="[val => $v.photo.required || 'Photo is required']"
                accept="image/png,image/jpeg"
                bottom-slots
                :disable="photo != null"
              >
                <template v-slot:prepend>
                  <q-icon name="cloud_upload" />
                </template>
              </q-file>
            </div>
          </div>
          <div class="row justify-center">
            <div class="col-3" v-if="previewphoto != null">
              <div class="row justify-center">
                <label class="text-dark q-mb-xs">New Photo</label>
              </div>
              <div class="row justify-center">
                <q-avatar size="72px">
                  <img v-if="previewphoto" :src="apiurl + previewphoto" />
                </q-avatar>
              </div>
            </div>
            <div
              class="col-3"
              v-if="currentphoto != null && previewphoto == null"
            >
              <div class="row justify-center">
                <label class="text-dark q-mb-xs">Current Photo</label>
              </div>
              <div class="row justify-center">
                <q-avatar size="72px">
                  <img
                    :src="apiurl + currentphoto"
                    v-if="currentphoto != null"
                  />
                </q-avatar>
              </div>
            </div>
          </div>
          <div class="row justify-center">
            <div class="text-center q-mt-md q-mb-md q-gutter-md">
              <q-btn
                label="REMOVE SELECTED PHOTO"
                outline
                @click.prevent="deleteImage"
                v-if="this.previewphoto"
              ></q-btn>
              <q-btn
                label="CHANGE PHOTO"
                @click.prevent="ChangePhoto"
                outline
                :disabled="$v.$invalid"
                v-if="this.previewphoto"
              ></q-btn>
              <q-btn
                label="DELETE CURRENT PHOTO"
                @click.prevent="DeleteUserPhoto"
                outline
                v-if="currentphoto && previewphoto == null"
              ></q-btn>
            </div>
          </div>
        </q-card>
      </div>
    </div>
  </div>
</template>

<script>
import Sidebar from "components/account/Sidebar.vue";
import { required } from "vuelidate/lib/validators";
export default {
  components: {
    Sidebar
  },
  data() {
    return {
      show: false,
      message: null,
      errors: [],
      photo: null,
      previewphoto: null,
      currentphoto: null,
      user: {}
    };
  },
  validations: {
    photo: {
      required
    }
  },
  computed: {
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    },
    success() {
      return this.message != null ? "bg-green" : "bg-red";
    },
    isError() {
      return this.errors.length > 0
        ? "fas fa-exclamation-circle fa-2x"
        : "fas fa-check-circle fa-2x";
    }
  },
  watch: {
    photo(newValue, oldValue) {
      if (this.photo != null) {
        this.setPreviewPhoto();
      } else {
        this.previewphoto = null;
      }
    }
  },
  created() {
    this.$store.dispatch("user/GetProfileData").then(response => {
      this.user = response;
      this.currentphoto = response.photo;
    });
  },
  methods: {
    setPreviewPhoto() {
      //this.previewphoto = URL.createObjectURL(this.photo);
      const data = new FormData();
      data.append("Image", this.photo);
      this.$store.dispatch("user/uploadimage", data).then(response => {
        this.previewphoto = response;
        this.user.photo = response;
      });
    },
    ChangePhoto() {
      var payload = {
        Photo: this.previewphoto == null ? this.currentphoto : this.previewphoto
      };
      this.$store
        .dispatch("user/addorupdatePhoto", payload)
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.previewphoto = null;
          this.photo = null;
          this.currentphoto = this.user.photo;
          setTimeout(() => (this.show = false), 2000);
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
          setTimeout(() => (this.show = false), 2000);
        });
    },
    deleteImage() {
      var payload = {
        image: this.user.photo
      };
      this.$store.dispatch("user/deleteimage", payload).then(response => {
        this.user.photo = null;
        this.previewphoto = null;
        this.photo = null;
      });
    },
    DeleteUserPhoto() {
      this.$store
        .dispatch("user/deleteuserPhoto")
        .then(response => {
          this.show = true;
          this.message = response.message;
          this.previewphoto = null;
          this.photo = null;
          this.currentphoto = null;
          setTimeout(() => (this.show = false), 2000);
        })
        .catch(error => {
          this.show = true;
          this.errors = error;
          setTimeout(() => (this.show = false), 2000);
        });
    }
  },
  beforeRouteLeave(to, from, next) {
    if (this.previewphoto != null) {
      this.deleteImage();
      next();
    } else {
      next();
    }
  }
};
</script>

<style lang="scss" scoped></style>
