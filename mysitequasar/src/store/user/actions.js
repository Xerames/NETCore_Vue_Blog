export function getToken(context) {
  let token = localStorage.getItem("token");
  if (token) {
    context.commit("setToken", token);
    var decodedtoken = JSON.parse(atob(token.split(".")[1]));
    var expdatefromtoken = parseInt(decodedtoken["exp"] * 1000);
    let time = new Date().getTime();
    if (time >= +expdatefromtoken) {
      context.commit("logout");
    } else {
      let timerSecond = +expdatefromtoken - time;
      context.dispatch("setTimeoutTimer", timerSecond);
    }
  } else {
    return false;
  }
}

export function setTimeoutTimer(context, expiresIn) {
  setTimeout(() => {
    context.dispatch("logout");
  }, expiresIn);
}

export function getUserWithRoles(context) {
  return this.$axios
    .get("users/getuserwithroles", {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      context.commit("setUserWithRoles", response.data.data);
    });
}

export function register(context, credentials) {
  return this.$axios
    .post("auths/register", credentials)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function confirmemail(context, userId, token) {
  return this.$axios
    .post("auths/confirmemail", userId, token)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function login(context, credentials) {
  return this.$axios
    .post("auths/login", credentials)
    .then(response => {
      context.commit("login", response.data.data);
      context.dispatch("getUserWithRoles");
      context.dispatch("getToken");
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function logout(context) {
  var refreshtoken = localStorage.getItem("refreshtoken");
  var payload = {
    RefreshToken: refreshtoken
  };
  return this.$axios
    .post("auths/logout", payload, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(() => {
      context.commit("logout");
    });
}

export function updateUser(context, user) {
  return this.$axios
    .put("users/updateuser", user, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      context.commit("updateUser", user);
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateUserByAdmin(context, user) {
  return this.$axios
    .put("users/updateuserbyadmin", user, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        return response.data;
      }
    })
    .catch(error => {
       throw error.response.data.Errors;
    });
}

export function passwordChangeByAdmin(context, data) {
  return this.$axios
    .put("users/passwordchangebyadmin", data, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteUser(context, userid) {
  return this.$axios
    .delete("users/deleteuser/" + userid, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        context.commit("deleteUser", userid);
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function changePassword(context, data) {
  return this.$axios
    .post("users/passwordchange", data, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}
export function addorupdatePhoto(context, data) {
  return this.$axios
    .post("users/addorupdatephoto", data, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        context.dispatch("getUserWithRoles");
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteuserPhoto(context) {
  return this.$axios
    .post("users/deleteuserphoto", null, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      if (response.data.success) {
        context.dispatch("getUserWithRoles");
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getUserswithRoles(context) {
  return this.$axios
    .get("users/getuserswithroles", {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      context.commit("setUsersdata", response.data.data);
    });
}

export function GetProfileData(context) {
  return this.$axios
    .get("users/getuserwithroles", {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      return response.data.data.user;
    });
}

export function forgetPassword(context, email) {
  return this.$axios
    .post("users/forgetpassword", email)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function resetPassword(context, data) {
  return this.$axios
    .post("users/resetpassword", data)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getUser(context, userid) {
  return this.$axios
    .get("users/getuser/" + userid, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      return response.data.data;
    });
}

export function uploadimage(context, image) {
  return this.$axios
    .post("users/uploadimage", image, {
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
    .post("users/deleteimage", image, {
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token")
      }
    })
    .then(response => {
      return response.data;
    });
}
