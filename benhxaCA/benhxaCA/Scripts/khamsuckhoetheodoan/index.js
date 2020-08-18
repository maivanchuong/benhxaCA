var khamsuckhoetheodoancontroller = {
    init: function () {
        khamsuckhoetheodoancontroller.registerEvents();
    },
    registerEvents: function () {
        $("#lammoi").click(function () {
            var ngaykham = $("#ngaykhamsuckhoe").val();
            var donvikham = $("#donvi").val();
            var trangthai = $(".trangthaikham:checked").val();
            //var trangthai = $("#trangthaikham").val();
            var formdata = new FormData();
            formdata.append("dvkham", donvikham);
            formdata.append("ngay", ngaykham);
            formdata.append("status", trangthai);
            $.ajax({
                url: '/khamsuckhoetheodoan/_danhsachcanbo',            //chỗ này ajax nó truyền vào controoler
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: formdata,
                async: false,
                success: function (result) {
                    //$("#ds_canbo").html(result);
                    //$("#ds_canbo").append(result);
                    if (result != 'Không có dữ liệu' && trangthai === '3') {
                        var html = '';
                        $.each(result, function (key, item) {
                            html += '<tr>';
                            html += '<td>' + item.ttcb_hoten + '</td>';
                            html += '<td>' + item.ttcb_gioitinh + '</td>';
                            html += '<td>' + item.ttcb_ngaysinh + '</td>';
                            //html += '<td><a href="#" onclick="return get_baocao(' + item.ttcb_id + ')" class="btn-baocao" target="_blank">Tạo báo cáo</a> </td>';
                            html += '<td align="center"><a href="#" data-macb=' + item.ttcb_id + ' class="btn-baocao" target="_blank"><img src="../img/reports.png" width="20" height="20"/></a> </td>';
                            html += '<td align="center" border="0"><a href="#" data-macb=' + item.ttcb_id + ' class="btn-thongtincanhan" target="_blank"><img src="../img/vcard.png" width="20" height="20"/></a> </td>';
                            //html += '<td><button data-macb="' + item.ttcb_id + '" class="btn-thongtincanhan">In thông tin cá nhân</button> </td>';
                            //html += '<td><button data-macb="' + item.ttcb_id + '" class="btn-baocao">Tạo báo cáo khám bệnh</button> </td>';
                            html += '</tr>';
                        });
                        $("#tbody").html(html);
                    } else if (result != 'Không có dữ liệu' && trangthai === '1') {
                        var html = '';
                        $.each(result, function (key, item) {
                            html += '<tr>';
                            html += '<td>' + item.ttcb_hoten + '</td>';
                            html += '<td>' + item.ttcb_gioitinh + '</td>';
                            html += '<td>' + item.ttcb_ngaysinh + '</td>';
                            //html += '<td><a href="#" onclick="return get_baocao(' + item.ttcb_id + ')" class="btn-baocao" target="_blank">Tạo báo cáo</a> </td>';
                            //html += '<td><a href="#" data-macb=' + item.ttcb_id + ' class="btn-baocao" target="_blank"><img src="../img/reports.png" width="20" height="20"/></a> </td>';
                            html += '<td align="center"><a href="#" data-macb=' + item.ttcb_id + ' class="btn-thongtincanhan" target="_blank"><img src="../img/vcard.png" width="20" height="20"/></a> </td>';
                            //html += '<td><button data-macb="' + item.ttcb_id + '" class="btn-thongtincanhan">In thông tin cá nhân</button> </td>';
                            //html += '<td><button data-macb="' + item.ttcb_id + '" class="btn-baocao">Tạo báo cáo khám bệnh</button> </td>';
                            html += '</tr>';
                        });
                        $("#tbody").html(html);
                    } else {
                        alert(result);
                        var html = '';
                        html += '<tr>';
                        html += '<td colspan="3">' + result + '</td>';
                        html += '</tr>';
                        $("#tbody").html(html);
                    }
                },
                error: function (err) {
                    alert(err.statusText);
                }

            });
            $('.btn-baocao').off('click').on('click', function (event)  {
                event.preventDefault();
                var btn = $(this);
                var id = btn.data('macb')
                var formdata = new FormData();
                formdata.append("macb", id);
                $.ajax({
                    url: '/khamsuckhoetheodoan/GetKhamBenhReport',
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: formdata,
                    async: false,
                    success: function (result) {
                        if (result == "Thành công") {
                            window.open("../Report/ReportViewer.aspx", "_newtab");
                        } else {
                            alert(result);
                        }
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            });
            $('.btn-thongtincanhan').off('click').on('click', function (event) {
                event.preventDefault();
                var btn = $(this);
                var id = btn.data('macb')
                var formdata = new FormData();
                formdata.append("macb", id);
                $.ajax({
                    url: '/khamsuckhoetheodoan/GetThongTinReport',
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: formdata,
                    async: false,
                    success: function (result) {
                        if (result == "Thành công") {
                            window.open("../Report/ReportViewer_TT.aspx", "_newtab");
                        } else {
                            alert("result");
                        }
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            });
        });

    },
}
khamsuckhoetheodoancontroller.init();

