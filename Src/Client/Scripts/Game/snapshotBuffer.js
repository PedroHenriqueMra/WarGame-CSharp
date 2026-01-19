export class SnapshotBuffer {
    constructor() {
        this.buffer = []
        
        this.MAX_SIZE = 10
        this.MIN_SIZE = 3

        this.SNAPSHOT_INTERVAL = 10 / 60
        this.INTERPOLATION_DELAY = this.SNAPSHOT_INTERVAL * 2
    }

    addSnapshot (snapshot) {
        snapshot.time = snapshot.Tick / 60;
        this.buffer.push(snapshot);

        if (this.buffer.length >= this.MAX_SIZE)
            this.buffer.shift();
    }

    getPairInterpolation (renderTime) {
        if (this.buffer.length < this.MIN_SIZE)
            return null;
        
        if (renderTime < this.buffer[0].time)
            return null;

        for (var i = 0;i < this.buffer.length;i++) {

            const a = this.buffer[i];
            const b = this.buffer[i+1];
            if (!b)
                continue;
    
            if (a.time <= renderTime && renderTime <= b.time) 
                return { p0: a, p1: b };

        }

        return null;
    }
}
