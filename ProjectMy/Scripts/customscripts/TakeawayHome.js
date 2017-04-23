/// <reference path="../jquery-3.1.1.min.js" />

$(function () {
    $("#dialog").dialog({
        autoOpen: false,
        height: 600,
        width: 750,
        modal: true,
        buttons: {
            "Add Selected Toppings": function () {
                AddOrderItem();
                $(this).dialog("close");
                clearSelections
            },
            Cancel: function () {
                $(this).dialog("close");
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
    $("#sizesDialog").dialog({
        autoOpen: false,
        height: 350,
        width: 350,
        modal: true,
        buttons: {
            "Add Selected Toppings": function () {
                //GetSetToppingsPrice();
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
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
    //$("#toppings").on("click", function () {
    //    $("#dialog").dialog("open");
    //});
});


$(document).ready(function () {

    //$.ajax({
    //    url: '/Home/GetSizes',
    //    type: 'GET',
    //    data: { categoryId: 1 },
    //    traditional: true,
    //    success: function (data) {
    //        $('#sizesDialog').html(data);
    //    },
    //    error: function (request, error) {
    //        alert("request:" + error);
    //    }

    //});

    //$.ajax({
    //    async: false,
    //    url: '/Home/GetExtras',
    //    type: 'GET',
    //    data: { categoryId: 1 },
    //    traditional: true,
    //    success: function (data) {
    //        $('#dialog').html(data);
    //    },
    //    error: function (request, error) {
    //        alert("request:" + error);
    //    }

    //});




    $(".customBttncat").on("click", function (e) {
        var catId = $(this).siblings('input').val();
        $.ajax({
            async: false,
            url: '/Home/TakeAwayItems',
            type: 'GET',
            data: { categoryId: catId },
            traditional: true,
            success: function (data) {
                $('#TakeAwayitemsArea').html(data);
            },
            error: function (request, error) {
                alert("request:" + error);
            }

        });

        $.ajax({
            async: false,
            url: '/Home/GetSizes',
            type: 'GET',
            data: { categoryId: catId },
            traditional: true,
            success: function (data) {
                $('#sizesDialog').html(data);
            },
            error: function (request, error) {
                alert("request:" + error);
            }

        });
        $.ajax({
            async: false,
            url: '/Home/GetExtras',
            type: 'GET',
            data: { categoryId: catId },
            traditional: true,
            success: function (data) {
                $('#dialog').html(data);
            },
            error: function (request, error) {
                alert("request:" + error);
            }

        });

    });


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
        var catId = $(this).siblings('input').val();

        var sizecount = $('.sizeBtn').length;
        var extrasCount = $('.Extraitem').length;

        if (sizecount === 0 && extrasCount === 0) {
            var name = $('.selected').text();
            name = $('.selected').text();
            var itemID = $('.selected').attr('id');
            var size = 1;
            GetItemPrice(name, itemID, size);
            $('.selected').removeClass('selected');
        }

        else {
            $("#sizesDialog").dialog("open");
        }

    });

    $(document).on("click", '.sizeBtn', function () {
        $('.sizeBtn').removeClass("SizeSelected");
        $(this).addClass("SizeSelected");
        $('#sizesDialog').dialog("close");
        var extrasCount = $('.Extraitem').length;

        if (extrasCount === 0) {
            var name = $('.selected').text();
            alert($('.SizeSelected').length);
            name = name + "-" + $('.SizeSelected').text();
            
            var itemId = $('.selected').attr('id');
            var size = $(".SizeSelected").siblings('input').val();
            var price = 0;
            $.ajax({
                async: false,
                url: '/Home/GetItemPrice',
                type: 'GET',
                data: { itemID: itemId, sizeId: size },
                dataType: "json",
                traditional: true,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    price = parseFloat(price) + parseFloat(data);
                    
                },
                error: function (request, error) {
                    alert("request:" + error);
                }

            });
            var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + price + '</td></tr>');
            $('.tableBody').append(item);
            var totalAmount = $('#totalAmount').text();
            var newTotal = parseFloat(totalAmount) + parseFloat(price);

            $('#totalAmount').text('');
            $('#totalAmount').text(newTotal.toFixed(2));
            $('.singleitem').removeClass("selected");
            $('.sizeBtn').removeClass("SizeSelected");
        }
        else {
            $("#dialog").dialog("open");
        }
        //var name = $('.selected').text();
        //if (name.length > 2) {
        //    name = name + "-" + $('.SizeSelected').text();
        //    var itemID = $('.selected').attr('id');
        //    var size = $(this).siblings('input').val();
        //    //GetItemPrice(name, itemID, size);
        //}
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
    $(document).on("click", '.Extraitem', function () {
        $(this).toggleClass("selectedTopping");
    });

});
function GetSetToppingsPrice() {
    var name = "Toppings - ";
    var ids = [];
    $('.selectedTopping').each(function () {
        name = name + $(this).text() + " , ";
        var eId = $(this).siblings('input').val();
        ids.push(eId);
        $(this).removeClass('selectedTopping');
    });
    var size = $(".SizeSelected").siblings('input').val();

    //GetItemPrice(name, itemID, size);

    //$.get('/Home/GetExtrasPrices/?' + "toppingIds=" + ids + "&sizeId=" + size, function (data) {
    //$.get('/Home/GetExtrasPrices', {toppingIds : ids , sizeId:size}, function (data) {
    //    

    //})
    $.ajax({

        url: '/Home/GetExtrasPrices',
        type: 'GET',
        data: { toppingIds: ids, sizeId: size },
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + data + '</td></tr>');
            $('.tableBody').append(item);
            var totalAmount = $('#totalAmount').text();
            var newTotal = parseFloat(totalAmount) + parseFloat(data);

            $('#totalAmount').text('');
            $('#totalAmount').text(newTotal.toFixed(2));
        },
        error: function (request, error) {
            alert("request:" + error);
        }
    });

}

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

};

function AddOrderItem() {
    var name = $('.selected').text();
    name = name + "-" + $('.SizeSelected').text() + " ";
    var itemId = $('.selected').attr('id');
    var size = $(".SizeSelected").siblings('input').val();
    var price = 0;
    $.ajax({
        async: false,
        url: '/Home/GetItemPrice',
        type: 'GET',
        data: { itemID: itemId, sizeId: size },
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            price = parseFloat(price) + parseFloat(data);
        },
        error: function (request, error) {
            alert("request:" + error);
        }

    });

    var ids = [];
    $('.selectedTopping').each(function () {
        name = name + $(this).text() + " , ";
        var eId = $(this).siblings('input').val();
        ids.push(eId);
        $(this).removeClass('selectedTopping');
    });
    if (ids.length > 0) {
        //name = name + "  +  ";
        $.ajax({
            async: false,
            url: '/Home/GetExtrasPrices',
            type: 'GET',
            data: { toppingIds: ids, sizeId: size },
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                price = parseFloat(price.toFixed(2)) + parseFloat(data.toFixed(2));
                var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + price.toFixed(2) + '</td></tr>');
                $('.tableBody').append(item);
                var totalAmount = $('#totalAmount').text();
                var newTotal = parseFloat(totalAmount) + parseFloat(price);

                $('#totalAmount').text('');
                $('#totalAmount').text(newTotal.toFixed(2));
            },
            error: function (request, error) {
                alert("request:" + error);
            }
        });
    }
    else {
        var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + price + '</td></tr>');
        $('.tableBody').append(item);
        var totalAmount = $('#totalAmount').text();
        var newTotal = parseFloat(totalAmount) + parseFloat(price);

        $('#totalAmount').text('');
        $('#totalAmount').text(newTotal.toFixed(2));
    }
}

function clearSelections() {
    $('.singleitem').removeClass("selected");
    $('.sizeBtn').removeClass("SizeSelected");
    $('.selectedTopping').removeClass('selectedTopping');
}

