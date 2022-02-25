import {SET_USERS, EDIT_USER, DELETE_USER, NEW_USER} from './ActionTypes'


const initialState = {
    users: [{UserID:1, FirstName: 'Richard', LastName: 'Pallangyo', EmailAddress: 'rpallangyo@seattleu.edu',UserType:'Student'},
    {UserID:2, FirstName: 'Quinn', LastName: 'Wass', EmailAddress: 'qwass@seattleu.edu',UserType:'Professor'},
    {UserID:3, FirstName: 'Malav', LastName: 'Dipankar', EmailAddress: 'mdipankar@seattleu.edu',UserType:'Student'},
    {UserID:4, FirstName: 'Eric',  LastName: 'Larson', EmailAddress: 'elarson@seattleu.edu',UserType:'Chair'},
    {UserID:5, FirstName: 'Amanda',LastName: 'Chujun', EmailAddress: 'zhengchujun@seattleu.edu',UserType:'Dean'}],
}


export default function userManagement(state = initialState, action) {
    switch (action.type) {
        case SET_USERS:
            return { ...state, users: [...action.payload] };
        case DELETE_USER:
            return {
                ...state, users: [...state.users.filter(user =>
                    user.UserID !== action.payload.UserID)]         
            }
        case NEW_USER:
            return { ...state, users: [...state.users, action.payload] }
        case EDIT_USER:
            let users = state.users.map(user => {
                if (user.UserID === action.payload.UserID) {
                    user = action.payload
                }
                return user;
            })
            return { ...state, users: [...users] }
        default:
            return state;
    }
}