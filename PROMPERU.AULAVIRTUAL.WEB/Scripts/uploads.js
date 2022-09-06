function upload_image(element, input, message) {

	$(message).html("<span class='neutral-feedback'>Cargando...</span>");

	const data = new FormData();
	const fileSelect = element[0];
	const file = fileSelect.files[0];

	data.append("file", file, file.name);

	$.ajax({
		type: "POST",
		enctype: "multipart/form-data",
		url: "/api/UploadImage",
		data: data,
		processData: false,
		contentType: false,
		cache: false,
		timeout: 600000,
		success: function (res) {
			if (!res.status) {
				$(message).html("<span class='invalid-feedback'>" + res.message + "</span>");
			} else {
				$(message).html("<span class='valid-feedback'>" + res.message + "</span>");
				$(input).val(res.content.Data.clientPathFile);
			}
		},
		error: function (err) {
			$(message).html("<span class='invalid-feedback'>" + err.message + "</span>");
		}
	})
}

function upload_task(element, input, message) {

	$(message).html("<span class='neutral-feedback'>Cargando...</span>");

    const data = new FormData();
	const fileSelect = element;
	const file = fileSelect.files[0];

	data.append("file", file, file.name);

    $.ajax({
        type: "POST",
        enctype: "multipart/form-data",
        url: "/api/UploadTask",
        data: data,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 600000,
        success: function(res) {
            if (!res.status) {
                $(message).html("<span class='invalid-feedback'>" + res.message + "</span>");
            } else {
                $(message).html("<span class='valid-feedback'>" + res.message + "</span>");
                $(input).val(res.content.Data.clientPathFile);
            }
        },
        error: function(err) {
            $(message).html("<span class='invalid-feedback'>" + err.message + "</span>");
        }
    });
}

function upload_image_path(element, input, message, path) {

	$(message).html("<span class='neutral-feedback'>Cargando...</span>");

	const data = new FormData();
	const fileSelect = element[0];
	const file = fileSelect.files[0];

	data.append("file", file, file.name);
	data.append("path", path);

	$.ajax({
		type: "POST",
		enctype: "multipart/form-data",
		url: "/api/UploadImagePath",
		data: data,
		processData: false,
		contentType: false,
		cache: false,
		timeout: 600000,
		success: function (res) {
			if (!res.status) {
				$(message).html("<span class='invalid-feedback'>" + res.message + "</span>");
			} else {
				$(message).html("<span class='valid-feedback'>" + res.message + "</span>");
				$(input).val(res.content.Data.clientPathFile);
			}
		},
		error: function (err) {
			$(message).html("<span class='invalid-feedback'>" + err.message + "</span>");
		}
	})
}

function upload_file_path(element, input, message, path) {

	$(message).html("<span class='neutral-feedback'>Cargando...</span>");

	const data = new FormData();
	const fileSelect = element[0];
	const file = fileSelect.files[0];

	data.append("file", file, file.name);
	data.append("path", path);

	$.ajax({
		type: "POST",
		enctype: "multipart/form-data",
		url: "/api/UploadFilePath",
		data: data,
		processData: false,
		contentType: false,
		cache: false,
		timeout: 600000,
		success: function (res) {
			if (!res.status) {
				$(message).html("<span class='invalid-feedback'>" + res.message + "</span>");
			} else {
				$(message).html("<span class='valid-feedback'>" + res.message + "</span>");
				$(input).val(res.content.Data.clientPathFile);
			}
		},
		error: function (err) {
			$(message).html("<span class='invalid-feedback'>" + err.message + "</span>");
		}
	})
}