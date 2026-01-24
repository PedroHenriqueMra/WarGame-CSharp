import * as draw from "./draw.js";

import { lerp } from "../Utils/physicalFormulas.js";
import { localContext } from "../main.js";

export class Render {
    constructor (canvas) {
        this.canvas = canvas

        this.interpolation = { 
            player: {
                snapA: [], snapB: [], alpha: null, 
                update: function (a, b, alpha) {
                    this.snapA = a;
                    this.snapB = b;
                    this.alpha = alpha;
                },
                hasSnapshots: function () {
                    return this.snapA.length >= 1 && this.snapB.length >= 1;
                }
            }, 
            map: {

            } 
        }

        this.noInterpolation = {
            renderPosition: {
                position: { X: null, Y: null },
                update: function (x, y) {
                    this.position.X = x;
                    this.position.Y = y;
                    return this.position;
                }
            },
            logicalPosition: {
                position: { X: null, Y: null },
                update : function (x, y) {
                    this.position.X = x;
                    this.position.Y = y;
                    return this.position;
                }
            }
        }
    }

    render (delta) {
        draw.clear(this.canvas.width, this.canvas.height);
        
        this.renderInterpolatated();

        this.renderLocalPlayer(delta);
    }

    renderInterpolatated() {

        const playerInterpoll = this.interpolation.player;
        if (playerInterpoll.hasSnapshots())
            this.#interpolatePlayerPosition();

    }

    renderLocalPlayer (delta) {

        var rp = this.noInterpolation.renderPosition.position;
        const lp = this.noInterpolation.logicalPosition.position;
        
        // Init reder position
        if (rp.X === null || rp.Y === null) {
            rp = this.noInterpolation.renderPosition.update(lp.X, lp.Y);
            draw.drawPlayer(rp.X, rp.Y);

            return;
        }

        // Reconcile
        if (localContext.justHadReconcile) {
            const errorX = Math.abs(lp.X - rp.X);

            const SNAP_THRESHOLD_X = 6;

            // Snap
            if (errorX > SNAP_THRESHOLD_X) {
                rp = this.noInterpolation.renderPosition.update(lp.X, lp.Y);
                draw.drawPlayer(rp.X, rp.Y);

                localContext.justHadReconcile = false;

                return;
            }

            localContext.justHadReconcile = false;
        }

        // smoothing
        const correctionSpeed = 12;
        const alpha = 1 - Math.exp(-correctionSpeed * delta);

        const x = lerp(rp.X, lp.X, alpha);
        const y = lerp(rp.Y, lp.Y, alpha);

        this.noInterpolation.renderPosition.update(x, y);

        draw.drawPlayer(x, y);

    }

    #interpolatePlayerPosition () {
        const snapshots = this.interpolations.player;
        const countSnapshots = Math.min(
            snapshots.snapA.Players.length, snapshots.snapB.Players.length
        );

        for (var i = 0;i < countSnapshots;i++) {

            var newX = lerp(snapshots.snapA.Players[i].X, snapshots.snapB.Players[i].X, snapshots.alpha);

            var newY = lerp(snapshots.snapA.Players[i].Y, snapshots.snapB.Players[i].Y, snapshots.alpha);
        
            draw.drawPlayer(newX, newY);

        }
    }
}
