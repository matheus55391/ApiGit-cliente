// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function BtnPesquisar() {
    var result = document.getElementById("Formulario-Conteudo").value
    console.log("Pesquisou: " +result)
    if (result != "") {
        document.getElementById("Formulario-Pesquisa").submit();
    }
    else {
        bootbox.alert({
            message: "Ops!! - Preencha o campo para poder pesquisar!!",
            backdrop: true
        });       
    }

    
}

