$(function(){
	if(sessionStorage.getItem('rol')!='administrador'){
		window.location.href = '../../';
	}


	$("#sesion-nombre").html(sessionStorage.getItem('id'));

























                             


                                ñkjhñkjhñkhñkjhñkjhñ  ññ ñ 

	function mostrarCursos(){
		var message = { 
			accion : 'verTodosLosCursos',
			mensaje : '',
			valores : { 
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
       
	$("#curso-agregar").click(function(){
		window.location.href = './nuevocurso.html';
	})


	function llenarCursos(cursos){

		$("#lista-cursos").empty();

		$.each(cursos, function(i, curso){

			var div = $("<div></div>").addClass('main-noticia');

			var html = '<div class="panel panel-default">'+
		                    '<div class="panel-heading">'+
		                      '<h3 class="panel-title title-noticia">'+
		                        curso.nombre+
		                      '</h3>'+
		                    '</div>'+
		                    '<div class="panel-body">'+
		                      '<h5 class="h5 text-justify">'+
		                        curso.descripcion+
		                      '</h5>'+
		                      '<h4>'+
		                        '<strong>'+
		                        	curso.sitio+
		                        '</strong>'+
		                      '</h4>'+
		                      '<h4>'+
		                        '<strong>Cupo: '+curso.maximo_personas+'</strong>'+
		                      '</h4>'+
		                      '<h4>'+
		                        '<strong>Profesional: </strong>'+
		                        '<span>'+curso.profesional+'</span>'+
		                      '</h4>'+
		                      '<div id="eventos-'+curso.id+'" class="col-xs-8 col-xs-offset-2">'+
		                        '<h4>'+
		                          'Eventos:'+
		                        '</h4>'+
		                        '<div class="list-group">'+
		                          
		                        '</div>'+
		                        '</div>'+
		                    '</div>'+
		                    '<div class="panel-footer">'+
		                      '<button id="borrar-'+curso.id+'" class="btn btn-danger pull-right">'+
		                        '<span class="glyphicon glyphicon-trash"></span>'+
		                      '</button>'+
		                      '<span class="pull-right">&nbsp;&nbsp;</span>'+
		                      '<button id="editar-'+curso.id+'" class="btn btn-warning pull-right">'+
		                        '<span class="glyphicon glyphicon-edit"></span>'+
		                      '</button>'+
		                      '<div class="clearfix"></div>'+
		                    '</div>'+
		                  '</div>';

	    		$("#lista-cursos").append(html)

	    		$("#editar-"+curso.id).click(function(){
	    			window.location.href = './editarCurso.html?id='+curso.id;
	    		})

	    		$("#borrar-"+curso.id).click(function(){
	    			console.log("hi")
	    			eliminarCurso(curso.id);
	    		})

	    		cargarEventos(curso.id);

		})

	}

	function eliminarCurso(id){
		var message = { 
			accion : 'eliminarCurso',
			mensaje : '',
			valores : { 
				"idCurso" : id
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
            		toastr.success("Curso eliminado con exito")
            		mostrarCursos();
            	}
            	else
            		toastr.warning("No se pudo eliminar")
            },
            error: function (info) {
                alert("error");
            }
        });
	}

	function cargarEventos(cursoId){


		var message = { 
			accion : 'verEventosCurso',
			mensaje : '',
			valores : { 
				"idCurso" : cursoId
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
            	llenarEventos(cursoId, data.datos)
            },
            error: function (info) {
                alert("error");
            }
        });

	}

	function llenarEventos(curso, eventos){

		var listado = $("#eventos-"+curso)


		$.each(eventos, function(i, evento){

			listado.append($("<div>"+evento.fecha+" - "+evento.duracion_minutos+" minutos </div>").addClass("list-group-item"))
		})

		if(eventos.length == 0){
			listado.append($("<h4>Sin eventos</h4>"))
		}

	}


})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}