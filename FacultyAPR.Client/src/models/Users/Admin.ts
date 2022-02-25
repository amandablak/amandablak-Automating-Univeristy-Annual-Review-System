import IUser from "./IUser"
import UserType from "./UserType"

type Admin = IUser & { userType: UserType.Admin; }

export default Admin