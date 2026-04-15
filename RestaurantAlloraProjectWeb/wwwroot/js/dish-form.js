(function () {
    document.addEventListener("DOMContentLoaded", function () {
        const allergensSelect = document.getElementById("allergensSelect");

        if (!allergensSelect || typeof Choices === "undefined") {
            return;
        }

        new Choices(allergensSelect, {
            removeItemButton: true,
            searchEnabled: true,
            placeholderValue: "Избери алергени",
            noResultsText: "Алергени не са намерени",
            itemSelectText: "Избери",
            shouldSort: false
        });
    });
})();
