﻿'use strict';

function hoverFile(event) {
    let file = event.currentTarget;

    let margin = 6;
    let px = 'px';

    let name = file.querySelector('.storage-file-name');
    let image = file.querySelector('.storage-image');
    let offset = image.getBoundingClientRect().bottom - name.getBoundingClientRect().height - margin;

    let top = offset + window.scrollY + px;
    let left = file.getBoundingClientRect().left + window.scrollX + px;

    let toolbar = document.getElementById('storage-file-toolbar');
    toolbar.style.top = top;
    toolbar.style.left = left;

    let imageId = file.querySelector('div.storage-file-name').getAttribute('image-id');
    toolbar.querySelector('#selected-file-path').value = imageId;
}

function unhoverFiles(event) {
    let toolbar = document.getElementById('storage-file-toolbar');
    toolbar.style.visibility = 'hidden';
}

function hoverFiles(event) {
    let toolbar = document.getElementById('storage-file-toolbar');
    toolbar.style.visibility = 'visible';
}

function editFileName() {
    document.querySelector('#edit-file-name-input').value = '';
    let fileName = document.querySelector('#selected-file-path').value;
    let name = document.querySelector(`.storage-file-name[image-id="${fileName}"]`);

    let input = document.querySelector('#edit-file-name-input');
    input.value = name.getAttribute('value');
    input.style.display = 'block';

    let container = name.parentNode;

    container.insertBefore(input, name);
    name.style.display = 'none';

    document.querySelector('#show-input-button').style.display = 'none';
    document.querySelector('#edit-name-button').style.display = 'grid';

    document.body.addEventListener('keydown', checkKeys);
}

function checkKeys(event) {
    if (event.key != 'Enter' && event.key != 'Escape') {
        return;
    }

    event.preventDefault();

    let input = document.querySelector('#edit-file-name-input');
    let name = input.parentNode.querySelector('div.storage-file-name');

    document.querySelector('#storage-form').insertBefore(input, document.querySelector('#storage-file-toolbar'));

    document.removeEventListener('onkeydown', checkKeys);

    document.querySelector('#show-input-button').style.display = 'grid';
    document.querySelector('#edit-name-button').style.display = 'none';

    input.style.display = 'none';
    name.style.display = 'block';

    if (event.key == 'Enter') {
        document.querySelector('#edit-file-name-input').value = input.value;
        document.querySelector('#edit-name-button').click();
    }
}