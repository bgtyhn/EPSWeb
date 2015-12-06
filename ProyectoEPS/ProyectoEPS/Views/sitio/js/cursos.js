$(function(){

	var idCurso = $.getUrlVar('id')

	cargarInformacionCurso();

	function cargarInformacionCurso(){
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
	}

	function llenarCampos(curso){
		$("#curso-nombre").html(curso.nombre)
		$("#curso-descripcion").html(curso.descripcion)
		$("#curso-cupos").html("Cupos: "+curso.maximo_personas)
		cargarEventos();
	}

	function cargarEventos(){
		
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
            	console.log(data)
                llenarEventos(data.datos)
            },
            error: function (info) {
                alert("error");
            }
        });

	}

	function llenarEventos(eventos){
		$("#curso-eventos").empty()

		$.each(eventos, function(i, evento){
			var chtml = '<div class="list-group-item">'+
								'<h3 class="list-group-item-heading">'+
									evento.fecha+
								'</h3>'+
								'<h5 class="list-group-item-text">'+
									evento.duracion_minutos + ' minutos '+
								'</h5>'+
							'</div>'
			$("#curso-eventos").append(chtml)
		})

		if(eventos.length == 0){
			$("#curso-eventos").append("<h3>No hay eventos registrados para este curso</h3>")
		}
	}


});