$(document).ready(function () {
    $('.change-upload-name-js').change(changeInputText);
    $('.storage-js').mouseover(hoverFiles);
    $('.storage-js').mouseout(unhoverFiles);
    $('.storage-file-js').mouseover(hoverFile);
    $('.storage-image-js').click(enlargeImage);
    $('.close-enlarge-image-js').click(closeEnlargeImage);
    $('.edit-file-name-js').click(editFileName);
});

function hoverFile(event) {
    if ($('#window-hover').is(":visible")) {
        return;
    }

    let file = event.currentTarget;
    let $toolbar = $('#storage-toolbar')[0];
    let $name = $(file).find('.storage-filename')[0];
    let $image = $(file).find('.storage-image')[0];

    let top = ($($image).position().top + $($image).height()) - $($name).height() - 6;
    let left = $(file).position().left + (($(file).innerWidth() - $($toolbar).width()) / 2);

    $($toolbar).css('top', top);
    $($toolbar).css('left', left);

    let imageId = $($(file).find('div.storage-filename')[0]).attr('image-id');
    $($toolbar).find('#storage-selected-path').val(imageId);
}

function unhoverFiles(event) {
    if (!$('#window-hover').is(":visible")) {
        $('#storage-toolbar').css('visibility', 'hidden');
    }
}

function hoverFiles(event) {
    if (!$('#window-hover').is(":visible")) {
        $('#storage-toolbar').css('visibility', 'visible');
    }
}

function editFileName() {
    $('#filename-input').val('');
    let $fileName = $('#storage-selected-path').val();
    let $name = $(`.storage-filename[image-id='${$fileName}']`);

    let $input = $('#filename-input');
    $input.val($($name).prop('innerText'));
    $input.css('display', 'block');

    $($input).insertBefore($($name));
    $name.css('display', 'none');

    $('#show-input-button').css('display', 'none');
    $('#edit-name-button').css('display', 'grid');

    $('body').keydown(checkKeys);
}

function checkKeys(event) {
    if (event.key != 'Enter' && event.key != 'Escape') {
        return;
    }

    $input.css('display', 'none');
    $name.css('display', 'block');

    $('#edit-name-button').css('display', 'none');
    $('#show-input-button').css('display', 'grid');

    let $input = $('#filename-input');
    let $name = $input.next();

    $($input).insertBefore($('#storage-toolbar'));

    document.off('keydown');

    if (event.key == 'Enter') {
        $('#filename-input').val($input.val());
        $('#edit-name-button').click();
    }
}

function enlargeImage() {
    $('#storage-toolbar').css('visibility', 'hidden');
    let $fileName = $('#storage-selected-path').val();
    let $imageSrc = $(`.storage-image[image-id='${$fileName}']`).attr('src');

    let $enlargeImageContainer = $('#enlarge-image-container');
    let $enlargeImage = $('#enlarge-image');

    $enlargeImage.attr('src', $imageSrc);
    $enlargeImageContainer.css('display', 'block');

    let $hover = $('#window-hover');
    $hover.css('display', 'block');

    $hover.click(closeEnlargeImage);
}

function closeEnlargeImage() {
    $('#enlarge-image-container').css('display', 'none');
    $('#enlarge-image').attr('src', '');
    $('#window-hover').css('display', 'none');

    $('#window-hover').off('click');
}

function changeInputText(event) {
    let input = event.currentTarget;
    let file = input.files[0];
    $('.file-upload-name').text(file.name);
    $('.file-upload-button').removeAttr('disabled');
}