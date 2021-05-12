export function setTags(state, tags) {
  state.tags = tags;
}

export function addTag(state, tag) {
  state.tags.push(tag);
}

export function updateTag(state, tag) {
  let index = state.tags.findIndex(c => c.id == tag.id);
  if (index > -1) {
    state.tags.splice(index, 1, tag);
  }
}

export function deleteTag(state, id) {
  let index = state.tags.findIndex(c => c.id == id);
  if (index > -1) {
    state.tags.splice(index, 1);
  }
}
