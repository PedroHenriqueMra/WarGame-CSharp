import { handleSnapshot } from "./handlers.js";

const WS_URL = "ws://localhost:5115/ws";
export const socket = new WebSocket(WS_URL);

const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);

export const roomId = urlParams.get('roomId');

if (socket.readyState === WebSocket.CONNECTING) {
    console.log("Connecting...");
}

socket.addEventListener("open", async _ => {
    console.log("connection was opened");
})

socket.addEventListener("close", _ => {
    console.log("connection was closed");
})

socket.addEventListener("message", (snapshot) => {
    var json = JSON.parse(snapshot.data);
    console.log(`data received ${json}`);
    
    handleSnapshot(json);
})

export function send(input) {
    console.log(`sending data: ${input.payload}`);

    socket.send(input);
}

export function onHandShakeAsync () {
    waitForSocketConnection();
}

function waitForSocketConnection () {
    setTimeout(_ => {
        if (socket.readyState === WebSocket.OPEN) {
            send(JSON.stringify({ type: "handshake", payload: { roomId } }));
        } else {
            waitForSocketConnection();
        }
    })
}
