<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Replies"
        :data="replies"
        :hide-header="mode === 'grid'"
        :columns="columns"
        :grid="mode == 'grid'"
        :filter="filter"
        :pagination.sync="pagination"
      >
        <template v-slot:top-right="props">
          <q-input
            outlined
            dense
            debounce="300"
            v-model="filter"
            placeholder="Search"
          >
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </q-input>
          <q-btn
            flat
            round
            dense
            :icon="props.inFullscreen ? 'fullscreen_exit' : 'fullscreen'"
            @click="props.toggleFullscreen"
            v-if="mode === 'list'"
          >
            <q-tooltip :disable="$q.platform.is.mobile" v-close-popup
              >{{
                props.inFullscreen ? "Exit Fullscreen" : "Toggle Fullscreen"
              }}
            </q-tooltip>
          </q-btn>

          <q-btn
            flat
            round
            dense
            :icon="mode === 'grid' ? 'list' : 'grid_on'"
            @click="
              mode = mode === 'grid' ? 'list' : 'grid';
              separator = mode === 'grid' ? 'none' : 'horizontal';
            "
            v-if="!props.inFullscreen"
          >
            <q-tooltip :disable="$q.platform.is.mobile" v-close-popup
              >{{ mode === "grid" ? "List" : "Grid" }}
            </q-tooltip>
          </q-btn>

          <q-btn
            color="primary"
            icon-right="archive"
            label="Export to csv"
            no-caps
            @click="exportTable"
          />
        </template>
        <template v-slot:body-cell-action="props">
          <q-td :props="props">
            <div class="q-gutter-sm">
              <q-btn
                dense
                color="primary"
                @click="getReply(props.row.id)"
                icon="edit"
              />
              <q-btn
                dense
                color="red"
                @click="deleteReply(props.row.id)"
                icon="delete"
              />
            </div>
          </q-td>
        </template>
        <template v-slot:body-cell-Confirmation="props">
          <q-td :props="props">
            <q-checkbox
              dense
              outlined
              disable
              v-model="props.row.confirmation"
            />
          </q-td>
        </template>
      </q-table>
    </q-card>
    <q-dialog v-model="showmodal">
      <q-card style="width: 600px; max-width: 60vw;">
        <q-card-section>
          <q-banner
            class="text-white q-mb-md"
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
          <div class="text-h6">
            <span>Update Reply</span>
            <q-btn
              round
              flat
              dense
              icon="close"
              class="float-right"
              color="grey-8"
              v-close-popup
              @click="resetReply"
            ></q-btn>
          </div>
        </q-card-section>
        <q-separator inset></q-separator>
        <q-card-section class="q-pt-none">
          <q-form class="q-gutter-md">
            <q-list>
              <q-item>
                <q-item-section>
                  <q-checkbox
                    dense
                    outlined
                    v-model="reply.confirmation"
                    label="Confirmation"
                  />
                </q-item-section>
              </q-item>
            </q-list>
          </q-form>
        </q-card-section>
        <q-card-actions align="right" class="text-teal">
          <q-btn label="Update" @click="UpdateReply" color="primary" />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script>
import { exportFile } from "quasar";
function wrapCsvValue(val, formatFn) {
  let formatted = formatFn !== void 0 ? formatFn(val) : val;
  formatted =
    formatted === void 0 || formatted === null ? "" : String(formatted);
  formatted = formatted.split('"').join('""');
  return `"${formatted}"`;
}
export default {
  data() {
    return {
      showmodal: false,
      reply: {
        id: null,
        content: null,
        commentId: null,
        confirmation: null,
        userId: null
      },
      show: false,
      message: null,
      errors: [],
      filter: "",
      mode: "list",
      columns: [
        {
          name: "Reply",
          align: "left",
          label: "Reply",
          field: "content",
          sortable: true
        },
        {
          name: "Confirmation",
          align: "left",
          label: "Confirmation",
          field: "confirmation"
        },
        {
          name: "action",
          align: "left",
          label: "Actions",
          field: "action"
        }
      ],
      pagination: {
        rowsPerPage: 10
      }
    };
  },
  created() {
    this.$store.dispatch("reply/getReplies");
  },
  computed: {
    replies() {
      return this.$store.getters["reply/getReplies"];
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
  methods: {
    exportTable() {
      // naive encoding to csv format
      const content = [this.columns.map(col => wrapCsvValue(col.label))]
        .concat(
          this.replies.map(row =>
            this.columns
              .map(col =>
                wrapCsvValue(
                  typeof col.field === "function"
                    ? col.field(row)
                    : row[col.field === void 0 ? col.name : col.field],
                  col.format
                )
              )
              .join(",")
          )
        )
        .join("\r\n");
      const status = exportFile("replies.csv", content, "text/csv");
      if (status !== true) {
        this.$q.notify({
          message: "Browser denied file download...",
          color: "negative",
          icon: "warning"
        });
      }
    },
    UpdateReply() {
      var payload={
        id:this.reply.id,
        commentId:this.reply.commentId,
        userId:this.reply.userId,
        content:this.reply.content,
        confirmation:this.reply.confirmation,
      }
      this.$store
        .dispatch("reply/updateReplyByAdmin", payload)
        .then(response => {
          if(response.success){
            this.resetReply();
          }
        });
    },
    deleteReply(id) {
      this.$store.dispatch("reply/deleteReply", id);
    },
    getReply(id) {
      if (id > 0) {
        this.showmodal = true;
        this.$store.dispatch("reply/getReply", id).then(response => {
          this.reply = response;
        });
      }
    },
    resetReply() {
      (this.reply.id = null),
        (this.reply.content = ""),
        (this.reply.commentId = null),
        (this.reply.userId = null),
        (this.reply.confirmation = null),
        (this.showmodal = false);
      (this.show = false), (this.message = null), (this.errors = []);
    }
  }
};
</script>
<style scoped>
.q-chip__content {
  display: block;
  text-align: center;
}
</style>
