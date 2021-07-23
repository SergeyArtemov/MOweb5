function HandlingCardForCallResult33(res) {
    //alert('res: ' + res);
    if (res != '') {
        var intervalID = document.getElementById('intervalID').value;
        clearInterval(intervalID);
        var substringArray = res.split("|");
        OrderNo = substringArray[0];
        Host = substringArray[1];
        Customer = substringArray[2];
        if (OrderNo < 0) {
            content = '<a id="ShowCardForCall" href="@Url.Action("ShowOrders", "Order")?CustomerId=' + Customer +
                '&Host=' + Host +
                '&OrderNo=' + OrderNo +
                '&CurrentUser=' + @Model.App.CurrentUser.AccountId + '" hidden>RedirectTest</a>';
        }
        else {
            content = '<a id="ShowCardForCall" href="@Url.Action("ShowCustomerOrders", "Order")?CustomerId=' + Customer +
                '&Host=1' + Host + '&OrderNo=' + OrderNo +
                '" hidden>RedirectTest</a>';
        }
        alert(content);

        var el = document.getElementById('ShowCardForCall');
        el.outerHTML = content;
        el = document.getElementById('ShowCardForCall');
        el.click()
    }
}; //HandlingCardForCallResult





















/*
function SendCallParams(Phone, Line, Customer = 0, Host = 3, TypeClientCard = 1) {
    alert('showOnOk1 до');

    $.ajax({
        url: '/Home/MakeCall',
        data: { Phone: Phone, Line: Line, Customer: Customer, Host: Host, TypeClientCard: TypeClientCard },
        type: 'POST',
        success: function (data) { ; },
        error: function (data) { alert('error'); }
    });
    //setSingleRowSelect(ths);

}
*/
/*
function the_call(TelPhone, Line) {
    var text = 'MsgType=CALL_ATR;TelPhone=' + TelPhone + ';Line=' + Line;
    text = '"data:text/plain;charset=utf-8,%EF%BB%BF' + encodeURIComponent(text) + '"';
    var content = '<a id="coll" href=' + text + ' download="cosc_msg.txt" hidden>Позвонить</a>';

    var CL = document.getElementById("coll");
    if (CL == null) {
        let CL = document.createElement("a")
        CL.id = "coll"
    }
    CL.outerHTML = content;

    CL = document.getElementById('coll');
    CL.click()
}
*/


function get_card_for_call_ex() {

 //   alert("get_card_for_call_ex");

/*
    $.ajax({
        url: '/Home/ExistsCardForCall',
        data: {},
        type: 'POST',
        success: function (OrderNo, Host) { AOrderNo = OrderNo; AHost = Host; },
        error: function (data) { alert('error'); }
    });



    if (AOrderNo != 0) {


    $.ajax({
        url: '/Order/ShowOrderHost',
        data: { OrderNo: AOrderNo, HostId: AHost },
        type: 'POST',
        success: function (data) {},
        error: function (data) { alert('error'); }
    });

    }
*/
}

/*
function get_card_for_call() {
    $.ajax({
        url: '/Home/GetCardForCall',
        data: {},
        type: 'POST',
        success: function (data) { ; },
        error: function (data) { alert('error'); }
    });
*/

}


