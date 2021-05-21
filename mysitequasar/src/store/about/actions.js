export function getAbout(context, id) {
  return this.$axios
    .get("abouts/getabout/" + id)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getAbouts(context) {
  return this.$axios.get("abouts/getabouts").then(response => {
    context.commit("setAbouts", response.data);
  });
}

export function addAbout(context, about) {
  return this.$axios
    .post("abouts/addabout", about)
    .then(response => {
      if (response.data.success) {
        context.commit("addAbout", response.data.data);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateAbout(context, about) {
  return this.$axios
    .put("abouts/updateabout", about)
    .then(response => {
      if (response.data.success) {
        context.commit("updateAbout", about);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteAbout(context, id) {
  return this.$axios.delete("abouts/deleteabout/" + id).then(response => {
    if (response.data.success) {
      context.commit("deleteAbout", id);
    }
  });
}

export function uploadimage(context, image) {
  return this.$axios.post("abouts/uploadimage", image).then(response => {
    return response.data.data;
  });
}

export function deleteimage(context, image) {
  return this.$axios.post("abouts/deleteimage", image).then(response => {
    return response.data;
  });
}
