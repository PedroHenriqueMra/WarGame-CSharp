import { game, localContext } from '../main.js';
import { registerHandler, getHandler } from './router.js';
import { snapshotBuffer } from '../main.js';

export function handleSnapshot (json) {
    const handler = getHandler(json.Type);
    if (handler) {
        handler(json.Payload);
    }
}

function handleSystemInfo (payload) {
    console.log(`system.info: ${payload.Message} - ${payload.Code}`);

    if (payload.Code.includes("HANDSHAKE") && payload.Code !== "HANDSHAKE_SUCCESS") {
        alert(payload.Message);
        window.location.href = "/room";
        return;
    } 

    alert("Success Handshake");
}

function handleGameInfoSnapshot (payload) {
    console.log(`game.info: ${payload.Message} - ${payload.Code}`);
    if (payload.Content) {
        localContext.setLocalPlayerId(payload.Content.PlayerId);
        localContext.saveLocalData();
    }
}

function handleGameplaySnapshot (payload) {
    console.log(payload);
    snapshotBuffer.addSnapshot(payload);
    
    game.applySnapshot(payload); // <- Reconciliation (server authoritative)
}

registerHandler("system.info", handleSystemInfo);
registerHandler("game.info", handleGameInfoSnapshot);
registerHandler("game.snapshot", handleGameplaySnapshot);
