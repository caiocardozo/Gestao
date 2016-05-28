var geocoder;
var map;
var marker;

function initialize() {
    var latlng = new google.maps.LatLng(-16.6868912, -49.264794300000005);
	var options = {
		zoom: 5,
		center: latlng,
		mapTypeId: google.maps.MapTypeId.ROADMAP
	};
	
	map = new google.maps.Map(document.getElementById("mapa"), options);
	
	geocoder = new google.maps.Geocoder();
	
	marker = new google.maps.Marker({
		map: map,
		draggable: true,
	});
	
	marker.setPosition(latlng);
}

$(document).ready(function () {

	initialize();
	
	function carregarNoMapa(endereco) {
		geocoder.geocode({ 'address': endereco + ', Brasil', 'region': 'BR' }, function (results, status) {
			if (status == google.maps.GeocoderStatus.OK) {
				if (results[0]) {
					var latitude = results[0].geometry.location.lat();
					var longitude = results[0].geometry.location.lng();
		
					$('#pres_endereco').val(results[0].formatted_address);
					$('#pres_Latitude').val(latitude);
                   	$('#pres_Longitude').val(longitude);
		
					var location = new google.maps.LatLng(latitude, longitude);
					marker.setPosition(location);
					map.setCenter(location);
					map.setZoom(16);
				}
			}
		})
	}
	
	$("#btnEndereco").click(function() {
		if($(this).val() != "")
		    carregarNoMapa($("#pres_endereco").val());
	})
	
	$("#pres_endereco").blur(function () {
		if($(this).val() != "")
			carregarNoMapa($(this).val());
	})
	
	google.maps.event.addListener(marker, 'drag', function () {
		geocoder.geocode({ 'latLng': marker.getPosition() }, function (results, status) {
			if (status == google.maps.GeocoderStatus.OK) {
				if (results[0]) {  
				    $('#pres_endereco').val(results[0].formatted_address);
					$('#pres_Latitude').val(marker.getPosition().lat());
					$('#pres_Longitude').val(marker.getPosition().lng());
				}
			}
		});
	});
	
	$("#pres_endereco").autoComplete({
		source: function (request, response) {
			geocoder.geocode({ 'address': request.term + ', Brasil', 'region': 'BR' }, function (results, status) {
				response($.map(results, function (item) {
					return {
						label: item.formatted_address,
						value: item.formatted_address,
						latitude: item.geometry.location.lat(),
          				longitude: item.geometry.location.lng()
					}
				}));
			})
		},
		select: function (event, ui) {
			$("#pres_Latitude").val(ui.item.latitude);
    		$("#pres_Longitude").val(ui.item.longitude);
			var location = new google.maps.LatLng(ui.item.latitude, ui.item.longitude);
			marker.setPosition(location);
			map.setCenter(location);
			map.setZoom(16);
		}
	});
	
	$("form").submit(function(event) {
		event.preventDefault();
		
		var endereco = $("#pres_endereco").val();
		var latitude = $("#pres_Latitude").val();
		var longitude = $("#pres_Longitude").val();
		
		alert("Endereço: " + endereco + "\nLatitude: " + latitude + "\nLongitude: " + longitude);
	});

});