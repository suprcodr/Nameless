(function () {
    "use strict";

    [].slice.call(document.querySelectorAll('input.input_field')).forEach(function (inputElement) {
        // in case the input is already filled..
        if (inputElement.value.trim() !== '') {
            classie.add(inputElement.parentNode, 'input-filled');
        }

        // events:
        inputElement.addEventListener('focus', onInputFocus);
        inputElement.addEventListener('blur', onInputBlur);
    });

    function onInputFocus(ev) {
        classie.add(ev.target.parentNode, 'input-filled');
    }

    function onInputBlur(ev) {
        if (ev.target.value.trim() === '') {
            classie.remove(ev.target.parentNode, 'input-filled');
        }
    }
})();