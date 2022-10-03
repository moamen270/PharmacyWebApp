/**
 * WEBSITE: https://themefisher.com
 * TWITTER: https://twitter.com/themefisher
 * FACEBOOK: https://www.facebook.com/themefisher
 * GITHUB: https://github.com/themefisher/
 */

(function ($) {
    'use strict';

    // Preloader js
    $(window).on('load', function () {
        $('.preloader').fadeOut(100);
    });

    // navfixed
    $(window).scroll(function () {
        if ($('.navigation').offset().top > 50) {
            $('.navigation').addClass('nav-bg');
        } else {
            $('.navigation').removeClass('nav-bg');
        }
    });

    // masonry
    $('.masonry-wrapper').masonry({
        columnWidth: 1
    });

    // clipboard
    var clipInit = false;
    $('code').each(function () {
        var code = $(this),
            text = code.text();
        if (text.length > 2) {
            if (!clipInit) {
                var text, clip = new ClipboardJS('.copy-to-clipboard', {
                    text: function (trigger) {
                        text = $(trigger).prev('code').text();
                        return text.replace(/^\$\s/gm, '');
                    }
                });
                clipInit = true;
            }
            code.after('<span class="copy-to-clipboard">copy</span>');
        }
    });
    $('.copy-to-clipboard').click(function () {
        $(this).html('copied');
    });

    // match height
    $(function () {
        $('.match-height').matchHeight({
            byRow: true,
            property: 'height',
            target: null,
            remove: false
        });
    });

    // search
    $('#search-by').keyup(function () {
        if (this.value) {
            $(this).addClass('active')
        } else {
            $(this).removeClass('active')
        }
    })

    // Accordions
    $('.collapse').on('shown.bs.collapse', function () {
        $(this).parent().find('.ti-plus').removeClass('ti-plus').addClass('ti-minus');
    }).on('hidden.bs.collapse', function () {
        $(this).parent().find('.ti-minus').removeClass('ti-minus').addClass('ti-plus');
    });
})(jQuery);

// HEAD
(function ($) {
    "use strict"

    var form = $("#step-form-horizontal");
    form.children('div').steps({
        headerTag: "h4",
        bodyTag: "section",
        transitionEffect: "slideLeft",
        autoFocus: true,
        transitionEffect: "slideLeft",
        onStepChanging: function (event, currentIndex, newIndex) {
            form.validate().settings.ignore = ":disabled,:hidden";
            return form.valid();
        }
    });

    var form2 = $("#step-form-tab");
    form2.children('div').steps({
        headerTag: "h4",
        bodyTag: "section",
        // Disables the finish button (required if pagination is enabled)
        enableFinishButton: false,
        // Disables the next and previous buttons (optional)
        enablePagination: false,
        // Enables all steps from the begining
        enableAllSteps: true,
        // Removes the number from the title
        titleTemplate: "#title#"
    });

    var form3 = $('#step-form-vertical');
    form3.children('div').steps({
        headerTag: "h4",
        bodyTag: "section",
        transitionEffect: "slideLeft",
        stepsOrientation: "vertical",
        // onStepChanging: function (event, currentIndex, newIndex)
        // {
        //     form3.validate().settings.ignore = ":disabled,:hidden";
        //     return form.valid();
        // }
    });
})(jQuery);
// 2f79b01d9b20711481b000203e2c79f1ac30081d

$(document).ready(function ($) {
    "use strict";

    $('.skill-shortcode').appear(function () {
        $('.progress').each(function () {
            $('.progress-bar').css('width', function () { return ($(this).attr('data-percentage') + '%') });
        });
    }, { accY: -100 });
});