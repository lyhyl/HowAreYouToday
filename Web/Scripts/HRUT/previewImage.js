function ShowImagePreview(input) {
    //$(getImagePreviewer()).fadeOut("fast");
    if (input.value == "") {
        $(getImagePreviewer()).prop('src', "Content/ui/selectImage.png");
        return;
    }
    if (input.files && input.files[0]) {
        var fail = false;
        if (!ValidateSingleInput(input)) {
            MakeAlert("你上传的不是自拍吧?");
            fail = true;
        }
        if (!fail && input.files[0].size > 8 * 1024 * 1024) {
            MakeAlert("图片太大辣,换一张吧(<8M)");
            fail = true;
        }
        if (!fail && input.files[0].size < 1 * 1024) {
            MakeAlert("图片这么小,你确定这不是表情包?(>1K)");
            fail = true;
        }
        if (fail) {
            $(getImagePreviewer()).prop('src', "Content/ui/selectImage.png");
            input.value = "";
            return;
        }
        $(getImagePreviewer()).prop('src', "Content/ui/uploading.gif");
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = new Image();
            img.src = e.target.result;
            $(getImagePreviewer()).prop('src', e.target.result).fadeIn("fast");
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function ValidateSingleInput(input) {
    var ext = [".jpg", ".jpeg", ".png"];
    var fn = input.value;
    if (fn.length > 0) {
        for (var i = 0; i < ext.length; i++) {
            var fnext = fn.substr(fn.length - ext[i].length, ext[i].length);
            if (fnext.toLowerCase() == ext[i]) {
                return true;
            }
        }
    }
    return false;
}
