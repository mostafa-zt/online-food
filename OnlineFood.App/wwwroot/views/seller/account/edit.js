(function (jQuery) {

    $(".ConfirmPhoneNumberToken_back").click(function () {
        $('.ui.modal').modal('hide');
    })

    $("#frm-account-edit-submit-btn").click(function (e) {
        $mobileNumber = $("#PhoneNumber").val();
        $.ajax({
            type: 'POST',
            data: { mobileNumber: $mobileNumber },
            url: "/seller/account/generatemobiletoken/",
            datatype: 'application/json',
            complete: function (data) {
            },
            success: function (data) {
                if (data.success) {
                    $("#confirmation-mobile-label").html($mobileNumber);
                    $("#UserConfirmPhoneNumberToken").val(data.code);
                    $("#PhoneNumberToken").val(data.code);
                    //$(".message .header").html(data.title);
                    $('.ui.modal').modal({
                        closable: false,
                        onDeny: function () {
                            window.alert('Wait not yet!');
                            return false;
                        },
                        onApprove: function () {
                            var userPhoneNumberToken = $("#UserConfirmPhoneNumberToken").val();
                            var phoneNumberToken = $("#PhoneNumberToken").val();
                            if (userPhoneNumberToken == phoneNumberToken) {
                                $("#ConfirmPhoneNumberToken").val(userPhoneNumberToken);
                                $("#frm-account-edit").submit();// Submit the form
                            }
                            else {
                                ShowNotification('danger', "کد تایید شماره موبایل اشتباه است", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
                                return false;
                            }
                        }
                    }).modal('show');
                    $(".ConfirmPhoneNumberToken_timer").html('دریافت مجدد کد تایید&nbsp;(<span id="timer"></span>)')
                    timer(5);
                }
            },
            beforeSend: function (data, status) {
            },
            error: function () {
                ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
            }
        })
    });

    $('.ui.dropdown').dropdown('setting', {
        allowAdditions: false,
        message: { noResults: 'گزینه ای وجود ندارد', count: '{count} انتخاب شده است', },
    });

})(jQuery);

let timerOn = true;
function timer(remaining) {
    var m = Math.floor(remaining / 60);
    var s = remaining % 60;
    m = m < 10 ? '0' + m : m;
    s = s < 10 ? '0' + s : s;
    document.getElementById('timer').innerHTML = m + ':' + s;
    remaining -= 1;
    if (remaining >= 0 && timerOn) {
        setTimeout(function () {
            timer(remaining);
        }, 1000);
        return;
    }
    if (!timerOn) {
        // Do validate stuff here
        return;
    }
    $(".ConfirmPhoneNumberToken_timer").html('<a id="lnk-resend-phonenumber-token" onclick="resend();"  href="javascript:void(0);" class="lnk-effect">دریافت مجدد کد تایید</a>')
}

function resend() {
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
            $("#confirmation-mobile-label").html($mobileNumber);
            $("#UserConfirmPhoneNumberToken").val(data.code);
            $("#PhoneNumberToken").val(data.code);
            ShowNotification('success', data.message, data.title, 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
            $(".ConfirmPhoneNumberToken_timer").html('دریافت مجدد کد تایید&nbsp;(<span id="timer"></span>)')
            timer(5);
        },
        beforeSend: function (data, status) {
        },
        error: function () {
            ShowNotification('danger', "خطایی در سیستم رخ داده است!", "خطا", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');
        }
    })
}