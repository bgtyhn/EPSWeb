$(function(){
	
	var idUsuario = "2";

	llenarTablaLlamadosAtencion();


	function llenarTablaLlamadosAtencion(){

		console.log(idUsuario);

		//Stub

		var llamados = []

		for(i = 0; i < 6; i++){
			llamados.push({
				fecha : "fecha "+i,
				contenido : "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Deserunt, eum, ipsum! Qui vitae fuga consequuntur voluptates iure non accusantium ut, a iusto, eum ullam laudantium repudiandae deleniti deserunt quidem explicabo, accusamus hic. Unde id facilis praesentium quisquam perferendis earum totam officiis enim iusto, iure iste expedita aperiam minus autem omnis."
			})
		}

		//llenar tbody

		$.each(llamados, function(i, llamado){

			var tr = $("<tr></tr>").append([
				$("<td>"+llamado.fecha+"</td>"),
				$("<td>"+llamado.contenido+"</td>"),
			])

			$("#llamados-tbody").append(tr);

		});

	}

})