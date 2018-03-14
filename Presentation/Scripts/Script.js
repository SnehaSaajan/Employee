
$(document).ready(function () {
    $("#FromDate").hide();
    $("#ToDate").hide();
    $("#Value").hide();
    $("#Designation").hide();
    $("#Band").hide();
    $("#Field").change(ChooseOption);
    $("#Field").click(ChooseOption);
    function ChooseOption() {        
        var value = $('#Field').val();
        $("#Type option[value='0']").show();
        $("#Type option[value='1']").show();
        $("#Type option[value='2']").show();
        $("#Type option[value='3']").show();
        $("#Type option[value='4']").show();
        $("#Type option[value='5']").show();
        $("#Type option[value='6']").show();
        $("#Type option[value='7']").show();
        if (value == '0') {
            $("#Value").show();
            $("#FromDate").hide();
            $("#ToDate").hide();          
            $("#Designation").hide();
            $("#Band").hide();
            $("#Type option[value='6']").hide();
            $("#Type option[value='7']").hide();
        }

        else if (value == '1') {
            $("#Value").show();
            $("#FromDate").hide();
            $("#ToDate").hide();
            $("#Designation").hide();
            $("#Band").hide();
            $("#Type option[value='0']").hide();
            $("#Type option[value='1']").hide();
            $("#Type option[value='2']").hide();
            $("#Type option[value='3']").hide();
            $("#Type option[value='7']").hide();
        }

        else if (value == '2') {
            $("#FromDate").show();
            $("#ToDate").hide();
            $("#Value").hide();
            $("#Designation").hide();
            $("#Band").hide();
            $("#Type option[value='5']").hide();
            $("#Type option[value='6']").hide();
        }

        else if (value == '3') {
            $("#Designation").show();
            $("#FromDate").hide();
            $("#ToDate").hide();
            $("#Value").hide();
            $("#Band").hide();
            $("#Type option[value='0']").hide();
            $("#Type option[value='1']").hide();
            $("#Type option[value='2']").hide();
            $("#Type option[value='3']").hide();
            $("#Type option[value='6']").hide();
            $("#Type option[value='7']").hide();
        }

        else if (value == '4') {
            $("#Band").show();
            $("#FromDate").hide();
            $("#ToDate").hide();
            $("#Value").hide();
            $("#Designation").hide();
            $("#Type option[value='0']").hide();
            $("#Type option[value='1']").hide();
            $("#Type option[value='2']").hide();
            $("#Type option[value='3']").hide();
            $("#Type option[value='6']").hide();
            $("#Type option[value='7']").hide();
        }


    }
    $('#Type').change(function () {
        var value = $('#Type').val();
        if (value == '7') {
            $('#ToDate').show();

        }
        else {

            $('#ToDate').hide();
        }

    });

});

