$(function(){
	if(sessionStorage.getItem('rol')!='afiliado'){
		window.location.href = '../../';
	}

	var idSesion = sessionStorage.getItem('id');

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	$("#cita-pendiente-area-sel").val("todas");

	var area = $("#cita-pendiente-area-sel option:selected").val();

	cargarCitasPendientes();

	$("#cita-pendiente-area-sel").change(function(event) {
		areaSel = $("#cita-pendiente-area-sel option:selected").val();
		if(areaSel == "todas")
			cargarCitasPendientes()
		else
			cargarCitasArea(areaSel);
	});

	function cargarCitasArea(area){
		
		var message = { 
				accion : 'citasPendientesArea',
				mensaje : '',
				valores : {
							"idUsuario" : idSesion,
							"areaProfesional" : area
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Citas/accionCitas',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	llenarCitas(data.datos)
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });


	}

	function cargarCitasPendientes(){
		var message = { 
				accion : 'citasPendientesUsuario',
				mensaje : '',
				valores : {
							"idUsuario" : idSesion
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Citas/accionCitas',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	llenarCitas(data.datos)
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });


	}

	function llenarCitas(citas){

		$("#citas-lista").empty();

		//poner citas en paneles

		$.each(citas, function(i, c){

			var panel = $("<div></div>").addClass("panel panel-default cita-afiliado-panel");

			var panelBody = $("<div></div>").addClass("panel-body");

			var row = $("<div></div>").addClass("row");

			row.append([
				$("<h4>"+c.fecha+"</h4>").addClass("h4 col-xs-4"),
				$("<div></div>").addClass("col-xs-5 col-xs-offset-3").append(
					$("<h4>"+c.tipo_atencion+"</h4>").addClass("h4 pull-right")	
				)
			]);

			var doc = $("<h4>"+c.profesional+"</h4>");
			var area = $("<h4>Duración: "+c.duracion_minutos+" minutos</h4>");

			panel.append(panelBody.append([row, $("<hr>")]).append([doc, area]));

			$("#citas-lista").append(panel);

		})

	}


})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}