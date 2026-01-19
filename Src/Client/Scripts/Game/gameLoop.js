import { InterpolatedRender } from "../Render/render.js";;
import { snapshotBuffer } from "../main.js";
import { handlerPrediction } from "./handlerPrediction.js";

import { clamp } from "../Utils/clamp.js";

// logicTime answers: In which tick am i now?
var renderTime = null;
var serverTime = null;
var lastNow = performance.now();

export function gameLoop () {

    const now = performance.now();
    const delta = (now - lastNow) / 1000;
    lastNow = now;

    handlerPrediction();

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
    
    const { p0, p1 } = pairInterpolation;

    const alpha = (renderTime - p0.time) / (p1.time - p0.time);

    InterpolatedRender( p0, p1, alpha);

    requestAnimationFrame(gameLoop);
}
