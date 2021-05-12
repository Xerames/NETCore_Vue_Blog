<template>
  <div class="q-mb-lg" :class="this.$q.screen.lt.sm ? 'q-px-md' : ''">
    <q-item-label header>Comments : {{ totalcomments }}</q-item-label>
    <q-list class="q-my-xs" v-for="comment in comments" :key="comment.id">
      <q-item>
        <q-item-section avatar top>
          <q-avatar>
            <img
              v-if="comment.user.photo != null"
              :src="apiurl + comment.user.photo"
            />
            <img v-else :src="defaultphoto" />
          </q-avatar>
        </q-item-section>
        <q-item-section top>
          <q-item-label
            >{{ comment.user.firstName }} {{ comment.user.lastName }} -
            {{ formatDate(comment.commentDate) }}
          </q-item-label>
          <q-item-label class="q-pt-xs" v-if="!editcommentclicked(comment.id)">
            {{ comment.content }}
          </q-item-label>
          <q-item-label v-if="editcommentclicked(comment.id) && editing">
            <q-input
              autofocus
              dense
              :value="editcommentdata.content"
              v-model="editcommentdata.content"
              :rules="[val => $v.editcommentdata.content.required || 'Content is required']"
            >
              <template v-slot:after>
                <q-btn
                  flat
                  dense
                  color="negative"
                  icon="cancel"
                  @click="cancelEditing"
                />
                <q-btn
                  flat
                  dense
                  color="positive"
                  :disabled="$v.editcommentdata.$invalid"
                  icon="check_circle"
                  @click="saveComment"
                />
              </template>
            </q-input>
          </q-item-label>
          <q-item-label class="q-pt-xs"
            ><q-btn
              @click="replycomment(comment.id)"
              v-if="getloggedIn"
              size="12px"
              flat
              dense
              color="deep-purple-6"
              round
              label="Reply"
          /></q-item-label>
        </q-item-section>
        <q-item-section center side>
          <div class="text-grey-8 q-gutter-xs q-mb-md">
            <q-btn
              size="12px"
              v-if="controluser(comment.userId)"
              @click="editcomment(comment.id, comment.content)"
              flat
              dense
              round
              icon="edit"
            />
          </div>
        </q-item-section>
      </q-item>
      <div v-if="controlselectedcomment(comment.id)">
        <div class="row justify-center">
          <q-input
            class="col-10 "
            v-model="reply.content"
            :rules="[val => $v.reply.content.required || 'Content is required']"
            filled
            type="textarea"
          />
        </div>
        <div class="row justify-center q-my-md">
          <q-btn
            @click="sendReply"
            rounded
            :disabled="$v.reply.$invalid"
            color="deep-purple-7"
            label="Send Reply"
          ></q-btn>
          <q-btn
            @click="cancelsendreply"
            rounded
            color="deep-purple-7"
            class="q-ml-md"
            label="Cancel"
          ></q-btn>
        </div>
      </div>
      <q-list
        class="q-ml-xl q-my-xs"
        v-for="reply in comment.replies"
        :key="reply.id"
      >
        <q-item>
          <q-item-section avatar top>
            <q-avatar>
              <img
                v-if="reply.user.photo != null"
                :src="apiurl + reply.user.photo"
              />
              <img v-else :src="defaultphoto" />
            </q-avatar>
          </q-item-section>
          <q-item-section top>
            <q-item-label
              >{{ reply.user.firstName }} {{ reply.user.lastName }} -
              {{ formatDate(reply.replyDate) }}</q-item-label
            >
            <q-item-label class="q-pt-xs" v-if="!editreplyclicked(reply.id)">
              {{ reply.content }}
            </q-item-label>
            <q-item-label v-if="editreplyclicked(reply.id) && editing">
              <q-input
                autofocus
                dense
                :value="editreplydata.content"
                v-model="editreplydata.content"
                :rules="[val => $v.editreplydata.content.required || 'Content is required']"
              >
                <template v-slot:after>
                  <q-btn
                    flat
                    dense
                    color="negative"
                    icon="cancel"
                    @click="cancelEditing"
                  />
                  <q-btn
                    flat
                    dense
                    color="positive"
                    icon="check_circle"
                    :disabled="$v.editreplydata.$invalid"
                    @click="saveReply"
                  />
                </template>
              </q-input>
            </q-item-label>
          </q-item-section>
          <q-item-section center side>
            <div class="text-grey-8 q-gutter-xs q-mb-md">
              <q-btn
                size="12px"
                v-if="controluser(reply.userId)"
                flat
                @click="editreply(reply.id, reply.commentId, reply.content)"
                dense
                round
                icon="edit"
              />
            </div>
          </q-item-section>
        </q-item>
      </q-list>
    </q-list>
    <div v-if="getloggedIn">
      <div class="row justify-center">
        <q-input
          class="col-10 "
          v-model="comment.content"
          :rules="[val => $v.comment.content.required || 'Content is required']"
          filled
          type="textarea"
        />
      </div>
      <div class="row justify-center q-my-md">
        <q-btn
          @click="sendComment"
          :disabled="$v.comment.$invalid"
          rounded
          color="deep-purple-7"
          label="Send Comment"
        ></q-btn>
      </div>
    </div>
  </div>
</template>

<script>
import { date } from "quasar";
import { required } from "vuelidate/lib/validators";
export default {
  props: ["totalcomments", "comments", "blogid"],
  data() {
    return {
      clickreply: false,
      editing: false,
      comment: {
        content: ""
      },
      reply: {
        content: "",
        commentid: null
      },
      editcommentdata: {
        id: null,
        content: ""
      },
      editreplydata: {
        id: null,
        commentid: null,
        content: ""
      }
    };
  },
  validations: {
    comment: {
      content: {
        required
      }
    },
    editcommentdata: {
      content: {
        required
      }
    },
    reply: {
      content: {
        required
      }
    },
    editreplydata: {
      content: {
        required
      }
    }
  },
  computed: {
    apiurl() {
      return this.$store.getters["siteinformation/getApiUrl"];
    },
    defaultphoto() {
      return this.$store.getters["siteinformation/getDefaultPhoto"];
    },
    getUser() {
      return this.$store.getters["user/getUser"];
    },
    getloggedIn() {
      return this.$store.getters["user/getloggedIn"];
    }
  },
  methods: {
    getBlogDetail() {
      this.$store.dispatch("blog/getBlogDetail", this.$route.params.slug);
    },
    formatDate(commentorreplydate) {
      return date.formatDate(commentorreplydate, "DD/MM/YYYY HH:mm");
    },
    replycomment(commentid) {
      this.clickreply = true;
      this.reply.commentid = commentid;
    },
    cancelsendreply() {
      this.clickreply = false;
      this.reply.commentid = null;
      this.errors = null;
    },
    controlselectedcomment(commentid) {
      return commentid == this.reply.commentid;
    },
    controluser(userid) {
      if (this.getloggedIn) {
        return userid == this.getUser.id;
      }
    },
    editcomment(commentid, content) {
      this.editing = true;
      this.editcommentdata.id = commentid;
      this.editcommentdata.content = content;
    },
    editcommentclicked(commentid) {
      return commentid == this.editcommentdata.id;
    },
    editreply(replyid, commentid, content) {
      this.editing = true;
      this.editreplydata.id = replyid;
      this.editreplydata.commentid = commentid;
      this.editreplydata.content = content;
    },
    editreplyclicked(repylid) {
      return repylid == this.editreplydata.id;
    },
    cancelEditing() {
      this.editing = false;
      this.editcommentdata.id = null;
      this.editcommentdata.content = null;
      (this.editreplydata.id = null),
        (this.editreplydata.commentid = null),
        (this.editreplydata.content = null);
      this.replyerrors = null;
      this.editreplyerrors = null;
      this.commenterrors = null;
      this.editcommenterrors = null;
    },
    saveComment() {
      var payload={
        userId:this.getUser.id,
        id:this.editcommentdata.id,
        blogId:this.blogid,
        content:this.editcommentdata.content
      }
      this.$store.dispatch("comment/updateComment", payload).then(response => {
        if (response.success) {
          this.getBlogDetail();
          this.editcommentdata.id = null;
          this.editcommentdata.content = "";
          this.editing = false;
        } else {
          alert(response.errors);
        }
      });
    },
    saveReply() {
      var payload={
        userId:this.getUser.id,
        id:this.editreplydata.id,
        commentId:this.editreplydata.commentid,
        content:this.editreplydata.content
      }
      this.$store.dispatch("reply/updateReply", payload).then(response => {
        if (response.success) {
          this.getBlogDetail();
          this.editreplydata.id = null;
          this.editreplydata.content = "";
          this.editreplydata.commentid = null;
          this.editing = false;
        } else {
          alert(response.errors);
        }
      });
    },
    sendComment() {
      var payload={
        userId:this.getUser.id,
        blogId:this.blogid,
        content:this.comment.content
      }
      this.$store.dispatch("comment/addComment", payload).then(response => {
        if (response.success) {
          this.getBlogDetail();
          this.comment.content = "";
        } else {
          alert(response.errors);
        }
      });
    },
    sendReply() {
       var payload={
        userId:this.getUser.id,
        commentId:this.reply.commentid,
        content:this.reply.content
      }
      this.$store.dispatch("reply/addReply", payload).then(response => {
        if (response.success) {
          this.getBlogDetail();
          this.reply.content = "";
          this.reply.commentid = null;
        } else {
          alert(response.errors);
        }
      });
    }
  }
};
</script>

<style lang="scss" scoped></style>
