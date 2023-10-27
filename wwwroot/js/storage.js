'use strict';

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

    let path = file.querySelector('.storage-file-name').getAttribute('value');
    toolbar.querySelector('#selected-file-path').value = path;
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
    let fileName = document.querySelector('#selected-file-path').value;
    let name = document.querySelector(`.storage-file-name[value="${fileName}"]`);
    name.setAttribute('contenteditable', 'true');
    name.className += ' storage-edit-file-name';
    document.body.addEventListener('keydown', checkKeys);
}

function checkKeys(event) {
    if (event.key == 'Enter' || event.key == 'Escape') {
        event.preventDefault();
        let name = document.querySelector('.storage-file-name[contenteditable="true"]');

        let regex = /^[a-z0-9_.@()-]+$/i;
        let newName = trim(name.textContent);

        if (regex.test(newName)) {

            document.removeEventListener('onkeydown', checkKeyss);
        }
    }
}

function endEditFileName(event) {
}