import { send } from "../Network/websocket.js";
import { localContext } from "../main.js";
import { createInput } from "./inputFactory.js";

document.addEventListener("keydown", event => {
    var key = event.key;

    console.log(key);
    
    if (key === " ") localContext.intentions.wantJump = true;
    if (key === "a" || key === "ArrowLeft") localContext.intentions.direction = -1;
    if (key === "d" || key === "ArrowRight") localContext.intentions.direction = 1;
})

document.addEventListener("keyup", event => {
    var key = event.key;
    
    if (key === "a" || key === "ArrowLeft") localContext.intentions.direction = 0;
    if (key === "d" || key === "ArrowRight") localContext.intentions.direction = 0;
})

export function sendInput (localInput) {

    var inputData = createInput(localInput);
    
    for (var inp of inputData) {
        send(JSON.stringify({ type: inp.type, payload: inp.payload }));
    }
}
