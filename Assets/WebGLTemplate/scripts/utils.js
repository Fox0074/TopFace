function unityProgress(unityInstance, progress) {
    if (!unityInstance.Module) return;

    if (!unityInstance.prepreloaderNode) {
        var nodes = document.getElementsByClassName("prepreloader");
        if (nodes.length > 0) {
            unityInstance.prepreloaderNode = nodes[0];
        }
    }

    if (!unityInstance.progressHandleNode) {
        var nodes = document.getElementsByClassName(
            "prepreloader__progress-handle"
        );
        if (nodes.length > 0) {
            unityInstance.progressHandleNode = nodes[0];
        }
    }

    unityInstance.progressHandleNode.style.width =
        Math.floor(progress * 100) + "%";

    if (progress >= 1) {
        unityInstance.prepreloaderNode.style.display = "none";
    }
}

function unityPopup(text, buttons) {
    buttons = buttons || [{ text: "OK" }];

    var popupsContainerNode;

    var query = document.getElementsByClassName("popups-container");
    if (query.length === 0) return;
    popupsContainerNode = query[0];

    var popupNode = document.createElement("div");
    popupNode.className = "popup";
    popupsContainerNode.appendChild(popupNode);

    var popupLabelNode = document.createElement("div");
    popupLabelNode.className = "popup__label";
    popupLabelNode.textContent = text;
    popupNode.appendChild(popupLabelNode);

    var popupButtonsContainerNode = document.createElement("div");
    popupButtonsContainerNode.className = "popup__buttons-container";
    popupLabelNode.appendChild(popupButtonsContainerNode);

    for (var i = 0; i < buttons.length; i++) {
        var button = buttons[i];

        var buttonNode = document.createElement("div");
        buttonNode.className = "popup__button";
        buttonNode.textContent = button.text;
        popupNode.appendChild(buttonNode);

        buttonNode.addEventListener("click", function () {
            if (button.callback) {
                button.callback();
            }

            popupsContainerNode.removeChild(popupNode);
        });
    }
}

window.addEventListener("load", function () {
    var sessionTS = Date.now();

    try {
        localStorage.setItem("session_ts", sessionTS);
    } catch (e) {}

    window.addEventListener("focus", function () {
        var savedSessionTS;

        try {
            var savedSessionTS = parseInt(
                localStorage.getItem("session_ts"),
                10
            );
        } catch (e) {}

        if (savedSessionTS && savedSessionTS > sessionTS) {
            var nodes = document.getElementsByClassName("another-tab-warning");

            if (nodes.length > 0) {
                nodes[0].style.display = "block";
            }
        }
    });
});
