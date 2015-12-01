$(document).ready(function () {

    $('#btn1').click(function () {
        texto = $('#txt1').val();
        alert(texto);
        //var diccionario = { parametro1: 'p1', parametro2: 'p2' };
        var diccionario = {'userCorreo': 'juan.1234', 'userPassword': '1234', 'tipoUsuario': 'afiliado'};
        var message = { accion: 'ejecutar', mensaje :'holi', valores: diccionario};
        $.ajax({
            type: 'POST',
            url: 'http://localhost:6570/api/Message/messagePostController',
            data: JSON.stringify(message),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                console.log(data)
                mostrarDatos(data);
            },
            error: function (info) {
                alert("error");
            }
        });
    });
});

function mostrarDatos(datos) {
    $.each(datos.datos, function () {
        console.log(this.first_name);
    });
}