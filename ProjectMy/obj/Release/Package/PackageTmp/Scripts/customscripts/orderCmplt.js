/// <reference path="../jquery-3.1.1.min.js" />

$(document).ready(function () {

    var totalAmount = 0;
    $('.tableBody tr').each(function () {
        if (!this.rowIndex) return; // skip first row

        totalAmount = parseFloat(totalAmount) + parseFloat(this.cells[2].innerHTML);

    });
    $('#totalAmount').text(totalAmount.toFixed(2));
    $('#Subtotal').text(totalAmount.toFixed(2));


    $(function () {
        $("#discountDiv").dialog({
            autoOpen: false,
            width: 800,
            position: { my: 'top', at: 'top+20' },
            modal: true,
            buttons: {
                Ok: function () {

                    $('#discountDiv').dialog("close");
                },
                Cancel: function () {

                    $('#discountDiv').dialog("close");
                }
            },
            show: {
                effect: "blind",
                duration: 100
            },
            hide: {
                effect: "explode",
                duration: 100
            }
        });
    });

    $('#discountBtn').on('click', function () {
        $('#discountDiv').load("/Home/Discounts");
        $("#discountDiv").dialog("open");
    });

    $('#fullAmount').on("click", function () {

        $('#paidAmount').text(totalAmount.toFixed(2));
        var balance = parseFloat($('#paidAmount').text()) - parseFloat($('#totalAmount').text());

        $('#balance').text(balance.toFixed(2));
    });


    $('#cancelOrder').on('click', function (evt) {
        if (!confirm("Are you sure to Cancel this Order ?")) {
            evt.preventDefault();
        }
        //else {

        //}
    });

    $('#exit').on('click', function (evt) {
        if (confirm("Are you sure to Leave this Page ?")) {

        }
        else {
            evt.preventDefault();
        }
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
        
        if ($('#balance').text() === "") {
            evt.preventDefault();
            alert("Amount is not Cleared Yet...");
        }
        else {
            var ordersDetails = [];

            $('.tableBody tr').each(function () {
                if (!this.rowIndex) { return; }
                Quantity = this.cells[0].innerHTML;
                productName = this.cells[1].innerHTML;
                amount = this.cells[2].innerHTML;
                
                var item = { ItemName: productName, ItemQty: parseInt(Quantity), Amount: parseFloat(amount) };

                ordersDetails.push(item);
            });
            
            var totalpaid = $('#paidAmount').text();
            var totalAmount = $('#totalAmount').text();
            var balance = $('#balance').text();
            var discount = 0;
            var order = {
                OrderTypeName: 'Take Away', TotalAmount: parseFloat(totalAmount), TotalPaid: parseFloat(totalpaid), Discount: parseFloat(discount), Balance: parseFloat(balance), PaymentTypeId: 1
            }

            addOrder(ordersDetails, order);
        }
    });

    $('#print').on('click', function (evt) {

        if ($('#balance').text() === "") {
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
            var totalAmount1 = $('#totalAmount').text();
            var totalpaid = $('#paidAmount').text();
            var balance = $('#balance').text();
            var model1 = {
                ordersItems: itemsList,
                CustomerName: customerName,
                TotalAmount: parseFloat(totalAmount1),
                totalpaid: parseFloat(totalpaid),
                balance: parseFloat(balance)
            };
            PrintReciept(model1);
        }

        //if ($('#balance').text() == "") {
        //    evt.preventDefault();
        //    alert("Amount is not Paid Yet...");
        //}
        //else {
        //    var prtContent = document.getElementById('operationsArea');
        //    var WinPrint = window.open('', '', 'letf=100,top=100,width=600,height=600');
        //    WinPrint.document.write(prtContent.innerHTML);
        //    WinPrint.document.close();
        //    WinPrint.focus();
        //    WinPrint.print();
        //}
    });

});

function PrintReciept(model1) {

    $.ajax({
        url: '/Home/PrintOrder',
        type: 'POST',
        data: JSON.stringify({ model: model1 }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            alert(data);
        },
        error: function (request, error) {
            alert("request: " + error);
        }
    });
};
function addOrder(ordersDetails, order) {
   
    $.ajax({
        url: '/Home/ClearCash',
        type: 'POST',
        data: JSON.stringify({ 'ordersDetails': ordersDetails, 'order': order }),
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
