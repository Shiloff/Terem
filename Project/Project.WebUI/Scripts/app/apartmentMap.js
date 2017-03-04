ymaps.ready(init);
var myMap, myPlacemark;
var $lat, $lon, $Adress;

function init() {
    myMap = new ymaps.Map("map", {
        center: [55.76, 37.64],
        zoom: 7
    });
    $lat = $("#Latitude").val();
    $lon = $("#Longitude").val();

    if (($lat != "") || ($lon != "")) {
        $Adress = $("#Adress").val();
        myPlacemark = new ymaps.Placemark([$lat.replace(",", "."), $lon.replace(",", ".")], {
            iconContent: $Adress
        }, {
            preset: 'islands#violetStretchyIcon'
        });
        myMap.geoObjects.add(myPlacemark);
        myMap.setBounds(myMap.geoObjects.getBounds(), {
            // Проверяем наличие тайлов на данном масштабе.
            checkZoomRange: true
        });
    }
   
} 