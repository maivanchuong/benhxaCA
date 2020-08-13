var baocaoksktheodonvicontroller = {
    init: function () {
        baocaoksktheodonvicontroller.registerEvents();
    },
    registerEvents: function () {
        $("#inbaocao").click(function () {
            var tungay = $("#tungay").val();
            var denngay = $("#denngay").val();
            var donvikham = $("#donvi").val();
            var formdata = new FormData();
            formdata.append("tungay", tungay);
            formdata.append("denngay", denngay);
            formdata.append("dvk", donvikham);
            $.ajax({
                url: '/baocaoksktheodonvi/GetRPKSKTheoDonViReport',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: formdata,
                async: false,
                success: function (result) {
                    if (result == "Thành công") {
                        window.open("../Report/RPKSKTheoDonViReport.aspx", "_newtab");
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
baocaoksktheodonvicontroller.init();