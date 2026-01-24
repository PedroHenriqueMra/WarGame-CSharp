import { moveTowards } from "../Utils/physicalFormulas.js";
import { game } from "../main.js";

export function simulateTick(player, input, dt) {
    applyInput(player, input);

    applyHorizontalPhysics(player, dt);
    applyVerticalPhysics(player, dt);

    return player;
}

function applyInput(player, input) {
    player.Dir = input.direction;

    if (player.IsGrounded && input.wantJump) {
        player.VelocityY = player.PlayerAttributes.JumpForce;
        player.IsGrounded = false;
    }
}

function applyHorizontalPhysics(player, dt) {
    const speed = player.PlayerAttributes.Speed;

    const ACCELERATION_DIVISOR = 2.5;
    const DECELERATION_DIVISOR = 1.0;

    const acceleration = speed / ACCELERATION_DIVISOR;
    const deceleration = speed / DECELERATION_DIVISOR;

    var deltaV = 0;

    // Stopped. Decelerate to zero
    if (player.Dir === 0) {

        deltaV = -Math.sign(player.VelocityX) * deceleration * dt;

        if (Math.abs(player.VelocityX) <= deceleration * dt)
            deltaV = -player.VelocityX;
    }
    // Change direction. Decelerate before acelerate
    else if (player.VelocityX !== 0 && Math.sign(player.VelocityX) !== player.Dir) {

        deltaV = -Math.sign(player.VelocityX) * deceleration * dt;
        
        if (Math.abs(player.VelocityX) <= deceleration * dt)
            deltaV = -player.VelocityX;
    }
    // Normal aceleration
    else {
        deltaV = player.Dir * acceleration * dt;
    }
    
    player.VelocityX += deltaV;

    // Clamp velocity
    player.VelocityX = Math.max( -speed, Math.min(player.VelocityX, speed) );
    
    const nextX = player.X + player.VelocityX * dt;
    // Clmap position
    player.X = Math.max( 0, Math.min(nextX, 500) ); // <-- after add map width here

    console.log(`Velocidade: ${player.VelocityX}`)
}

function applyVerticalPhysics(player, dt) {
    player.VelocityY += game.GamePhysics.Gravity * dt;

    const nextY = player.Y + player.VelocityY * dt;

    resolveVerticalCollision(player, nextY);
}

function resolveVerticalCollision(player, nextY) {
    // Ground
    if (nextY <= 0) {
        player.Y = 0;
        player.VelocityY = 0;
        player.IsGrounded = true;
        return;
    }

    // Roof
    //if (nextY >= game.Map.Height) {
    //    player.Y = game.Map.Height;
    //    player.VelocityY = 0;
    //    player.IsGrounded = false;
    //    return;
    //}

    // Air
    player.Y = nextY;
    player.IsGrounded = false;
}
