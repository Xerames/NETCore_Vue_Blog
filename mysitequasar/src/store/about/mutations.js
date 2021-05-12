export function setAbouts(state, abouts) {
  state.abouts = abouts;
}

export function addAbout(state, about) {
  state.abouts.push(about);
}
export function updateAbout(state, about) {
  let index = state.abouts.findIndex(c => c.id == about.id);
  // if (existabout!=null){
  //   existabout.id=about.Id;
  //   existabout.description=about.Description;
  //   existabout.image=about.Image;
  // }
  if (index > -1) {
    state.abouts.splice(index, 1, about);
  }
}

export function deleteAbout(state, id) {
  let index = state.abouts.findIndex(c => c.id == id);
  if (index > -1) {
    state.abouts.splice(index, 1);
  }
}
