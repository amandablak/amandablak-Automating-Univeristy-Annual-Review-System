import { connect } from "react-redux";
import {set_user} from "../../redux/userProfile/userProfileActions"
import Button from 'react-bootstrap/Button'
import {useHistory} from 'react-router-dom'
import {GetUsersByEmail} from "../../api/Routing"
import {useState, useEffect} from 'react'
import UserType from "../../models/Users/UserType";
import './user.css'

const ProfileSelection = ({set_user , auth}) => {
    var history = useHistory()
    const [profiles, setProfiles] = useState([]);

    useEffect(() => {
        async function getProfiles() {
            try
            {
                var profiles = await GetUsersByEmail(auth.email);
                setProfiles(profiles.data || []);
            } 
            catch(_) {}
        }
        getProfiles();
    }, [])
    return (
        <div className = "user-profiles">
            {profiles.map((profile, index) => 
            {
                switch (profile.userType) {
                    case UserType.Faculty:
                        return (<div className="user-child"><Button key={index} className="mt-2-user" variant="info" onClick={() => {
                            set_user(profile)
                            history.push("/")
                        }}>Faculty</Button></div>)
                    case UserType.FacultyChair || UserType.Dean:
                        return (<div className="user-child"><Button key={index} className="mt-2-user" variant="info" onClick={() => {
                            set_user(profile)
                            history.push("/")
                        }}>Reviewer</Button></div>)
                    case UserType.Admin:
                        return (<div className="user-child"><Button key={index} className="mt-2-user" variant="info" onClick={() => {
                            set_user(profile)
                            history.push("/")
                        }}>Admin</Button></div>)
                    default:
                        return null;
                }
            })}
        </div>
    )
}


const mapStateToProps = (state) => 
{
    return {user: state.user, auth: state.auth}
}

export default connect(mapStateToProps, {set_user})(ProfileSelection);