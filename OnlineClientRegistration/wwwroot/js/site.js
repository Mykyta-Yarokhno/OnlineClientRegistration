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

