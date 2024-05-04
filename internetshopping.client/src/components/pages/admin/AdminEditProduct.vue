<template>
    <div class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Редактировать товар</h2>
                    <button type="button" class="close" @click="closeModal">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <form @submit.prevent="saveChanges">
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="title">Название:</label>
                            <input type="text" class="form-control" id="title" v-model="editedProduct.title">
                        </div>
                        <div class="form-group">
                            <label for="category">Категория:</label>
                            <input type="text" class="form-control" id="category" v-model="editedProduct.category.title">
                        </div>
                        <div class="form-group">
                            <label for="producer">Производитель:</label>
                            <input type="text" class="form-control" id="producer" v-model="editedProduct.producer">
                        </div>
                        <div class="form-group">
                            <label for="supplier">Поставщик:</label>
                            <input type="text" class="form-control" id="supplier" v-model.number="editedProduct.supplier.title">
                        </div>
                        <div class="form-group">
                            <label for="content">Описание:</label>
                            <textarea class="form-control" id="content" rows="3" v-model="editedProduct.content"></textarea>
                        </div>
                        <div class="form-group">
                            <label for="price">Цена:</label>
                            <input type="number" class="form-control" id="price" v-model="editedProduct.price">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-primary">Сохранить</button>
                        <button type="button" class="btn btn-secondary" @click="closeModal">Отмена</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            product: Object
        },
        data() {
            return {
                editedProduct: {}
            };
        },
        watch: {
            product: {
                handler(newProduct) {
                    this.editedProduct = { ...newProduct };
                },
                immediate: true
            }
        },
        methods: {
            saveChanges() {
                this.$emit('save', this.editedProduct);
            },
            closeModal() {
                this.$emit('close');
            }
        }
    };
</script>

<style>
    .modal {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.5);
        display: flex;
        justify-content: center;
        align-items: center;
        z-index: 9999; /* Ensure modal is on top of other content */
    }

    .modal-dialog {
        max-width: 600px; /* Limit width for better readability */
        margin: 30px; /* Add some space around the modal */
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
