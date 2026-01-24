// Translate client input to server input
export function createInput(localInput) {

    return translateInput(localInput);
    
}

function translateInput (localInput) {

    var inputs = []

    if (localInput.wantJump) 
        inputs.push({type: "jump", payload: {
            inputTick: localInput.tick
        }});
    
    inputs.push({ type: "move", payload: {
        direction: localInput.direction, 
        inputTick: localInput.tick 
    }});

    return inputs;
}
