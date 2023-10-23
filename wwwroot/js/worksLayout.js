var works = document.querySelector('.works');
var msnry = new Masonry(works, {
    fitWidth: true,
    horizontalOrder: true,
    itemSelector: '.work-container',
    columnWidth: '.work-container',
    gutter: 40,
    stagger: 40
});