

function resend_email() {

    window.location = "/EmailPurchaseOrder/index"
    /*
    $.ajax(
        {
            type: "POST",
            url: "/purchaseorderdetail/checkbeforsendemail",
            data: {
                PONo: 12092000,
                
            },
            
            success: function (data) {
                alert("success : " + data);
            },

            error: function () {
                alert("error");
            },
            timeout: function () {
                alert("timeout");
            },
        });
    */
}