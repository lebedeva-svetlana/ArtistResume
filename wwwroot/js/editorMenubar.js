$(document).ready(function () {
    $('.set-italic-js').click(setItalic);
    $('.set-bold-js').click(setBold);
    $('.set-strikethrough-js').click(setStrikethrough);
    $('.set-underline-js').click(setUnderline);
    $('.align-center-js').click(alignCenter);
    $('.align-left-js').click(alignLeft);
    $('.align-right-js').click(alignRight);
    $('.align-justify-js').click(alignJustify);
    $('.set-h2-js').click(setH2);
    $('.set-h3-js').click(setH3);
    $('.set-h4-js').click(setH4);
    $('.add-quote-js').click(addQuote);
    $('.add-link-js').click(addLink);
});

function hasSelection(editorTextarea) {
    return !(editorTextarea.selectionEnd == 0 && editorTextarea.selectionStart == 0);
}

function getSlicesForStyle(editorTextarea) {
    let startIndex = editorTextarea.selectionStart;
    let endIndex = editorTextarea.selectionEnd;

    let begin = editorTextarea.value.slice(0, startIndex);
    let selection = editorTextarea.value.slice(startIndex, endIndex);
    let end = editorTextarea.value.slice(endIndex, editorTextarea.value.length);

    return [begin, selection, end];
}

function setStyle(startChar, endChar) {
    let $editorTextarea = $('.editor-textarea')[0];

    if (!hasSelection($editorTextarea)) {
        return;
    }

    let slices = getSlicesForStyle($editorTextarea);
    let textForInsert = startChar + slices[1] + endChar;
    $editorTextarea.value = slices[0] + textForInsert + slices[2];
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

function setH3() {
    setStyle('### ', '');
}

function setH4() {
    setStyle('#### ', '');
}

function addQuote() {
    setStyle('>  ', '');
}

function alignCenter() {
    setStyle('<p style="text-align: center;">', '</p>');
}

function alignLeft() {
    setStyle('<p style="text-align: left;">', '</p>');
}

function alignRight() {
    setStyle('<p style="text-align: right;">', '</p>');
}

function alignJustify() {
    setStyle('<p style="text-align: justify;">', '</p>');
}

function addLink() {
    setStyle('[', '](https://example.com/)');
}