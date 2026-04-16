
document.addEventListener("click", function (event) {
    const confirmElement = event.target.closest("[data-confirm]");

    if (!confirmElement) {
        return;
    }

    const message = confirmElement.getAttribute("data-confirm");

    if (message && !window.confirm(message)) {
        event.preventDefault();
    }
});

document.addEventListener("change", function (event) {
    const passwordToggle = event.target.closest("[data-toggle-password]");

    if (!passwordToggle) {
        return;
    }

    const selector = passwordToggle.getAttribute("data-toggle-password");
    const inputType = passwordToggle.checked ? "text" : "password";

    document.querySelectorAll(selector).forEach(input => {
        if (input instanceof HTMLInputElement) {
            input.type = inputType;
        }
    });
});
