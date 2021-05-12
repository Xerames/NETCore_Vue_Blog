export function getUserdata(state) {
  return state.userdata;
}

export function getUsersdata(state) {
  return state.usersdata;
}

export function getUser(state) {
  return state.userdata.user;
}

export function getCurrentUserRoles(state) {
  return state.userdata.roles;
}

export function getloggedIn(state) {
  return state.loggedIn;
}
