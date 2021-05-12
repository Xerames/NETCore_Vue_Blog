export function setSiteInformations(state, siteinformations) {
  state.siteinformations = siteinformations;
}

export function addSiteInformation(state, siteinformation) {
  state.siteinformations.push(siteinformation);
}

export function updateSiteInformation(state, siteinformation) {
  let index = state.siteinformations.findIndex(c => c.id == siteinformation.id);
  if (index > -1) {
    state.siteinformations.splice(index, 1, siteinformation);
  }
}

export function deleteSiteInformation(state, id) {
  let index = state.siteinformations.findIndex(c => c.id == id);
  if (index > -1) {
    state.siteinformations.splice(index, 1);
  }
}
