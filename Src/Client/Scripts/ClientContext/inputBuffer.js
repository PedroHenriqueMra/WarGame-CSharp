export class InputBuffer {
    constructor () {
        this.buffer = []
    }

    addInput (tick, input) {
        this.buffer.push({ tick, input });
    }

    removeUpTo (tick) {
        this.buffer = this.buffer.filter(i => i.tick > tick);
    }
}
