$(function(){

	if(sessionStorage.getItem('id') != null){
		window.location.href = './vistas/'+sessionStorage.getItem('rol')+'/';
	}

	$("#login-form").submit(function(event) {
		event.preventDefault();

		//login-rol
		//login-email
		//login-password

		var message = { 
			accion : 'inicioSesion',
			mensaje : '',
			valores : {
						"userCorreo" : $("#login-email").val(),
						"userPassword" : $("#login-password").val(),
						"tipoUsuario" : $("#login-rol").val()
					}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
	        type: 'POST',
	        url: 'http://localhost:6570/api/Usuarios/inicioSesion',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);

	        	iniciarSesion(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

	});

	function iniciarSesion(datos){
		if(datos.operacionExitosa){
			var id = datos.id
			var rol = datos.rol

			sessionStorage.setItem('id', id)
			sessionStorage.setItem('rol', rol)

			switch(rol){
				case "administrador":
					window.location.href = './vistas/administrador/';
					break;
				case "afiliado":
					window.location.href = './vistas/afiliado/';
					break;
				case "atencioncliente":
					window.location.href = './vistas/atencioncliente/';
					break;
				case "profesional":
					window.location.href = './vistas/profesional/';
					break;
			}
		} else {

			$("#login-form").trigger("reset");

		}

	}

	$("#solicitud-form").submit(function(event) {
		event.preventDefault();
		
		var message = { 
			accion : 'nuevaSolicitud',
			mensaje : '',
			valores : {
						"idAfiliado" : $("#solicitud-nombreusuario").val(),
						"passwordAfiliado" : $("#solicitud-password").val(),
						"nombreAfiliado" : $("#solicitud-nombre").val(),
						"apellidosAfiliado" :$("#solicitud-apellido").val(),
						"categoriaAfiliado" : $("#solicitud-categoria option:selected").val(),
						"edadAfiliado" : $("#solicitud-edad").val(),
						"correoAfiliado" : $("#solicitud-correo").val(),
						"cedulaAfiliado" : $("#solicitud-cedula").val()
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
	        	$("#solicitud-form").trigger("reset");
				$("#modal-solicitud-registro").modal('hide');
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

	});

	$("#a-solicitar-registro").click(function(event) {
		$("#modal-solicitud-registro").modal('show');
	});

	$("#solicitud-cancelar").click(function(event) {
		$("#solicitud-form").trigger("reset");
		$("#modal-solicitud-registro").modal('hide');
	});

	cargarNoticias()

	cargarCursos()

	function cargarNoticias(){

		var message = { 
			accion : 'mostrarNoticiasTodas',
			mensaje : '',
			valores : {}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Noticia/accionNoticias',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                crearNoticias(data.datos);
            },
            error: function (info) {
                alert("error");
            }
        });

/*
					<div class="main-noticia">
						<div class="panel panel-default">
							<div class="panel-heading">
								<a href="noticias.html">
									<h3 class="panel-title title-noticia">
										titulo Lorem ipsum dolor sit amet.
									</h3>
								</a>
								<span class="label panel-title fecha-noticia pull-right">
									fecha_publicacion
								</span>
							</div>
							<div class="panel-body">
								<img class="main-img-noticia" src="http://placehold.it/150x150">
								<h5 class="h5 text-justify">
									encabezado Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quisquam natus perspiciatis, aperiam, quidem nobis, maiores repudiandae numquam accusamus soluta ipsa officia delectus ratione reiciendis eveniet quam totam odit, tenetur voluptatibus perferendis ut at. Excepturi incidunt repudiandae, quisquam alias! Sed, cum iure soluta necessitatibus aspernatur amet unde, veritatis harum quisquam tenetur.
								</h5>
							</div>
						</div>
					</div>
*/

	}

	function crearNoticias(noticias){
		$.each(noticias, function(i, noticia){
			var mainN = $("<div></div>").addClass('main-noticia');

			var panel = '<div class="main-noticia"><div class="panel panel-default"><div class="panel-heading"><a href="noticias.html?id='+noticia.id+'"><h3 class="panel-title title-noticia">'+noticia.titulo+'</h3></a><span class="label panel-title fecha-noticia pull-right">'+noticia.fecha_publicacion+'</span></div><div class="panel-body"><img class="main-img-noticia" src="http://placehold.it/150x150"><h5 class="h5 text-justify">'+noticia.encabezado+'</h5></div></div></div>';

			mainN.html(panel);

			$("#main-noticias").append(mainN);

		})
	}

	function cargarCursos(){
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

	function llenarCursos(cursos){
		$("#main-eventos").empty()

		$.each(cursos, function(i, curso){
			var curhtml = '<a href="cursos.html?id='+curso.id+'">'+
								'<div class="main-evento">'+
									'<span class="main-tit-evento">'+curso.nombre+'</span>'+
									'<br><br>'+
									'<p class="text-justify">'+curso.descripcion+'</p>'+
									'<span><strong>Cupos: </strong>'+curso.maximo_personas+'</span>'+
								'</div>'+
							'</a>'

			$("#main-eventos").append(curhtml);
		})

		/*
		<a href="cursos.html">
			<div class="main-evento">
				<span class="main-tit-evento">nombre Lorem ipsum.</span>
				<br><br>
				<p class="text-justify">descripcion Lorem ipsum dolor sit amet, consectetur adipisicing elit. Illum tempore officiis non debitis commodi aliquam tenetur, cumque obcaecati nisi earum!</p>
				<span><strong>Cupos:</strong> numero_cupos</span>
			</div>
		</a>
		<hr>
		*/
	}

});