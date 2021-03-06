﻿/// <reference path="../jquery-3.1.1.min.js" />


$(document).ready(function () {

    function rgbToHex(rgb) {
        var a = rgb.split("(")[1].split(")")[0].split(",");
        return "#" + a.map(function (x) {
            x = parseInt(x).toString(16);
            return (x.length === 1) ? "0" + x : x;
        }).join("");
    }

    $(document).on("click", '.singleSizeitem', function () {
        $('.singleSizeitem').removeClass("selected");

        $(this).addClass("selected");

        var TitleText = $(this).text();
        if ($('#text').length > 0) {
            $('#text').val(TitleText);
        }


    });
    $(document).on('click', "#deleteAll", function (evt) {

        if ($('.DeptSelected').length === 0) {
            alert('Please Select any Category.');
            evt.preventDefault();
        }

        else if (confirm("You are attemting to Delete All Items , OK ? ")) {
            if (confirm("Are you sure to Delete All Items ? ")) {

            }
            else {
                evt.preventDefault();
            }
        }
        else {
            evt.preventDefault();
        }
    });

    $(document).on("click", '.singleitem', function () {
        $("#myForm")[0].reset();
        $(this).toggleClass("selected");

        if ($('.selected').length === 0) {
            //$("#myForm")[0].reset();
            $('#editItem').attr('disabled', true);
            return
        }
        else {
            $('#editItem').attr('disabled', false);
        }
        if ($('#text').length > 0) {
            var bgColor = $(this).css('backgroundColor');
            var txtColor = $(this).css('Color');

            var IsBold = $(this).css('font-weight');

            var IsItalic = $(this).css('font-style');

            if (IsBold === "normal") {
                $('#isBold').prop('checked', false);
            }
            else if (IsBold === "400" || IsBold === "bold") {
                $('#isBold').prop('checked', true);
            }

            if (IsItalic === "italic") {
                $('#isitalic').prop('checked', true);
            }
            else if (IsItalic === "none") {
                $('#isitalic').prop('checked', false);
            }
            var ItemId = $(this).siblings('input').attr("id");
            $('#ItemId').val(ItemId);

            $('#btncolor').val(rgbToHex(bgColor));
            $('#textcolor').val(rgbToHex(txtColor));

            var TitleText = $(this).text();
            var TextStyle = $(this).css('font-family');
            var fontsize = $(this).css('font-size');
            var topping = $(this).siblings('.topppings').text();
            $('#text').val(TitleText);
            $('#toppings').val(topping);
            $('#fontstyle').val(TextStyle);
            $('#fontsize').val(fontsize);
        }

    });


    $(document).on("click", '.singleCookingItem', function () {
        $("#myForm")[0].reset();
        $(this).toggleClass("selected");

        if ($('.selected').length === 0) {
            //$("#myForm")[0].reset();
            $('#UpdateCookInstrItem').attr('disabled', true);
            return
        }
        else {
            $('#UpdateCookInstrItem').attr('disabled', false);
        }
        if ($('#text').length > 0) {
            var bgColor = $(this).css('backgroundColor');
            var txtColor = $(this).css('Color');

            var IsBold = $(this).css('font-weight');

            var IsItalic = $(this).css('font-style');

            if (IsBold === "normal") {
                $('#isBold').prop('checked', false);
            }
            else if (IsBold === "400" || IsBold === "bold") {
                $('#isBold').prop('checked', true);
            }

            if (IsItalic === "italic") {
                $('#isitalic').prop('checked', true);
            }
            else if (IsItalic === "none") {
                $('#isitalic').prop('checked', false);
            }
            var ItemId = $(this).siblings('input').val();
            $('#InstrId').val(ItemId);

            $('#btncolor').val(rgbToHex(bgColor));
            $('#textcolor').val(rgbToHex(txtColor));

            var TitleText = $(this).text();
            var TextStyle = $(this).css('font-family');
            var fontsize = $(this).css('font-size');
            //var topping = $(this).siblings('.topppings').text();
            $('#text').val(TitleText);
            //$('#toppings').val(topping);
            $('#fontstyle').val(TextStyle);
            $('#fontsize').val(fontsize);
        }

    });


    $(document).on("click", '.singleCatitem', function () {
        //$("#myForm")[0].reset();
        $(this).toggleClass("selected");

        if ($('.selected').length === 0) {
            $('#editCategory').attr('disabled', true);
            return
        }
        else {
            $('#editCategory').attr('disabled', false);
        }
        if ($('#text').length > 0) {
            var bgColor = $(this).css('backgroundColor');
            var txtColor = $(this).css('Color');

            var IsBold = $(this).css('font-weight');

            var IsItalic = $(this).css('font-style');

            if (IsBold === "normal") {
                $('#isBold').prop('checked', false);
            }
            else if (IsBold === "400" || IsBold === "bold") {
                $('#isBold').prop('checked', true);
            }

            if (IsItalic === "italic") {
                $('#isitalic').prop('checked', true);
            }
            else if (IsItalic === "none") {
                $('#isitalic').prop('checked', false);
            }
            var ItemId = $(this).siblings('input').attr("id");
            $('#ItemId').val(ItemId);

            $('#btncolor').val(rgbToHex(bgColor));
            $('#textcolor').val(rgbToHex(txtColor));

            var TitleText = $(this).text();

            $('#text').val(TitleText);

        }

    });
    //$(document).on('click', '.extrasCat', function () {
    //    var category = $(this).text;
    //    if (category == "Pizza") {
    //        $('#toppingsRow').removeAttr("style");
    //        $('#toppingslableRow').css("display", "block");
    //    }
    //    else {
    //        $('#toppingsRow').fadeOut();
    //        $('#toppingslableRow').fadeOut();
    //    }

    //});

    $('.customBttn').on("click", function () {
        $('.customBttn').removeClass("DeptSelected");
        $(this).addClass("DeptSelected");
        var dept = $('.DeptSelected').html();

        $('.title').text(dept);
    });

    $('.extrasCat').on("click", function () {

        $("#categoryId").val($(this).siblings('input').val());
        $('.extrasCat').removeClass("DeptSelected");
        $(this).addClass("DeptSelected");
        var dept = $('.DeptSelected').text();
        var CateId = $(this).siblings('input[type=hidden]').val();
        $('#deleteAllCatId').val(CateId);
        $('.title').text(dept);
    });

    $(document).on("click", '#addItem', function (evt) {

        if ($(".DeptSelected").length == 0) {
            evt.preventDefault();
            alert("Please Select a Category.");
        }

        else if ($('#text').val() == "" && $('#toppings').val() == "") {
            evt.preventDefault();
            alert("enter information");
        }
        else if ($('.singleitem').length > 30) {
            evt.preventDefault();
            alert('Sorry ! you can not add more Items (max=30).');
        }
    });

    $(document).on("click", '#addCookInstrItem', function (evt) {

        if ($('#text').val() == "") {
            evt.preventDefault();
            alert("enter information");
        }
    });

    $(document).on("click", '#addExtraItem', function (evt) {

        if ($(".DeptSelected").length === 0) {
            evt.preventDefault();
            alert("Please Select a Category.");
        }
        else {
            var TitleText = $('#text').val();
            var categoryId = $(".DeptSelected").siblings('input').val();

            if (TitleText !== "") {

                var item = { Title: TitleText, Price1: 0.00, Price2: 0.00, Price3: 0.00, CategoryId: categoryId };
                addExtraItem(item);

                $('#text').val("");

            }
            else {
                alert("enter information");
            }
        }

    });

    $('#addCategory').on("click", function (evt) {

        var textColor = $('#textcolor').val();
        var bgColor = $('#btncolor').val();

        var isBold = $('#isBold').is(':checked');
        var isItalic = $('#isitalic').is(':checked');

        var TitleText = $('#text').val();

        var textStyle = $('#fontstyle').val();
        var positionNumb = parseInt($(".singleitem").length) + 1;

        if (TitleText !== "" && bgColor !== "") {
            var category = { Title: TitleText, BackgroundColor: bgColor, TextColor: textColor, PositionNumber: positionNumb };
            addCategory(category);
            location.reload(true);
        }
        else {
            alert("enter information");
        }

    });

    $('#addSize').on("click", function (evt) {

        if ($(".DeptSelected").length === 0) {
            alert('Select Departement .');
        }
        else {
            var textColor = $('#textcolor').val();
            var bgColor = $('#btncolor').val();
            var TitleText = $('#text').val();

            var textStyle = $('#fontstyle').val();
            var categoryId = $(".DeptSelected").siblings('input').val();
            if (TitleText !== "") {

                var size = { Title: TitleText, CategoryId: categoryId };
                addSize(size);
            }
            else {
                alert("Enter Title");
            }
        }
    });

    $('#deleteItem').on("click", function (evt) {

        if ($(".selected").length === 0) {
            evt.preventDefault();
            alert("Please Select button(s) to Delete Item.");
        }
        else if ($(".selected").length === 1) {
            var id = $(".selected").siblings("input").attr("id");

            if ($(".selected").text() === "") {
                alert("Please Select a valid Item.");
            }
            else if (confirm("Are you sure to delete this item ?")) {
                DeleteItem(id);
            }

            else {
                evt.preventDefault();
            }
        }
        else if ($(".selected").length > 1) {

            var itemsIds = [];

            $('.selected').each(function () {
                var Id = $(this).siblings("input").attr("id");

                itemsIds.push(Id);
            });
            var count = itemsIds.length;
            if (confirm("Are you sure to delete " + count + " item ?")) {
                DeleteMultipleItemsItem(itemsIds);
            }

            else {
                evt.preventDefault();
            }
        }
    });
    $('#deleteCategory').on("click", function (evt) {
        if ($(".selected").length === 0) {
            evt.preventDefault();
            alert("Please Select a button to Delete Item.");
        }
        else {
            var id = $(".selected").siblings("input").attr("id");
            if ($(".selected").text() === "") {
                alert("Please Select a valid Item.");
            }
            else if (confirm("Are you sure to delete this item ?")) {
                DeleteCategory(id);
            }

            else {
                evt.preventDefault();
            }


        }
    });

    $('#deleteSize').on("click", function (evt) {
        if ($(".selected").length === 0) {
            evt.preventDefault();
            alert("Please Select a button to Delete Item.");
        }
        else {
            var id = $(".selected").siblings("input").attr("id");
            if ($(".selected").text() === "") {
                alert("Please Select a valid Item.");
            }
            else if (confirm("Are you sure to delete this item ?")) {
                DeleteSize(id);
            }

            else {
                evt.preventDefault();
            }


        }
    });

    $('#deleteExtrasItem').on("click", function (evt) {
        if ($(".selectedTopping").length === 0) {
            evt.preventDefault();
            alert("Please Select a button to Delete Item.");
        }
        else {
            var id = $(".selectedTopping").siblings("input").attr("id");
            if ($(".selectedTopping").text() === "") {
                alert("Please Select a valid Item.");
            }
            else if (confirm("Are you sure to delete this item ?")) {
                DeleteExtra(id);
            }

            else {
                evt.preventDefault();
            }


        }
    });

    $('#deleteCookInstrItem').on("click", function (evt) {
        if ($(".selected").length === 0) {
            evt.preventDefault();
            alert("Please Select a button to Delete Item.");
        }
        else {
            var id = $(".selected").siblings("input").val();
            alert(id);
            if (confirm("Are you sure to delete this item ?")) {
                DeleteCookingInstr(parseInt(id));
            }

            else {
                evt.preventDefault();
            }


        }
    });

    $(document).on("click", '#editItem', function (evt) {

        if ($('.selected').length > 1) {
            evt.preventDefault();
            alert('You have selected more than 1 item , Please unselect other items.');
        }
        else {
            var ItemId = $('.selected').siblings('input').attr("id");
            var NewbgColor = $('#btncolor').val();
            var NewTitleText = $('#text').val();
            if (NewTitleText !== "" && NewbgColor !== "") {
                if (confirm('Are you sure to update settings ?')) {
                    $.post("/ManagementSection/EditItem", $("#myForm").serialize(), function (data) {
                        $('.selected').css({
                            "background-color": data.BackgroundColor,
                            'color': data.TextColor,
                            'font-family': data.TextStyle,
                            'font-style': data.fontStyle,
                            'font-weight': data.fontWeight,
                            'font-size': data.FontSize

                        });

                        $('.selected').text(data.Title);
                        $("#myForm")[0].reset();
                        $('.selected').removeClass("selected");
                    });
                }
                else {
                    evt.preventDefault();
                }
            }
            else {
                alert('Some Information missing.');
            }
        }
    });

    $(document).on("click", '#UpdateCookInstrItem', function (evt) {

        if ($('.selected').length > 1) {
            evt.preventDefault();
            alert('You have selected more than 1 item , Please unselect other items.');
        }
        else {
            // var ItemId = $('.selected').siblings('input').attr("id");
            var NewbgColor = $('#btncolor').val();
            var NewTitleText = $('#text').val();
            if (NewTitleText !== "" && NewbgColor !== "") {
                if (confirm('Are you sure to update settings ?')) {
                    $.post("/ManagementSection/UpdateCookingInstr", $("#myForm").serialize(), function (data) {
                        $('.selected').css({
                            "background-color": data.BackgroundColor,
                            'color': data.TextColor,
                            'font-family': data.TextStyle,
                            'font-style': data.fontStyle,
                            'font-weight': data.fontWeight,
                            'font-size': data.FontSize

                        });

                        $('.selected').text(data.Title);
                        $("#myForm")[0].reset();
                        $('.selected').removeClass("selected");
                    });
                }
                else {
                    evt.preventDefault();
                }
            }
            else {
                alert('Some Information missing.');
            }
        }
    });

    $(document).on("click", '#editSize', function (evt) {

        //getting Updated Values
        var ItemId = $('.selected').siblings('input').attr("id");

        var NewTitleText = $('#text').val();

        if ($(".selected").length === 0) {
            evt.preventDefault();
            alert("Please Select a button .");
        }

        else if (confirm("Are you sure to Update Settings ?")) {
            var textColor = $('#textcolor').val();
            var bgColor = $('#btncolor').val();
            var TitleText = $('#text').val();

            var textStyle = $('#fontstyle').val();

            if (TitleText !== "") {

                var size = { Id: ItemId, Title: TitleText };
                EditSize(size);
                $(".selected").text(NewTitleText);

                $('.singleitem').removeClass("selected");
            }
            else {
                alert("enter information");
            }
        }

    });

    $(document).on("click", '#editCategory', function (evt) {

        if ($('.selected').text() === "") {
            alert("Select a valid Button.");
        }
        else {


            //getting Updated Values
            var ItemId = $('.selected').siblings('input').attr("id");
            var NewtextColor = $('#textcolor').val();
            var NewbgColor = $('#btncolor').val();

            var NewTitleText = $('#text').val();

            var NewtextStyle = $('#fontstyle').val();

            if (NewTitleText !== "" && NewbgColor !== "") {

                var item = { Id: ItemId, Title: NewTitleText, BackgroundColor: NewbgColor, TextColor: NewtextColor };
                if (confirm("Are you sure to Update Settings ?")) {
                    EditCategory(item);
                    var textColor = $('#textcolor').val("");
                    var bgColor = $('#btncolor').val("");

                    var TitleText = $('#text').val("");

                    var textStyle = $('#fontstyle').val("");

                    $(".selected").css({
                        "background-color": NewbgColor,
                        "color": NewtextColor
                    });

                    $(".selected").text(NewTitleText);

                    $('.singleitem').removeClass("selected");
                }
                else {
                    evt.preventDefault();
                }

            }

            else {
                alert("Please enter information");
            }
        }
    });
    $(document).on("click", "#EditExtrasItem", function (evt) {

        //getting Updated Values

        if ($(".selectedTopping").text() === "") {
            evt.preventDefault();
            alert("Please Select a valid button .");
        }

        else if (confirm("Are you sure to Update Settings ?")) {

            var ItemId = $('.selectedTopping').siblings('input').attr("id");

            var NewTitleText = $('#text').val();


            //var textColor = $('#textcolor').val();
            //var bgColor = $('#btncolor').val();
            //var TitleText = $('#text').val();

            //var textStyle = $('#fontstyle').val();

            if (NewTitleText !== "") {

                var extraItem = { Id: ItemId, Title: NewTitleText };
                EditExtra(extraItem);
                $(".selectedTopping").text(NewTitleText);

                $('.Extraitems').removeClass("selected");
            }
            else {
                alert("enter information");
            }
        }

    });


});

function addItem(data1) {
    $.ajax({

        url: '/ManagementSection/AddItem',
        type: 'POST',
        data: JSON.stringify({
            model: data1
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $(".selected").css({
                "background-color": data.BackgroundColor,
                "color": data.TextColor
            });
            $(".selected").text(data.Title);
            $(".selected").siblings("input").attr("id", data.Id);
            alert('Data: ' + "Item Added");
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function NewItem(data) {
    var $html = '<div class="col-md-2 col-sm-2 col-lg-2 cusotomCol"><button class="singleitem" style="background-color:' + data.BackgroundColor + ';color:' + data.TextColor + ';font-family:' + data.TextStyle + ';font-style:' + data.fontStyle + ';font-weight:' + data.fontWeight + ';font-size:' + data.FontSize + '; " >' + data.Title + '</button><input type="hidden" id="' + data.Id + '" value="' + data.PositionNumber + '" /><p hidden class="topppings">' + data.Toppings + ' </p></div>';
    $('#itemRow').append($html);
    $("#myForm")[0].reset();
};

function NewCookingInstrItem(data) {
    var $html = '<div class="col-md-4 col-sm-4 col-lg-4 cusotomCol"><button class="singleCookingItem" style="background-color:' + data.BackgroundColor + ';color:' + data.TextColor + ';font-family:' + data.TextStyle + ';font-style:' + data.fontStyle + ';font-weight:' + data.fontWeight + ';font-size:' + data.FontSize + ';" >' + data.Title + ' </button><input type="hidden" value="' + data.Id + '" /></div>';
    $('#CookInstrItemRow').append($html);
    $("#myForm")[0].reset();
};

function DeleteAllSuccess(data) {
    $('#itemRow div').each(function () {
        $(this).hide();
    });
    alert(data);
}
function DeleteCookingInstr(id) {

    $.ajax({

        url: '/ManagementSection/DeleteCookingInstr',
        type: 'POST',
        data: JSON.stringify({
            value: id
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) {
                $(".selected").closest("div").hide();
            }
            else {

                alert("error : smething went wrong.");
            }
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });

}

function DeleteExtra(value) {
    $.ajax({

        url: '/ManagementSection/DeleteExtra',
        type: 'POST',
        data: JSON.stringify({
            id: value
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $(".selectedTopping").closest("div").hide();
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });

}

function DeleteItem(value) {
    $.ajax({

        url: '/ManagementSection/DeleteItem',
        type: 'POST',
        data: JSON.stringify({
            id: value
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $(".selected").closest("div").hide();
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}
function DeleteMultipleItemsItem(Ids) {
    $.ajax({

        url: '/ManagementSection/DeleteMultipleItemsItem',
        type: 'POST',
        data: JSON.stringify({
            ids: Ids
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $('.selected').each(function () {
                $(this).closest("div").hide();
            });
            alert(data);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });

}


function DeleteCategory(value) {
    $.ajax({

        url: '/ManagementSection/DeleteCategory',
        type: 'POST',
        data: JSON.stringify({
            id: value
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $(".selected").closest("div").hide();
            $(".selected").removeClass(".selected");
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });

}

function EditSize(value) {
    $.ajax({

        url: '/ManagementSection/EditSize',
        type: 'POST',
        data: JSON.stringify({
            sizeItem: value
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //$(".selected").closest("div").hide();
            alert(data);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });

}

function EditExtra(value) {
    $.ajax({

        url: '/ManagementSection/EditExtra',
        type: 'POST',
        data: JSON.stringify({
            extraItem: value
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //$(".selectedTopping").closest("div").hide();
            alert(data);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });

}

//function EditItem(value) {

//    $.ajax({
//        url: '/ManagementSection/EditItem',
//        type: 'POST',
//        data: JSON.stringify({
//            item: value
//        }),
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            alert(data);
//        },
//        error: function (request, error) {
//            alert("Request: " + JSON.stringify(request));
//        }
//    });
//}

function EditCategory(value) {
    $.ajax({

        url: '/ManagementSection/EditCategory',
        type: 'POST',
        data: JSON.stringify({
            category: value
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //$(".selected").closest("div").hide();
            //$(".selected").removeClass(".selected");
            alert(data);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });

}


function addExtraItem(data1) {
    $.ajax({

        url: '/ManagementSection/AddExrtaItem',
        type: 'POST',
        data: JSON.stringify({
            extra: data1
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var html = '<div class="col-md-2 cusotomCol"><button class="Extraitem" style="background-color:hotpink;color:white">' + data.Title + ' </button><input type="hidden" id="' + data.Id + '" value="' + data.Id + '" /> </div>'
            $('#extrasDiv').append(html);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function addCategory(category) {
    $.ajax({
        async: false,
        url: '/ManagementSection/DefineCategories',
        type: 'POST',
        data: JSON.stringify({
            category: category
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $(".selected").css({
                "background-color": data.BackgroundColor,
                "color": data.TextColor
            });
            $(".selected").text(data.Title);
            $(".selected").siblings("input").attr("id", data.Id);
            alert('Data: ' + "Item Added");
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });

}

function addSize(item) {
    $.ajax({

        url: '/ManagementSection/Sizes',
        type: 'POST',
        data: JSON.stringify({
            size: item
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $(".selected").text(data.Title);
            $(".selected").siblings("input").attr("id", data.Id);
            var html = '<div class="col-md-4 cusotomCol"><button class="singleSizeitem" style="background-color:aqua;color:black">' + data.Title + '</button><input id="' + data.Id + '" type="hidden" value="" /></div>'
            $('#sizeDiv').append(html);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });

}
