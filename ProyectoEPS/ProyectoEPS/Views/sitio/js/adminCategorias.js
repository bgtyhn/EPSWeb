$(function(){

	if(sessionStorage.getItem('rol')!='administrador'){
		window.location.href = '../../';
	}

	$("#sesion-nombre").html(sessionStorage.getItem('id'));
	
	cargarCuotas();

	$("#form-categorias").on("submit", function(event){
		event.preventDefault();
		
		guardarPreciosCuotas();

	})

	function guardarPreciosCuotas(){


		for(i = 0; i < 5; i++){
			var message = { 
				accion : 'cambiarCuotaCategoria',
				mensaje : '',
				valores : {
					"idCategoria" : i,
					"cuotaNueva" : $("#cuota-"+i).val()
					}
			}

			$.ajax({
				async:false,
		        type: 'POST',
		        url: 'http://localhost:6570/api/Medicamentos/accionMedicamentos',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	if(data.exito == 1)
		        		toastr.success('Cuotas nuevas guardadas con éxito', 'Guardado completo')
		        	else
		        		toastr.warning(data.mensajeError)
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });
		}

		

	}

	function cargarCuotas(){


		for(i = 0; i < 5; i++){
			var message = { 
				accion : 'detalleCategoria',
				mensaje : '',
				valores : {
					"idCategoria" : i
					}
			}

			$.ajax({
				async:false,
		        type: 'POST',
		        url: 'http://localhost:6570/api/Medicamentos/accionMedicamentos',
		        data: JSON.stringify(message),
		        contentType: 'application/json; charset=utf-8',
		        dataType: 'json',
		        success: function (data) {
		        	console.log(data);
		        	$("#cuota-"+i).val(data.datos.cuota);
		        },
		        error: function (info) {
		            alert("error");
		        }
		    });
		}
	}

});

function cerrarSesion(){
	sessionStorage.removeItem('id')
	sessionStorage.removeItem('rol')
	window.location.href = '../../';
}