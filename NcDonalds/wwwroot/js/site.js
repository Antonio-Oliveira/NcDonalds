// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getEnderecos() {
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Account/GetEnderecosUser",
        success: function (dados) {
            alert("aqqqq")
        },
        error(err) {
            //alert(err)
        }
    });
}

function incluirLanche(id) {
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/CarrinhoCompra/AdicionarItem",
        data: { lancheId: id },
        success: function (dados) {
            $("#total-itens-carrinho").text(dados).css("display", "inline-block");
            $("#icon-carrinho path").css("fill", "yellow");
        },
        error(err) {
            //alert(err)
        }
    });
}

function validarCupom() {
    var cdCupom = document.getElementById('codigo-cupom').value;
    alert(cdCupom);
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Pedido/ValidarCupom",
        data: { codigoCupom: cdCupom },
        success: function (dados) {
            alert("aq")
            if (dados == null) {
                alert("Conteudo nulo");
            }
            var result = JSON.parse(dados)
            alert(result.CodigoCupom);
        },
        error(err) {
            //alert(err)
        }
    });
}

