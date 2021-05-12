export function setContacts(state, contacts) {
  state.contacts = contacts;
}

export function addContact(state, contact) {
  state.contacts.push(contact);
}

export function updateContact(state, contact) {
  let index = state.contacts.findIndex(c => c.id == contact.id);
  if (index > -1) {
    state.contacts.splice(index, 1, contact);
  }
}

export function deleteContact(state, id) {
  let index = state.contacts.findIndex(c => c.id == id);
  if (index > -1) {
    state.contacts.splice(index, 1);
  }
}
