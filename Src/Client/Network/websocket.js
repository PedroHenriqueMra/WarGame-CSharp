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

socket.addEventListener("message", (snapshot) => {
    var json = JSON.parse(snapshot.data);
    console.log(`data received`);
    
    receiveSystemInfoSnapshot(json)

    receiveGameInfoSnapshot(json)

    receiveGameSnapshot(json)
})

function receiveSystemInfoSnapshot(json) {
    if (json.Type == "system.info") {
        if (json.Payload.Code != "HANDSHAKE_SUCCESS") {
            alert(json.Payload.Message)
            window.location.href = "http://localhost:5115/room";
        } else {
            alert("Success")
        }
    }
}

function receiveGameSnapshot(json) {
    if (json.Type == "game.gameplay") {
        console.log(json.Payload)
    }
}

function receiveGameInfoSnapshot(json) {
    if (json.Type == "game.info") {
        alert(json.Payload.Message)
    }
}


document.addEventListener('click', button => {
    if (button.target.id === 'start') {
        onStartGame(roomId);
    }

    if (button.target.id === 'stop') {
        onStopGame(roomId);
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
