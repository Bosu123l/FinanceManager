$(function () {
    loadListOfSourceAmount();
    loadListOfTypeOfAmount();
});
function OnClickOutgoingType() {
    var modelAddSourceOfAmount = document.getElementById("modelAddTypeOfAmount").value;

    $.ajax({
        url: "/FinancialManager/AddTypeOfOutgoing/",
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            "Name": modelAddSourceOfAmount
        }),
        dataType: "json",
        success: function (data) {
            document.getElementById("listOfTypeOfAmountsOutgoing").innerHTML = "";
            document.getElementById("modelAddTypeOfAmount").value = "";
            loadListOfTypeOfAmount();


        },
        error: function (result) {
            alert("Error");
        }
    });
}
function OnClickIncomingSouce() {
    var modelAddSourceOfAmount = document.getElementById("modelAddSourceOfAmount").value;

    $.ajax({
        url: "/FinancialManager/AddSourceOfAmount/",
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            "Name": modelAddSourceOfAmount
        }),
        dataType: "json",
        success: function (data) {
            document.getElementById("listOfSourceOfAmountsIncome").innerHTML = "";
            document.getElementById("modelAddSourceOfAmount").value = "";
            loadListOfSourceAmount();
        },
        error: function (result) {
            alert("Error");
        }
    });
}
function Clear(comboBox) {
    while (comboBox.options.length > 0) {
        comboBox.remove(0);
    }
}
function loadListOfTypeOfAmount() {
    $.ajax({
        url: "/FinancialBalance/GetTypeOfOutgoings/",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            var row = "";
            $.each(data,
                function (index, item) {
                    row += "<option " + "value=" + item.Id + ">" + item.Name + "</option>";
                });
            $('#listOfTypeOfAmountsOutgoing').html(row);
        },
        error: function (result) {
            alert("Error");
        }
    });
}
function loadListOfSourceAmount() {
    $.ajax({
        url: "/FinancialBalance/GetSourceOfAmounts/",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            var row = "";
            $.each(data,
                function (index, item) {
                    row += "<option " + "value=" + item.Id + ">" + item.Name + "</option>";
                });
            $('#listOfSourceOfAmountsIncome').html(row);
        },
        error: function (result) {
            alert("Error");
        }
    });
}
function OnAddClickOutgoing() {
    var AmountTextBox = document.getElementById('AmountTextBoxOutgoing').value;
    var DateDatePicker = document.getElementById('DateDatePickerOutgoing').value;
    var listOfTypeOfAmount = document.getElementById('listOfTypeOfAmountsOutgoing').value;
    var DescriptionTextBox = document.getElementById('DescriptionTextBoxOutgoing').value;

    $.ajax({
        url: "/FinancialManager/AddOutgoing/",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            "Amount": AmountTextBox.replace('.', ','),
            "Date": DateDatePicker,
            "TypeId": listOfTypeOfAmount,
            "Description": DescriptionTextBox
        }),
        dataType: "json",
        success: function (data) {
            
            ShowAlert("SuccessOutcomeAdded");
        }
    });
}
function OnAddClickIncomes() {
    var AmountTextBox = document.getElementById('AmountTextBoxIncome').value;
    var DateDatePicker = document.getElementById('DateDatePickerIncome').value;
    var listOfSourceOfAmounts = document.getElementById('listOfSourceOfAmountsIncome').value;
    var DescriptionTextBox = document.getElementById('DescriptionTextBoxIncome').value;

    $.ajax({
        url: "/FinancialManager/AddIncome/",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            "Amount": AmountTextBox.replace('.', ','),
            "Date": DateDatePicker,
            "SourceId": listOfSourceOfAmounts,
            "Description": DescriptionTextBox
        }),
        dataType: "json",
        success: function (data) {
            ShowAlert("SuccessIncomeAdded");
        }
    });
}

function ShowAlert(alertName) {
    var alert = document.getElementById(alertName);
    alert.alert();
}
