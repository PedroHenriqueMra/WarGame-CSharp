export class LocalContext {
    #roomId
    #localPlayerId
    constructor (roomIdd) {
        this.#roomId = roomIdd;
        this.#localPlayerId = null;
        this.playerInput = {
            left: false,
            right: false,
            wantJump: false,
        };
    }

    getRoomId () {
        return this.#roomId;
    }

    getLocalPlayerId () {
        return this.#localPlayerId;
    }

    setLocalPlayerId (playerId) {
        this.#localPlayerId = playerId;
    }

    recoveryLocalData() {
        if (this.#localPlayerId == null)
            this.#localPlayerId = Number(localStorage.getItem("playerId") ?? 0);

        if (this.#roomId == null)
            this.#roomId = localStorage.getItem("roomId");
    }

    saveLocalData () {
        localStorage.setItem("roomId", this.#roomId);
        localStorage.setItem("playerId", this.#localPlayerId);
    }
}