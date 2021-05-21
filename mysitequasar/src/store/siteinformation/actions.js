export function getSiteInformation(context, id) {
  return this.$axios
    .get("siteinformations/getsiteinformation/" + id)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getSiteInformations(context, id) {
  return this.$axios
    .get("siteinformations/getsiteinformations")
    .then(response => {
      context.commit("setSiteInformations", response.data);
    });
}

export function addSiteInformation(context, siteinformation) {
  return this.$axios
    .post("siteinformations/addsiteinformation", siteinformation,)
    .then(response => {
      if (response.data.success) {
        context.commit("addSiteInformation", response.data.data);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateSiteInformation(context, siteinformation) {
  return this.$axios
    .put("siteinformations/updatesiteinformation", siteinformation)
    .then(response => {
      if (response.data.success) {
        context.commit("updateSiteInformation", siteinformation);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteSiteInformation(context, id) {
  return this.$axios
    .delete("siteinformations/deletesiteinformation/" + id)
    .then(response => {
      if (response.data.success) {
        context.commit("deleteSiteInformation", id);
      }
    });
}

export function uploadimage(context, image) {
  return this.$axios
    .post("siteinformations/uploadimage", image)
    .then(response => {
      return response.data.data;
    });
}

export function deleteimage(context, image) {
  return this.$axios
    .post("siteinformations/deleteimage", image)
    .then(response => {
      return response.data;
    });
}
