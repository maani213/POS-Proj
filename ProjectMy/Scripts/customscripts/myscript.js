/// <reference path="../jquery-3.1.1.min.js" />


$(document).ready(function () {
    $('.singleitem').on("click", function () {
        $('.singleitem').removeClass("selected");

        $(this).addClass("selected");
    });

    $('.customBttn').on("click", function () {
        $('.customBttn').removeClass("DeptSelected");
        $(this).addClass("DeptSelected");
        var dept = $('.DeptSelected').html();

        $('.title').text(dept);
    });
    $('#addItem').on("click", function () {

        var textColor = $('#textcolor').val();
        var bgColor = $('#btncolor').val();
        var price = $('#price').val();
        var isBold = $('#isBold').is(':checked');
        var isItalic = $('#isitalic').is(':checked');

        var TitleText = $('#text').val();
        var description = $('#desc').val();
        var textStyle = $('#fontstyle').val();
        
        if(TitleText!=""&&bgColor!="")
        {
            var html = $('<div class="col-md-2 singleitem" style="background-color:' + bgColor + ';">' + TitleText + '</div>');
            $('.itemsArea').append(html);
            var item = { Title: TitleText, Description: description, Price: price, BackgroundColor: bgColor, TextStyle: textStyle, Size: "1" };
            //addItem(item);
        }
        else {
            alert("enter information");
        }
        
    });
    $('#deleteItem').on("click", function () {
        $('div').remove(".selected");
    });

})

function addItem(data1) {
    $.ajax({

        url: '/Home/AddItem',
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