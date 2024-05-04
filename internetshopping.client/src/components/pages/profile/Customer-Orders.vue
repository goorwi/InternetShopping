<template>
    <Sidebar />
    <div class="main">
        <h1>Ваши заказы</h1>
        <div v-if="customerOrders.length === 0">У вас пока нет заказов.</div>
        <div v-else>
            <div v-for="order in customerOrders" :key="order.id" class="card mb-3">
                <div class="card-body">
                    <h5 class="card-title">Заказ от {{ order.orderDate }}</h5>
                    <p class="card-text">Итоговая цена: {{ order.cost }}</p>
                    <p class="card-text">Товары:</p>
                    <ul class="list-group">
                        <li v-for="line in order.orderLines" :key="line.lineID" class="list-group-item">
                            {{ line.product.title }} - ({{ line.amount }} шт.)
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { computed } from 'vue';
    import { useStore } from 'vuex';
    import axios from 'axios';
    import Sidebar from '@/components/pages/profile/Profile-sidebar.vue';

    export default {
        name: 'OrdersPage',
        components: {
            Sidebar,
        },
        data() {
            return {
                customerOrders: [],
            };
        },
        setup() {
            const store = useStore();
            const currentUser = computed(() => store.state.currentUser);

            return {
                currentUser
            };
        },
        methods: {
            async fetchOrders() {
                try {
                    const response = await axios.get('/orders');
                    const orders = response.data;
                    this.customerOrders = orders.filter(order => order.customerId.toString() === this.currentUser.userId);
                    
                    const orderLinesResponse = await axios.get(`/order_item/get_all`);
                    const orderLines = orderLinesResponse.data;

                    for (const order of this.customerOrders) {
                        order.orderLines = orderLines.filter(line => line.orderId === order.id);
                    }
                } catch (error) {
                    console.error('Ошибка при получении заказов:', error);
                }
            },
            formatDate(dateString) {
                const date = new Date(dateString);
                return `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()}`;
            }
        },
        created() {
            this.fetchOrders();
        }
    };
</script>

<style scoped>
    .main {
        margin-left: 200px;
        padding: 20px;
    }

    .empty-orders {
        font-style: italic;
        color: #666;
    }

    .card {
        border: 1px solid #ddd;
        border-radius: 8px;
        overflow: hidden;
    }

    .card-body {
        padding: 20px;
    }

    .card-title {
        font-size: 1.25rem;
        margin-bottom: 10px;
    }

    .card-text {
        margin-bottom: 15px;
    }

    .list-group {
        margin-top: 15px;
    }

    .list-group-item {
        border: none;
        padding: 8px 0;
    }
</style>