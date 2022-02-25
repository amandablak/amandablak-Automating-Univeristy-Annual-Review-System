import { combineReducers } from "redux";
import user from './userProfile/reducers'
import form from './form/reducers/form';
import auth from './auth/reducers';
import userManagement from './userManagement/userManagement'

export default combineReducers({form, user, auth, userManagement});