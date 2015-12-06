$(function(){
	
	if(sessionStorage.getItem('rol')!='administrador'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));

	var eventos = []

	$("form").trigger("reset")

	$(".form-control").prop("disabled", false)

	llenarSelectProfesionales();

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
			accion : 'crearUnCurso',
			mensaje : '',
			valores : { 
				"nombreCurso" : $("#curso-nombre").val(),
				"descripcionCurso" : $("#curso-descripcion").val(),
				"sitioCurso" : $("#curso-sitio").val(),
				"maximosPersonasCurso" : $("#curso-cupo").val(),
				"profesionalCurso" : $("#curso-profesional option:selected").val()
			}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };
		console.log(message)
		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Cursos/accionCursos',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
            	console.log(data);
            	if(data.exito == 1){
            		toastr.success("Curso agregado con éxito")
            		$("form").trigger("reset")
            	}
            	else
            		toastr.warning("Error creando el curso")
            },
            error: function (info) {
                alert("error");
            }
        });
	})

	function crearEventos(){

        $.each(eventos, function(i, evento){
        	var message = { 
				accion : 'crearEvento',
				mensaje : '',
				valores : { 
					"fechaCurso" : evento.fecha+" "+evento.hora,
					"idCurso" : $("#curso-nombre").val(),
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

        $("form").trigger("reset");

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

		$("#curso-profesional").select2();
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
		              '<div class="form-group">'+
		                '<button id="add-evento-'+(indice+1)+'" class="btn btn-success pull-right">'+
		                  '<span class="glyphicon glyphicon-plus">'+
		                  '</span>'+
		                '</button>'+
		              '</div>'+
		              '<div class="clearfix"></div>'+
		            '</div>'+
		          '</form>'

        $("#eventosCurso").append(nuevoForm);

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