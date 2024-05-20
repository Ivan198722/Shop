

function toggleMenu(listItem, listItemMenu) {
    $(listItem).click(function () {
        $(this).next(listItemMenu).toggle();
    });
}

$(document).ready(function () {
    toggleMenu('.category', '.category-menu');
    toggleMenu('.model-bar', '.model-menu');
    //toggleMenu('.another-list-item', '.another-list-item-menu');
});

//document.getElementById("addProduct").addEventListener("submit", function (event) {
//    // Получаем значение из textarea
//    var textareaManufacturer = document.getElementById("addManufacturer").value;
//    var textareaName = document.getElementById("addName").value;
//    var textareaPrice = document.getElementById("addPrice").value;
//    // Помещаем это значение в скрытое поле
//    document.getElementById("manufacturer").value = textareaManufacturer;
//    document.getElementById("name").value = textareaName;
//    document.getElementById("price").value = textareaPrice;
//});