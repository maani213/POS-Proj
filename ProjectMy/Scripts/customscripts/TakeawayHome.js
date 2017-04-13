/// <reference path="../jquery-3.1.1.min.js" />


$(document).ready(function () {
    $('.singleitemBtn').on("click", function () {
        $('.singleitemBtn').removeClass("selected");
        $(this).addClass("selected");
        var price = $(this).val();
        var name = $(this).text();

        var item = $('<tr><td>1</td><td>' + name + '</td><td id="price">' + price + '</td></tr>');
        $('.tableBody').append(item);
        var totalAmount = $('#totalAmount').text();
        var newTotal = parseInt(totalAmount) + parseInt(price);

        $('#totalAmount').text('');
        $('#totalAmount').text(newTotal);
    });

    $('#orderCmplt').on("click", function () {

        var itemsList = [];

        $('.tableBody tr').each(function () {
            if (!this.rowIndex) return; // skip first row
            Quantity = this.cells[0].innerHTML;
            productName = this.cells[1].innerHTML;
            alert(productName);
            totalAmount = this.cells[2].innerHTML;
            alert(totalAmount);
            var item = { ItemName: productName, ItemQty: Quantity, ItemTotalPrice: totalAmount };

            itemsList.push(item);
        });


        addItem(itemsList);
    });
});
    function addItem(orders) {
        $.ajax({

            url: '/Home/OrderComplete',
            type: 'POST',
            data: JSON.stringify(orders),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                alert('Data: ' + data);
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    };

