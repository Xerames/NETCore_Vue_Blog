export function getBlogsWithCategory(state) {
  return state.blogs.blogs;
}

export function getBlogsWithCategoryTotalCount(state) {
  return state.blogs.totalItems;
}

export function getBlogsByCategoryName(state) {
  return state.categoryblogs.blogs;
}
export function getBlogsByCategoryNameTotalCount(state) {
  return state.categoryblogs.totalItems;
}

export function getBlogsByTagName(state) {
  return state.tagblogs.blogs;
}

export function getBlogsByTagNameTotalCount(state) {
  return state.tagblogs.totalItems;
}

export function getLast5Blogs(state) {
  return state.last5blogs;
}
export function getBlogDetail(state) {
  return state.blogdetail;
}

export function getBlogWithCategoryAndTags(state) {
  return state.blogwithcategoryandtags;
}
