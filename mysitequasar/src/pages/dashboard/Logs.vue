<template>
  <q-page class="q-pa-sm">
    <q-card>
      <q-table
        title="Logs"
        :data="logs"
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
      </q-table>
    </q-card>
  </q-page>
</template>

<script>
import { exportFile } from "quasar";
import { date } from "quasar";
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
      filter: "",
      mode: "list",
      columns: [
        {
          name: "Username",
          align: "left",
          label: "Username",
          field: row=>JSON.parse(row.messageTemplate).Username,
          sortable: true
        },
        {
          name: "Manager Name",
          required: true,
          label: "Manager Name",
          align: "left",
          field:  row=>JSON.parse(row.messageTemplate).ManagerName,
          sortable: true
        },
        {
          name: "Method Name",
          align: "left",
          label: "Method Name",
          field: row=>JSON.parse(row.messageTemplate).MethodName,
          sortable: true
        },
        {
          name: "level",
          align: "left",
          label: "Level",
          field: "level",
          sortable: true
        },
        {
          name: "TimeStamp",
          align: "left",
          label: "TimeStamp",
          field: row=>this.formatDate(row.timeStamp),
          sortable: true
        },
      ],
      pagination: {
        rowsPerPage: 10
      }
    };
  },
  created() {
    this.$store.dispatch("log/getLogs");
  },
  computed: {
    logs() {
      return this.$store.getters["log/getLogs"];
    }
  },
  methods: {
    exportTable() {
      // naive encoding to csv format
      const content = [this.columns.map(col => wrapCsvValue(col.label))]
        .concat(
          this.contacts.map(row =>
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
      const status = exportFile("contacts.csv", content, "text/csv");
      if (status !== true) {
        this.$q.notify({
          message: "Browser denied file download...",
          color: "negative",
          icon: "warning"
        });
      }
    },
    formatDate(logdate) {
      return date.formatDate(logdate, "DD/MM/YYYY HH:mm");
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
