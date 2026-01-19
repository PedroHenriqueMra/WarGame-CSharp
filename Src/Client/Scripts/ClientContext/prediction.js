import { moveTowards } from "../Utils/physicalFormulas.js";

export function prediction (gameState, localContext, dt) {
    var player;
    gameState.Players.forEach(p => {
        if (p.PlayerId == localContext.getLocalPlayerId()) {
            player = p;
            return;
        }
    });

    if (!player) return;

    predictMove(gameState, player, dt);

    predictJump(gameState, player, dt);
}

function predictMove (gameState, player, dt) {
    // aceleration
    var targetVell = player.Dir * player.PlayerAttributes.Speed;
    var acell = player.PlayerAttributes.Speed / 2.5;

    var newVell = moveTowards(player.VelocityX, targetVell, acell * dt);
    player.VelocityX = newVell

    player.X += player.VelocityX * dt;

    // deceleration
    
}

function predictJump (gameState, player, dt) {
    if (player.Y == 0) 
        return;

    player.VelocityY += gameState.GamePhysics.Gravity * dt;

    player.Y += player.VelocityY * dt;
}
