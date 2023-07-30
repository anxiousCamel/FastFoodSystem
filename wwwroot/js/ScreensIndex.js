$(document).ready(function () {
    // Load checkboxes state when the page loads
    loadCheckboxesState();

    // Attach click event to each card
    $(".card").on("click", function () {
        $.ajax({
            type: "POST",
            url: "/Screens/UpdateConditionKitchen",
            data: { id: $(this).data("id") },
            success: function () { window.location.href = "/Screens"; },
            error: function (error) { console.log("Error updating ConditionKitchen: " + error.responseText); }
        });
    });


    // Function to filter the cards
    function filterCards() {
        var selectedTypes = $(".chkFilter:checked").map(function () { return parseInt($(this).val()); }).get();
        var isJaServidosChecked = $("#chkServidos").prop("checked");

        $("#cardsContainer > .card").each(function () {
            var cardType = parseInt($(this).data("type"));
            var isConditionKitchen5 = $(this).data("conditionkitchen") === 5;

            $(this).toggle(
                (selectedTypes.includes(cardType) && !isConditionKitchen5) ||
                (isJaServidosChecked && isConditionKitchen5 && selectedTypes.includes(cardType))
            );
        });
    }

    // Attach change event to each checkbox
    $(".chkFilter").change(function () {
        saveCheckboxesState();
        filterCards();
    });

    // Function to save the checkboxes state in localStorage
    function saveCheckboxesState() {
        var selectedCheckboxes = $(".chkFilter:checked").map(function () { return this.id; }).get();
        localStorage.setItem("selectedCheckboxes", JSON.stringify(selectedCheckboxes));
    }

    // Function to load the checkboxes state from localStorage
    function loadCheckboxesState() {
        var selectedCheckboxes = JSON.parse(localStorage.getItem("selectedCheckboxes")) || [];
        selectedCheckboxes.forEach(function (checkboxId) {
            $("#" + checkboxId).prop("checked", true);
        });
    }

    // Filter cards on page load
    loadCheckboxesState();
    filterCards();


    function updateTime() {
        // Itera sobre todos os elementos com a classe 'card'
        $('.card').each(function () {
            var productId = $(this).data('id');
            var conditionKitchen = $(this).data('conditionkitchen');

            // Se o produto não estiver na condição 5 (Já Servido), atualize o tempo
            if (conditionKitchen !== 5) {
                var productDateTime = new Date($(this).data('datetime'));
                var currentTime = new Date();
                var timeDifference = currentTime - productDateTime;

                // Converte a diferença de tempo para horas, minutos e segundos
                var hours = Math.floor(timeDifference / 3600000);
                var minutes = Math.floor((timeDifference % 3600000) / 60000);
                var seconds = Math.floor((timeDifference % 60000) / 1000);

                // Atualiza o conteúdo do elemento com o novo tempo formatado
                $(this).find('#timeSpan').text(formatTime(hours, minutes, seconds));
            }
        });
    }

    // Função para formatar o tempo
    function formatTime(hours, minutes, seconds) {
        return hours.toString().padStart(2, '0') + ':' +
            minutes.toString().padStart(2, '0') + ':' +
            seconds.toString().padStart(2, '0');
    }

    // Atualiza o tempo a cada 1 segundo (1000 milissegundos)
    setInterval(updateTime, 1000);



});

