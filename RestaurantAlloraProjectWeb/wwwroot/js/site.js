
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
