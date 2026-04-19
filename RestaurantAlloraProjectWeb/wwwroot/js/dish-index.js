(function () {
    const searchInput = document.getElementById("searchInput");
    const categoryFilter = document.getElementById("categoryFilter");
    const allergenFilter = document.getElementById("allergenFilter");
    const emptyState = document.getElementById("menuFilterEmpty");
    const cards = document.querySelectorAll(".dish-card");

    if (!searchInput || !categoryFilter || cards.length === 0) {
        return;
    }

    function filterMenu() {
        const search = searchInput.value.toLowerCase();
        const category = categoryFilter.value;
        const allergen = allergenFilter ? allergenFilter.value : "";
        let visibleCards = 0;

        cards.forEach(card => {
            const name = card.dataset.name || "";
            const cardCategory = card.dataset.category || "";
            const allergens = (card.dataset.allergens || "").split("|").filter(Boolean);
            const matchSearch = name.includes(search);
            const matchCategory = !category || cardCategory === category;
            const matchAllergen = !allergen ||
                (allergen === "no-allergens" ? allergens.length === 0 : !allergens.includes(allergen));
            const isVisible = matchSearch && matchCategory && matchAllergen;

            card.style.display = isVisible ? "block" : "none";

            if (isVisible) {
                visibleCards++;
            }
        });

        if (emptyState) {
            emptyState.classList.toggle("d-none", visibleCards !== 0);
        }
    }

    searchInput.addEventListener("input", filterMenu);
    categoryFilter.addEventListener("change", filterMenu);
    allergenFilter?.addEventListener("change", filterMenu);
})();
