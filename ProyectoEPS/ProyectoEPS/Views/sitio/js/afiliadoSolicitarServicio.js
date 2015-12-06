$(function(){
	if(sessionStorage.getItem('rol')!='afiliado'){
		window.location.href = '../../';
	}
	var doctorSel = "";
	var areaSel = "";

	idSesion = sessionStorage.getItem('id');

	$("#sesion-nombre").html(sessionStorage.getItem('id'));

	//Partes invisibles
	$("#cita-profesionales-div").css('visibility', 'hidden');
	$("#cita-horarios-div").css('visibility', 'hidden');

	$("#cita-area-sel").val("");

	//Click a algun elemento del option
	$("#cita-area-sel").change(function(event) {
		areaSel = $("#cita-area-sel option:selected").val();
		if(areaSel != ""){
			mostrarProfesionales(areaSel);
			$("#cita-horarios-div").css('visibility', 'hidden');
		}
	});

	$("#cita-form").on("submit", function(event){
		event.preventDefault();

		var message = { 
			accion : 'nuevaCita',
			mensaje : '',
			valores : {
				"fechaCita" : $("#cita-fecha").html(),
				"duracionCita" : $("#cita-duracion").val(),
				"tipoAtencionCita" : $("#cita-tipoatencion").val(),
				"calificacionCita" : 0,
				"profesionalCita" : $("#cita-doctor").html(),
				"afiliadoCita" : idSesion
			}
		}

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Citas/accionCitas',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log(data);
            	mostrarProfesionales(areaSel);
            	$("#cita-horarios-list").empty();
            	mostrarHorariosProfesional({id : doctorSel, nombre : ""});
            	$("#cita-form").trigger("reset");
            	$("#modal-solicitar-servicio").modal("hide");
            },
            error: function (info) {
                alert("error");
            }
        });


	})

	function mostrarProfesionales(area){

		var message = { 
			accion : 'medicosArea',
			mensaje : '',
			valores : {
				"areaProfesional" : area
			}
		}

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Profesional/accionProfesionales',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log(data);
            	llenarListaProfesionales(data.datos)
            },
            error: function (info) {
                alert("error");
            }
        });


		//en #cita-profesionales-div hacer visible
		
	}

	function llenarListaProfesionales(profesionales){
		$("#cita-profesionales-div").css('visibility', 'visible');

		/*
		en #cita-profesionales-list agregar cada profesional del value del option

			formato:
				<a href="#" class="list-group-item">Doctor 1</a>
                <a href="#" class="list-group-item">Doctor 2</a>
                <a href="#" class="list-group-item">Doctor 3</a>
                <a href="#" class="list-group-item">Doctor 4</a>
                <a href="#" class="list-group-item">Doctor 5</a>
		*/

		var list = $("#cita-profesionales-list");

		list.empty();

		$.each(profesionales, function(i, profesional){
			list.append(
				$("<a href='javascript:void(0)'>"+profesional.nombre+" "+profesional.apellidos+"</a>").addClass("list-group-item").click(function(event) {
					


					//Cosa bonita
					$.each($("#cita-profesionales-list > a"), function(i, o){
						$(o).removeClass('active');
					});

					$(event.target).addClass('active');

					//Variable doctor selecccionado
					doctorSel = profesional.id;

					mostrarHorariosProfesional(profesional);
				})
			)
		});
	}

	//click en alguno llama funcion con id de doctor, funcion hace:

	function mostrarHorariosProfesional(profesional){



		//#cita-horarios-div visible = si
		$("#txthdisp").html("Horarios disponibles "+profesional.nombre);
		$("#cita-horarios-div").css('visibility', 'visible');

		

		var message = { 
			accion : 'fechasDisponibles',
			mensaje : '',
			valores : {
						"idProfesional" : profesional.id
					}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/Profesional/accionProfesionales',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	listarFechas(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });


		//Obtener fecha y horas de los date recibidos

		

	}

	function convertir12a24(hora12){
		var time = hora12;
		var hours = Number(time.match(/^(\d+)/)[1]);
		var minutes = Number(time.match(/:(\d+)/)[1]);
		var AMPM = time.match(/\s(.*)$/)[1];
		if(AMPM == "PM" && hours<12) hours = hours+12;
		if(AMPM == "AM" && hours==12) hours = hours-12;
		var sHours = hours.toString();
		var sMinutes = minutes.toString();
		if(hours<10) sHours = "0" + sHours;
		if(minutes<10) sMinutes = "0" + sMinutes;
		return sHours + ":" + sMinutes;
	}

	function formatearFecha(fechaFea){

		fechaFea = fechaFea.split("-")

		var year = "20"+fechaFea[2];

		var months = ["JAN", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DEC"]

		var month = months.indexOf(fechaFea[1]) + 1;

		var day = fechaFea[0]

		return year+"/"+month+"/"+day		
	}

	function listarFechas(horarios){



		var list = $("#cita-horarios-list");

		list.empty();

		$.each(horarios, function(i, h){

			var cadFecha = []

			for(i = 0; i < h.length; i++){
				var arrInicio = h[i].split(" ")
				var arrHora = arrInicio[1].split(".")
				var hora24 = convertir12a24(arrHora[0]+":"+arrHora[1]+" "+arrInicio[2]);
				var fechaFormateada = formatearFecha(arrInicio[0]);
				cadFecha.push(fechaFormateada+" "+hora24);
			}

			list.append(
				$("<a href='javascript:void(0)'></a>").addClass('list-group-item').append(
					$("<h5><strong>Inicio: </strong>"+cadFecha[0]+"</h5>")).append(
					$("<h5><strong>Fin: </strong>"+cadFecha[1]+"</h5>")
				).click(function(event){
					mostrarModalCita(cadFecha);
				})
			)
		});
	}

	//Recibir fecha de inicio, tomar profesional del seleccionado de profesionales
	function mostrarModalCita(fechaInicio){
		$("#cita-doctor").html(doctorSel);
		$("#cita-area").html(areaSel);
		$("#cita-fecha").html(fechaInicio[0]);
		$("#modal-solicitar-servicio").modal('show');
	}

	$("#cita-cancelar").click(function(event) {
		$("#modal-solicitar-servicio").modal('hide');	
		$("#cita-form").trigger('reset');
	});

});

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}









