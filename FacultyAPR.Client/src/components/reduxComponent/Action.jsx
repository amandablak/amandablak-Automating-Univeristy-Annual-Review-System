
export function AddUser(user)
{
    return {
    type:'ADD_USER',
    payload:user
    }
}

export function DeleteUser(UserID)
{
    return {
    type:'DELETE_USER',
    payload:UserID
    }
}

export function EditUser(UserID)
{
    return {
        type:'EDIT_USER',
        payload:UserID
        }

}