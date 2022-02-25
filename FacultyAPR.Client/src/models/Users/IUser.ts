import UserType from "./UserType";

interface IUser {
    id: string;
    firstName: string;
    lastName: string;
    emailAddress: string;
    userType: UserType;
}

export default IUser