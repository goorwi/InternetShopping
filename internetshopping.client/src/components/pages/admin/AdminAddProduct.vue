<template>
    <div class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Добавить товар</h2>
                    <button type="button" class="close" @click="closeModal">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="addProduct">
                        <div class="form-group">
                            <label for="title">Название:</label>
                            <input type="text" class="form-control" id="title" v-model="newProduct.title" placeholder="Введите название товара">
                        </div>
                        <div class="form-group">
                            <label for="category">Категория:</label>
                            <input type="text" class="form-control" id="category" v-model="newProduct.category" placeholder="Введите категорию товара">
                        </div>
                        <div class="form-group">
                            <label for="producer">Производитель:</label>
                            <input type="text" class="form-control" id="producer" v-model="newProduct.producer" placeholder="Введите производителя товара">
                        </div>
                        <div class="form-group">
                            <label for="supplier">Поставщик:</label>
                            <input type="text" class="form-control" id="supplier" v-model.number="newProduct.supplier" placeholder="Введите поставщика товара">
                        </div>
                        <div class="form-group">
                            <label for="content">Описание:</label>
                            <textarea class="form-control" id="content" rows="3" v-model="newProduct.content" placeholder="Введите описание товара"></textarea>
                        </div>
                        <div class="form-group">
                            <label for="price">Цена:</label>
                            <input type="number" class="form-control" id="price" v-model="newProduct.price" placeholder="Введите цену товара">
                        </div>
                        <div class="modal-footer">
                            <button type="submit" class="btn btn-primary">Сохранить</button>
                            <button type="button" class="btn btn-secondary" @click="closeModal">Отмена</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import checkProduct from './checkNewProduct.js'

    export default {
        data() {
            return {
                newProduct: {
                    title: '',
                    category: '',
                    producer: '',
                    supplier: '',
                    content: '',
                    price: 0,
                }
            };
        },
        methods: {
            async addProduct() {
                try {
                    const product = await checkProduct(this.newProduct);
                    const result = {
                        Title: product.title,
                        CategoryId: product.category,
                        Producer: product.producer,
                        SupplierId: product.supplier,
                        Content: product.content,
                        Price: product.price
                    }
                    this.$emit('add', result);
                    this.closeModal();
                } catch (error) {
                    console.error(error);
                }
            },
            closeModal() {
                this.$emit('close');
            },
        }
    };
</script>

<style>
    .modal {
        background-color: rgba(0, 0, 0, 0.5); /* Semi-transparent background */
        display: flex;
        justify-content: center;
        align-items: center;
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        z-index: 999; /* Ensure modal is on top */
    }

    .modal-dialog {
        max-width: 500px; /* Limit width for better readability */
    }

    .modal-content {
        background-color: #fff;
        border-radius: 5px;
        box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.2); /* Add shadow for depth */
    }

    .modal-header {
        border-bottom: 1px solid #dee2e6;
        padding: 15px;
        background-color: #f8f9fa; /* Light background color for header */
        border-top-left-radius: 5px;
        border-top-right-radius: 5px;
    }

    .modal-title {
        margin-bottom: 0;
    }

    .modal-body {
        padding: 15px;
    }

    .modal-footer {
        border-top: 1px solid #dee2e6;
        padding: 15px;
        background-color: #f8f9fa; /* Light background color for footer */
        border-bottom-left-radius: 5px;
        border-bottom-right-radius: 5px;
        display: flex;
        justify-content: flex-end; /* Align buttons to the right */
    }

    .close {
        font-size: 1.5rem;
        font-weight: bold;
        color: #6c757d; /* Close button color */
        opacity: 0.7; /* Slightly transparent */
    }

        .close:hover {
            color: #343a40; /* Hover color */
            opacity: 1; /* Fully opaque on hover */
        }

    .form-control {
        border-radius: 3px;
    }

    .btn-primary {
        background-color: #007bff; /* Primary button color */
        border-color: #007bff;
    }

        .btn-primary:hover {
            background-color: #0069d9; /* Darker shade on hover */
            border-color: #0062cc;
        }

    .btn-secondary {
        background-color: #6c757d; /* Secondary button color */
        border-color: #6c757d;
    }

        .btn-secondary:hover {
            background-color: #5a6268; /* Darker shade on hover */
            border-color: #545b62;
        }
</style>
