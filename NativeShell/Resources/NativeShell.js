(function () {
    let id = 1;
    const map = new Map();

    function runCode($rid$, $code$, $args$) {
        return function () {
            try {
                let result = ($code$).apply({ clr, evalInPage }, ...$args$);
                if (result && result.then) {
                    result.then((r) => {
                        evalInPage(`window.nativeShell.on($rid$, ${JSON.stringify(r) || 1})`);
                    }, (e) => {
                        evalInPage(`window.nativeShell.on($rid$, void 0, ${JSON.stringify(e.stack || e)})`);
                    });
                } else {
                    setTimeout(() =>
                        evalInPage(`window.nativeShell.on($rid$, ${JSON.stringify(r) || 1})`),
                        1);
                }
            } catch (error) {
                setTimeout(() =>
                    evalInPage(`window.nativeShell.on($rid$, void 0, ${JSON.stringify(error.stack || error)})`),
                    1);
            }
        }
    }

    const scriptTemplate = runCode().toString();

    window.nativeShell = {

        on(rid, result, error) {
            try {
                const { resolve, reject } = map.get(rid);
                if (result) {
                    resolve(result);
                    return;
                }
                if (error) {
                    reject(error);
                }
            } finally {
                map.delete(rid);
            }
        },

        run(script, ...args) {
            return new Promise((resolve, reject) => {
                let rid = id++;
                map.set(rid, { resolve, reject });
                const inScript = script.toString();
                let sendScript = scriptTemplate;
                sendScript = sendScript
                    .replace(/\$rid\$/g, rid)
                    .replace("$args$", JSON.stringify(args));
                sendScript = sendScript
                    .replace("$code$", inScript);
                if (window.webkit && window.webkit.messageHandlers && window.webkit.messageHandlers.mainScript) {
                    window.webkit.messageHandlers.mainScript.postMessage(`(${sendScript})();`);
                }
                if (typeof androidBridge !== "undefined") {
                    androidBridge.invokeAction(`(${sendScript})();`);
                }
            });
        }
    };
}());