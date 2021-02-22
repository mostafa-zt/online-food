﻿(function ($) {
    "use strict";

    jQuery("#city-dropdown-search").change(function () {
        
        var cityId = this.value;
        var citytile = $("#city-dropdown-search option:selected").text();
        var placeholder = "جستجوی نام محله در " + citytile;
        $("#cityarea-txt-search").attr("placeholder", placeholder);
        $.getJSON("/home/GetCityAreas/", { cityId: cityId }, function (response) {
            
            var list = response.result;
            var $target = $("#cityarea-list");
            $target.removeData().html("");
            $("#HF_city-title-english").val(response.city);
            $.each(list, function (index, entry) {
                var html = '<li class="cityarea-list-item"><a href="javascript:void(0);" class="cityarea-item"  data-val="' + entry.value + '" data-title="' + entry.title + '"  >' + entry.title + '</a></li>';
                $target.append(html);
            });
        });
    });

    jQuery("#cityarea-txt-search").on("keyup", function () {
        
        var $ele = $(this);
        var query = $ele.val();
        var cityId = $("#city-dropdown-search").val()
        $.getJSON("/home/GetCityAreas", { cityId: cityId, query: query }, function (response) {
            
            var list = response.result;
            var $target = $("#cityarea-list");
            $target.removeData().html("");
            $.each(list, function (index, entry) {
                var html = '<li class="cityarea-list-item"><a href="javascript:void(0);" class="cityarea-item"  data-val="' + entry.value + '" data-title="' + entry.title + '"  >' + entry.title + '</a></li>';
                $target.append(html);
            });
        });
    });

    jQuery(document).on('click', ".cityarea-item", function () {
        
        var $ele = $(this);
        $(".cityarea-list-item").each(function (index, entry) {
            $(entry).removeClass("selected");
        });
        //select style of city area and hide menu
        $ele.parent().addClass("selected");
        $(".c-header__address-dropdown").hide();
        // get new title of location and put label
        var city = $("#city-dropdown-search option:selected").text();
        var cityarea = $ele.attr("data-title");
        var text = city + " - " + cityarea;
        $("#custom-search-address-title-cityarea").text(text);
        // set city area value to hidden field
        var cityarea_value = $ele.attr("data-val");
        $("#HF_cityarea-title-english").val(cityarea_value);
    });

    // Just a random third party autocomplete module. Nothing special about this. You can have your own autocomplete module
    // https://goodies.pixabay.com/javascript/auto-complete/demo.html
    var autoCompelete = new autoComplete({
        selector: 'input[name="searchfoodcat"]',
        minChars: 2,
        cache: 0,  // set to 1 if you need caching and making less requests
        source: function (term, response) {
            // Since we wont have full control over ajax responses and caching problems may occure if we directly
            // use geocoder.query, we build the request URL instead and then we use it our custom auto complete module.
            // It's a workaround for not getting cached results in our list.
            //var queryURL = geocoder.queryURL({ query: term, ne: globalSearchBoundingBox.ne, sw: globalSearchBoundingBox.sw });
            var queryURL = "/restaurant/getrestaurants";
            //var restaurantId = $("#RestaurantId").val();
            // In this demo we use a tiny 3rd party library for AJAX requests as we don't use jQuery or other bulky libraries.
            // So feel free to replace it with your preferred one. jQuery syntax for example would be like: $.ajax()
            $.getJSON(queryURL, { q: term }, function (res) {
                
                var json = /*JSON.parse(res);*/ res;
                if (window.console) console.log(json);
                if (typeof json != 'undefined') {
                    if (json.results) response(json.results);
                    //no results
                    else { }
                }
                response(json.results);
            });
        },
        renderItem: function (item, search) {
            var html;
            if (item.url != "") {
                html = '<div class="autocomplete-suggestion autocomplete-search-restaurant" data-url="' + item.url + '" data-value="' + item.value + '" >' +
                    '<a class="item-search-restaurant" href="' + item.url + '"><i class="im im-icon-Shop icon-style"></i>' + item.text + '</a>' +
                    '</div >';
                //html += '<hr />';
            }
            else {
                html = '<div class="autocomplete-suggestion autocomplete-search-restaurant" data-friendlytext="' + item.friendlytext + '" data-text="' + item.text + '" data-value="' + item.value + '" >' +
                         '<div class="item-search-restaurant"><i class="im im-icon-Plate icon-style"></i>' + item.text + '</div>' +
                       '</div >';
            }
            return html;
        },
        onSelect: function (e, term, item) {
            
            //var value = item.getAttribute("data-value");
            var url = item.getAttribute("data-url");
            if (url != null) {
                window.location = url;
                return true;
            }
            var text = item.getAttribute("data-text");
            var searchtxtSearch = document.getElementById("restaurant-foodcategory-txt-search");
            searchtxtSearch.value = text;
            // set friendly food search url to hidden text box
            var friendlytext = item.getAttribute("data-friendlytext");
            $("#HF_food-title-friendly-url").val(friendlytext);
        }
    });

    jQuery(document).on('click', "#search-main-btn", function () {
        var url = "/restaurants/" + $("#HF_city-title-english").val() + "/";
        if ($("#HF_cityarea-title-english").val() != "") {
            url = url + $("#HF_cityarea-title-english").val() + "/";
        }
        if ($("#HF_food-title-friendly-url").val() != "") {
            url = url + "?search=" + $("#HF_food-title-friendly-url").val();
        }
        window.location = url;
    });

})(jQuery);