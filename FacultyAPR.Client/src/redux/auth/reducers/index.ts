import { SET_TOKEN } from "../authActionTypes";
interface IAuth  {
    token: string,
    email: string,
    name: string,
    isAuthenticated: boolean
}
const none_state: IAuth = {
    token: "",
    email: "",
    name: "",
    isAuthenticated: false
}

export default function reducer(state = none_state, action: {type: string, payload: {token: string, name: string, email: string, isAuthenticated: boolean} }){
    switch(action.type)
    {
        case SET_TOKEN:
            return { 
                token: action.payload.token, 
                email: action.payload.email,
                name: action.payload.name,
                isAuthenticated: action.payload.isAuthenticated 
            }
            
        default:
            return state;
    }
}