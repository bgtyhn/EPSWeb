$(function(){

	$("#login-form").submit(function(event) {
		event.preventDefault();
		console.log("submit login");
	});

	$("#solicitud-form").submit(function(event) {
		event.preventDefault();
		console.log("submit solicitud");
	});

	$("#a-solicitar-registro").click(function(event) {
		$("#modal-solicitud-registro").modal('show');
	});

	$("#solicitud-cancelar").click(function(event) {
		$("#solicitud-form").trigger("reset");
		$("#modal-solicitud-registro").modal('hide');
	});

});