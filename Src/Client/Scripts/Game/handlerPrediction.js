import { prediction } from "../ClientContext/prediction.js";
import { game, localContext } from "../main.js";

const DT = 1 / 60;

var acumulatedTime = 0;
var lastTime = performance.now();

export function handlerPrediction () {
    acumulatedTime += (performance.now() - lastTime) / 1000; // convert unity time from MS to S
    
    if (acumulatedTime > DT * 5)
        acumulatedTime = DT * 5;

    lastTime = performance.now();

    while (acumulatedTime >= DT) {
        prediction(game, localContext, DT);
        acumulatedTime -= DT;
    }
}
