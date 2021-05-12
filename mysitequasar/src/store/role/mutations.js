export function setRoles(state, roles) {
  state.roles = roles;
}

export function setUserRoles(state, userroles) {
  state.userroles = userroles;
}

export function addRole(state, role) {
  state.roles.push(role);
}

export function updateRole(state, role) {
  let index = state.roles.findIndex(c => c.id == role.id);
  if (index > -1) {
    state.roles.splice(index, 1, role);
  }
}

export function deleteRole(state, id) {
  let index = state.roles.findIndex(c => c.id == id);
  if (index > -1) {
    state.roles.splice(index, 1);
  }
}
