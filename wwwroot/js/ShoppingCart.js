document.addEventListener('DOMContentLoaded', function () {
    // Selecionar os botões de diminuir e aumentar a quantidade
    var decreaseQuantityButtons = document.querySelectorAll('.decrease-quantity');
    var increaseQuantityButtons = document.querySelectorAll('.increase-quantity');

    // Selecionar os elementos que exibem os preços totais
    var totalPrices = document.querySelectorAll('[data-price-element]');

    // Selecionar os inputs de quantidade
    var quantityInputs = document.querySelectorAll('.quantity');

    // Função para diminuir a quantidade
    decreaseQuantityButtons.forEach(function (button, index) {
        button.addEventListener('click', function () {
            var quantityInput = this.closest('.modal').querySelector('.quantity');
            var currentQuantity = parseInt(quantityInput.value);
            if (currentQuantity > 1) {
                currentQuantity--;
                quantityInput.value = currentQuantity;
                updateTotalPrice(index);
            }
        });
    });

    // Função para aumentar a quantidade
    increaseQuantityButtons.forEach(function (button, index) {
        button.addEventListener('click', function () {
            var quantityInput = this.closest('.modal').querySelector('.quantity');
            var currentQuantity = parseInt(quantityInput.value);
            currentQuantity++;
            quantityInput.value = currentQuantity;
            updateTotalPrice(index);
        });
    });

    // Função para atualizar o preço total
    function updateTotalPrice(index) {
        var quantityInput = quantityInputs[index];
        var modal = quantityInput.closest('.modal');
        var basePriceElement = modal.querySelector('.total-price[data-price-element]');
        var basePrice = parseFloat(basePriceElement.getAttribute('data-price'));
        var quantity = parseInt(quantityInput.value);

        var additionalProducts = modal.querySelectorAll('.additional-product-checkbox:checked');
        var additionalPrice = 0;
        additionalProducts.forEach(function (checkbox) {
            additionalPrice += parseFloat(checkbox.getAttribute('data-price'));
        });

        var newTotal = (basePrice + additionalPrice) * quantity;
        totalPrices[index].textContent = newTotal.toFixed(2);
    }

    // Selecionar os checkboxes de produtos adicionais
    var additionalProductCheckboxes = document.querySelectorAll('.additional-product-checkbox');

    // Função para lidar com a alteração dos checkboxes
    additionalProductCheckboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            updateTotalPrices();
        });
    });

    // Função para atualizar todos os preços totais
    function updateTotalPrices() {
        totalPrices.forEach(function (price, index) {
            updateTotalPrice(index);
        });
    }
});
