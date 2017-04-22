﻿/// <reference path="../jquery-3.1.1.min.js" />


$(document).ready(function () {

    $('#canelOrder').on("click", function (evt) {
        if ($('.table tbody').find('tr').length > 0) {
            if (confirm('Are you sure to Cancel Order?')) {
                $('.table tbody').empty();
                $('#totalAmount').text(0);
            }
            else {
                evt.preventDefault();
            }
        }
        else {
            alert('Order is already empty.');
        }

    });
    $(document).on("click", '.singleitem', function () {
        $('.singleitem').removeClass("selected");
        $(this).addClass("selected");
        if($('.sizeBtn').length===0)
        {
            var name = $('.selected').text();
            if (name.length > 2) {
                name = $('.selected').text();
                var itemID = $('.selected').attr('id');
                var size = 1;
                GetItemPrice(name, itemID, size);
            }
        }
    });

    $(document).on("click", '.sizeBtn', function () {
        $('.sizeBtn').removeClass("SizeSelected");
        $(this).addClass("SizeSelected");
        var name = $('.selected').text();
        if (name.length > 2) {
            name = name + "-" + $('.SizeSelected').text();
            var itemID = $('.selected').attr('id');
            var size = $(this).siblings('input').val();
            GetItemPrice(name, itemID, size);
        }
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

        if (itemsList.length === 0) {
            alert("Please Select at least 1 item to place an order.");
            ev.preventDefault();
        }
        addItem(itemsList);
    });

    $(document).on("click", "#row1", function () {

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
function GetItemPrice(name, itemId, size) {
    $.get('/Home/GetItemPrice/?' + "itemID=" + itemId + "&sizeId=" + size, function (data) {

        var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + data + '</td></tr>');
        $('.tableBody').append(item);
        var totalAmount = $('#totalAmount').text();
        var newTotal = parseFloat(totalAmount) + parseFloat(data);

        $('#totalAmount').text('');
        $('#totalAmount').text(newTotal.toFixed(2));
        $('.selected').removeClass('selected');
    })
    //$.ajax({
    //    url: '/Home/GetItemPrice/?' + "itemID=" + itemId + "&sizeId=" + size,
    //    type: 'GET',
    //    success: function (data) {
    //        json.stringify(data)
    //        return data;
    //    },
    //    error: function (request, error) {
    //        //alert("request: " + json.stringify(request));
    //    }
    //});
};

