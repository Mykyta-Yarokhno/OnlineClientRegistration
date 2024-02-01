function SelectService(service) {
    var selectedService = service.options[service.selectedIndex];
    var choosenServicesSelect = document.getElementById('ChoosenServices');
    choosenServicesSelect.appendChild(selectedService);

    // Устанавливаем "выбрано" для всех элементов во втором списке
    var allChoosenServices = choosenServicesSelect.options;
    for (var i = 0; i < allChoosenServices.length; i++) {
        allChoosenServices[i].selected = true;
    }

    // Сбрасываем "выбрано" для перемещенного элемента из первого списка
    var servicesToChooseSelect = document.getElementById('ServicesToChoose');
    var allServicesToChoose = servicesToChooseSelect.options;
    for (var i = 0; i < allServicesToChoose.length; i++) {
        allServicesToChoose[i].selected = false;
    }
}

function UnselectService(service) {
    var selectedService = service.options[service.selectedIndex];
    var servicesToChooseSelect = document.getElementById('ServicesToChoose');
    servicesToChooseSelect.appendChild(selectedService);


    // Устанавливаем "выбрано" для всех элементов во втором списке
    var choosenServicesSelect = document.getElementById('ChoosenServices');
    var allChoosenServices = choosenServicesSelect.options;
    for (var i = 0; i < allChoosenServices.length; i++) {
        allChoosenServices[i].selected = true;
    }

    // Сбрасываем "выбрано" для перемещенного элемента из второго списка
    var allServicesToChoose = servicesToChooseSelect.options;
    for (var i = 0; i < allServicesToChoose.length; i++) {
        allServicesToChoose[i].selected = false;
    }

    if (choosenServicesSelect.options.length === 1)
    {
        return true;
    }

    return false;

}

function checkIfUserCreated(phoneNumber) {
    $.ajax({
        type: "GET",
        url: "/api/Users/user?phoneNumber=" + encodeURIComponent(phoneNumber),
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (result, textStatus, jqXHR) {

            document.getElementById('name').removeAttribute('hidden');

            if (jqXHR.status == "204") {
                document.getElementById('name').removeAttribute('readonly');
                return;
            }

            var retrievedName = result.name;
            document.getElementById('name').value = retrievedName;
            document.getElementById('name').setAttribute('readonly', '');
            validateClientFields();

        },
        error: function () {
            alert("there was an error");
        }
    })
}
function validatePhoneNumber(phoneNumber) {
                var regex = /^\+38[0-9]{10}$/;

    if (!regex.test(phoneNumber)) {
        alert('Невірний формат номеру телефона України! Будь ласка, введіть номер телефону у форматі +38XXXXXXXXXX ');
    document.getElementById('phoneNumber').value = '+38';
    return false;
                }

    return true;
        }
function validateClientFields() {
    var phoneNumber = document.getElementById('phoneNumber').value;
    var name = document.getElementById('name').value;


    if (validatePhoneNumber(phoneNumber) && name.trim() !== ''){

        var btnLogin = document.getElementById('submitButtonLogin');
        var serviceBlock = document.getElementById('servicesBlock'); 

        if (btnLogin != null) {
            btnLogin.removeAttribute('hidden');
        }
        if (serviceBlock != null) {
            serviceBlock.removeAttribute('hidden');
        }

       
    }
    else if (name.trim() === '') {
        alert('Пожалуйста, введите имя');
    }
}
function onPhoneNumberChanged(phoneNumber) {
    validatePhoneNumber(phoneNumber);
    checkIfUserCreated(phoneNumber);
}



