$(document).ready(function () {
    $('.change-input-text-js').change(changeInputText);
    $('.hover-files-js').mouseover(hoverFiles);
    $('.hover-files-js').mouseout(unhoverFiles);
    $('.hover-file-js').mouseover(hoverFile);
    $('.enlarge-image-js').click(enlargeImage);
    $('.close-enlarge-image-js').click(closeEnlargeImage);
    $('.edit-file-name-js').click(editFileName);
});

function hoverFile(event) {
    if ($('#black-hover').is(":visible")) {
        return;
    }

    let file = event.currentTarget;
    let $toolbar = $('#storage-file-toolbar')[0];
    let $name = $(file).find('.storage-file-name')[0];
    let $image = $(file).find('.storage-image')[0];

    let top = ($($image).position().top + $($image).height()) - $($name).height() - 6;
    let left = $(file).position().left + (($(file).innerWidth() - $($toolbar).width()) / 2);

    $($toolbar).css('top', top);
    $($toolbar).css('left', left);

    let imageId = $($(file).find('div.storage-file-name')[0]).attr('image-id');
    $($toolbar).find('#selected-file-path').val(imageId);
}

function unhoverFiles(event) {
    if (!$('#black-hover').is(":visible")) {
        $('#storage-file-toolbar').css('visibility', 'hidden');
    }
}

function hoverFiles(event) {
    if (!$('#black-hover').is(":visible")) {
        $('#storage-file-toolbar').css('visibility', 'visible');
    }
}

function editFileName() {
    $('#edit-file-name-input').val('');
    let $fileName = $('#selected-file-path').val();
    let $name = $(`.storage-file-name[image-id='${$fileName}']`);

    let $input = $('#edit-file-name-input');
    $input.val($($name).text());
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

    let $input = $('#edit-file-name-input');
    let $name = $input.next();

    $($input).insertBefore($('#storage-file-toolbar'));

    document.off('keydown');

    if (event.key == 'Enter') {
        $('#edit-file-name-input').val($input.val());
        $('#edit-name-button').click();
    }
}

function enlargeImage() {
    $('#storage-file-toolbar').css('visibility', 'hidden');
    let $fileName = $('#selected-file-path').val();
    let $imageSrc = $(`.storage-image[image-id='${$fileName}']`).attr('src');

    let $enlargeImageContainer = $('#enlarge-image-container');
    let $enlargeImage = $('#enlarge-image');

    $enlargeImage.attr('src', $imageSrc);
    $enlargeImageContainer.css('display', 'block');

    let $hover = $('#black-hover');
    $hover.css('display', 'block');

    $hover.click(closeEnlargeImage);
}

function closeEnlargeImage() {
    $('#enlarge-image-container').css('display', 'none');
    $('#enlarge-image').attr('src', '');
    $('#black-hover').css('display', 'none');

    $('#black-hover').off('click');
}

function changeInputText(event) {
    let input = event.currentTarget;
    let file = input.files[0];
    $('.input-file-text').text(file.name);
    $('.upload-storage-file-button').removeAttr('disabled');
}