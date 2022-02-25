import IUser from "./IUser"
import Rank from "../BusinessObjects/Rank"
import Department from "../BusinessObjects/Department"
import UserType from "./UserType"

type FacultyUser = IUser & {
    rank: Rank;
    department: Department;
    userType: UserType.Faculty;
};
// Example usage
// let user = {} as FacultyUser

export default FacultyUser