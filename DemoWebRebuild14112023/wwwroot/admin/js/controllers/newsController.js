var news = {
    init: function () {
        news.registerEvents();
    },
    registerEvents: function () {
        // Detail news
        $(document).on('click', '.detail', function () {
            var id = $(this).data('id');
            //@Url.Action("Detail", "Users")
            $.get('/Admin/News/Detail', { id: id })
                .done(function (response) {
                    if (response.success) {
                        $('#modal-form-detail').modal('show');
                        $('.modal-title').text('Hiển thị thông tin chi tiết tin tức');
                        $('#UserNameDetail').text(response.data.userName);
                        $('#FullNameDetail').text(response.data.fullName);
                        $('#RoleDetail').text(response.data.roleId);
                        $('#AddressDetail').text(response.data.address);
                        $('#EmailDetail').text(response.data.email);
                        $('#StatusDetail').text(response.data.status);
                    } else {
                        $('modal-fail').modal('show');
                        //alert(response.message);
                    }
                })
                .fail(function (error) {
                    console.log(error);
                });
        });
        // Save news
        $(document).on('click', '#btn-save', function (event) {
            event.preventDefault();  // Chặn hành động gửi form mặc định
            var form = $('#form-news');
            var data = form.serializeArray();
            // Lấy giá trị từ các trường thuộc tính của news trong form
            // Ví dụ:
            var news = {
                Id: $('#Id').val(),
                UserId: $('#UserId').val(),
                Title: $('#Title').val(),
                Description: $('#Description').val(),
                SubjectContent: $('#SubjectContent').val(),
                Avatar: $('#ImageFile').val(),
                CategoryId: $('#cboNewsCategoryId').val(),
                DateUpdate: $('#DateUpdate').val(),
                Status: $('#Status').is(':checked') // Lấy giá trị checked của trường Status
            };
            data.push({ name: 'news.Title', value: news.Title });
            data.push({ name: 'news.Description', value: news.Description });
            data.push({ name: 'news.SubjectContent', value: news.SubjectContent });
            data.push({ name: 'news.Avatar', value: news.Avatar });
            data.push({ name: 'news.CategoryId', value: news.CategoryId });
            data.push({ name: 'news.Status', value: news.Status });
            data.push({ name: 'news.Id', value: news.Id });
            data.push({ name: 'news.UserId', value: news.UserId });
            data.push({ name: 'news.DateUpdate', value: news.DateUpdate });

            $.ajax({
                url: $(this).data('action'),
                type: 'POST',
                data: $.param(data),
                success: function (response) {
                    if (response.success) {
                        location.reload();
                    } else {
                        //$('#modal-form').modal('hide');
                        //$('#modal-fail').modal('show');
                        $('#alert-fail').removeClass('hidden');
                        $('#alert-fail').text('Yêu cầu nhập đầy đủ thông tin');
                        //alert(response.message);
                    }
                },
                error: function (error) {
                    console.log(error);
                }
            });

        });

        // Edit product
        $(document).on('click', '.edit', function () {
            $('#alert-fail').addClass('hidden');
            var id = $(this).data('id');
            //@Url.Action("Edit", "Users")
            $.get('/Admin/News/Edit', { id: id })
                .done(function (response) {
                    if (response.success) {
                        $('#modal-form').modal('show');
                        $('.modal-title').text('Chỉnh sửa tin tức');
                        $('#btn-save').attr('data-action', '/Admin/News/Edit');
                        $('#Id').val(response.data.id);
                        $('#Title').val(response.data.title);
                        $('#Description').val(response.data.description);
                        $('#SubjectContent').val(response.data.subjectContent);
                        $('#ImageFile').val(response.data.avatar);
                        $('#cboNewsCategoryId').val(response.data.categoryId);
                        $('#DateUpdate').val(response.data.dateUpdate);
                        $('#UserId').val(response.data.userId);
                        $('#preview').attr('src', '../Upload/Images/' + response.data.avatar);
                        $('#Status').prop('checked', response.data.status);
                    } else {
                        alert(response.message);
                    }
                })
                .fail(function (error) {
                    console.log(error);
                });
        });

        $('#uploadButton').click(function () {
            var fileInput = $('#fileInput').get(0);
            var file = fileInput.files[0];

            var formData = new FormData();
            formData.append("file", file);

            $.ajax({
                url: '/Admin/News/Upload',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    // Xử lý phản hồi từ server (nếu cần)
                    $('#preview').attr('src', '../Upload/Images/' + response.data.avatar);
                    $('#ImageFile').val(response.data.avatar);
                    console.log("Upload thành công");
                },
                error: function (error) {
                    // Xử lý lỗi (nếu có)
                }
            });
        });

        // Delete product
        $(document).on('click', '.delete', function () {
            var id = $(this).data('id');
            $('#delete-id').val(id);
            $('#modal-delete').modal('show');
        });

        // Delete product
        $(document).on('click', '#btn-delete', function () {
            var id = $('#delete-id').val();
            //var url = '/NewsCategory/Delete/' + id;
            //console.log(id);
            //@Url.Action("Delete", "Users")
            $.post('/Admin/Users/Delete', { userId: id })
                .done(function (response) {
                    if (response.success) {
                        $('#modal-delete').modal('hide');
                        location.reload();
                    } else {
                        alert(response.message);
                    }
                })
                .fail(function (error) {
                    console.log(error);
                });
        });

        // Create User
        $(document).on('click', '.create', function () {
            $('#alert-fail').addClass('hidden');
            $('#modal-form').modal('show');
            $('.modal-title').text('Tạo mới tin tức');
            //@Url.Action("Create", "Users")
            $('#btn-save').attr('data-action', '/Admin/News/Create');
            $('#form-news').trigger('reset');
        });
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();//Nếu sự kiện click đã được click thì
            //gỡ ra và gán sự kiện click khác vào với function truyền event mới gọi AJAX, đầu tiên
            //event sẽ về default như mặc định ban đầu(giống như khi chưa làm gì cả) – tức reset
            //lại(e.preventDefault)
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/News/ChangeStatus",
                data: { id: id },
                datatype: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Kích hoạt');
                        btn.removeClass('btn-default').addClass('btn-danger');
                    }
                    else {
                        btn.text('Khóa');
                        btn.removeClass('btn-danger').addClass('btn-default');
                    }
                }
            });
        });
    }
}
news.init();