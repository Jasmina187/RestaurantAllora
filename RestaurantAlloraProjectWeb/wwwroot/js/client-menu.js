(function () {
    let cart = [];

    function formatPrice(price) {
        return parseFloat(price).toFixed(2) + " €";
    }

    function addToCart(id, name, price) {
        const existingItem = cart.find(item => item.id === id);

        if (existingItem) {
            existingItem.quantity++;
        } else {
            cart.push({
                id: id,
                name: name,
                price: parseFloat(price),
                quantity: 1
            });
        }

        updateCartUI();
    }

    function changeQuantity(id, delta) {
        const item = cart.find(cartItem => cartItem.id === id);

        if (!item) {
            return;
        }

        item.quantity += delta;

        if (item.quantity <= 0) {
            cart = cart.filter(cartItem => cartItem.id !== id);
        }

        updateCartUI();
    }

    function updateCartUI() {
        const cartContainer = document.getElementById("cart-items");
        const checkoutBtn = document.getElementById("checkout-btn");
        const totalElem = document.getElementById("cart-total");

        if (!cartContainer || !totalElem) {
            return;
        }

        if (cart.length === 0) {
            cartContainer.innerHTML = '<div class="empty-cart text-center mt-5"><p>Количката е празна</p></div>';
            totalElem.innerText = "0.00 €";

            if (checkoutBtn) {
                checkoutBtn.disabled = true;
            }

            return;
        }

        if (checkoutBtn) {
            checkoutBtn.disabled = false;
        }

        let html = "";
        let total = 0;

        cart.forEach(item => {
            const itemTotal = item.price * item.quantity;
            total += itemTotal;

            html += `
                <div class="cart-item">
                    <div>
                        <h6 class="cart-item-title">${item.name}</h6>
                        <small class="cart-item-price">${formatPrice(itemTotal)}</small>
                    </div>
                    <div class="qty-controls">
                        <button class="qty-btn js-cart-quantity" data-dish-id="${item.id}" data-delta="-1">-</button>
                        <span>${item.quantity}</span>
                        <button class="qty-btn js-cart-quantity" data-dish-id="${item.id}" data-delta="1">+</button>
                    </div>
                </div>`;
        });

        cartContainer.innerHTML = html;
        totalElem.innerText = formatPrice(total);
    }

    function filterMenu(category, button) {
        document.querySelectorAll(".pill-btn").forEach(btn => btn.classList.remove("active"));
        button.classList.add("active");

        document.querySelectorAll(".dish-card").forEach(card => {
            if (category === "all" || card.getAttribute("data-category") === category) {
                card.style.display = "flex";
            } else {
                card.style.display = "none";
            }
        });
    }

    async function submitOrder(button) {
        const formattedCart = cart.map(item => ({
            dishId: item.id,
            dishName: item.name,
            price: item.price,
            quantity: item.quantity
        }));

        button.disabled = true;
        button.innerText = "Обработка...";

        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');

        const response = await fetch("/Order/SubmitOrder", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": tokenInput ? tokenInput.value : ""
            },
            body: JSON.stringify(formattedCart)
        });

        if (response.ok) {
            alert("Поръчката е приета!");
            cart = [];
            window.location.href = "/Order/Index";
            return;
        }

        alert("Грешка при поръчката.");
        button.disabled = false;
        button.innerText = "Поръчай";
    }

    document.addEventListener("click", function (event) {
        const addButton = event.target.closest(".js-add-to-cart");
        if (addButton) {
            addToCart(addButton.dataset.dishId, addButton.dataset.dishName, addButton.dataset.dishPrice);
            return;
        }

        const quantityButton = event.target.closest(".js-cart-quantity");
        if (quantityButton) {
            changeQuantity(quantityButton.dataset.dishId, parseInt(quantityButton.dataset.delta, 10));
            return;
        }

        const filterButton = event.target.closest(".js-menu-filter");
        if (filterButton) {
            filterMenu(filterButton.dataset.category, filterButton);
        }
    });

    const checkoutButton = document.getElementById("checkout-btn");

    if (checkoutButton) {
        checkoutButton.addEventListener("click", function () {
            submitOrder(checkoutButton);
        });
    }
})();
