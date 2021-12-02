// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function CustomConfirm(title, content = "", icon = 'question', textbuttons) {

    return new Promise(resolve => {


        Swal.fire({
            position: 'center',
            icon: icon,
            title: title,
            text: content,
            showConfirmButton: true,
            showCancelButton: true,
            confirmButtonText: textbuttons[0],
            cancelButtonText: textbuttons[1],
            cancelButtonColor: "#bf0618",
            confirmButtonColor: "#162c56"


        }).then((result) => {

            resolve(result.isConfirmed)

        });




    })


}

function CustomError(title = "hubo un error", content = "Trate mas tarde") {

    Swal.fire({
        icon: 'error',
        title: title,
        text: content,
        confirmButtonColor: "#162c56",
        footer: `<a href=""> ${Date(Date.now())}</a>`
    })

}

function CustomSucess(title = "Realizado Correctamente", content = "") {


    Swal.fire({
        position: 'center',
        icon: 'success',
        title: title,
        text: content,
        showConfirmButton: false,
        timer: 1500
    })


}



function Solicitudes(url, method, data, callback) {
    var settings = {
        url: url,
        method: method,
        timeout: 0,
        headers: {
            "Content-Type": "application/json",
        },
        data: data,
    };

    $.ajax(settings).done(function (response) {
        callback(response);
    });
}
