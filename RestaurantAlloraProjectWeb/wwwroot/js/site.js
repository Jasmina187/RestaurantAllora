let pendingConfirmElement = null;

if (window.jQuery && window.jQuery.validator) {
    window.jQuery.extend(window.jQuery.validator.messages, {
        required: "Това поле е задължително.",
        remote: "Моля, поправете това поле.",
        email: "Моля, въведете валиден имейл адрес.",
        url: "Моля, въведете валиден URL адрес.",
        date: "Моля, въведете валидна дата.",
        dateISO: "Моля, въведете валидна дата.",
        number: "Моля, въведете валидно число.",
        digits: "Моля, въведете само цифри.",
        equalTo: "Моля, въведете същата стойност отново.",
        maxlength: window.jQuery.validator.format("Моля, въведете най-много {0} символа."),
        minlength: window.jQuery.validator.format("Моля, въведете поне {0} символа."),
        rangelength: window.jQuery.validator.format("Моля, въведете стойност между {0} и {1} символа."),
        range: window.jQuery.validator.format("Моля, въведете стойност между {0} и {1}."),
        max: window.jQuery.validator.format("Моля, въведете стойност, по-малка или равна на {0}."),
        min: window.jQuery.validator.format("Моля, въведете стойност, по-голяма или равна на {0}.")
    });
}

function getConfirmModalElements() {
    const modalElement = document.getElementById("alloraConfirmModal");
    const messageElement = document.getElementById("alloraConfirmMessage");
    const acceptButton = document.getElementById("alloraConfirmAccept");

    if (!modalElement || !messageElement || !acceptButton || !window.bootstrap) {
        return null;
    }

    return {
        modal: window.bootstrap.Modal.getOrCreateInstance(modalElement),
        messageElement,
        acceptButton
    };
}

function continueConfirmedAction(element) {
    if (!element) {
        return;
    }

    element.dataset.confirmAccepted = "true";

    if (element instanceof HTMLAnchorElement && element.href) {
        window.location.href = element.href;
        return;
    }

    const form = element.closest("form");
    const canSubmitForm = form && (element instanceof HTMLButtonElement || element instanceof HTMLInputElement);

    if (canSubmitForm) {
        if (typeof form.requestSubmit === "function") {
            form.requestSubmit(element);
        } else {
            form.submit();
        }

        return;
    }

    element.click();
}

function showConfirmModal(confirmElement, message) {
    const modalParts = getConfirmModalElements();

    if (!modalParts) {
        return false;
    }

    pendingConfirmElement = confirmElement;
    modalParts.messageElement.textContent = message;
    modalParts.modal.show();
    return true;
}

document.addEventListener("click", function (event) {
    const confirmElement = event.target.closest("[data-confirm]");

    if (!confirmElement) {
        return;
    }

    if (confirmElement.dataset.confirmAccepted === "true") {
        delete confirmElement.dataset.confirmAccepted;
        return;
    }

    const message = confirmElement.getAttribute("data-confirm");

    if (!message) {
        return;
    }

    if (!showConfirmModal(confirmElement, message)) {
        return;
    }

    event.preventDefault();
    event.stopPropagation();
});

document.addEventListener("submit", function (event) {
    const submitter = event.submitter;

    if (!submitter || !submitter.matches("[data-confirm]")) {
        return;
    }

    if (submitter.dataset.confirmAccepted === "true") {
        delete submitter.dataset.confirmAccepted;
        return;
    }

    const message = submitter.getAttribute("data-confirm");

    if (!message || !showConfirmModal(submitter, message)) {
        return;
    }

    event.preventDefault();
});

document.addEventListener("DOMContentLoaded", function () {
    const modalParts = getConfirmModalElements();

    if (!modalParts) {
        return;
    }

    modalParts.acceptButton.addEventListener("click", function () {
        const element = pendingConfirmElement;
        pendingConfirmElement = null;
        modalParts.modal.hide();
        continueConfirmedAction(element);
    });
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

document.addEventListener("click", async function (event) {
    const copyButton = event.target.closest("[data-copy-value]");

    if (!copyButton) {
        return;
    }

    const value = copyButton.getAttribute("data-copy-value");

    if (!value) {
        return;
    }

    const originalText = copyButton.textContent;
    const successText = copyButton.getAttribute("data-copy-success") || "Копирано";

    try {
        await navigator.clipboard.writeText(value);
        copyButton.textContent = successText;
    } catch {
        copyButton.textContent = value;
    }

    window.setTimeout(function () {
        copyButton.textContent = originalText;
    }, 1800);
});

document.addEventListener("DOMContentLoaded", function () {
    if (!window.bootstrap) {
        return;
    }

    document.querySelectorAll(".allora-toast").forEach(toastElement => {
        window.bootstrap.Toast.getOrCreateInstance(toastElement, {
            delay: 4200
        }).show();
    });
});
