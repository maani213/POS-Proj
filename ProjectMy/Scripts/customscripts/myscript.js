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

    $('.extrasCat').on("click", function () {
        $('.extrasCat').removeClass("DeptSelected");
        $(this).addClass("DeptSelected");
        var dept = $('.DeptSelected').text();

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

    $(document).on("click", '#addExtraItem', function (evt) {

        if ($(".DeptSelected").length === 0) {
            evt.preventDefault();
            alert("Please Select a Category.");
        }
        else {
            var TitleText = $('#text').val();
            var categoryId = $(".DeptSelected").siblings('input').val();

            if (TitleText !== "") {
                $(".selected").css({
                    "background-color": "green",
                    "color": "white"
                });

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
            //var html = '<div class="col-md-2 cusotomCol"><button class="singleitem" style="background-color:' + bgColor + ';color:' + textColor + '>' + TitleText + '</button><input type="hidden" value="' + positionNumb + '" /></div>'
            //$('#catArea').append(html);
            //$(".selected").css({
            //    "background-color": bgColor,
            //    "color": textColor
            //});
            //$(".selected").text(TitleText);
            var category = { Title: TitleText, BackgroundColor: bgColor, TextColor: textColor, PositionNumber: positionNumb };
            addCategory(category);
            location.reload(true);
        }
        else {
            alert("enter information");
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
            alert('Data: ' + data);
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