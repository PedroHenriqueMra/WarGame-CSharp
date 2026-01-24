import { render } from "../main.js";
import { snapshotBuffer } from "../main.js";
import { handlerPrediction } from "./handlerPrediction.js";
import { game, localContext } from "../main.js";

import { clamp } from "../Utils/clamp.js";

// logicTime answers: In which tick am i now?
var renderTime = null;
var serverTime = null;
var lastNow = performance.now();

export function gameLoop () {

    if (!game.IsRunning || !localContext.state) {
        requestAnimationFrame(gameLoop);
        return;
    }

    const now = performance.now();
    const delta = (now - lastNow) / 1000;
    lastNow = now;
    
    // prediction (local player)
    handlerPrediction(delta);

    const current = localContext.state;

    render.noInterpolation.logicalPosition.update( current.X, current.Y );

    // interpolation (remote players)
    if (snapshotBuffer.buffer.length >= snapshotBuffer.MIN_SIZE) {
        serverTime = snapshotBuffer.buffer.at(-1).time;

        if (!renderTime || renderTime < snapshotBuffer.buffer[0].time) {
            renderTime = serverTime - snapshotBuffer.INTERPOLATION_DELAY;
        } else {
            const targetTime = serverTime - snapshotBuffer.INTERPOLATION_DELAY;
            const error = targetTime - renderTime;
            const timeCorrection = clamp(error * 0.1, -0.5, 0.5);

            renderTime += delta * (1 + timeCorrection);
        }
    }

    const pairInterpolation = snapshotBuffer.getPairInterpolation(renderTime);
    
    if (!pairInterpolation) {
        requestAnimationFrame(gameLoop);
        return;
    }
    
    var { p0, p1 } = pairInterpolation;

    const alpha = (renderTime - p0.time) / (p1.time - p0.time);

    render.interpolation.player.update( p0, p1, alpha);

    render.render(delta);

    requestAnimationFrame(gameLoop);
}
