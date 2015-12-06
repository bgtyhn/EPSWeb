$(function(){
	if(sessionStorage.getItem('rol')!='administrador'){
		window.location.href = '../../';
	}
	var accion = ""

	$("#sesion-nombre").html(sessionStorage.getItem('id'));

	cargarTablaMedicamentos();

	$("#medicamento-categorias").on("change", function(){
		console.log($("#medicamento-categorias").val())
	})

	$("#medicamento-agregar").click(function(event) {
		accion = "agregar"
		$("#med-cats").show();
		$("#medicamento-id").prop("readonly", false);
		$("#medicamento-categorias").prop("required", true);
		$("#titulo-modal-medicamento").html("Registrar nuevo medicamento");
		$("#agregar-medicamento-form").trigger("reset");
		$("#modal-agregar-medicamento").modal("show");
	});

	$("#registro-cancelar").click(function(event){
		$("#modal-agregar-medicamento").modal("hide");
	})

	$("#agregar-medicamento-form").on("submit", function(event){
		event.preventDefault();



		

		if(accion == "agregar"){

			var categorias = "";
			var array = $("#medicamento-categorias").val();

			for(i = 0; i < array.length; i++){
				categorias+=array[i]+","
			}

			categorias = ""+categorias.substring(0,categorias.length-1);

			console.log(categorias);

			var message = { 
				accion : 'agregarMedicamento',
				mensaje : '',
				valores : {
						"idMedicamento" : $("#medicamento-id").val(),
						"nombreMedicamento" : $("#medicamento-nombre").val(),
						"descripcionMedicamento" : $("#medicamento-descripcion").val(),
						"categorias" : categorias
						
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Medicamentos/accionMedicamentos',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);

		        	cargarTablaMedicamentos();
					$("#modal-agregar-medicamento").modal("hide");
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });
		} else if( accion == "editar"){

			var message = { 
				accion : 'editarMedicamento',
				mensaje : '',
				valores : {
						"idMedicamento" : $("#medicamento-id").val(),
						"nombreMedicamento" : $("#medicamento-nombre").val(),
						"descripcionMedicamento" : $("#medicamento-descripcion").val(),
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Medicamentos/accionMedicamentos',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	cargarTablaMedicamentos();
					$("#modal-agregar-medicamento").modal("hide");
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

		}
	})


	function cargarTablaMedicamentos(){

		

		//Traer de bd

		//Stub
		var message = { 
				accion : 'listarMedicamentos',
				mensaje : '',
				valores : {
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Medicamentos/accionMedicamentos',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);

		        	llenarTablaMedicamentos(data.datos);
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });

	}

	function llenarTablaMedicamentos(rows){

		$(".tr-med").remove();

		$.each(rows, function(i, medicamento){

			var tr = $("<tr></tr>").addClass("tr-med");

			tr.append([

					$("<td>"+medicamento.id+"</td>"),
					$("<td>"+medicamento.nombre+"</td>"),
					$("<td>"+medicamento.descripcion+"</td>"),
					$("<td></td>").append([
						$("<button></button>").addClass("btn btn-xs btn-warning").append(
								$("<span><span>").addClass("glyphicon glyphicon-edit")
							).click(function(){
									editarMedicamento(medicamento);
								}),
						$("<span>&nbsp;&nbsp;</span>"),
						$("<button></button>").addClass("btn btn-xs btn-danger").append(
								$("<span><span>").addClass("glyphicon glyphicon-trash")
							).click(function(event){
								eliminarMedicamento(medicamento)
							})
						]
					)
				])

			$("#lista-medicamentos").append(tr)

		})
	}

	function eliminarMedicamento(medicamento){
		console.log("eliminar")
		
		var message = { 
				accion : 'eliminarMedicamento',
				mensaje : '',
				valores : {
					"idMedicamento" : medicamento.id
						}
			}

			// var message = { accion: 'detalleNoticia', mensaje: 'holi', valores: diccionario };

			$.ajax({
		        type: 'POST',
		        url: 'http://localhost:6570/api/Medicamentos/accionMedicamentos',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);

		        	cargarTablaMedicamentos();
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });
	}

	function editarMedicamento(medicamento){

		$("#titulo-modal-medicamento").html("Editar medicamento");
		accion = "editar";

		$("#medicamento-id").prop("readonly", true);

		$("#agregar-medicamento-form").trigger("reset");

		$("#medicamento-id").val(medicamento.id);
		$("#medicamento-nombre").val(medicamento.nombre);
		$("#medicamento-descripcion").val(medicamento.descripcion);

		$("#med-cats").hide();
		$("#medicamento-categorias").prop("required", false);

		$("#modal-agregar-medicamento").modal("show");
	}



})

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}