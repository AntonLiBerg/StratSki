const GUtil = {
    runningFuncs: {},
    toggleDisplayStandBy: function (selectorParam) {
        var selector;
        if (selectorParam.id)
            selector = "#" + selectorParam.id;
        else if (selectorParam.class)
            selector = "." + selectorParam.class;

        if (this.runningFuncs[selector]) {
            clearInterval(this.runningFuncs[selector]);
            delete this.runningFuncs[selector]
        } else {
            var animation = {
                index:"1",
                "1": ["Asking.", "2", ],
                "2": ["Asking..", "3"],
                "3": ["Asking...", "1"],
                nextFrame: function () {
                    var nAnimation = this[this.index];
                    this.index = nAnimation[1];
                    return nAnimation[0];
                }
            }
            function animateWaiting() {
                document.querySelector("#resultText").textContent = animation.nextFrame();
            }
            this.runningFuncs[selector] = setInterval(animateWaiting,1000);
        }

    },
    displayPopup: function (text) {
        alert(text);
    },
};