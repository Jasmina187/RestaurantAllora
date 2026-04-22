(function () {
    const automaticTableValue = "00000000-0000-0000-0000-000000000000";

    function parseLocalDateTime(value) {
        if (!value) {
            return null;
        }

        const parts = value.split("T");

        if (parts.length !== 2) {
            return null;
        }

        return parseDateAndTime(parts[0], parts[1]);
    }

    function parseDateAndTime(dateValue, timeValue) {
        if (!dateValue || !timeValue) {
            return null;
        }

        const dateParts = dateValue.split("-").map(part => parseInt(part, 10));
        const timeParts = timeValue.split(":").map(part => parseInt(part, 10));

        if (dateParts.length !== 3 || timeParts.length < 2 || dateParts.some(Number.isNaN) || timeParts.some(Number.isNaN)) {
            return null;
        }

        return new Date(dateParts[0], dateParts[1] - 1, dateParts[2], timeParts[0], timeParts[1]);
    }

    function initializeReservationDateTime() {
        const reservationDateInput = document.getElementById("ReservationDate");
        const dateSelect = document.getElementById("ReservationDateOnly");
        const timeSelect = document.getElementById("ReservationTimeOnly");

        if (!reservationDateInput || !dateSelect || !timeSelect) {
            return;
        }

        const minDateTime = parseLocalDateTime(reservationDateInput.dataset.minDatetime);
        const maxDateTime = parseLocalDateTime(reservationDateInput.dataset.maxDatetime);

        function updateReservationDateTime() {
            let firstEnabledTime = null;
            let selectedTimeIsEnabled = false;

            Array.from(timeSelect.options).forEach(option => {
                const optionDateTime = parseDateAndTime(dateSelect.value, option.value);
                const isDisabled = !optionDateTime
                    || (minDateTime && optionDateTime < minDateTime)
                    || (maxDateTime && optionDateTime > maxDateTime);

                option.disabled = isDisabled;

                if (!isDisabled && firstEnabledTime === null) {
                    firstEnabledTime = option.value;
                }

                if (!isDisabled && option.selected) {
                    selectedTimeIsEnabled = true;
                }
            });

            if (!selectedTimeIsEnabled && firstEnabledTime !== null) {
                timeSelect.value = firstEnabledTime;
            }

            const selectedDateTime = parseDateAndTime(dateSelect.value, timeSelect.value);
            const isValid = selectedDateTime
                && (!minDateTime || selectedDateTime >= minDateTime)
                && (!maxDateTime || selectedDateTime <= maxDateTime);

            reservationDateInput.value = `${dateSelect.value}T${timeSelect.value}`;
            timeSelect.setCustomValidity(isValid ? "" : "Изберете бъдещ час за резервация.");
        }

        dateSelect.addEventListener("change", updateReservationDateTime);
        timeSelect.addEventListener("change", updateReservationDateTime);
        updateReservationDateTime();
    }

    function getTableOptions(tableSelect) {
        return Array.from(tableSelect.options).filter(option => option.dataset.capacity);
    }

    function getGuestRangeForCapacity(capacity) {
        if (!Number.isInteger(capacity) || capacity < 1) {
            return null;
        }

        if (capacity <= 2) {
            return { min: 1, max: capacity };
        }

        if (capacity <= 4) {
            return { min: Math.min(3, capacity), max: capacity };
        }

        if (capacity <= 6) {
            return { min: Math.min(5, capacity), max: capacity };
        }

        return { min: 8, max: capacity };
    }

    function formatGuestRange(range) {
        if (!range) {
            return "";
        }

        if (range.min === range.max) {
            const guestWord = range.max === 1 ? "гост" : "гости";
            return `${range.max} ${guestWord}`;
        }

        return `между ${range.min} и ${range.max} гости`;
    }

    function getSelectedTableRange(tableSelect) {
        const selectedOption = tableSelect.selectedOptions[0];

        if (!selectedOption || selectedOption.value === automaticTableValue || !selectedOption.dataset.capacity) {
            return null;
        }

        const capacity = parseInt(selectedOption.dataset.capacity, 10);
        const range = getGuestRangeForCapacity(capacity);

        if (!range) {
            return null;
        }

        return { ...range, capacity };
    }

    function applyGuestInputRange(guestsInput, tableSelect, hint) {
        const selectedRange = getSelectedTableRange(tableSelect);

        if (selectedRange) {
            guestsInput.min = selectedRange.min.toString();
            guestsInput.max = selectedRange.max.toString();

            const guests = parseInt(guestsInput.value, 10);

            if (Number.isInteger(guests) && (guests < selectedRange.min || guests > selectedRange.max)) {
                guestsInput.setCustomValidity(`Избраната маса приема ${formatGuestRange(selectedRange)}.`);
            } else {
                guestsInput.setCustomValidity("");
            }

            if (hint) {
                hint.textContent = `Избраната маса е за ${selectedRange.capacity} места. Броят гости трябва да бъде ${formatGuestRange(selectedRange)}.`;
            }

            return selectedRange;
        }

        guestsInput.min = guestsInput.dataset.defaultMin || "1";
        guestsInput.max = guestsInput.dataset.defaultMax || "10";
        guestsInput.setCustomValidity("");

        return null;
    }

    function clampGuestsToSelectedTable() {
        const guestsInput = document.getElementById("NumberOfGuests");
        const tableSelect = document.getElementById("TableId");

        if (!guestsInput || !tableSelect) {
            return;
        }

        const selectedRange = getSelectedTableRange(tableSelect);
        const guests = parseInt(guestsInput.value, 10);

        if (!selectedRange || !Number.isInteger(guests)) {
            return;
        }

        if (guests < selectedRange.min) {
            guestsInput.value = selectedRange.min;
        }

        if (guests > selectedRange.max) {
            guestsInput.value = selectedRange.max;
        }

        guestsInput.setCustomValidity("");
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
        const selectedRange = applyGuestInputRange(guestsInput, tableSelect, hint);

        tableOptions.forEach(option => {
            option.hidden = false;
            option.disabled = false;
        });

        if (selectedRange) {
            return;
        }

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

            tableSelect.value = automaticTableValue;
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
            tableSelect.value = automaticTableValue;
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

    document.addEventListener("change", function (event) {
        if (event.target && event.target.id === "NumberOfGuests") {
            clampGuestsToSelectedTable();
            updateTableOptions();
            return;
        }

        if (event.target && event.target.id === "TableId") {
            clampGuestsToSelectedTable();
            updateTableOptions();
        }
    });

    document.addEventListener("DOMContentLoaded", function () {
        initializeReservationDateTime();
        clampGuestsToSelectedTable();
        updateTableOptions();
    });
})();
