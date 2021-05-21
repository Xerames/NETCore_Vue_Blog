export function getSlider(context, id) {
  return this.$axios
    .get("sliders/getslider/" + id)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getSliders(context) {
  return this.$axios.get("sliders/getsliders").then(response => {
    context.commit("setSliders", response.data);
  });
}

export function addSlider(context, slider) {
  return this.$axios
    .post("sliders/addslider", slider)
    .then(response => {
      if (response.data.success) {
        context.commit("addSlider", response.data.data);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateSlider(context, slider) {
  return this.$axios
    .put("sliders/updateslider", slider)
    .then(response => {
      if (response.data.success) {
        context.commit("updateSlider", slider);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteSlider(context, id) {
  return this.$axios
    .delete("sliders/deleteslider/" + id)
    .then(response => {
      if (response.data.success) {
        context.commit("deleteSlider", id);
      }
    });
}

export function uploadimage(context, image) {
  return this.$axios
    .post("sliders/uploadimage", image)
    .then(response => {
      return response.data.data;
    });
}

export function deleteimage(context, image) {
  return this.$axios
    .post("sliders/deleteimage", image)
    .then(response => {
      return response.data;
    });
}
