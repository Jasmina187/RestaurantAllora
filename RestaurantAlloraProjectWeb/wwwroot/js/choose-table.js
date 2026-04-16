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
            tables.forEach(table => {
                const tableCap = parseInt(table.getAttribute("data-cap"), 10);

                if (tableCap >= minCap && tableCap <= maxCap) {
                    table.classList.remove("hidden-table");
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

    function showUnavailableTableMessage(table) {
        const infoBox = document.getElementById("table-reservation-info");

        if (!infoBox) {
            return;
        }

        const tableNumber = table.dataset.tableNumber;
        const isPending = table.classList.contains("table-card-pending");
        const rangesAttribute = isPending ? "pendingRanges" : "reservationRanges";
        const ranges = (table.dataset[rangesAttribute] || "")
            .split("|")
            .map(range => range.trim())
            .filter(Boolean);

        if (ranges.length > 0) {
            const rangesText = ranges.join("; ");
            infoBox.textContent = isPending
                ? `Маса #${tableNumber} очаква одобрение: ${rangesText}. Изберете друг час или друга маса.`
                : `Маса #${tableNumber} е резервирана: ${rangesText}. Изберете друг час или друга маса.`;
        } else {
            infoBox.textContent = isPending
                ? `Маса #${tableNumber} има заявка, която очаква одобрение.`
                : `Маса #${tableNumber} е маркирана като резервирана.`;
        }

        infoBox.classList.add("table-reservation-info-visible");
        infoBox.scrollIntoView({ behavior: "smooth", block: "center" });
    }

    document.addEventListener("click", function (event) {
        const unavailableTable = event.target.closest(".luxury-table-card.table-card-reserved, .luxury-table-card.table-card-pending");

        if (unavailableTable) {
            event.preventDefault();
            showUnavailableTableMessage(unavailableTable);
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

    const autoFilterSource = document.querySelector(".luxury-container");

    if (autoFilterSource) {
        const minCap = parseInt(autoFilterSource.dataset.autoMinCap, 10);
        const maxCap = parseInt(autoFilterSource.dataset.autoMaxCap, 10);

        if (!Number.isNaN(minCap) && !Number.isNaN(maxCap)) {
            filterTables(minCap, maxCap);
        }
    }
})();
