const WS_URL = "ws://localhost:5115/ws";
const socket = new WebSocket(WS_URL);

const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const roomId = Number(urlParams.get('roomId'));

if (socket.readyState === WebSocket.CONNECTING) {
    console.log("Connecting...");
}

socket.addEventListener("open", async _ => {
    console.log("connection was opened");

    sendHandshake();
})

socket.addEventListener("close", _ => {
    log("connection was closed");
})

function receiveHandshake(snapshot) {
    if (snapshot.type === "handshake") {
        if (snapshot.Payload.State === "success") {
            console.log("handshake was successful");
        }
        else {
            console.log("handshake was not successful");
            window.location.href = "http://localhost:5115/";
        }
    }
}

socket.addEventListener("message", (snapshot) => {
    var json = JSON.parse(snapshot.data);

    receiveHandshake(json)
    
    console.log(`data received: ${json}`);
})


// DEBUG
document.addEventListener('click', button => {
    if (button.target.id === 'start') {
        onStartGame(Number(document.getElementById('startid').value));
    }

    if (button.target.id === 'stop') {
        onStopGame(Number(document.getElementById('stopid').value));
    }
});

function sendHandshake() {
    socket.send(JSON.stringify({
        "type": "handshake",
        "Payload": {
            "RoomId": roomId
        }
    }));
}

function onStartGame(id) {
    socket.send(JSON.stringify({
        "type": "startgame",
        "Payload": {
            "roomid": id
        }
    }));
}

function onStopGame(id) {
    socket.send(JSON.stringify({
        "type": "stopgame",
        "Payload": {
            "roomid": id
        }
    }));
}
