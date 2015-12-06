$(function(){
	if(sessionStorage.getItem('rol')!='profesional'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));

	var idSesion = sessionStorage.getItem('id');

	$("form").trigger("reset")


	$("#cita-med-agregar").select2();

	var idVar = $.getUrlVar("id");
	console.log(idVar)

	
	var cita = cargarCita(idVar);


	function cargarCita(idVar){

		if(idVar != undefined){

			//Stub
			var message = { 
				accion : 'detalleCita',
				mensaje : '',
				valores : { 
					"idCita" : idVar,
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
	            	llenarDatos(data.datos)
	            },
	            error: function (info) {
	                alert("error");
	            }
	        });

		} else {
			$("body").html("<h1>No se encuentran los datos que pide</h1>")
		}
	}

	function llenarDatos(cita){

		$("#cita-nombre-pac").html(cita.afiliado);
		$("#cita-fecha").html(cita.fecha);
		$("#cita-tipo").html(cita.tipo_atencion);
		$("#cita-estado").html(cita.estado);

		if(cita.estado == "incumplida"){

			$("#modificables-cita").empty();
			$("#botones-estado-cita").empty();

		} else if (cita.estado == "pendiente"){
			cargarDiagnosticos();

			cargarMedicamentos();

			$("#cita-med-form").on("submit", function(event){
				event.preventDefault();
				agregarMedicamento();
			})

			$("#cita-diag-form").on("submit", function(event){
				event.preventDefault();
				agregarDiagnostico();
			})

			$("#btn-atendida").click(function(){
				darCitaAtendida();
			})

			$("#btn-incumplida").click(function(){
				darCitaIncumplida();
			})

		} else if (cita.estado == "atendida"){

			cargarDiagnosticos();

			cargarMedicamentos();

			$(".form-control").prop("disabled", true)

			$(".btn").prop("disabled", true)

			$("#botones-estado-cita").empty();

		} 

	}

	function cargarDiagnosticos(){
		//Conseguir diagnosticos de cita

		var message = { 
			accion : 'diagnosticosCita',
			mensaje : '',
			valores : { 
				"idCita" : idVar,
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
            	llenarDiagnosticos(data.datos)
            },
            error: function (info) {
                alert("error");
            }
        });

	}

	function llenarDiagnosticos(diagnosticos){
		$("#cita-diag-lista").empty();

		$.each(diagnosticos, function(i, diagnostico){
			$("#cita-diag-lista").append(
				$("<li>"+diagnostico.descripcion+"</li>").addClass("list-group-item")
			)
		});
	}

	function cargarMedicamentos(){
		//Conseguir medicamentos de cita

		var message = { 
			accion : 'medicamentosCita',
			mensaje : '',
			valores : { 
				"idCita" : idVar,
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
            	llenarMedicamentos(data.datos)
            },
            error: function (info) {
                alert("error");
            }
        });

        //Conseguir medicamentos para el select

        var categoria = "";

        var message = { 
			accion : 'verDatosAfiliado',
			mensaje : '',
			valores : {
						"idAfiliado" : $("#cita-nombre-pac").html()
					}
		}

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
			async : false,
	        type: 'POST',
	        url: 'http://localhost:6570/api/Afiliados/accionAfiliados',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log("paciente: ")
	        	console.log(data);
	        	categoria = data.datos.categoria
	        	$("#cita-cat-pac").html(categoria)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

		var message = { 
				accion : 'posAfiliado',
				mensaje : '',
				valores : {
					"idCategoriaAfiliado" : categoria
				}
		}

		$.ajax({
			async : false,
	        type: 'POST',
	        url: 'http://localhost:6570/api/Afiliados/accionAfiliados',
	        data: JSON.stringify(message),
	        contentType: 'application/json; charset=utf-8',
	        dataType: 'json',
	        success: function (data) {
	        	console.log(data);
	        	llenarSelect(data.datos)
	        },
	        error: function (info) {
	            alert("error");
	        }
	    });

	}

	function llenarSelect(medicamentos){
		$("#cita-med-agregar").empty();
		$.each(medicamentos, function(i, m){
			var option = $("<option value='"+m.id+"'>"+m.nombre+"</option>")
			$("#cita-med-agregar").append(option)
		})
		$("#cita-med-agregar").select2();
	}

	function llenarMedicamentos(medicamentos){
		$("#cita-med-lista").empty();
		$.each(medicamentos, function(i, medicamento){
			$("#cita-med-lista").append(
				$("<li>"+medicamento.nombre+"</li>").addClass("list-group-item")
			)
		});
	}

	function agregarDiagnostico(){
		var diag = $("#cita-diag").val()
		
		var message = { 
			accion : 'agregarDiagnosticoCita',
			mensaje : '',
			valores : { 
				"idCita" : idVar,
				"descripcionDiagnostico" : diag
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
            	if(data.exito == 1){
            		$("#cita-diag-form").trigger("reset");
            		toastr.success("Diagnostico agregado con éxito")
	            	cargarDiagnosticos();
            	} else {
            		toastr.warning("No se pudo agregar el diagnóstico", "Error de servidor")
            	}
            },
            error: function (info) {
                alert("error");
            }
        });
	}

	function agregarMedicamento(){
		var idMed = $("#cita-med-agregar option:selected").val()
		console.log("agregarMedicamento "+idMed+" cita "+idVar);

		var message = { 
			accion : 'agregarMedicamentoCita',
			mensaje : '',
			valores : { 
				"idCita" : idVar,
				"idMedicamento" : idMed
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
            	if(data.exito == 1){
            		$("#cita-diag-form").trigger("reset");
            		toastr.success("Medicamento agregado con éxito")
	            	cargarMedicamentos();
            	} else {
            		toastr.warning("No se pudo agregar el diagnóstico", "Error de servidor")
            	}
            },
            error: function (info) {
                alert("error");
            }
        });

	}

	function darCitaAtendida(){
		
		var message = { 
			accion : 'cambiarEstadoCita',
			mensaje : '',
			valores : { 
				"idCita" : idVar,
				"estadoCita" : "atendida"
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
            	if(data.exito == 1){
            		toastr.success("Cita atendida con éxito")
	            	cargarCita(idVar);
            	} else {
            		toastr.warning("No se pudo terminar la acción", "Error de servidor")
            	}
            },
            error: function (info) {
                alert("error");
            }
        });

	}


	function darCitaIncumplida(){
		
		var message = { 
			accion : 'cambiarEstadoCita',
			mensaje : '',
			valores : { 
				"idCita" : idVar,
				"estadoCita" : "incumplida"
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
            	if(data.exito == 1){
            		toastr.success("Cita dada por incumplida con éxito")
	            	cargarCita(idVar);
            	} else {
            		toastr.warning("No se pudo terminar la acción", "Error de servidor")
            	}
            },
            error: function (info) {
                alert("error");
            }
        });

        var message = { 
			accion : 'generarMulta',
			mensaje : '',
			valores : { 
				"idCita" : idVar,
				"idAfiliado" : $("#cita-nombre-pac").html()
			}
		}
		console.log(message)

		// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

		$.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Citas/accionCitas',
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

	}
})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}