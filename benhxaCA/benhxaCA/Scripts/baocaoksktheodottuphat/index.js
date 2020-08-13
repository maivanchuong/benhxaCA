var baocaoksktheodottuphatcontroller = {
    init: function () {
        baocaoksktheodottuphatcontroller.registerEvents();
    },
    registerEvents: function () {
        $("#inbaocao").click(function () {
            var tungay = $("#tungay").val();
            var denngay = $("#denngay").val();
            var formdata = new FormData();
            formdata.append("tungay", tungay);
            formdata.append("denngay", denngay);
            $.ajax({
                url: '/baocaoksktheodottuphat/GetRPKSKTheoDotReport_TP',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: formdata,
                async: false,
                success: function (result) {
                    if (result == "Thành công") {
                        window.open("../Report/RPKSKTheoDotReport_TP.aspx", "_newtab");
                    } else {
                        alert(result);
                    }
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        });
    }
}
baocaoksktheodottuphatcontroller.init();