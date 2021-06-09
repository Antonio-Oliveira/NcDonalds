// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#entrega").click(function () {
        $("#form-entrega").css("display", "block")
    });
});


function getEnderecos() {
    $.ajax({
        dataType: "json",
        type: "GET",
        url: "/Account/GetEnderecosUser",
        success: function (dados) {
            alert("aq")
            //console.log(dados);
            alert(dados[0].detalhe)
        },
        error(err) {
            console.error(err);
        }
    });
}

function getEnderecos() {
    $.ajax({
        dataType: "json",
        type: "GET",
        url: "/Account/GetEnderecosUser",
        success: function (dados) {
            alert("aq")
            //console.log(dados);
            alert(dados[0].detalhe)
        },
        error(err) {
            console.error(err);
        }
    });
}

function incluirLanche(id) {
    $(".btn-carrinho").prop('disabled', true);
    var spinnerId = "#spinner" + id;
    $(spinnerId).css("display", "block");

    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/CarrinhoCompra/AdicionarItem",
        data: { lancheId: id },
        success: function (dados) {
            
            setTimeout(function () {
                $("#total-itens-carrinho").text(dados).css("display", "inline-block");
                $("#icon-carrinho path").css("fill", "yellow");
                $(spinnerId).css("display", "none");
                $(".btn-carrinho").prop('disabled', false);
            }, 1500);

        },
        error(err) {
            console.error(err);
        }
    });
}

function validarCupom() {
    var cdCupom = document.getElementById('codigo-cupom').value;
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Pedido/ValidarCupom",
        data: { codigoCupom: cdCupom },
        success: function (dados) {
            alert("aq");
            alert(dados);
            this.cupom = dados;
            if (dados == null) {
                alert("Conteudo nulo");
            }
        },
        error(err) {
            console.error(err);
        }
    });
}

