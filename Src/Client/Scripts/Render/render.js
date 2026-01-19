import { clear, drawPlayer } from "./draw.js";
import { canvas, game } from "../main.js";

import { lerp } from "../Utils/physicalFormulas.js";

export function InterpolatedRender (snpA, snpB, alpha) {
    if (!game.IsRunning) return;

    clear(canvas.width, canvas.height);

    interpolationPlayersPos(snpA, snpB, alpha);

}

export function localPlayerRender (player) {

    drawPlayer(player.X, player.Y);

}

function interpolationPlayersPos (snpA, snpB, alpha) {
    for (var i = 0;i < Math.min(snpA.Players.length, snpB.Players.length);i++) {
        var newX = lerp(snpA.Players[i].X, snpB.Players[i].X, alpha);

        var newY = lerp(snpA.Players[i].Y, snpB.Players[i].Y, alpha);
    
        drawPlayer(newX, newY);
    }
}
