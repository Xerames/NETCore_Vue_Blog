export function getSiteInformation(context, id) {
  return this.$axios
    .get("siteinformations/getsiteinformation/" + id, {
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

export function getSiteInformations(context, id) {
  return this.$axios
    .get("siteinformations/getsiteinformations")
    .then(response => {
      context.commit("setSiteInformations", response.data);
    });
}

export function addSiteInformation(context, siteinformation) {
  return this.$axios
    .post("siteinformations/addsiteinformation", siteinformation, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
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
    .put("siteinformations/updatesiteinformation", siteinformation, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
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
    .delete("siteinformations/deletesiteinformation/" + id, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        context.commit("deleteSiteInformation", id);
      }
    });
}

export function uploadimage(context, image) {
  return this.$axios
    .post("siteinformations/uploadimage", image, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      return response.data.data;
    });
}

export function deleteimage(context, image) {
  return this.$axios
    .post("siteinformations/deleteimage", image, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      return response.data;
    });
}
