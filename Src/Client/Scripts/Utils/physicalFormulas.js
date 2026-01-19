export function lerp (prev, next, alpha) {
    return prev + (next - prev) * alpha
}

export function moveTowards (current, target, maxDelta) {
    if (Math.round(target - current) <= maxDelta)
        return target;

    return current + Math.sign(target - current);
}
