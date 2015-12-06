$(function(){

	if(sessionStorage.getItem('rol')!='administrador'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));

	var eventos = []

	var idCurso = $.getUrlVar('id')

	llenarSelectProfesionales();

	cargarValoresCampos();

	$(".form-control").prop("disabled", false)

	$("#form-evento-0").trigger("reset")

	$("#evento-fecha-0").datepicker({
		format:'yyyy/mm/dd'
	});

	$("#form-evento-0").on("submit", function(event){
		event.preventDefault();
		submitEvento(0);
	})

	$("#form-curso").on("submit", function(event){
		event.preventDefault();

		var message = { 
			accion : 'editarCurso',
			mensaje : '',
			valores : { 
				"idCurso" : idCurso,
				"nombreCurso" : $("#curso-nombre").val(),
				"descripcionCurso" : $("#curso-descripcion").val(),
				"sitioCurso" : $("#curso-sitio").val(),
				"maximosPersonasCurso" : $("#curso-cupo").val(),
				"profesionalCurso" : $("#curso-profesional option:selected").val()
			}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };
		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Cursos/accionCursos',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log(data);
            	if(data.exito == 1){
            		crearEventos()
            		toastr.success("Curso editado con éxito")
            	}
            	else
            		toastr.warning("Error creando el curso")
            },
            error: function (info) {
                alert("error");
            }
        });
	})

	function cargarValoresCampos(){
		var message = { 
			accion : 'detalleCurso',
			mensaje : '',
			valores : {
				"idCurso" : idCurso

			}
		}

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Cursos/accionCursos',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log(data)
                llenarCampos(data.datos)
            },
            error: function (info) {
                alert("error");
            }
        });

        cargarEventosExistentes()
        
	}

	function cargarEventosExistentes(){

		var message = { 
			accion : 'verEventosCurso',
			mensaje : '',
			valores : {
				"idCurso" : idCurso

			}
		}

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Cursos/accionCursos',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log("eventos existentes: ")
            	console.log(data)
                llenarEventos(data.datos)
            },
            error: function (info) {
                alert("error");
            }
        });
	}

	function llenarEventos(eventos){

		$.each(eventos, function(i, evento){
			var nuevoForm ='<form>'+
		              '<div class="form-group">'+
		                '<label>Fecha</label>'+
		                '<input value="'+evento.fecha+'" type="text" placeholder="Fecha del evento" class="form-control" disabled>'+
		              '</div>'+
		              '<div class="form-group">'+
		                '<label>Duración</label>'+
		                '<input value="'+evento.duracion_minutos+'" type="number" placeholder="Duración del evento en minutos" class="form-control" disabled>'+
		              '</div>'+
		            '</div>'+
		          '</form><hr>'

		    $("#eventosCurso").append(nuevoForm)
		})


	}

	function llenarCampos(curso){
		console.log(curso)
		$("#curso-nombre").val(curso.nombre)
		$("#curso-descripcion").val(curso.descripcion)
		$("#curso-sitio").val(curso.sitio)
		$("#curso-cupo").val(curso.maximo_personas)
		$("#curso-profesional").val(curso.profesional)
		$("#curso-profesional").select2()
	}

	function crearEventos(){

        $.each(eventos, function(i, evento){
        	var message = { 
				accion : 'crearEvento',
				mensaje : '',
				valores : { 
					"fechaCurso" : evento.fecha+" "+evento.hora,
					"idCurso" : idCurso,
					"duracionEvento" : evento.duracion
				}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
				async : false,
	            type: 'POST',
	            url: 'http://localhost:6570/api/Cursos/accionCursos',
	            data: JSON.stringify(message),
	            contentType: 'application/json; charset=utf-8',
	            dataType: 'json',
	            success: function (data) {
	            	console.log(data);
	            },
	            error: function (info) {
	                alert("error");
	            }
	        });
        })

        //toastr.success("Curso agregado con éxito")

	}

	function llenarSelectProfesionales(){
		var message = { 
			accion : 'listarProfesionales',
			mensaje : '',
			valores : {}
		}

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Profesional/accionProfesionales',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log(data)
                llenarSelect(data.datos)
            },
            error: function (info) {
                alert("error");
            }
        });
	}

	function llenarSelect(profesionales){

		var select = $("#curso-profesional");

		$.each(profesionales, function(i, profesional){
			select.append($("<option value='"+profesional.id+"'>"+profesional.nombre+"</option>"))
		})

	}

	function submitEvento(indice){

		var h = $("#evento-hora-"+indice)
		var f = $("#evento-fecha-"+indice)
		var d = $("#evento-duracion-"+indice)

		eventos.push({
			hora : h.val(),
			fecha : f.val(),
			duracion : d.val()
		})

		h.prop("disabled", true)
		f.prop("disabled", true)
		d.prop("disabled", true)

		var nuevoForm ='<form id="form-evento-'+(indice+1)+'">'+
          			  '<div class="form-group">'+
                		'<label for="evento-hora-'+(indice+1)+'">Hora</label>'+
                    	'<input type="text" placeholder="Hora del evento" class="form-control" id="evento-hora-'+(indice+1)+'"  required>'+
                      '</div>'+
		              '<div class="form-group">'+
		                '<label for="evento-fecha-'+(indice+1)+'">Fecha</label>'+
		                '<input type="text" placeholder="Fecha del evento" class="form-control" id="evento-fecha-'+(indice+1)+'"  required>'+
		              '</div>'+
		              '<div class="form-group">'+
		                '<label for="evento-duracion-'+(indice+1)+'">Duración</label>'+
		                '<input type="number" placeholder="Duración del evento en minutos" class="form-control" id="evento-duracion-'+(indice+1)+'"  required>'+
		              '</div>'+
		            '</div>'+
		          '</form>'

        $("#eventosCurso").prepend(nuevoForm);

        $("#evento-fecha-"+(indice+1)).datepicker({
        	format:'yyyy/mm/dd'
        });

        var x = '#form-evento-'+(indice+1)

        $(x).on("submit", function(event){
        	event.preventDefault();
			submitEvento(indice+1);
        })

	}


})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}