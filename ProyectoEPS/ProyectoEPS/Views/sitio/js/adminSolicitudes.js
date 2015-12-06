$(function(){
	if(sessionStorage.getItem('rol')!='administrador'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	cargarTablas();

	function cargarTablas(){
		cargarTablaSolicitudes();
		cargarTablaAnteriores();
	}

	function cargarTablaSolicitudes(){

		$("#lista-solicitudes").empty();

		//Traer de bd

		var message = { 
			accion : 'verSolicitudesPendientes',
			mensaje : '',
			valores : {
					}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/Solicitudes/accionSolicitudes',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	llenarTabla(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });
	}

	function cargarTablaAnteriores(){

		$("#lista-solicitudes-anteriores").empty();

		//Traer de bd

		var message = { 
			accion : 'verTodasLasSolicitudes',
			mensaje : '',
			valores : {
					}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/Solicitudes/accionSolicitudes',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	llenarTablaAnteriores(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });
	}

	function aceptarSolicitud(solicitud){
		//aprobarSolicitud

		var message = { 
			accion : 'aprobarSolicitud',
			mensaje : '',
			valores : {
					"numeroSolicitud" : solicitud.numero
					}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/Solicitudes/accionSolicitudes',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	cargarTablas();
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

	}

	function rechazarSolicitud(solicitud){
		//aprobarSolicitud

		var message = { 
			accion : 'rechazarSolicitud',
			mensaje : '',
			valores : {
					"numeroSolicitud" : solicitud.numero
					}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/Solicitudes/accionSolicitudes',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	cargarTablas();
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

	}


	function llenarTabla(solicitudes){
		$.each(solicitudes, function(i, solicitud){

			var tr = $("<tr></tr>");

			tr.append([

					$("<td>"+solicitud.id+"</td>"),
					$("<td>"+solicitud.password+"</td>"),
					$("<td>"+solicitud.nombre+"</td>"),
					$("<td>"+solicitud.apellidos+"</td>"),
					$("<td>"+solicitud.cedula+"</td>"),
					$("<td>"+solicitud.edad+"</td>"),
					$("<td>"+solicitud.correo+"</td>"),
					$("<td>"+solicitud.estado+"</td>"),
					$("<td>"+solicitud.categoria+"</td>"),
					$("<td></td>").append([
						$("<button></button>").addClass("btn btn-xs btn-success").append(
								$("<span><span>").addClass("glyphicon glyphicon-ok")
							).click(function(){
									aceptarSolicitud(solicitud);
								}),
						$("<span>&nbsp;&nbsp;</span>"),
						$("<button></button>").addClass("btn btn-xs btn-danger").append(
								$("<span><span>").addClass("glyphicon glyphicon-remove")
							).click(function(event){
								rechazarSolicitud(solicitud)
							})
						]
					)
				])

			$("#lista-solicitudes").append(tr)

		})
	}

	function llenarTablaAnteriores(solicitudes){
		$.each(solicitudes, function(i, solicitud){

			if(solicitud.estado != "pendiente"){
				var tr = $("<tr></tr>");

				tr.append([

						$("<td>"+solicitud.id+"</td>"),
						$("<td>"+solicitud.password+"</td>"),
						$("<td>"+solicitud.nombre+"</td>"),
						$("<td>"+solicitud.apellidos+"</td>"),
						$("<td>"+solicitud.cedula+"</td>"),
						$("<td>"+solicitud.edad+"</td>"),
						$("<td>"+solicitud.correo+"</td>"),
						$("<td>"+solicitud.estado+"</td>"),
						$("<td>"+solicitud.categoria+"</td>")
					])

				$("#lista-solicitudes-anteriores").append(tr)
			}
		})
	}

})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}