(function () {
    function getTableOptions(tableSelect) {
        return Array.from(tableSelect.options).filter(option => option.dataset.capacity);
    }

    function updateTableOptions() {
        const guestsInput = document.getElementById("NumberOfGuests");
        const tableSelect = document.getElementById("TableId");
        const hint = document.getElementById("table-capacity-hint");

        if (!guestsInput || !tableSelect) {
            return;
        }

        const guests = parseInt(guestsInput.value, 10);
        const tableOptions = getTableOptions(tableSelect);

        tableOptions.forEach(option => {
            option.hidden = false;
            option.disabled = false;
        });

        if (!Number.isInteger(guests) || guests < 1) {
            if (hint) {
                hint.textContent = "Въведете брой гости, за да видите най-подходящите маси.";
            }

            return;
        }

        const matchingCapacities = tableOptions
            .map(option => parseInt(option.dataset.capacity, 10))
            .filter(capacity => capacity >= guests)
            .sort((first, second) => first - second);

        if (matchingCapacities.length === 0) {
            if (hint) {
                hint.textContent = `Няма маса с достатъчен капацитет за ${guests} гости.`;
            }

            tableSelect.value = "00000000-0000-0000-0000-000000000000";
            return;
        }

        const closestCapacity = matchingCapacities[0];

        tableOptions.forEach(option => {
            const capacity = parseInt(option.dataset.capacity, 10);
            const isClosest = capacity === closestCapacity;

            option.hidden = !isClosest;
            option.disabled = !isClosest;
        });

        const selectedOption = tableSelect.selectedOptions[0];

        if (selectedOption && selectedOption.dataset.capacity && parseInt(selectedOption.dataset.capacity, 10) !== closestCapacity) {
            tableSelect.value = "00000000-0000-0000-0000-000000000000";
        }

        if (hint) {
            hint.textContent = `Показани са най-близките подходящи маси: ${closestCapacity} места за ${guests} гости.`;
        }
    }

    document.addEventListener("input", function (event) {
        if (event.target && event.target.id === "NumberOfGuests") {
            updateTableOptions();
        }
    });

    document.addEventListener("DOMContentLoaded", updateTableOptions);
})();
