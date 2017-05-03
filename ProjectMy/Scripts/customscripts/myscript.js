/// <reference path="../jquery-3.1.1.min.js" />


$(document).ready(function () {

    function rgbToHex(rgb) {
        var a = rgb.split("(")[1].split(")")[0].split(",");
        return "#" + a.map(function (x) {
            x = parseInt(x).toString(16);
            return (x.length == 1) ? "0" + x : x;
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

    $(document).on("click", '.singleitem', function () {
        $('.singleitem').removeClass("selected");
        $(this).addClass("selected");
        if ($('#text').length > 0) {
            var bgColor = $(this).css('backgroundColor');
            var txtColor = $(this).css('Color');

            $('#btncolor').val(rgbToHex(bgColor));
            $('#textcolor').val(rgbToHex(txtColor));

            var TitleText = $(this).text();
            var topping = $(this).siblings('.topppings').text();
            $('#text').val(TitleText);
            $('#toppings').val(topping);
        }

    });

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

                var size = { Title: TitleText, CategoryId: categoryId };
                addSize(size);
            }
            else {
                alert("enter information");
            }
        }

    });

    $('#deleteItem').on("click", function (evt) {
        //$('div').remove(".selected");
        if ($(".selected").length === 0) {
            evt.preventDefault();
            alert("Please Select a button to Delete Item.");
        }
        else {
            var id = $(".selected").siblings("input").attr("id");
            var t = $(".selected").text();

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
    $(document).on("click", '#editItem', function (evt) {
        //getting Updated Values
        var ItemId = $('.selected').siblings('input').attr("id");
        var NewtextColor = $('#textcolor').val();
        var NewbgColor = $('#btncolor').val();

        var NewTitleText = $('#text').val();
        var Newtoppings = $('#toppings').val();
        var NewtextStyle = $('#fontstyle').val();

        if (NewTitleText !== "" && NewbgColor !== "") {

            var item = { Id: ItemId, Title: NewTitleText, Toppings: Newtoppings, BackgroundColor: NewbgColor, TextColor: NewtextColor };
            if (confirm("Are you sure to Update Settings ?")) {
                EditItem(item);
                var textColor = $('#textcolor').val("");
                var bgColor = $('#btncolor').val("");

                var TitleText = $('#text').val("");
                var toppings = $('#toppings').val("");
                var textStyle = $('#fontstyle').val("");

                $(".selected").css({
                    "background-color": NewbgColor,
                    "color": NewtextColor
                });

                $(".selected").text(NewTitleText);
                $(".selected").siblings('.topppings').text(Newtoppings);
                $('.singleitem').removeClass("selected");
            }
            else {
                evt.preventDefault();
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

function DeleteSize(value) {
    $.ajax({

        url: '/ManagementSection/DeleteSize',
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

function EditItem(value) {
    $.ajax({

        url: '/ManagementSection/EditItem',
        type: 'POST',
        data: JSON.stringify({
            item: value
        }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            alert(data);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

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
            $(".selectedTopping").css({
                "background-color": "pink",
                "color": "white"
            });
            $(".selectedTopping").text(data.Title);
            $(".selectedTopping").siblings("input").attr("id", data.Id);

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
            //$(".selected").css({
            //    "background-color": data.BackgroundColor,
            //    "color": data.TextColor
            //});
            $(".selected").text(data.Title);
            $(".selected").siblings("input").attr("id", data.Id);
            //alert('Data: ' + "Item Added");
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}