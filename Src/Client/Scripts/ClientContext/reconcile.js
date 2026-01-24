import { localContext } from "../main.js";
import { inputBuffer } from "../main.js";
import { simulateTick } from "./prediction.js";

const DT = 1 / 60;
var lastProcessedTick = 0;

export function reconcile(payload) {
    
    localContext.justHadReconcile = true;

    var serverPlayer = payload.Players.find(
        p => p.PlayerId === localContext.getLocalPlayerId()
    );
    if (!serverPlayer)
        return;
    
    // Reconcile (Server authoritative):
    localContext.state = structuredClone(serverPlayer);
    
    var lastReceivedInputTick = payload.LastReceivedInputTick;
    
    if (!lastReceivedInputTick)
        return;
    
    if (lastReceivedInputTick > lastProcessedTick) {
        lastProcessedTick = lastReceivedInputTick;

        inputBuffer.removeUpTo(lastReceivedInputTick);
    
        localContext.clientTick = lastReceivedInputTick + 1;

        for (const entry of inputBuffer.buffer) {
    
            simulateTick(localContext.state, entry.input, DT);
            localContext.clientTick += 1;
        }
    }
}
