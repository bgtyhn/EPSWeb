$(function(){
	var doctorSel = "";
	var areaSel = "";

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

	function mostrarProfesionales(area){
		//en #cita-profesionales-div hacer visible
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

		var vars = [0,1,2,3,4];

		$.each(vars, function(i, v){
			list.append(
				$("<a href='javascript:void(0)'>Doctor "+area+" "+vars[i]+"</a>").addClass("list-group-item").click(function(event) {
					
					//Cosa bonita
					$.each($("#cita-profesionales-list > a"), function(i, o){
						$(o).removeClass('active');
					});

					$(event.target).addClass('active');

					//Variable doctor selecccionado
					doctorSel = vars[i];

					mostrarHorariosProfesional(vars[i]);
				})
			)
		});
	}

	//click en alguno llama funcion con id de doctor, funcion hace:

	function mostrarHorariosProfesional(doctor){
		//#cita-horarios-div visible = si
		$("#txthdisp").html("Horarios disponibles "+doctorSel);
		$("#cita-horarios-div").css('visibility', 'visible');

		var list = $("#cita-horarios-list");

		list.empty();

		var horarios = [];
		
		for(i = 0; i < 6; i++){
			horarios.push({
				fecha : "Fecha "+i,
				horaInicio : "Hora inicio "+i,
				horaFin : "Hora fin "+i
			});
		}

		/*
		#cita-horarios-list agrega cada "horario disponible en formato:"

				<a href="#" class="list-group-item">
                  <h5><strong>Fecha:</strong> fecha</h5>
                  <span>Hora inicio - Hora fin</span>
                </a>
                <a href="#" class="list-group-item">
                  <h5><strong>Fecha:</strong> fecha</h5>
                  <span>Hora inicio - Hora fin</span>
                </a>
                <a href="#" class="list-group-item">
                  <h5><strong>Fecha:</strong> fecha</h5>
                  <span>Hora inicio - Hora fin</span>
                </a>
                <a href="#" class="list-group-item">
                  <h5><strong>Fecha:</strong> fecha</h5>
                  <span>Hora inicio - Hora fin</span>
                </a>
		*/

		//Obtener fecha y horas de los date recibidos

		$.each(horarios, function(i, h){
			list.append(
				$("<a href='javascript:void(0)'></a>").addClass('list-group-item').append(
					$("<h5><strong>Fecha: </strong>"+h.fecha+"</h5>")).append(
					$("<span>"+h.horaInicio+" - "+h.horaFin+"</span>")	
				).click(function(event){
					mostrarModalCita(h);
				})
			)
		});

	}

	//Recibir fecha de inicio, tomar profesional del seleccionado de profesionales
	function mostrarModalCita(fechaInicio){
		$("#cita-doctor").html(doctorSel);
		$("#cita-area").html(areaSel);
		$("#cita-fecha").html(fechaInicio.fecha);
		$("#modal-solicitar-servicio").modal('show');
	}

	$("#cita-cancelar").click(function(event) {
		$("#modal-solicitar-servicio").modal('hide');	
		$("#cita-form").trigger('reset');
	});

});









