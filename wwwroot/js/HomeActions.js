// Função para REMOVER um produto do carrinho
$(document).ready(function () {
    $('.remove-button').click(function () {
        var itemId = $(this).data('id');
        $.ajax({
            url: '/Home/RemoveToCart',
            type: 'POST',
            data: { id: itemId },
            success: function (result) {
                if (result.removed) {
                    $('#cartItem-' + itemId).next('tr').remove(); // Remover o próximo <tr> (collapse)
                    $('#cartItem-' + itemId).remove(); // Remover o próprio <tr>
                    $('#totalPrice').text(result.totalPrice);
                }
            }
        });
    });
});

// Funçoes para EDITAR um produto do carrinho
$('.edit-button').click(function () {
    var productId = $(this).data('id');
    var product = GetProductInfo(productId); // Função para obter as informações do produto do servidor

    // Preencher os campos da modal com as informações do produto
    $('#productId').val(productId);
    $('#selectedIngredients').val(product.Ingredients);
    $('#selectedAdditionalProducts').val(product.AdditionalProducts);
    $('#quantity').val(product.Quantity);

    // Abrir a modal após preencher os campos
    $('#productModal').modal('show');
});


$('#saveButton').click(function () {
    var formData = $('#productModal form').serialize(); // Serializar os dados do formulário

    // Enviar as alterações para o servidor usando AJAX
    $.ajax({
        url: '/Home/Edit',
        type: 'POST',
        data: formData,
        success: function (response) {
            var editedProduct = response.edited;

            // Atualizar os dados do produto no carrinho na página
            var cartItemRow = $('#cartItem-' + editedProduct.Id);
            cartItemRow.find('td[data-bs-toggle]').text('$' + editedProduct.Price);

            $('#productModal').modal('hide');
        },
        error: function (error) {
            console.error('Error editing product:', error);
        }
    });
});


function openProductModal(cartItemId, productId) {
    // Fazer uma solicitação AJAX para obter as informações do produto original
    $.ajax({
        url: "/Home/GetProductInfo",
        type: "POST",
        data: { Id: productId },
        success: function (response) {
            if (response.success) {
                var productInfo = response.product;
                var selectedIngredients = productInfo.SelectedIngredients;
                var selectedAdditionalProducts = productInfo.SelectedAdditionalProductsId;

                // Preencher os campos da modal com as informações do produto original
                $("#productId").val(productInfo.ProductId);
                $("#selectedIngredients").val(selectedIngredients);
                $("#selectedAdditionalProducts").val(selectedAdditionalProducts);

                // Preencher os campos com as informações do produto no carrinho
                var cartItem = $("#cartItem-" + cartItemId);
                var quantity = cartItem.find("td:eq(3)").text(); // Obtém a quantidade do produto no carrinho
                $("#quantity").val(quantity);
            }
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}
