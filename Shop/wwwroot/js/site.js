

function toggleMenu(listItem, listItemMenu) {
    $(listItem).click(function () {
        $(this).next(listItemMenu).toggle();
    });
}

$(document).ready(function () {
    toggleMenu('.category', '.category-menu');
    //toggleMenu('.another-list-item', '.another-list-item-menu');
});