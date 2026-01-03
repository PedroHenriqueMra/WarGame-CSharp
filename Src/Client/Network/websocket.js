import { log } from "../test/testFuncs.js";

const WS_URL = "ws://localhost:5115/ws";
const socket = new WebSocket(WS_URL);

if (socket.readyState === WebSocket.CONNECTING) {
    log("Connecting...");
}

socket.addEventListener("open", _ => {
    log("connection was opened");
})
socket.addEventListener("close", _ => {
    log("connection was closed");
})

socket.addEventListener("message", (snapshot) => {
    var json = JSON.parse(snapshot.data);
    console.log(json);
    log(`data received: ${json}`);
    
    const log = document.getElementById('log');
    var height = log.scrollHeight;
    log.scrollTop = height;
})


// DEBUG
document.addEventListener('click', button => {
    if (button.target.id === 'create') {
        onCreateRoom(document.getElementById('createName').value);
    }

    if (button.target.id === 'join') {
        onJoinRoom(Number(document.getElementById('joinid').value));
    }

    if (button.target.id === 'leave') {
        onLeaveRoom(Number(document.getElementById('leaveid').value));
    }

    if (button.target.id === 'start') {
        onStartGame(Number(document.getElementById('startid').value));
    }

    if (button.target.id === 'stop') {
        onStopGame(Number(document.getElementById('stopid').value));
    }
});

function onCreateRoom(name) {
    socket.send(JSON.stringify({
        "type": "CreateRoom",
        "Payload": {
            "roomname": name
        }
    }));
}
function onJoinRoom(id) {
    socket.send(JSON.stringify({
        "type": "joinroom",
        "Payload": {
            "roomid": id
        }
    }));
}
function onLeaveRoom(id) {
    socket.send(JSON.stringify({
        "type": "leaveroom",
        "Payload": {
            "roomid": id
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

