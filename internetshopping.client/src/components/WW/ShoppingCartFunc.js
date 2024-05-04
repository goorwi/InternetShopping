import { reactive } from 'vue';

// Ďîëó÷ŕĺě ęîđçčíó čç localStorage, ĺńëč îíŕ ňŕě ĺńňü
const savedCartItems = JSON.parse(localStorage.getItem('cartItems')) || [];

const state = reactive({
    cartItems: savedCartItems
});

const methods = {
    addToCart(product) {
        const existingProductId = state.cartItems.findIndex(item => item.id === product.id)

        if (existingProductId !== -1) {
            state.cartItems[existingProductId].quantity++;
        } else {
            product.quantity = 1;
            state.cartItems.push(product);
        }

        localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
    },
    removeFromCart(index) {
        state.cartItems.splice(index, 1);
        // Ńîőđŕí˙ĺě îáíîâëĺííóţ ęîđçčíó â localStorage
        localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
    },
    increaseQuantity(item) {
        item.quantity++;
        // Ńîőđŕí˙ĺě îáíîâëĺííóţ ęîđçčíó â localStorage
        localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
    },
    decreaseQuantity(item) {
        if (item.quantity === 1) {
            var index = state.cartItems.lastIndexOf(item);
            this.removeFromCart(index);
        } else {
            item.quantity--;
            // Ńîőđŕí˙ĺě îáíîâëĺííóţ ęîđçčíó â localStorage
            localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
        }
    },
    getTotalCartItems() {
        // Ńóěěčđóĺě ęîëč÷ĺńňâî âńĺő ęíčă â ęîđçčíĺ
        return state.cartItems.reduce((total, item) => total + item.quantity, 0);
    },
    clearCart() {
        state.cartItems = [];
        // Î÷čůŕĺě ęîđçčíó â localStorage
        localStorage.removeItem('cartItems');
    }
}

export default {
    state,
    ...methods
};