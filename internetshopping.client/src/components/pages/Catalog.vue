<template>
    <div>
        <h1>Каталог товаров</h1>
        <div class="row">
            <div class="col-md-3" v-for="product in products" :key="product.id">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">{{product.title}}</h4>
                        <h5 class="card-text">{{product.supplier.title}}</h5>
                        <div class="card-text">{{product.price}}</div>
                        <div class="row justify-content-end">
                            <button class="btn btn-success" @click="addToCart(product)">В корзину</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { defineComponent } from 'vue';
    import ShoppingCartFunc from '../WW/ShoppingCartFunc';
    import axios from 'axios'

    export default defineComponent({
        data() {
            return {
                loading: false,
                products: null,
                isConnected: false
            }
        },
        mounted() {{
            this.fetchData();

        }},
        watch: {
            '$route': 'fetchData'
        },
        methods: {
            async fetchData() {
                this.loading = true;
                this.products = null;

                const response = await axios.get('/product');
                this.products = response.data;
            },
            addToCart(product) {
                ShoppingCartFunc.addToCart(product);
            }
        }
    });
</script>

<style scoped>
    .card {
        box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
        transition: 0.3s;
        border-radius: 5px;
    }

    .card:hover {
        box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2);
    }

    .card-body {
        padding: 20px;
    }

    .btn-success {
        width: 100%;
    }

    .spinner-border {
        margin-top: 50px;
    }

    .text-center {
        text-align: center;
    }

    .mb-4 {
        margin-bottom: 2rem;
    }

    .mt-3 {
        margin-top: 0.75rem;
    }

    .mt-4 {
        margin-top: 1rem;
    }
</style>