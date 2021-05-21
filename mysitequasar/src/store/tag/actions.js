export function getTags(context) {
  return this.$axios.get("tags/gettags").then(response => {
    context.commit("setTags", response.data);
  });
}

export function addTag(context, tag) {
  return this.$axios
    .post("tags/addtag", tag)
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
    .put("tags/updatetag", tag)
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
    .delete("tags/deletetag/" + id)
    .then(response => {
      context.commit("deleteTag", id);
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteBlogFromTag(context, data) {
  return this.$axios
    .post("tags/deleteblogfromtag", data)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getTag(context, id) {
  return this.$axios
    .get("tags/gettag/" + id)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getTagWithBlogs(context, id) {
  return this.$axios
    .get("tags/gettagwithblogs/" + id)
    .then(response => {
      return response.data;
    });
}
