var works = document.querySelector('.works');
var msnry = new Masonry(works, {
    fitWidth: true,
    horizontalOrder: true,
    itemSelector: '.work',
    columnWidth: '.work',
    gutter: 30,
    stagger: 30
});