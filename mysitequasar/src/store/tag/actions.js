export function getTags(context) {
  return this.$axios.get("tags/gettags").then(response => {
    context.commit("setTags", response.data);
  });
}

export function addTag(context, tag) {
  return this.$axios
    .post("tags/addtag", tag, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        context.commit("addTag", response.data.data);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateTag(context, tag) {
  return this.$axios
    .put("tags/updatetag", tag, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        context.commit("updateTag", tag);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteTag(context, id) {
  return this.$axios
    .delete("tags/deletetag/" + id, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      context.commit("deleteTag", id);
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteBlogFromTag(context, data) {
  return this.$axios
    .post("tags/deleteblogfromtag", data, {
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

export function getTag(context, id) {
  return this.$axios
    .get("tags/gettag/" + id, {
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

export function getTagWithBlogs(context, id) {
  return this.$axios
    .get("tags/gettagwithblogs/" + id, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      return response.data;
    });
}
