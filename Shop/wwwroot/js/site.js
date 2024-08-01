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





function autoResize(textarea) {
    textarea.style.height = 'auto';
    textarea.style.height = textarea.scrollHeight + 'px';
}

document.addEventListener('DOMContentLoaded', function () {
    const textareas = document.querySelectorAll('textarea');
    textareas.forEach(function (textarea) {
        autoResize(textarea);
        textarea.addEventListener('input', function () {
            autoResize(textarea);
        });
    });
});

//function autoResize(textarea) {
//    textarea.style.height = 'auto';
//    textarea.style.height = (textarea.scrollHeight) + 'px';
//}

//document.addEventListener('DOMContentLoaded', function () {
//    const textarea = document.querySelector('#editDescriptionField');
//    if (textarea) {
//        autoResize(textarea);
//    }
//});

 //function autoResize(textarea) {
 //         textarea.style.height = 'auto';
 //         textarea.style.height = (textarea.scrollHeight) + 'px';
 //     }
 //                     document.querySelectorAll('textarea[id^="editDescriptionField-"]').forEach(function(textarea) {
 //                   autoResize(textarea);
 //                     });

function submitForm1(productId, formIdPrefix, inputIdPrefix, isChecked) {
    // Устанавливаем значение скрытого поля в форме
    document.getElementById(inputIdPrefix + '_' + productId).value = isChecked;

    // Отправляем форму
    document.getElementById(formIdPrefix + '_' + productId).submit();
};

function showUploadBlock() {
    let uploadBlock = document.querySelector('#uploadBlock');
    let imgPlus = document.querySelector('#imgPlus');
    if (uploadBlock.style.display === "none") {
        uploadBlock.style.display = "block";
        imgPlus.style.display = "none";
    } else {
        uploadBlock.style.display = "none";
        imgPlus.style.display = "block";
    }
}

function showImageUrl(input) {
    const file = input.files[0];
    const reader = new FileReader();
    reader.onload = function (e) {
        const imageUrl = e.target.result;
        const fileName = input.value.split("\\").pop();
        const fileInputLabel = document.querySelector('#fileInputLabel');
        fileInputLabel.innerText = fileName;

        // Установка расширения файла
        document.querySelector('#fileExtension').value = fileName.split('.').pop();
    };
    reader.readAsDataURL(file);
}

function handleFormSubmit(event) {
    const fileInput = document.querySelector('#fileInput');
    const fileExtensionInput = document.querySelector('#fileExtension');

    if (fileInput.files.length === 0) {
        alert("Wybierz plik");
        event.preventDefault();
        return false;
    }

    const file = fileInput.files[0];
    const fileExtension = file.name.split('.').pop().toLowerCase();

    if (fileExtension !== 'png' && fileExtension !== 'jpg' && fileExtension !== 'jpeg' && fileExtension !== 'webp') {
        alert("O wybrany plik nie jest obrazem (obsługiwane formaty: PNG, JPG, JPEG, WEBP).");
        event.preventDefault();
        return false;
    }

    fileExtensionInput.value = fileExtension;
    return true;
}
document.addEventListener('DOMContentLoaded', function () {
    // Функция для установки фокуса на кнопку
    function buttonFocus(event) {
        event.target.focus();
    }

    // Устанавливаем фокус на кнопку перед отправкой формы
    document.querySelectorAll('.button-edit').forEach(button => {
        button.addEventListener('click', buttonFocus);
    });

    // Восстанавливаем положение прокрутки при загрузке страницы
    function restoreScrollPosition() {
        const scrollPosition = localStorage.getItem('scrollPosition');
        if (scrollPosition !== null) {
            window.scrollTo(0, parseInt(scrollPosition, 10));
            localStorage.removeItem('scrollPosition'); // Удаляем сохраненное значение после использования
        }
    }

    // Сохраняем положение прокрутки перед отправкой формы
    function saveScrollPosition() {
        localStorage.setItem('scrollPosition', window.scrollY);
    }

    window.onload = restoreScrollPosition;

    document.querySelectorAll('.button-edit').forEach(button => {
        button.addEventListener('click', saveScrollPosition);
    });
});
//function buttonFocus() {
//    const buttons = document.querySelectorAll('.button-edit');
//    buttons.forEach(button => {
//        button.focus();
//    });
//}

//document.addEventListener('DOMContentLoaded', function () {
//    // Восстанавливаем фокус на кнопке при загрузке страницы
//    window.onload = buttonFocus;

//    // Устанавливаем фокус на кнопку перед отправкой формы
//    document.querySelectorAll('.button-edit').forEach(button => {
//        button.addEventListener('click', buttonFocus);
//    });
//});


//document.addEventListener('DOMContentLoaded', function () {
//    document.querySelectorAll('#delete-button').forEach(button => {
//        button.addEventListener('click', function (event) {
//            event.preventDefault();
//            const productId = this.getAttribute('data-product-id');
//            const alertDiv = document.querySelector('.modal');
//            alertDiv.style.display = 'block';

//            document.querySelector('#yes').addEventListener('click', function () {
//                window.location.href = `/Admin/deleteProduct?productId=${productId}`;
//            });

//            document.querySelector('#no').addEventListener('click', function () {
//                alertDiv.style.display = 'none';
//            });
//        });
//    });
//});

//document.addEventListener('DOMContentLoaded', function () {
//    // Функция для сохранения положения прокрутки
//    function saveScrollPosition() {
//        localStorage.setItem('scrollPosition', window.scrollY);
//    }

//    // Функция для восстановления положения прокрутки
//    function restoreScrollPosition() {
//        const scrollPosition = localStorage.getItem('scrollPosition');
//        if (scrollPosition !== null) {
//            window.scrollTo(0, parseInt(scrollPosition, 10));
//            localStorage.removeItem('scrollPosition'); // Удаляем сохраненное значение после использования
//        }
//    }

//    // Восстанавливаем положение прокрутки при загрузке страницы
//    window.onload = restoreScrollPosition;

//    // Сохраняем положение прокрутки перед отправкой формы
//    document.querySelectorAll('.button-edit').forEach(button => {
//        button.addEventListener('click', saveScrollPosition);
//    });

//});

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