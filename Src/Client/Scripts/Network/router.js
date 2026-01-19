var routeHandlers = {}

export function registerHandler (type, handler) {
    routeHandlers[type] = handler;
}

export function getHandler (type) {
    return routeHandlers[type]
}
