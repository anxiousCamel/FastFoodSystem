$(document).ready(function () {
    var decreaseButtons = $('.decrease-quantity');
    var increaseButtons = $('.increase-quantity');
    var totalPrices = $('[data-price-element]');
    var quantityInputs = $('.quantity');

    decreaseButtons.click(function () {
        var quantityInput = $(this).closest('.modal').find('.quantity');
        var currentQuantity = parseInt(quantityInput.val());
        if (currentQuantity > 1) {
            quantityInput.val(--currentQuantity);
            updateTotalPrice();
        }
    });

    increaseButtons.click(function () {
        var quantityInput = $(this).closest('.modal').find('.quantity');
        var currentQuantity = parseInt(quantityInput.val());
        quantityInput.val(++currentQuantity);
        updateTotalPrice();
    });

    function updateTotalPrice() {
        quantityInputs.each(function (index) {
            var modal = $(this).closest('.modal');
            var basePriceElement = modal.find('.total-price[data-price-element]');
            var basePrice = parseFloat(basePriceElement.attr('data-price'));
            var quantity = parseInt($(this).val());
            var additionalProducts = modal.find('.additional-product-checkbox:checked');
            var additionalPrice = 0;

            additionalProducts.each(function () {
                additionalPrice += parseFloat($(this).attr('data-price'));
            });

            var newTotal = (basePrice + additionalPrice) * quantity;
            totalPrices.eq(index).text(newTotal.toFixed(2));
        });
    }

    var additionalProductCheckboxes = $('.additional-product-checkbox');
    additionalProductCheckboxes.change(updateTotalPrice);

    $('.edit-button').click(function () {
        var productId = $(this).data('id');
        var product = GetProductInfo(productId);
        $('#productId').val(productId);
        $('#selectedIngredients').val(product.Ingredients);
        $('#selectedAdditionalProducts').val(product.AdditionalProducts);
        $('#quantity').val(product.Quantity);
        $('#productModal').modal('show');
    });

    setTimeout(function () {
        updateTotalPrice();
    }, 100);

    $('#saveButton').click(function () {
        var formData = $('#productModal form').serialize();
        $.ajax({
            url: '/Home/Edit',
            type: 'POST',
            data: formData,
            success: function (response) {
                var editedProduct = response.edited;
                var cartItemRow = $('#cartItem-' + editedProduct.Id);
                cartItemRow.find('td[data-bs-toggle]').text('$' + editedProduct.Price);
                $('#productModal').modal('hide');
            },
            error: function (error) {
                console.error('Error editing product:', error);
            }
        });
    });
});

$('.remove-button').click(function () {
    var itemId = $(this).data('id');
    $.ajax({
        url: '/Home/RemoveToCart',
        type: 'POST',
        data: { id: itemId },
        success: function (result) {
            if (result.removed) {
                $('#cartItem-' + itemId).next('tr').remove();
                $('#cartItem-' + itemId).remove();
                $('#payment-' + itemId).next('tr').remove();
                $('#payment-' + itemId).remove();

                // Obter o novo preço total
                $.ajax({
                    url: '/Home/GetTotalPrice',
                    type: 'POST',
                    success: function (response) {
                        // Atualizar o preço total
                        $('.totalCartPrice').text(response.totalPrice);
                    }
                });
            }
        }
    });
});


// Select Pay Method
$(document).ready(function () {
    $('.radio-group .radio').click(function () {
        $('.selected .fa').removeClass('fa-check');
        $('.radio').removeClass('selected');
        $(this).addClass('selected');
    });
});


$(document).ready(function () {
    $('.payment-option').click(function () {
        var paymentMethod = $(this).data('payment-method');

        $('.payment-column').hide(); // Oculta todas as colunas de pagamento

        if (paymentMethod === 'pix') {
            $('#pixColumn').show(); processPixPayment();
        } else if (paymentMethod === 'card') {
            $('#cardColumn').show(); processCardPayment();
        } else if (paymentMethod === 'money') {
            $('#moneyColumn').show();
        }
    });
});







///FakeSimulation

// Função para exibir o botão "Finish"
function showFinishButton() {
    var modalFooter = document.getElementById("modalFooter");
    modalFooter.style.display = "block";
}
// Função para exibir a mensagem de status do Pix atualizada na página
function showPixStatusMessage(message, isLastMessage) {
    var pixStatusMessage = document.getElementById("pixStatusMessage");
    pixStatusMessage.textContent = message;
    if (isLastMessage) {
        pixStatusMessage.classList.add("fw-bold", "text-success");
        showFinishButton(); // Exibir o botão "Finish"
    }
}

// Função para exibir a mensagem de status do cartão atualizada na página
function showCardStatusMessage(message, isLastMessage) {
    var cardStatusMessages = document.getElementById("cardStatusMessages");
    var newMessage = document.createElement("p");
    newMessage.textContent = message;
    if (isLastMessage) {
        newMessage.classList.add("fw-bold", "text-success");
        showFinishButton(); // Exibir o botão "Finish"
    }
    cardStatusMessages.innerHTML = ""; // Limpar todas as mensagens existentes
    cardStatusMessages.appendChild(newMessage);
}

// Função para simular o processo de pagamento via Pix
function processPixPayment() {
    // Exibir a mensagem "Aguardando pagamento..."
    showPixStatusMessage("Aguardando pagamento...");

    // Exibir o spinner
    document.getElementById("pixColumn").classList.add("show-spinner");

    // Simular um atraso antes de confirmar o pagamento
    setTimeout(() => {
        // Ocultar o spinner
        document.getElementById("pixColumn").classList.remove("show-spinner");

        // Exibir a mensagem "Pagamento confirmado!"
        showPixStatusMessage("Pagamento confirmado!", true);
    }, 2500); // Tempo de espera em milissegundos antes de confirmar o pagamento
}

// Função para simular o processo de transação do cartão
function processCardPayment() {
    // Exibir a mensagem "Aproxime o cartão na maquininha"
    showCardStatusMessage("Aproxime ou insira o cartão na maquininha...");

    // Exibir o spinner
    document.getElementById("cardColumn").classList.add("show-spinner");

    // Simular um atraso antes de solicitar a senha
    setTimeout(() => {
        // Ocultar o spinner
        document.getElementById("cardColumn").classList.remove("show-spinner");

        // Exibir a mensagem "Insira a senha"
        showCardStatusMessage("Insira a senha...");

        // Exibir o spinner novamente
        document.getElementById("cardColumn").classList.add("show-spinner");

        // Simular um atraso antes de processar a transação
        setTimeout(() => {
            // Ocultar o spinner
            document.getElementById("cardColumn").classList.remove("show-spinner");

            // Exibir a mensagem "Processando..."
            showCardStatusMessage("Processando...");

            // Exibir o spinner novamente
            document.getElementById("cardColumn").classList.add("show-spinner");

            // Simular um atraso antes de mostrar a transação aprovada
            setTimeout(() => {
                // Ocultar o spinner
                document.getElementById("cardColumn").classList.remove("show-spinner");

                // Exibir a mensagem "Transação aprovada, retire o cartão..."
                showCardStatusMessage("Transação aprovada, retire o cartão...", true);
            }, 1500); // Tempo de espera em milissegundos antes de mostrar a transação aprovada
        }, 3500); // Tempo de espera em milissegundos antes de processar a transação
    }, 2000); // Tempo de espera em milissegundos antes de solicitar a senha
}



document.addEventListener('DOMContentLoaded', function () {
    const moneyButtons = document.querySelectorAll('.money-button');
    const amountPaidElement = document.getElementById('amountPaid');
    const amountRemainingElement = document.getElementById('amountRemaining');
    const changeDueElement = document.getElementById('changeDue');
    let amountPaid = 0;
    let amountRemaining = parseFloat(document.getElementById('totalCartPrice').getAttribute('data-total-cart-price'));
    let changeDue = 0;

    moneyButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            const value = parseFloat(button.getAttribute('data-value'));
            amountPaid += value;
            amountRemaining = Math.max(amountRemaining - value, 0);
            changeDue = Math.max(amountPaid - amountRemaining, 0);

            amountPaidElement.textContent = 'Valor pago: R$ ' + amountPaid.toFixed(2);
            amountRemainingElement.textContent = 'Valor restante: R$ ' + amountRemaining.toFixed(2);
            changeDueElement.textContent = 'Troco: R$ ' + changeDue.toFixed(2);

            // Atualizar estilos e cores dos valores
            amountPaidElement.classList.toggle('fw-bold', amountPaid < amountRemaining);
            amountPaidElement.classList.toggle('text-warning', amountPaid < amountRemaining);
            amountPaidElement.classList.toggle('text-primary', amountPaid >= amountRemaining);

            amountRemainingElement.classList.toggle('fw-bold', amountRemaining > 0);
            amountRemainingElement.classList.toggle('text-danger', amountRemaining > 0);
            amountRemainingElement.classList.toggle('text-secondary', amountRemaining <= 0);

            changeDueElement.classList.toggle('text-secondary', changeDue === 0);

            if (changeDue > 0) {
                changeDueElement.classList.toggle('fw-bold');
                changeDueElement.classList.toggle('text-success');
                showFinishButton();
            }
        });
    });
});