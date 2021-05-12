export function setBlogs(state, blogswithtotalcount) {
  state.blogs.blogs = blogswithtotalcount.data;
  state.blogs.totalItems = blogswithtotalcount.totalItems;
}

export function setCategoryBlogs(state, categoryblogswithtotalcount) {
  state.categoryblogs.blogs = categoryblogswithtotalcount.data;
  state.categoryblogs.totalItems=categoryblogswithtotalcount.totalItems;
}

export function setTagBlogs(state, tagblogsswithtotalcount) {
  state.tagblogs.blogs = tagblogsswithtotalcount.data;
  state.tagblogs.totalItems=tagblogsswithtotalcount.totalItems;
}

export function setLast5Blogs(state, last5blogs) {
  state.last5blogs = last5blogs;
}

export function setBlogDetail(state, blogdetail) {
  state.blogdetail = blogdetail;
}

export function setBlogCategoryAndTags(state, blogwithcategoryandtags) {
  state.blogwithcategoryandtags = blogwithcategoryandtags;
}

export function addBlog(state, blog) {
  state.blogs.blogs.push(blog);
}

export function updateBlog(state, blog) {
  let index = state.blogs.blogs.findIndex(c => c.id == blog.id);
  if (index > -1) {
    state.blogs.blogs.splice(index, 1, blog);
  }
}

export function deleteBlog(state, id) {
  let index = state.blogs.blogs.findIndex(c => c.id == id);
  if (index > -1) {
    state.blogs.blogs.splice(index, 1);
  }
}
