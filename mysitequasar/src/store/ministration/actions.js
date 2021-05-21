export function getMinistration(context, id) {
  return this.$axios
    .get("ministrations/getministration/" + id)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getMinistrations(context) {
  return this.$axios.get("ministrations/getministrations").then(response => {
    context.commit("setMinistrations", response.data);
  });
}

export function addMinistration(context, ministration) {
  return this.$axios
    .post("ministrations/addministration", ministration)
    .then(response => {
      if (response.data.success) {
        context.commit("addMinistration", response.data.data);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateMinistration(context, ministration) {
  return this.$axios
    .put("ministrations/updateministration", ministration)
    .then(response => {
      if (response.data.success) {
        context.commit("updateMinistration", ministration);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteMinistration(context, id) {
  return this.$axios
    .delete("ministrations/deleteministration/" + id)
    .then(response => {
      if (response.data.success) {
        context.commit("deleteMinistration", id);
      }
    });
}

export function uploadimage(context, image) {
  return this.$axios
    .post("ministrations/uploadimage", image)
    .then(response => {
      return response.data.data;
    });
}

export function deleteimage(context, image) {
  return this.$axios
    .post("ministrations/deleteimage", image)
    .then(response => {
      return response.data;
    });
}
