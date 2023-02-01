let elements = {
    message: document.querySelector("#message"),
    username: document.querySelector("#username"),
    password: document.querySelector("#password"),
    passwordConfirm: document.querySelector("#password-confirm"),
    logo: document.querySelector("#logo")
}

timesLogoClicked = 0;

logo.addEventListener(() => {
    timesLogoClicked++;

    if (timesLogoClicked == 50) {
        logo.setAttribute("src", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi5.walmartimages.com%2Fasr%2Ff9d58c4e-436d-44fa-a0ae-4c06af347d9a_1.78bce77e95603ad61c7f098b1025f79f.jpeg&f=1&nofb=1&ipt=01c3a1d2aa37ca215be50fd16e1ae02e4a1aedcf9f00a634c9d54abf5fc648c8&ipo=images");
    }
});

function reset() {

}

function createUser() {

}

function login() {
    checkDetails();
}

function checkDetails() {
    let username = elements.username.value;
    let password = elements.password.value;
    let passwordConfirm = elements.passwordConfirm.value;

    try {
        presenceCheck(username, "Username cannot be empty");
        presenceCheck(password, "Password cannot be empty");
        lengthCheck(password, 4, 12, "Password must be between 4 and 12 characters");
        if (password != passwordConfirm) { throw "Passwords must be the same" };
        elements.message.innerText = "Authenticating";
    }

    catch (e) {
        elements.message.innerText = e;
    }
}

function presenceCheck(input, error) {
    if (input.length == 0) {
        throw error;
    }
}

function lengthCheck(input, min, max, error) {
    if (input.length > max || input.length < min) {
        throw error;
    }
}