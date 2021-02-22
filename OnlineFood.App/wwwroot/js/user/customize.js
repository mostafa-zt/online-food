(function (window) {

    'use strict';

    // class helper functions from bonzo https://github.com/ded/bonzo

    function classReg(className) {
        return new RegExp("(^|\\s+)" + className + "(\\s+|$)");
    }

    // classList support for class management
    // altho to be fair, the api sucks because it won't accept multiple classes at once
    var hasClass, addClass, removeClass;

    if ('classList' in document.documentElement) {
        hasClass = function (elem, c) {
            return elem.classList.contains(c);
        };
        addClass = function (elem, c) {
            elem.classList.add(c);
        };
        removeClass = function (elem, c) {
            elem.classList.remove(c);
        };
    }
    else {
        hasClass = function (elem, c) {
            return classReg(c).test(elem.className);
        };
        addClass = function (elem, c) {
            if (!hasClass(elem, c)) {
                elem.className = elem.className + ' ' + c;
            }
        };
        removeClass = function (elem, c) {
            elem.className = elem.className.replace(classReg(c), ' ');
        };
    }

    function toggleClass(elem, c) {
        var fn = hasClass(elem, c) ? removeClass : addClass;
        fn(elem, c);
    }

    var classie = {
        // full names
        hasClass: hasClass,
        addClass: addClass,
        removeClass: removeClass,
        toggleClass: toggleClass,
        // short names
        has: hasClass,
        add: addClass,
        remove: removeClass,
        toggle: toggleClass
    };

    // transport
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(classie);
    } else {
        // browser global
        window.classie = classie;
    }

})(window);

(function (jQuery) {
    "use strict";


    jQuery.MyAdmin = {};
    /* Input - Function */
    jQuery.MyAdmin.input = {
        activate: function () {
            //On focus event
            $(document).on('focus', '.form-control', function () {
                $(this).parent().addClass('focused');
            });
            //On focusout event
            $(document).on('focusout', '.form-control', function () {
                var $this = $(this);
                if ($this.parents('.form-group').hasClass('form-float')) {
                    if ($this.val() == '') { $this.parents('.form-line').removeClass('focused'); }
                }
                else {
                    $this.parents('.form-line').removeClass('focused');
                }
            });
            //On label click
            $('body').on('click', '.form-float .form-line .form-label', function () {
                $(this).parent().find('input').focus();
            });
            //Not blank form
            $('.form-control').each(function () {
                if ($(this).val() !== '') {
                    $(this).parents('.form-line').addClass('focused');
                }
            });
        }
    }


    /* Initilize Input */
    jQuery.MyAdmin.input.activate();

    /* Animate loader off screen */
    jQuery(window).load(function () {
        jQuery('body').addClass('loaded');
    });


    /* niceScroll */
    //jQuery("html").niceScroll({
    //	scrollspeed: 60,
    //	mousescrollstep: 38,
    //	cursorwidth: 6,
    //	cursorborder: 0,
    //	cursorcolor: '#6c6c6c', // color
    //	autohidemode: false,
    //	zindex: 9999999,
    //	horizrailenabled: false,
    //	cursorborderradius: 0,
    //});

    var SidebarMenuEffects = (function () {

        function hasParentClass(e, classname) {
            if (e === document) return false;
            if (classie.has(e, classname)) {
                return true;
            }
            return e.parentNode && hasParentClass(e.parentNode, classname);
        }

        // http://coveroverflow.com/a/11381730/989439
        function mobilecheck() {
            var check = false;
            (function (a) { if (/(android|ipad|playbook|silk|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true })(navigator.userAgent || navigator.vendor || window.opera);
            return check;
        }

        function init() {
            var container = document.getElementById('st-container'),
                buttons = Array.prototype.slice.call(document.querySelectorAll('.m_trigger')),
                // event type (if mobile use touch events)
                eventtype = mobilecheck() ? 'touchstart' : 'click',
                resetMenu = function () {
                    classie.remove(container, 'st-menu-open');
                },
                bodyClickFn = function (evt) {
                    if (!hasParentClass(evt.target, 'st-menu')) {
                        resetMenu();
                        document.removeEventListener(eventtype, bodyClickFn);
                    }
                };
            buttons.forEach(function (el, i) {
                var effect = el.getAttribute('data-effect');
                var isOverlay = el.getAttribute('data-overlay');
                el.addEventListener(eventtype, function (ev) {
                    ev.stopPropagation();
                    ev.preventDefault();
                    container.className = 'st-container'; // clear
                    classie.add(container, effect);
                    if (isOverlay) {
                        $('.overlay').addClass('active');
                    }
                    setTimeout(function () {
                        classie.add(container, 'st-menu-open');
                    }, 25);
                    document.addEventListener(eventtype, bodyClickFn);
                });
            });
        }
        init();
    })();


    // the selector will match all input controls of type :checkbox
    // and attach a click event handler
    jQuery(document).on('click', ".img-gallery-checkbox", function () {
        // in the handler, 'this' refers to the box clicked on
        var $box = $(this);
        if ($box.is(":checked")) {
            // the name of the box is retrieved using the .attr() method
            // as it is assumed and expected to be immutable
            var group = "input:checkbox[name='" + $box.attr("name") + "']";
            // the checked state of the group/box on the other hand will change
            // and the current value is retrieved using .prop() method
            $(group).prop("checked", false);
            $box.prop("checked", true);
        } else {
            $box.prop("checked", false);
        }
    });

    //Collapse or Expand
    jQuery(document).on('click', ".expand", function () {
        var content = $(this).attr("data-collpase");
        $('#' + content).slideToggle('slow');
    });

    /* add class sticky in header */
    jQuery(window).on('scroll', function () {
        if (jQuery(this).scrollTop() > 1) {
            jQuery('.sticky header').addClass('sticky')
        } else {
            jQuery('.sticky header').removeClass('sticky')
        }
        return false;
    });

    /* main menu */
    jQuery('a.open_close').on('click', function () {
        jQuery('#main-menu').toggleClass('show');
        jQuery('.layer').toggleClass('layer-is-visible');
    });

    jQuery('a.show-submenu').on('click', function () {
        jQuery(this).next().toggleClass('show_normal');
    });

    jQuery('a.show-submenu-mega').on('click', function () {
        jQuery(this).next().toggleClass('show_mega');
    });

    jQuery(window).width() <= 600 && jQuery('a.open_close').on('click', function () {
        jQuery('.np-toggle-switch').removeClass('active');
    });

    /* Go up */
    jQuery(window).on('scroll', function () {
        if (jQuery(this).scrollTop() > 100) {
            jQuery(".go-up").css("right", "20px");
        } else {
            jQuery(".go-up").css("right", "-60px");
        }
    });

    jQuery(".go-up").on('click', function () {
        jQuery("html,body").animate({ scrollTop: 0 }, 500);
        return false;
    });

    /* sticky sidebar */
    jQuery('.sticky-sidebar').theiaStickySidebar({
        // Settings
        additionalMarginTop: 100,
        additionalMarginBottom: 25,
    });

    /* slideshow */
    if (jQuery(".tp-banner").length) {
        jQuery('.tp-banner').revolution({
            delay: 5000,
            startwidth: 1170,
            startheight: 700,
            hideThumbs: 10,
            fullWidth: "off",
            fullScreen: "off"
        });
    }
    if (jQuery(".tp-banner-2").length) {
        jQuery('.tp-banner-2').revolution({
            delay: 5000,
            startwidth: 1170,
            startheight: 500,
            hideThumbs: 10,
            fullWidth: "off",
            fullScreen: "off"
        });
    }

    // remove-recipe-col
    jQuery(".remove-recipe-col").on('click', function () {
        jQuery(this).parent().remove();
        return false;
    });

    //// Products Filter
    //$("#range_slider").ionRangeSlider({
    //    type: "double",
    //    grid: true,
    //    min: 1,
    //    max: 1000,
    //    from: 250,
    //    to: 600,
    //    prefix: "$"
    //});

    // Check Also Box
    jQuery(function () {
        var $check_also_box = jQuery("#check-also-box");
        if ($check_also_box.length > 0) {
            var articleHeight = jQuery('#the-post').outerHeight();
            var checkAlsoClosed = false;
            jQuery(window).scroll(function () {
                if (!checkAlsoClosed) {
                    var articleScroll = jQuery(document).scrollTop();
                    if (articleScroll > articleHeight) {
                        $check_also_box.addClass('show-check-also');
                    }
                    else {
                        $check_also_box.removeClass('show-check-also');
                    }
                }
            });
        }
        jQuery('#check-also-close').click(function () {
            $check_also_box.removeClass("show-check-also");
            checkAlsoClosed = true;
            return false;
        });
    });

    // Reading Position Indicator
    var reading_content = jQuery('#the-post');
    if (reading_content.length > 0) {
        reading_content.imagesLoaded(function () {
            var content_height = reading_content.height();
            var window_height = jQuery(window).height();
            jQuery(window).scroll(function () {
                var percent = 0,
                    content_offset = reading_content.offset().top,
                    window_offest = jQuery(window).scrollTop();
                if (window_offest > content_offset) {
                    percent = 100 * (window_offest - content_offset) / (content_height - window_height);
                }
                jQuery('#reading-position-indicator').css('width', percent + '%');
            });
        });
    }


    jQuery(document).on('keyup', ".amount", function (event) {
        // When user select text in the document, also abort.
        var selection = window.getSelection().toString();
        if (selection !== '') {
            return;
        }
        // When the arrow keys are pressed, abort.
        if ($.inArray(event.keyCode, [38, 40, 37, 39]) !== -1) {
            return;
        }
        var $this = $(this);
        // Get the value.
        var input = $this.val();
        var input = input.replace(/[\D\s\._\-]+/g, "");
        input = input ? parseInt(input, 10) : 0;

        $this.val(function () {
            return (input === 0) ? "" : input.toLocaleString("en-US");
        });
    });

})(jQuery);


function ShowNotification(colorName, text, title, placementFrom, placementAlign, animateEnter, animateExit) {
    if (colorName === null || colorName === '') { colorName = 'bg-black'; }
    if (text === null || text === '') { text = 'Turning standard Bootstrap alerts'; }
    if (animateEnter === null || animateEnter === '') { animateEnter = 'animated fadeInDown'; }
    if (animateExit === null || animateExit === '') { animateExit = 'animated fadeOutUp'; }
    if (title === null || title === '') { title = ''; }
    var allowDismiss = true;

    colorName = "minimalist-" + colorName;

    $.notify({
        message: text,
        title: title,
        //icon: '/image/web/items/alarm-notify.svg',
    },
        {
            type: colorName,
            allow_dismiss: allowDismiss,
            newest_on_top: true,
            timer: 3000,
            placement: {
                from: placementFrom,
                align: placementAlign
            },
            animate: {
                enter: animateEnter,
                exit: animateExit
            },
            icon_type: 'image',
            template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                      '<span data-notify="title">{1}</span>' +
                      '<span data-notify="message" class="message-notify">{2}</span>' +
                      '</div>'
        });
}
