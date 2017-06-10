
$(document).ready(function () {

    $('#enter').on('click', function () {
        if ($('#searchtext').val() == "") {
            alert('Please enter a value');
        }
        else {

            $('#searchform').submit();
        }
    });

    $('#delivery').on('click', function () {
        $('#customerForm').attr('action','/DeliveryCollection/Delivery');// "@Url.Action("Delivery", "DeliveryCollection")");
        $('#customerForm').submit();
    });

    $('#collection').on('click', function () {
        $('#customerForm').attr('action','/DeliveryCollection/Collection');// "@Url.Action("Collection", "DeliveryCollection")");
        $('#customerForm').submit();
    });


    $('#repeatOrder').on('click', function (evt) {

        if ($('#Id').val() == 0) {
            alert('This Customer does not have any Previous order.')
        }
        else {
            $('#customerForm').attr('action','/DeliveryCollection/RepeatOrder');//"@Url.Action("RepeatOrder", "DeliveryCollection")");
            $('#customerForm').submit();
        }
    });
    $('#exitBtn').on('click', function () {
        window.location = '/Home/MainMenu';
    });

    $('input').on('click', function () {
        $('input').removeClass('inputselected');
        $(this).addClass('inputselected');
    });

    $('.KeyBoardBtn').on('click', function () {
        var text = $('.inputselected').val();
        //var text = $('#searchtext').val();
        var NewValue = text + $(this).text();
        //$('#searchtext').val(NewValue);
        $('.inputselected').val(NewValue);
    });

    $('#space').on('click', function () {
        var text = $('.inputselected').val();
        
        var NewValue = text + " ";
        
        $('.inputselected').val(NewValue);
    });

    $('.type').on('click', function () {
        $('.type').removeClass('selectedType');
        $(this).addClass('selectedType');
        var type = $(this).siblings('input').val();
        $('#searchType').val(type);
    });

    $('#deleteBtn').on('click', function () {
        var inputString = $('.inputselected').val();
        var shortenedString = inputString.substr(0, (inputString.length - 1));
        $('.inputselected').val(shortenedString);
    });

});
