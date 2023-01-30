let elements = {
    message: document.querySelector("#message"),
    username: document.querySelector("#username"),
    password: document.querySelector("#password")
}

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

    try {
        presenceCheck(username, "Username cannot be empty");
        presenceCheck(password, "Password cannot be empty");
        lengthCheck(password, 4, 12, "Password must be between 4 and 12 characters");
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