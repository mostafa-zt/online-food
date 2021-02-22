(function (jQuery) {
    $(".wizard-step:first").show(); //show first step

    $("#FrontOfNationalCardFile").fileinput({
        language: "fa",
        //theme: "fas",
        overwriteInitial: true,
        maxFileSize: 1500,
        showClose: false,
        showCaption: false,
        showBrowse: false,
        //showPreview: false,
        browseOnZoneClick: true,
        removeLabel: '',
        removeIcon: '<i class="glyphicon glyphicon-remove"></i>',
        removeTitle: 'حذف',
        //elErrorContainer: '#kv-avatar-errors-2',
        //msgErrorClass: 'alert alert-block alert-danger',
        defaultPreviewContent: '<img src="/image/web/items/upload-placeholder.jpg" alt="بارگزاری تصویر" title="بارگزاری تصویر">',
        layoutTemplates: { main2: '{preview} {remove} {browse}' },
        allowedFileExtensions: ["jpg", "png", "jpeg"]
    });

    $("#BehindOfNationalCardFile").fileinput({
        language: "fa",
        //theme: "fas",
        overwriteInitial: true,
        maxFileSize: 1500,
        showClose: false,
        showCaption: false,
        showBrowse: false,
        //showPreview: false,
        browseOnZoneClick: true,
        removeLabel: '',
        removeIcon: '<i class="glyphicon glyphicon-remove"></i>',
        removeTitle: 'حذف',
        //elErrorContainer: '#kv-avatar-errors-2',
        //msgErrorClass: 'alert alert-block alert-danger',
        defaultPreviewContent: '<img src="/image/web/items/upload-placeholder.jpg" alt="بارگزاری تصویر" title="بارگزاری تصویر">',
        layoutTemplates: { main2: '{preview} {remove} {browse}' },
        allowedFileExtensions: ["jpg", "png", "jpeg"]
    });

    $('.ui.dropdown').dropdown('setting', {
        allowAdditions: false,
        message: { noResults: 'گزینه ای وجود ندارد', count: '{count} انتخاب شده است', },
    });

    //change radio buttons group
    $("input[name='IsLegal']").change(function () {
        // Your code for any radio button change event
        var isSelected = $("input[name=IsLegal]:checked").val();
        if ($("#seller-info-main").hasClass("hide")) {
            $("#seller-info-main").addClass("animated fadeIn").removeClass("hide");
        }
        if (isSelected == "true") {
            $("#seller-info-legal").removeClass("hide").addClass("animated fadeIn");
            $("#seller-info-personal").addClass("hide").removeClass("animated fadeIn");
        }
        else if (isSelected == "false") {
            $("#seller-info-personal").removeClass("hide").addClass("animated fadeIn");
            $("#seller-info-legal").addClass("hide").removeClass("animated fadeIn");
        }
    })

    $("#btn-account-info-next").click(function (e) {
        $btn = $(this);
        $mobileNumber = $("#PhoneNumber").val();
        var $step = $(".wizard-step:visible"); // get current step
        if (!validate($step)) return false;
        $.ajax({
            type: 'POST',
            data: { mobileNumber: $mobileNumber },
            url: "/seller/account/generatemobiletoken/",
            datatype: 'application/json',
            complete: function (data) {
            },
            success: function (data) {
                $("#phonenumber-confirmation").addClass("animated fadeIn").removeClass("hide");
                $("#account-info").addClass("hide").removeClass("animated fadeIn");
                $("#ConfirmPhoneNumberToken").val(data.code);
                $("#PhoneNumberToken").val(data.code);
            },
            beforeSend: function (data, status) {
            },
            error: function () {
                ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
            }
        })
    })

    $("#btn-phonenumber-confirmation-back").click(function () {
        $btn = $(this);
        $("#phonenumber-confirmation").addClass("hide").removeClass("animated fadeIn");
        $("#account-info").addClass("animated fadeIn").removeClass("hide");
        $("#ConfirmPhoneNumberToken").val("");
        $("#PhoneNumberToken").val("");
    })

    $("#btn-phonenumber-confirmation-next").click(function (e) {
        $btn = $(this);
        if (CheckMobileToken()) {
            $("#phonenumber-confirmation").addClass("hide").removeClass("animated fadeIn");
            $("#seller-info").addClass("animated fadeIn").removeClass("hide");
        }
    })

    $("#btn-seller-info-back").click(function () {
        $btn = $(this);
        $("#seller-info").addClass("hide").removeClass("animated fadeIn");
        $("#account-info").addClass("animated fadeIn").removeClass("hide");
    })

    //click resend phonenumber token
    $("#lnk-resend-phonenumber-token").click(function (e) {
        $btn = $(this);
        $mobileNumber = $("#PhoneNumber").val();
        $.ajax({
            type: 'POST',
            data: { mobileNumber: $mobileNumber },
            url: "/seller/account/generatemobiletoken/",
            datatype: 'application/json',
            complete: function (data) {
            },
            success: function (data) {
                $("#ConfirmPhoneNumberToken").val(data.code);
                $("#PhoneNumberToken").val(data.code);
                ShowNotification('success', data.message, data.title, 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
            },
            beforeSend: function (data, status) {
            },
            error: function () {
                ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
            }
        })
    })

    //attach backStep button handler
    $(".back-step").click(function (e) {
        var $step = $(".wizard-step:visible"); // get current step
        if ($step.prev().hasClass("wizard-step")) { // is there any previous step?
            $step.hide().prev().fadeIn(); // show it and hide current step
            $('.steps').find('.active').removeClass('active').addClass('disabled').prev().addClass('active');
            //// disable backstep button?
            //if (!$step.prev().prev().hasClass("wizard-step")) {
            //    $(".back-step").hide();
            //}
        }
    });

    //attach nextStep button handler
    $(".next-step").click(function (e) {
        var $step = $(".wizard-step:visible"); // get current step
        var $btn = $(this);
        //$("#myForm").validate({
        //    // your options,
        //    ignore: ":hidden, #yourDiv :input"
        //});
        //validate form
        if (!validate($step)) return false;
        //if ($step.next().hasClass("confirm")) { // is it confirmation?
        //    // show confirmation asynchronously
        //    $.post("/wizard/confirm", $("form").serialize(), function (r) {
        //        // inject response in confirmation step
        //        $(".wizard-step.confirm").html(r);
        //    });
        //}
        if ($btn.hasClass("callFunction")) {
            e.preventDefault();
            var method_name = $btn.attr("data-function");
            // Call function:
            var result = window[method_name]();
            if (!result) return false;
        }
        if ($step.next().hasClass("wizard-step")) { //is there any next step?
            $step.hide().next().fadeIn(); //show it and hide current step
            $('.steps').find('.active').removeClass('active').next().addClass('active').removeClass('disabled'); //activate next step naviagtion and remove current
        }
        else { // this is last step, submit form
            $("form").submit();
        }
    });

})(jQuery);

function CheckMobileToken() {
    //var $btn = $(this);
    var isValid = false;
    var $phoneNumberToken = $("#PhoneNumberToken").val();
    var $confirmPhoneNumberToken = $("#ConfirmPhoneNumberToken").val();
    $.ajax({
        type: 'POST',
        data: { confirmPhoneNumberToken: $confirmPhoneNumberToken, phoneNumberToken: $phoneNumberToken },
        url: "/seller/account/checkmobiletoken/",
        datatype: 'application/json',
        async: false,
        complete: function (data) {
        },
        success: function (data) {
            isValid = data.success;
            if (!isValid) {
                ShowNotification('danger', data.message, data.title, 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
            }
        },
        beforeSend: function (data, status) {
        },
        error: function () {
            ShowNotification('danger', '@Message.GeneralError', '@Title.Error', 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
        }
    })
    return isValid;
}

function LoadConfirmation() {
    //var $btn = $(this);
    var isValid = false;
    var $phoneNumberToken = $("#PhoneNumberToken").val();
    var $confirmPhoneNumberToken = $("#ConfirmPhoneNumberToken").val();
    $.ajax({
        type: 'POST',
        data: $("form").serialize(),
        url: "/seller/account/registerconfirmation/",
        datatype: 'application/json',
        async: false,
        complete: function (data) {
        },
        success: function (data) {
            isValid = data.success;
            if (!isValid) {
                ShowNotification('danger', data.message, data.title, 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
            }
            else {
                var $boxes = $(data.view);
                var container = $('#confirmation-info-main');
                container.removeData().html("");
                container.html($boxes);
            }
        },
        beforeSend: function (data, status) {
        },
        error: function () {
            ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
        }
    })

    return isValid;
}

function validate(step) {
    var anyError = false;
    var validator = $("form").validate(); // obtain validator
    step.find("input").each(function () {
        if (!validator.element(this)) { // validate every input element inside this step
            anyError = true;
        }
    });
    if (anyError)
        return false; // exit if any error found
    return true;
}