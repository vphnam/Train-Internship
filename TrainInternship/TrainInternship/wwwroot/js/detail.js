$(document).ready(function () {
    update_all();
    hide_tfoot();
    check_po_status();
});
//disable all input
function disable_all() {
    $("input, textarea").prop('disabled', true);
    $("#btnAddLine").prop('disabled', true);
}

//check purchase order status to disable all input
function check_po_status() {
    var orderNo = $('#PO_id').text();
    $.ajax({
        url: '/PurchaseOrder/GetPOStatus',
        type: 'GET',
        data: { OrderNo: orderNo },
        dataType: "json",
        success: function (data, textStatus) {
            if (data.poStatus == false) {
                disable_all();
            }
        },
        failure: function () {
            alert(result);
        },
        error: function (xhr) {
            alert('Error: ' + xhr.statusText);
        },
    });
}
function hide_tfoot() {
    $('#myTable > tfoot').hide();

}

//update amount and total when document ready
function update_all() {
    var sum = 0.0;
    $('#myTable > tbody > tr').each(function () {
        var qty = $(this).find('#QtyOrder').val();
        var price = $(this).find('#BuyPrice').val();
        var amount = $(this).find('#Amount');
        amount.val((Number(qty) * Number(price)).toFixed(2));
        sum += Number(amount.val());
        $(this).find('.amount').text('' + amount);
    });
    $('.total').text(sum.toFixed(2));
}


function update_totals() {
    var sum = 0.0;
    $('#myTable > tbody > tr').each(function () {
        var amount = $(this).find('#Amount');
        sum += Number(amount.val());
        $(this).find('.amount').text('' + amount);
    });
    $('.total').text(sum.toFixed(2));
}

//update amount and total when quantity change
var qtyOrder = $('.QtyOrder');
qtyOrder.change(function () {
    var qty = $(this).closest("tr").find('.QtyOrder').val();
    var price = $(this).closest("tr").find('.BuyPrice').val();
    var amount = $(this).closest("tr").find('.Amount');
    amount.val((Number(qty) * Number(price)).toFixed(2));
    update_totals();
});

//update amount and total when price change
var price = $('.BuyPrice');
price.change(function () {
    var qty = $(this).closest("tr").find('.QtyOrder').val();
    var price = $(this).closest("tr").find('.BuyPrice').val();
    var amount = $(this).closest("tr").find('.Amount');
    amount.val((Number(qty) * Number(price)).toFixed(2));
    update_totals();
});

//add po line
var addLine = $('#btnAddLine');
addLine.click(function () {
    $('#myTable > tfoot').show();
});

//remove po line
var remove = $('.btnRemove');
remove.click(function () {
    var partNo = $(this).closest("tr").find('.PartNo').val();
    var orderNo = $('#PO_id').text();
    if (confirm("Do you want to delete: " + partNo)) {
        $.ajax({
            url: '/PurchaseOrder/DeletePOLine',
            type: 'DELETE',
            data: {
                "orderNo": orderNo,
                "partNo": partNo,
            },
            success: function (result) {
                alert(result);
                location.reload();
            },
            failure: function () {
                alert(result);
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },

        });
    }
});

//add po line to db
var add = $('.btnAdd');
add.click(function () {
    var orderNo = $('#PO_id').text();
    var partDes = $(this).closest("tr").find('.PartDes').val();
    var manu = $(this).closest("tr").find('.Manu').val();
    var ordDate = $(this).closest("tr").find('.OrdDate').val();
    var qtyOrder = $(this).closest("tr").find('.QtyOrder').val();
    var buyPrice = $(this).closest("tr").find('.BuyPrice').val();
    var memo = $(this).closest("tr").find('.MeMo').val();

    var pol = {};
    pol.OrderNo = orderNo;
    pol.PartNo = null;
    pol.PartDescription = partDes;
    pol.Manufacturer = manu;
    pol.QuantityOrder = qtyOrder;
    pol.BuyPrice = buyPrice;
    pol.OrderDate = ordDate;
    pol.MeMo = memo;

    if (confirm("Do you want to add new line to the purchase order?")) {
        $.ajax({
            url: '/PurchaseOrder/AddPOLine',
            type: 'POST',
            data: pol,
            dataType: "json",
            success: function (result) {
                alert(result);
                location.reload();
            },
            failure: function () {
                alert(result);
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },

        });
    }
});

//cancel purchase order
var cancel = $('#btnCancelPO');
cancel.click(function () {
    var orderNo = $('#PO_id').text();
    if (confirm("Do you want to cancel this purchase order?")) {
        $.ajax({
            url: '/PurchaseOrder/CancelPO',
            type: 'POST',
            data: {OrderNo: orderNo },
            success: function (result) {
                alert(result);
                location.reload();
            },
            failure: function () {
                alert(result);
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },

        });
    }
});

//redirect to sendmail page
var sendmail = $('#btnSendMail');
sendmail.click(function () {
    $.ajax({
        url: '/PurchaseOrder/ResendMail',
        type: 'POST',
        data: {
        },
        success: function () {
            window.location.href = '/PurchaseOrder/ResendMail';
        },
        failure: function () {
            alert("Failure");
        },
        error: function (xhr) {
            alert('Error: ' + xhr.statusText);
        },

    });
})

//save changes
var save = $('#savechanges-button');
save.click(function () {
    var po = {};
    po.OrderNo = $('#PO_id').text();
    po.Note = $('#Note').val();
    po.Address = $('#Address').val();
    po.County = $('#County').val();
    po.PostCode = $('#PostCode').val();


    var polArr = new Array();
    $("#myTable TBODY TR").each(function () {
        var row = $(this);
        var pol = {};
        pol.PartNo = row.find("TD").children('.PartNo').val();
        pol.QuantityOrder = row.find("TD").children('.QtyOrder').val();
        pol.BuyPrice = row.find("TD").children('.BuyPrice').val();
        pol.MeMo = row.find("TD").children('.MeMo').val();
        polArr.push(pol);
    });

    if (confirm("Do you want save changes?")) {
        $.ajax({
            url: '/PurchaseOrder/UpdatePO',
            type: 'POST',
            data: {
                poEntity: po,
                polList: polArr
            },
            success: function (result) {
                alert(result);
                location.reload();
            },
            failure: function () {
                alert(result);
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },

        });
    }
});