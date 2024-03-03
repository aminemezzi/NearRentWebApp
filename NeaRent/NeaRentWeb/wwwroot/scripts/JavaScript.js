async function GetCoords() {
    const pos = await new Promise((resolve, reject) => {
        navigator.geolocation.getCurrentPosition(resolve, reject);
    });
    return [pos.coords.longitude, pos.coords.latitude];
};

async function StartLocationAnimation() {
    var element1 = document.getElementById("location-grid");
    var element2 = document.getElementById("location-button");

    element1.classList.add("animation-grid");
    element2.classList.add("animation-button");
}

let authenticationStateProviderInstance = null;

function GoogleInitialize(clientId, authenticationStateProvider) {
    // disable Exponential cool-down
    /*document.cookie = `g_state=;path=/;expires=Thu, 01 Jan 1970 00:00:01 GMT`;*/
    authenticationStateProviderInstance = authenticationStateProvider;
    google.accounts.id.initialize({ client_id: clientId, callback: googleCallback });
    GooglePrompt();
//    google.accounts.id.prompt();
}

function GooglePrompt() {
    google.accounts.id.prompt((notification) => {
        if (notification.isNotDisplayed() || notification.isSkippedMoment()) {
            document.cookie = `g_state=;path=/;expires=Thu, 01 Jan 1970 00:00:01 GMT`;
            google.accounts.id.prompt()
        }
    });
}

function googleCallback(googleResponse) {
    authenticationStateProviderInstance.invokeMethodAsync("GoogleLogin", { ClientId: googleResponse.clientId, SelectedBy: googleResponse.select_by, Credential: googleResponse.credential });
}