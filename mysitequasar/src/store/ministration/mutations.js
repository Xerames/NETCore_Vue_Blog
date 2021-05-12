export function setMinistrations(state, ministrations) {
  state.ministrations = ministrations;
}

export function addMinistration(state, ministration) {
  state.ministrations.push(ministration);
}

export function updateMinistration(state, ministration) {
  let index = state.ministrations.findIndex(c => c.id == ministration.id);
  if (index > -1) {
    state.ministrations.splice(index, 1, ministration);
  }
}

export function deleteMinistration(state, id) {
  let index = state.ministrations.findIndex(c => c.id == id);
  if (index > -1) {
    state.ministrations.splice(index, 1);
  }
}
