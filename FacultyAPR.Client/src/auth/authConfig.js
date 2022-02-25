// For a full list of MSAL.js configuration parameters, 
// visit https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/configuration.md
export const msalConfig = {
    auth: {
        clientId: "eb27650f-d428-45e9-87cc-c93a116ae9dc",
        authority: "https://login.microsoftonline.com/redhawks.onmicrosoft.com",
        redirectUri: "https://localhost:3000"
    },
    cache: {
        cacheLocation: "localStorage", // This configures where your cache will be stored
        storeAuthStateInCookie: false // Set this to "true" if you are having issues on IE11 or Edge
    },
}

// Coordinates and required scopes for your web API
export const apiConfig = {
    resourceUri: "https://localhost:44351/api/profile",
    resourceScopes: ["api://27226534-0bd7-4dba-b895-040efd8f8602/.default"]
}

/** 
 * Scopes you enter here will be consented once you authenticate. For a full list of available authentication parameters, 
 * visit https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/configuration.md
 */
export const loginRequest = {
    scopes: ["openid", "profile", "offline_access", ...apiConfig.resourceScopes]
}

// Add here scopes for access token to be used at the API endpoints.
export const tokenRequest = {
    scopes: [...apiConfig.resourceScopes]
}

// Add here scopes for silent token request
export const silentRequest = {
    scopes: ["openid", "profile", ...apiConfig.resourceScopes]
}
