export function setReplies(state, replies) {
  state.replies = replies;
}

export function updateReplyByAdmin(state, reply) {
  let index = state.replies.findIndex(c => c.id == reply.id);
  if (index > -1) {
    state.replies.splice(index, 1, reply);
  }
}

export function deleteReply(state, id) {
  let index = state.replies.findIndex(c => c.id == id);
  if (index > -1) {
    state.replies.splice(index, 1);
  }
}
