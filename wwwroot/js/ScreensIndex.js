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

    
});

