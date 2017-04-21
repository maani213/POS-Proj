/// <reference path="../jquery-3.1.1.min.js" />


$(document).ready(function () {

    $('.singleitemBtn').on("click", function () {
        $('.singleitemBtn').removeClass("selected");
        $(this).addClass("selected");
        var price = $(this).val();
        var name = $(this).text();

        var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + price + '</td></tr>');
        $('.tableBody').append(item);
        var totalAmount = $('#totalAmount').text();
        var newTotal = parseInt(totalAmount) + parseInt(price);

        $('#totalAmount').text('');
        $('#totalAmount').text(newTotal);
    });

    $('#orderCmplt').on("click", function (evt) {

        var itemsList = [];
        
        $('.tableBody tr').each(function () {
            if (!this.rowIndex) return; // skip first row
            Quantity = this.cells[0].innerHTML;
            productName = this.cells[1].innerHTML;
            totalAmount = this.cells[2].innerHTML;

            var item = { ItemName: productName, ItemQty: Quantity, ItemTotalPrice: totalAmount };

            itemsList.push(item);
        });
        
        if (itemsList.length == 0)
        {
            alert("Please Select at least 1 item to place an order.");
            ev.preventDefault();
        }
        addItem(itemsList);
    });

    $("#row1").click(function () {
        alert("clicke");
        $(this).toggleClass("rowSelected");
    });
});

function addItem(orders) {
    $.ajax({

        url: '/Home/CompleteOrder',
        type: 'POST',
        data: JSON.stringify(orders),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            window.location.href = "/Home/OrderComplete";
        },
        error: function (request, error) {
            alert("request: " + json.stringify(request));
        }
    });
};

