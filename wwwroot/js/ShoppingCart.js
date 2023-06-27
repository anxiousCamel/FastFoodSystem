document.addEventListener('DOMContentLoaded', function () {
    var decreaseQuantityButtons = document.querySelectorAll('.decrease-quantity');
    var increaseQuantityButtons = document.querySelectorAll('.increase-quantity');
    var totalPrices = document.querySelectorAll('[data-price-element]');
    var quantityInputs = document.querySelectorAll('.quantity');

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

    increaseQuantityButtons.forEach(function (button, index) {
        button.addEventListener('click', function () {
            var quantityInput = this.closest('.modal').querySelector('.quantity');
            var currentQuantity = parseInt(quantityInput.value);
            currentQuantity++;
            quantityInput.value = currentQuantity;
            updateTotalPrice(index);
        });
    });

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

    var additionalProductCheckboxes = document.querySelectorAll('.additional-product-checkbox');

    additionalProductCheckboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            updateTotalPrices();
        });
    });

    function updateTotalPrices() {
        totalPrices.forEach(function (price, index) {
            updateTotalPrice(index);
        });
    }
});
