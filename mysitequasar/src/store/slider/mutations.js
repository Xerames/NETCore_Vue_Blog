export function setSliders(state, sliders) {
  state.sliders = sliders;
}

export function addSlider(state, slider) {
  state.sliders.push(slider);
}

export function updateSlider(state, slider) {
  let index = state.sliders.findIndex(c => c.id == slider.id);
  if (index > -1) {
    state.sliders.splice(index, 1, slider);
  }
}

export function deleteSlider(state, id) {
  let index = state.sliders.findIndex(c => c.id == id);
  if (index > -1) {
    state.sliders.splice(index, 1);
  }
}
