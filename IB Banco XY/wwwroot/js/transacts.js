$(document).ready(function () {
  $(".input-money").change((evt) => {
    evt.preventDefault();
    evt.stopPropagation();

    let idNewTarget = evt.target.getAttribute("unmaskRef");

    $(idNewTarget).val($("#" + evt.target.id).cleanVal());
  });

  $(".input-money").mask("#####$", { reverse: true });
  InicializeEvents();

  $("#formTransact").validate();
});

function InicializeEvents() {
  $("#buscadorCuenta").click(() => {
    let id_destino = "idDestinoPartialView";

    var no_cuenta = $("#no_cuenta_destino").val();

    if (no_cuenta != "") {
      $.get(
        "/Partial/AccountDetailsCard",
        { id_campo: id_destino, no_cuenta: no_cuenta },
        function (data) {
          $("#cuentaDestino").empty();
          $("#cuentaDestino").append(data);

          let CuentaDestino = $("#idDestinoPartialView").val();

          $("#cuentaDestinoInput").val(CuentaDestino);
        }
      );
    } else {
      CustomError("Error", "No puede dejar vacio el numero de cuenta");
    }
  });
}

async function PostTransaccion() {
  if (parseInt($("#monto").val()) < parseInt($("#balanceOrigen").val())) {
    var Id_CuentaOrigen = parseInt($("#cuentaOrigenInput").val());
    var Id_CuentaDestino = parseInt($("#cuentaDestinoInput").val());
    var MontoActual = parseFloat($("#monto").val());

    if (Id_CuentaDestino == Id_CuentaOrigen) {
      CustomError(
        "Error",
        "No es posible realizar una transferencia a la misma cuenta"
      );
      return;
    }

    var transact = {
      Id_CuentaOrigen,
      Id_CuentaDestino,
      MontoActual,
    };

    data = JSON.stringify(transact);

    Solicitudes("/transact/create", "POST", data, async (response) => {
      if (response == 1) {
        await CustomSucess();
        var time = setTimeout(() => {
          location.href = "/dashboard";
        }, 1500);
      } else {
        await CustomError();
      }
      console.log(response);
    });

    console.log(transact);
  } else {
    CustomError(
      "Error",
      "El monto a transferir no puede ser mayor del disponible"
    );
  }
}
