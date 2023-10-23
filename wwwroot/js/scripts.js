'use strict';

function hasSelection(editorTextarea) {
    let has = !(editorTextarea.selectionEnd == 0 && editorTextarea.selectionStart == 0);
    return has;
}

function getSlicesForStyle(editorTextarea) {
    let startIndex = editorTextarea.selectionStart;
    let endIndex = editorTextarea.selectionEnd;

    let begin = editorTextarea.value.slice(0, startIndex);
    let selection = editorTextarea.value.slice(startIndex, endIndex);
    let end = editorTextarea.value.slice(endIndex, editorTextarea.value.length);

    /*  let foo = selection.includes('\r\n')*/
    //let begin = editorTextarea.value.slice(0, startIndex);
    //let selection = editorTextarea.value.slice(startIndex, endIndex);
    //let end = editorTextarea.value.slice(endIndex, editorTextarea.value.length);

    // Bonus: place cursor behind replacement
    /*    textarea.selectionEnd = (first + editorTextarea.innerText).length;*/

    return [begin, selection, end];
}

function setStyle(startChar, endChar) {
    let editorTextarea = document.querySelector(".editor-textarea");

    if (!hasSelection(editorTextarea)) {
        return;
    }

    let slices = getSlicesForStyle(editorTextarea);
    let textForInsert = startChar + slices[1] + endChar;
    editorTextarea.value = slices[0] + textForInsert + slices[2];
}

function setItalic() {
    setStyle('*', '*');
}

function setBold() {
    setStyle('**', '**');
}

function setStrikethrough() {
    setStyle('~~', '~~');
}

function setUnderline() {
    setStyle('++', '++');
}

function setH2() {
    setStyle('## ', '');
}

function alignCenter() {
    setStyle('<span style="text-align: center;">', '</span>');
}

function alignLeft() {
    setStyle('<span style="text-align: left;">', '</span>');
}

function alignRight() {
    setStyle('<span style="text-align: right;">', '</span>');
}

function alignJustify() {
    setStyle('<span style="text-align: justify;">', '</span>');
}

function addLink() {
    setStyle('[', '](https://example.com/)');
}