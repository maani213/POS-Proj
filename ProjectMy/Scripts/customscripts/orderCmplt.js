/// <reference path="../jquery-3.1.1.min.js" />

$(document).ready(function () {

    var totalAmount = 0;
    $('.tableBody tr').each(function () {
        if (!this.rowIndex) return; // skip first row

        totalAmount = totalAmount + parseInt(this.cells[2].innerHTML);

    });
    $('#totalAmount').text(totalAmount);
    $('#Subtotal').text(totalAmount);


    $('#fullAmount').on("click", function () {

        $('#paidAmount').text(totalAmount);
        var balance = parseInt($('#paidAmount').text()) - parseInt($('#totalAmount').text());

        $('#balance').text(balance);
    });

    $('.CustomfBtn').on("click", function () {
        var amount = $('#paidAmount').text();
        var numb = $(this).find("span").text();
        $('#paidAmount').text(numb);
        var balance = parseInt($('#paidAmount').text()) - parseInt($('#totalAmount').text());

        $('#balance').text(balance);
    });

    $('.CustomPercBtn').on("click", function () {
        var amount = $('#paidAmount').text();
        var numb = $(this).find("span").text();

        var calculated = (numb / 100) * totalAmount;

        $('#paidAmount').text(calculated);
        var balance = parseInt($('#paidAmount').text()) - parseInt($('#totalAmount').text());

        $('#balance').text(balance);
    });


    $('#doublezero').on("click", function () {
        var paidamount = $('#paidAmount').text();
        var numb = $(this).text();
        
        var newAmount = 0;
        if (parseInt(paidamount) > 0) {
            var newAmount = paidamount + numb;
            $('#paidAmount').text(newAmount);
            var balance = parseInt($('#paidAmount').text()) - parseInt($('#totalAmount').text());

            $('#balance').text(balance);
        }


    });

    $('.calculatorBtn').on("click", function () {
        var amount = $('#paidAmount').text();
        var numb = $(this).text();
        var newAmount = amount + numb;
        $('#paidAmount').text(newAmount);
        var balance = parseInt($('#paidAmount').text()) - parseInt($('#totalAmount').text());

        $('#balance').text(balance);
    })

    $('#clearCash').on("click", function () {

        var itemsList = [];

        $('.tableBody tr').each(function () {
            if (!this.rowIndex) return; // skip first row
            Quantity = this.cells[0].innerHTML;
            productName = this.cells[1].innerHTML;
            totalAmount = this.cells[2].innerHTML;

            var item = { ItemName: productName, ItemQty: Quantity, ItemTotalPrice: totalAmount };

            itemsList.push(item);
        });
        var totalpaid = $('#paidAmount').text();
        var balance = $('#balance').text();

        addItem(itemsList,totalpaid,balance);
    });

});
function addItem(orders , totalpaid , balance) {
    $.ajax({

        url: '/Home/ClearCash',
        type: 'POST',
        data: JSON.stringify({ orders, totalpaid, balance }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            window.location.href = "/Home/TakeAway";
        },
        error: function (request, error) {
            alert("request: " + json.stringify(request));
        }
    });
};