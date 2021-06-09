// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


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
    /*setTimeout(function () {
        $("#collapse-tipo-entrega").removeClass("show collapse").addClass("collapsing");
    }, 2000)
    setTimeout(function () {
        $("#collapse-tipo-entrega").removeClass("collapsing").addClass("collapse");
    }, 2000)*/
    
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
            }, 1500);

        },
        error(err) {
            console.error(err);
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
            console.log(dados);
            alert("aq");
        },
        error(err) {
            console.error(err);
        }
    });
}

