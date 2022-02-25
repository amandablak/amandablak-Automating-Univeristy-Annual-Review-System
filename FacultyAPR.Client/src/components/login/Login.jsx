import {set_token} from "../../redux/auth/authActions"
import { connect } from "react-redux";
import ProfileSelection from "../Profile/ProfileSelection";
import {useHistory} from 'react-router-dom'

const Login = ({isAuthenticated}) => {
    var history = useHistory();
    if (isAuthenticated)
    {
        return <ProfileSelection />
    }
    
    return(
        <div>Please Log In</div>
    );
}

const mapStateToProps = (state) => {
    return {token: state.auth.token, isAuthenticated: state.auth.isAuthenticated}
}

export default connect(mapStateToProps, {set_token})(Login);