import { send } from "../Network/websocket.js";
import { roomId } from "../Network/websocket.js";

import { startButton, stopButton } from "../main.js";

document.addEventListener("click", event => {
    if (event.target === startButton) send(JSON.stringify({ type: "StartGame", payload: { roomId } }));
    if (event.target === stopButton) send(JSON.stringify({ type: "StopGame", payload: { roomId } }));
})
