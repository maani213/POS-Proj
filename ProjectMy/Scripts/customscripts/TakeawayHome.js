/// <reference path="../jquery-3.1.1.min.js" />




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

    var isFree = 0;
    var finalName = "";
    var FinalPrice = 0.00;

    $(function () {
        $("#dialog").dialog({
            autoOpen: false,
            height: 600,
            width: 750,
            modal: true,
            buttons: {
                "Add Selected Toppings": function () {

                    if (isFree === 0) {
                        AddOrderItem();
                    }
                    else if (isFree === 1 || isFree === 2) {
                        AddPizzas();
                    }
                    $(this).dialog("close");
                    //clearSelections
                },
                Cancel: function () {
                    clearSelections();
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
                "Ok": function () {
                    //GetSetToppingsPrice();
                    $(this).dialog("close");
                },
                Cancel: function () {
                    clearSelections();
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
    $(function () {
        $("#dialog-message").dialog({
            autoOpen: false,
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    });
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

    $(document).on("click", '.pizzItem', function () {
        isFree++;

        $('.singleitem').removeClass("selected");
        $('.pizzItem').removeClass("selected");

        $(this).addClass("selected");
        var catId = $(this).siblings('input').val();
        if (isFree === 1) {
            $("#sizesDialog").dialog("open");
        }

        if (isFree === 2) {
            $("#dialog").dialog("open");
        }

    });

    $(document).on("click", '.sizeBtn', function () {
        $('.sizeBtn').removeClass("SizeSelected");
        $(this).addClass("SizeSelected");

        var extrasCount = $('.Extraitem').length;

        if (extrasCount === 0) {
            var name = $('.selected').text();

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
            $('#sizesDialog').dialog("close");
        }
        else {
            $('#sizesDialog').dialog("close");
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

    //$(document).on("click", '#editPrice', function () {
    //    var price1 = $('.rowSelected').find('#price').text();
    //    $('#price').html('<input class="thVal" maxlength="4" type="text" width="2" value="" />');
    //    //$(".thVal").val(price1);
    //    $(".thVal").focus();
    //    $(".thVal").keyup(function (event) {
    //        if (event.keyCode == 13) {
    //            $(currentEle).html($(".thVal").val().trim());
    //            //$(currentEle).html($(".thVal").val().trim());
    //        }
    //    });
    //});
    $(document).on("click", '#plus', function () {
        var price = $('.rowSelected').find('#price').text();
        var qty = $('.rowSelected').find('#qty').text();
        var amountToAdd = parseFloat(price) / qty;
        var newPrice = parseFloat(price) + amountToAdd;

        var qty = $('.rowSelected').find('#qty').text();
        var newQty = parseInt(qty) + 1;
        $('.rowSelected').find('#qty').text(newQty);
        $('.rowSelected').find('#price').text(newPrice.toFixed(2));

    });

    $(document).on("click", '#minus', function (evt) {

        var qty = $('.rowSelected').find('#qty').text();
        var newQty = parseInt(qty) - 1;
        var price = $('.rowSelected').find('#price').text();
        var amountToMinus = parseFloat(price) / qty;
        var newPrice = parseFloat(price) - amountToMinus;

        if (newQty === 0) {

            if (confirm("Are you sure to Delete this Item ?")) {
                $('.rowSelected').empty();
            }
            else {
                evt.preventDefault();
            }
        }
        else {
            $('.rowSelected').find('#price').text(newPrice.toFixed(2));
            $('.rowSelected').find('#qty').text(newQty);
        }
        $(this).removeClass('rowSelected');

    });

    $(document).on("click", "#row1", function () {
        $('.rowSelected').removeClass("rowSelected");
        $(this).toggleClass("rowSelected");
    });
    $(document).on("click", '.Extraitem', function () {
        $(this).toggleClass("selectedTopping");
    });
    function AddPizzas() {

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
                    if (isFree === 1) {
                        FirstPrice = price;
                        finalName = finalName + name;
                        price = 0.00;
                        name = "";
                    }
                    else {
                        var totalAmount = $('#totalAmount').text();
                        var newTotal = 0.00;
                        var newName = finalName + " + " + name;
                        if (price > FirstPrice) {

                            var item = $('<tr id="row1"><td id="qty">1</td><td>' + newName + '</td><td id="price">' + price.toFixed(2) + '</td></tr>');
                            newTotal = parseFloat(totalAmount) + parseFloat(price);
                        }
                        else if (price === FirstPrice) {

                            var item = $('<tr id="row1"><td id="qty">1</td><td>' + newName + '</td><td id="price">' + price.toFixed(2) + '</td></tr>');
                            newTotal = parseFloat(totalAmount) + parseFloat(price);
                        }
                        else {
                            var item = $('<tr id="row1"><td id="qty">1</td><td>' + newName + '</td><td id="price">' + FirstPrice.toFixed(2) + '</td></tr>');
                            newTotal = parseFloat(totalAmount) + parseFloat(FirstPrice);
                        }
                        //var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + price.toFixed(2) + '</td></tr>');
                        $('.tableBody').append(item);
                        if (isFree === 2) {
                            isFree = 0;
                            finalName = "";
                            FinalPrice = 0.00;


                        }
                        //var totalAmount = $('#totalAmount').text();
                        //var newTotal = parseFloat(totalAmount) + parseFloat(price);

                        $('#totalAmount').text('');
                        $('#totalAmount').text(newTotal.toFixed(2));
                    }
                },
                error: function (request, error) {
                    alert("request:" + error);
                }
            });
        }
        else {
            if (isFree === 1) {
                FirstPrice = price;
                finalName = finalName + name;
            }
            else {
                var totalAmount = $('#totalAmount').text();
                var newTotal = 0.00;
                var newName = finalName + " + " + name;
                if (price > FirstPrice) {
                    var item = $('<tr id="row1"><td id="qty">1</td><td>' + newName + '</td><td id="price">' + price.toFixed(2) + '</td></tr>');
                    newTotal = parseFloat(totalAmount) + parseFloat(price);
                }
                else if (price === FirstPrice) {

                    var item = $('<tr id="row1"><td id="qty">1</td><td>' + newName + '</td><td id="price">' + price.toFixed(2) + '</td></tr>');
                    newTotal = parseFloat(totalAmount) + parseFloat(price);
                }
                else {
                    var item = $('<tr id="row1"><td id="qty">1</td><td>' + newName + '</td><td id="price">' + FirstPrice.toFixed(2) + '</td></tr>');
                    newTotal = parseFloat(totalAmount) + parseFloat(FirstPrice);
                }
                if (isFree === 2) {
                    isFree = 0;
                    finalName = "";
                    FinalPrice = 0.00;
                }
                //var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + price.toFixed(2) + '</td></tr>');
                $('.tableBody').append(item);
                //var totalAmount = $('#totalAmount').text();
                //var newTotal = parseFloat(totalAmount) + parseFloat(price);

                $('#totalAmount').text('');
                $('#totalAmount').text(newTotal.toFixed(2));
            }
            if (isFree === 1) {
                $('#dialog-message').dialog("open");
                $('.singleitem').removeClass("selected");
                $('.Extraitem').removeClass('selectedTopping');
            }
            else {

                $('.singleitem').removeClass("selected");
                $('.sizeBtn').removeClass("SizeSelected");
                $('.Extraitem').removeClass('selectedTopping');
            }
        }
    }
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
            var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + data.toFixed(2) + '</td></tr>');
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

        var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + data.toFixed(2) + '</td></tr>');
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
        var item = $('<tr id="row1"><td id="qty">1</td><td>' + name + '</td><td id="price">' + price.toFixed(2) + '</td></tr>');
        $('.tableBody').append(item);
        var totalAmount = $('#totalAmount').text();
        var newTotal = parseFloat(totalAmount) + parseFloat(price);

        $('#totalAmount').text('');
        $('#totalAmount').text(newTotal.toFixed(2));
    }
    $('.selected').removeClass('selected');
    $(".SizeSelected").removeClass('SizeSelected');

}

function clearSelections() {
    $('.singleitem').removeClass("selected");
    $('.sizeBtn').removeClass("SizeSelected");
    $('.Extraitem').removeClass('selectedTopping');
}

