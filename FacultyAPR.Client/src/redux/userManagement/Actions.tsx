import {SET_USERS, EDIT_USER, DELETE_USER, NEW_USER} from './ActionTypes'
import IUser from '../../models/Users/IUser'
import UserType from '../../models/Users/UserType'

export const setUsers = (users:Array<IUser>) => ({
    type: SET_USERS,
    payload: {
        user: users
    }
});

export const deleteUser = (UserID: string, UserType:UserType) => ({
    type: DELETE_USER, 
    payload:{
        UserID: UserID,
        UserType: UserType
    }
});

export const newUser = (user:IUser) => ({
    type: NEW_USER,
     payload:{
         user: user,
        }
});

export const editUser = (user:IUser) => ({
    type: EDIT_USER, 
    payload: {
        user: user

    }
});