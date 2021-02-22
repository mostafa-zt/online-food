(function ($) {
    "use strict";

    jQuery('.owl-carousel').owlCarousel({
        rtl: true,
        loop: false,
        margin: 10,
        pagination: false,
        dots: false,
        nav: false,
        responsive: {
            0: {
                items: 2
            },
            600: {
                items: 3
            },
            1000: {
                items: 5
            }
        }
    })

    //// Add smooth scrolling to all links
    //$(document).on('click', ".smoothscroll", function (event) {
    //    // Make sure this.hash has a value before overriding default behavior
    //    if (this.hash !== "") {
    //        // Prevent default anchor click behavior
    //        event.preventDefault();
    //        // Store hash
    //        var hash = $(this).attr("data-id");
    //        $(".smoothscroll").removeClass("active");
    //        $(this).addClass("active");
    //        $target = $(hash)
    //        $targetOffsetTop = $target.offset().top - 58 - 100 - 5;
    //        // Using jQuery's animate() method to add smooth page scroll
    //        // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
    //        $("html, body").stop().animate({ scrollTop: $targetOffsetTop }, 500, "swing");
    //        // Add hash (#) to URL when done scrolling (default click behavior)
    //        //window.location.hash = hash;
    //    } // End if
    //});

    jQuery(document).on('click', ".m_closepanel", function (e) {
        $("#st-container").removeClass("st-menu-open");
    })

    jQuery(document).on('click', "#usercommenttab", function (e) {
        
        var $element = $(this);
        $("#restaurantmenu").addClass("hide");
        var restaurantId = $element.attr("data-val");
        $(".tabcentent").addClass("hide");
        $(".tablink").removeClass("active");
        $element.addClass("active");
        $("#owl-carousel-foodcat").addClass("hide");
        ScrollTop();
        var url = '/restaurantmenu/getcomment/'
        $.ajax({
            type: 'GET',
            data: { restaurantId: restaurantId },
            url: url,
            datatype: 'application/json',
            async: true,
            complete: function (data) {
            },
            success: function (data) {
                
                if (data.success) {
                    var $boxes = $(data.view);
                    var $target = $('#usercomments');
                    $target.removeData().html("").removeClass("hide");
                    $target.css({ opacity: '1' }).html($boxes).delay(1).animate({ opacity: '1.0' }, 300);
                }
            },
            beforeSend: function (data, status) {
                var html = '<div class="userrates"><div class="col-md-12 col-sm-12 col-xs-12">\
                                                                                                <div class="review-box review-percentage review-hearts"> <h2 class="inline-block placeholder-animation placeholder-line-short"></h2>\
                                                                                                    <div class="placeholder-item">\
                                                                                                        <div class="inline-block placeholder-animation placeholder-line-short pull-right"></div>\
                                                                                                        <div class="inline-block placeholder-animation placeholder-line-short pull-left"></div>\
                                                                                                    </div>\
                                                                                                    <div class="placeholder-item">\
                                                                                                        <div class="inline-block placeholder-animation placeholder-line-short pull-right"></div>\
                                                                                                        <div class="inline-block placeholder-animation placeholder-line-short pull-left"></div>\
                                                                                                    </div>\
                                                                                                    <div class="placeholder-item">\
                                                                                                        <div class="inline-block placeholder-animation placeholder-line-short pull-right "></div>\
                                                                                                        <div class="inline-block placeholder-animation placeholder-line-short pull-left "></div>\
                                                                                                    </div>\
                                                                                                    <div class="placeholder-item">\
                                                                                                        <div class="emoji-review centered">\
                                                                                                            <div class="inline-block placeholder-animation placeholder-squar-small placeholder-margin_5"></div>\
                                                                                                            <div class="inline-block placeholder-animation placeholder-squar-small placeholder-margin_5"></div>\
                                                                                                            <div class="inline-block p\laceholder-animation placeholder-squar-small placeholder-margin_5"></div>\
                                                                                                        </div>\
                                                                                                    </div>\
                                                                                                </div>\
                                                                                            </div>\
                                                                                     </div>\
                                                                                        <div class="col-lg-12">\
                                                                                            <div>\
                                                                                                <h3 class="inline-block placeholder-animation placeholder-line-short"></h3>\
                                                                                                <ol class="commentlist">\
                                                                                                    <li class="comment">\
                                                                                                        <div class="comment-body">\
                                                                                                            <div class="author">\
                                                                                                                <div class="comment-meta">\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                </div>\
                                                                                                                <span class="inline-block placeholder-animation placeholder-squar-small pull-left"></span>\
                                                                                                            </div>\
                                                                                                            <div class="text">\
                                                                                                                <p class="inline placeholder-animation placeholder-line-large"></p>\
                                                                                                            </div>\
                                                                                                            <div class="comment-footer full-width inline-block">\
                                                                                                                <div class="pull-right">\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                </div>\
                                                                                                                <div class="inline-block placeholder-animation placeholder-line-short pull-left"></div>\
                                                                                                            </div>\
                                                                                                        </div>\
                                                                                                    </li>\
                                                                                                    <li class="comment">\
                                                                                                        <div class="comment-body">\
                                                                                                            <div class="author">\
                                                                                                                <div class="comment-meta">\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                </div>\
                                                                                                                <span class="inline-block placeholder-animation placeholder-squar-small pull-left"></span>\
                                                                                                            </div>\
                                                                                                            <div class="text">\
                                                                                                                <p class="inline placeholder-animation placeholder-line-large"></p>\
                                                                                                            </div>\
                                                                                                            <div class="comment-footer full-width inline-block">\
                                                                                                                <div class="pull-right">\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                </div>\
                                                                                                                <div class="inline-block placeholder-animation placeholder-line-short pull-left"></div>\
                                                                                                            </div>\
                                                                                                        </div>\
                                                                                                    </li>\
                                                                                                    <li class="comment">\
                                                                                                        <div class="comment-body">\
                                                                                                            <div class="author">\
                                                                                                                <div class="comment-meta">\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                </div>\
                                                                                                                <span class="inline-block placeholder-animation placeholder-squar-small pull-left"></span>\
                                                                                                            </div>\
                                                                                                            <div class="text">\
                                                                                                                <p class="inline placeholder-animation placeholder-line-large"></p>\
                                                                                                            </div>\
                                                                                                            <div class="comment-footer full-width inline-block">\
                                                                                                                <div class="pull-right">\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                    <div class="inline-block placeholder-animation placeholder-line-short"></div>\
                                                                                                                </div>\
                                                                                                                <div class="inline-block placeholder-animation placeholder-line-short pull-left"></div>\
                                                                                                            </div>\
                                                                                                        </div>\
                                                                                                    </li>\
                                                                                                </ol>\
                                                                                            </div>\
                                                                                        </div>';
                var $target = $('#usercomments');
                $target.removeData().html("").removeClass("hide");
                $target.css({ opacity: '1' }).html(html)
            },
            error: function () {
                ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
            }
        })
    });

    jQuery(document).on('click', "#restaurantinfotab", function (e) {
        
        var $element = $(this);
        $(".tabcentent").addClass("hide");
        $("#restaurantinfo").removeClass("hide");
        $(".tablink").removeClass("active");
        $element.addClass("active");
        $("#owl-carousel-foodcat").addClass("hide");
        ScrollTop();
    });

    jQuery(document).on('click', "#restaurantmenutab", function (e) {
        
        var $element = $(this);
        $("#owl-carousel-foodcat").removeClass("hide");
        $(".tabcentent").addClass("hide");
        $("#restaurantmenu").removeClass("hide");
        $(".tablink").removeClass("active");
        $element.addClass("active");
        ScrollTop();
    });

    jQuery(document).on('click', ".menu-add-btn", function (e) {
        
        var $element = $(this);
        //restaurant
        var restauarntlogo = $("#restaurant-logoholder").attr("src");
        var restauarnttitle = $("#restaurant-title").html();
        //menu
        var menuId = $element.attr("data-val");
        var price = $element.attr("data-price");
        var title = $element.attr("data-title");
        var url = '/order/insert/'
        $.ajax({
            type: 'GET',
            data: { menuId: menuId, price: price, title: title, logo: restauarntlogo, restauarntTitle: restauarnttitle },
            url: url,
            datatype: 'application/json',
            async: true,
            complete: function (data) {
            },
            success: function (data) {
                
                if (data.success) {
                    var $boxes = $(data.view);
                    var $target = $('#ordercart-body-restaurant');
                    $target.removeData().html("");
                    $target.css({ opacity: '1' }).html($boxes).delay(1).animate({ opacity: '1.0' }, 300);
                    $(".basket-counter-value").html(data.totalnumber);
                    $("#basket-totalprice-value").html(data.totalprice);

                    $("#ordercart_restaurant_logo").attr("src", restauarntlogo);
                    $("#ordercart_restaurant_title").html(restauarnttitle);
                    $("#ordercart_restaurant").removeClass("hide");

                    var $section = $("#menuitem" + menuId);
                    $section.addClass("ordered");
                    $section.find(".ordercart-management").removeClass("hide");
                    $section.find(".restaurant-menu-add").addClass("hide");
                    $section.find(".incdec-value").html(data.number);

                    //$element.parent().parent().find(".ordercart-management").removeClass("hide");
                    //$element.parent().addClass("hide");
                    //$element.parent().parent().find(".incdec-value").html(data.number);
                }
            },
            beforeSend: function (data, status) {
                $(".ordercart-item").addClass("placeholder-animation");
            },
            error: function () {
                ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
            }
        })
    });

    jQuery(document).on('click', ".btn-panel-delete", function () {
        
        var $target = $(this).parent().find('.tooltip-text-action-delete');
        if ($target.hasClass("hide")) {
            $(this).parent().find('.tooltip-text-action-delete').addClass("animated fadeIn visible").removeClass("hide");
        }
        else {
            $(this).parent().find('.tooltip-text-action-delete').addClass("animated fadeIn hide").removeClass("visible");
        }
    });

    jQuery(document).on('click', ".btn-panel-delete-no", function () {
        
        var $target = $(this).parent();
        $target.addClass("hide").removeClass("visible");
    });

    var scrollLink = $('.smoothscroll');
    // Smooth scrolling
    scrollLink.click(function (e) {
        e.preventDefault();
        var hash = $(this).attr("data-id");
        var $target = $(hash)
        var $targetOffsetTop = $target.offset().top - 58 - 100 - 5;
        $("html, body").stop().animate({ scrollTop: $targetOffsetTop }, 500, "swing");
    });

    //// Active link switching
    jQuery(window).scroll(function () {
        var scrollbarLocation = $(this).scrollTop();
        scrollLink.each(function () {
            var hash = $(this).attr("data-id");
            var $target = $(hash)
            var $targetOffsetTop = $target.offset().top - 58 - 100 - 5;
            if ($targetOffsetTop <= scrollbarLocation) {
                $(".smoothscroll").removeClass('active');
                $(this).addClass('active');
            }
        })
    })

    function ScrollTop() {
        var body = $("html, body");
        body.stop().animate({ scrollTop: 200 }, 500, 'swing');
    }

})(jQuery);

function Increase(menuid, ele) {
    //menu
    var menuId = menuid;
    var $element = $(ele);
    //var price = $element.attr("data-price");
    //var title = $element.attr("data-title");
    var url = '/order/increase/'
    $.ajax({
        type: 'GET',
        data: { menuId: menuId },
        url: url,
        datatype: 'application/json',
        async: true,
        complete: function (data) {
        },
        success: function (data) {
            if (data.success) {
                var $boxes = $(data.view);
                var $target = $('#ordercart-body-restaurant');
                $target.removeData().html("");
                $target.css({ opacity: '1' }).html($boxes).delay(1).animate({ opacity: '1.0' }, 300);
                $(".basket-counter-value").html(data.totalnumber);
                $("#basket-totalprice-value").html(data.totalprice);
                var $section = $("#menuitem" + menuId);
                $section.addClass("ordered");
                $section.find(".ordercart-management").removeClass("hide");
                $section.find(".restaurant-menu-add").addClass("hide");
                $section.find(".incdec-value").html(data.number);
            }
        },
        beforeSend: function (data, status) {
            $(".ordercart-item").addClass("placeholder-animation");
        },
        error: function () {
            ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
        }
    })
}

function Decrease(menuid, ele) {
    //menu
    var menuId = menuid;
    var $element = $(ele);
    //var price = $element.attr("data-price");
    //var title = $element.attr("data-title");
    var url = '/order/decrease/'
    $.ajax({
        type: 'GET',
        data: { menuId: menuId },
        url: url,
        datatype: 'application/json',
        async: true,
        complete: function (data) {
        },
        success: function (data) {

            if (data.success) {
                var $boxes = $(data.view);
                var $target = $('#ordercart-body-restaurant');
                $target.removeData().html("");
                $target.css({ opacity: '1' }).html($boxes).delay(1).animate({ opacity: '1.0' }, 300);

                $(".basket-counter-value").html(data.totalnumber);
                $("#basket-totalprice-value").html(data.totalprice);

                //$("#ordercart_restaurant_logo").attr("src", restauarntlogo);
                //$("#ordercart_restaurant_title").html(restauarnttitle);
                //$("#ordercart_restaurant").removeClass("hide");

                //$element.parent().parent().find(".ordercart-management").removeClass("hide");
                //$element.parent().addClass("hide");


                if (data.number != "" && data.number != "undefined" && data.number != " " && data.number != null && data.number != "0") {
                    var $section = $("#menuitem" + menuId);
                    $section.addClass("ordered");
                    $section.find(".ordercart-management").removeClass("hide");
                    $section.find(".restaurant-menu-add").addClass("hide");
                    $section.find(".incdec-value").html(data.number);
                }
                else {
                    var $section = $("#menuitem" + menuId);
                    $section.removeClass("ordered");
                    $section.find(".ordercart-management").addClass("hide");
                    $section.find(".restaurant-menu-add").removeClass("hide");
                    $section.find(".incdec-value").html("0");
                }
            }
        },
        beforeSend: function (data, status) {
            $(".ordercart-item").addClass("placeholder-animation");
        },
        error: function () {
            ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
        }
    })
}

function ClearOrderCart(ele) {

    var $element = $(ele);
    var url = '/order/clear/'
    $.ajax({
        type: 'GET',
        data: {},
        url: url,
        datatype: 'application/json',
        async: true,
        complete: function (data) {
        },
        success: function (data) {

            if (data.success) {
                var $boxes = $(data.view);
                var $target = $('#ordercart-body-restaurant');
                $target.removeData().html("");
                $target.css({ opacity: '1' }).html($boxes).delay(1).animate({ opacity: '1.0' }, 300);

                $(".basket-counter-value").html(data.totalnumber);
                $("#basket-totalprice-value").html(data.totalprice);

                //$("#ordercart_restaurant_logo").attr("src", restauarntlogo);
                //$("#ordercart_restaurant_title").html(restauarnttitle);

                //var $section = $("#menuitem" + menuId);
                $(".menuitem").removeClass("ordered");
                $(".ordercart-management").addClass("hide");
                $(".restaurant-menu-add").removeClass("hide");
                $(".incdec-value").html("0");

                $("#ordercart_restaurant").addClass("hide");


                $element.parent().addClass("hide").removeClass("visible");
            }
        },
        beforeSend: function (data, status) {
            $(".ordercart-item").addClass("placeholder-animation");
        },
        error: function () {
            ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
        }
    })
}

function Remove(menuid, ele) {

    var menuId = menuid;
    var $element = $(ele);
    var url = '/order/remove/'
    $.ajax({
        type: 'GET',
        data: { menuId: menuId },
        url: url,
        datatype: 'application/json',
        async: true,
        complete: function (data) {
        },
        success: function (data) {

            if (data.success) {
                var $boxes = $(data.view);
                var $target = $('#ordercart-body-restaurant');
                $target.removeData().html("");
                $target.css({ opacity: '1' }).html($boxes).delay(1).animate({ opacity: '1.0' }, 300);

                $(".basket-counter-value").html(data.totalnumber);
                $("#basket-totalprice-value").html(data.totalprice);

                var $section = $("#menuitem" + menuId);
                $section.removeClass("ordered");
                $section.find(".ordercart-management").addClass("hide");
                $section.find(".restaurant-menu-add").removeClass("hide");
                $section.find(".incdec-value").html("0");
            }
        },
        beforeSend: function (data, status) {
            $(".ordercart-item").addClass("placeholder-animation");
        },
        error: function () {
            ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
        }
    })
}