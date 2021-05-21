export function getRoles(context) {
  return this.$axios.get("roles/getroles").then(response => {
    context.commit("setRoles", response.data.data);
  });
}

export function addRole(context, role) {
  return this.$axios
    .post("roles/addrole", role)
    .then(response => {
      if (response.data.success) {
        context.commit("addRole", response.data.data);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateRole(context, role) {
  return this.$axios
    .put("roles/updaterole", role)
    .then(response => {
      if (response.data.success) {
        context.commit("updateRole", role);
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteRole(context, id) {
  return this.$axios
    .delete("roles/deleterole/" + id)
    .then(response => {
      if (response.data.success) {
        context.commit("deleteRole", id);
      }
    });
}

export function getAssignedRoles(context, userid) {
  return this.$axios
    .get("roles/getassignedroles/" + userid)
    .then(response => {
      context.commit("setUserRoles", response.data.data);
      return response.data.data;
    });
}

export function roleAssign(context, assignroles) {
  return this.$axios
    .post("roles/roleassign/", assignroles)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getRole(context, id) {
  return this.$axios.get("roles/getrole/" + id).then(response => {
    return response.data.data;
  });
}
