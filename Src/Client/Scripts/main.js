import { onHandShakeAsync, roomId  } from "./Network/websocket.js";
import { LocalContext } from "./ClientContext/localContext.js";
import { GameState } from "./Game/gameState.js";
import { gameLoop } from "./Game/gameLoop.js";
import { SnapshotBuffer } from "./Game/snapshotBuffer.js";

// instances
export const startButton = document.getElementById("start");
export const stopButton = document.getElementById("stop");

export const localContext = new LocalContext(roomId);
if (localContext.getLocalPlayerId() == null)
    localContext.recoveryLocalData();

export const game = new GameState(roomId);

export const canvas = document.getElementById("canvas");
export const ctx = canvas.getContext("2d");

export const snapshotBuffer = new SnapshotBuffer();

onHandShakeAsync();

requestAnimationFrame(gameLoop);
