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







document.getElementById("Finish").addEventListener("click", function () {
    const dateTimeInput = document.getElementById("DateTime");
    const now = new Date();
    dateTimeInput.value = now.toISOString(); // Define o valor do input com o horário atual no formato ISO (UTC)
});
















// Select Pay Method
$(document).ready(function () {
    $('.radio-group .radio').click(function () {
        $('.selected .fa').removeClass('fa-check');
        $('.radio').removeClass('selected');
        $(this).addClass('selected');
    });

    var isPaymentInProgress = false;
    var currentTimer = null;

    $('.payment-option').click(function () {
        var paymentMethod = $(this).data('payment-method');

        // Encontra todos os elementos com a classe "condition-payment" e define o valor
        $('.condition-payment').val(paymentMethod);

        $('.payment-column').hide();
        $('#payment-info').hide();
        $('#backButton').show();

        if (!isPaymentInProgress) {
            if (currentTimer) {
                clearTimeout(currentTimer); // Clear the ongoing payment simulation
            }

            if (paymentMethod === 'pix') {
                $('#pixColumn').show();
                processPixPayment();
            } else if (paymentMethod === 'debit card' || paymentMethod === 'credit card') {
                $('#cardColumn').show();
                processCardPayment();
            } else if (paymentMethod === 'money') {
                $('#moneyColumn').show();
            }
        }
    });


    $('#backButton').click(function () {
        isPaymentInProgress = false;
        $('.payment-column').hide();
        $('#backButton').hide();
        $('#payment-info').show();
        $('.radio').removeClass('selected');

        // Clear the ongoing payment simulation and hide the Finish button
        if (currentTimer) {
            clearTimeout(currentTimer);
        }
        $('#modalFooter').hide();

        // Reset the status messages
        $('#pixStatusMessage, #cardStatusMessages').removeClass('fw-bold text-success').text('');
    });

    function showFinishButton() {
        $('#Finish').show();
        $('#backButton').hide();
    }

    function showStatusMessage(element, message, isLastMessage) {
        isPaymentInProgress = true;
        element.text(message);
        if (isLastMessage) {
            element.addClass('fw-bold text-success');
            showFinishButton();
        }
    }

    function processPixPayment() {
        var pixStatusMessage = $('#pixStatusMessage');
        showStatusMessage(pixStatusMessage, 'Aguardando pagamento...');
        $('#pixColumn').addClass('show-spinner');

        currentTimer = setTimeout(() => {
            $('#pixColumn').removeClass('show-spinner');
            showStatusMessage(pixStatusMessage, 'Pagamento confirmado!', true);
            $('#backButton').hide();
        }, 2500);
    }

    function processCardPayment() {
        var cardStatusMessages = $('#cardStatusMessages');
        showStatusMessage(cardStatusMessages, 'Aproxime ou insira o cartão na maquininha...');

        $('#cardColumn').addClass('show-spinner');
        currentTimer = setTimeout(() => {
            $('#cardColumn').removeClass('show-spinner');
            showStatusMessage(cardStatusMessages, 'Insira a senha...');

            $('#cardColumn').addClass('show-spinner');
            currentTimer = setTimeout(() => {
                $('#cardColumn').removeClass('show-spinner');
                showStatusMessage(cardStatusMessages, 'Processando...');

                $('#cardColumn').addClass('show-spinner');
                currentTimer = setTimeout(() => {
                    $('#cardColumn').removeClass('show-spinner');
                    showStatusMessage(cardStatusMessages, 'Transação aprovada, retire o cartão...', true);
                    $('#backButton').hide();
                }, 1500);
            }, 3500);
        }, 2000);
    }

    const moneyButtons = document.querySelectorAll('.money-button');
    const amountPaidElement = document.getElementById('amountPaid');
    const amountRemainingElement = document.getElementById('amountRemaining');
    const changeDueElement = document.getElementById('changeDue');
    let amountPaid = 0;
    let amountRemaining = totalCartPrice;
    let changeDue = 0;

    moneyButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            const value = parseFloat(button.getAttribute('data-value'));
            amountPaid += value;
            amountRemaining = Math.max(amountRemaining - value, 0);
            changeDue = Math.max(amountPaid - totalCartPrice, 0);

            amountPaidElement.textContent = 'Valor pago: R$ ' + amountPaid.toFixed(2);
            amountRemainingElement.textContent = 'Valor restante: R$ ' + amountRemaining.toFixed(2);
            changeDueElement.textContent = 'Troco: R$ ' + changeDue.toFixed(2);

            // Atualizar estilos e cores dos valores
            amountPaidElement.classList.toggle('fw-bold', amountPaid < totalCartPrice);
            amountPaidElement.classList.toggle('text-warning', amountPaid < totalCartPrice);
            amountPaidElement.classList.toggle('text-primary', amountPaid >= totalCartPrice);

            amountRemainingElement.classList.toggle('fw-bold', amountRemaining > 0);
            amountRemainingElement.classList.toggle('text-danger', amountRemaining > 0);


            changeDueElement.classList.toggle('text-secondary', changeDue === 0);
            if (amountRemaining <= 0) {
                amountRemainingElement.classList.toggle('text-secondary');
                showFinishButton();
            }

            if (changeDue > 0) {
                changeDueElement.classList.toggle('fw-bold');
                changeDueElement.classList.toggle('text-success');
            }
        });
    });

});








// Verifica se o estado do off-canvas está armazenado no localStorage
const isOffCanvasOpen = localStorage.getItem('offCanvasOpen') === 'true';

// Função para atualizar o estado do off-canvas no localStorage
function atualizarEstadoOffCanvas(estahAberto) {
    localStorage.setItem('offCanvasOpen', estahAberto ? 'true' : 'false');
}

// Função para lidar com a alternância do off-canvas
function alternarOffCanvas() {
    const offCanvas = document.querySelector('.offcanvas');
    if (offCanvas) {
        const estahAbertoAtualmente = offCanvas.classList.contains('show');
        if (estahAbertoAtualmente) {
            offCanvas.classList.remove('show');
        } else {
            offCanvas.classList.add('show');
        }
        atualizarEstadoOffCanvas(!estahAbertoAtualmente);
    }
}

// Verifica o estado ao carregar a página e abre/fecha o off-canvas conforme necessário
window.addEventListener('DOMContentLoaded', () => {
    const estadoOffCanvas = localStorage.getItem('offCanvasOpen');
    if (estadoOffCanvas === 'true') {
        // Adiciona uma classe especial para desativar temporariamente a animação ao abrir o off-canvas
        document.body.classList.add('no-transition');
        alternarOffCanvas();
        // Aguarde um pequeno intervalo para garantir que o off-canvas esteja aberto antes de remover a classe
        setTimeout(() => {
            document.body.classList.remove('no-transition');
        }, 10);
    }
});

// Adicione um ouvinte de eventos para o botão de fechar o off-canvas
const botaoFechar = document.querySelector('.btn-close');
if (botaoFechar) {
    botaoFechar.addEventListener('click', () => {
        alternarOffCanvas();
    });
}

// Salvar o estado do off-canvas antes de recarregar a página
window.addEventListener('beforeunload', () => {
    const offCanvas = document.querySelector('.offcanvas');
    const estahAbertoAtualmente = offCanvas.classList.contains('show');
    atualizarEstadoOffCanvas(estahAbertoAtualmente);
});
