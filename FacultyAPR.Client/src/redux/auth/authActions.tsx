import { SET_TOKEN } from "./authActionTypes";

export const set_token = (token: string, name: string, email: string, isAuth: boolean) => ({
    type: SET_TOKEN,
    payload: {
        token: token,
        name: name,
        email: email,
        isAuthenticated: isAuth,
    }
});