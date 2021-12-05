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
    var id_tarjeta = parseInt($("#id_creditCard").val());
    var MontoPagar = parseFloat($("#monto").val());


    var payCredit = {
        "id_TarjetaCredito": id_tarjeta,
        "id_CuentaOrigen": Id_CuentaOrigen,
        "pagado": MontoPagar,
    };

    data = JSON.stringify(payCredit);

    console.log(data);

    Solicitudes("/tarjetacredito/paycredit", "POST", data, async (response) => {
        if (response.status == 1) {
            await CustomSucess();
            var time = setTimeout(() => {
                location.href = "/dashboard";
            }, 1500);
        } else {
            await CustomError("Hubo un error",response.message);
        }
        console.log(response);
    });


}
