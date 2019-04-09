function onSignIn(googleUser) {
    var signOutButton = $(".g-signin2").get(0);
    if (typeof signOutButton === 'undefined')
        return;
    signOutButton.classList.remove("g-signin2");
    $(signOutButton).click(function (event) {
        event.stopPropagation();
        event.preventDefault();
        var auth2 = gapi.auth2.getAuthInstance();
        if (typeof auth2 !== 'undefined') {
            auth2.signOut().then(function () {
            });
            auth2.disconnect();
        }
    });
}
$(document).ready(function () {
});
//# sourceMappingURL=googler.js.map