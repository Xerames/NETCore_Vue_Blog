export function getReplies(context) {
  return this.$axios
    .get("replies/getreplies")
    .then(response => {
      context.commit("setReplies", response.data);
    });
}

export function addReply(context, reply) {
  return this.$axios
    .post("replies/addreply", reply)
    .then(response => {
      if (response.data.success) {
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateReply(context, reply) {
  return this.$axios
    .put("replies/updatereply", reply)
    .then(response => {
      if (response.data.success) {
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateReplyByAdmin(context, reply) {
  return this.$axios
    .put("replies/updatereplybyadmin", reply)
    .then(response => {
      if (response.data.success) {
        context.commit("updateReplyByAdmin", reply);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteReply(context, id) {
  return this.$axios
    .delete("replies/deletereply/" + id)
    .then(response => {
      if (response.data.success) {
        context.commit("deleteReply", id);
      }
    });
}

export function getReply(context, id) {
  return this.$axios
    .get("replies/getreply/" + id)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}
