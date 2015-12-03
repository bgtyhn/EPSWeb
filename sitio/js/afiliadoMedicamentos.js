$(function(){
	
	//stub
	var idUsuario = 2;

	var categoriaUsuario = obtenerCategoriaUsuario(idUsuario);

	$("#categoria-med-sel").val(categoriaUsuario);

	cargarListaMedicamentos(categoriaUsuario);

	$("#categoria-med-sel").change(function(event) {
		categoriaSel = $("#categoria-med-sel option:selected").val();
		if(categoriaSel != ""){
			cargarListaMedicamentos(categoriaSel);
		}
	});

	function obtenerCategoriaUsuario(id){
		//stub
		return "B";
	}

	function cargarListaMedicamentos(categoria){

		var medicamentos = []

		//stub
		for(i = 0; i < 5; i++){

			medicamentos.push({
				nombre : "Nombre "+i+" - "+categoria,
				descripcion : "Descripcion "+i+" Lorem ipsum dolor sit amet, consectetur adipisicing elit. Natus quo delectus officiis repellendus perspiciatis hic ad esse temporibus odio provident vero tempore, consectetur, voluptates quos, voluptas cupiditate, ab numquam repellat soluta laborum similique ex! Nulla quis tempora, vitae harum illum, quidem eum sunt, ut architecto modi expedita nesciunt. Saepe, quaerat! Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Provident, est." 
			})

		}

		//Llenar lista

		var lista = $("#medicamentos-lista");

		lista.empty();

		$.each(medicamentos, function(i, m){

			var panelMed = $("<div></div>").addClass("panel panel-default medicamento-panel");

			var panelBody = $("<div></div>").addClass("panel-body");

			panelBody.append([
				$("<img src='http://placehold.it/150x150'>").addClass('afiliado-img-medicamento'),
				$("<h4>"+m.nombre+"</h4>"),
				$("<span>"+m.descripcion+"</span>")
			]);

			lista.append(panelMed.append(panelBody));

		});

	}

})