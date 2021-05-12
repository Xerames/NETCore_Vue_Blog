export function getLogs(context) {
  return this.$axios
    .get("logs/getlogs", {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      context.commit("setLogs", response.data);
    });
}
