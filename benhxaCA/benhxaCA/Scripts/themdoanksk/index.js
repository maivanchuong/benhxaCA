var themdoankskcontroller = {
    init: function () {
        themdoankskcontroller.registerEvents();
    },
    registerEvents: function () {
        $('#hienthi').off('click').on('click', function (event) {
            event.preventDefault();
            $("#danhsachdotkham").load("/themdoanksk/danhsachdotkham", {},function(){
                $("#view").modal("show");
           }); 
        });
        $('#themdotkham input').on('change', function()  {
            var donvikham = $("#donviduockham").val();
            var loai = $(".loaikham:checked").val();
            var formdata = new FormData();
            formdata.append("dvkham", donvikham);
            formdata.append("loaiksk", loai);
            $.ajax({
                url: '/themdoanksk/get_canbo',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: formdata,
                async: false,
                success: function (result) {
                    var html = '';
                    if (result != "Không có dữ liệu" && loai == "Khám sức khỏe tự phát") {
                        html += '<td>Cán bộ:</td>'
                        html +='<td>'
                        html += '<select name="tencanbo" class="width100per" id="tencanbo" style="width: 100%;">';
                        $.each(result, function (key, item) {
                            html += '<option value="' + item.ttcb_hoten + '" id="hoten">' + item.ttcb_hoten + '</option>';
                        });
                        html += '</select>'
                        html += '</td>'
                        $("#canbo").html(html);
                    } else {
                        html += '';
                        $("#canbo").html(html);
                    }
                   
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        });
        $("#themdotkham select").on('change',function () {
                var donvikham = $("#donviduockham").val();
                var loai = $(".loaikham:checked").val();
                var formdata = new FormData();
                formdata.append("dvkham", donvikham);
                formdata.append("loaiksk", loai);
                $.ajax({
                    url: '/themdoanksk/get_canbo',
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: formdata,
                    async: false,
                    success: function (result) {
                        var html = '';
                        if (result != "Không có dữ liệu" && loai=="Khám sức khỏe tự phát") {
                            html += '<td>Cán bộ:</td>'
                            html += '<td>'
                            html += '<select name="tencanbo" class="width100per" id="tencanbo" style="width: 100%;">';
                            $.each(result, function (key, item) {
                                html += '<option value="'+item.ttcb_hoten +'" id="hoten">' + item.ttcb_hoten + '</option>';
                            });
                            html += '</select>'
                            html += '</td>'
                            $("#canbo").html(html);
                        } else {
                            html += '';
                            $("#canbo").html(html);
                        }

                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            });
        $('#them').off('click').on('click', function (event) {
            event.preventDefault();
            var donvikham = $("#donviduockham").val();
            var ngaykham = $("#ngaykham").val();
            var loai =$(".loaikham:checked").val();
            var ghichu = $("#ghichu").val();
            //var btn = $("#hoten");
            //var name = btn.data('tencb');
            var name = $("#tencanbo").val();
            if (ghichu == "")
                ghichu = " ";
            var formdata = new FormData();
            formdata.append("dvkham", donvikham);
            formdata.append("ngay", ngaykham);
            formdata.append("loaiksk", loai);
            formdata.append("note", ghichu);
            formdata.append("tencanbo", name);
            $.ajax({
                url: '/themdoanksk/themdotkham',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: formdata,
                async: false,
                success: function (result) {
                    //success
                    if (result == "Thêm thành công") {
                        alert(result);
                        $("#danhsachdotkham").load("/themdoanksk/danhsachdotkham", {}, function () {
                            $("#view").modal("show");
                        });
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
themdoankskcontroller.init();