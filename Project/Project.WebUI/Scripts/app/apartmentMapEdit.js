ymaps.ready(init);
var myMap, myPlacemark;
var $lat, $lon, $Adress;
var newLat, newLon, newCoords;
function init() {
    myMap = new ymaps.Map("map", {
        center: [55.76, 37.64],
        zoom: 7
    });

    $lat = $("#Latitude").val();
    $lon = $("#Longitude").val();
    
    if (($lat == "") || ($lon == "")) {
        GetCoords();
    }
    $Adress = $("#Adress").val();
    myPlacemark = new ymaps.Placemark([$lat.replace(",", "."), $lon.replace(",", ".")], {
        iconContent: $Adress
    }, {
        preset: 'islands#violetStretchyIcon',
        draggable: true
    });
    myPlacemark.events.add('dragend', OnDragEnd);
    myMap.geoObjects.add(myPlacemark);

    myMap.setBounds(myMap.geoObjects.getBounds(), {
        // Проверяем наличие тайлов на данном масштабе.
        checkZoomRange: true
    });
}

function GetCoords()
{
    $Adress = $("#Adress").val();
    $ApartmentId = $("#ApartmentId").val();
    ShowLoading('map', '#ajax_loader', 1);
    myMap.geoObjects.re
    ymaps.geocode($Adress, {
        results: 1
    }).then(function (res) {
        // Выбираем первый результат геокодирования.
        var firstGeoObject = res.geoObjects.get(0),
            // Координаты геообъекта.
            coords = firstGeoObject.geometry.getCoordinates()
        myMap.geoObjects.remove(myPlacemark);
        // Добавляем первый найденный геообъект на карту.
        myPlacemark = new ymaps.Placemark(coords, {
            iconContent: $Adress
        }, {
            preset: 'islands#violetStretchyIcon',
            draggable: true
        });
        myPlacemark.events.add('dragend', OnDragEnd);
        myMap.geoObjects.add(myPlacemark);
        // Масштабируем карту на область видимости геообъекта.
        myMap.setBounds(myMap.geoObjects.getBounds(), {
            // Проверяем наличие тайлов на данном масштабе.
            checkZoomRange: true
        });
        HideLoading('#ajax_loader');
        SaveCoords($ApartmentId, coords[0], coords[1]);
        
    });
    return;
}
function SaveCoords(ApartmentId, lat, lon){
    var value = { ApartmentId: ApartmentId, Latitude: lat, Longitude: lon };
    $("#SaveCoordsButton").attr("disabled", "disabled");
    ShowLoading('map', '#ajax_loader', 1);
    $.post("/rent/EditApartmentMap", value, function () {
        HideLoading('#ajax_loader');
    });
}
function SaveManualCoords() {
    $ApartmentId = $("#ApartmentId").val();
    SaveCoords($ApartmentId, newCoords[0], newCoords[1]);
}
function OnDragEnd(event) {
    $("#SaveCoordsButton").removeAttr("disabled");
    newCoords = myPlacemark.geometry.getCoordinates();
    newLat = newCoords[0];
    newLon = newCoords[1];
}