export function setToken(state, token) {
  state.token = token;
  state.loggedIn = true;
}

export function setUserWithRoles(state, userdata) {
  state.userdata = userdata;
  state.loggedIn = true;
}

export function login(state, data) {
  state.token = data.accessToken;
  state.loggedIn = true;
  localStorage.setItem("token", data.accessToken);
  localStorage.setItem("refreshtoken",data.refreshToken);
}

export function logout(state) {
  state.token = null;
  state.loggedIn = false;
  state.userdata = null;
  localStorage.removeItem("token");
  localStorage.removeItem("refreshtoken");
}

export function updateUser(state, user) {
  state.userdata.user.id = user.id;
  state.userdata.user.userName = user.userName;
  state.userdata.user.firstName = user.firstName;
  state.userdata.user.lastName = user.lastName;
  state.userdata.user.email = user.email;
}

export function setUsersdata(state, data) {
  state.usersdata = data;
}

export function deleteUser(state, userid) {
  let index = state.usersdata.findIndex(c => c.user.id == userid);
  if (index > -1) {
    state.usersdata.splice(index, 1);
  }
}




