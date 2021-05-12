export function setComments(state, comments) {
  state.comments = comments;
}

export function updateCommentByAdmin(state, comment) {
  let index = state.comments.findIndex(c => c.id == comment.id);
  if (index > -1) {
    state.comments.splice(index, 1, comment);
  }
}

export function deleteComment(state, id) {
  let index = state.comments.findIndex(c => c.id == id);
  if (index > -1) {
    state.comments.splice(index, 1);
  }
}
