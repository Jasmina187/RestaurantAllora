(function () {
    const cartStorageKey = "restaurantAllora.cart";
    let cart = loadCart();

    function loadCart() {
        try {
            const storedCart = JSON.parse(localStorage.getItem(cartStorageKey) || "[]");

            if (!Array.isArray(storedCart)) {
                return [];
            }

            return storedCart
                .map(item => ({
                    id: item.id,
                    name: item.name,
                    price: parseFloat(item.price),
                    quantity: parseInt(item.quantity, 10)
                }))
                .filter(item => item.id && item.name && !Number.isNaN(item.price) && item.quantity > 0);
        } catch {
            return [];
        }
    }

    function saveCart() {
        localStorage.setItem(cartStorageKey, JSON.stringify(cart));
    }

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

        saveCart();
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

        saveCart();
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
            cartContainer.innerHTML = '<div class="empty-cart text-center"><p>Количката е празна</p></div>';
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
                        <button type="button" class="qty-btn js-cart-quantity" data-dish-id="${item.id}" data-delta="-1">-</button>
                        <span>${item.quantity}</span>
                        <button type="button" class="qty-btn js-cart-quantity" data-dish-id="${item.id}" data-delta="1">+</button>
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

    function addHiddenInput(form, name, value) {
        const input = document.createElement("input");
        input.type = "hidden";
        input.name = name;
        input.value = value;
        form.appendChild(input);
    }

    function goToCheckout(button) {
        if (cart.length === 0) {
            return;
        }

        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        const form = document.createElement("form");
        form.method = "post";
        form.action = "/Order/Checkout";

        if (tokenInput) {
            addHiddenInput(form, "__RequestVerificationToken", tokenInput.value);
        }

        cart.forEach((item, index) => {
            addHiddenInput(form, `CustomerOrderItems[${index}].DishId`, item.id);
            addHiddenInput(form, `CustomerOrderItems[${index}].DishName`, item.name);
            addHiddenInput(form, `CustomerOrderItems[${index}].Quantity`, item.quantity);
            addHiddenInput(form, `CustomerOrderItems[${index}].Price`, item.price);
        });

        button.disabled = true;
        button.innerText = "Към финализиране...";
        localStorage.removeItem(cartStorageKey);
        document.body.appendChild(form);
        form.submit();
    }

    document.addEventListener("click", function (event) {
        const addButton = event.target.closest(".js-add-to-cart");
        if (addButton) {
            addToCart(addButton.dataset.dishId, addButton.dataset.dishName, addButton.dataset.dishPrice);

            if (addButton.dataset.redirectUrl) {
                window.location.href = addButton.dataset.redirectUrl;
            }

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
            goToCheckout(checkoutButton);
        });
    }

    updateCartUI();
})();
