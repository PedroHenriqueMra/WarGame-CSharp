import { send } from "../Network/websocket.js";
import { roomId } from "../Network/websocket.js";

import { startButton, stopButton } from "../main.js";

document.addEventListener("click", event => {
    if (event.target === startButton) send("StartGame", {roomId});
    if (event.target === stopButton) send("StopGame", {roomId});
})
