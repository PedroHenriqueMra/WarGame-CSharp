export function lerp (prev, next, alpha) {
    return prev + (next - prev) * alpha
}

export function moveTowards (current, target, maxDistnaceDelta) {
    if (Math.abs(target - current) <= maxDistnaceDelta)
        return target;

    return current + Math.sign(target - current) * maxDistnaceDelta;
}
