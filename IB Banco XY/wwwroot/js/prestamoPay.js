$(document).ready(function () {
    $(".input-money").change((evt) => {
        evt.preventDefault();
        evt.stopPropagation();

        let idNewTarget = evt.target.getAttribute("unmaskRef");

        $(idNewTarget).val($("#" + evt.target.id).cleanVal());
    });

    $(".input-money").mask("#####$", { reverse: true });

});



async function PostTransaccion() {

    var Id_CuentaOrigen = parseInt($("#cuenta_origen").val());
    var Id_prestamo = parseInt($("#id_prestamo").val());
    var MontoPagar = parseFloat($("#monto").val());


    var payPrestamo = {
        "id_CuentaOrigen": Id_CuentaOrigen,
        "id_Prestamo": Id_prestamo,
        "monto_pagado": MontoPagar,
    };

    data = JSON.stringify(payPrestamo);

    console.log(data);

    Solicitudes("/prestamo/payprestamo", "POST", data, async (response) => {
        if (response.status == 1) {
            await CustomSucess();
            var time = setTimeout(() => {
                location.href = "/dashboard";
            }, 1500);
        } else {
            await CustomError("Hubo un error", response.message);
        }
        console.log(response);
    });


}
