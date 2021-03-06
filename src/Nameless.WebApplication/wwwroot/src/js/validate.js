﻿(function ($) {
    if ($.fn.validate) {
        $.validator.setDefaults({
            errorClass: 'has-error',
            validClass: '',
            highlight: function (element, errorClass, validClass) {
                $(element).parents('div.form-group').addClass(errorClass).removeClass(validClass);
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents('div.form-group').removeClass(errorClass).addClass(validClass);
            }
        });
    }
})(jQuery);