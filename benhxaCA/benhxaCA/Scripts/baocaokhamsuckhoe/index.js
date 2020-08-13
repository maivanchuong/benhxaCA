var baocaokhamsuckhoecontroller = {
    init: function () {
        baocaokhamsuckhoecontroller.registerEvents();
    },
    registerEvents: function () {
        $("#xembaocao").click(function () {
            var tungay = $("#tungay").val();
            var denngay = $("#denngay").val();
            var donvi = $("#donvi").val();
            var loaiksk = $("#loaiksk").val();
            //var trangthai = $("#trangthaikham").val();
            var formdata = new FormData();
            formdata.append("tungay", tungay);
            formdata.append("denngay", denngay);
            formdata.append("donvi", donvi);
            formdata.append("loaiksk", loaiksk);
            $.ajax({
                url: '/baocaokhamsuckhoe/hienthibaocao',            //chỗ này ajax nó truyền vào controoler
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: formdata,
                async: false,
                success: function (result) {
                    //$("#ds_canbo").html(result);
                    //$("#ds_canbo").append(result);
                    if (result == "Xin chọn ngày") {
                        alert(result)
                    } else {
                        var html = '';
                        $.each(result, function (key, item) {
                            html += '<tr>';
                            html += '<td>' + item.loaikham + '</td>';
                            html += '<td>' + item.soluot + '</td>';
                            //html += '<td><a href="#" onclick="return get_baocao(' + item.ttcb_id + ')" class="btn-baocao" target="_blank">Tạo báo cáo</a> </td>';
                            //html += '<td><a href="../khamsuckhoetheodoan/GetKhamBenhReport/' + item.ttcb_id + '" class="btn-baocao" target="_blank">Tạo báo cáo</a> </td>';
                            html += '</tr>';
                        });
                        $("#tbody").html(html);
                    }
                    

                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        });
        $("#inbaocao").click(function () {
            var tungay = $("#tungay").val();
            var denngay = $("#denngay").val();
            var formdata = new FormData();
            formdata.append("tungay", tungay);
            formdata.append("denngay", denngay);
            $.ajax({
                url: '/baocaokhamsuckhoe/GetTongHopKSKReport',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: formdata,
                async: false,
                success: function (result) {
                    if (result == "Thành công") {
                        window.open("../Report/TongHopKSKReport.aspx", "_newtab");
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
baocaokhamsuckhoecontroller.init();