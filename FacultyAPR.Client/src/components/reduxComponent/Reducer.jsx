

const userReducer = (state = [], action) => {
    switch(action.type) {
        case "ADD_USER":
            let stateCopy = [...state, action.payload];
            localStorage.setItem('users', JSON.stringify(stateCopy));
            return stateCopy;

        case 'DELETE_USER':
            stateCopy = state.filter( user=> user.UserID!== action.payload);
            localStorage.setItem('user',JSON.stringify(stateCopy));
            return stateCopy

        case "EDIT_USER":
            stateCopy = state.map((user) => {
                const {UserID, FirstName, LastName, EmailAddress, UserType} = action.payload;
                if (user.UserID === UserID) {
                    user.FirstName = FirstName;
                    user.LastName = LastName;
                    user.EmailAddress = EmailAddress;
                    user.UserType = UserType;
                }
                return user;
            })
            
            localStorage.setItem('user', JSON.stringify(stateCopy));
            return stateCopy

            default:
                return state;

    }
}

export default userReducer;