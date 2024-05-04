<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-8">
                <h1 class="header">Корзина</h1>
                <div v-if="cartItems.length === 0" class="empty-cart">
                    <p>Корзина пуста</p>
                </div>
                <div v-else>
                    <div v-for="(item, index) in cartItems" :key="index" class="card mb-3 mx-3">
                        <div class="card-body">
                            <h5 class="card-title">{{ item.title }}</h5>
                            <h6 class="card-subtitle mb-2 text-muted">{{ item.supplier.title }}</h6>
                            <p class="card-text">Цена: {{ item.price }} | Количество: <button @click="decrease(item)" class="btn btn-primary btn-sm">-</button> {{ item.quantity }} <button @click="increase(item)" class="btn btn-primary btn-sm">+</button></p>
                            <button @click="removeFromCart(index)" class="btn btn-danger">Удалить</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Товары в корзине</h5>
                        <ul class="list-group">
                            <li class="list-group-item" v-for="(item, index) in cartItems" :key="index">
                                {{ item.title }} - {{ item.quantity }} шт. - {{ item.price * item.quantity }} ₽
                            </li>
                        </ul>
                        <h5 class="mt-3">Итого: {{ totalCartPrice }} ₽</h5>
                        <button @click="createOrder" class="btn btn-success btn-block">Оформить заказ</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import ShoppingCartFunc from '../WW/ShoppingCartFunc.js';
    import axios from 'axios';
    import { computed } from 'vue';
    import { useStore } from 'vuex';

    export default {
        setup() {
            const store = useStore();
            const currentUser = computed(() => store.state.currentUser);

            return {
                currentUser
            };
        },
        data() {
            return {
                totalCartPrice: 0,
                order: {}
            };
        },
        computed: {
            cartItems() {
                return ShoppingCartFunc.state.cartItems;
            }
        },
        watch: {
            cartItems: {
                handler: 'updateTotalPrice',
                deep: true
            }
        },
        mounted() {
            this.updateTotalPrice();
        },
        methods: {
            async createOrder() {
                if (this.cartItems.length !== 0) {
                    const cust = await axios.get('customers/' + this.currentUser.userId);
                    const order = {
                        OrderDate: new Date().toLocaleDateString('en-US').toString(),
                        CustomerId: this.currentUser.userId,
                        OrderStatus: 'Done',
                        Cost: this.totalCartPrice,
                        customer: cust.data
                    };
                    await axios.post(`/orders/add_order`, order);
                    const curOrder = (await axios.get(`/orders/get`, {
                        params: {
                            CustomerId: order.CustomerId,
                            Cost: order.Cost,
                            OrderStatus: order.OrderStatus,
                        }
                    })).data;
                    await Promise.all(this.cartItems.map(async (item) => {
                        const stockroom = await axios.get('/stockroom').then(x => x.data.find(x => x.product.id === item.id))
                        await axios.post('/order_item/add', {
                            OrderId: curOrder.id,
                            Amount: item.quantity,
                            ProductId: item.id,
                            StockroomId: stockroom.id
                        });
                    }));
                    ShoppingCartFunc.clearCart();
                }
            },
            removeFromCart(index) {
                ShoppingCartFunc.removeFromCart(index);
            },
            increase(item) {
                ShoppingCartFunc.increaseQuantity(item);
            },
            decrease(item) {
                ShoppingCartFunc.decreaseQuantity(item);
            },
            updateTotalPrice() {
                this.totalCartPrice = this.cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
            }
        }
    };
</script>

<style scoped>
    .empty-cart {
        text-align: center;
        margin-top: 50px;
        font-size: 20px;
        color: #555;
    }

    .card {
        box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
        transition: 0.3s;
        border-radius: 5px;
        margin-top: 25px;
    }

    .card:hover {
        box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2);
    }

    .card-body {
        padding: 20px;
    }

    .btn-danger,
    .btn-primary {
        margin-right: 5px;
    }

    .btn-success {
        margin-top: 10px;
    }

    .list-group-item {
        font-size: 14px;
    }

    .col-md-8 {
        margin-top: 25px
    }

    .header {
        margin-left: 10px
    }
</style>
