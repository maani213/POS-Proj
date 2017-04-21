/// <reference path="../jquery-3.1.1.min.js" />


$(document).ready(function () {
    //$('.singleitem').on("click", function () {
    //    $('.singleitem').removeClass("selected");

    //    $(this).addClass("selected");
    //});

    $('.customBttn').on("click", function () {
        $('.customBttn').removeClass("DeptSelected");
        $(this).addClass("DeptSelected");
        var dept = $('.DeptSelected').html();

        $('.title').text(dept);
    });


    $('#addItem').on("click", function (evt) {

        if ($(".selected").length === 0) {
            evt.preventDefault();
            alert("Please Select a button to add Item.");
        }
        else if ($(".DeptSelected").length === 0) {
            evt.preventDefault();
            alert("Please Select a Category.");
        }
        else {
            var textColor = $('#textcolor').val();
            var bgColor = $('#btncolor').val();
            var price = $('#price').val();
            var isBold = $('#isBold').is(':checked');
            var isItalic = $('#isitalic').is(':checked');

            var TitleText = $('#text').val();
            var toppings = $('#toppings').val();
            var textStyle = $('#fontstyle').val();
            var categoryId = $(".DeptSelected").siblings('input').val();
            var positionNumb = $(".selected").siblings('input').val();
            if (TitleText !== "" && bgColor !== "") {
                $(".selected").css({
                    "background-color": bgColor,
                    "color": textColor
                });
                $(".selected").text(TitleText);
                var item = { Title: TitleText, Toppings: toppings, BackgroundColor: bgColor, TextColor: textColor, PositionNumber: positionNumb, CategoryId: categoryId };
                addItem(item);


                var textColor = $('#textcolor').val("");
                var bgColor = $('#btncolor').val("");
                var price = $('#price').val();

                var TitleText = $('#text').val("");
                var toppings = $('#toppings').val("");
                var textStyle = $('#fontstyle').val("");


            }
            else {
                alert("enter information");
            }
        }

    });

    $('#addCategory').on("click", function (evt) {

        if ($(".selected").length === 0) {
            evt.preventDefault();
            alert("Please Select a button to add Item.");
        }

        else {
            var textColor = $('#textcolor').val();
            var bgColor = $('#btncolor').val();
            var price = $('#price').val();
            var isBold = $('#isBold').is(':checked');
            var isItalic = $('#isitalic').is(':checked');

            var TitleText = $('#text').val();

            var textStyle = $('#fontstyle').val();
            var positionNumb = $(".selected").siblings('input').val();
            if (TitleText !== "" && bgColor !== "") {
                $(".selected").css({
                    "background-color": bgColor,
                    "color": textColor
                });
                $(".selected").text(TitleText);
                var category = { Title: TitleText, BackgroundColor: bgColor, TextColor: textColor, PositionNumber: positionNumb };
                addCategory(category);
            }
            else {
                alert("enter information");
            }
        }

    });

    $('#addSize').on("click", function (evt) {

        if ($(".selected").length === 0) {
            evt.preventDefault();
            alert("Please Select a button to add Item.");
        }

        else {
            var textColor = $('#textcolor').val();
            var bgColor = $('#btncolor').val();
            var TitleText = $('#text').val();

            var textStyle = $('#fontstyle').val();
            var categoryId = $(".DeptSelected").siblings('input').val();
            if (TitleText !== "") {
                //$(".selected").css({
                //    "background-color": bgColor,
                //    "color": textColor
                //});
                $(".selected").text(TitleText);
                var size = { Title: TitleText, CategoryId: categoryId };
                addSize(size);
            }
            else {
                alert("enter information");
            }
        }

    });

    $('#deleteCategory').on("click", function () {
        $('div').remove(".selected");
    });

    $('#deleteItem').on("click", function () {
        $('div').remove(".selected");
    });

})

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
            alert('Data: ' + data);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}
function addCategory(category) {
    $.ajax({

        url: '/ManagementSection/DefineCategories',
        type: 'POST',
        data: JSON.stringify({
            category: category
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            alert('Data: ' + data);
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
            alert('Data: ' + data);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}