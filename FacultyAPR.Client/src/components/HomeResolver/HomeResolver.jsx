import {useHistory} from 'react-router-dom'
import FacultyHome from "../Faculty/Home/FacultyHome";
import ReviewerHome from "../Reviewer/ReviewerHome";
import UserManagement from "../Admin/UserManagement";
import UserType from "../../models/Users/UserType";
import { connect } from "react-redux";
const HomeResolver = ({user, isAuthenticated}) => {
    var history = useHistory();
    if (!isAuthenticated)
    {
        history.push("/Login");
    }
    return (
        <div>
            {MapUserToRoute(user.userType)}
        </div>);
}

const MapUserToRoute = (userType) => {
    switch (userType) {
        case UserType.Faculty:
            return <FacultyHome/>;
        case UserType.FacultyChair || UserType.Dean:
            return <ReviewerHome/>;
        case UserType.Admin:
            return <UserManagement/>;
    }
}

const mapStateToProps = (state) => {
    return {user: state.user, isAuthenticated: state.auth.isAuthenticated}
}

export default connect(mapStateToProps)(HomeResolver);