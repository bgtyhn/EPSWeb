$(function(){
	if(sessionStorage.getItem('rol')!='afiliado'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));

	//Generar lista de cursos

	//Obtener de url
	var idAfiliado = sessionStorage.getItem('id');

	mostrarCursos()

	function mostrarCursos(){
		var message = { 
			accion : 'mostrarInfoCursos',
			mensaje : '',
			valores : { 
				"idUsuario" : idAfiliado
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
            	llenarCursos(data.datos)
            },
            error: function (info) {
                alert("error");
            }
        });
	}

	function llenarEventos(eventos, lista){
		$.each(eventos, function(i, e){
				lista.append(
				"<li class='list-group-item'><strong>Sesión "+i+": </strong>"+e.fecha+", de "+e.duracion_minutos+" minutos</li>"
			);
		});
		if(eventos.length == 0)
			lista.append("<h4>No hay eventos registrados para este curso</h4>")
	}

	function cargarEventosCurso(curso, listaEventos) {
		//ajax evtos de curso

		var message = { 
			accion : 'verEventosCurso',
			mensaje : '',
			valores : {
				"idCurso" : curso.id

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
                llenarEventos(data.datos, listaEventos)
            },
            error: function (info) {
                alert("error");
            }
        });

	}

	function llenarCursos(cursos){

		var lista = $("#lista-cursos");

		lista.empty();

		$.each(cursos, function(i, c){

			var listaEventos = $("<ul></ul>").addClass('list-group');
			
			cargarEventosCurso(c, listaEventos);

			

			var mensaje = "";

			if(c.requisitos == "no" || c.completo == "si"){
				mensaje = "<span class='curso-aviso-inscripcion pull-right'>No cumple con los requisitos o se agotaron los cupos</span>"
			}

			var boton = "";

			if (c.inscrito == "inscrito") {
				boton = '<button class="btn btn-success pull-right" disabled ="disabled">Ya está inscrito</button>';
			} else {
				boton = $("<button>Inscribirse</button>").addClass("btn btn-primary pull-right").click(function(event) {
					mostrarModalInscripcion(c);
				});
			}

			lista.append(
				$("<div></div>").addClass("afiliado-cursos").append(
					$("<div></div>").addClass('panel panel-default').append(
						$("<div></div>").addClass("panel-heading").append(
							$("<h3>"+c.nombre+"</h3>").addClass("panel-title")
						)).append(
						$("<div></div>").addClass('panel-body').append(
							$("<span>"+c.descripcion+"</span>")
						).append($("<br>")).append($("<br>")).append(
							$("<span><strong>Cupos: </strong>"+c.maximo_personas+"</span>")
						).append($("<br>")).append(
							$("<span><strong>Encargado: </strong>"+c.profesional+"</span>")
						).append(
							$("<hr>")
						).append(
							listaEventos
						).append(
							mensaje
						)).append(
						$("<div></div>").addClass("panel-footer").append(
							boton
						).append(
							$("<div></div>").addClass("clearfix")
						)
					)
				)
			)

		});

	}

	function mostrarModalInscripcion(curso){
		var nombre = curso.nombre;
		$("#modal-curso-nombre").html("Seguro que se va a inscribir al curso "+nombre+"?");
		$('#modal-curso').modal('show');
		cursoInscribir = curso.id;
	}

	$("#curso-inscribir").click(function(event) {
		inscribirCurso(idAfiliado, cursoInscribir);
	});

	function inscribirCurso(idAfiliado, cursoInscribir){
		var message = { 
			accion : 'inscribirPersonaCurso',
			mensaje : '',
			valores : {
				"idCurso" : cursoInscribir,
				"idAfiliado" : idAfiliado
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
            	$('#modal-curso').modal('hide');
                mostrarCursos();
            },
            error: function (info) {
                alert("error");
            }
        });
		
	}

})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}