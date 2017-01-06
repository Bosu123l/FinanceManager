document.getElementById('buttonComfirm')
       .addEventListener('click',
           function () {
               checkSelectedRadioButton();
           });

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
});

function IncomingsByLastOperations() {
    var counterOfLastOperations = document.getElementById('counterOfLastOperations').value;

    $.ajax({
        url: "FinancialBalance/GetIncomesByLastOperations/?count=" + counterOfLastOperations,
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
    $.ajax({
        url: "FinancialBalance/GetIncomingsSumByLastOperations/?count=" + counterOfLastOperations,
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

function OutGoingsByLastOperations() {
    var counterOfLastOperations = document.getElementById('counterOfLastOperations').value;
    $.ajax({
        url: "FinancialBalance/GetOutgoingsByLastOperations/?count=" + counterOfLastOperations,
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
    $.ajax({
        url: "FinancialBalance/GetOutgoingsSumByLastOperations/?count=" + counterOfLastOperations,
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

function OutgoingLastTimeFilter() {
    var timeDiffValue = document.getElementById('difTimeSelect').value;
    var selectedEndPoint = "";
    var selectedEndPointSum = "";
    switch (timeDiffValue) {
        case "days":
            selectedEndPoint = "GetOutgoingsByNumberOfDays/?days=";
            selectedEndPointSum = "GetOutgoingsSumByNumberOfDays/?days=";
            break;
        case "weeks":
            selectedEndPoint = "GetOutgoingsByNumberOfWeeks/?weeks=";
            selectedEndPointSum = "GetOutgoingsSumByNumberOfWeeks/?weeks="
            break;
        case "months":
            selectedEndPoint = "GetOutgoingsByNumberOfMonth/?month=";
            selectedEndPointSum = "GetOutgoingsSumByNumberOfMonth/?month=";
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

    $.ajax({
        url: "FinancialBalance/" + selectedEndPointSum + selectableFilterValue,
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
function IncomingLastTimeFilter() {
    var timeDiffValue = document.getElementById('difTimeSelect').value;
    var selectedEndPoint = "";
    var selectedEndPointSum = "";
    switch (timeDiffValue) {
        case "days":
            selectedEndPoint = "GetIncomingsByNumberOfDays/?days=";
            selectedEndPointSum = "GetIncomingsSumByNumberOfDays/?days=";
            break;
        case "weeks":
            selectedEndPoint = "GetIncomesByNumberOfWeeks/?weeks=";
            selectedEndPointSum = "GetIncomingsSumByNumberOfWeeks/?days=";
            break;
        case "months":
            selectedEndPoint = "GetIncomingsByNumberOfMonth/?month=";
            selectedEndPointSum = "GetIncomingsSumByNumberOfMonth/?days=";
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
            fillTableIncoming(data, "#tBodyIncomes");
        },
        error: function (result) {
            alert("Error");
        }
    });
    $.ajax({
        url: "FinancialBalance/" + selectedEndPointSum + selectableFilterValue,
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
function fillTableIncoming(data, tableName) {
    var row = "";
    $.each(data,
        function (index, item) {
            row += "<tr><td>" +
                AmountWithUnit(item.Amount) +
                "</td><td>" +
                ConvertData(item.Date) +
                "</td><td>" +
                item.Source.Name +
                "</td><td>" +
                checkIfEmpty(item.Description) +
                "</td></tr>";
        });
    $(tableName).html(row);
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
                "</td></tr>";
        });
    $(tableName).html(row);
}
function incomingDateFilter(firstDate, secondDate) {
    $.ajax({
        url: "FinancialBalance/GetIncomesByTimeFilter/?firstDateTime=" + firstDate + "&" + "secondDateTime=" + secondDate,
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
    $.ajax({
        url: "FinancialBalance/SumOfIncomings/?firstDateTime=" + firstDate + "&" + "secondDateTime=" + secondDate,
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
function outgoingDateFilter(firstDate, secondDate) {
    $.ajax({
        url: "FinancialBalance/GetOutgoingsByTimeFilter/?firstDateTime=" + firstDate + "&" + "secondDateTime=" + secondDate,
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
    $.ajax({
        url: "FinancialBalance/SumOfOutgoings/?firstDateTime=" + firstDate + "&" + "secondDateTime=" + secondDate,
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
function ConvertData(date) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(date);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
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