// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#cupom-desconto").hide();
    $("#valor-final").hide();
});

function removeIdAndUser() {
    $("#endereco-userId").val("");
    $("#endereco-Id").val("");
}

function removerEndereco() {
    $("#endereco-detalhe").val("");
    $("#endereco-bairro").val("");
    $("#endereco-cep").val("");
    $("#endereco-cidade").val("");
    $("#endereco-complemento").val("");
    $("#endereco-estado").val("");
    $("#endereco-numero").val("");
    $("#endereco-rua").val("");
    $("#endereco-userId").val("");
}

function pedidoEntrega() {
    $("#endereco-bairro").attr('data-val', true).prop('disabled', false);
    $("#endereco-cep").attr('data-val', true).prop('disabled', false);
    $("#endereco-cidade").attr('data-val', true).prop('disabled', false);
    $("#endereco-complemento").attr('data-val', true).prop('disabled', false);
    $("#endereco-estado").attr('data-val', true).prop('disabled', false);
    $("#endereco-numero").attr('data-val', true).prop('disabled', false);
    $("#endereco-rua").attr('data-val', true).prop('disabled', false);
    $("#endereco-userId").attr('data-val', true).prop('disabled', false);
    $("#endereco-detalhe").attr('data-val', true);
}

function pedidoRetirada() {
    $("#endereco-detalhe").attr('data-val', false).prop('disabled', true);
    $("#endereco-bairro").attr('data-val', false).prop('disabled', true);
    $("#endereco-cep").attr('data-val', false).prop('disabled', true);
    $("#endereco-cidade").attr('data-val', false).prop('disabled', true);
    $("#endereco-complemento").attr('data-val', false).prop('disabled', true);
    $("#endereco-estado").attr('data-val', false).prop('disabled', true);
    $("#endereco-numero").attr('data-val', false).prop('disabled', true);
    $("#endereco-rua").attr('data-val', false).prop('disabled', true);
    $("#endereco-userId").attr('data-val', false).prop('disabled', true);
}


function getEndereco(id) {
    $.ajax({
        dataType: "json",
        type: "GET",
        url: "/Account/GetEnderecoUserById",
        data: { enderecoId: id },
        success: function (dados) {
            console.log(dados);
            if (dados != null) {
                $("#endereco-detalhe").val(dados.detalhe);
                $("#endereco-bairro").val(dados.bairro);
                $("#endereco-cep").val(dados.cep);
                $("#endereco-cidade").val(dados.cidade);
                $("#endereco-complemento").val(dados.complemento);
                $("#endereco-estado").val(dados.estado);
                $("#endereco-numero").val(dados.numero);
                $("#endereco-rua").val(dados.rua);
                $("#endereco-userId").val(dados.userId);
                $("#endereco-Id").val(dados.enderecoId);
            } else {
                alert("Error");
                throw new Erro("endereço não encontrado");
            }
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
            }, 500);

        },
        error(err) {
            console.error(err);
        }
    });
}

function validarCupom() {
    var cdCupom = document.getElementById('codigo-cupom').value;
    console.log(cdCupom);

    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Pedido/ValidarCupom",
        data: { codigoCupom: cdCupom },
        success: function (dados) {
            var desconto = dados.valor;
            var total = $("#total-final #valor").html();
            total = parseFloat(total.replace('R$', ''));

            valorFinal = total - desconto;
            valorFinal = valorFinal > 0 ? valorFinal : 0;

            desconto = parseFloat(desconto).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
            valorFinal = parseFloat(valorFinal).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

            $("#desconto #valor").html('- '+desconto);
            $("#total-final #valor").html(valorFinal);
        },
        error(err) {
            console.error(err);
        }
    });
}

