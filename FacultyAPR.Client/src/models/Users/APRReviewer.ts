import IUser from "./IUser"
import Department from "../BusinessObjects/Department"
import UserType from "./UserType"

type APRReviewer = IUser & {
    department: Department;
    userType: UserType.Dean | UserType.FacultyChair;
};

export default APRReviewer