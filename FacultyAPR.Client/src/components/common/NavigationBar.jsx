import './Navigation.css'
import axios from 'axios';
import {Navbar, Button} from 'react-bootstrap'
import { connect } from "react-redux";
import { PublicClientApplication } from "@azure/msal-browser";
import { msalConfig, loginRequest, tokenRequest, silentRequest } from '../../auth/authConfig';
import {set_token} from '../../redux/auth/authActions'
import {useEffect} from 'react'
import {useHistory} from 'react-router-dom'

const isIE = () => {
    const ua = window.navigator.userAgent;
    const msie = ua.indexOf("MSIE ") > -1;
    const msie11 = ua.indexOf("Trident/") > -1;

    // If you as a developer are testing using Edge InPrivate mode, please add "isEdge" to the if check
    // const isEdge = ua.indexOf("Edge/") > -1;

    return msie || msie11;
};

// If you support IE, our recommendation is that you sign-in using Redirect flow
const useRedirectFlow = isIE();

const msalApp = new PublicClientApplication(msalConfig);

const NavigationBar = ({set_token, auth}) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${auth.token}`;
    var history = useHistory()
    useEffect(() => {
        if (useRedirectFlow) {
            msalApp.handleRedirectPromise()
                .then(handleResponse)
                .catch(err => {
                    console.error(err);
                });
        }
        acquireToken()
    }, [])

    const getAccounts = () => {
        /**
         * See here for more info on account retrieval: 
         * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-common/docs/Accounts.md
         */
        const currentAccounts = msalApp.getAllAccounts();
        console.log(currentAccounts)
        if (currentAccounts === null) {
            console.error("No accounts detected!");
            return;
        } else if (currentAccounts.length > 1) {
            console.warn("Multiple accounts detected.");

            console.log({
                username: currentAccounts[0].username,
                account: msalApp.getAccountByUsername(currentAccounts[0].username),
                isAuthenticated: true,
            });
        } else if (currentAccounts.length === 1) {
            console.log({
                username: currentAccounts[0].username,
                account: msalApp.getAccountByUsername(currentAccounts[0].username),
                isAuthenticated: true,
                token: "asd"
            });
        }
    }

    const handleResponse = (response) => {
        if (response !== null) {
            set_token(
                response.accessToken,
                response.account.name,
                response.account.username,
                true
            );
        } else {
            getAccounts();
        }
    }

    const acquireToken = async () => {
        /**
         * See here for more info on account retrieval: 
         * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-common/docs/Accounts.md
         */
        silentRequest.account = msalApp.getAccountByUsername(auth.email);

        return msalApp.acquireTokenSilent(silentRequest).catch(error => {
            console.warn("silent token acquisition fails. acquiring token using interactive method");
            if (error) {
                // fallback to interaction when silent call fails
                tokenRequest.account = msalApp.getAccountByUsername(auth.email);

                return msalApp.acquireTokenPopup(tokenRequest)
                    .then(handleResponse)
                    .catch(err => {
                        console.error(err);
                    });
            } else {
                console.warn(error);
            }
        });
    }

    const signIn = async (redirect) => {
        if (redirect) {
            return msalApp.loginRedirect(loginRequest);
        }

        return msalApp.loginPopup(loginRequest)
            .then(handleResponse)
            .catch(err => {
                console.log(err)
            });
    }

    const signOut = async () => {
        const logoutRequest = {
            account: msalApp.getAccountByUsername(auth.username),
        };
        set_token(
            "",
            "",
            "",
            false
        );

        return (await msalApp.logoutRedirect(logoutRequest));
    }
    axios.defaults.headers.common['FACULTY_APR_AUTH_ID'] = localStorage.getItem('FACULTY_APR_AUTH_ID') || "";

    return (
        
        <Navbar sticky="top" className="Navigation" expand="lg">
            <Navbar.Brand href="/"> <h2>SU College of Science And Engineering</h2></Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
        </Navbar.Collapse>
        {auth.isAuthenticated 
            ?   <Button
                    className="mt-2 ml-2" 
                    variant="info" 
                    onClick={() => signOut()}>
                    Log Out {auth.name}
                </Button> 
            :   <Button 
                    className="mt-2 ml-2"
                    variant="info" 
                    onClick={() => signIn()}>
                    Log In
                </Button>
        }
         <Button
            className="mt-2 ml-2" 
            variant="info" 
            onClick={() => history.push("/Login")}>
            Profiles
        </Button>
        </Navbar>
    )
}

const mapStateToProps = (state) => 
{
    return {auth: state.auth}
}

export default connect(mapStateToProps, {set_token})(NavigationBar);