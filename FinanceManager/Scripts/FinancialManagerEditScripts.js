var IdIncomeForEditing = 0;
var IdOutgoingForEditing = 0;
document.getElementById('buttonComfirm')
       .addEventListener('click',
           function () {
               checkSelectedRadioButton();
           });

function OnEditClickConfirmOutgoings() {
    var AmountTextBox = document.getElementById('AmountTextBoxOutgoing').value;
    var DateDatePicker = document.getElementById('DateDatePickerOutgoing').value;
    var listOfSourceOfAmounts = document.getElementById('listOfSourceOfAmountsOutgoing').value;
    var DescriptionTextBox = document.getElementById('DescriptionTextBoxOutgoing').value;

    $.ajax({
        url: "/FinancialManager/UpdateOutgoing/",
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            "Id": IdOutgoingForEditing,
            "Amount": AmountTextBox.replace('.', ','),
            "Date": DateDatePicker,
            "TypeId": listOfSourceOfAmounts,
            "Description": DescriptionTextBox
        }),
        dataType: "json",
        success: function (data) {
            checkSelectedRadioButton();
        },
        error: function (result) {
            $('.alert').show();
        }
    });
}
function OnEditClickConfirmIncomes() {
    var AmountTextBox = document.getElementById('AmountTextBoxIncome').value;
    var DateDatePicker = document.getElementById('DateDatePickerIncome').value;
    var listOfSourceOfAmounts = document.getElementById('listOfSourceOfAmountsIncome').value;
    var DescriptionTextBox = document.getElementById('DescriptionTextBoxIncome').value;

    $.ajax({
        url: "/FinancialManager/UpdateIncome/",
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            "Id": IdIncomeForEditing,
            "Amount": AmountTextBox.replace('.', ','),
            "Date": DateDatePicker,
            "SourceId": listOfSourceOfAmounts,
            "Description": DescriptionTextBox
        }),
        dataType: "json",
        success: function (data) {
            checkSelectedRadioButton();
        },
        error: function (result) {
            $('.alert').show();
        }
    });
}

function OnEditClickOutgoingForEdit(id) {
    IdOutgoingForEditing = id;

    $.ajax({
        url: "/FinancialBalance/GetOutgoing/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {

            document.getElementById("AmountTextBoxOutgoing").value = data.Amount;
            document.getElementById("DateDatePickerOutgoing").value = ConvertDataYearFirst(data.Date);
            document.getElementById("listOfSourceOfAmountsOutgoing").value = data.TypeId;
            document.getElementById("DescriptionTextBoxOutgoing").value = checkIfEmpty(data.Description);
        },
        error: function (result) {
            alert("Error");
        }
    });


    alert(IdOutgoingForEditing);
}
function OnEditClickIncomingForEdit(id) {
    IdIncomeForEditing = id;
    $.ajax({
        url: "/FinancialBalance/GetIncome/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {

            document.getElementById("AmountTextBoxIncome").value = data.Amount;
            document.getElementById("DateDatePickerIncome").value = ConvertDataYearFirst(data.Date);
            document.getElementById("listOfSourceOfAmountsIncome").value = data.SourceId;
            document.getElementById("DescriptionTextBoxIncome").value = checkIfEmpty(data.Description);
        },
        error: function (result) {
            alert("Error");
        }
    });
}

function loadListOfSourceAmountOutgoing() {
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
            $('#listOfSourceOfAmountsOutgoing').html(row);
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
function checkSelectedRadioButton() {

    var firstRadioButton = document.getElementById('firstRadioButton');
    var secondRadioButton = document.getElementById('secondRadioButton');
    var thirdRadioButton = document.getElementById('thirdRadioButton');

    var firstDate = document.getElementById('dayFirstFilter').value;
    var secondDate = document.getElementById('daySecondFilter').value;

    if (firstRadioButton.checked) {
        incomingDateFilter(firstDate, secondDate);
        outgoingDateFilter(firstDate, secondDate);
    } else if (secondRadioButton.checked) {
        OutgoingLastTimeFilter();
        IncomingLastTimeFilter();
    } else if (thirdRadioButton.checked) {
        IncomingsByLastOperations();
        OutGoingsByLastOperations();
    }

}

function GetSumOfOutgoings() {
    $.ajax({
        url: "FinancialBalance/SumOfOutgoings",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {
            document.getElementById('labelSumOfOutgoings').innerHTML = "Suma to: " + AmountWithUnit(data);
        },
        error: function (result) {
            alert("Error");
        }
    });
}
function GetSumOfIncomes() {
    $.ajax({
        url: "FinancialBalance/SumOfIncomings",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {
            document.getElementById('labelSumOfIncomes').innerHTML = "Suma to: " + AmountWithUnit(data);
        },
        error: function (result) {
            alert("Error");
        }
    });
}

$(function () {
    checkSelectedRadioButton();
    loadListOfSourceAmount();
    loadListOfSourceAmountOutgoing();
});

function IncomingsByLastOperations() {

    var counterOfLastOperations = document.getElementById('counterOfLastOperations').value;

    $.ajax({
        url: "/FinancialBalance/GetIncomesByLastOperations/?count=" + counterOfLastOperations,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            fillTableIncoming(data, "#tBodyIncomes");
        },
        error: function (result) {
            alert("Error");
        }
    });
}

function OutGoingsByLastOperations() {
    var counterOfLastOperations = document.getElementById('counterOfLastOperations').value;
    $.ajax({
        url: "/FinancialBalance/GetOutgoingsByLastOperations/?count=" + counterOfLastOperations,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            fillTableOutgoings(data, "#tBodyOutgoings");
        },
        error: function (result) {
            alert("Error");
        }
    });

}

function OutgoingLastTimeFilter() {

    var timeDiffValue = document.getElementById('difTimeSelect').value;
    var selectedEndPoint = "";
    switch (timeDiffValue) {
        case "days":
            selectedEndPoint = "GetOutgoingsByNumberOfDays/?days=";
            break;
        case "weeks":
            selectedEndPoint = "GetOutgoingsByNumberOfWeeks/?weeks=";
            break;
        case "months":
            selectedEndPoint = "GetOutgoingsByNumberOfMonth/?month=";
            break;
    }
    var selectableFilterValue = document.getElementById('selectableFilterValue').value;

    $.ajax({
        url: "FinancialBalance/" + selectedEndPoint + selectableFilterValue,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            fillTableOutgoings(data, "#tBodyOutgoings");
        },
        error: function (result) {
            alert("Error");
        }
    });

}
function IncomingLastTimeFilter() {

    var timeDiffValue = document.getElementById('difTimeSelect').value;
    var selectedEndPoint = "";
    switch (timeDiffValue) {
        case "days":
            selectedEndPoint = "GetIncomingsByNumberOfDays/?days=";
            break;
        case "weeks":
            selectedEndPoint = "GetIncomesByNumberOfWeeks/?weeks=";
            break;
        case "months":
            selectedEndPoint = "GetIncomingsByNumberOfMonth/?month=";
            break;
    }
    var selectableFilterValue = document.getElementById('selectableFilterValue').value;

    $.ajax({
        url: "/FinancialBalance/" + selectedEndPoint + selectableFilterValue,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            fillTableIncoming(data, "#tBodyIncomes");
        },
        error: function (result) {
            alert("Error");
        }
    });

}
function fillTableIncoming(data, tableName) {


    var row = "";
    $.each(data,
        function (index, item) {

            var buttonEdit = document.createElement("BUTTON");
            buttonEdit.value = item.Id;
            buttonEdit.Text = "Edytuj";

            row += "<tr><td>" +
                AmountWithUnit(item.Amount) +
                "</td><td>" +
                ConvertData(item.Date) +
                "</td><td>" +
                item.Source.Name +
                "</td><td>" +
                checkIfEmpty(item.Description) +
                 "</td><td>" +
                 "<Button type=\"button\" class=\"btn btn-default\" data-toggle=\"modal\" data-target=\"#myModalIncomes\" onclick=\"OnEditClickIncomingForEdit(" + item.Id + ")\">Edytuj</Button>" +
                  "</td><td>" +
                  "<Button type=\"button\" class=\"btn btn-default\" onclick=\"OnDeleteClickIncoming(" + item.Id + ")\">Usuń</Button>" +
                 "</td></tr>";
        });
    $(tableName).html(row);
}

function ONDeleteClickOutgoing(id) {
    $.ajax({
        url: "/FinancialManager/DeleteOutgoing/" + id,
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            ;
            checkSelectedRadioButton();
        },
        error: function (result) {
            alert("Error");
        }
    });
}
function OnDeleteClickIncoming(id) {
    $.ajax({
        url: "/FinancialManager/DeleteIncome/" + id,
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {

            checkSelectedRadioButton();
        },
        error: function (result) {
            alert("Error");
        }
    });
}
function fillTableOutgoings(data, tableName) {
    var row = "";
    $.each(data,
        function (index, item) {
            row += "<tr><td>" +
                AmountWithUnit(item.Amount) +
                "</td><td>" +
                ConvertData(item.Date) +
                "</td><td>" +
                item.Type.Name +
                "</td><td>" +
                checkIfEmpty(item.Description) +
                "</td><td>" +
                 "<Button type=\"button\" class=\"btn btn-default\" data-toggle=\"modal\" data-target=\"#myModalOutgoing\" onclick=\"OnEditClickOutgoingForEdit(" + item.Id + ")\">Edytuj</Button>" +
                  "</td><td>" +
                  "<Button type=\"button\" class=\"btn btn-default\" onclick=\"OnDeleteClickIncoming(" + item.Id + ")\">Usuń</Button>" +
                "</td></tr>";
        });
    $(tableName).html(row);
}

function incomingDateFilter(firstDate, secondDate) {

    $.ajax({
        url: "/FinancialBalance/GetIncomesByTimeFilter/?firstDateTime=" + firstDate + "&" + "secondDateTime=" + secondDate,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            fillTableIncoming(data, "#tBodyIncomes");
        },
        error: function (result) {
            alert("Error");
        }
    });
}
function outgoingDateFilter(firstDate, secondDate) {
    $.ajax({
        url: "/FinancialBalance/GetOutgoingsByTimeFilter/?firstDateTime=" + firstDate + "&" + "secondDateTime=" + secondDate,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            fillTableOutgoings(data, "#tBodyOutgoings");
        },
        error: function (result) {
            alert("Error");
        }
    });
}
function ConvertDataYearFirst(date) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(date);
    var dt = new Date(parseFloat(results[1]));
    return dt.getFullYear() + "-" + (dt.getMonth() + 1) + "-" + dt.getDate();
}
function ConvertData(date) {

    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(date);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "-" + dt.getDate() + "-" + dt.getFullYear();
}

function AmountWithUnit(amount) {
    return amount + " zł";
}

function checkIfEmpty(description) {
    if (description) {
        return description;
    }
    return "";
}