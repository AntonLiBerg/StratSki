const Util = {
    ready: (callback) => {
        function onReadyFunc() {
            Util.baseUrl = window.location.origin;
            callback();
        }
        if (document.readyState != "loading") onReadyFunc();
        else document.addEventListener("DOMContentLoaded", onReadyFunc());
    },
    makeError: function (e) {
        if (typeof e !== 'object')
            return new Error(e);
        if (e.cause)
            return new Error(e.cause);
        if (e.status) {
            switch (e.status) {
                case "404":
                    return new Error("The request took too long!");
                default:
                    return new Error("Something went wrong!");
            }
        }
        return new Error("Something went wrong!");
    }
}