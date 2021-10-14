export function getComment(context, id) {
  return this.$axios
    .get("comments/getcomment/" + id)
    .then(response => {
        return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getComments(context) {
  return this.$axios
    .get("comments/getcomments")
    .then(response => {
      context.commit("setComments", response.data);
    });
}

export function addComment(context, comment) {
  return this.$axios
    .post("comments/addcomment", comment)
    .then(response => {
      if (response.data.success) {
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateComment(context, comment) {
  return this.$axios
    .put("comments/updatecomment", comment)
    .then(response => {
      if (response.data.success) {
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateCommentByAdmin(context, comment) {
  return this.$axios
    .put("comments/updatecommentbyadmin", comment)
    .then(response => {
      if (response.data.success) {
        context.commit("updateCommentByAdmin", comment);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteComment(context, id) {
  return this.$axios
    .delete("comments/deletecomment/" + id)
    .then(response => {
      if (response.data.success) {
        context.commit("deleteComment", id);
      }
    });
}
