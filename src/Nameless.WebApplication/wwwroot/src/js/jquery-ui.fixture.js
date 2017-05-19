// Resolve conflict in jQuery UI tooltip with Bootstrap tooltip
(function ($) {
    $.widget.bridge('uibutton', $.ui.button);
})(jQuery);