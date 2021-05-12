export function getComment(context, id) {
  return this.$axios
    .get("comments/getcomment/" + id, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
        return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getComments(context) {
  return this.$axios
    .get("comments/getcomments", {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      context.commit("setComments", response.data);
    });
}

export function addComment(context, comment) {
  return this.$axios
    .post("comments/addcomment", comment, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
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
    .put("comments/updatecomment", comment, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
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
    .put("comments/updatecommentbyadmin", comment, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
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
    .post("comments/deletecomment/" + id, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        context.commit("deleteComment", id);
      }
    });
}
