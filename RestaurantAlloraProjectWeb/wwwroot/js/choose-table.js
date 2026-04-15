(function () {
    function filterTables(minCap, maxCap) {
        const resultsArea = document.getElementById("results-area");
        const title = document.getElementById("filter-title");
        const tables = document.querySelectorAll(".luxury-table-card");

        if (!resultsArea || !title) {
            return;
        }

        resultsArea.classList.remove("results-visible");

        setTimeout(() => {
            let visibleCount = 0;

            tables.forEach(table => {
                const tableCap = parseInt(table.getAttribute("data-cap"), 10);

                if (tableCap >= minCap && tableCap <= maxCap) {
                    table.classList.remove("hidden-table");
                    visibleCount++;
                } else {
                    table.classList.add("hidden-table");
                }
            });

            const titleText = minCap === maxCap ? `за ${minCap} души` : `от ${minCap} до ${maxCap} души`;
            title.innerHTML = `<span class="filter-title-highlight">Налични маси ${titleText}</span>`;
            resultsArea.classList.add("results-visible");
            window.scrollTo({ top: resultsArea.offsetTop - 50, behavior: "smooth" });
        }, 300);
    }

    function showReservedTableMessage(reservedTable) {
        const infoBox = document.getElementById("table-reservation-info");

        if (!infoBox) {
            return;
        }

        const tableNumber = reservedTable.dataset.tableNumber;
        const reservationRanges = (reservedTable.dataset.reservationRanges || "")
            .split("|")
            .map(range => range.trim())
            .filter(Boolean);

        if (reservationRanges.length > 0) {
            const rangesText = reservationRanges.join("; ");
            infoBox.textContent = `Маса #${tableNumber} е резервирана: ${rangesText}. Изберете друг час или друга маса.`;
        } else {
            infoBox.textContent = `Маса #${tableNumber} е маркирана като резервирана, но няма намерена активна или бъдеща одобрена резервация.`;
        }

        infoBox.classList.add("table-reservation-info-visible");
        infoBox.scrollIntoView({ behavior: "smooth", block: "center" });
    }

    document.addEventListener("click", function (event) {
        const reservedTable = event.target.closest(".luxury-table-card.table-card-reserved");

        if (reservedTable) {
            event.preventDefault();
            showReservedTableMessage(reservedTable);
            return;
        }

        const filterButton = event.target.closest(".js-table-filter");

        if (!filterButton) {
            return;
        }

        const minCap = parseInt(filterButton.dataset.minCap, 10);
        const maxCap = parseInt(filterButton.dataset.maxCap, 10);
        filterTables(minCap, maxCap);
    });
})();
