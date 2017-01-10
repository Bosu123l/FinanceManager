$(function () {
    checkSelectedRadioButton();
});
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
        incomingDateFilterToChart(firstDate, secondDate);
        outgoingDateFilterToChart(firstDate, secondDate);
    } else if (secondRadioButton.checked) {
        OutgoingLastTimeFilter();
        IncomingLastTimeFilter();
        OutgoingLastTimeFilterToChart();
        IncomingLastTimeFilterToChart();
    } else if (thirdRadioButton.checked) {
        IncomingsByLastOperations();
        OutGoingsByLastOperations();
        IncomingsByLastOperationsToChart();
        OutGoingsByLastOperationsToChart();
    }
}

function barChartGenerateOutgoing(data) {
    var resultTypeOfOutgoing = data.map(function (object) {
        return object.TypeOfOutgoing;
    });

    var resultSum = data.map(function (object) {
        return object.Sum;
    });

    var ctx = document.getElementById("OutgoingChartBar");
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: resultTypeOfOutgoing,
            datasets: [{
                label: 'Wykres Wydatków',
                data: resultSum,
                backgroundColor: "rgba(255,192,192,0.4)",

                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}
function barChartGenerateIncomig(data) {
    var resultSourceOfAmount = data.map(function (object) {
        return object.SourceOfAmount;
    });

    var resultSum = data.map(function (object) {
        return object.Sum;
    });

    var ctx = document.getElementById("IncomingChartBar");
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: resultSourceOfAmount,
            datasets: [{
                label: 'Wykres Wydatków',
                data: resultSum,
                backgroundColor: "rgba(255,192,192,0.4)",

                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}
function chartGenerateOutgoing(data) {
    var resultAmount = data.map(function (object) {
        return object.Amount;
    });

    var typeOfAmount = data.map(function (object) {
        return [ConvertData(object.Date), object.Type.Name];
    });
    var ctx = document.getElementById("OutgoingChart");
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: typeOfAmount,
            datasets: [{
                label: 'Wykres Wydatków',
                data: resultAmount,
                backgroundColor: "rgba(255,192,192,0.4)",

                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}

function chartGenerateIncoming(data) {
    var resultAmount = data.map(function (object) {
        return object.Amount;
    });

    var typeOfAmount = data.map(function (object) {
        return [ConvertData(object.Date), object.Source.Name];
    });

    var ctx = document.getElementById("IncomingChart");
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: typeOfAmount,
            datasets: [{
                label: 'Wykres Przychodów',
                data: resultAmount,
                backgroundColor: "rgba(75,192,192,0.4)",

                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}
function incomingDateFilterToChart(firstDate, secondDate) {
    $.ajax({
        url: "Charts/GetSumsInSpecficIncomeByDate/?firstDateTime=" + firstDate + "&" + "secondDateTime=" + secondDate,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            barChartGenerateIncomig(data);
        }
    });
}
function incomingDateFilter(firstDate, secondDate) {
    $.ajax({
        url: "FinancialBalance/GetIncomesByTimeFilter/?firstDateTime=" + firstDate + "&" + "secondDateTime=" + secondDate,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            chartGenerateIncoming(data);
        }
    });
}

function outgoingDateFilterToChart(firstDate, secondDate) {
    $.ajax({
        url: "Charts/GetSumsInSpecficOutgoingByDate/?firstDateTime=" + firstDate + "&" + "secondDateTime=" + secondDate,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            chartGenerateOutgoing(data);
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
            chartGenerateOutgoing(data);
        }
    });
}

function OutgoingLastTimeFilterToChart() {
    var timeDiffValue = document.getElementById('difTimeSelect').value;
    var selectedEndPoint = "";
    switch (timeDiffValue) {
        case "days":
            selectedEndPoint = "GetSumInSpecificOutgoingTypeByNumberOfDays/?days=";
            break;
        case "weeks":
            selectedEndPoint = "GetSumsInSpecficOutgoingTypeNumberOfWeeks/?weeks=";
            break;
        case "months":
            selectedEndPoint = "GetSumsInSpecficOutgoingTypeNumberOfMonths/?month=";
            break;
    }
    var selectableFilterValue = document.getElementById('selectableFilterValue').value;

    $.ajax({
        url: "Charts/" + selectedEndPoint + selectableFilterValue,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            barChartGenerateOutgoing(data);
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
            chartGenerateOutgoing(data);
        }
    });
}

function IncomingLastTimeFilterToChart() {
    var timeDiffValue = document.getElementById('difTimeSelect').value;
    var selectedEndPoint = "";
    switch (timeDiffValue) {
        case "days":
            selectedEndPoint = "GetSumInSpecificIncomeTypeByNumberOfDays/?days=";
            break;
        case "weeks":
            selectedEndPoint = "GetSumsInSpecficIncomeTypeNumberOfWeeks/?weeks=";
            break;
        case "months":
            selectedEndPoint = "GetSumsInSpecficIncomeTypeNumberOfMonths/?month=";
            break;
    }
    var selectableFilterValue = document.getElementById('selectableFilterValue').value;

    $.ajax({
        url: "Charts/" + selectedEndPoint + selectableFilterValue,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            barChartGenerateIncomig(data);
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
        url: "FinancialBalance/" + selectedEndPoint + selectableFilterValue,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            chartGenerateIncoming(data);
        }
    });
}

function IncomingsByLastOperationsToChart() {
    var counterOfLastOperations = document.getElementById('counterOfLastOperations').value;

    $.ajax({
        url: "Charts/GetSumsInSpecficIncomeByLastOperations/?count=" + counterOfLastOperations,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            barChartGenerateIncomig(data);
        }
    });
}
function IncomingsByLastOperations() {
    var counterOfLastOperations = document.getElementById('counterOfLastOperations').value;

    $.ajax({
        url: "FinancialBalance/GetIncomesByLastOperations/?count=" + counterOfLastOperations,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            chartGenerateIncoming(data);
        }
    });
}

function OutGoingsByLastOperationsToChart() {
    var counterOfLastOperations = document.getElementById('counterOfLastOperations').value;
    $.ajax({
        url: "Charts/GetSumsInSpecficOutgoingByLastOperations/?count=" + counterOfLastOperations,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            barChartGenerateOutgoing(data);
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
            chartGenerateOutgoing(data);
        }
    });
}

function ConvertData(date) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(date);
    var dt = new Date(parseFloat(results[1]));
    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
}