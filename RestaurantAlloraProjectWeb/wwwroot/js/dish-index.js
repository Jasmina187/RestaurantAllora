(function () {
    const searchInput = document.getElementById("searchInput");
    const categoryFilter = document.getElementById("categoryFilter");
    const cards = document.querySelectorAll(".dish-card");

    if (!searchInput || !categoryFilter || cards.length === 0) {
        return;
    }

    function filterMenu() {
        const search = searchInput.value.toLowerCase();
        const category = categoryFilter.value;

        cards.forEach(card => {
            const name = card.dataset.name || "";
            const cardCategory = card.dataset.category || "";
            const matchSearch = name.includes(search);
            const matchCategory = !category || cardCategory === category;

            card.style.display = (matchSearch && matchCategory) ? "block" : "none";
        });
    }

    searchInput.addEventListener("input", filterMenu);
    categoryFilter.addEventListener("change", filterMenu);
})();
