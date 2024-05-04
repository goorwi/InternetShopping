<template>
    <div class="main">
        <h1 class="mb-4">Список книг</h1>
        <div class="table-responsive">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Название</th>
                        <th>Цена</th>
                        <th>Категория</th>
                        <th>Описание</th>
                        <th>Производитель</th>
                        <th>Поставщик</th>
                        <th>Действия</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="product in products" :key="product.id">
                        <td>{{ product.title }}</td>
                        <td>{{ product.price }} ₽</td>
                        <td>{{ product.category.title }}</td>
                        <td>{{ product.content }}</td>
                        <td>{{ product.producer }}</td>
                        <td>{{ product.supplier.title }}</td>
                        <td>
                            <button @click="openEditModal(product)" class="btn btn-primary"><i class="fa fa-pencil" aria-hidden="true"></i></button>
                            <button @click="deleteProduct(product.id)" class="btn btn-danger"><i class="fa fa-trash"></i></button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <!-- Edit Product Modal -->
        <AdminEditProduct v-if="showEditModal" :product="selectedProduct" @close="closeEditModal" @save="updateProduct" />

        <!-- Add Product Button -->
        <button @click="openAddModal" class="btn btn-success mt-4">Добавить товар</button>

        <!-- Add Product Modal -->
        <AdminAddProduct v-if="showAddModal" @close="closeAddModal" @add="addProduct" />
    </div>
</template>

<script>
    import axios from 'axios';
    import AdminEditProduct from './AdminEditProduct.vue';
    import AdminAddProduct from './AdminAddProduct.vue';

    export default {
        components: {
            AdminAddProduct,
            AdminEditProduct,
        },
        data() {
            return {
                products: [],
                showEditModal: false,
                showAddModal: false,
                selectedProduct: null,
            };
        },
        mounted() {
            this.getProducts();
        },
        methods: {
            async getProducts() {
                try {
                    const response = await axios.get('/product');
                    this.products = response.data;
                } catch (error) {
                    console.error(error);
                }
            },
            async deleteProduct(productId) {
                try {
                    await axios.delete(`product/${productId}/delete`);
                    this.products = this.products.filter((product) => product.id !== productId);
                } catch (error) {
                    console.error(error);
                }
            },
            openEditModal(product) {
                this.selectedProduct = { ...product };
                this.showEditModal = true;
            },
            closeEditModal() {
                this.showEditModal = false;
            },
            async updateProduct(updatedProduct) {
                try {
                    await axios.put(`product/${updatedProduct.id}/update`, updatedProduct);
                    const index = this.products.findIndex((product) => product.id === updatedProduct.id);
                    if (index !== -1) {
                        this.products.splice(index, 1, updatedProduct);
                    }
                    this.closeEditModal();
                } catch (error) {
                    console.error(error);
                }
            },
            openAddModal() {
                this.showAddModal = true;
            },
            closeAddModal() {
                this.showAddModal = false;
            },
            async addProduct(product) {
                try {
                    const response = await axios.post('/product/add', product);
                    this.products.push(response.data);
                    this.closeAddModal();
                    this.getProducts();
                } catch (error) {
                    console.error(error);
                }
            },
        },
    };
</script>

<style>
    .main {
        margin-left: 100px;
        padding: 20px;
    }

    .table-responsive {
        overflow-x: auto;
    }

    .btn {
        font-size: 14px;
    }
</style>
