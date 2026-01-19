import { send } from "../Network/websocket.js";
import { localContext } from "../main.js";

document.addEventListener("keydown", event => {
    var key = event.key;

    console.log(key);
    
    if (key === " " || key === "BackSpace") localContext.playerInput.wantJump = true;
    if (key === "a" || key === "ArrowLeft") localContext.playerInput.left = true;
    if (key === "d" || key === "ArrowRight") localContext.playerInput.right = true;
    sendGameplayInput();
})

document.addEventListener("keyup", event => {
    var key = event.key;
    
    if (key === " " || key === "BackSpace") localContext.playerInput.wantJump = false;
    if (key === "a" || key === "ArrowLeft") localContext.playerInput.left = false;
    if (key === "d" || key === "ArrowRight") localContext.playerInput.right = false;
    sendGameplayInput();
})

function sendGameplayInput () {
    var inputList = getInputs();
    
    inputList.forEach(input => {
        send(input.type, input.payload);
    });
}

function getInputs () {
    var inputs = [];

    if (localContext.playerInput.wantJump) inputs.push({type: "jump", payload: {}});
    
    inputs.push({type: "move", payload: getMoveInput()});

    return inputs;
}

function getMoveInput () {
    if (!localContext.playerInput.left && !localContext.playerInput.right) return { direction: 0 };

    if (localContext.playerInput.left && !localContext.playerInput.right) return { direction: -1 };
    if (!localContext.playerInput.left && localContext.playerInput.right) return { direction: 1 };
}
