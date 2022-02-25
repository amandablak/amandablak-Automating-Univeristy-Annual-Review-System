import IUser from "../../../models/Users/IUser";
import UserType from "../../../models/Users/UserType";
import { SET_USER } from "../userProfileActionTypes";

const none_state: IUser = {
    id: "",
    firstName: "",
    lastName: "",
    emailAddress: "wassq@seattleu.edu",
    userType: UserType.None
}

export default function reducer(state = none_state, action: {type: string, payload: {user: IUser} }){
    switch(action.type)
    {
        case SET_USER:
            return action.payload.user
        default:
            return state;
    }
}