import { ctx } from "../main.js";

export function drawPlayer(x, y) {
    ctx.beginPath();
    ctx.fillStyle = "blue";
    ctx.fillRect(x, y, 50, 50);
    ctx.closePath();
}

//export function drawWorld () {
//    
//}

export function clear(width, hight) {
    ctx.clearRect(0, 0, width, hight);
}
