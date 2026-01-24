import { simulateTick } from "../ClientContext/prediction.js";
import { game, localContext } from "../main.js";
import { sendInput } from "../Input/gameplayInput.js";
import { inputBuffer } from "../main.js";

const DT = 1 / 60;

var acumulatedTime = 0;
var lastInputSnapshot = null;

export function handlerPrediction (delta) {

    if (!localContext.hasPlayer())
        return;

    acumulatedTime += delta;
    
    if (acumulatedTime > DT * 5)
        acumulatedTime = DT * 5;

    while (acumulatedTime >= DT) {
        var tick = localContext.clientTick;

        var inputSnapshot = {
            tick: tick,
            direction: localContext.intentions.direction,
            wantJump: localContext.intentions.wantJump
        }
        
        inputBuffer.addInput(tick, inputSnapshot);
        
        // only sent if input changed
        if (!lastInputSnapshot || !isInputEqual(lastInputSnapshot, inputSnapshot))
            sendInput(inputSnapshot);

        lastInputSnapshot = {
            tick: inputSnapshot.tick,
            direction: inputSnapshot.direction,
            wantJump: inputSnapshot.wantJump
        }

        simulateTick(localContext.state, inputSnapshot, DT);

        localContext.clearIntentions();

        localContext.clientTick += 1;
        acumulatedTime -= DT;
    }
 
    return acumulatedTime / DT;
}

function isInputEqual (inpA, inpB) {
    if (inpA.direction !== inpB.direction)
        return false;
    if (inpA.wantJump !== inpB.wantJump)
        return false;

    return true;
}
