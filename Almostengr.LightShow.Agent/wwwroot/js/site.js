const modalContenlElement = document.getElementById("modalContent");
const modalElement = new bootstrap.Modal(document.getElementById("modal"), {
    keyboard: false,
});

async function loadAndShowModalAsync(url) {
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(response);
        }

        modalContenlElement.innerHTML = response.body;
        modalElement.show();
    }
    catch (error) {
        alert("Unable to display form.");
        console.error(error.message);
    }
}

function addModalListeners() {
    const modalLinks = document.getElementsByClassName("modalLink");

    modalLinks.forEach(link => {
        addEventListener("click", async function () {
            const linkUrl = link.getAttribute("data-url");
            await loadAndShowModalAsync(linkUrl);
        });
    });
}

async function submitModalAsync(submitButton) {
    const formElement = document.getElementById("modalForm");
    const formMethod = formElement.getAttribute("method");
    const formAction = formElement.getAttribute("action");

    const response = await fetch(
        formAction, {
        method: formMethod
    }
    );

    if (!response.ok) {

    }

    if (response.body === "") {
        window.location.reload();
        return;
    }

    modalContenlElement.innerHTML = response.body;
}


addModalListeners();