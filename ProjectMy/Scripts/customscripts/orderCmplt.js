/// <reference path="../jquery-3.1.1.min.js" />

$(document).ready(function () {

    var totalAmount = 0;
    $('.tableBody tr').each(function () {
        if (!this.rowIndex) return; // skip first row

        totalAmount = parseFloat(parseFloat(totalAmount) + parseFloat(this.cells[2].innerHTML));

    });
    $('#totalAmount').text(totalAmount.toFixed(2));
    $('#Subtotal').text(totalAmount.toFixed(2));


    $('#fullAmount').on("click", function () {

        $('#paidAmount').text(totalAmount.toFixed(2));
        var balance = parseFloat($('#paidAmount').text()) - parseFloat($('#totalAmount').text());

        $('#balance').text(balance.toFixed(2));
    });

    $('.CustomfBtn').on("click", function () {
        var amount = $('#paidAmount').text();
        var numb = $(this).find("span").text();
        $('#paidAmount').text(numb);
        var balance = parseFloat($('#paidAmount').text()) - parseFloat($('#totalAmount').text());

        $('#balance').text(balance.toFixed(2));
    });

    $('.CustomPercBtn').on("click", function () {
        var amount = $('#paidAmount').text();
        var numb = $(this).find("span").text();

        var calculated = (numb / 100) * totalAmount;

        $('#paidAmount').text(calculated);
        var balance = parseFloat($('#paidAmount').text()) - parseFloat($('#totalAmount').text());

        $('#balance').text(balance.toFixed(2));
    });


    $('#doublezero').on("click", function () {
        var paidamount = $('#paidAmount').text();
        var numb = $(this).text();

        var newAmount = 0;
        if (parseFloat(paidamount) > 0) {
            var newAmount = paidamount + numb;
            $('#paidAmount').text(newAmount.toFixed(2));
            var balance = parseFloat($('#paidAmount').text()) - parseFloat($('#totalAmount').text());

            $('#balance').text(balance.toFixed(2));
        }


    });

    $('.calculatorBtn').on("click", function () {
        var amount = $('#paidAmount').text();
        var numb = $(this).text();
        var newAmount = amount + numb;
        $('#paidAmount').text(newAmount);
        var balance = parseFloat($('#paidAmount').text()) - parseFloat($('#totalAmount').text());

        $('#balance').text(balance.toFixed(2));
    })

    $('#clearCash').on("click", function (evt) {
        if ($('#balance').text()=="")
        {
            evt.preventDefault();
            alert("Amount is not Cleared Yet...");
        }
        else {
            var itemsList = [];

            $('.tableBody tr').each(function () {
                if (!this.rowIndex) return; // skip first row
                Quantity = this.cells[0].innerHTML;
                productName = this.cells[1].innerHTML;
                totalAmount = this.cells[2].innerHTML;

                var item = { ItemName: productName, ItemQty: Quantity, ItemTotalPrice: totalAmount };

                itemsList.push(item);
            });
            var customerName = $('#CustomerName').val();
            var totalpaid = $('#paidAmount').text();
            var balance = $('#balance').text();
            
            addItem(itemsList, totalpaid, balance, customerName);
        }
    });

    $('#print').on('click', function (evt) {
        if ($('#balance').text() == "") {
            evt.preventDefault();
            alert("Amount is not Paid Yet...");
        }
        else {
            var prtContent = document.getElementById('operationsArea');
            var WinPrint = window.open('', '', 'letf=100,top=100,width=600,height=600');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
    })

});
function addItem(itemList, totalpaid1, balance1, customerName1) {
    
    $.ajax({
        url: '/Home/ClearCash',
        type: 'POST',
        data: JSON.stringify({ 'orders': itemList, 'CustomerName': customerName1, 'totalpaid': totalpaid1, 'balance': balance1 }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            window.location.href = "/Home/TakeAway";
        },
        error: function (request, error) {
            alert("request: " + error);
        }
    });
};
