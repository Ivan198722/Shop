$(function () {
  
    $.datepicker.setDefaults($.datepicker.regional["pl"]);
    
    $("#from-date").datepicker({
        dateFormat: "yy.mm.dd"
    });
    $("#to-date").datepicker({
        dateFormat: "yy.mm.dd"
    });
});
function validateDateFormat(dateInput) {
    var datePattern = /^\d{4}\.\d{2}\.\d{2}$/;
    if (!datePattern.test(dateInput.value)) {
        dateInput.classList.add("invalid-date");
    } else {
        dateInput.classList.remove("invalid-date");
    }
}
$("#from-date, #to-date").on("change", function () {
    validateDateFormat(this);
});

function toggleMenu(listItem, listItemMenu) {
    $(listItem).click(function () {
        $(this).next(listItemMenu).toggle();
    });
}

$(document).ready(function () {
    toggleMenu('.category', '.category-menu');
   // toggleMenu('.model-bar', '.model-menu');
    
});


document.getElementById("addProductForm").addEventListener("submit", function (event) {
    let manufacturer = document.getElementById("addManufacturer");
    let name = document.getElementById("addName");
    let price = document.getElementById("addPrice");

    
    let isValid = true;


    if (!manufacturer.value) {
        manufacturer.placeholder = "* wprowadż markę";
        manufacturer.classList.add("invalid-placeholder");
        isValid = false;
    } else {
        manufacturer.placeholder = "Marka";
        manufacturer.classList.remove("invalid-placeholder");
    }

    if (!name.value) {
        name.placeholder = "* pole musi być wypełnione";
        name.classList.add("invalid-placeholder");
        isValid = false;
    } else {
        name.placeholder = "Nazwa";
        name.classList.remove("invalid-placeholder");
    }

    if (!price.value) {
        price.placeholder = "* wprowadż cene";
        price.classList.add("invalid-placeholder");
        isValid = false;
    } else {
        price.placeholder = "Cena";
        price.classList.remove("invalid-placeholder");
    }

    if (!isValid) {
        event.preventDefault(); 
        return;
    }

    
    price.value = price.value.replace(',', '.');

    
    if (isNaN(price.value) || parseFloat(price.value) <= 0) {
        price.placeholder = "Wprowadź prawidłową cenę";
        price.classList.add("invalid-placeholder");
        event.preventDefault();
        return;
    }

    
    price.value = price.value;
});

document.addEventListener("DOMContentLoaded", function () {
    const urlParams = new URLSearchParams(window.location.search);

    const priceFrom = urlParams.get('priceFrom');
    if (priceFrom !== null) {
        document.getElementById('price-from').value = priceFrom;
    }

    const priceTo = urlParams.get('priceTo');
    if (priceTo !== null) {
        document.getElementById('price-to').value = priceTo;
    }

    const fromDate = urlParams.get('fromDate');
    if (fromDate !== null) {
       
        const formattedFromDate = fromDate.substring(0, 10);
        document.getElementById('from-date').value = formattedFromDate;
    }

    const toDate = urlParams.get('toDate');
    if (toDate !== null) {
        
        const formattedToDate = toDate.substring(0, 10);
        document.getElementById('to-date').value = formattedToDate;
    }
    
});
    
function submitForm() {
    document.filterForm.submit();
}


//function updateFilter() {
//    // Сохраняем позицию курсора перед обновлением формы
//    const activeElement = document.activeElement;
//    const cursorPosition = activeElement.selectionStart;

//    // Выполняем обновление фильтра с новыми значениями
//    submitForm();

//    // Восстанавливаем позицию курсора после обновления формы
//    activeElement.setSelectionRange(cursorPosition, cursorPosition);
//}
//const checkboxes = document.querySelectorAll('input[name="selectedModels"]');
//checkboxes.forEach(checkbox => {
//    checkbox.addEventListener('change', updateFilter);
//});

//// Обработчики событий для изменений цен
//const priceInputs = document.querySelectorAll('#price-from, #price-to');
//priceInputs.forEach(input => {
//    input.addEventListener('input', updateFilter);
//});

//// Обработчики событий для изменений дат
//const dateInputs = document.querySelectorAll('#from-date, #to-date');
//dateInputs.forEach(input => {
//    input.addEventListener('input', updateFilter);
//});


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