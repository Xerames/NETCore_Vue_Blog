export function getCategory(context, id) {
  return this.$axios
    .get("categories/getcategory/" + id, {
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

export function getCategories(context) {
  return this.$axios.get("categories/getcategorieswithblogs").then(response => {
    context.commit("setCategories", response.data);
  });
}

export function addCategory(context, category) {
  return this.$axios
    .post("categories/addcategory", category, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      context.commit("addCategory", response.data.data);
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateCategory(context, category) {
  return this.$axios
    .put("categories/updatecategory", category, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        context.commit("updateCategory", category);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteCategory(context, id) {
  return this.$axios
    .delete("categories/deletecategory/" + id, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        context.commit("deleteCategory", id);
      }
    });
}
