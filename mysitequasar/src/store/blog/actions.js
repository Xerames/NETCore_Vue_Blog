export function getBlogsWithCategory(context, query) {
  return this.$axios
    .get("blogs/getblogswithcategory?" + query)
    .then(response => {
      context.commit("setBlogs", response.data);
    }).catch(error => {
      throw error.response.data.Errors;
    });
}

export function getLast5Blogs(context) {
  return this.$axios.get("blogs/getlast5blogs").then(response => {
    context.commit("setLast5Blogs", response.data);
  }).catch(error => {
    throw error.response.data.Errors;
  });
}

export function getBlogsByCategoryName(context, payload) {
  return this.$axios
    .get(
      "blogs/getblogsbycategoryname/" +
        payload.categoryname +
        "?" +
        payload.query
    )
    .then(response => {
      context.commit("setCategoryBlogs", response.data);
    }).catch(error => {
      throw error.response.data.Errors;
    });
}

export function getBlogsByTagName(context, payload) {
  return this.$axios
    .get("blogs/getblogsbytagname/" + payload.tagname + "?" + payload.query)
    .then(response => {
      context.commit("setTagBlogs", response.data);
    }).catch(error => {
      throw error.response.data.Errors;
    });
}

export function addBlog(context, blog) {
  return this.$axios
    .post("blogs/addblog", blog)
    .then(response => {
      if (response.data.success) {
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function updateBlog(context, blog) {
  return this.$axios
    .put("blogs/updateblog", blog)
    .then(response => {
      if (response.data.success) {
        return response.data;
      }
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function deleteBlog(context, id) {
  return this.$axios
    .delete("blogs/deleteblog/" + id)
    .then(response => {
      if (response.data.success) {
        context.commit("deleteBlog", id);
      }
    }).catch(error => {
      throw error.response.data.Errors;
    });
}

export function getBlog(context, id) {
  return this.$axios
    .get("blogs/getblog/" + id)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error.response.data.Errors;
    });
}

export function getBlogWithCategoryAndTags(context, id) {
  return this.$axios
    .get("blogs/getblogwithcategoryandtags/" + id)
    .then(response => {
      context.commit("setBlogCategoryAndTags", response.data);
      return response.data;
    }).catch(error => {
      throw error.response.data.Errors;
    });
}

export function getBlogDetail(context, slug) {
  return this.$axios
    .get("blogs/getblogwithcategorytagsandcommentswithreplies/" + slug)
    .then(response => {
      context.commit("setBlogDetail", response.data.data);
    }).catch(error => {
      throw error.response.data.Errors;
    });
}

export function searchBlogs(context, payload) {
  return this.$axios
    .get(
      "blogs/searchblogs?search=" + payload.searchquery + "&" + payload.query
    )
    .then(response => {
      return response.data;
    }).catch(error => {
      throw error.response.data.Errors;
    });
}

export function uploadimage(context, image) {
  return this.$axios
    .post("blogs/uploadimage", image)
    .then(response => {
      return response.data.data;
    });
}

export function deleteimage(context, image) {
  return this.$axios
    .post("blogs/deleteimage", image)
    .then(response => {
      return response.data;
    });
}
