export const getUserProfile = store => store.user;
export const getUserType = store => getUserProfile(store).userType;

