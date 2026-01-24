import { game, localContext } from '../main.js';
import { registerHandler, getHandler } from './router.js';
import { snapshotBuffer } from '../main.js';
import { reconcile } from "../ClientContext/reconcile.js";

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

    var payloadLocalPlayer = structuredClone(payload);
    var payloadGame = structuredClone(payload);
    
    reconcile(payloadLocalPlayer);
    
    payloadGame.Players = payload.Players.filter(
        p => p.PlayerId != localContext.getLocalPlayerId()
    ) ?? [];
    
    snapshotBuffer.addSnapshot(payloadGame);
    game.applySnapshot(payloadGame);

}

registerHandler("system.info", handleSystemInfo);
registerHandler("game.info", handleGameInfoSnapshot);
registerHandler("game.snapshot", handleGameplaySnapshot);
