export function getLogs(context) {
  return this.$axios
    .get("logs/getlogs")
    .then(response => {
      context.commit("setLogs", response.data);
    });
}
