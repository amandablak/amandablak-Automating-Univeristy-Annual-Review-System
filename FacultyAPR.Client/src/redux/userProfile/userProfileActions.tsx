import {SET_USER} from './userProfileActionTypes'
import IUser from '../../models/Users/IUser'

export const set_user = (user: IUser) => ({
    type: SET_USER,
    payload: {
        user: user
    }
});