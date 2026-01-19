export class GameState {
    constructor () {
        this.Tick = 0;
        this.IsRunning = false;
        this.Players = [];
        this.World = null;
        this.GamePhysics = {
            Gravity: -5.8,
        }
    }

    applySnapshot (snapshot) {
        Object.assign(this, snapshot);
    }
}
