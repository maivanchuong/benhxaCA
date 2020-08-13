var baocaophanloaikskcontroller = {
    init: function () {
        baocaophanloaikskcontroller.registerEvents();
    },
    registerEvents: function () {
        $("#inbaocao").click(function () {
            var tungay = $("#tungay").val();
            var denngay = $("#denngay").val();
            var formdata = new FormData();
            formdata.append("tungay", tungay);
            formdata.append("denngay", denngay);
            $.ajax({
                url: '/baocaophanloaiksk/GetPLKSKReport',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: formdata,
                async: false,
                success: function (result) {
                    if (result == "Thành công") {
                        window.open("../Report/PhanLoaiKSKReport.aspx", "_newtab");
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
baocaophanloaikskcontroller.init();